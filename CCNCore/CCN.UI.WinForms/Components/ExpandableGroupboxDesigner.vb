Imports System.ComponentModel

''' <summary>
''' Designer class that enables the user to expand/collapse the <see cref="ExpandableGroupbox" /> control during Design-Time.
''' </summary>
''' <remarks></remarks>
Public Class ExpandableGroupboxDesigner
    Inherits System.Windows.Forms.Design.ParentControlDesigner

    Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
        MyBase.Initialize(component)

        'Add an event handler for the button click event so we can update the control
        Dim btnExpand As Button = CType(Me.Control, ExpandableGroupbox).ExpandButton
        AddHandler btnExpand.Click, AddressOf ExpandButtonClicked
    End Sub

    Public Sub ExpandButtonClicked(ByVal sender As Object, ByVal ev As EventArgs)
        'Tell the designer to update the control
        'If we don't do this, the selection rectangle would not update
        Me.RaiseComponentChanged(TypeDescriptor.GetProperties(Me.Control)("Size"), Nothing, Nothing)
    End Sub

    'This function enables us to click the button during design-time
    Protected Overrides Function GetHitTest(ByVal point As System.Drawing.Point) As Boolean
        Dim btnExpand As Button = CType(Me.Control, ExpandableGroupbox).ExpandButton
        If (btnExpand.Bounds.Contains(Me.Control.PointToClient(point))) Then
            Return True
        End If
        Return MyBase.GetHitTest(point)
    End Function

End Class