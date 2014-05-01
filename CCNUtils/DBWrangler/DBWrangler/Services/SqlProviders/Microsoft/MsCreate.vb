
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Microsoft

    Public Class MsCreate
        Inherits SqlCreate

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _escStart = "["
            _escEnd = "]"
        End Sub

        Protected Overrides Function PrimaryKey(table As Table) As String

            Return "CONSTRAINT " & _escStart & table.PrimaryKey.Name & _escEnd & " PRIMARY KEY" & If(table.PrimaryKey.Clustered, " CLUSTERED", " NONCLUSTERED") & _
                   " (" & ColumnList(table.PrimaryKey.Columns) & ")"
        End Function

        Protected Overrides Function UniqueKey(key As KeyUnique) As String

            Return "CONSTRAINT " & _escStart & key.Name & _escEnd & " UNIQUE" & If(key.Clustered, " CLUSTERED", " NONCLUSTERED") & " (" & ColumnList(key.Columns) & ")"
        End Function

        Public Overrides Function CreatePrimaryKey(key As KeyPrimary) As String

            If ((key.Table.PrimaryKey Is Nothing) OrElse (key.Table.PrimaryKey.Columns.IsEmpty())) Then Return ""

            Return "IF OBJECT_ID('" & key.Table.PrimaryKey.Name & "','PK') IS NULL ALTER TABLE " & _escStart & key.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & _
                   key.Table.PrimaryKey.Name & _escEnd & " PRIMARY KEY" & If(key.Table.PrimaryKey.Clustered, " CLUSTERED", " NON CLUSTERED") & _
                   " (" & ColumnList(key.Table.PrimaryKey.Columns) & ")" & _commandEnd
        End Function

        Protected Overrides Function CreateUniqueKey(key As KeyUnique) As String

            Return "ALTER TABLE " & _escStart & key.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & key.Name & _
                   _escEnd & " UNIQUE" & If(key.Clustered, " CLUSTERED", " NON CLUSTERED") & _
                   " (" & ColumnList(key.Columns) & ")" & _commandEnd
        End Function

        Public Overrides Function CreateUniqueKeys(table As Table) As String

            Dim sql As String = ""

            For Each key In table.UniqueKeys

                sql &= "IF OBJECT_ID('" & key.Name & "','UQ') IS NULL " & CreateUniqueKey(key)
            Next

            Return sql
        End Function

        Public Overrides Function CreateForeignKeys(table As Table) As String

            Dim sql As String = ""

            For Each key In table.ForeignKeys

                sql &= "IF OBJECT_ID('" & key.Name & "','F') IS NULL " & CreateForeignKey(key)
            Next

            Return sql
        End Function

        Public Overrides Function CreateIndexes(table As Table) As String

            Dim sql As String = ""

            For Each index In table.Indexes

                sql &= "IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = '" & index.Name & "') CREATE "

                If (index.Unique) Then sql &= "UNIQUE "

                sql &= If(index.Clustered, "CLUSTERED ", "NONCLUSTERED ")

                sql &= "INDEX " & _escStart & index.Name & _escEnd & " ON " & _escStart & table.Name & _escEnd & " (" & ColumnList(index.Columns) & ");" & Environment.NewLine
            Next

            Return sql
        End Function
    End Class
End Namespace