Imports System.Text
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders

Namespace Model.Slice

    Public Class Slice

#Region "Properties"

        Public Property SourceFile As String
        Public Property Source As Schema.Schema

        Private ReadOnly _queries As New List(Of Query)
        Public ReadOnly Property Queries As List(Of Query)
            Get
                Return _queries
            End Get
        End Property

#End Region

#Region "BL"

        Public Sub Execute(sourceConnector As IConnector, targetConnector As IConnector, progress As ProgressReporter, context As QueryContext)

            Dim weight As Single = 100.0F / _queries.Count

            For Each query As Query In _queries

                query.Execute(targetConnector, sourceConnector, context)

                If (progress IsNot Nothing) Then progress.Progress += weight
            Next
        End Sub

        Public Function Sql(sourceConnector As IConnector, targetConnector As IConnector, progress As ProgressReporter, context As QueryContext) As String

            Dim result As New StringBuilder()

            Dim weight As Single = 100.0F / _queries.Count

            For Each query As Query In _queries

                query.Sql(targetConnector, sourceConnector, context, result)

                If (progress IsNot Nothing) Then progress.Progress += weight
            Next

            Return result.ToString()
        End Function

#End Region

    End Class
End Namespace