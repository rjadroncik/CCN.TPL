Imports C1.C1Preview

Public Class CCNPrintRectangle
    Inherits CCNPrintElement

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintRectangle(_document)

        CopyElementProperties(result)
        CopyRectangleProperties(result)

        Return result

    End Function

    Protected Sub CopyRectangleProperties(ByVal target As CCNPrintRectangle)

        target._borders = New CCNPrintBorders(_borders)
        target._backgroundColor = _backgroundColor
    End Sub

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As RenderObject

        Dim rect As New RenderRectangle()
        _c1Object = rect

        ApplyRectangleProperties(rect)

        If (Not IsNothing(_parent)) Then

            _parent.C1Object.Children.Add(rect)
        Else
            _document.C1Document.Body.Children.Add(rect)
        End If

        Return rect
    End Function

    Protected Sub ApplyRectangleProperties(ByVal rect As RenderObject)

        ApplyElementProperties(rect)

        If (_borders.Left.Thickness > 0) Then
            rect.Style.Borders.Left = New LineDef(New Unit(_borders.Left.Thickness, UnitTypeEnum.Mm), _borders.Left.Color, _borders.Left.DashStyle)
        Else
            rect.Style.Borders.Left = New LineDef(0, Drawing.Color.Transparent, Drawing.Drawing2D.DashStyle.Solid)
        End If

        If (_borders.Top.Thickness > 0) Then
            rect.Style.Borders.Top = New LineDef(New Unit(_borders.Top.Thickness, UnitTypeEnum.Mm), _borders.Top.Color, _borders.Top.DashStyle)
        Else
            rect.Style.Borders.Top = New LineDef(0, Drawing.Color.Transparent, Drawing.Drawing2D.DashStyle.Solid)
        End If

        If (_borders.Right.Thickness > 0) Then
            rect.Style.Borders.Right = New LineDef(New Unit(_borders.Right.Thickness, UnitTypeEnum.Mm), _borders.Right.Color, _borders.Right.DashStyle)
        Else
            rect.Style.Borders.Right = New LineDef(0, Drawing.Color.Transparent, Drawing.Drawing2D.DashStyle.Solid)
        End If

        If (_borders.Bottom.Thickness > 0) Then
            rect.Style.Borders.Bottom = New LineDef(New Unit(_borders.Bottom.Thickness, UnitTypeEnum.Mm), _borders.Bottom.Color, _borders.Bottom.DashStyle)
        Else
            rect.Style.Borders.Bottom = New LineDef(0, Drawing.Color.Transparent, Drawing.Drawing2D.DashStyle.Solid)
        End If

        rect.Style.ShapeLine = New LineDef(0, Drawing.Color.Transparent, Drawing.Drawing2D.DashStyle.Solid)
        rect.Style.BackColor = _backgroundColor
    End Sub

#End Region

#Region "Properties"

    Public Overridable Property BackgroundColor As Drawing.Color = Drawing.Color.Transparent

    Protected _borders As New CCNPrintBorders
    Public ReadOnly Property Borders As CCNPrintBorders
        Get
            Return _borders
        End Get
    End Property

#End Region

End Class
