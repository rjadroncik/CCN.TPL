Imports System.Text
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders

Namespace Model.Slice

    Public MustInherit Class Query

#Region "Properties"

        Public Property Source As Table

#End Region

#Region "Must Override"

        Public MustOverride Sub Execute(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext)

        Public MustOverride Sub Sql(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext, result As StringBuilder)
#End Region

    End Class
End Namespace