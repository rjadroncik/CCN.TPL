Imports System.Data
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Base
Imports DBWrangler.Model.Schema.Datatypes

Namespace Services.SqlProviders.Oracle

    Public Class OracleExport

        Private Shared _command As IDbCommand
        Private Shared _userName As String

        Private Shared _allKeys As IDictionary(Of String, KeyUnique)

        Public Shared Function Execute(tables As IEnumerable(Of String), _
                                       userName As String, progress As ProgressReporter, connector As IConnector) As Schema

            _userName = userName
            _command = connector.CommandNew(String.Empty)

            _allKeys = New Dictionary(Of String, KeyUnique)

            Dim schema As New Schema()
            Dim sql As String = "SELECT TABLE_NAME FROM ALL_ALL_TABLES WHERE OWNER = '" & _userName & "' AND TABLE_NAME IN ("

            Dim first As Boolean = True
            For Each table As String In tables

                If (Not first) Then sql &= ", "

                sql &= "'" & table & "'"

                first = False
            Next

            Dim weight As Single = 100.0F / tables.Count

            _command.CommandText = sql & ") ORDER BY TABLE_NAME"

            Dim readerTables As IDataReader = _command.ExecuteReader()
            While (readerTables.Read())

                Dim table As New Table(schema)
                table.Name = readerTables.GetString(0)

                ReadColumns(table)
                ReadPrimaryKey(table)
                ReadUniqueKeys(table)
                ReadIndexes(table)

                schema.Tables.Add(table)

                progress.Progress += weight
            End While
            readerTables.Close()

            sql = "SELECT VIEW_NAME FROM ALL_VIEWS WHERE OWNER = '" & _userName & "' AND VIEW_NAME IN ("

            first = True
            For Each table As String In tables

                If (Not first) Then sql &= ", "

                sql &= "'" & table & "'"

                first = False
            Next

            _command.CommandText = sql & ") ORDER BY VIEW_NAME"

            Dim readerViews As IDataReader = _command.ExecuteReader()
            While (readerViews.Read())

                Dim table As New Table(schema)
                table.Name = readerViews.GetString(0)

                ReadColumns(table)

                schema.Tables.Add(table)

                progress.Progress += weight
            End While
            readerViews.Close()

            'Foreign keys are dependant on tables, so they need to be created after all tables have been constructed
            For Each table As Table In schema.Tables

                ReadForeignKeys(table)
            Next

            CoerceRelatedKeyDataTypes(schema)

            Return schema
        End Function

        Protected Shared Sub ReadColumns(table As Table)

            _command.CommandText = "SELECT COLUMN_NAME, NULLABLE, DATA_TYPE, DATA_PRECISION, DATA_SCALE, DATA_LENGTH" & _
                                   " FROM ALL_TAB_COLUMNS" & _
                                   " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "'"

            Dim readerColumns As IDataReader = _command.ExecuteReader()
            While (readerColumns.Read())

                Dim column As New Column(table, MatchDatatype(readerColumns.GetString(2), _
                                                              If(readerColumns.IsDBNull(3), Nothing, New Integer?(CType(readerColumns.GetDecimal(3), Integer))), _
                                                              If(readerColumns.IsDBNull(4), Nothing, New Integer?(CType(readerColumns.GetDecimal(4), Integer))), _
                                                              CType(readerColumns.GetDecimal(5), Integer)))

                column.Name = readerColumns.GetString(0)
                column.Nullable = readerColumns.GetString(1) = "Y"

                If ((column.DataType IsNot Nothing)) Then table.Columns.Add(column)
            End While
            readerColumns.Close()
        End Sub

        Protected Shared Sub ReadPrimaryKey(table As Table)

            _command.CommandText = "SELECT CONSTRAINT_NAME" & _
                                   " FROM ALL_CONSTRAINTS" & _
                                   " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_TYPE = 'P'"

            Dim readerPrimaryKey As IDataReader = _command.ExecuteReader()
            While (readerPrimaryKey.Read())

                Dim incomplete = False
                Dim primaryKey As New KeyPrimary(table)
                primaryKey.Name = readerPrimaryKey.GetString(0)

                _command.CommandText = "SELECT COLUMN_NAME" & _
                                       " FROM ALL_CONS_COLUMNS" & _
                                       " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_NAME = '" & primaryKey.Name & "' ORDER BY POSITION ASC"

                Dim readerPrimaryKeyColumns As IDataReader = _command.ExecuteReader()
                While (readerPrimaryKeyColumns.Read())

                    Dim column As Column = table.ColumnNamed(readerPrimaryKeyColumns.GetString(0))

                    If (column Is Nothing) Then incomplete = True : Exit While
                    primaryKey.Columns.Add(column)

                End While
                readerPrimaryKeyColumns.Close()

                If (Not incomplete) Then

                    table.PrimaryKey = primaryKey
                    _allKeys.Add(table.PrimaryKey.Name, table.PrimaryKey)
                End If
            End While
            readerPrimaryKey.Close()
        End Sub

        Protected Shared Sub ReadUniqueKeys(table As Table)

            _command.CommandText = "SELECT CONSTRAINT_NAME" & _
                                   " FROM ALL_CONSTRAINTS" & _
                                   " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_TYPE = 'U'"

            Dim readerUniqueKeys As IDataReader = _command.ExecuteReader()
            While (readerUniqueKeys.Read())

                Dim incomplete = False
                Dim uniqueKey As New KeyUnique(table)
                uniqueKey.Name = readerUniqueKeys.GetString(0)

                _command.CommandText = "SELECT COLUMN_NAME" & _
                                       " FROM ALL_CONS_COLUMNS" & _
                                       " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_NAME = '" & uniqueKey.Name & "' ORDER BY POSITION ASC"

                Dim readerUniqueKeyColumns As IDataReader = _command.ExecuteReader()
                While (readerUniqueKeyColumns.Read())

                    Dim column As Column = table.ColumnNamed(readerUniqueKeyColumns.GetString(0))

                    If (column Is Nothing) Then incomplete = True : Exit While
                    uniqueKey.Columns.Add(column)

                End While
                readerUniqueKeyColumns.Close()

                If (Not incomplete) Then

                    table.UniqueKeys.Add(uniqueKey)
                    _allKeys.Add(uniqueKey.Name, uniqueKey)
                End If

            End While
            readerUniqueKeys.Close()
        End Sub

        Protected Shared Sub ReadIndexes(table As Table)

            _command.CommandText = "SELECT INDEX_NAME, UNIQUENESS" & _
                                   " FROM ALL_INDEXES" & _
                                   " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "'"

            Dim readerIndexes As IDataReader = _command.ExecuteReader()
            While (readerIndexes.Read())

                Dim name As String = readerIndexes.GetString(0)

                'Dont want to duplicate primary key as it is an index aswell as a constraint
                If ((table.PrimaryKey IsNot Nothing) AndAlso (name = table.PrimaryKey.Name)) Then Continue While

                'Dont want to duplicate unique keys as they are indexes aswell as constraints
                For Each key In table.UniqueKeys

                    If (name = key.Name) Then Continue While
                Next

                Dim incomplete = False
                Dim index As New Index(table)
                index.Name = readerIndexes.GetString(0)
                index.Unique = If(readerIndexes.GetString(1) = "UNIQUE", True, False)

                _command.CommandText = "SELECT COLUMN_NAME" & _
                                       " FROM ALL_IND_COLUMNS" & _
                                       " WHERE INDEX_OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND INDEX_NAME = '" & index.Name & "'"

                Dim readerIndexColumns As IDataReader = _command.ExecuteReader()
                While (readerIndexColumns.Read())

                    Dim column As Column = table.ColumnNamed(readerIndexColumns.GetString(0))

                    If (column Is Nothing) Then incomplete = True : Exit While
                    index.Columns.Add(column)

                End While
                readerIndexColumns.Close()

                If (Not incomplete) Then table.Indexes.Add(index)
            End While
            readerIndexes.Close()
        End Sub

        Protected Shared Sub ReadForeignKeys(table As Table)

            _command.CommandText = "SELECT CONSTRAINT_NAME, R_CONSTRAINT_NAME" & _
                                   " FROM ALL_CONSTRAINTS" & _
                                   " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_TYPE = 'R'"

            Dim readerForeignKeys As IDataReader = _command.ExecuteReader()
            While (readerForeignKeys.Read())

                'Skip references to tables outside of the exported scope
                If (Not _allKeys.ContainsKey(readerForeignKeys.GetString(1))) Then Continue While

                Dim incomplete = False
                Dim foreignKey As New KeyForeign(table)
                foreignKey.Name = readerForeignKeys.GetString(0)
                foreignKey.ReferencedKey = _allKeys(readerForeignKeys.GetString(1))

                _command.CommandText = "SELECT COLUMN_NAME" & _
                                       " FROM ALL_CONS_COLUMNS" & _
                                       " WHERE OWNER = '" & _userName & "' AND TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_NAME = '" & foreignKey.Name & "' ORDER BY POSITION ASC"

                Dim readerConstraintColumns As IDataReader = _command.ExecuteReader()
                While (readerConstraintColumns.Read())

                    Dim column As Column = table.ColumnNamed(readerConstraintColumns.GetString(0))

                    If (column Is Nothing) Then incomplete = True : Exit While
                    'TODO: [DBWrangler] export je este po starom - vyzaduje zhodu nazvov stlpcov v klucoch
                    foreignKey.Columns.Add(column, foreignKey.Table.ColumnNamed(column.Name))

                End While
                readerConstraintColumns.Close()

                If (Not incomplete) Then table.ForeignKeys.Add(foreignKey)

            End While
            readerForeignKeys.Close()
        End Sub

        Protected Shared Sub CoerceRelatedKeyDataTypes(schema As Schema)

            Dim columnKeys As New Dictionary(Of Column, List(Of KeyForeign))

            For Each table As Table In schema.Tables

                For Each foreignKey As KeyForeign In table.ForeignKeys

                    For Each column As Column In foreignKey.Columns.Keys

                        If (columnKeys.ContainsKey(column)) Then

                            columnKeys(column).Add(foreignKey)
                        Else
                            Dim list As New List(Of KeyForeign)
                            list.Add(foreignKey)

                            columnKeys.Add(column, list)
                        End If
                    Next

                    For Each column As Column In foreignKey.ReferencedKey.Columns

                        If (columnKeys.ContainsKey(column)) Then

                            columnKeys(column).Add(foreignKey)
                        Else
                            Dim list As New List(Of KeyForeign)
                            list.Add(foreignKey)

                            columnKeys.Add(column, list)
                        End If
                    Next
                Next
            Next

            Dim changeOccurred As Boolean = True
            While (changeOccurred)

                changeOccurred = False
                For Each column As Column In columnKeys.Keys

                    For Each foreignKey As KeyForeign In columnKeys(column)

                        For Each columnFK As Column In foreignKey.Columns.Keys

                            Dim columnRK As Column = foreignKey.Columns(columnFK)

                            If ((TypeOf columnFK.DataType Is DtDecimal) AndAlso (TypeOf columnRK.DataType Is DtDecimal)) Then

                                Dim datatypeFK As DtDecimal = DirectCast(columnFK.DataType, DtDecimal)
                                Dim datatypeRK As DtDecimal = DirectCast(columnRK.DataType, DtDecimal)

                                If (datatypeFK.Precision < datatypeRK.Precision) Then

                                    changeOccurred = True
                                    datatypeFK.Precision = datatypeRK.Precision

                                ElseIf (datatypeFK.Precision > datatypeRK.Precision) Then

                                    changeOccurred = True
                                    datatypeRK.Precision = datatypeFK.Precision
                                End If

                                If (datatypeFK.Scale < datatypeRK.Scale) Then

                                    changeOccurred = True
                                    datatypeFK.Scale = datatypeRK.Scale

                                ElseIf (datatypeFK.Scale > datatypeRK.Scale) Then

                                    changeOccurred = True
                                    datatypeRK.Scale = datatypeFK.Scale
                                End If

                                Continue For
                            End If

                            If ((TypeOf columnFK.DataType Is DtDecimal) AndAlso (TypeOf columnRK.DataType Is DtDouble)) Then

                                columnFK.DataType = New DtDouble()

                                changeOccurred = True
                                Continue For
                            End If

                            If ((TypeOf columnFK.DataType Is DtDouble) AndAlso (TypeOf columnRK.DataType Is DtDecimal)) Then

                                columnRK.DataType = New DtDouble()

                                changeOccurred = True
                                Continue For
                            End If
                        Next
                    Next
                Next
            End While
        End Sub

        Protected Shared Function MatchDatatype(type As String, precision As Integer?, scale As Integer?, length As Integer) As DataType

            Select Case type

                Case "INTEGER"
                    Return New DtInt32()
                Case "NUMBER"
                    Return New DtDecimal(precision, scale)
                Case "CHAR"
                    Return If(length = 1, DirectCast(New DTChar(), DataType), New DtString(length, True))
                Case "VARCHAR2"
                    Return New DtString(length, False)
                Case "DATE"
                    Return New DtDate()
            End Select

            If (type.StartsWith("TIMESTAMP")) Then

                If (type.EndsWith(")")) Then

                    Dim precisionParsed = Integer.Parse(type.Substring(type.IndexOf("("c) + 1, type.Length - 2 - type.IndexOf("("c)))

                    Return New DtTimestamp(precisionParsed)
                Else
                    Return New DtTimestamp(precision)
                End If
            End If

            Return Nothing
        End Function
    End Class
End Namespace