Imports C1.C1Preview

Public MustInherit Class CCNPrintTableElement

#Region "Enums"

    Public Enum Properties

        AlignmentVertical
        AlignmentHorizontal

        FontName
        FontSize
        FontBold
        FontItalic
        FontUnderline

        TextColor
        BackgroundColor

        Width
        Height
    End Enum

#End Region

#Region "Properties"

    Protected _table As CCNPrintTable
    Protected Sub New(ByVal table As CCNPrintTable)

        _table = table
    End Sub

    Protected _borders As New CCNPrintBorders
    Public ReadOnly Property Borders() As CCNPrintBorders
        Get
            Return _borders
        End Get
    End Property

    Protected _gridLines As New CCNPrintGridLines
    Public ReadOnly Property GridLines() As CCNPrintGridLines
        Get
            Return _gridLines
        End Get
    End Property

    Protected _padding As New CCNPrintOffsets
    Public ReadOnly Property Padding() As CCNPrintOffsets
        Get
            Return _padding
        End Get
    End Property

    Protected _propertiesChanged As New HashSet(Of Properties)
    Public ReadOnly Property PropertiesChanged() As HashSet(Of Properties)
        Get
            Return _propertiesChanged
        End Get
    End Property

#End Region

#Region "Properties - Change tracked"

    Protected _alignmentVertical As AlignVertEnum = AlignVertEnum.Top
    Public Property AlignmentVertical() As AlignVertEnum
        Get
            Return _alignmentVertical
        End Get
        Set(ByVal value As AlignVertEnum)
            _alignmentVertical = value

            If (Not _propertiesChanged.Contains(Properties.AlignmentVertical)) Then _propertiesChanged.Add(Properties.AlignmentVertical)
        End Set
    End Property

    Protected _alignmentHorizontal As AlignHorzEnum = AlignHorzEnum.Left
    Public Property AlignmentHorizontal() As AlignHorzEnum
        Get
            Return _alignmentHorizontal
        End Get
        Set(ByVal value As AlignHorzEnum)
            _alignmentHorizontal = value

            If (Not _propertiesChanged.Contains(Properties.AlignmentHorizontal)) Then _propertiesChanged.Add(Properties.AlignmentHorizontal)
        End Set
    End Property

    Protected _fontName As String = "Arial"
    Public Property FontName() As String
        Get
            Return _fontName
        End Get
        Set(ByVal value As String)
            _fontName = value

            If (Not _propertiesChanged.Contains(Properties.FontName)) Then _propertiesChanged.Add(Properties.FontName)
        End Set
    End Property

    Protected _fontSize As Single = 10.0F
    Public Property FontSize() As Single
        Get
            Return _fontSize
        End Get
        Set(ByVal value As Single)
            _fontSize = value

            If (Not _propertiesChanged.Contains(Properties.FontSize)) Then _propertiesChanged.Add(Properties.FontSize)
        End Set
    End Property

    Protected _fontBold As Boolean = False
    Public Property FontBold() As Boolean
        Get
            Return _fontBold
        End Get
        Set(ByVal value As Boolean)
            _fontBold = value

            If (Not _propertiesChanged.Contains(Properties.FontBold)) Then _propertiesChanged.Add(Properties.FontBold)
        End Set
    End Property

    Protected _fontItalic As Boolean = False
    Public Property FontItalic() As Boolean
        Get
            Return _fontItalic
        End Get
        Set(ByVal value As Boolean)
            _fontItalic = value

            If (Not _propertiesChanged.Contains(Properties.FontItalic)) Then _propertiesChanged.Add(Properties.FontItalic)
        End Set
    End Property

    Protected _fontUnderline As Boolean = False
    Public Property FontUnderline() As Boolean
        Get
            Return _fontUnderline
        End Get
        Set(ByVal value As Boolean)
            _fontUnderline = value

            If (Not _propertiesChanged.Contains(Properties.FontUnderline)) Then _propertiesChanged.Add(Properties.FontUnderline)
        End Set
    End Property

    Protected _textColor As Drawing.Color = Drawing.Color.Black
    Public Overridable Property TextColor() As Drawing.Color
        Get
            Return _textColor
        End Get
        Set(ByVal value As Drawing.Color)
            _textColor = value

            If (Not _propertiesChanged.Contains(Properties.TextColor)) Then _propertiesChanged.Add(Properties.TextColor)
        End Set
    End Property

    Protected _backgroundColor As Drawing.Color = Drawing.Color.Transparent
    Public Overridable Property BackgroundColor() As Drawing.Color
        Get
            Return _backgroundColor
        End Get
        Set(ByVal value As Drawing.Color)
            _backgroundColor = value

            If (Not _propertiesChanged.Contains(Properties.BackgroundColor)) Then _propertiesChanged.Add(Properties.BackgroundColor)
        End Set
    End Property

#End Region

End Class
