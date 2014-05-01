<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlexGridOverlay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Group = New CCN.UI.WinForms.FlexGridGroup()
        CType(Me.Group, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Group
        '
        Me.Group.AutoSize = True
        Me.Group.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Group.Column = Nothing
        Me.Group.ColumnOriginalIndex = 0
        Me.Group.ColumnWidthDisplay = 0
        Me.Group.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Group.Location = New System.Drawing.Point(0, 0)
        Me.Group.MinimumSize = New System.Drawing.Size(40, 17)
        Me.Group.Name = "Group"
        Me.Group.Size = New System.Drawing.Size(83, 17)
        Me.Group.TabIndex = 0
        Me.Group.Tag = Nothing
        Me.Group.Text = "Flex grid overlay"
        Me.Group.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Group.TextDetached = True
        '
        'FlexGridOverlay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(135, 17)
        Me.ControlBox = False
        Me.Controls.Add(Me.Group)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FlexGridOverlay"
        Me.Opacity = 0.8R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "OverlayWindow"
        CType(Me.Group, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Group As FlexGridGroup
End Class
