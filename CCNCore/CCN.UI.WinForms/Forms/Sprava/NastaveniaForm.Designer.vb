<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NastaveniaForm
    Inherits Form

    'Form overrides dispose to clean up the component list.
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
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.UlozButton = New System.Windows.Forms.ToolStripButton()
        Me.ZrusButton = New System.Windows.Forms.ToolStripButton()
        Me.Editor = New CCN.UI.WinForms.NastaveniaEditor()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UlozButton, Me.ZrusButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(792, 25)
        Me.ToolStrip.TabIndex = 4
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'UlozButton
        '
        Me.UlozButton.Image = Global.CCN.UI.WinForms.Resources.small_save
        Me.UlozButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UlozButton.Name = "UlozButton"
        Me.UlozButton.Size = New System.Drawing.Size(50, 22)
        Me.UlozButton.Text = "Ulož"
        '
        'ZrusButton
        '
        Me.ZrusButton.Image = Global.CCN.UI.WinForms.Resources.small_close
        Me.ZrusButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZrusButton.Name = "ZrusButton"
        Me.ZrusButton.Size = New System.Drawing.Size(50, 22)
        Me.ZrusButton.Text = "Zruš"
        '
        'Editor
        '
        Me.Editor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Editor.Location = New System.Drawing.Point(0, 25)
        Me.Editor.Name = "Editor"
        Me.Editor.Size = New System.Drawing.Size(792, 545)
        Me.Editor.TabIndex = 5
        '
        'NastaveniaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 570)
        Me.Controls.Add(Me.Editor)
        Me.Controls.Add(Me.ToolStrip)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NastaveniaForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nastavenia programu"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents UlozButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ZrusButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Editor As CCN.UI.WinForms.NastaveniaEditor
End Class
