Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Oracle

    Public Class OracleSelect
        Inherits SqlSelect

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _scriptStart = "BEGIN" + Environment.NewLine
            _scriptEnd = Environment.NewLine + "END;"
        End Sub

        Public Overrides Function SelectTable(table As Table) As String

            Return "SELECT " & ColumnList(table.Columns) & " FROM " & table.Name
        End Function

        Public Overrides Function SelectTable(table As Table, condition As Condition) As String

            Return SelectTable(table) & " " & condition.ToSql(_connector)
        End Function

        Public Overrides Function Execute(table As Table) As IDataReader

            Return _connector.CommandNew(SelectTable(table)).ExecuteReader()
        End Function

        Public Overrides Function Execute(table As Table, condition As Condition) As IDataReader

            Return _connector.CommandNew(SelectTable(table, condition)).ExecuteReader()
        End Function

    End Class
End Namespace