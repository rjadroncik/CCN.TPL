<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PouzivatelForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PouzivatelForm))
        Me.Login = New System.Windows.Forms.TextBox()
        Me.LoginLabel = New System.Windows.Forms.Label()
        Me.Meno = New System.Windows.Forms.TextBox()
        Me.OpisLabel = New System.Windows.Forms.Label()
        Me.Priezvisko = New System.Windows.Forms.TextBox()
        Me.EvidencneCisloLabel = New System.Windows.Forms.Label()
        Me.UlozButton = New System.Windows.Forms.ToolStripButton()
        Me.ZrusButton = New System.Windows.Forms.ToolStripButton()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.HesloLabel = New System.Windows.Forms.Label()
        Me.Heslo = New System.Windows.Forms.TextBox()
        Me.Aktivny = New System.Windows.Forms.CheckBox()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.Panel.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Login
        '
        Me.Login.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Login.Location = New System.Drawing.Point(13, 51)
        Me.Login.Name = "Login"
        Me.Login.Size = New System.Drawing.Size(195, 20)
        Me.Login.TabIndex = 6
        '
        'LoginLabel
        '
        Me.LoginLabel.AutoSize = True
        Me.LoginLabel.Location = New System.Drawing.Point(10, 35)
        Me.LoginLabel.Name = "LoginLabel"
        Me.LoginLabel.Size = New System.Drawing.Size(33, 13)
        Me.LoginLabel.TabIndex = 7
        Me.LoginLabel.Text = "Login"
        '
        'Meno
        '
        Me.Meno.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Meno.Location = New System.Drawing.Point(13, 90)
        Me.Meno.Name = "Meno"
        Me.Meno.Size = New System.Drawing.Size(195, 20)
        Me.Meno.TabIndex = 6
        '
        'OpisLabel
        '
        Me.OpisLabel.AutoSize = True
        Me.OpisLabel.Location = New System.Drawing.Point(10, 74)
        Me.OpisLabel.Name = "OpisLabel"
        Me.OpisLabel.Size = New System.Drawing.Size(34, 13)
        Me.OpisLabel.TabIndex = 7
        Me.OpisLabel.Text = "Meno"
        '
        'Priezvisko
        '
        Me.Priezvisko.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Priezvisko.Location = New System.Drawing.Point(13, 129)
        Me.Priezvisko.Name = "Priezvisko"
        Me.Priezvisko.Size = New System.Drawing.Size(195, 20)
        Me.Priezvisko.TabIndex = 6
        '
        'EvidencneCisloLabel
        '
        Me.EvidencneCisloLabel.AutoSize = True
        Me.EvidencneCisloLabel.Location = New System.Drawing.Point(10, 113)
        Me.EvidencneCisloLabel.Name = "EvidencneCisloLabel"
        Me.EvidencneCisloLabel.Size = New System.Drawing.Size(55, 13)
        Me.EvidencneCisloLabel.TabIndex = 7
        Me.EvidencneCisloLabel.Text = "Priezvisko"
        '
        'UlozButton
        '
        Me.UlozButton.Image = CType(resources.GetObject("UlozButton.Image"), System.Drawing.Image)
        Me.UlozButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UlozButton.Name = "UlozButton"
        Me.UlozButton.Size = New System.Drawing.Size(50, 22)
        Me.UlozButton.Text = "Ulož"
        '
        'ZrusButton
        '
        Me.ZrusButton.Image = CType(resources.GetObject("ZrusButton.Image"), System.Drawing.Image)
        Me.ZrusButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZrusButton.Name = "ZrusButton"
        Me.ZrusButton.Size = New System.Drawing.Size(50, 22)
        Me.ZrusButton.Text = "Zruš"
        '
        'Panel
        '
        Me.Panel.Controls.Add(Me.HesloLabel)
        Me.Panel.Controls.Add(Me.Heslo)
        Me.Panel.Controls.Add(Me.Aktivny)
        Me.Panel.Controls.Add(Me.ToolStrip)
        Me.Panel.Controls.Add(Me.Login)
        Me.Panel.Controls.Add(Me.EvidencneCisloLabel)
        Me.Panel.Controls.Add(Me.Meno)
        Me.Panel.Controls.Add(Me.OpisLabel)
        Me.Panel.Controls.Add(Me.Priezvisko)
        Me.Panel.Controls.Add(Me.LoginLabel)
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel.Location = New System.Drawing.Point(0, 0)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(221, 232)
        Me.Panel.TabIndex = 10
        '
        'HesloLabel
        '
        Me.HesloLabel.AutoSize = True
        Me.HesloLabel.Location = New System.Drawing.Point(10, 181)
        Me.HesloLabel.Name = "HesloLabel"
        Me.HesloLabel.Size = New System.Drawing.Size(34, 13)
        Me.HesloLabel.TabIndex = 10
        Me.HesloLabel.Text = "Heslo"
        '
        'Heslo
        '
        Me.Heslo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Heslo.Location = New System.Drawing.Point(13, 197)
        Me.Heslo.Name = "Heslo"
        Me.Heslo.Size = New System.Drawing.Size(195, 20)
        Me.Heslo.TabIndex = 9
        '
        'Aktivny
        '
        Me.Aktivny.AutoSize = True
        Me.Aktivny.Checked = True
        Me.Aktivny.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Aktivny.Location = New System.Drawing.Point(13, 157)
        Me.Aktivny.Name = "Aktivny"
        Me.Aktivny.Size = New System.Drawing.Size(63, 17)
        Me.Aktivny.TabIndex = 8
        Me.Aktivny.Text = "Aktívny"
        Me.Aktivny.UseVisualStyleBackColor = True
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UlozButton, Me.ZrusButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(221, 25)
        Me.ToolStrip.TabIndex = 5
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'PouzivatelForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 232)
        Me.Controls.Add(Me.Panel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PouzivatelForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nový používate¾"
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Login As System.Windows.Forms.TextBox
    Friend WithEvents LoginLabel As System.Windows.Forms.Label
    Friend WithEvents Meno As System.Windows.Forms.TextBox
    Friend WithEvents OpisLabel As System.Windows.Forms.Label
    Friend WithEvents Priezvisko As System.Windows.Forms.TextBox
    Friend WithEvents EvidencneCisloLabel As System.Windows.Forms.Label
    Friend WithEvents UlozButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ZrusButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents Aktivny As System.Windows.Forms.CheckBox
    Friend WithEvents HesloLabel As System.Windows.Forms.Label
    Friend WithEvents Heslo As System.Windows.Forms.TextBox
End Class
