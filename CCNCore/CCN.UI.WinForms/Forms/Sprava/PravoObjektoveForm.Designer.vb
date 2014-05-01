<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PravoObjektoveForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PravoObjektoveForm))
        Me.Nazov = New System.Windows.Forms.TextBox()
        Me.NazovLabel = New System.Windows.Forms.Label()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.TypObjektuLabel = New System.Windows.Forms.Label()
        Me.TypObjektu = New System.Windows.Forms.ComboBox()
        Me.CinnostiVyber = New CCN.UI.WinForms.CinnostiVyber()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.UlozButton = New System.Windows.Forms.ToolStripButton()
        Me.ZrusButton = New System.Windows.Forms.ToolStripButton()
        Me.Panel.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Nazov
        '
        Me.Nazov.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Nazov.Location = New System.Drawing.Point(13, 51)
        Me.Nazov.Name = "Nazov"
        Me.Nazov.Size = New System.Drawing.Size(195, 20)
        Me.Nazov.TabIndex = 6
        '
        'NazovLabel
        '
        Me.NazovLabel.AutoSize = True
        Me.NazovLabel.Location = New System.Drawing.Point(10, 35)
        Me.NazovLabel.Name = "NazovLabel"
        Me.NazovLabel.Size = New System.Drawing.Size(38, 13)
        Me.NazovLabel.TabIndex = 7
        Me.NazovLabel.Text = "Názov"
        '
        'Panel
        '
        Me.Panel.Controls.Add(Me.TypObjektuLabel)
        Me.Panel.Controls.Add(Me.TypObjektu)
        Me.Panel.Controls.Add(Me.CinnostiVyber)
        Me.Panel.Controls.Add(Me.ToolStrip)
        Me.Panel.Controls.Add(Me.Nazov)
        Me.Panel.Controls.Add(Me.NazovLabel)
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel.Location = New System.Drawing.Point(0, 0)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(221, 236)
        Me.Panel.TabIndex = 10
        '
        'TypObjektuLabel
        '
        Me.TypObjektuLabel.AutoSize = True
        Me.TypObjektuLabel.Location = New System.Drawing.Point(12, 74)
        Me.TypObjektuLabel.Name = "TypObjektuLabel"
        Me.TypObjektuLabel.Size = New System.Drawing.Size(63, 13)
        Me.TypObjektuLabel.TabIndex = 10
        Me.TypObjektuLabel.Text = "Typ objektu"
        '
        'TypObjektu
        '
        Me.TypObjektu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypObjektu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypObjektu.FormattingEnabled = True
        Me.TypObjektu.Location = New System.Drawing.Point(15, 90)
        Me.TypObjektu.Name = "TypObjektu"
        Me.TypObjektu.Size = New System.Drawing.Size(192, 21)
        Me.TypObjektu.TabIndex = 9
        '
        'CinnostiVyber
        '
        Me.CinnostiVyber.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CinnostiVyber.Location = New System.Drawing.Point(15, 117)
        Me.CinnostiVyber.Name = "CinnostiVyber"
        Me.CinnostiVyber.Size = New System.Drawing.Size(192, 107)
        Me.CinnostiVyber.TabIndex = 8
        Me.CinnostiVyber.Vsetky = Nothing
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
        'PravoObjektoveForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 236)
        Me.Controls.Add(Me.Panel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PravoObjektoveForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nové právo"
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Nazov As System.Windows.Forms.TextBox
    Friend WithEvents NazovLabel As System.Windows.Forms.Label
    Friend WithEvents UlozButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ZrusButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents CinnostiVyber As CinnostiVyber
    Friend WithEvents TypObjektuLabel As System.Windows.Forms.Label
    Friend WithEvents TypObjektu As System.Windows.Forms.ComboBox
End Class
