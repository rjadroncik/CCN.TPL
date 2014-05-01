Imports DBWrangler.Connectors

Namespace Services.SqlProviders.Common

    Public MustInherit Class SqlInfo
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public MustOverride Function SchemaLastChange() As String

    End Class
End Namespace