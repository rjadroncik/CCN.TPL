Imports System.Text
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Filtering

Namespace Services.SqlProviders.Common

    Public Class SqlInsertFiltered
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overridable Function InsertBegin(table As Table, skipIdentities As Boolean) As String

            Return "INSERT INTO " & table.Name & " (" & ColumnList(table.Columns, skipIdentities) & ") " & "VALUES "
        End Function

        Public Overridable Function InsertTable(table As Table, skipIdentities As Boolean, reader As IDataReader, limit As Integer, context As QueryContext) As String

            If ((table.PrimaryKey Is Nothing) AndAlso (table.UniqueKeys.IsEmpty())) Then

                Return _connector.SqlInsert.InsertTable(table, skipIdentities, reader, limit, context)
            End If

            Dim insert As String = InsertBegin(table, skipIdentities)

            Dim sql As String = ""

            For i = 0 To limit - 1

                If (Not reader.Read()) Then reader.Close() : Exit For

                If (Not context.KeyExists(table, New DbValues(table.PrimaryKey.Columns, reader, _connector))) Then

                    sql &= insert & InsertRow(table, skipIdentities, reader, context) & _commandEnd
                End If
            Next

            Return sql
        End Function

        Protected Overridable Function InsertRow(table As Table, skipIdentities As Boolean, reader As IDataReader, context As QueryContext) As String

            Dim sql As String = "("

            Dim first As Boolean = True
            For Each column As Column In table.Columns

                If (skipIdentities AndAlso column.Identity) Then Continue For

                If (Not first) Then sql &= ", "

                sql &= column.DataType.ReadToSql(reader, column, _connector)

                first = False
            Next

            If ((table.PrimaryKey IsNot Nothing) AndAlso (context IsNot Nothing)) Then

                context.IdAdd(table, New DbValues(table.PrimaryKey.Columns, reader, _connector))
            End If

            Return sql & ")"
        End Function

        Public Overridable Sub Execute(table As Table, skipIdentities As Boolean, reader As IDataReader, context As QueryContext)

            While (Not reader.IsClosed)

                ExecuteNonQuery(InsertTable(table, skipIdentities, reader, 500, context))
            End While
        End Sub

        Public Overridable Sub Sql(table As Table, skipIdentities As Boolean, reader As IDataReader, result As StringBuilder, context As QueryContext)

            While (Not reader.IsClosed)

                result.AppendLine(InsertTable(table, skipIdentities, reader, 500, context))
            End While
        End Sub
    End Class
End Namespace