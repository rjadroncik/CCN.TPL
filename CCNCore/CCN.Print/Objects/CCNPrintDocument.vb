Imports C1.C1Preview
Imports CCN.Core.VB

Public Class CCNPrintDocument

#Region "Properties"

    Private _c1Document As New C1PrintDocument
    Public ReadOnly Property C1Document() As C1PrintDocument
        Get
            Return _c1Document
        End Get
    End Property

    Private _version As String = "1.0"
    Public ReadOnly Property Version() As String
        Get
            Return _version
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New()
    End Sub

#End Region

#Region "Rendering"

    Public Sub Render(progress As ProgressReporter)

        _c1Document.Clear()
        _c1Document.TagOpenParen = "(~]"
        _c1Document.TagCloseParen = "[~)"
        _c1Document.DefaultUnit = UnitTypeEnum.Mm

        _c1Document.PageLayout.PageSettings.Width = New Unit(_PageWidth, UnitTypeEnum.Mm)
        _c1Document.PageLayout.PageSettings.Height = New Unit(_PageHeight, UnitTypeEnum.Mm)

        _c1Document.PageLayout.PageSettings.LeftMargin = New Unit(_margins.Left.Ammount, UnitTypeEnum.Mm)
        _c1Document.PageLayout.PageSettings.TopMargin = New Unit(_margins.Top.Ammount, UnitTypeEnum.Mm)
        _c1Document.PageLayout.PageSettings.RightMargin = New Unit(_margins.Right.Ammount, UnitTypeEnum.Mm)
        _c1Document.PageLayout.PageSettings.BottomMargin = New Unit(_margins.Bottom.Ammount, UnitTypeEnum.Mm)

        _c1Document.PageLayout.PageSettings.Landscape = PageLandscape

        _c1Document.PageLayout.PageHeader = _pageHeader.Render()
        _c1Document.PageLayout.PageFooter = _pageFooter.Render()

        _header.Render()
        progress.Progress += 10
        _table.Render()
        progress.Progress += 10
        _footer.Render()
        progress.Progress += 10

        _c1Document.Generate()
        progress.Progress += 70
    End Sub

#End Region

#Region "Data binding"

    Protected Friend _elementsById As New Dictionary(Of String, CCNPrintElement)
    Public ReadOnly Property ElementsById(ByVal id As String) As CCNPrintElement
        Get
            If (_elementsById.ContainsKey(id)) Then
                Return _elementsById(id)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private _values As New Dictionary(Of String, Object)
    Public Property Values(ByVal valueId As String) As Object
        Get
            If (_values.ContainsKey(valueId)) Then
                Return _values(valueId)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As Object)

            If (_values.ContainsKey(valueId)) Then
                _values.Remove(valueId)
            End If
            _values.Add(valueId, value)
        End Set
    End Property

#End Region

#Region "Properties - Contents"

    Protected _header As New CCNPrintHeader(Me)
    Public ReadOnly Property Header As CCNPrintHeader
        Get
            Return _header
        End Get
    End Property

    Protected _table As New CCNPrintTable(Me)
    Public ReadOnly Property Table As CCNPrintTable
        Get
            Return _table
        End Get
    End Property

    Protected _footer As New CCNPrintFooter(Me)
    Public ReadOnly Property Footer As CCNPrintFooter
        Get
            Return _footer
        End Get
    End Property

    Protected _layers As New List(Of CCNPrintLayer)
    Public ReadOnly Property Layers As List(Of CCNPrintLayer)
        Get
            Return _layers
        End Get
    End Property

    Protected _pageHeader As New CCNPrintPageHeader(Me)
    Public ReadOnly Property PageHeader As CCNPrintPageHeader
        Get
            Return _pageHeader
        End Get
    End Property

    Protected _pageFooter As New CCNPrintPageFooter(Me)
    Public ReadOnly Property PageFooter As CCNPrintPageFooter
        Get
            Return _pageFooter
        End Get
    End Property

#End Region

#Region "Properties - Page Setup"

    Protected _margins As New CCNPrintOffsets
    Public ReadOnly Property Margins() As CCNPrintOffsets
        Get
            Return _margins
        End Get
    End Property

    Public Property PageWidth As Double = 210
    Public Property PageHeight As Double = 297
    Public Property PageLandscape As Boolean

#End Region

End Class
