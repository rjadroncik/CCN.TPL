Imports C1.C1Preview

Public MustInherit Class CCNPrintElement

#Region "Internal"

    Public Overridable Sub OnParentChanged()
        _document = _parent.Document
    End Sub

#End Region

#Region "MustOverride"

    Public MustOverride Function Render() As RenderObject
    Public MustOverride Function Copy() As CCNPrintElement

#End Region

#Region "Properties"

    Protected _document As CCNPrintDocument
    Public ReadOnly Property Document() As CCNPrintDocument
        Get
            Return _document
        End Get
    End Property

    Protected _c1Object As RenderObject
    Public ReadOnly Property C1Object() As RenderObject
        Get
            Return _c1Object
        End Get
    End Property

    Protected _parent As CCNPrintContainer
    Public Property Parent() As CCNPrintContainer
        Get
            Return _parent
        End Get
        Set(ByVal value As CCNPrintContainer)

            If (Not IsNothing(_parent)) Then
                If (_parent.Children.Contains(Me)) Then _parent.Children.Remove(Me)
            End If

            _parent = value
            If (Not IsNothing(_parent)) Then

                If (Not _document Is _parent.Document) Then OnParentChanged()

                If (Not _parent.Children.Contains(Me)) Then _parent.Children.Add(Me)
            End If
        End Set
    End Property


#End Region

#Region "Properties - Data binding"

    Protected _id As String = ""
    Public Property Id As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)

            If (Not IsNothing(value)) Then

                If (_document._elementsById.ContainsKey(value)) Then
                    Throw New ArgumentException("Duplicitné ID objektu: " & value)
                End If

                If (_id <> "") Then _document._elementsById.Remove(_id)

                _id = value
                _document._elementsById.Add(_id, Me)
            Else
                If (_id <> "") Then _document._elementsById.Remove(_id)
                _id = ""
            End If
        End Set
    End Property

    Protected _valueId As String = ""
    Public Property ValueId As String
        Get
            Return _valueId
        End Get
        Set(ByVal value As String)
            If (Not IsNothing(value)) Then

                _valueId = value
            Else
                _valueId = ""
            End If
        End Set
    End Property

    Public ReadOnly Property ValueIdFull As String
        Get
            Dim result As String = ""
            Dim parent As CCNPrintContainer = _parent

            While (Not IsNothing(parent))

                If (parent._valueId.Length > 0) Then
                    If (result.Length > 0) Then result &= "."

                    result &= parent._valueId
                End If

                parent = parent._parent
            End While

            If (result.Length > 0) Then result &= "."
            result &= _valueId

            Return result
        End Get
    End Property

#End Region

#Region "Properties - Visual"

    Public Property OnNewPage As Boolean

    Protected _positioning As Positionings = Positionings.Flow
    Public Overridable Property Positioning As Positionings
        Get
            Return _positioning
        End Get
        Set(ByVal value As Positionings)
            _positioning = value
        End Set
    End Property

    Protected _x As Double = 0
    Public Overridable Property X As Double
        Get
            Return _x
        End Get
        Set(ByVal value As Double)
            _x = value

            If (_positioning = Positionings.Flow) Then _positioning = Positionings.Relative
        End Set
    End Property

    Protected _y As Double = 0
    Public Overridable Property Y As Double
        Get
            Return _y
        End Get
        Set(ByVal value As Double)
            _y = value

            If (_positioning = Positionings.Flow) Then _positioning = Positionings.Relative
        End Set
    End Property

    Protected _width As Double = 0
    Public Overridable Property Width As Double
        Get
            Return _width
        End Get
        Set(ByVal value As Double)
            _width = value
        End Set
    End Property

    Protected _height As Double = 0
    Public Overridable Property Height As Double
        Get
            Return _height
        End Get
        Set(ByVal value As Double)
            _height = value
        End Set
    End Property

    Protected _spacing As New CCNPrintOffsets
    Public ReadOnly Property Spacing As CCNPrintOffsets
        Get
            Return _spacing
        End Get
    End Property

#End Region

#Region "Initialization"

    Protected Sub New(ByVal document As CCNPrintDocument)

        _document = document
    End Sub

#End Region

#Region "Copying"

    Protected Sub CopyElementProperties(ByVal target As CCNPrintElement)

        target._positioning = _positioning

        target._x = _x
        target._y = _y

        target._width = _width
        target._height = _height

        target._spacing = New CCNPrintOffsets(_spacing)

        target._valueId = _valueId
    End Sub

#End Region

#Region "Rendering"

    Protected Sub ApplyElementProperties(ByVal element As RenderObject)

        If _positioning = Positionings.Absolute Then

            element.X = New Unit(_x, UnitTypeEnum.Mm)
            element.Y = New Unit(_y, UnitTypeEnum.Mm)

        ElseIf _positioning = Positionings.Relative Then

            If (Not IsNothing(_parent)) Then

                element.X = "parent.left + " & Str(_x) & "mm"
                element.Y = "parent.top + " & Str(_y) & "mm"
            Else
                element.X = New Unit(_x, UnitTypeEnum.Mm)
                element.Y = New Unit(_y, UnitTypeEnum.Mm)
            End If
        Else
            element.X = C1.C1Preview.Unit.Auto
            element.Y = C1.C1Preview.Unit.Auto
        End If

        If (_height = 0) Then
            element.Height = C1.C1Preview.Unit.Auto
        Else
            element.Height = New Unit(_height, UnitTypeEnum.Mm)
        End If

        If (_width = 0) Then
            element.Width = C1.C1Preview.Unit.Auto
        Else
            element.Width = New Unit(_width, UnitTypeEnum.Mm)
        End If

        element.Style.Spacing.Left = Spacing.Left.Ammount
        element.Style.Spacing.Right = Spacing.Right.Ammount
        element.Style.Spacing.Top = Spacing.Top.Ammount
        element.Style.Spacing.Bottom = Spacing.Bottom.Ammount

        If (OnNewPage) Then element.BreakBefore = BreakEnum.Page
    End Sub

#End Region

#Region "Enums"

    Public Enum Positionings
        Relative
        Absolute
        Flow
    End Enum

#End Region

End Class
