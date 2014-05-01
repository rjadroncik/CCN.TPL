Imports System.Text
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Services.SqlProviders

Namespace Model.Slice

    Public Class QueryCondition
        Inherits Query

#Region "Properties"

        Public Property Condition As Condition

#End Region

#Region "Overridden"

        Public Overrides Sub Execute(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext)

            targetConnector.SqlInsert.Execute(Source, False, sourceConnector.SqlSelect.Execute(Source, Condition), context)
        End Sub

        Public Overrides Sub Sql(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext, result As StringBuilder)

            targetConnector.SqlInsert.Sql(Source, False, sourceConnector.SqlSelect.Execute(Source, Condition), result, context)
        End Sub

#End Region

    End Class
End Namespace