<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObjektVyberList(Of T As Class)
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Grid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GridZvoleny = New C1.Win.C1FlexGrid.C1FlexGrid()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridZvoleny, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.Grid.AllowEditing = False
        Me.Grid.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.Grid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.Grid.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.Grid.AutoSearchDelay = 5.0R
        Me.Grid.ColumnInfo = "1,0,0,0,0,85,Columns:0{Width:129;AllowEditing:False;Style:""TextAlign:GeneralCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.ExtendLastCol = True
        Me.Grid.Location = New System.Drawing.Point(0, 21)
        Me.Grid.Margin = New System.Windows.Forms.Padding(0)
        Me.Grid.Name = "Grid"
        Me.Grid.Rows.Count = 1
        Me.Grid.Rows.DefaultSize = 17
        Me.Grid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.Grid.Size = New System.Drawing.Size(200, 179)
        Me.Grid.TabIndex = 11
        Me.Grid.Tree.Column = 0
        '
        'GridZvoleny
        '
        Me.GridZvoleny.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridZvoleny.AllowEditing = False
        Me.GridZvoleny.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.GridZvoleny.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.GridZvoleny.AutoSearchDelay = 5.0R
        Me.GridZvoleny.ColumnInfo = "1,0,0,0,0,85,Columns:0{Width:129;AllowEditing:False;Style:""TextAlign:GeneralCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.GridZvoleny.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridZvoleny.ExtendLastCol = True
        Me.GridZvoleny.Location = New System.Drawing.Point(0, 0)
        Me.GridZvoleny.Margin = New System.Windows.Forms.Padding(0)
        Me.GridZvoleny.Name = "GridZvoleny"
        Me.GridZvoleny.Rows.Count = 0
        Me.GridZvoleny.Rows.DefaultSize = 17
        Me.GridZvoleny.Rows.Fixed = 0
        Me.GridZvoleny.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridZvoleny.Size = New System.Drawing.Size(200, 21)
        Me.GridZvoleny.TabIndex = 12
        Me.GridZvoleny.Tree.Column = 0
        '
        'ObjektVyberList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Grid)
        Me.Controls.Add(Me.GridZvoleny)
        Me.Name = "ObjektVyberList"
        Me.Size = New System.Drawing.Size(200, 200)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridZvoleny, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GridZvoleny As C1.Win.C1FlexGrid.C1FlexGrid

End Class
