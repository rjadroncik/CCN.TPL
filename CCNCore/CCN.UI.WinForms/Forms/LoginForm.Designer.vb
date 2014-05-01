Imports CCN.Model

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class LoginForm(Of T As IPouzivatel)
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OK As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.UsernameTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.Copyright = New C1.Win.C1Input.C1Label()
        Me.Podnadpis = New C1.Win.C1Input.C1Label()
        Me.Nadpis = New C1.Win.C1Input.C1Label()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Copyright, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Podnadpis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nadpis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsernameLabel.Location = New System.Drawing.Point(181, 61)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(176, 23)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "&Login"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PasswordLabel.Location = New System.Drawing.Point(181, 103)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(176, 23)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Heslo"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsernameTextBox
        '
        Me.UsernameTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsernameTextBox.Location = New System.Drawing.Point(183, 81)
        Me.UsernameTextBox.Name = "UsernameTextBox"
        Me.UsernameTextBox.Size = New System.Drawing.Size(174, 20)
        Me.UsernameTextBox.TabIndex = 1
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PasswordTextBox.Location = New System.Drawing.Point(183, 123)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(174, 20)
        Me.PasswordTextBox.TabIndex = 3
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(303, 160)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(54, 23)
        Me.OK.TabIndex = 4
        Me.OK.Text = "&OK"
        '
        'Logo
        '
        Me.Logo.Dock = System.Windows.Forms.DockStyle.Left
        Me.Logo.Location = New System.Drawing.Point(0, 0)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(170, 195)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Logo.TabIndex = 0
        Me.Logo.TabStop = False
        '
        'Copyright
        '
        Me.Copyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Copyright.AutoSize = True
        Me.Copyright.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Copyright.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Copyright.Location = New System.Drawing.Point(181, 164)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(104, 15)
        Me.Copyright.TabIndex = 7
        Me.Copyright.Tag = Nothing
        Me.Copyright.Text = "© 2012 CCN s.r.o."
        Me.Copyright.TextDetached = True
        '
        'Podnadpis
        '
        Me.Podnadpis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Podnadpis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Podnadpis.Font = New System.Drawing.Font("Arial Black", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Podnadpis.Location = New System.Drawing.Point(193, 40)
        Me.Podnadpis.Name = "Podnadpis"
        Me.Podnadpis.Size = New System.Drawing.Size(185, 15)
        Me.Podnadpis.TabIndex = 12
        Me.Podnadpis.Tag = Nothing
        Me.Podnadpis.Text = "Podnadpis"
        Me.Podnadpis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Podnadpis.TextDetached = True
        '
        'Nadpis
        '
        Me.Nadpis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Nadpis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Nadpis.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Nadpis.Location = New System.Drawing.Point(165, 9)
        Me.Nadpis.Name = "Nadpis"
        Me.Nadpis.Size = New System.Drawing.Size(202, 33)
        Me.Nadpis.TabIndex = 11
        Me.Nadpis.Tag = Nothing
        Me.Nadpis.Text = "Napdis"
        Me.Nadpis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Nadpis.TextDetached = True
        '
        'LoginForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(369, 195)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.Copyright)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UsernameTextBox)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.Nadpis)
        Me.Controls.Add(Me.Podnadpis)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Prihlásenie"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Copyright, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Podnadpis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nadpis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Logo As System.Windows.Forms.PictureBox
    Public WithEvents Copyright As C1.Win.C1Input.C1Label
    Public WithEvents Podnadpis As C1.Win.C1Input.C1Label
    Public WithEvents Nadpis As C1.Win.C1Input.C1Label
    Public WithEvents PasswordTextBox As System.Windows.Forms.TextBox

End Class
