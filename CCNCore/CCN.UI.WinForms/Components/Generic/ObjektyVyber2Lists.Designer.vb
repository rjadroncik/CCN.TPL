<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObjektyVyber2Lists(Of T As Class)
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MoveRight = New C1.Win.C1Input.C1Button()
        Me.MoveLeft = New C1.Win.C1Input.C1Button()
        Me.GridZvolene = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GridVsetko = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.GridZvolene, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridVsetko, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MoveRight, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MoveLeft, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GridZvolene, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GridVsetko, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(400, 300)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'MoveRight
        '
        Me.MoveRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.MoveRight.Image = Global.CCN.UI.WinForms.Resources.small_arrow_right
        Me.MoveRight.Location = New System.Drawing.Point(188, 153)
        Me.MoveRight.Name = "MoveRight"
        Me.MoveRight.Size = New System.Drawing.Size(24, 75)
        Me.MoveRight.TabIndex = 16
        Me.MoveRight.UseVisualStyleBackColor = True
        '
        'MoveLeft
        '
        Me.MoveLeft.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MoveLeft.Image = Global.CCN.UI.WinForms.Resources.small_arrow_left
        Me.MoveLeft.Location = New System.Drawing.Point(188, 72)
        Me.MoveLeft.Name = "MoveLeft"
        Me.MoveLeft.Size = New System.Drawing.Size(24, 75)
        Me.MoveLeft.TabIndex = 11
        Me.MoveLeft.UseVisualStyleBackColor = True
        '
        'GridZvolene
        '
        Me.GridZvolene.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridZvolene.AllowEditing = False
        Me.GridZvolene.AllowFiltering = True
        Me.GridZvolene.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.GridZvolene.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.GridZvolene.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.GridZvolene.AutoSearchDelay = 5.0R
        Me.GridZvolene.ColumnInfo = "1,0,0,0,0,85,Columns:0{Width:129;Caption:""Zvolené"";AllowEditing:False;Style:""Text" & _
    "Align:GeneralCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.GridZvolene.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridZvolene.ExtendLastCol = True
        Me.GridZvolene.Location = New System.Drawing.Point(0, 0)
        Me.GridZvolene.Margin = New System.Windows.Forms.Padding(0)
        Me.GridZvolene.Name = "GridZvolene"
        Me.GridZvolene.Rows.Count = 1
        Me.GridZvolene.Rows.DefaultSize = 17
        Me.TableLayoutPanel1.SetRowSpan(Me.GridZvolene, 2)
        Me.GridZvolene.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridZvolene.Size = New System.Drawing.Size(185, 300)
        Me.GridZvolene.TabIndex = 10
        Me.GridZvolene.Tree.Column = 0
        '
        'GridVsetko
        '
        Me.GridVsetko.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridVsetko.AllowEditing = False
        Me.GridVsetko.AllowFiltering = True
        Me.GridVsetko.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.GridVsetko.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.GridVsetko.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromCursor
        Me.GridVsetko.AutoSearchDelay = 5.0R
        Me.GridVsetko.ColumnInfo = "1,0,0,0,0,85,Columns:0{Width:129;Caption:""Všetko"";AllowEditing:False;Style:""TextA" & _
    "lign:GeneralCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.GridVsetko.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridVsetko.ExtendLastCol = True
        Me.GridVsetko.Location = New System.Drawing.Point(215, 0)
        Me.GridVsetko.Margin = New System.Windows.Forms.Padding(0)
        Me.GridVsetko.Name = "GridVsetko"
        Me.GridVsetko.Rows.Count = 1
        Me.GridVsetko.Rows.DefaultSize = 17
        Me.TableLayoutPanel1.SetRowSpan(Me.GridVsetko, 2)
        Me.GridVsetko.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridVsetko.Size = New System.Drawing.Size(185, 300)
        Me.GridVsetko.TabIndex = 17
        Me.GridVsetko.Tree.Column = 0
        '
        'ObjektyVyber2Lists
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ObjektyVyber2Lists"
        Me.Size = New System.Drawing.Size(400, 300)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.GridZvolene, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridVsetko, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MoveRight As C1.Win.C1Input.C1Button
    Friend WithEvents MoveLeft As C1.Win.C1Input.C1Button
    Friend WithEvents GridZvolene As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GridVsetko As C1.Win.C1FlexGrid.C1FlexGrid

End Class
