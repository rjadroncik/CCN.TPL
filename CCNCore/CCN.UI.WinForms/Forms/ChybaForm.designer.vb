<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChybaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChybaForm))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Sprava = New System.Windows.Forms.Label()
        Me.Ikona = New System.Windows.Forms.PictureBox()
        Me.BottomPanel = New System.Windows.Forms.Panel()
        Me.Ok = New System.Windows.Forms.Button()
        Me.Upozornit = New System.Windows.Forms.CheckBox()
        Me.Groupbox = New CCN.UI.WinForms.ExpandableGroupbox()
        Me.Grid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel1.SuspendLayout()
        CType(Me.Ikona, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BottomPanel.SuspendLayout()
        Me.Groupbox.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Sprava)
        Me.Panel1.Controls.Add(Me.Ikona)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(492, 72)
        Me.Panel1.TabIndex = 9
        '
        'Sprava
        '
        Me.Sprava.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Sprava.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Sprava.Location = New System.Drawing.Point(66, 12)
        Me.Sprava.Name = "Sprava"
        Me.Sprava.Size = New System.Drawing.Size(414, 48)
        Me.Sprava.TabIndex = 1
        Me.Sprava.Text = "[Sprava pre pouzivatela]"
        Me.Sprava.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Sprava.UseMnemonic = False
        '
        'Ikona
        '
        Me.Ikona.Image = Global.CCN.UI.WinForms.Resources.large_warning
        Me.Ikona.Location = New System.Drawing.Point(12, 12)
        Me.Ikona.Name = "Ikona"
        Me.Ikona.Size = New System.Drawing.Size(48, 48)
        Me.Ikona.TabIndex = 0
        Me.Ikona.TabStop = False
        '
        'BottomPanel
        '
        Me.BottomPanel.Controls.Add(Me.Ok)
        Me.BottomPanel.Controls.Add(Me.Upozornit)
        Me.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottomPanel.Location = New System.Drawing.Point(0, 336)
        Me.BottomPanel.Name = "BottomPanel"
        Me.BottomPanel.Size = New System.Drawing.Size(492, 34)
        Me.BottomPanel.TabIndex = 10
        '
        'Ok
        '
        Me.Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Ok.Location = New System.Drawing.Point(405, 0)
        Me.Ok.Name = "Ok"
        Me.Ok.Size = New System.Drawing.Size(75, 23)
        Me.Ok.TabIndex = 1
        Me.Ok.Text = "Ok"
        Me.Ok.UseVisualStyleBackColor = True
        '
        'Upozornit
        '
        Me.Upozornit.AutoSize = True
        Me.Upozornit.Checked = True
        Me.Upozornit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Upozornit.Location = New System.Drawing.Point(12, 7)
        Me.Upozornit.Name = "Upozornit"
        Me.Upozornit.Size = New System.Drawing.Size(164, 17)
        Me.Upozornit.TabIndex = 0
        Me.Upozornit.Text = "Upozorniť technickú podporu"
        Me.Upozornit.UseVisualStyleBackColor = True
        Me.Upozornit.Visible = False
        '
        'Groupbox
        '
        Me.Groupbox.BorderColor = System.Drawing.Color.Transparent
        Me.Groupbox.Caption = "Informácie pre technickú podporu"
        Me.Groupbox.Controls.Add(Me.Grid)
        Me.Groupbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Groupbox.ExpandedSize = New System.Drawing.Size(492, 264)
        Me.Groupbox.HeaderClickExpand = True
        Me.Groupbox.Location = New System.Drawing.Point(0, 72)
        Me.Groupbox.Name = "Groupbox"
        Me.Groupbox.Size = New System.Drawing.Size(492, 264)
        Me.Groupbox.TabIndex = 11
        '
        'Grid
        '
        Me.Grid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.Grid.AllowEditing = False
        Me.Grid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free
        Me.Grid.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.Grid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.AutoClipboard = True
        Me.Grid.ColumnInfo = resources.GetString("Grid.ColumnInfo")
        Me.Grid.ExtendLastCol = True
        Me.Grid.Location = New System.Drawing.Point(12, 21)
        Me.Grid.Name = "Grid"
        Me.Grid.Rows.DefaultSize = 17
        Me.Grid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.Grid.Size = New System.Drawing.Size(468, 237)
        Me.Grid.TabIndex = 3
        Me.Grid.Tree.Column = 0
        '
        'ChybaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 370)
        Me.Controls.Add(Me.Groupbox)
        Me.Controls.Add(Me.BottomPanel)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ChybaForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[Aplikaica] - [Nadpis]"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        CType(Me.Ikona, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BottomPanel.ResumeLayout(False)
        Me.BottomPanel.PerformLayout()
        Me.Groupbox.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Ikona As System.Windows.Forms.PictureBox
    Friend WithEvents Sprava As System.Windows.Forms.Label
    Friend WithEvents BottomPanel As System.Windows.Forms.Panel
    Friend WithEvents Ok As System.Windows.Forms.Button
    Friend WithEvents Groupbox As Global.CCN.UI.WinForms.ExpandableGroupbox
    Friend WithEvents Grid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Upozornit As System.Windows.Forms.CheckBox
End Class
