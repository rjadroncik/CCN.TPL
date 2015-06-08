Imports System
Imports System.Data
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Base
Imports DBWrangler.Model.Schema.Datatypes

Namespace Services.SqlProviders.Postgre

    Public Class PgExport

        Protected Shared _connector As IConnector
        Protected Shared _allKeys As Dictionary(Of String, KeyUnique)

        Public Shared Function Execute(tables As IEnumerable(Of String), _
                                       progress As ProgressReporter, connector As IConnector) As Schema

            _connector = connector
            _allKeys = New Dictionary(Of String, KeyUnique)

            Dim weight As Single = 100.0F / If(tables Is Nothing, 1, tables.Count)

            Dim schema As New Schema()
            Dim sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES" &
                      If(tables Is Nothing, "", " WHERE TABLE_NAME IN (" & Converting.Values2String(tables, Function(x) "'" & x & "'") & ")") & _
                      " ORDER BY TABLE_NAME"

            Using command As IDbCommand = _connector.CommandNew(sql), readerTables As IDataReader = command.ExecuteReader()

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
            End Using

            'Foreign keys are dependant on tables, so they need to be created after all tables have been constructed
            For Each table As Table In schema.Tables

                ReadForeignKeys(table)
            Next

            Return schema
        End Function

        Protected Shared Sub ReadColumns(table As Table)

            Dim sql = "SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, NUMERIC_PRECISION, NUMERIC_SCALE, CHARACTER_MAXIMUM_LENGTH" & _
                      " FROM INFORMATION_SCHEMA.COLUMNS" & _
                      " WHERE TABLE_NAME = '" & table.Name & "'"

            Using command As IDbCommand = _connector.CommandNew(sql), readerColumns As IDataReader = command.ExecuteReader()

                While (readerColumns.Read())

                    Dim column As New Column(table, MatchDatatype(readerColumns.GetString(2), _
                                                                  If(readerColumns.IsDBNull(3), Nothing, New Integer?(readerColumns.GetByte(3))), _
                                                                  If(readerColumns.IsDBNull(4), Nothing, New Integer?(readerColumns.GetInt32(4))), _
                                                                  If(readerColumns.IsDBNull(5), Nothing, New Integer?(readerColumns.GetInt32(5)))))

                    column.Name = readerColumns.GetString(0)
                    column.Nullable = readerColumns.GetString(1) = "YES"

                    If ((column.DataType IsNot Nothing)) Then table.Columns.Add(column)
                End While
            End Using

            sql = "SELECT ic.name, ic.seed_value, ic.increment_value" & _
                  " FROM sys.identity_columns ic" & _
                  " JOIN sys.tables t ON ic.object_id = t.object_id" & _
                  " WHERE t.name = '" & table.Name & "'"

            Using command As IDbCommand = _connector.CommandNew(sql), readerColumns As IDataReader = command.ExecuteReader()

                While (readerColumns.Read())

                    Dim column As Column = table.ColumnNamed(readerColumns.GetString(0))

                    column.Identity = True
                    column.IdentitySeed = CInt(column.DataType.Read(readerColumns, New Column("seed_value"), _connector))
                    column.IdentityIncrement = CInt(column.DataType.Read(readerColumns, New Column("increment_value"), _connector))
                End While
            End Using
        End Sub

        Protected Shared Function MatchDatatype(type As String, precision As Integer?, scale As Integer?, length As Integer?) As DataType

            Select Case type

                Case "tinyint"
                    Return New DtByte()

                Case "smallint"
                    Return New DtInt16()
                Case "int", "integer", "serial"
                    Return New DtInt32()

                Case "bigint", "timestamp", "bigserial"
                    Return New DtInt64()

                Case "uniqueidentifier"
                    Return New DtGuid()

                Case "float"
                    Return New DtDouble()
                Case "real"
                    Return New DtSingle()

                Case "decimal", "numeric"
                    Return New DtDecimal(precision.Value, If(Not scale.HasValue, 0, scale.Value))
                Case "money"
                    Return New DtDecimal(precision.Value, If(Not scale.HasValue, 0, scale.Value), True)

                Case "nchar"
                    Return If(length = 1, DirectCast(New DTChar(), DataType), New DtString(length.Value, True))
                Case "char"
                    Return If(length = 1, DirectCast(New DTChar(False), DataType), New DtString(length.Value, True, False))

                Case "nvarchar", "ntext"
                    Return New DtString(length.Value, False)
                Case "varchar", "text"
                    Return New DtString(length.Value, False, False)

                Case "date", "datetime"
                    Return New DtDate()

                Case "smalldatetime"
                    Return New DtDate(True)

                Case "bit"
                    Return New DtBoolean()

                Case "image", "binary", "varbinary"
                    Return New DtByteArray()

                Case "sql_variant"
                    Return New DtVariant()
            End Select

            Throw New InvalidCastException("Cant map DB datatype: " & type)
        End Function

        Protected Shared Sub ReadPrimaryKey(table As Table)

            Dim sql = "SELECT i.name, i.type_desc" & _
                      " FROM sys.indexes i" & _
                      " JOIN sys.tables t ON i.object_id = t.object_id" & _
                      " WHERE is_primary_key = 1 AND t.name = '" & table.Name & "'"

            Using command As IDbCommand = _connector.CommandNew(sql), readerPrimaryKey As IDataReader = command.ExecuteReader()

                While (readerPrimaryKey.Read())

                    Dim incomplete = False
                    Dim primaryKey As New KeyPrimary(table)
                    primaryKey.Name = readerPrimaryKey.GetString(0)
                    primaryKey.Clustered = (readerPrimaryKey.GetString(1) = "CLUSTERED")

                    sql = "SELECT COLUMN_NAME" & _
                          " FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE" & _
                          " WHERE TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_NAME = '" & primaryKey.Name & "' ORDER BY ORDINAL_POSITION ASC"

                    Using command2 As IDbCommand = _connector.CommandNew(sql), readerPrimaryKeyColumns As IDataReader = command2.ExecuteReader()

                        While (readerPrimaryKeyColumns.Read())

                            Dim column As Column = table.ColumnNamed(readerPrimaryKeyColumns.GetString(0))

                            If (column Is Nothing) Then incomplete = True : Exit While
                            primaryKey.Columns.Add(column)

                        End While
                    End Using

                    If (Not incomplete) Then

                        table.PrimaryKey = primaryKey
                        _allKeys.Add(table.PrimaryKey.Name, table.PrimaryKey)
                    End If
                End While
            End Using
        End Sub

        Protected Shared Sub ReadUniqueKeys(table As Table)

            Dim sql = "SELECT i.name, i.type_desc" & _
                      " FROM sys.indexes i" & _
                      " JOIN sys.tables t ON i.object_id = t.object_id" & _
                      " WHERE is_primary_key = 0 AND is_unique_constraint = 1 AND t.name = '" & table.Name & "'"

            Using command As IDbCommand = _connector.CommandNew(sql), readerUniqueKeys As IDataReader = command.ExecuteReader()

                While (readerUniqueKeys.Read())

                    Dim incomplete = False
                    Dim uniqueKey As New KeyUnique(table)
                    uniqueKey.Name = readerUniqueKeys.GetString(0)
                    uniqueKey.Clustered = (readerUniqueKeys.GetString(1) = "CLUSTERED")

                    sql = "SELECT COLUMN_NAME" & _
                          " FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE" & _
                          " WHERE TABLE_NAME = '" & table.Name & "' AND CONSTRAINT_NAME = '" & uniqueKey.Name & "' ORDER BY ORDINAL_POSITION ASC"

                    Using command2 As IDbCommand = _connector.CommandNew(sql), readerUniqueKeyColumns As IDataReader = command2.ExecuteReader()

                        While (readerUniqueKeyColumns.Read())

                            Dim column As Column = table.ColumnNamed(readerUniqueKeyColumns.GetString(0))

                            If (column Is Nothing) Then incomplete = True : Exit While
                            uniqueKey.Columns.Add(column)

                        End While
                    End Using

                    If (Not incomplete) Then

                        table.UniqueKeys.Add(uniqueKey)
                        _allKeys.Add(uniqueKey.Name, uniqueKey)
                    End If

                End While
            End Using
        End Sub

        Protected Shared Sub ReadIndexes(table As Table)

            Dim sql = "SELECT i.name, is_unique, i.type_desc" & _
                      " FROM sys.indexes i JOIN sys.tables ON i.object_id = sys.tables.object_id" & _
                      " WHERE is_primary_key = 0 AND is_unique_constraint = 0 AND i.type_desc <> 'HEAP' AND sys.tables.name = '" & table.Name & "'"

            Using command As IDbCommand = _connector.CommandNew(sql), readerIndexes As IDataReader = command.ExecuteReader()

                While (readerIndexes.Read())

                    Dim name As String = readerIndexes.GetString(0)

                    Dim index As New Index(table)
                    index.Name = readerIndexes.GetString(0)
                    index.Unique = readerIndexes.GetBoolean(1)
                    index.Clustered = (readerIndexes.GetString(2) = "CLUSTERED")

                    sql = "SELECT c.name, is_descending_key" & _
                          " FROM sys.index_columns ic" & _
                          " JOIN sys.indexes i ON ic.object_id = i.object_id AND i.index_id = ic.index_id" & _
                          " JOIN sys.tables t ON i.object_id = t.object_id" & _
                          " JOIN sys.columns c ON t.object_id = c.object_id AND ic.column_id = c.column_id" & _
                          " WHERE t.name = '" & table.Name & "' AND i.name = '" & index.Name & "'"

                    Using command2 As IDbCommand = _connector.CommandNew(sql), readerIndexColumns As IDataReader = command2.ExecuteReader()

                        While (readerIndexColumns.Read())

                            index.Columns.Add(table.ColumnNamed(readerIndexColumns.GetString(0)))
                        End While
                    End Using

                    table.Indexes.Add(index)
                End While
            End Using
        End Sub

        Protected Shared Sub ReadForeignKeys(table As Table)

            Dim sql = "SELECT c.CONSTRAINT_NAME, c.UNIQUE_CONSTRAINT_NAME" & _
                      " FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c " & _
                      " JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE u ON c.CONSTRAINT_NAME = u.CONSTRAINT_NAME" & _
                      " WHERE u.TABLE_NAME = '" & table.Name & "'"

            Using command As IDbCommand = _connector.CommandNew(sql), readerForeignKeys As IDataReader = command.ExecuteReader()

                While (readerForeignKeys.Read())

                    'Skip references to tables outside of the exported scope
                    If (Not _allKeys.ContainsKey(readerForeignKeys.GetString(1))) Then Continue While

                    Dim incomplete = False
                    Dim foreignKey As New KeyForeign(table)
                    foreignKey.Name = readerForeignKeys.GetString(0)
                    foreignKey.ReferencedKey = _allKeys(readerForeignKeys.GetString(1))

                    sql = "SELECT poc.name, roc.name" & _
                          " FROM sys.foreign_key_columns fkc" & _
                          " JOIN sys.foreign_keys fk ON fkc.constraint_object_id = fk.object_id" & _
                          " JOIN sys.tables fkt ON fkc.parent_object_id = fkt.object_id" & _
                          " JOIN sys.columns poc ON fkc.parent_object_id = poc.object_id AND fkc.parent_column_id = poc.column_id" & _
                          " JOIN sys.columns roc ON fkc.referenced_object_id = roc.object_id AND fkc.referenced_column_id = roc.column_id" & _
                          " WHERE fkt.name = '" & table.Name & "' AND fk.name = '" & foreignKey.Name & "' ORDER BY fkc.constraint_column_id ASC"

                    Using command2 As IDbCommand = _connector.CommandNew(sql), readerConstraintColumns As IDataReader = command2.ExecuteReader()

                        While (readerConstraintColumns.Read())

                            Dim column As Column = table.ColumnNamed(readerConstraintColumns.GetString(0))

                            If (column Is Nothing) Then incomplete = True : Exit While

                            foreignKey.Columns.Add(column, foreignKey.ReferencedKey.Table.ColumnNamed(readerConstraintColumns.GetString(1)))

                        End While
                    End Using

                    If (Not incomplete) Then table.ForeignKeys.Add(foreignKey)

                End While
            End Using
        End Sub

    End Class
End Namespace