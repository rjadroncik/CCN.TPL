Imports CCN.Model

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PouzivateliaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PouzivateliaForm))
        Me.Grid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.NovyButton = New System.Windows.Forms.ToolStripButton()
        Me.UpravitButton = New System.Windows.Forms.ToolStripButton()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.Grid.AllowEditing = False
        Me.Grid.AllowFiltering = True
        Me.Grid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free
        Me.Grid.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.Grid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Grid.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.Grid.AutoSearchDelay = 5.0R
        Me.Grid.ColumnInfo = resources.GetString("Grid.ColumnInfo")
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.Location = New System.Drawing.Point(0, 25)
        Me.Grid.Name = "Grid"
        Me.Grid.Rows.Count = 1
        Me.Grid.Rows.DefaultSize = 17
        Me.Grid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.Grid.Size = New System.Drawing.Size(442, 345)
        Me.Grid.TabIndex = 2
        Me.Grid.Tree.Column = 0
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NovyButton, Me.UpravitButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.Size = New System.Drawing.Size(442, 25)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'NovyButton
        '
        Me.NovyButton.Image = Global.CCN.UI.WinForms.Resources.small_user_add
        Me.NovyButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NovyButton.Name = "NovyButton"
        Me.NovyButton.Size = New System.Drawing.Size(55, 22)
        Me.NovyButton.Text = "Nov˝"
        '
        'UpravitButton
        '
        Me.UpravitButton.Image = Global.CCN.UI.WinForms.Resources.small_user_edit
        Me.UpravitButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UpravitButton.Name = "UpravitButton"
        Me.UpravitButton.Size = New System.Drawing.Size(66, 22)
        Me.UpravitButton.Text = "Upraviù"
        '
        'PouzivateliaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 370)
        Me.Controls.Add(Me.Grid)
        Me.Controls.Add(Me.ToolStrip)
        Me.MinimizeBox = False
        Me.Name = "PouzivateliaForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PouûÌvatelia"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Grid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents NovyButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UpravitButton As System.Windows.Forms.ToolStripButton
End Class
