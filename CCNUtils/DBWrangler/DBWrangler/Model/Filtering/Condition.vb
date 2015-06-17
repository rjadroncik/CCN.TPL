Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Model.Filtering

    Public Class Condition

#Region "Properties"

        Public Property Expression As String

        Private ReadOnly _variables As New List(Of Variable)
        Public ReadOnly Property Variables As IList(Of Variable)
            Get
                Return _variables
            End Get
        End Property

#End Region

#Region "Initialization"

        Public Sub New()

            _Expression = ""
        End Sub

        Public Sub New(expression As String)

            _Expression = expression
        End Sub

        Public Sub New(key As ICollection(Of Column), values As DbValues, connector As IConnector)

            _Expression = WhereClause(key, values, connector)
        End Sub

        Public Sub New(key As ICollection(Of Column), values As DbValues, customSql As String, connector As IConnector)

            _Expression = WhereClause(key, values, connector) & " " & customSql
        End Sub

        Public Sub New(table As Table, values As DbValues, connector As IConnector)

            Dim key As New List(Of Column)

            For Each name As String In values.Keys

                key.Add(table.ColumnNamed(name))
            Next

            _Expression = WhereClause(key, values, connector)
        End Sub

        Public Sub New(table As Table, values As DbValues, customSql As String, connector As IConnector)

            Dim key As New List(Of Column)

            For Each name As String In values.Keys

                key.Add(table.ColumnNamed(name))
            Next

            _Expression = WhereClause(key, values, connector) & " " & customSql
        End Sub

        Public Sub New(key As ICollection(Of Column), values As Object(), connector As IConnector)

            _Expression = WhereClause(key, New DbValues(key, values), connector)
        End Sub

        Public Sub New(key As ICollection(Of Column), values As Object(), customSql As String, connector As IConnector)

            _Expression = WhereClause(key, New DbValues(key, values), connector) & " " & customSql
        End Sub

        Public Sub New(key As Column, value As Object, connector As IConnector)

            _Expression = WhereClause(key, value, connector)
        End Sub

        Public Sub New(keys As ICollection(Of Column), values As ICollection(Of Object), connector As IConnector)

            _Expression = WhereClause(keys, values, connector)
        End Sub

        Public Overloads Function ToString(connector As IConnector) As String

            Dim sql As String = _Expression

            For Each variable As Variable In _variables

                sql = sql.Replace(variable.Name, variable.Value(connector))
            Next

            Return sql
        End Function

#End Region

#Region "Where"

        Private Shared Function WhereClause(columns As ICollection(Of Column), values As DbValues, connector As IConnector) As String

            Dim sql As String = "WHERE "

            Dim first As Boolean = True
            For Each column As Column In columns

                If (Not first) Then sql &= " AND "

                sql &= column.Name & " = " & column.DataType.ValueToSql(values(column.Name), connector)
                first = False
            Next

            Return sql
        End Function

        Private Shared Function WhereClause(columns As ICollection(Of Column), values As ICollection(Of Object), connector As IConnector) As String

            Dim sql As String = "WHERE "

            Dim valueEnumerator As IEnumerator = values.GetEnumerator()

            Dim first As Boolean = True
            For Each column As Column In columns

                valueEnumerator.MoveNext()
                If (Not first) Then sql &= " AND "

                sql &= column.Name & " = " & column.DataType.ValueToSql(valueEnumerator.Current, connector)
                first = False
            Next

            Return sql
        End Function

        Private Shared Function WhereClause(column As Column, value As Object, connector As IConnector) As String

            Return "WHERE " & column.Name & " = " & column.DataType.ValueToSql(value, connector)
        End Function

#End Region

#Region "BL"

        Public Function ToSql(connector As IConnector) As String

            Dim sql As String = _Expression

            For Each variable As Variable In _variables

                sql = sql.Replace(variable.Name, variable.Value(connector))
            Next

            Return sql
        End Function

#End Region

    End Class
End Namespace