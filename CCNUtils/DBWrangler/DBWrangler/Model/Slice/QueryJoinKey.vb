
Imports DBWrangler.Model.Schema

Namespace Model.Slice

    Public Class QueryJoinKey
        Inherits QueryJoin

#Region "Properties"

        Public Property ForeignKey As KeyForeign

#End Region

#Region "Overriden"

        'Public Overrides Sub Execute(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext)

        '    Dim where As String = targetConnector.SqlJoin.QueryWhere(Me, targetSchema.TableNamed(Target.Name + suffix))
        '    If (where IsNot Nothing) Then

        '        Dim cmd As IDbCommand = sourceConnector.CommandNew(sourceConnector.SqlJoin.QuerySelect(Me) & where)

        '        targetConnector.SqlSliceInsert.Execute(targetSchema.TableNamed(Source.Name + suffix), False, cmd.ExecuteReader())
        '    End If
        'End Sub

        'Public Overrides Sub Sql(targetConnector As IConnector, sourceConnector As IConnector, context As QueryContext, result As StringBuilder)

        '    Dim where As String = targetConnector.SqlJoin.QueryWhere(Me, targetSchema.TableNamed(Target.Name + suffix))
        '    If (where IsNot Nothing) Then

        '        Dim cmd As IDbCommand = sourceConnector.CommandNew(sourceConnector.SqlJoin.QuerySelect(Me) & where)

        '        targetConnector.SqlSliceInsert.Sql(targetSchema.TableNamed(Source.Name + suffix), False, cmd.ExecuteReader(), result, ids)
        '    End If
        'End Sub

#End Region

    End Class
End Namespace