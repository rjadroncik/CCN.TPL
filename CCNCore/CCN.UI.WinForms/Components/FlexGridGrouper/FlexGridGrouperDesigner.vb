Public Class FlexGridGrouperDesigner
    Inherits Windows.Forms.Design.ParentControlDesigner

    Public Overrides Sub Initialize(ByVal component As System.ComponentModel.IComponent)
        MyBase.Initialize(component)

        Dim grouper As FlexGridGrouper = DirectCast(component, FlexGridGrouper)

        Me.EnableDesignMode(grouper.Grid, "Grid")
    End Sub

End Class
