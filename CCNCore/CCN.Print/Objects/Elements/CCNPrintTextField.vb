Imports C1.C1Preview

Public MustInherit Class CCNPrintTextField
    Inherits CCNPrintRectangle

#Region "Initialization"

    Protected Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Copying"

    Protected Sub CopyTextFieldProperties(ByVal target As CCNPrintTextField)

        target._AlignmentVertical = _AlignmentVertical
        target._AlignmentHorizontal = _AlignmentHorizontal

        target._FontName = _FontName
        target._FontSize = _FontSize

        target._FontBold = _FontBold
        target._FontItalic = _FontItalic
        target._FontUnderline = _FontUnderline

        target._TextColor = _TextColor

        target._padding = New CCNPrintOffsets(_padding)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As RenderObject

        Dim field As New RenderText()
        _c1Object = field

        ApplyRectangleProperties(field)

        field.Style.Padding.Left = Padding.Left.Ammount
        field.Style.Padding.Right = Padding.Right.Ammount
        field.Style.Padding.Top = Padding.Top.Ammount
        field.Style.Padding.Bottom = Padding.Bottom.Ammount

        field.Style.TextColor = _TextColor
        field.Style.TextAlignHorz = _AlignmentHorizontal
        field.Style.TextAlignVert = _AlignmentVertical

        field.Style.FontSize = _FontSize
        field.Style.FontName = _FontName

        field.Style.FontBold = _FontBold
        field.Style.FontItalic = _FontItalic
        field.Style.FontUnderline = _FontUnderline

        field.Text = Text

        If (Not IsNothing(_parent)) Then

            _parent.C1Object.Children.Add(field)
        Else
            _document.C1Document.Body.Children.Add(field)
        End If

        Return field
    End Function

#End Region

#Region "Properties - MustOverride"

    Public MustOverride Property Text() As String

#End Region

#Region "Properties"

    Public Property AlignmentVertical As AlignVertEnum = AlignVertEnum.Top
    Public Property AlignmentHorizontal As AlignHorzEnum = AlignHorzEnum.Left

    Public Property FontName As String = "Arial"
    Public Property FontSize As Double = 4.23

    Public Property FontBold As Boolean
    Public Property FontItalic As Boolean
    Public Property FontUnderline As Boolean

    Public Overridable Property TextColor As Drawing.Color = Drawing.Color.Black

    Protected _padding As New CCNPrintOffsets
    Public ReadOnly Property Padding As CCNPrintOffsets
        Get
            Return _padding
        End Get
    End Property

#End Region

End Class
