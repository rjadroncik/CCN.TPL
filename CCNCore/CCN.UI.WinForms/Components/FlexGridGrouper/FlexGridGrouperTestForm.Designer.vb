<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlexGridGrouperTestForm
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
        Me.FlexGridGrouper1 = New FlexGridGrouper()
        Me.Grid = New C1.Win.C1FlexGrid.C1FlexGrid()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FlexGridGrouper1
        '
        Me.FlexGridGrouper1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlexGridGrouper1.Grid = Me.Grid
        Me.FlexGridGrouper1.Location = New System.Drawing.Point(0, 0)
        Me.FlexGridGrouper1.Name = "FlexGridGrouper1"
        Me.FlexGridGrouper1.Size = New System.Drawing.Size(549, 333)
        Me.FlexGridGrouper1.TabIndex = 0
        '
        'Grid
        '
        Me.Grid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
        Me.Grid.ColumnInfo = "6,1,0,0,0,95,Columns:0{Width:0;Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Name:""A"";Caption:""A"";Style:"""";}" & Global.Microsoft.VisualBasic.ChrW(9) & _
    "2{Name:""B"";Caption:""B"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "3{Name:""C"";Caption:""C"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "4{Name:""D"";Caption:""D"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "5{Nam" & _
    "e:""E"";Caption:""E"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.Location = New System.Drawing.Point(0, 0)
        Me.Grid.Name = "Grid"
        Me.Grid.Rows.DefaultSize = 19
        Me.Grid.Size = New System.Drawing.Size(549, 305)
        Me.Grid.TabIndex = 1
        Me.Grid.Tree.Column = 0
        '
        'FlexGridGrouperTestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 333)
        Me.Controls.Add(Me.FlexGridGrouper1)
        Me.Name = "FlexGridGrouperTestForm"
        Me.Text = "FlexGridGrouperTestForm"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FlexGridGrouper1 As FlexGridGrouper
    Friend WithEvents Grid As C1.Win.C1FlexGrid.C1FlexGrid

End Class
