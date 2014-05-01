Imports System.Text
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders

Namespace Model.Slice

    Public Class QueryJoin
        Inherits Query

#Region "Properties"

        Public Property Target As Table

        Private ReadOnly _columnMap As New Dictionary(Of Column, Column)
        Public ReadOnly Property ColumnMap As Dictionary(Of Column, Column)
            Get
                Return _columnMap
            End Get
        End Property

#End Region

#Region "Overridden"

        Public Overrides Sub Execute(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext)

            Dim where As String = targetConnector.SqlSelect.QueryWhere(Target, context, "t")
            If (where IsNot Nothing) Then

                Dim cmd As IDbCommand = sourceConnector.CommandNew(sourceConnector.SqlJoin.QuerySelectCustom(Me) & where)

                targetConnector.SqlInsertFiltered.Execute(Source, False, cmd.ExecuteReader(), context)
            End If
        End Sub

        Public Overrides Sub Sql(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext, result As StringBuilder)

            Dim where As String = sourceConnector.SqlSelect.QueryWhere(Target, context, "t")
            If (where IsNot Nothing) Then

                Dim cmd As IDbCommand = sourceConnector.CommandNew(sourceConnector.SqlJoin.QuerySelectCustom(Me) & where)

                targetConnector.SqlInsertFiltered.Sql(Source, False, cmd.ExecuteReader(), result, context)
            End If
        End Sub

#End Region

    End Class
End Namespace