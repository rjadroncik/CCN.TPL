
Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgTruncate
        Inherits SqlTruncate

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

    End Class
End Namespace