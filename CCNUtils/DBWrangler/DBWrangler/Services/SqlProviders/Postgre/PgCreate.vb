
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgCreate
        Inherits SqlCreate

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overrides Function PrimaryKey(table As Table) As String

            Return "CONSTRAINT " & _escStart & table.PrimaryKey.Name & _escEnd & " PRIMARY KEY" & _
                   " (" & ColumnList(table.PrimaryKey.Columns) & ")"
        End Function

        Protected Overrides Function UniqueKey(key As KeyUnique) As String

            Return "CONSTRAINT " & _escStart & key.Name & _escEnd & " UNIQUE" & If(key.Clustered, " CLUSTERED", " NONCLUSTERED") & " (" & ColumnList(key.Columns) & ")"
        End Function

        Public Overrides Function CreatePrimaryKey(key As KeyPrimary) As String

            If ((key.Table.PrimaryKey Is Nothing) OrElse (key.Table.PrimaryKey.Columns.IsEmpty())) Then Return ""

            Return "ALTER TABLE " & _escStart & key.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & _
                   key.Table.PrimaryKey.Name & _escEnd & " PRIMARY KEY" & _
                   " (" & ColumnList(key.Table.PrimaryKey.Columns) & ");" & Environment.NewLine
        End Function

        Protected Overrides Function CreateUniqueKey(uniqueKey As KeyUnique) As String

            Return "ALTER TABLE " & _escStart & uniqueKey.Table.Name & _escEnd & " ADD CONSTRAINT " & _escStart & uniqueKey.Name & _
                   _escEnd & " UNIQUE" & _
                   " (" & ColumnList(uniqueKey.Columns) & ");" & Environment.NewLine
        End Function

        Public Overrides Function CreateIndexes(table As Table) As String

            Dim sql As String = ""

            For Each index As Index In table.Indexes

                sql &= "CREATE "

                If (index.Unique) Then sql &= "UNIQUE "

                sql &= "INDEX " & _escStart & index.Name & _escEnd & " ON " & _escStart & table.Name & _escEnd & " (" & ColumnList(index.Columns) & ");" & Environment.NewLine
            Next

            Return sql
        End Function

    End Class
End Namespace