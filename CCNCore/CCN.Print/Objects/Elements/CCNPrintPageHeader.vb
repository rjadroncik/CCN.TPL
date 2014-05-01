Imports C1.C1Preview

Public Class CCNPrintPageHeader
    Inherits CCNPrintContainer

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintHeader(_document)

        CopyElementProperties(result)
        CopyRectangleProperties(result)
        CopyContainerProperties(result)

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

        Dim area As RenderArea = New RenderArea()
        _c1Object = area

        ApplyRectangleProperties(area)

        area.Style.Padding.Left = _padding.Left.Ammount
        area.Style.Padding.Right = _padding.Right.Ammount
        area.Style.Padding.Top = _padding.Top.Ammount
        area.Style.Padding.Bottom = _padding.Bottom.Ammount

        area.Stacking = _stacking
        area.SplitVertBehavior = _splitVertical

        'If (Not IsNothing(_parent)) Then

        '    _parent.C1Object.Children.Add(area)
        'Else
        '    _document.C1Document.Body.Children.Add(area)
        'End If

        For Each element As CCNPrintElement In _children

            element.Render()
        Next

        Return area
    End Function

#End Region

End Class
