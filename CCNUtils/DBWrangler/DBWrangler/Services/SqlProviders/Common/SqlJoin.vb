Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Slice

Namespace Services.SqlProviders.Common

    Public Class SqlJoin
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overridable Function SelectJoin(source As Table, target As Table, columns As List(Of Column), join As Dictionary(Of Column, Column)) As String

            Dim tableNames As New Dictionary(Of Table, String)
            tableNames.Add(source, "s")
            tableNames.Add(target, "t")

            Dim sql As String = "SELECT " & ColumnList(columns, tableNames) & " FROM " & source.Name & _
                                " s JOIN " & target.Name & " t ON "

            Dim first As Boolean = True
            For Each key As Column In join.Keys

                If (Not first) Then sql &= " AND "

                sql &= "s." & key.Name & " = t." & join(key).Name

                first = False
            Next

            Return sql
        End Function

        Public Overridable Function Execute(source As Table, target As Table, columns As List(Of Column), _
                                            join As Dictionary(Of Column, Column)) As IDataReader

            Return _connector.CommandNew(SelectJoin(source, target, columns, join)).ExecuteReader()
        End Function

        Public Overridable Function Execute(source As Table, target As Table, columns As List(Of Column), _
                                            join As Dictionary(Of Column, Column), condition As Condition) As IDataReader

            Return _connector.CommandNew(SelectJoin(source, target, columns, join) & " " & condition.ToString()).ExecuteReader()
        End Function

#Region "Query - join"

        Public Overridable Function QuerySelect(join As QueryJoinKey) As String

            With join
                Dim sql As String = "SELECT " & ColumnList(.Source.Columns, "s") & " FROM " & .Source.Name & _
                                    " s JOIN " & .Target.Name & " t ON "

                Dim first As Boolean = True
                For Each pair In .ForeignKey.Columns

                    If (Not first) Then sql &= " AND "

                    sql &= "s." & pair.Key.Name & " = t." & pair.Value.Name
                    first = False
                Next

                Return sql
            End With
        End Function

        Public Overridable Function QuerySelectCustom(join As QueryJoin) As String

            With join
                Dim sql As String = "SELECT " & ColumnList(.Source.Columns, "s") & " FROM " & .Source.Name & _
                                    " s JOIN " & .Target.Name & " t ON "

                Dim first As Boolean = True
                For Each pair In .ColumnMap

                    If (Not first) Then sql &= " AND "

                    sql &= "s." & pair.Key.Name & " = t." & pair.Value.Name

                    first = False
                Next

                Return sql
            End With
        End Function

#End Region


    End Class
End Namespace