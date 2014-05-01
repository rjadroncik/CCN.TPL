Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Text
Imports System.Drawing.Imaging
Imports System.ComponentModel
Imports CCN.Core.VB

Public Class CCNReportDesigner

    Public Event BeginDraw(ByVal G As Graphics)

    Public Event SelectionChanged(ByVal selected As CCNReportObject)

    Public Event ItemAdded(ByVal item As CCNReportObject)
    Public Event ItemRemoved(ByVal item As CCNReportObject)

    Protected WithEvents _report As New CCNReport()
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property Report() As CCNReport
        Get
            Return _report
        End Get
    End Property

    Protected WithEvents _layer As CCNReportLayer
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Layer() As CCNReportLayer
        Get
            Return _layer
        End Get
        Set(ByVal value As CCNReportLayer)

            If (Not _report.Layers.Contains(value)) Then Throw New InvalidOperationException("Setting unknown layer as current")
            _layer = value
        End Set
    End Property

    Protected _changed As Boolean
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property Changed() As Boolean
        Get
            Return _changed
        End Get
    End Property

    Protected WithEvents _settings As New CCNReportSettings()
    Public ReadOnly Property Settings() As CCNReportSettings
        Get
            Return _settings
        End Get
    End Property

    Protected _selected As CCNReportObject
    Protected _hovered As CCNReportObject

    Protected _mouse As Point
    Protected _mouseStart As Point
    Protected _itemStart As RectangleF

    Protected _dragging As Boolean = False
    Protected _resizing As Boolean = False

    Protected _origin As Drawing.Point

    Protected Shared _dpi As Integer

    Public Function PointInResizeBox(ByVal x As Integer, ByVal y As Integer, ByVal scale As Single) As Boolean

        Dim size = ResizeBoxSize()

        If ((x < (_selected.X + _selected.W - size)) OrElse (x > (_selected.X + _selected.W))) Then Return False
        If ((y < (_selected.Y + _selected.H - size)) OrElse (y > (_selected.Y + _selected.H))) Then Return False

        Return True
    End Function

    Protected Function ResizeBoxSize() As Single

        Dim scale As Single = _settings.Zoom / 100

        Return Math.Min(Math.Min(Pixels2Milimeters(16), _selected.W * scale), _
                        Math.Min(Pixels2Milimeters(16), _selected.H * scale)) / scale
    End Function

    Protected Sub DrawResizeBox(ByVal g As Graphics)

        Dim size As Single = ResizeBoxSize()

        If (PointInResizeBox(Scr2Doc(_mouse.X - _settings.MarginLeft + _origin.X), _
                             Scr2Doc(_mouse.Y - _settings.MarginTop + _origin.Y), _settings.Zoom / 100)) Then

            g.DrawImage(My.Resources._16x16_resize_red, _
                        _selected.X + _selected.W - size, _
                        _selected.Y + _selected.H - size, size, size)
        Else
            g.DrawImage(My.Resources._16x16_resize, _
                        _selected.X + _selected.W - size, _
                        _selected.Y + _selected.H - size, size, size)
        End If
    End Sub

    Protected Shared _brushBlackSemiTransparent As New SolidBrush(Color.FromArgb(4, 0, 0, 0))

    Protected Sub DrawSelection(ByVal g As Graphics)

        Dim pen As New Pen(Color.Red, 0.01)

        Dim offset As Single = Pixels2Milimeters(6) / (_settings.Zoom / 100)

        'top left
        g.DrawLine(pen, _selected.X - offset, _selected.Y - offset, _selected.X + offset, _selected.Y - offset)
        g.DrawLine(pen, _selected.X - offset, _selected.Y - offset, _selected.X - offset, _selected.Y + offset)

        'top right
        g.DrawLine(pen, _selected.X + _selected.W - offset, _selected.Y - offset, _selected.X + _selected.W + offset, _selected.Y - offset)
        g.DrawLine(pen, _selected.X + _selected.W + offset, _selected.Y - offset, _selected.X + _selected.W + offset, _selected.Y + offset)

        'bottom left
        g.DrawLine(pen, _selected.X - offset, _selected.Y + _selected.H + offset, _selected.X + offset, _selected.Y + _selected.H + offset)
        g.DrawLine(pen, _selected.X - offset, _selected.Y + _selected.H - offset, _selected.X - offset, _selected.Y + _selected.H + offset)

        'bottom right
        g.DrawLine(pen, _selected.X + _selected.W - offset, _selected.Y + _selected.H + offset, _selected.X + _selected.W + offset, _selected.Y + _selected.H + offset)
        g.DrawLine(pen, _selected.X + _selected.W + offset, _selected.Y + _selected.H - offset, _selected.X + _selected.W + offset, _selected.Y + _selected.H + offset)

    End Sub

    Public Sub Draw(ByVal g As Graphics)

        g.PageUnit = GraphicsUnit.Pixel
        g.FillRectangle(Brushes.Beige, 0, 0, Width, Height)
        g.SetClip(New Drawing.Rectangle(0, 0, Width - VScrollBar.Width, Height - HScrollBar.Height))

        g.PageUnit = GraphicsUnit.Millimeter
        g.TranslateTransform(Pixels2Milimeters(-_origin.X + _settings.MarginLeft), Pixels2Milimeters(-_origin.Y + _settings.MarginTop))

        g.SmoothingMode = Drawing2D.SmoothingMode.None
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

        g.ScaleTransform(_settings.Zoom / 100, _settings.Zoom / 100)

        Dim PW As Integer = _report.PageWidth
        Dim PH As Integer = _report.PageHeight

        If (_settings.Shadow) Then

            Dim size As Single = Scr2Doc(4)

            g.FillRectangle(New SolidBrush(Color.FromArgb(96, 0, 0, 0)), PW, size, size, PH)
            g.FillRectangle(New SolidBrush(Color.FromArgb(96, 0, 0, 0)), size, PH, PW - size, size)
        End If

        g.FillRectangle(Brushes.White, 0, 0, PW, PH)
        If (_settings.GridShow) Then

            For x As Single = _settings.GridSize To _report.PageWidth Step _settings.GridSize

                CCNGraphics.DrawLine(g, Color.LightGray, 0.01, x, 0, x, _report.PageHeight)
            Next
            For y As Single = _settings.GridSize To _report.PageHeight Step _settings.GridSize

                CCNGraphics.DrawLine(g, Color.LightGray, 0.01, 0, y, _report.PageWidth, y)
            Next
        End If
        CCNGraphics.DrawRectangle(g, Color.Black, 0.1, 0, 0, PW, PH)

        RaiseEvent BeginDraw(g)

        For Each layer As CCNReportLayer In Report.Layers

            For Each item As CCNReportObject In layer.Items
                item.Draw(g)
            Next
        Next

        If (Not IsNothing(_selected)) Then

            DrawSelection(g)
            DrawResizeBox(g)
        End If

        If (Not IsNothing(_hovered)) Then g.FillRectangle(_brushBlackSemiTransparent, _hovered.X, _hovered.Y, _hovered.W, _hovered.H)
    End Sub

    Protected Sub AdjustZoom()

        Select Case _settings.ZoomMode
            Case CCNZoomMode.zmPercent10
                _settings.Zoom = 10
            Case CCNZoomMode.zmpercent25
                _settings.Zoom = 25
            Case CCNZoomMode.zmPercent50
                _settings.Zoom = 50
            Case CCNZoomMode.zmPercent75
                _settings.Zoom = 75
            Case CCNZoomMode.zmPercetn100
                _settings.Zoom = 100
            Case CCNZoomMode.zmPercent200
                _settings.Zoom = 200
            Case CCNZoomMode.zmpercent300
                _settings.Zoom = 300
            Case CCNZoomMode.zmPercent400
                _settings.Zoom = 400
            Case CCNZoomMode.zmPageWidth
                _settings.Zoom = ZoomPageWidth()
            Case CCNZoomMode.zmPageHeight
                _settings.Zoom = ZoomPageHeight()
            Case CCNZoomMode.zmWholePage
                _settings.Zoom = Math.Min(ZoomPageWidth(), ZoomPageHeight())
        End Select
    End Sub

    Protected Function ZoomPageWidth() As Single

        Dim desiredScreenWidth As Integer = Width - VScrollBar.Width - _settings.MarginLeft - _settings.MarginRight

        Return Math.Round((100 * (desiredScreenWidth / (_dpi / 25.4)) / _report.PageWidth), 2)
    End Function

    Protected Function ZoomPageHeight() As Single

        Dim desiredScreenHeight As Integer = Height - HScrollBar.Height - _settings.MarginTop - _settings.MarginBottom

        Return Math.Round((100 * (desiredScreenHeight / (_dpi / 25.4)) / _report.PageHeight), 2)
    End Function

    Protected Sub AdjustSizes()

        Dim PW As Integer = Doc2Scr(_report.PageWidth) + _settings.MarginLeft + _settings.MarginRight + VScrollBar.Width
        Dim PH As Integer = Doc2Scr(_report.PageHeight) + _settings.MarginTop + _settings.MarginBottom + HScrollBar.Height

        If (PW > Width) Then

            HScrollBar.Enabled = True
            HScrollBar.Minimum = 0
            HScrollBar.Maximum = PW + VScrollBar.Width
            HScrollBar.LargeChange = Width + VScrollBar.Width
        Else
            HScrollBar.Enabled = False
            _origin.X = 0
        End If

        If (PH > Height) Then

            VScrollBar.Enabled = True
            VScrollBar.Minimum = 0
            VScrollBar.Maximum = PH + HScrollBar.Height
            VScrollBar.LargeChange = Height + HScrollBar.Height
        Else
            VScrollBar.Enabled = False
            _origin.Y = 0
        End If
    End Sub

    Public Shared Function Milimeters2Pixels(ByVal milimenters As Single) As Integer

        Return milimenters * _dpi / 25.4
    End Function

    Public Shared Function Pixels2Milimeters(ByVal pixels As Integer) As Single

        Return pixels * 25.4 / _dpi
    End Function

    Protected Function Doc2Scr(ByVal docCoord As Single) As Integer

        Return Math.Round((_settings.Zoom / 100) * Milimeters2Pixels(docCoord))
    End Function

    Protected Function Scr2Doc(ByVal scrCoord As Integer) As Single

        Return Pixels2Milimeters(scrCoord) / (_settings.Zoom / 100)
    End Function

    Protected Sub ReportDesigner_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        Dim _selectedOld As CCNReportObject = _selected
        _selected = Nothing

        Dim selectable As New LinkedList(Of CCNReportObject)

        For Each item As CCNReportObject In _layer.Items
            item.Selected = False
            If (item.CointainsPoint(Scr2Doc(e.X - _settings.MarginLeft + _origin.X), _
                                    Scr2Doc(e.Y - _settings.MarginTop + _origin.Y))) Then selectable.AddLast(item)
        Next

        If (Not selectable.IsEmpty()) Then

            If (e.Clicks = 2) Then

                _selected = selectable.First.Value
            Else
                _selected = selectable.Last.Value
            End If
        End If

        If (Not IsNothing(_selected)) Then

            _selected.Selected = True

            _layer.MoveToFront(_selected)
            _changed = True

            _mouseStart = e.Location
            _itemStart = New RectangleF(_selected.X, _selected.Y, _selected.W, _selected.H)

            If (PointInResizeBox(Scr2Doc(e.X - _settings.MarginLeft + _origin.X), _
                                 Scr2Doc(e.Y - _settings.MarginTop + _origin.Y), _settings.Zoom / 100)) Then

                _resizing = True
            Else
                _dragging = True
            End If
        End If

        If (Not _selected Is _selectedOld) Then RaiseEvent SelectionChanged(_selected)
        Refresh()
    End Sub

    Protected Sub ReportDesigner_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        _mouse = e.Location

        If (e.Button = Windows.Forms.MouseButtons.Left) Then

            If (Not IsNothing(_selected)) Then

                If (_dragging) Then

                    _changed = True

                    _selected.X = _itemStart.X + Scr2Doc(e.X - _mouseStart.X)
                    _selected.Y = _itemStart.Y + Scr2Doc(e.Y - _mouseStart.Y)

                    If (_settings.GridSnap) Then

                        _selected.X = Math.Round(_selected.X / _settings.GridSize) * _settings.GridSize
                        _selected.Y = Math.Round(_selected.Y / _settings.GridSize) * _settings.GridSize
                    End If
                End If

                If (_resizing) Then

                    _changed = True

                    _selected.W = _itemStart.Width + Scr2Doc(e.X - _mouseStart.X)
                    _selected.H = _itemStart.Height + Scr2Doc(e.Y - _mouseStart.Y)

                    If (_settings.GridSnap) Then

                        _selected.W = Math.Round(_selected.W / _settings.GridSize) * _settings.GridSize
                        _selected.H = Math.Round(_selected.H / _settings.GridSize) * _settings.GridSize
                    End If

                    If (_selected.W < 1) Then _selected.W = 1
                    If (_selected.H < 1) Then _selected.H = 1
                End If

                Refresh()
            End If
        Else
            _hovered = Nothing

            For Each item As CCNReportObject In _layer.Items
                item.Hovered = False
                If (item.CointainsPoint(Scr2Doc(e.X - _settings.MarginLeft + _origin.X), _
                                        Scr2Doc(e.Y - _settings.MarginTop + _origin.Y))) Then
                    _hovered = item
                End If
            Next

            If (Not IsNothing(_hovered)) Then _hovered.Hovered = True

            Refresh()
        End If
    End Sub

    Public Sub CancelDrag()

        If (Not IsNothing(_selected)) Then

            If (_dragging) Then

                _selected.X = _itemStart.X
                _selected.Y = _itemStart.Y
                _dragging = False
            End If

            If (_resizing) Then

                _selected.W = _itemStart.Width
                _selected.H = _itemStart.Height
                _resizing = False
            End If
        End If
    End Sub

    Protected Sub ReportDesigner_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

        If (Not IsNothing(_selected)) Then
            _dragging = False
            _resizing = False

            RaiseEvent SelectionChanged(_selected)
        End If
    End Sub

    Protected Sub ReportDesigner_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Draw(e.Graphics)
    End Sub

    Protected Sub OnReportPropertyChanged(ByVal propertyName As String) Handles _report.PropertyChanged

        _changed = True

        If ((propertyName = "PageWidth") OrElse _
            (propertyName = "PageHeight")) Then

            AdjustZoom()
        End If

        AdjustSizes()
        Refresh()
    End Sub

    Protected Sub OnReportItemRemoved(ByVal item As CCNReportObject) Handles _layer.ItemRemoved

        _changed = True

        If (item Is _selected) Then _selected = Nothing : RaiseEvent SelectionChanged(_selected)
        If (item Is _hovered) Then _hovered = Nothing

        Refresh()

        RaiseEvent ItemRemoved(item)
    End Sub

    Protected Sub OnReportItemAdded(ByVal item As CCNReportObject) Handles _layer.ItemAdded

        _changed = True

        RaiseEvent ItemAdded(item)
    End Sub

    Protected Sub OnReportLayerAdded(ByVal layer As CCNReportLayer) Handles _report.LayerAdded

        _changed = True
    End Sub

    Protected Sub OnReportLayerRemoved(ByVal layer As CCNReportLayer) Handles _report.LayerAdded

        _changed = True
    End Sub

    Protected Sub OnSettingsPropertyChanged(ByVal propertyName As String) Handles _settings.PropertyChanged

        If ((propertyName = "Zoom") OrElse _
            (propertyName = "ZoomMode")) Then AdjustZoom()

        AdjustSizes()
        Refresh()
    End Sub

    Public Sub New()

        If (_dpi = 0) Then
            Using g As Graphics = Me.CreateGraphics()
                _dpi = g.DpiX
            End Using
        End If

        _layer = Report.Layers.Last()
        _changed = False

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)

        HScrollBar.Minimum = 0
        HScrollBar.Maximum = 100
        HScrollBar.Value = 0
    End Sub

    Protected Sub ReportDesigner_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        AdjustZoom()
        AdjustSizes()
    End Sub

    Private Sub HScrollBar_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HScrollBar.ValueChanged
        _origin.X = HScrollBar.Value
        Refresh()
    End Sub

    Protected Sub HScrollBar_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar.Scroll
        _origin.X = HScrollBar.Value
        Refresh()
    End Sub

    Private Sub VScrollBar_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VScrollBar.ValueChanged
        _origin.Y = VScrollBar.Value
        Refresh()
    End Sub

    Protected Sub VScrollBar_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar.Scroll
        _origin.Y = VScrollBar.Value
        Refresh()
    End Sub

    Public Property SelectedItem() As CCNReportObject
        Get
            Return _selected
        End Get
        Set(ByVal value As CCNReportObject)

            _selected = Nothing

            For Each layer As CCNReportLayer In _report.Layers

                For Each element As CCNReportObject In layer.Items

                    If (element Is value) Then

                        _selected = element
                        element.Selected = True
                    Else
                        element.Selected = False
                    End If
                Next
            Next
        End Set
    End Property
End Class
