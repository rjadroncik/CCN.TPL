
Imports System.Data.SqlClient
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports System.Text

Namespace Services.SqlProviders.Common

    Public Class SqlSelect
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overridable Function SelectTable(table As Table) As String

            Return "SELECT " & ColumnList(table.Columns) & " FROM " & table.Name
        End Function

        Public Overridable Function SelectTable(table As Table, condition As Condition) As String

            Return SelectTable(table) & " " & condition.ToSql(_connector)
        End Function
        
        Protected Overridable Function QueryWhereIn(table As Table, column As Column, context As QueryContext, tableAlias As String) As String

            Dim sql As New StringBuilder()
            sql.Append(" WHERE ")

            If (tableAlias <> Nothing) Then sql.Append(tableAlias & ".")
            sql.Append(column.Name & " IN (")

            Dim first As Boolean = True
            For Each key In context.Ids(table)

                If (Not first) Then sql.Append(", ")

                sql.Append(column.DataType.ValueToSql(key.Values(column.Name), _connector))

                first = False
            Next

            If (first) Then Return Nothing

            sql.Append(")")

            Return sql.ToString()
        End Function

        Protected Overridable Function QueryWhereAndOr(table As Table, columns As ICollection(Of Column), context As QueryContext, tableAlias As String) As String

            Dim sql As New StringBuilder()
            sql.Append(" WHERE ")

            Dim first As Boolean = True
            For Each key In context.Ids(table)

                If (Not first) Then sql.Append(" OR ")

                sql.Append("(")
                Dim first2 As Boolean = True
                For Each column As Column In columns

                    If (Not first2) Then sql.Append(" AND ")

                    If (tableAlias <> Nothing) Then sql.Append(tableAlias & ".")
                    sql.Append(column.Name & " = " & column.DataType.ValueToSql(key.Values(column.Name), _connector))
                    first2 = False
                Next
                sql.Append(")")

                first = False
            Next

            If (first) Then Return Nothing

            Return sql.ToString()
        End Function

        Public Overridable Function QueryWhere(table As Table, context As QueryContext, tableAlias As String) As String

            If (table.PrimaryKey.Columns.Count > 1) Then

                Return QueryWhereAndOr(table, table.PrimaryKey.Columns, context, tableAlias)
            Else
                Return QueryWhereIn(table, table.PrimaryKey.Columns.First(), context, tableAlias)
            End If
        End Function

        Public Overridable Function IsEmpty(reader As SqlDataReader) As Boolean
            Try
                Return Not reader.Read()
            Finally
                reader.Close()
            End Try
        End Function

        Public Overridable Function Execute(sql As String) As IDataReader

            Return _connector.CommandNew(sql).ExecuteReader()
        End Function

        Public Overridable Function Execute(schema As Schema) As IDataReader

            Dim sql As String = ""

            For Each table As Table In schema.Tables

                sql &= SelectTable(table) & Environment.NewLine
            Next

            Return _connector.CommandNew(sql).ExecuteReader()
        End Function

        Public Overridable Function Execute(table As Table) As IDataReader

            Return _connector.CommandNew(SelectTable(table)).ExecuteReader()
        End Function

        Public Overridable Function Execute(table As Table, condition As Condition) As IDataReader

            Return _connector.CommandNew(SelectTable(table, condition)).ExecuteReader()
        End Function
    End Class
End Namespace