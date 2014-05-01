
Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Oracle

    Public Class OracleTruncate
        Inherits SqlTruncate

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _scriptStart = "DECLARE" + Environment.NewLine + "BEGIN" + Environment.NewLine
            _scriptEnd = Environment.NewLine + "END;"

            _commandStart = "EXECUTE IMMEDIATE '"
            _commandEnd = "';" & Environment.NewLine
        End Sub
    End Class
End Namespace