Imports C1.C1Preview

Public Class CCNPrintTableCell
    Inherits CCNPrintTableElement

#Region "Initialization"

    Public Sub New(ByVal table As CCNPrintTable)

        MyBase.New(table)
    End Sub

#End Region

#Region "Properties"

    Public Property SpanCols As Integer
    Public Property SpanRows As Integer

    Private _text As String = ""
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

#End Region

#Region "Rendering"

    Public Overridable Sub ApplyStyle(ByVal cell As TableCell)

        If (_propertiesChanged.Contains(Properties.TextColor)) Then cell.Style.TextColor = _textColor
        If (_propertiesChanged.Contains(Properties.AlignmentHorizontal)) Then cell.Style.TextAlignHorz = _alignmentHorizontal
        If (_propertiesChanged.Contains(Properties.AlignmentVertical)) Then cell.Style.TextAlignVert = _alignmentVertical

        If (_propertiesChanged.Contains(Properties.FontSize)) Then cell.Style.FontSize = _fontSize
        If (_propertiesChanged.Contains(Properties.FontName)) Then cell.Style.FontName = _fontName

        If (_propertiesChanged.Contains(Properties.FontBold)) Then cell.Style.FontBold = _fontBold
        If (_propertiesChanged.Contains(Properties.FontItalic)) Then cell.Style.FontItalic = _fontItalic
        If (_propertiesChanged.Contains(Properties.FontUnderline)) Then cell.Style.FontUnderline = _fontUnderline

        If (_borders.Left.Changed) Then cell.CellStyle.Borders.Left = New LineDef(New Unit(_borders.Left.Thickness, UnitTypeEnum.Mm), _borders.Left.Color, _borders.Left.DashStyle)
        If (_borders.Top.Changed) Then cell.CellStyle.Borders.Top = New LineDef(New Unit(_borders.Top.Thickness, UnitTypeEnum.Mm), _borders.Top.Color, _borders.Top.DashStyle)
        If (_borders.Right.Changed) Then cell.CellStyle.Borders.Right = New LineDef(New Unit(_borders.Right.Thickness, UnitTypeEnum.Mm), _borders.Right.Color, _borders.Right.DashStyle)
        If (_borders.Bottom.Changed) Then cell.CellStyle.Borders.Bottom = New LineDef(New Unit(_borders.Bottom.Thickness, UnitTypeEnum.Mm), _borders.Bottom.Color, _borders.Bottom.DashStyle)

        If (_gridLines.Left.Changed) Then cell.Style.GridLines.Left = New LineDef(New Unit(_gridLines.Left.Thickness, UnitTypeEnum.Mm), _gridLines.Left.Color, _gridLines.Left.DashStyle)
        If (_gridLines.Top.Changed) Then cell.Style.GridLines.Top = New LineDef(New Unit(_gridLines.Top.Thickness, UnitTypeEnum.Mm), _gridLines.Top.Color, _gridLines.Top.DashStyle)
        If (_gridLines.Right.Changed) Then cell.Style.GridLines.Right = New LineDef(New Unit(_gridLines.Right.Thickness, UnitTypeEnum.Mm), _gridLines.Right.Color, _gridLines.Right.DashStyle)
        If (_gridLines.Bottom.Changed) Then cell.Style.GridLines.Bottom = New LineDef(New Unit(_gridLines.Bottom.Thickness, UnitTypeEnum.Mm), _gridLines.Bottom.Color, _gridLines.Bottom.DashStyle)
        If (_gridLines.Vertical.Changed) Then cell.Style.GridLines.Vert = New LineDef(New Unit(_gridLines.Vertical.Thickness, UnitTypeEnum.Mm), _gridLines.Vertical.Color, _gridLines.Vertical.DashStyle)
        If (_gridLines.Horizontal.Changed) Then cell.Style.GridLines.Horz = New LineDef(New Unit(_gridLines.Horizontal.Thickness, UnitTypeEnum.Mm), _gridLines.Horizontal.Color, _gridLines.Horizontal.DashStyle)

        If (_padding.Left.Changed) Then cell.CellStyle.Padding.Left = New Unit(_padding.Left.Ammount, UnitTypeEnum.Mm)
        If (_padding.Top.Changed) Then cell.CellStyle.Padding.Top = New Unit(_padding.Top.Ammount, UnitTypeEnum.Mm)
        If (_padding.Right.Changed) Then cell.CellStyle.Padding.Right = New Unit(_padding.Right.Ammount, UnitTypeEnum.Mm)
        If (_padding.Bottom.Changed) Then cell.CellStyle.Padding.Bottom = New Unit(_padding.Bottom.Ammount, UnitTypeEnum.Mm)

        If (_propertiesChanged.Contains(Properties.BackgroundColor)) Then cell.Style.BackColor = _backgroundColor

        If (SpanCols > 1) Then cell.SpanCols = SpanCols
        If (SpanRows > 1) Then cell.SpanRows = SpanRows
    End Sub

#End Region

End Class
