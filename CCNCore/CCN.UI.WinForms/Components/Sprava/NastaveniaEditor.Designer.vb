﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NastaveniaEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NastaveniaEditor))
        Me.Grid = New C1.Win.C1FlexGrid.C1FlexGrid()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.Grid.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free
        Me.Grid.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None
        Me.Grid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.Grid.ColumnInfo = resources.GetString("Grid.ColumnInfo")
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.ExtendLastCol = True
        Me.Grid.Location = New System.Drawing.Point(0, 0)
        Me.Grid.Name = "Grid"
        Me.Grid.Rows.Count = 1
        Me.Grid.Rows.DefaultSize = 17
        Me.Grid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.Grid.Size = New System.Drawing.Size(600, 400)
        Me.Grid.TabIndex = 3
        Me.Grid.Tree.Column = 0
        Me.Grid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.System
        '
        'NastaveniaEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Grid)
        Me.Name = "NastaveniaEditor"
        Me.Size = New System.Drawing.Size(600, 400)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grid As C1.Win.C1FlexGrid.C1FlexGrid

End Class
