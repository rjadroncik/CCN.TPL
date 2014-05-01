<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlexGridGrouper
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
        Me.Split = New System.Windows.Forms.SplitContainer()
        Me.GroupingView = New System.Windows.Forms.Panel()
        Me.Prepinace = New System.Windows.Forms.Panel()
        Me.ExpandAll = New System.Windows.Forms.Button()
        Me.CollapseAll = New System.Windows.Forms.Button()
        CType(Me.Split, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Split.Panel1.SuspendLayout()
        Me.Split.SuspendLayout()
        Me.Prepinace.SuspendLayout()
        Me.SuspendLayout()
        '
        'Split
        '
        Me.Split.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Split.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.Split.IsSplitterFixed = True
        Me.Split.Location = New System.Drawing.Point(0, 0)
        Me.Split.Name = "Split"
        Me.Split.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'Split.Panel1
        '
        Me.Split.Panel1.Controls.Add(Me.GroupingView)
        Me.Split.Panel1.Controls.Add(Me.Prepinace)
        Me.Split.Size = New System.Drawing.Size(557, 358)
        Me.Split.SplitterDistance = 30
        Me.Split.SplitterWidth = 1
        Me.Split.TabIndex = 2
        '
        'GroupingView
        '
        Me.GroupingView.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.GroupingView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GroupingView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupingView.Location = New System.Drawing.Point(14, 0)
        Me.GroupingView.Name = "GroupingView"
        Me.GroupingView.Size = New System.Drawing.Size(543, 30)
        Me.GroupingView.TabIndex = 2
        '
        'Prepinace
        '
        Me.Prepinace.Controls.Add(Me.ExpandAll)
        Me.Prepinace.Controls.Add(Me.CollapseAll)
        Me.Prepinace.Dock = System.Windows.Forms.DockStyle.Left
        Me.Prepinace.Location = New System.Drawing.Point(0, 0)
        Me.Prepinace.Name = "Prepinace"
        Me.Prepinace.Size = New System.Drawing.Size(14, 30)
        Me.Prepinace.TabIndex = 6
        '
        'ExpandAll
        '
        Me.ExpandAll.FlatAppearance.BorderSize = 0
        Me.ExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExpandAll.Location = New System.Drawing.Point(0, 0)
        Me.ExpandAll.Name = "ExpandAll"
        Me.ExpandAll.Size = New System.Drawing.Size(14, 14)
        Me.ExpandAll.TabIndex = 0
        Me.ExpandAll.UseVisualStyleBackColor = True
        '
        'CollapseAll
        '
        Me.CollapseAll.FlatAppearance.BorderSize = 0
        Me.CollapseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CollapseAll.Location = New System.Drawing.Point(0, 15)
        Me.CollapseAll.Name = "CollapseAll"
        Me.CollapseAll.Size = New System.Drawing.Size(14, 14)
        Me.CollapseAll.TabIndex = 1
        Me.CollapseAll.UseVisualStyleBackColor = True
        '
        'FlexGridGrouper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Split)
        Me.Name = "FlexGridGrouper"
        Me.Size = New System.Drawing.Size(557, 358)
        Me.Split.Panel1.ResumeLayout(False)
        CType(Me.Split, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Split.ResumeLayout(False)
        Me.Prepinace.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Split As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupingView As System.Windows.Forms.Panel
    Friend WithEvents Prepinace As System.Windows.Forms.Panel
    Friend WithEvents ExpandAll As System.Windows.Forms.Button
    Friend WithEvents CollapseAll As System.Windows.Forms.Button

End Class
