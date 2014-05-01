Imports C1.Win.C1Input
Imports C1.Win.C1FlexGrid

Public Class FlexGridGroup
    Inherits C1Label

    Public Property ColumnOriginalIndex As Integer
    Public Property ColumnWidthDisplay As Integer
    Public Property Column As Column

    Public Sub New()

        MinimumSize = New Size(40, 17)
        AutoSize = False
        Height = 17
        BackColor = SystemColors.ButtonFace
        TextDetached = True
        TextAlign = ContentAlignment.MiddleCenter
        BorderStyle = Windows.Forms.BorderStyle.None
    End Sub

    Protected LightPen As New Pen(SystemColors.ButtonHighlight)
    Protected ShadowPen As New Pen(SystemColors.ButtonShadow)

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.DrawLine(LightPen, 0, 0, Width - 1, 0)
        e.Graphics.DrawLine(LightPen, 0, 0, 0, Height - 1)

        e.Graphics.DrawLine(ShadowPen, Width - 1, 0, Width - 1, Height - 1)
        e.Graphics.DrawLine(ShadowPen, 0, Height - 1, Width - 1, Height - 1)
    End Sub

End Class
