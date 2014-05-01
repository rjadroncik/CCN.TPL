Public Class CCNReportSettings

#Region "Events"

    Public Event PropertyChanged(ByVal name As String)

#End Region

#Region "Properties - Page setup"

    Protected _marginLeft As Integer = 10
    Public Property MarginLeft() As Integer
        Get
            Return _marginLeft
        End Get
        Set(ByVal value As Integer)
            _marginLeft = value
            RaiseEvent PropertyChanged("MarginLeft")
        End Set
    End Property

    Protected _marginRight As Integer = 10
    Public Property MarginRight() As Integer
        Get
            Return _marginRight
        End Get
        Set(ByVal value As Integer)
            _marginRight = value
            RaiseEvent PropertyChanged("MarginRight")
        End Set
    End Property

    Protected _marginTop As Integer = 10
    Public Property MarginTop() As Integer
        Get
            Return _marginTop
        End Get
        Set(ByVal value As Integer)
            _marginTop = value
            RaiseEvent PropertyChanged("MarginTop")
        End Set
    End Property

    Protected _marginBottom As Integer = 10
    Public Property MarginBottom() As Integer
        Get
            Return _marginBottom
        End Get
        Set(ByVal value As Integer)
            _marginBottom = value
            RaiseEvent PropertyChanged("MarginBottom")
        End Set
    End Property

#End Region

#Region "Properties - Runtime"

    Protected _zoomMode As CCNZoomMode = CCNZoomMode.zmPageWidth
    Public Property ZoomMode() As CCNZoomMode
        Get
            Return _zoomMode
        End Get
        Set(ByVal value As CCNZoomMode)
            _zoomMode = value
            RaiseEvent PropertyChanged("ZoomMode")
        End Set
    End Property

    Protected _shadow As Boolean = True
    Public Property Shadow() As Boolean
        Get
            Return _shadow
        End Get
        Set(ByVal value As Boolean)
            _shadow = value
            RaiseEvent PropertyChanged("Shadow")
        End Set
    End Property

    Protected _zoom As Double
    Public Property Zoom() As Double
        Get
            Return _zoom
        End Get
        Set(ByVal value As Double)

            _zoom = Math.Round(value, 2)

            If (_zoomMode = CCNZoomMode.zmCustom) Then RaiseEvent PropertyChanged("Zoom")
        End Set
    End Property

    Protected _gridShow As Boolean = False
    Public Property GridShow() As Boolean
        Get
            Return _gridShow
        End Get
        Set(ByVal value As Boolean)
            _gridShow = value
            RaiseEvent PropertyChanged("GridShow")
        End Set
    End Property

    Protected _gridSnap As Boolean = False
    Public Property GridSnap() As Boolean
        Get
            Return _gridSnap
        End Get
        Set(ByVal value As Boolean)
            _gridSnap = value
            RaiseEvent PropertyChanged("GridSnap")
        End Set
    End Property

    Protected _gridSize As Integer = 5
    Public Property GridSize() As Integer
        Get
            Return _gridSize
        End Get
        Set(ByVal value As Integer)

            _gridSize = value
            If (_gridSize = 0) Then _gridSize = 5
            RaiseEvent PropertyChanged("GridSize")
        End Set
    End Property

#End Region

End Class
