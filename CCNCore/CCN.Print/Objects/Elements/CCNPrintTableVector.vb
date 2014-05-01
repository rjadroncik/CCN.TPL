Imports C1.C1Preview

Public MustInherit Class CCNPrintTableVector
    Inherits CCNPrintTableElement

#Region "Properties"

    Public Property PageBreakBehavior As PageBreakBehaviorEnum = PageBreakBehaviorEnum.Default

#End Region

#Region "Initialization"

    Protected Sub New(ByVal table As CCNPrintTable)

        MyBase.New(table)
    End Sub

#End Region

#Region "Rendering"

    Public Overridable Sub ApplyStyle(ByVal vector As TableVector)

        If (_propertiesChanged.Contains(Properties.TextColor)) Then vector.Style.TextColor = _textColor
        If (_propertiesChanged.Contains(Properties.AlignmentHorizontal)) Then vector.Style.TextAlignHorz = _alignmentHorizontal
        If (_propertiesChanged.Contains(Properties.AlignmentVertical)) Then vector.Style.TextAlignVert = _alignmentVertical

        If (_propertiesChanged.Contains(Properties.FontSize)) Then vector.Style.FontSize = _fontSize
        If (_propertiesChanged.Contains(Properties.FontName)) Then vector.Style.FontName = _fontName

        If (_propertiesChanged.Contains(Properties.FontBold)) Then vector.Style.FontBold = _fontBold
        If (_propertiesChanged.Contains(Properties.FontItalic)) Then vector.Style.FontItalic = _fontItalic
        If (_propertiesChanged.Contains(Properties.FontUnderline)) Then vector.Style.FontUnderline = _fontUnderline

        If (_borders.Left.Changed) Then vector.CellStyle.Borders.Left = New LineDef(New Unit(_borders.Left.Thickness, UnitTypeEnum.Mm), _borders.Left.Color, _borders.Left.DashStyle)
        If (_borders.Top.Changed) Then vector.CellStyle.Borders.Top = New LineDef(New Unit(_borders.Top.Thickness, UnitTypeEnum.Mm), _borders.Top.Color, _borders.Top.DashStyle)
        If (_borders.Right.Changed) Then vector.CellStyle.Borders.Right = New LineDef(New Unit(_borders.Right.Thickness, UnitTypeEnum.Mm), _borders.Right.Color, _borders.Right.DashStyle)
        If (_borders.Bottom.Changed) Then vector.CellStyle.Borders.Bottom = New LineDef(New Unit(_borders.Bottom.Thickness, UnitTypeEnum.Mm), _borders.Bottom.Color, _borders.Bottom.DashStyle)

        If (_gridLines.Left.Changed) Then vector.Style.GridLines.Left = New LineDef(New Unit(_gridLines.Left.Thickness, UnitTypeEnum.Mm), _gridLines.Left.Color, _gridLines.Left.DashStyle)
        If (_gridLines.Top.Changed) Then vector.Style.GridLines.Top = New LineDef(New Unit(_gridLines.Top.Thickness, UnitTypeEnum.Mm), _gridLines.Top.Color, _gridLines.Top.DashStyle)
        If (_gridLines.Right.Changed) Then vector.Style.GridLines.Right = New LineDef(New Unit(_gridLines.Right.Thickness, UnitTypeEnum.Mm), _gridLines.Right.Color, _gridLines.Right.DashStyle)
        If (_gridLines.Bottom.Changed) Then vector.Style.GridLines.Bottom = New LineDef(New Unit(_gridLines.Bottom.Thickness, UnitTypeEnum.Mm), _gridLines.Bottom.Color, _gridLines.Bottom.DashStyle)
        If (_gridLines.Vertical.Changed) Then vector.Style.GridLines.Vert = New LineDef(New Unit(_gridLines.Vertical.Thickness, UnitTypeEnum.Mm), _gridLines.Vertical.Color, _gridLines.Vertical.DashStyle)
        If (_gridLines.Horizontal.Changed) Then vector.Style.GridLines.Horz = New LineDef(New Unit(_gridLines.Horizontal.Thickness, UnitTypeEnum.Mm), _gridLines.Horizontal.Color, _gridLines.Horizontal.DashStyle)

        If (_padding.Left.Changed) Then vector.CellStyle.Padding.Left = New Unit(_padding.Left.Ammount, UnitTypeEnum.Mm)
        If (_padding.Top.Changed) Then vector.CellStyle.Padding.Top = New Unit(_padding.Top.Ammount, UnitTypeEnum.Mm)
        If (_padding.Right.Changed) Then vector.CellStyle.Padding.Right = New Unit(_padding.Right.Ammount, UnitTypeEnum.Mm)
        If (_padding.Bottom.Changed) Then vector.CellStyle.Padding.Bottom = New Unit(_padding.Bottom.Ammount, UnitTypeEnum.Mm)

        If (_propertiesChanged.Contains(Properties.BackgroundColor)) Then vector.Style.BackColor = _backgroundColor

        vector.PageBreakBehavior = _PageBreakBehavior
    End Sub

#End Region

End Class
