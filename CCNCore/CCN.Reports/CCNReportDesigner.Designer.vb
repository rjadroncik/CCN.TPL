<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CCNReportDesigner
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.VScrollBar = New System.Windows.Forms.VScrollBar
        Me.HScrollBar = New System.Windows.Forms.HScrollBar
        Me.SuspendLayout()
        '
        'VScrollBar
        '
        Me.VScrollBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VScrollBar.Location = New System.Drawing.Point(404, 0)
        Me.VScrollBar.Name = "VScrollBar"
        Me.VScrollBar.Size = New System.Drawing.Size(16, 344)
        Me.VScrollBar.TabIndex = 0
        '
        'HScrollBar
        '
        Me.HScrollBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HScrollBar.Location = New System.Drawing.Point(0, 344)
        Me.HScrollBar.Name = "HScrollBar"
        Me.HScrollBar.Size = New System.Drawing.Size(404, 16)
        Me.HScrollBar.TabIndex = 1
        '
        'CCNReportDesigner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScrollMargin = New System.Drawing.Size(50, 50)
        Me.Controls.Add(Me.HScrollBar)
        Me.Controls.Add(Me.VScrollBar)
        Me.Name = "CCNReportDesigner"
        Me.Size = New System.Drawing.Size(420, 360)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VScrollBar As System.Windows.Forms.VScrollBar
    Friend WithEvents HScrollBar As System.Windows.Forms.HScrollBar

End Class
