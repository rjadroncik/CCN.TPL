Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Windows.Forms.VisualStyles
Imports C1.Win.C1FlexGrid

'Version 1.0 January 2009

<ToolboxItem(True), ToolboxBitmap(GetType(MultiSelect), "DDContainer.DDContainer.bmp")> _
<DefaultEvent("DropDown")> _
Public Class MultiSelect
    Inherits ContainerControl
    Implements IC1EmbeddedEditor

    Private blnIsResizeOK As Boolean = False

    Private rectTextBox As Rectangle = New Rectangle(0, 0, 107, 20)
    Private rectDropDownButton As Rectangle = New Rectangle(0, 0, 20, 20)

    Private TSDropDown As New ToolStripDropDown
    Private TSHost As ToolStripControlHost

    Private GripBox As Rectangle

    Public Event DropDown(sender As Object, IsOpen As Boolean)
    Public Event TextBoxChanged(sender As Object)

#Region "Initialize"

    Public Sub New()

        ' Add any initialization after the InitializeComponent() call.
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        AddHandler TSDropDown.Closing, AddressOf TSDropDown_Closing
        AddHandler TSDropDown.Opening, AddressOf TSDropDown_Opening

        Me.Width = 300
        Me.Height = 21
        blnIsResizeOK = False
        UpdateTextArea()

        Me.Visible = False
    End Sub

    Private Sub DDContainer_HandleCreated(sender As Object, _
      e As System.EventArgs) Handles Me.HandleCreated

        'I put this here because there is no Load Event
        If Not Me.DesignMode Then

            Me.CloseDesignDropDown()
            Me.Region = Nothing

            blnIsResizeOK = True

            If _dropControl IsNot Nothing Then

                TSHost = New ToolStripControlHost(_dropControl)
                TSHost.Margin = Padding.Empty
                TSHost.Padding = Padding.Empty
                TSHost.AutoSize = False
                TSHost.Size = _dropControl.Size

                TSDropDown.AutoSize = False
                TSDropDown.Size = TSHost.Size
                TSDropDown.Items.Add(TSHost)
                TSDropDown.BackColor = _ddBackColor
                TSDropDown.DropShadowEnabled = _ddShadow
                Me.Controls.Remove(_dropControl)

            End If
        End If

        ResizeMe()
    End Sub

#End Region 'Initialize

#Region "Hide Properties"

    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property BackgroundImage() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property BackgroundImageLayout() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property BorderStyle() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property AutoScroll() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(value As Boolean) 'empty 
        End Set
    End Property

    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property AutoSize() As Boolean
        Get
            Return False 'always false 
        End Get
        Set(value As Boolean) 'empty 
        End Set
    End Property

#End Region 'Hide Properties

#Region "Control Properties"

#Region "General"

    Private _dropControl As Control = Nothing
    <Category("Appearance DropDown")> _
    <Description("Get or Set Control to show in dropdown")> _
    Public Property DropControl() As Control
        Get
            Return _dropControl
        End Get
        Set(value As Control)
            _dropControl = Nothing
            If value IsNot Nothing Then
                _dropControl = value
                SizeToDropControl()
                AddHandler _dropControl.Resize, AddressOf DropControl_Resize
                AddHandler _dropControl.Move, AddressOf DropControl_Move
            End If
        End Set
    End Property

    Private Sub DropControl_Move(sender As Object, e As System.EventArgs)
        SizeToDropControl()
    End Sub

    Private Sub DropControl_Resize(sender As Object, e As System.EventArgs)
        SizeToDropControl()
    End Sub

    Private _isOpen As Boolean = False
    <Category("Appearance DropDown")> _
    <Description("Get or Set if the Panel is in the Open State")> _
    <DefaultValue(False)> _
    Public Property IsOpen() As Boolean
        Get
            Return _isOpen
        End Get
        Set(value As Boolean)
            blnIsResizeOK = False
            _isOpen = value

            If value Then
                If DesignMode Then
                    Me.BringToFront()
                    Me.Width = Math.Max(Me.PanelSize.Width, Me.HeaderWidth)
                    Me.Height = Me.PanelSize.Height + _headerHeight + 2
                    UpdateRegion()
                End If
            Else
                If DesignMode Then Me.Width = Me.HeaderWidth
                Me.Height = _headerHeight + 1
                Me.Region = Nothing
            End If

            Me.Invalidate()
            blnIsResizeOK = True
        End Set
    End Property

    Private _headerHeight As Integer = 20
    <Browsable(False)> _
    <Category("Appearance DropDown")> _
    <Description("Get or Set the width of the Text Window")> _
    <DefaultValue(150)> _
    Public Property HeaderHeight() As Integer
        Get
            Return _headerHeight
        End Get
        Set(value As Integer)
            _headerHeight = value
            If Not _isOpen Then
                blnIsResizeOK = False
                Me.Height = value + 1
                blnIsResizeOK = True
            End If
            UpdateTextArea()
            ResizeMe()
            Me.Invalidate()
        End Set
    End Property

    Private _headerWidth As Integer = 200
    <Category("Appearance DropDown")> _
    <Description("Get or Set the width of the Text Window")> _
    <DefaultValue(200)> _
    Public Property HeaderWidth() As Integer
        Get
            Return _headerWidth
        End Get
        Set(value As Integer)
            _headerWidth = value
            If Not _isOpen Then
                blnIsResizeOK = False
                Me.Width = Me.HeaderWidth
                blnIsResizeOK = True
            End If
            UpdateTextArea()
            ResizeMe()
            Me.Invalidate()
        End Set
    End Property

#End Region 'General

#Region "Panel"

    Private _panelSize As Size = New Size(150, 150)
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Size of the DropDown Panel")> _
    <DefaultValue(GetType(Size), "150, 150")> _
    Public Property PanelSize() As Size
        Get
            Return _panelSize
        End Get
        Set(value As Size)
            If _dropControl Is Nothing Then
                _panelSize = value
            Else
                _panelSize = Size.Add(_dropControl.Size, Me._ddPadding.Size)
            End If
            If DesignMode And _isOpen Then
                Me.Width = Me._panelSize.Width
                Me.Height = Me._panelSize.Height + _headerHeight + 2
                UpdateRegion()
            End If
            Me.Invalidate()
        End Set
    End Property

    Private _ddAlignment As StringAlignment = StringAlignment.Near
    <Category("Appearance DropDown"), _
    Description("Get or Set the horizontal position of the RunTime Dropdown"), _
    DefaultValue(StringAlignment.Near)> _
    Public Property DDAlignment() As StringAlignment
        Get
            Return _ddAlignment
        End Get
        Set(Value As StringAlignment)
            _ddAlignment = Value
        End Set
    End Property

    Private _ddPadding As New Padding
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Right and Bottom Margin from the controls")> _
    <DefaultValue(GetType(Padding), "0,0,0,0")> _
    Public Property DDPadding() As Padding
        Get
            Return _ddPadding
        End Get
        Set(value As Padding)
            _ddPadding = value
            SizeToDropControl()
        End Set
    End Property

    Private _ddBackColor As Color = Color.WhiteSmoke
    <Category("Appearance DropDown")> _
    <Description("Get or Set the DropDown BackColor")> _
    <DefaultValue(GetType(Color), "WhiteSmoke")> _
    Public Property DDBackColor() As Color
        Get
            Return _ddBackColor
        End Get
        Set(value As Color)
            _ddBackColor = value
            TSDropDown.BackColor = _ddBackColor
            Me.Invalidate()
        End Set
    End Property

    Private _ddShadow As Boolean = True
    <Category("Appearance DropDown")> _
    <Description("Get or Set if the RunTime DropDown has a Shadow")> _
    <DefaultValue(True)> _
    Public Property DDShadow() As Boolean
        Get
            Return _ddShadow
        End Get
        Set(value As Boolean)
            _ddShadow = value
            TSDropDown.DropShadowEnabled = value
        End Set
    End Property

    Private _ddOpacity As Double = 1
    <Category("Appearance DropDown")> _
    <Description("Get or Set The DropDown Opacity (number between 0 and 1)")> _
    <DefaultValue(True)> _
    Public Property DDOpacity() As Double
        Get
            Return _ddOpacity
        End Get
        Set(value As Double)
            If value > 1 Then value = 1
            If value < 0 Then value = 0
            _ddOpacity = value
            TSDropDown.Opacity = value
        End Set
    End Property

#End Region 'Panel

#Region "Text"

    Private _text As String = ""
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Text to appear in the Text Box")> _
    <Browsable(True)> _
    Overrides Property Text() As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
            Me.Invalidate(rectTextBox)
            RaiseEvent TextBoxChanged(Me)
        End Set
    End Property

    Private _textShadow As Boolean = False
    <Category("Appearance DropDown")> _
    <Description("Get or Set if the Text should be Shadowed")> _
    <DefaultValue(False)> _
    Public Property TextShadow() As Boolean
        Get
            Return _textShadow
        End Get
        Set(value As Boolean)
            _textShadow = value
            Me.Invalidate(rectTextBox)
        End Set
    End Property

    Private _textShadowColor As Color = Color.Gray
    <Category("Appearance DropDown")> _
    <Description("Get or Set if the Color of the Shadowed Text")> _
    <DefaultValue(GetType(Color), "Gray")> _
    Public Property TextShadowColor() As Color
        Get
            Return _textShadowColor
        End Get
        Set(value As Color)
            _textShadowColor = value
            Me.Invalidate(rectTextBox)
        End Set
    End Property

    Private _textBoxCornerRadius As Integer = 5
    <Category("Appearance DropDown"), _
   Description("Get or Set the Corner Radius of the Text Box"), _
   DefaultValue(5)> _
    Public Property TextBoxCornerRadius() As Integer
        Get
            Return _textBoxCornerRadius
        End Get
        Set(Value As Integer)
            If Value < 0 Then
                _textBoxCornerRadius = 0
            Else
                _textBoxCornerRadius = Value
            End If
            Me.Invalidate()
        End Set
    End Property

    Enum eGradientType
        Solid
        BackwardDiagonal
        ForwardDiagonal
        Horizontal
        Vertical
    End Enum

    Private _textBoxGradientType As eGradientType = eGradientType.Solid
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Gradient type to fill the Text Box with")> _
    <DefaultValue(eGradientType.Solid)> _
    Public Property TextBoxGradientType() As eGradientType
        Get
            Return _textBoxGradientType
        End Get
        Set(value As eGradientType)
            _textBoxGradientType = value
            Me.Invalidate()
        End Set
    End Property

    Private _textBoxBackColorA As Color = Color.White
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Primary Color of the Text Box")> _
    <DefaultValue(GetType(Color), "White")> _
    Public Property TextBoxBackColorA() As Color
        Get
            Return _textBoxBackColorA
        End Get
        Set(value As Color)
            _textBoxBackColorA = value
            Me.Invalidate()
        End Set
    End Property

    Private _textBoxBackColorB As Color = Color.White
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Secondary Color of the Text Box")> _
    <DefaultValue(GetType(Color), "White")> _
    Public Property TextBoxBackColorB() As Color
        Get
            Return _textBoxBackColorB
        End Get
        Set(value As Color)
            _textBoxBackColorB = value
            Me.Invalidate()
        End Set
    End Property

    Private _textBoxBorderColor As Color = Color.Gray
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Border Color of the Text Box")> _
    <DefaultValue(GetType(Color), "Gray")> _
    Public Property TextBoxBorderColor() As Color
        Get
            Return _textBoxBorderColor
        End Get
        Set(value As Color)
            _textBoxBorderColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _textAlignment As StringAlignment = StringAlignment.Center
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Alignment of the Text Box")> _
    <DefaultValue(StringAlignment.Center)> _
    Public Property TextAlignment() As StringAlignment
        Get
            Return _textAlignment
        End Get
        Set(value As StringAlignment)
            _textAlignment = value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Text

#Region "Button"

    Enum eButtonShape
        Square
        Circle
    End Enum

    Private _buttonShape As eButtonShape = eButtonShape.Circle
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Shape of the DropDown Button")> _
    <DefaultValue(eButtonShape.Circle)> _
    Public Property ButtonShape() As eButtonShape
        Get
            Return _buttonShape
        End Get
        Set(value As eButtonShape)
            _buttonShape = value
            Me.Invalidate()
        End Set
    End Property

    Private _buttonForeColor As Color = Color.DimGray
    <Category("Appearance DropDown")> _
    <Description("Get or Set the color of the Arrow on the DropDown Button")> _
    <DefaultValue(GetType(Color), "DimGray")> _
    Public Property ButtonForeColor() As Color
        Get
            Return _buttonForeColor
        End Get
        Set(value As Color)
            _buttonForeColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _buttonBackColor As Color = Color.LightSteelBlue
    <Category("Appearance DropDown")> _
    <Description("Get or Set the base color of the DropDown Button")> _
    <DefaultValue(GetType(Color), "LightSteelBlue")> _
    Public Property ButtonBackColor() As Color
        Get
            Return _buttonBackColor
        End Get
        Set(value As Color)
            _buttonBackColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _buttonHighlight As Color = Color.White
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Highlight color of the DropDown Button")> _
    <DefaultValue(GetType(Color), "White")> _
    Public Property ButtonHighlight() As Color
        Get
            Return _buttonHighlight
        End Get
        Set(value As Color)
            _buttonHighlight = value
            Me.Invalidate()
        End Set
    End Property

    Private _buttonBorder As Color = Color.Navy
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Border Color of the DropDown Button")> _
    <DefaultValue(GetType(Color), "Navy")> _
    Public Property ButtonBorder() As Color
        Get
            Return _buttonBorder
        End Get
        Set(value As Color)
            _buttonBorder = value
            Me.Invalidate()
        End Set
    End Property

#End Region 'Button

#Region "Graphic"

    Private _graphicBorderColor As Color = Color.Gray
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Border Color around the Graphic")> _
    <DefaultValue(GetType(Color), "Gray")> _
    Public Property GraphicBorderColor() As Color
        Get
            Return _graphicBorderColor
        End Get
        Set(value As Color)
            _graphicBorderColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _graphicImage As Bitmap = Nothing
    <Category("Appearance DropDown")> _
    <Description("Get or Set the Image next to the Text Box")> _
    Public Property GraphicImage() As Bitmap
        Get
            Return _graphicImage
        End Get
        Set(value As Bitmap)
            _graphicImage = value
            UpdateTextArea()
            Me.Invalidate(New Rectangle(0, 0, _headerWidth, _headerHeight + 1))
        End Set
    End Property

    Private _graphicWidth As Integer = 30
    <Category("Appearance DropDown")> _
    <Description("Get or Set the width of the Graphic Image width")> _
    <DefaultValue(30)> _
    Public Property GraphicWidth() As Integer
        Get
            Return _graphicWidth
        End Get
        Set(value As Integer)
            _graphicWidth = value
            UpdateTextArea()
            Me.Invalidate(New Rectangle(0, 0, _headerWidth, _headerHeight + 1))

        End Set
    End Property

    Private _graphicAutoWidth As Boolean = True
    <Category("Appearance DropDown")> _
    <Description("Get or Set to Automatically Size the Width from the Image Aspect Ratio")> _
    <DefaultValue(True)> _
    Public Property GraphicAutoWidth() As Boolean
        Get
            Return _graphicAutoWidth
        End Get
        Set(value As Boolean)
            _graphicAutoWidth = value
            UpdateTextArea()
            Me.Invalidate(New Rectangle(0, 0, _headerWidth, _headerHeight + 1))
        End Set
    End Property

#End Region 'Graphic

#End Region 'Control Properties

#Region "ToolStripDropDown"

    Private Sub TSDropDown_Opening(sender As Object, e As CancelEventArgs)

        If (TypeOf DropControl Is IEmbeddableDropDown) Then

            Dim size As Size = DirectCast(DropControl, IEmbeddableDropDown).GetPreferredBounds()
            TSDropDown.Size = New Size(size.Width + 2, size.Height + 2)
            TSDropDown.Update()
        End If

        RaiseEvent DropDown(Me, True)
    End Sub

    Private Sub TSDropDown_Closing(sender As Object, _
      e As ToolStripDropDownClosingEventArgs)
        Try
            If (Not GetButtonPath.IsVisible(PointToClient(Control.MousePosition)) _
                Or (e.CloseReason = ToolStripDropDownCloseReason.Keyboard)) Then
                IsOpen = False
            End If

            If Not e.Cancel Then
                Me.Invalidate()
                RaiseEvent DropDown(Me, False)

            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region 'ToolStripDropDown

#Region "Mouse Events"
    Private ButtonHighlightAdjust As Integer = 4
    Private MouseDownButton As Boolean = False

    Private Sub DDContainer_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If e.Y < _headerHeight Then
                If GetButtonPath.IsVisible(e.Location) Then
                    MouseDownButton = True
                    ButtonHighlightAdjust = 16
                    Me.Invalidate(rectDropDownButton)

                    'RunTime Only
                    If (Not DesignMode) Then
                        If (IsOpen) Then
                            CloseDropDown()
                        Else
                            OpenDropDown()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub DDContainer_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If MouseDownButton Then
            If DesignMode Then 'DesignTime Only
                IsOpen = Not IsOpen
                'Redraw the Selection Rectangle
                Dim selectservice As ISelectionService = _
                    CType(GetService(GetType(ISelectionService)), ISelectionService)
                Dim selection As New ArrayList
                selection.Clear()
                selectservice.SetSelectedComponents(selection, SelectionTypes.Replace)
                selection.Add(Me)
                selectservice.SetSelectedComponents(selection, SelectionTypes.Add)
            End If
            MouseDownButton = False
        End If
        ButtonHighlightAdjust = 4
        Me.Invalidate(rectDropDownButton)
    End Sub

#End Region 'Mouse Events

#Region "Painting"

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        'Draw the Graphic if available and resize
        If (_graphicImage IsNot Nothing) Then

            Dim GW As Integer = CInt(If(_graphicAutoWidth, _headerHeight * (_graphicImage.Width / _graphicImage.Height), _graphicWidth))

            e.Graphics.DrawImage(_graphicImage, 0, 0, GW, _headerHeight)
            e.Graphics.DrawRectangle(New Pen(_graphicBorderColor), _
                                     0, 0, GW, _headerHeight - 1)
        End If

        'Draw the Text Box
        If rectTextBox.Width > _textBoxCornerRadius * 2 Then DrawTextBox(e.Graphics)

        'Draw the Drop Down Button
        DrawDropDownButton(e.Graphics)

        'Adjust any miss placed control positioned on the Header
        For Each c As Control In Me.Controls
            If c.Location.Y < _headerHeight + 1 Then
                If CStr(c.Tag) <> "IgnoreMe" Then
                    c.Location = New Point(c.Location.X, _headerHeight + 1)
                End If
            End If
        Next
    End Sub

    Sub DrawTextBox(ByRef g As Graphics)

        If (Application.RenderWithVisualStyles) Then

            Dim renderer As New VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal)
            renderer.DrawBackground(g, rectTextBox)
        Else
            If Me._textBoxGradientType = eGradientType.Solid Then
                g.FillPath(New SolidBrush(_textBoxBackColorA), _
                    GetRectPath(rectTextBox, TextBoxCornerRadius))
            Else
                Using lgbr As LinearGradientBrush = New LinearGradientBrush _
                  (rectTextBox, _textBoxBackColorA, _textBoxBackColorB, _
                  CType([Enum].Parse(GetType(LinearGradientMode), _
                  _textBoxGradientType.ToString), LinearGradientMode))

                    g.FillPath(lgbr, GetRectPath(rectTextBox, TextBoxCornerRadius))
                End Using
            End If

            g.DrawPath(New Pen(_textBoxBorderColor), GetRectPath(rectTextBox, TextBoxCornerRadius))
        End If

        'Draw the Text if Available
        If _text <> "" Then

            Using sf As StringFormat = New StringFormat
                sf.Alignment = _textAlignment
                sf.LineAlignment = StringAlignment.Center
                sf.FormatFlags = StringFormatFlags.NoWrap
                sf.Trimming = StringTrimming.EllipsisCharacter
                If _textShadow Then
                    Dim Shadow As Rectangle = rectTextBox
                    Shadow.Offset(1, 1)
                    g.DrawString(_text, Me.Font, _
                        New SolidBrush(_textShadowColor), Shadow, sf)
                End If

                Dim rect As New Rectangle(rectTextBox.X + 1, rectTextBox.Y, rectTextBox.Width, rectTextBox.Height)

                g.DrawString(_text, Me.Font, _
                    New SolidBrush(Me.ForeColor), rect, sf)
            End Using

        End If
    End Sub

    Sub DrawDropDownButton(ByRef g As Graphics)

        If (Application.RenderWithVisualStyles) Then

            If IsOpen Then

                Dim renderer As New VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownPressed)
                renderer.DrawBackground(g, rectDropDownButton)
            Else
                Dim renderer As New VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownNormal)
                renderer.DrawBackground(g, rectDropDownButton)
            End If
        Else

            If IsOpen Then
                ControlPaint.DrawScrollButton(g, rectDropDownButton, ScrollButton.Down, ButtonState.Pushed)
            Else
                ControlPaint.DrawScrollButton(g, rectDropDownButton, ScrollButton.Down, ButtonState.Normal)
            End If
        End If
    End Sub

    Function GetButtonPath() As GraphicsPath
        Dim gp As New GraphicsPath
        If ButtonShape = eButtonShape.Circle Then
            gp.AddEllipse(rectDropDownButton)
        Else
            gp.AddRectangle(rectDropDownButton)
        End If
        Return gp
    End Function

    Public Function GetRectPath(BaseRect As RectangleF, CornerRadius As Integer) As GraphicsPath
        Dim ArcRect As RectangleF
        Dim MyPath As New GraphicsPath()
        If CornerRadius = 0 Then
            MyPath.AddRectangle(BaseRect)
        Else
            With MyPath
                ArcRect = New RectangleF(BaseRect.Location, _
                    New SizeF(CornerRadius * 2, CornerRadius * 2))
                ' top left arc
                .AddArc(ArcRect, 180, 90)

                ' top right arc
                ArcRect.X = BaseRect.Right - (CornerRadius * 2)
                .AddArc(ArcRect, 270, 90)

                ' bottom right arc
                ArcRect.Y = BaseRect.Bottom - (CornerRadius * 2)
                .AddArc(ArcRect, 0, 90)

                ' bottom left arc
                ArcRect.X = BaseRect.Left
                .AddArc(ArcRect, 90, 90)

                .CloseFigure()
            End With
        End If

        Return MyPath
    End Function

#End Region 'Painting

#Region "Methods"

    Private Sub UpdateTextArea()
        Dim vbit As Integer = 0
        Dim ShowPin As Integer = 0

        If _graphicImage IsNot Nothing Then
            vbit = CInt(If(_graphicAutoWidth, _headerHeight * (_graphicImage.Width / _graphicImage.Height), _graphicWidth)) + 3
        End If

        rectDropDownButton.X = _headerWidth - rectDropDownButton.Width
        rectTextBox = New Rectangle(vbit, 0, _headerWidth - rectDropDownButton.Width - ShowPin - vbit, _headerHeight)
    End Sub

    Private Sub UpdateRegion()
        Me.Region = Nothing
        Dim rgn As Region = New Region(New Rectangle(0, 0, Me.HeaderWidth, _headerHeight + 1))
        rgn.Union(New RectangleF(0, _headerHeight + 2, Me.PanelSize.Width, Me.PanelSize.Height))
        Me.Region = rgn.Clone
        rgn.Dispose()
    End Sub

#Region "Dropdown - RunTime"
    Private HorzPos As Integer
    Private VertPos As Integer

    Public Sub OpenDropDown()
        If TSHost IsNot Nothing Then
            VertPos = CInt(Me.rectTextBox.Bottom + 2)
            Dim TSDDD As ToolStripDropDownDirection = ToolStripDropDownDirection.BelowRight
            Select Case _ddAlignment
                Case StringAlignment.Far
                    HorzPos = Me.Width - Me.TSHost.Width
                Case StringAlignment.Near
                    HorzPos = 0
                Case StringAlignment.Center
                    HorzPos = (Me.Width - Me.TSHost.Width) \ 2
            End Select
            Try
                If Me.Location.Y + Me.ParentForm.Location.Y + VertPos + Me.TSHost.Height > Screen.FromControl(Me).WorkingArea.Bottom - 35 Then
                    VertPos = CInt(Me.rectTextBox.Top - 2)
                    TSDDD = ToolStripDropDownDirection.AboveRight
                End If
            Catch ex As Exception

            End Try
            Me.TSDropDown.Show(Me, New Point(HorzPos, VertPos), TSDDD)
            IsOpen = True
        End If
    End Sub

    Public Sub CloseDropDown()
        Me.TSDropDown.Hide()
        IsOpen = False
    End Sub

    Public Sub ForceCloseDropDown()
        Me.TSDropDown.Hide()
        IsOpen = False
    End Sub

#End Region 'Dropdown - RunTime

#Region "Dropdown - DesignTime"

    Private Sub CloseDesignDropDown()
        IsOpen = False
    End Sub

    Private Sub OpenDesignDropDown()
        IsOpen = True
    End Sub

#End Region 'Dropdown - DesignTime

    Private Sub ResizeMe()
        blnIsResizeOK = False
        If _isOpen Then
            If DesignMode Then
                If Me.Width < Me.HeaderWidth Then Me.Width = Me.HeaderWidth
                If Me.Height < _headerHeight Then Me.Height = _headerHeight
                UpdateRegion()
            End If
        Else
            If Me.Width < 21 Then Me.Width = 21
            If Me.HeaderWidth <> Me.Width Then Me.HeaderWidth = Me.Width
            Me.Region = Nothing
        End If
        blnIsResizeOK = True
    End Sub

    Private Sub SizeToDropControl()
        If _dropControl IsNot Nothing Then
            Me.PanelSize = Size.Add(_dropControl.Size, Me._ddPadding.Size)

            If DesignMode Then
                _dropControl.Location = New Point(Me._ddPadding.Left, _headerHeight + 2 + Me._ddPadding.Top)
            Else
                TSDropDown.Size = Size.Add(Me.PanelSize, New Size(2, 2))
                _dropControl.Location = New Point(Me._ddPadding.Left + 1, Me._ddPadding.Top + 1)
            End If
        End If
    End Sub
#End Region 'Methods

#Region "Control Events"

    Private Sub DDContainer_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        'Block resizing when the parent form minimizes 
        If Not DesignMode Then
            ForceCloseDropDown()
        End If

        If ((Me.FindForm IsNot Nothing) AndAlso _
            (Me.FindForm.WindowState <> FormWindowState.Minimized) AndAlso blnIsResizeOK) Then
            ResizeMe()
        End If
    End Sub

    Private Sub DDContainer_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        If Not _isOpen AndAlso Me.rectTextBox.Contains(e.Location) Then OpenDropDown()
    End Sub

    Private Sub DDContainer_ControlRemoved(sender As Object, e As ControlEventArgs) Handles Me.ControlRemoved
        If DesignMode And e.Control Is _dropControl Then
            RemoveHandler _dropControl.Resize, AddressOf DropControl_Resize
            RemoveHandler _dropControl.Move, AddressOf DropControl_Move
            _dropControl = Nothing
        End If
    End Sub

#End Region 'Control Events

#Region "IC1EmbeddedEditor"

    Public Function C1EditorFormat(value As Object, mask As String) As String Implements IC1EmbeddedEditor.C1EditorFormat

        Return value.ToString()
    End Function

    Public Function C1EditorGetStyle() As System.Drawing.Design.UITypeEditorEditStyle Implements IC1EmbeddedEditor.C1EditorGetStyle

        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Interface IEmbeddableDropDown

        Function GetValue() As Object
        Sub SetValue(value As Object)

        Function GetPreferredBounds() As Size
    End Interface

    Public Event GridValueChanged As Action(Of Object, Object)

    Public Function C1EditorGetValue() As Object Implements IC1EmbeddedEditor.C1EditorGetValue

        If (TypeOf DropControl Is IEmbeddableDropDown) Then

            If (Not GridValueChangedFired) Then RaiseEvent GridValueChanged(Me, DirectCast(DropControl, IEmbeddableDropDown).GetValue())
            GridValueChangedFired = True

            Return DirectCast(DropControl, IEmbeddableDropDown).GetValue()
        End If

        If (Not GridValueChangedFired) Then RaiseEvent GridValueChanged(Me, Me._text)
        GridValueChangedFired = True

        Return Me._text
    End Function

    Private GridValueChangedFired As Boolean

    Public Sub C1EditorInitialize(value As Object, editorAttributes As IDictionary) Implements IC1EmbeddedEditor.C1EditorInitialize

        GridValueChangedFired = False

        Me._text = CStr(value)

        If (TypeOf DropControl Is IEmbeddableDropDown) Then
            DirectCast(DropControl, IEmbeddableDropDown).SetValue(value)
        End If
    End Sub

    Public Function C1EditorKeyDownFinishEdit(e As KeyEventArgs) As Boolean Implements IC1EmbeddedEditor.C1EditorKeyDownFinishEdit

        If (e.KeyCode = Keys.Escape) OrElse (e.KeyCode = Keys.Tab) OrElse (e.KeyCode = Keys.Enter) Then Return True
        Return False
    End Function

    Public Sub C1EditorUpdateBounds(rc As System.Drawing.Rectangle) Implements IC1EmbeddedEditor.C1EditorUpdateBounds

        Me._headerWidth = rc.Width
        Me._headerHeight = rc.Height

        Me.rectDropDownButton.Height = rc.Height
        Me.rectDropDownButton.Width = rc.Height - 1

        Me.Bounds = rc
        Me.UpdateTextArea()
    End Sub

    Public Overridable Function C1EditorValueIsValid() As Boolean Implements IC1EmbeddedEditor.C1EditorValueIsValid

        Return True
    End Function

#End Region

End Class