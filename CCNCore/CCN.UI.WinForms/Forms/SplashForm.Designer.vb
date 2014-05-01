<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashForm
    Inherits Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Me.Copyright = New C1.Win.C1Input.C1Label()
        Me.Podnadpis = New C1.Win.C1Input.C1Label()
        Me.Nadpis = New C1.Win.C1Input.C1Label()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        Me.Verzia = New C1.Win.C1Input.C1Label()
        Me.Okraj = New System.Windows.Forms.Panel()
        Me.Akcia = New C1.Win.C1Input.C1Label()
        CType(Me.Copyright, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Podnadpis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nadpis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Verzia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Okraj.SuspendLayout()
        CType(Me.Akcia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Copyright
        '
        Me.Copyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Copyright.AutoSize = True
        Me.Copyright.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Copyright.Location = New System.Drawing.Point(12, 109)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(104, 15)
        Me.Copyright.TabIndex = 11
        Me.Copyright.Tag = Nothing
        Me.Copyright.Text = "© 2012 CCN s.r.o."
        Me.Copyright.TextDetached = True
        '
        'Podnadpis
        '
        Me.Podnadpis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Podnadpis.Font = New System.Drawing.Font("Arial Black", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Podnadpis.Location = New System.Drawing.Point(57, 40)
        Me.Podnadpis.Name = "Podnadpis"
        Me.Podnadpis.Size = New System.Drawing.Size(185, 15)
        Me.Podnadpis.TabIndex = 10
        Me.Podnadpis.Tag = Nothing
        Me.Podnadpis.Text = "Podnadpis"
        Me.Podnadpis.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Podnadpis.TextDetached = True
        '
        'Nadpis
        '
        Me.Nadpis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Nadpis.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Nadpis.Location = New System.Drawing.Point(29, 9)
        Me.Nadpis.Name = "Nadpis"
        Me.Nadpis.Size = New System.Drawing.Size(202, 33)
        Me.Nadpis.TabIndex = 9
        Me.Nadpis.Tag = Nothing
        Me.Nadpis.Text = "Nadpis"
        Me.Nadpis.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Nadpis.TextDetached = True
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Location = New System.Drawing.Point(12, 66)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(231, 17)
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Progress.TabIndex = 14
        '
        'Verzia
        '
        Me.Verzia.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Verzia.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Verzia.Location = New System.Drawing.Point(122, 109)
        Me.Verzia.Name = "Verzia"
        Me.Verzia.Size = New System.Drawing.Size(120, 16)
        Me.Verzia.TabIndex = 13
        Me.Verzia.Tag = Nothing
        Me.Verzia.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Verzia.TextDetached = True
        '
        'Okraj
        '
        Me.Okraj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Okraj.Controls.Add(Me.Akcia)
        Me.Okraj.Controls.Add(Me.Copyright)
        Me.Okraj.Controls.Add(Me.Podnadpis)
        Me.Okraj.Controls.Add(Me.Nadpis)
        Me.Okraj.Controls.Add(Me.Progress)
        Me.Okraj.Controls.Add(Me.Verzia)
        Me.Okraj.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Okraj.Location = New System.Drawing.Point(0, 0)
        Me.Okraj.Name = "Okraj"
        Me.Okraj.Size = New System.Drawing.Size(257, 135)
        Me.Okraj.TabIndex = 15
        '
        'Akcia
        '
        Me.Akcia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Akcia.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Akcia.Location = New System.Drawing.Point(29, 86)
        Me.Akcia.Name = "Akcia"
        Me.Akcia.Size = New System.Drawing.Size(214, 15)
        Me.Akcia.TabIndex = 15
        Me.Akcia.Tag = Nothing
        Me.Akcia.TextDetached = True
        '
        'SplashForm
        '
        Me.ClientSize = New System.Drawing.Size(257, 135)
        Me.ControlBox = False
        Me.Controls.Add(Me.Okraj)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplashForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dali 8"
        CType(Me.Copyright, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Podnadpis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nadpis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Verzia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Okraj.ResumeLayout(False)
        Me.Okraj.PerformLayout()
        CType(Me.Akcia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Copyright As C1.Win.C1Input.C1Label
    Friend WithEvents Verzia As C1.Win.C1Input.C1Label
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents Okraj As System.Windows.Forms.Panel
    Public WithEvents Podnadpis As C1.Win.C1Input.C1Label
    Public WithEvents Nadpis As C1.Win.C1Input.C1Label
    Private WithEvents Akcia As C1.Win.C1Input.C1Label
End Class
