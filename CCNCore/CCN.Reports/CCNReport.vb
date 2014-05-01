Public Class CCNReport

#Region "Events"

    Public Event PropertyChanged(ByVal name As String)

#End Region

#Region "Layers"

    Public Event LayerAdded(ByVal layer As CCNReportLayer)
    Public Event LayerRemoved(ByVal layer As CCNReportLayer)

    Protected _layers As New List(Of CCNReportLayer)
    Public ReadOnly Property Layers As IEnumerable(Of CCNReportLayer)
        Get
            Return _layers
        End Get
    End Property

    Public ReadOnly Property Layer(ByVal index As Integer) As CCNReportLayer
        Get
            Return _layers(index)
        End Get
    End Property

    Public Sub LayerAdd(ByVal layer As CCNReportLayer)

        _layers.Add(layer)
        RaiseEvent LayerAdded(layer)
    End Sub

    Public Sub LayerRemove(ByVal layer As CCNReportLayer)

        _layers.Remove(layer)
        RaiseEvent LayerRemoved(layer)
    End Sub

    Public Function LayerIndexOf(ByVal layer As CCNReportLayer) As Integer

        Return _layers.IndexOf(layer)
    End Function

#End Region

#Region "Properties - Page setup"

    Protected _pageHeight As Double = 297
    Public Property PageHeight As Double
        Get
            Return _pageHeight
        End Get
        Set(ByVal value As Double)
            _pageHeight = value
            RaiseEvent PropertyChanged("PageHeight")
        End Set
    End Property

    Protected _pageWidth As Double = 210
    Public Property PageWidth As Double
        Get
            Return _pageWidth
        End Get
        Set(ByVal value As Double)
            _pageWidth = value
            RaiseEvent PropertyChanged("PageWidth")
        End Set
    End Property

    Protected _pageLandscape As Boolean
    Public Property PageLandscape As Boolean
        Get
            Return _pageLandscape
        End Get
        Set(ByVal value As Boolean)
            _pageLandscape = value
            RaiseEvent PropertyChanged("PageLandscape")
        End Set
    End Property

#End Region

#Region "Initialization"

    Public Sub New()

        _layers.Add(New CCNReportLayer())
    End Sub

#End Region

End Class
