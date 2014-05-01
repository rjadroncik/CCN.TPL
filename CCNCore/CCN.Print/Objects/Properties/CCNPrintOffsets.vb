Public Class CCNPrintOffsets

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(ByVal source As CCNPrintOffsets)

        _left = New CCNPrintOffset(source._left)
        _top = New CCNPrintOffset(source._top)
        _right = New CCNPrintOffset(source._right)
        _bottom = New CCNPrintOffset(source._bottom)
    End Sub

#End Region

#Region "Properties"

    Protected _left As New CCNPrintOffset
    Public ReadOnly Property Left() As CCNPrintOffset
        Get
            Return _left
        End Get
    End Property

    Protected _top As New CCNPrintOffset
    Public ReadOnly Property Top() As CCNPrintOffset
        Get
            Return _top
        End Get
    End Property

    Protected _right As New CCNPrintOffset
    Public ReadOnly Property Right() As CCNPrintOffset
        Get
            Return _right
        End Get
    End Property

    Protected _bottom As New CCNPrintOffset
    Public ReadOnly Property Bottom() As CCNPrintOffset
        Get
            Return _bottom
        End Get
    End Property

#End Region

End Class
