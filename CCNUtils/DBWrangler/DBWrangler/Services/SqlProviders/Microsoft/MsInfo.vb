Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Microsoft

    Public Class MsInfo
        Inherits SqlInfo

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _escStart = "["
            _escEnd = "]"
        End Sub

        Public Overrides Function SchemaLastChange() As String

            Return "SELECT MAX(modify_date) AS value FROM [sys].[all_objects] WHERE TYPE <> 'S'"
        End Function

    End Class
End Namespace