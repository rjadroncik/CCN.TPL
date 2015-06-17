
Imports System.Text
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlCreate
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overridable Function CreateTable(table As Table, primaryKeys As Boolean, uniqueKeys As Boolean, _
                                                createIdentities As Boolean) As String

            Dim sql As String = _commandStart & "CREATE TABLE " & _escStart & table.Name & _escEnd & " ("

            Dim first As Boolean = True
            For Each column As Column In table.Columns

                If (Not first) Then sql &= ", "

                sql &= _escStart & column.Name & _escEnd & " " & column.DataType.ToSql(_connector)

                If (createIdentities AndAlso column.Identity) Then sql &= " IDENTITY(" & column.IdentitySeed & ", " & column.IdentityIncrement & ")"

                If (column.Nullable) Then

                    sql &= " NULL"
                Else
                    sql &= " NOT NULL"
                End If

                first = False
            Next

            If (primaryKeys AndAlso (table.PrimaryKey IsNot Nothing) AndAlso table.PrimaryKey.Columns.Any()) Then sql &= ", " & PrimaryKey(table)

            If (uniqueKeys) Then

                For Each key As KeyUnique In table.UniqueKeys

                    sql &= ", " & UniqueKey(key)
                Next
            End If

            Return sql & ")" & _commandEnd
        End Function

        Protected Overridable Function PrimaryKey(table As Table) As String

            Return "CONSTRAINT " & _escStart & table.PrimaryKey.Name & _escEnd & " PRIMARY KEY" & _
                   " (" & ColumnList(table.PrimaryKey.Columns) & ")"
        End Function

        Protected Overridable Function UniqueKey(key As KeyUnique) As String

            Return "CONSTRAINT " & _escStart & key.Name & _escEnd & " UNIQUE" & " (" & ColumnList(key.Columns) & ")"
        End Function

        Public Overridable Function CreatePrimaryKey(key As KeyPrimary) As String

            If ((key.Table.PrimaryKey Is Nothing) OrElse (key.Table.PrimaryKey.Columns.IsEmpty())) Then Return ""

            Return _commandStart & "ALTER TABLE " & _escStart & key.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & _
                   key.Table.PrimaryKey.Name & _escEnd & " PRIMARY KEY" & _
                   " (" & ColumnList(key.Table.PrimaryKey.Columns) & ")" & _commandEnd
        End Function

        Protected Overridable Function CreateUniqueKey(uniqueKey As KeyUnique) As String

            Return _commandStart & "ALTER TABLE " & _escStart & uniqueKey.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & uniqueKey.Name & _
                   _escEnd & " UNIQUE" & " (" & ColumnList(uniqueKey.Columns) & ")" & _commandEnd
        End Function

        Public Overridable Function CreateUniqueKeys(table As Table) As String

            Dim sql As String = ""

            For Each key In table.UniqueKeys

                sql &= CreateUniqueKey(key)
            Next

            Return sql
        End Function

        Public Overridable Function CreateForeignKey(foreignKey As KeyForeign) As String

            Return _commandStart & "ALTER TABLE " & _escStart & foreignKey.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & foreignKey.Name & _escEnd & " FOREIGN KEY (" & _
                   ColumnList(foreignKey.Columns.Keys) & ") REFERENCES " & _escStart & foreignKey.ReferencedKey.Table.Name & _escEnd & " (" & _
                   ColumnList(foreignKey.Columns.Values) & ")" & _commandEnd
        End Function

        Public Overridable Function CreateForeignKeys(table As Table) As String

            Dim sql As String = ""

            For Each key In table.ForeignKeys

                sql &= CreateForeignKey(key)
            Next

            Return sql
        End Function

        Public Overridable Function CreateIndex(index As Index) As String

            Dim sql As String = ""

            sql &= _commandStart & "CREATE "

            If (index.Unique) Then sql &= "UNIQUE "

            sql &= "INDEX " & _escStart & index.Name & _escEnd & " ON " & _escStart & index.Table.Name & _escEnd & " (" & ColumnList(index.Columns) & ")" & _commandEnd

            Return sql
        End Function

        Public Overridable Function CreateIndexes(table As Table) As String

            Dim sql As String = ""

            For Each index As Index In table.Indexes

                sql &= CreateIndex(index)
            Next

            Return sql
        End Function

        Public Overridable Function Sql(schema As Schema, primaryKeys As Boolean, foreignKeys As Boolean, uniqueKeys As Boolean, _
                                        identities As Boolean, indexes As Boolean, Optional skipTables As Boolean = False) As String

            Dim result As New StringBuilder()

            For Each table As Table In schema.Tables

                If (Not skipTables) Then result.Append(CreateTable(table, primaryKeys, uniqueKeys, identities))

                If (indexes) Then result.Append(CreateIndexes(table))
            Next

            If (foreignKeys) Then

                For Each table As Table In schema.Tables

                    result.Append(CreateForeignKeys(table))
                Next
            End If

            Return result.ToString()
        End Function

        Public Overridable Sub Execute(schema As Schema, primaryKeys As Boolean, foreignKeys As Boolean, uniqueKeys As Boolean, _
                                       identities As Boolean, indexes As Boolean, Optional skipTables As Boolean = False)

            ExecuteNonQuery(Sql(schema, primaryKeys, foreignKeys, uniqueKeys, identities, indexes))
        End Sub

        Public Overridable Sub Execute(table As Table, primaryKeys As Boolean, uniqueKeys As Boolean, _
                                       identities As Boolean, indexes As Boolean, Optional skipTables As Boolean = False)

            Dim sql = CreateTable(table, primaryKeys, uniqueKeys, identities)

            If (skipTables) Then sql = String.Empty
            If (indexes) Then sql &= CreateIndexes(table)

            ExecuteNonQuery(sql)
        End Sub

        Public Overridable Sub Execute(key As KeyPrimary)

            ExecuteNonQuery(CreatePrimaryKey(key))
        End Sub

        Public Overridable Sub Execute(key As KeyForeign)

            ExecuteNonQuery(CreateForeignKey(key))
        End Sub

        Public Overridable Sub Execute(key As KeyUnique)

            ExecuteNonQuery(CreateUniqueKey(key))
        End Sub

        Public Overridable Sub Execute(key As Index)

            ExecuteNonQuery(CreateIndex(key))
        End Sub

    End Class
End Namespace