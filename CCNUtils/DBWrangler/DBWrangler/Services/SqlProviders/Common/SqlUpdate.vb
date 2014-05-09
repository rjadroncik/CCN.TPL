
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlUpdate
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overridable Function UpdateBegin(table As Table) As String

            Return "UPDATE " & table.Name & " SET "
        End Function

        Public Overridable Function UpdateRow(table As Table, values As DbValues, where As DbValues) As String

            Dim sql As String = UpdateBegin(table)

            Dim first As Boolean = True
            For Each column As String In values.Keys

                If (Not first) Then sql &= ", "

                sql &= column & " = " & table.ColumnNamed(column).DataType.ValueToSql(values(column), _connector)

                first = False
            Next

            sql &= " WHERE "

            first = True
            For Each column As String In where.Keys

                If (Not first) Then sql &= " AND "

                sql &= column & " = " & table.ColumnNamed(column).DataType.ValueToSql(where(column), _connector)
                first = False
            Next

            Return sql
        End Function

        Public Overridable Function UpdateRow(table As Table, values As DbValues, where As Condition) As String

            Dim sql As String = UpdateBegin(table)

            Dim first As Boolean = True
            For Each column As String In values.Keys

                If (Not first) Then sql &= ", "

                sql &= column & " = " & table.ColumnNamed(column).DataType.ValueToSql(values(column), _connector)

                first = False
            Next

            Return sql & " " & where.ToSql(_connector)
        End Function

        Public Overridable Function Execute(table As Table, values As DbValues, where As DbValues) As Integer

            Return ExecuteNonQuery(UpdateRow(table, values, where) & _commandEnd)
        End Function

        Public Overridable Function Execute(table As Table, values As DbValues, where As Condition) As Integer

            Return ExecuteNonQuery(UpdateRow(table, values, where) & _commandEnd)
        End Function

    End Class
End Namespace