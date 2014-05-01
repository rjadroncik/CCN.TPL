Imports CCN.Model

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PravaObjektoveRoleForm
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
        Me.RoleVyber = New CCN.UI.WinForms.RoleVyber()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.NoveButton = New System.Windows.Forms.ToolStripButton()
        Me.UpravitButton = New System.Windows.Forms.ToolStripButton()
        Me.ZmazatButton = New System.Windows.Forms.ToolStripButton()
        Me.PravoVyber = New CCN.UI.WinForms.PravoObjektoveVyber()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'RoleVyber
        '
        Me.RoleVyber.Dock = System.Windows.Forms.DockStyle.Right
        Me.RoleVyber.Location = New System.Drawing.Point(327, 25)
        Me.RoleVyber.Name = "RoleVyber"
        Me.RoleVyber.Size = New System.Drawing.Size(365, 445)
        Me.RoleVyber.TabIndex = 8
        Me.RoleVyber.Vsetky = Nothing
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NoveButton, Me.UpravitButton, Me.ZmazatButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(692, 25)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'NoveButton
        '
        Me.NoveButton.Image = Global.CCN.UI.WinForms.Resources.small_user_add
        Me.NoveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NoveButton.Name = "NoveButton"
        Me.NoveButton.Size = New System.Drawing.Size(55, 22)
        Me.NoveButton.Text = "NovÈ"
        '
        'UpravitButton
        '
        Me.UpravitButton.Image = Global.CCN.UI.WinForms.Resources.small_user_edit
        Me.UpravitButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UpravitButton.Name = "UpravitButton"
        Me.UpravitButton.Size = New System.Drawing.Size(66, 22)
        Me.UpravitButton.Text = "Upraviù"
        '
        'ZmazatButton
        '
        Me.ZmazatButton.Image = Global.CCN.UI.WinForms.Resources.small_user_delete
        Me.ZmazatButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZmazatButton.Name = "ZmazatButton"
        Me.ZmazatButton.Size = New System.Drawing.Size(67, 22)
        Me.ZmazatButton.Text = "Zmazaù"
        '
        'PravoVyber
        '
        Me.PravoVyber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PravoVyber.Location = New System.Drawing.Point(0, 25)
        Me.PravoVyber.Name = "PravoVyber"
        Me.PravoVyber.Size = New System.Drawing.Size(327, 445)
        Me.PravoVyber.TabIndex = 9
        Me.PravoVyber.Vsetky = Nothing
        '
        'PravaObjektoveRoleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 470)
        Me.Controls.Add(Me.PravoVyber)
        Me.Controls.Add(Me.RoleVyber)
        Me.Controls.Add(Me.ToolStrip)
        Me.MinimizeBox = False
        Me.Name = "PravaObjektoveRoleForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pr·va/Role"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RoleVyber As RoleVyber
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents NoveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UpravitButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ZmazatButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PravoVyber As CCN.UI.WinForms.PravoObjektoveVyber
End Class
