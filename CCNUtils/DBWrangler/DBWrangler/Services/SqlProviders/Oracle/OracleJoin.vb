
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Connectors

Public Class OracleJoin
    Inherits SqlJoin

    Public Sub New(connector As IConnector)
        MyBase.New(connector)

        _scriptStart = "BEGIN" + Environment.NewLine
        _scriptEnd = Environment.NewLine + "END;"
    End Sub

End Class
