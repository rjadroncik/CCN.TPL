Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports CCN.Core.VB

Namespace Model.Filtering
    Public Class DbValues
        Implements IEquatable(Of DbValues)

#Region "Properties"

        Private ReadOnly _values As New Dictionary(Of String, Object)
        Default Public ReadOnly Property Values(columnName As String) As Object
            Get
                Return _values(columnName)
            End Get

        End Property

        Public ReadOnly Property Keys As Dictionary(Of String, Object).KeyCollection
            Get
                Return _values.Keys
            End Get
        End Property

#End Region

#Region "Initialization"

        Public Sub New()
        End Sub

        Public Sub New(columns As IEnumerable(Of Column), reader As IDataReader, connector As IConnector)

            For Each column As Column In columns

                _values.Add(column.Name, column.DataType.Read(reader, column, connector))
            Next
        End Sub

        'Public Sub New(columns As IEnumerable(Of Column), reader As IDataReader)

        '    For Each column As Column In columns

        '        _values.Add(column.Name, reader(column.Name))
        '    Next
        'End Sub

        'Public Sub New(columns As IEnumerable(Of Column), row As DataRow)

        '    For Each column As Column In columns

        '        _values.Add(column.Name, row(column.Name))
        '    Next
        'End Sub

        Public Sub New(column As String, value As Object)

            _values.Add(column, value)
        End Sub

        Public Sub New(columns As ICollection(Of String), values As ICollection(Of Object))

            For i As Integer = 0 To columns.Count - 1

                _values.Add(columns(i), values(i))
            Next
        End Sub

        Public Sub New(columns As ICollection(Of Column), values As ICollection(Of Object))

            For i As Integer = 0 To columns.Count - 1

                _values.Add(columns(i).Name, values(i))
            Next
        End Sub

#End Region

#Region "Manipulation"

        Public Function Subset(columns As ICollection(Of String)) As DbValues

            Return New DbValues(columns, _values.Where(Function(x) columns.Contains(x.Key)).Select(Function(x) x.Value).ToList())
        End Function

        Public Function Subset(columns As ICollection(Of Column)) As DbValues

            Return Subset(columns.Select(Function(x) x.Name).ToList())
        End Function

        Public Function ToPairList(table As Table) As IList(Of Pair(Of Column, Object))

            Return (From keyValue In _values Select New Pair(Of Column, Object)(table.ColumnNamed(keyValue.Key), keyValue.Value)).ToList()
        End Function

#End Region

#Region "Equality"

        Public Function EqualsGeneric(other As DbValues) As Boolean Implements IEquatable(Of DbValues).Equals

            Return _values.Keys.All(Function(key) other.Keys.Contains(key) AndAlso other.Values(key).Equals(_values(key)))
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean

            If (Not TypeOf obj Is DbValues) Then Return False

            Return EqualsGeneric(DirectCast(obj, DbValues))
        End Function

        Public Overrides Function GetHashCode() As Integer

            Return CType(_values.Values.Sum(Function(x) CType(x.GetHashCode(), Long)) Mod Integer.MaxValue, Integer)
        End Function

#End Region

#Region "Overridden"

        Public Overrides Function ToString() As String

            Return _values.ToStringAll()
        End Function

#End Region

    End Class
End Namespace