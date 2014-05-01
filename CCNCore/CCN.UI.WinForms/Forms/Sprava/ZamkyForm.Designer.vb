Imports CCN.Model

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZamkyForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZamkyForm))
        Me.Grid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ZamkaZmazButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TypFilter = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ObnovButton = New System.Windows.Forms.ToolStripButton()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.Grid.AllowEditing = False
        Me.Grid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free
        Me.Grid.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.Grid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Grid.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.Grid.AutoSearchDelay = 5.0R
        Me.Grid.ColumnInfo = resources.GetString("Grid.ColumnInfo")
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.ExtendLastCol = True
        Me.Grid.Location = New System.Drawing.Point(0, 25)
        Me.Grid.Name = "Grid"
        Me.Grid.Rows.DefaultSize = 17
        Me.Grid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.Grid.Size = New System.Drawing.Size(732, 425)
        Me.Grid.TabIndex = 2
        Me.Grid.Tree.Column = 0
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZamkaZmazButton, Me.ToolStripSeparator1, Me.TypFilter, Me.ToolStripSeparator2, Me.ObnovButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(732, 25)
        Me.ToolStrip.TabIndex = 4
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'ZamkaZmazButton
        '
        Me.ZamkaZmazButton.Image = Global.CCN.UI.WinForms.Resources.small_lock_delete
        Me.ZamkaZmazButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZamkaZmazButton.Name = "ZamkaZmazButton"
        Me.ZamkaZmazButton.Size = New System.Drawing.Size(56, 22)
        Me.ZamkaZmazButton.Text = "Zmaž"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TypFilter
        '
        Me.TypFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypFilter.Name = "TypFilter"
        Me.TypFilter.Size = New System.Drawing.Size(200, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ObnovButton
        '
        Me.ObnovButton.Image = Global.CCN.UI.WinForms.Resources.small_database_refresh
        Me.ObnovButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ObnovButton.Name = "ObnovButton"
        Me.ObnovButton.Size = New System.Drawing.Size(63, 22)
        Me.ObnovButton.Text = "Obnov"
        '
        'ZamkyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(732, 450)
        Me.Controls.Add(Me.Grid)
        Me.Controls.Add(Me.ToolStrip)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ZamkyForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aktívne zámky"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Grid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ZamkaZmazButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TypFilter As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ObnovButton As System.Windows.Forms.ToolStripButton
End Class
