
Public Class CCNPrintOffset

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(ByVal source As CCNPrintOffset)

        _changed = source._changed
        _ammount = source._ammount
    End Sub

#End Region

#Region "Properties"

    Private _changed As Boolean = False
    Public ReadOnly Property Changed() As Boolean
        Get
            Return _changed
        End Get
    End Property

    Private _ammount As Double = 0.0
    Public Property Ammount() As Double
        Get
            Return _ammount
        End Get
        Set(ByVal value As Double)
            _ammount = value

            _changed = True
        End Set
    End Property

#End Region

End Class
