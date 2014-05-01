Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgInfo
        Inherits SqlInfo

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overrides Function SchemaLastChange() As String

            Return "SELECT MAX(modify_date) AS value FROM [sys].[all_objects] WHERE TYPE <> 'S'"
        End Function

    End Class
End Namespace