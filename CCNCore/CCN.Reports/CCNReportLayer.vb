Public Class CCNReportLayer

#Region "Events"

    Public Event ItemAdded(ByVal item As CCNReportObject)
    Public Event ItemRemoved(ByVal item As CCNReportObject)

#End Region

#Region "Properties"

    Protected _items As New List(Of CCNReportObject)
    Public ReadOnly Property Items() As IEnumerable(Of CCNReportObject)
        Get
            Return _items
        End Get
    End Property

    Public ReadOnly Property Item(ByVal name As String) As CCNReportObject
        Get
            Return _items.Find(Function(x) x.Name = name)
        End Get
    End Property

#End Region

#Region "Editing"

    Public Sub Add(ByVal item As CCNReportObject)

        _items.Add(item)
        RaiseEvent ItemAdded(item)
    End Sub

    Public Sub Remove(ByVal item As CCNReportObject)

        _items.Remove(item)
        RaiseEvent ItemRemoved(item)
    End Sub

    Public Sub MoveToFront(ByVal item As CCNReportObject)

        _items.Remove(item)
        _items.Add(item)
    End Sub

#End Region

#Region "Initialization"

    Private _id As Integer

    Public Sub New()

        _id = New Random(DateTime.Now.ToFileTimeUtc Mod 100).Next
    End Sub

#End Region

End Class
