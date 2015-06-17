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

        Public Shared Operator =(first As DbValues, second As DbValues) As Boolean

            If (first Is Nothing) Then

                If (second Is Nothing) Then Return True

                Return False
            Else
                If (second Is Nothing) Then Return False
            End If

            Return first.EqualsGeneric(second)
        End Operator

        Public Shared Operator <>(first As DbValues, second As DbValues) As Boolean

            Return Not (first = second)
        End Operator

#End Region

#Region "Manipulation"

        Public Function Subset(columns As ICollection(Of String)) As DbValues

            Return New DbValues(columns, _values.Where(Function(x) columns.Contains(x.Key)).Select(Function(x) x.Value).ToList())
        End Function

        Public Function Subset(columns As ICollection(Of Column)) As DbValues

            Return Subset(columns.Select(Function(x) x.Name).ToList())
        End Function

        Public Function ToPairList(table As Table) As IList(Of KeyValuePair(Of Column, Object))

            Return (From keyValue In _values Select New KeyValuePair(Of Column, Object)(table.ColumnNamed(keyValue.Key), keyValue.Value)).ToList()
        End Function

#End Region

#Region "Equality"

        Public Function EqualsGeneric(other As DbValues) As Boolean Implements IEquatable(Of DbValues).Equals

            For Each key In _values.Keys

                If (Not other.Keys.Contains(key)) Then Return False

                Dim mine = _values(key)
                Dim their = other.Values(key)

                If (mine Is Nothing) Then

                    If (their IsNot Nothing) Then Return False

                    Continue For
                Else
                    If (their Is Nothing) Then Return False
                End If

                If (Not mine.Equals(their)) Then Return False
            Next

            Return True
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean

            If (Not TypeOf obj Is DbValues) Then Return False

            Return EqualsGeneric(DirectCast(obj, DbValues))
        End Function

        Public Overrides Function GetHashCode() As Integer

            Return CType(_values.Values.Where(Function(x) x IsNot Nothing).Sum(Function(x) CType(x.GetHashCode(), Long)) Mod Integer.MaxValue, Integer)
        End Function

#End Region

#Region "Overridden"

        Public Overrides Function ToString() As String

            Return _values.ToStringAll()
        End Function

#End Region

    End Class
End Namespace