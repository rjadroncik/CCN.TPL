Imports System.Text
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlInsert
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overridable Function InsertBegin(table As Table, skipIdentities As Boolean) As String

            Return "INSERT INTO " & table.Name & " (" & ColumnList(table.Columns, skipIdentities) & ") " & "VALUES "
        End Function

        Protected Overridable Function InsertBegin(table As Table, values As DbValues) As String

            Return "INSERT INTO " & table.Name & " (" & Converting.Values2StringInvariant(values.Keys) & ") " & "VALUES "
        End Function

        Public Overridable Function InsertTable(table As Table, skipIdentities As Boolean, reader As IDataReader, limit As Integer?, context As QueryContext) As String

            Dim sql As New StringBuilder()

            sql.Append(InsertBegin(table, skipIdentities))

            Dim count = 0

            While ((Not limit.HasValue) OrElse (count < limit))

                If (Not reader.Read()) Then reader.Close() : Exit While

                If (count > 0) Then sql.Append(", ")

                sql.Append(InsertRow(table, skipIdentities, reader, context))

                count += 1
            End While

            If (count > 0) Then

                sql.Append(";" & Environment.NewLine)
                Return sql.ToString()
            End If

            Return String.Empty
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

        Public Overridable Function InsertRow(table As Table, skipIdentities As Boolean, values() As Object) As String

            Dim sql As String = InsertBegin(table, skipIdentities) & "("

            Dim i As Integer = 0
            Dim first As Boolean = True
            For Each column As Column In table.Columns

                If (skipIdentities AndAlso column.Identity) Then Continue For

                If (Not first) Then sql &= ", "

                sql &= column.DataType.ValueToSql(values(i), _connector)

                first = False
                i += 1
            Next

            Return sql & ")"
        End Function

        Public Overridable Function InsertRow(table As Table, values As DbValues) As String

            Dim sql As String = InsertBegin(table, values) & "("

            Dim first As Boolean = True
            For Each name As String In values.Keys

                Dim column As Column = table.ColumnNamed(name)

                If (Not first) Then sql &= ", "

                sql &= column.DataType.ValueToSql(values.Values(name), _connector)

                first = False
            Next

            Return sql & ")"
        End Function

        Public Overridable Sub Execute(table As Table, skipIdentities As Boolean, reader As IDataReader, context As QueryContext, _
                                       Optional batchSize As Integer = 500)

            While (Not reader.IsClosed)

                ExecuteNonQuery(InsertTable(table, skipIdentities, reader, batchSize, context))
            End While
        End Sub

        Public Overridable Sub Sql(table As Table, skipIdentities As Boolean, reader As IDataReader, result As StringBuilder, context As QueryContext, _
                                   Optional batchSize As Integer = 500)

            While (Not reader.IsClosed)

                result.AppendLine(InsertTable(table, skipIdentities, reader, batchSize, context))
            End While
        End Sub

        Public Overridable Function Execute(table As Table, skipIdentities As Boolean, getLastId As Boolean, values() As Object) As Object

            Dim cmd = InsertRow(table, skipIdentities, values)

            If (cmd.Length > 0) Then

                If (getLastId) Then

                    cmd &= "; SELECT " & table.PrimaryKey.Columns(0).Name & " FROM " & table.Name & " WHERE " & table.PrimaryKey.Columns(0).Name & " = @@Identity;"
                    Return ExecuteScalar(cmd)
                Else
                    ExecuteNonQuery(cmd)
                End If
            End If

            Return Nothing
        End Function

        Public Overridable Function Execute(table As Table, getLastId As Boolean, values As DbValues) As Object

            Dim cmd = InsertRow(table, values)

            If (cmd.Length > 0) Then

                If (getLastId) Then

                    cmd &= "; SELECT " & table.PrimaryKey.Columns(0).Name & " FROM " & table.Name & " WHERE " & table.PrimaryKey.Columns(0).Name & " = @@Identity;"
                    Return ExecuteScalar(cmd)
                Else
                    ExecuteNonQuery(cmd)
                End If
            End If

            Return Nothing
        End Function

    End Class
End Namespace