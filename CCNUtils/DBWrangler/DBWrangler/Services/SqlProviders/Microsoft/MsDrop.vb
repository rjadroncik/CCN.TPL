﻿
Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Microsoft

    Public Class MsDrop
        Inherits SqlDrop

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _escStart = "["
            _escEnd = "]"
        End Sub

    End Class
End Namespace