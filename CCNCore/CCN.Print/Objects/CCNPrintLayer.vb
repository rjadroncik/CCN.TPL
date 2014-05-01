Public Class CCNPrintLayer

#Region "Properties"

    Private _items As New List(Of CCNPrintElement)
    Public ReadOnly Property Items() As List(Of CCNPrintElement)
        Get
            Return _items
        End Get
    End Property

#End Region

End Class
