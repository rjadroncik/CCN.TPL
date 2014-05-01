﻿
Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Oracle

    Public Class OracleDelete
        Inherits SqlDelete

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _scriptStart = "BEGIN" + Environment.NewLine
            _scriptEnd = Environment.NewLine + "END;"
        End Sub

    End Class
End Namespace