<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Loading
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        Me.Nadpis = New C1.Win.C1Input.C1Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.Nadpis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Progress
        '
        Me.Progress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Progress.Location = New System.Drawing.Point(4, 26)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(292, 25)
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 0
        '
        'Nadpis
        '
        Me.Nadpis.BackColor = System.Drawing.Color.Gray
        Me.Nadpis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Nadpis.Dock = System.Windows.Forms.DockStyle.Top
        Me.Nadpis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Nadpis.ForeColor = System.Drawing.Color.White
        Me.Nadpis.Location = New System.Drawing.Point(4, 4)
        Me.Nadpis.Name = "Nadpis"
        Me.Nadpis.Size = New System.Drawing.Size(292, 18)
        Me.Nadpis.TabIndex = 21
        Me.Nadpis.Tag = Nothing
        Me.Nadpis.Text = "Aktualizujem..."
        Me.Nadpis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Nadpis.TextDetached = True
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(4, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(292, 4)
        Me.Panel1.TabIndex = 22
        '
        'Loading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Nadpis)
        Me.Name = "Loading"
        Me.Padding = New System.Windows.Forms.Padding(4)
        Me.Size = New System.Drawing.Size(300, 55)
        CType(Me.Nadpis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Progress As System.Windows.Forms.ProgressBar
    Public WithEvents Nadpis As C1.Win.C1Input.C1Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
