Imports C1.C1Preview

Public MustInherit Class CCNPrintContainer
    Inherits CCNPrintRectangle

#Region "Internal"

    Public Overrides Sub OnParentChanged()

        _document = _parent.Document

        For Each child As CCNPrintElement In _children

            child.OnParentChanged()
        Next
    End Sub

#End Region

#Region "Copying"

    Protected Sub CopyContainerProperties(ByVal target As CCNPrintContainer)

        target._padding = New CCNPrintOffsets(_padding)
        target._stacking = _stacking
        target._splitVertical = _splitVertical

        For Each child As CCNPrintElement In _children

            child.Copy().Parent = target
        Next
    End Sub

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As RenderObject

        Dim area As RenderArea = New RenderArea()
        _c1Object = area

        ApplyRectangleProperties(area)

        area.Style.Padding.Left = _padding.Left.Ammount
        area.Style.Padding.Right = _padding.Right.Ammount
        area.Style.Padding.Top = _padding.Top.Ammount
        area.Style.Padding.Bottom = _padding.Bottom.Ammount

        area.Stacking = _stacking
        area.SplitVertBehavior = _splitVertical

        If (Not IsNothing(_parent)) Then

            _parent.C1Object.Children.Add(area)
        Else
            _document.C1Document.Body.Children.Add(area)
        End If

        For Each element As CCNPrintElement In _children

            element.Render()
        Next

        Return area
    End Function

#End Region

#Region "Properties"

    Protected _children As New List(Of CCNPrintElement)
    Public ReadOnly Property Children As List(Of CCNPrintElement)
        Get
            Return _children
        End Get
    End Property

    Protected _padding As New CCNPrintOffsets
    Public ReadOnly Property Padding As CCNPrintOffsets
        Get
            Return _padding
        End Get
    End Property

    Protected _stacking As StackingRulesEnum = StackingRulesEnum.BlockTopToBottom
    Public Property Stacking As StackingRulesEnum
        Get
            Return _stacking
        End Get
        Set(ByVal value As StackingRulesEnum)
            _stacking = value
        End Set
    End Property

    Protected _splitVertical As SplitBehaviorEnum = SplitBehaviorEnum.SplitIfNeeded
    Public Property SplitVertical As SplitBehaviorEnum
        Get
            Return _splitVertical
        End Get
        Set(ByVal value As SplitBehaviorEnum)
            _splitVertical = value
        End Set
    End Property

#End Region

End Class
