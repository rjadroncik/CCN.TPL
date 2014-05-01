Public Class ClrRow
    Inherits ClrTableElement

#Region "Properties"

    Private _items As Object()
    Default Public Property Item(column As String) As Object
        Get
            Return _items(_table.Column(column).Index)
        End Get
        Set(value As Object)

            _items(_table.Column(column).Index) = value
        End Set
    End Property

#End Region

#Region "Initialization"

    Public Sub New(table As ClrTable)

        Dim items(table.ColumnCount) As Object

        _table = table
        _items = items
    End Sub

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String

        Return _items.ToStringAll()
    End Function

#End Region

End Class
