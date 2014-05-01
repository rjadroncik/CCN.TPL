Imports CCN.Model

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoleForm
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
        Me.NovaButton = New System.Windows.Forms.ToolStripButton()
        Me.UpravitButton = New System.Windows.Forms.ToolStripButton()
        Me.ZmazatButton = New System.Windows.Forms.ToolStripButton()
        Me.GridRole = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.PouzivateliaVyber = New CCN.UI.WinForms.PouzivateliaVyber()
        Me.ToolStrip.SuspendLayout()
        CType(Me.GridRole, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NovaButton, Me.UpravitButton, Me.ZmazatButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(592, 25)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'NovaButton
        '
        Me.NovaButton.Image = Global.CCN.UI.WinForms.Resources.small_user_add
        Me.NovaButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NovaButton.Name = "NovaButton"
        Me.NovaButton.Size = New System.Drawing.Size(55, 22)
        Me.NovaButton.Text = "Nov·"
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
        'GridRole
        '
        Me.GridRole.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridRole.AllowEditing = False
        Me.GridRole.AllowFiltering = True
        Me.GridRole.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free
        Me.GridRole.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.GridRole.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.GridRole.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.GridRole.AutoSearchDelay = 5.0R
        Me.GridRole.ColumnInfo = "1,0,0,0,0,85,Columns:0{Width:163;AllowFiltering:None;Name:""Nazov"";Caption:""N·zov""" & _
    ";AllowEditing:False;Style:""DataType:System.String;TextAlign:LeftCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.GridRole.Dock = System.Windows.Forms.DockStyle.Left
        Me.GridRole.ExtendLastCol = True
        Me.GridRole.Location = New System.Drawing.Point(0, 25)
        Me.GridRole.Name = "GridRole"
        Me.GridRole.Rows.Count = 1
        Me.GridRole.Rows.DefaultSize = 17
        Me.GridRole.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridRole.Size = New System.Drawing.Size(184, 345)
        Me.GridRole.TabIndex = 7
        Me.GridRole.Tree.Column = 0
        '
        'PouzivateliaVyber
        '
        Me.PouzivateliaVyber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PouzivateliaVyber.Location = New System.Drawing.Point(184, 25)
        Me.PouzivateliaVyber.Name = "PouzivateliaVyber"
        Me.PouzivateliaVyber.Size = New System.Drawing.Size(408, 345)
        Me.PouzivateliaVyber.TabIndex = 8
        Me.PouzivateliaVyber.Vsetky = Nothing
        '
        'RoleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 370)
        Me.Controls.Add(Me.PouzivateliaVyber)
        Me.Controls.Add(Me.GridRole)
        Me.Controls.Add(Me.ToolStrip)
        Me.MinimizeBox = False
        Me.Name = "RoleForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Role"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        CType(Me.GridRole, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents NovaButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UpravitButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents GridRole As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents PouzivateliaVyber As PouzivateliaVyber
    Friend WithEvents ZmazatButton As System.Windows.Forms.ToolStripButton
End Class
