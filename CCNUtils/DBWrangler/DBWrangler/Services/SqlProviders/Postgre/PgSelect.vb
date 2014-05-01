
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgSelect
        Inherits SqlSelect

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overrides Function SelectTable(table As Table) As String

            Return "SELECT " & ColumnList(table.Columns) & " FROM " & table.Name
        End Function

        Public Overrides Function SelectTable(table As Table, condition As Condition) As String

            Return SelectTable(table) & " " & condition.ToSql(_connector)
        End Function

    End Class
End Namespace