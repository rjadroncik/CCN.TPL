Imports C1.C1Preview

Public Class CCNPrintLine
    Inherits CCNPrintElement

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintLine(_document)

        CopyElementProperties(result)

        result._x1 = _x1
        result._y1 = _y1
        result._x2 = _x2
        result._y2 = _y2

        result._thickness = _thickness
        result._dashStyle = _dashStyle
        result._color = _color

        Return result
    End Function

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As RenderObject

        Dim line As New RenderLine(_x1, _y1, _x2, _y2, New LineDef(_thickness, _color, _dashStyle))
        _c1Object = line

        line.X = 0
        line.Y = 0

        If (_height = 0) Then
            line.Height = C1.C1Preview.Unit.Auto
        Else
            line.Height = New Unit(_height, UnitTypeEnum.Mm)
        End If

        If (_width = 0) Then
            line.Width = C1.C1Preview.Unit.Auto
        Else
            line.Width = New Unit(_width, UnitTypeEnum.Mm)
        End If

        If _positioning = Positionings.Absolute Then

            line.Line.X1 = New Unit(_x1, UnitTypeEnum.Mm)
            line.Line.Y1 = New Unit(_y1, UnitTypeEnum.Mm)

            line.Line.X2 = New Unit(_x2, UnitTypeEnum.Mm)
            line.Line.Y2 = New Unit(_y2, UnitTypeEnum.Mm)

        ElseIf _positioning = Positionings.Relative Then

            If (Not IsNothing(_parent)) Then

                line.Line.X1 = "parent.left + " & Str(_x1) & "mm"
                line.Line.Y1 = "parent.top + " & Str(_y1) & "mm"

                line.Line.X2 = "parent.left + " & Str(_x2) & "mm"
                line.Line.Y2 = "parent.top + " & Str(_y2) & "mm"
            Else
                line.Line.X1 = New Unit(_x1, UnitTypeEnum.Mm)
                line.Line.Y1 = New Unit(_y1, UnitTypeEnum.Mm)

                line.Line.X2 = New Unit(_x2, UnitTypeEnum.Mm)
                line.Line.Y2 = New Unit(_y2, UnitTypeEnum.Mm)
            End If
        Else
            line.Line.X1 = C1.C1Preview.Unit.Auto
            line.Line.Y1 = C1.C1Preview.Unit.Auto

            line.Line.X2 = "self.left + " & Str(_width) & "mm"
            line.Line.Y2 = "self.top + " & Str(_height) & "mm"
        End If

        line.Style.Spacing.Left = Spacing.Left.Ammount
        line.Style.Spacing.Right = Spacing.Right.Ammount
        line.Style.Spacing.Top = Spacing.Top.Ammount
        line.Style.Spacing.Bottom = Spacing.Bottom.Ammount

        If (Not IsNothing(_parent)) Then

            _parent.C1Object.Children.Add(line)
        Else
            _document.C1Document.Body.Children.Add(line)
        End If

        Return line
    End Function

#End Region

#Region "Properties"

    Protected Sub AdjustBoundingBox()

        If (_x1 < _x2) Then
            _x = _x1
            _width = _x2 - _x1
        Else
            _x = _x2
            _width = _x1 - _x2
        End If

        If (_y1 < _y2) Then
            _y = _y1
            _height = _y2 - _y1
        Else
            _y = _y2
            _height = _y1 - _y2
        End If

    End Sub

    Public Overrides Property X() As Double
        Get
            Return _x
        End Get
        Set(ByVal value As Double)
            Throw New InvalidOperationException("Cannot directly set the position of a line!")
        End Set
    End Property

    Public Overrides Property Y() As Double
        Get
            Return _y
        End Get
        Set(ByVal value As Double)
            Throw New InvalidOperationException("Cannot directly set the position of a line!")
        End Set
    End Property

    Protected _x1 As Double = 0
    Public Property X1() As Double
        Get
            Return _x1
        End Get
        Set(ByVal value As Double)
            _x1 = value

            If (_positioning = Positionings.Flow) Then _positioning = Positionings.Relative
            AdjustBoundingBox()
        End Set
    End Property

    Private _y1 As Double = 0
    Public Property Y1() As Double
        Get
            Return _y1
        End Get
        Set(ByVal value As Double)
            _y1 = value

            If (_positioning = Positionings.Flow) Then _positioning = Positionings.Relative
            AdjustBoundingBox()
        End Set
    End Property

    Protected _x2 As Double = 0
    Public Property X2() As Double
        Get
            Return _x2
        End Get
        Set(ByVal value As Double)
            _x2 = value

            If (_positioning = Positionings.Flow) Then _positioning = Positionings.Relative
            AdjustBoundingBox()
        End Set
    End Property

    Private _y2 As Double = 0
    Public Property Y2() As Double
        Get
            Return _y2
        End Get
        Set(ByVal value As Double)
            _y2 = value

            If (_positioning = Positionings.Flow) Then _positioning = Positionings.Relative
            AdjustBoundingBox()
        End Set
    End Property

    Public Overrides Property Width() As Double
        Get
            Return _width
        End Get
        Set(ByVal value As Double)
            Throw New InvalidOperationException("Cannot directly set the width of a line! (width <> thickness)")
        End Set
    End Property

    Public Overrides Property Height() As Double
        Get
            Return _height
        End Get
        Set(ByVal value As Double)
            Throw New InvalidOperationException("Cannot directly set the height of a line!")
        End Set
    End Property

    Public Property Thickness As Double = 0.5
    Public Property DashStyle As Drawing.Drawing2D.DashStyle = Drawing.Drawing2D.DashStyle.Solid

    Public Overridable Property Color As Drawing.Color = Drawing.Color.Black

#End Region

End Class
