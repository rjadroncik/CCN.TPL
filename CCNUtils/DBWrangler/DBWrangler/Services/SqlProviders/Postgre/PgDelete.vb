
Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgDelete
        Inherits SqlDelete

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

    End Class
End Namespace