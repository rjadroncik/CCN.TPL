<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SystemStatus
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
        Me.components = New System.ComponentModel.Container()
        Me.Status = New System.Windows.Forms.StatusStrip()
        Me.Entita = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Pouzivatel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SpringLeft = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Server = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DB = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Verzia = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SpringRight = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Datum = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Cas = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.SuperTooltip = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Status.SuspendLayout()
        Me.SuspendLayout()
        '
        'Status
        '
        Me.Status.AllowMerge = False
        Me.Status.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Entita, Me.Pouzivatel, Me.SpringLeft, Me.Server, Me.DB, Me.Verzia, Me.SpringRight, Me.Datum, Me.Cas})
        Me.Status.Location = New System.Drawing.Point(0, 0)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(700, 21)
        Me.Status.TabIndex = 3
        '
        'Entita
        '
        Me.Entita.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Entita.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Entita.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.Entita.Name = "Entita"
        Me.Entita.Size = New System.Drawing.Size(32, 16)
        Me.Entita.Text = "PC: "
        '
        'Pouzivatel
        '
        Me.Pouzivatel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Pouzivatel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Pouzivatel.Margin = New System.Windows.Forms.Padding(3, 3, 2, 2)
        Me.Pouzivatel.Name = "Pouzivatel"
        Me.Pouzivatel.Size = New System.Drawing.Size(72, 16)
        Me.Pouzivatel.Text = "Používateľ: "
        '
        'SpringLeft
        '
        Me.SpringLeft.Name = "SpringLeft"
        Me.SpringLeft.Size = New System.Drawing.Size(165, 16)
        Me.SpringLeft.Spring = True
        '
        'Server
        '
        Me.Server.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Server.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Server.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.Server.Name = "Server"
        Me.Server.Size = New System.Drawing.Size(49, 16)
        Me.Server.Text = "Server: "
        '
        'DB
        '
        Me.DB.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.DB.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.DB.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.DB.Name = "DB"
        Me.DB.Size = New System.Drawing.Size(32, 16)
        Me.DB.Text = "DB: "
        '
        'Verzia
        '
        Me.Verzia.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Verzia.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Verzia.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.Verzia.Name = "Verzia"
        Me.Verzia.Size = New System.Drawing.Size(48, 16)
        Me.Verzia.Text = "Verzia: "
        Me.Verzia.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'SpringRight
        '
        Me.SpringRight.Name = "SpringRight"
        Me.SpringRight.Size = New System.Drawing.Size(165, 16)
        Me.SpringRight.Spring = True
        '
        'Datum
        '
        Me.Datum.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Datum.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Datum.Margin = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.Datum.Name = "Datum"
        Me.Datum.Size = New System.Drawing.Size(53, 16)
        Me.Datum.Text = "1.1.2012"
        '
        'Cas
        '
        Me.Cas.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Cas.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Cas.Margin = New System.Windows.Forms.Padding(2, 3, 3, 2)
        Me.Cas.Name = "Cas"
        Me.Cas.Size = New System.Drawing.Size(38, 16)
        Me.Cas.Text = "10:00"
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 30000
        '
        'SuperTooltip
        '
        Me.SuperTooltip.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.SuperTooltip.IsBalloon = True
        '
        'SystemStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Status)
        Me.Name = "SystemStatus"
        Me.Size = New System.Drawing.Size(700, 21)
        Me.Status.ResumeLayout(False)
        Me.Status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Status As System.Windows.Forms.StatusStrip
    Friend WithEvents Pouzivatel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Server As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Entita As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Verzia As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Datum As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Cas As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SpringLeft As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents SpringRight As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SuperTooltip As C1.Win.C1SuperTooltip.C1SuperTooltip

End Class
