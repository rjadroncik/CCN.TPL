Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Oracle

    Public Class OracleInfo
        Inherits SqlInfo

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _scriptStart = "BEGIN" + Environment.NewLine
            _scriptEnd = Environment.NewLine + "END;"
        End Sub

        Public Overrides Function SchemaLastChange() As String

            Return "SELECT MAX(modify_date) FROM [sys].[all_objects] WHERE TYPE <> 'S'"
        End Function

    End Class
End Namespace