Imports C1.Win.C1Preview

Public Class CCNPrintPreview
    Inherits C1PrintPreviewDialog

    Private components As System.ComponentModel.IContainer
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        StatusBarVisible = False
        NavigationPanelVisible = False

        PreviewPane.ShowRulers = ShowRulersFlags.None

        AddHandler PrintPreviewControl.PreviewPane.MouseDown, AddressOf PreviewPane_OnMouseDown
        PrintPreviewControl.PreviewPane.ContextMenuStrip = Nothing
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CCNPrintPreview))
        CType(Me.PrintPreviewControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewControl.PreviewPane, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CCNPrintPreview
        '
        Me.ClientSize = New System.Drawing.Size(716, 543)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CCNPrintPreview"
        '
        '
        '
        Me.PrintPreviewControl.AvailablePreviewActions = CType(((((((((((((((((C1.Win.C1Preview.C1PreviewActionFlags.FileOpen Or C1.Win.C1Preview.C1PreviewActionFlags.FileSave) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.PageSetup) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.Print) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.Reflow) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.GoFirst) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.GoPrev) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.GoNext) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.GoLast) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.GoPage) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.HistoryNext) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.HistoryPrev) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.ZoomIn) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.ZoomOut) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.ZoomFactor) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.ZoomInTool) _
                    Or C1.Win.C1Preview.C1PreviewActionFlags.ZoomOutTool), C1.Win.C1Preview.C1PreviewActionFlags)
        Me.PrintPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintPreviewControl.Location = New System.Drawing.Point(0, 0)
        Me.PrintPreviewControl.Name = "m_c1PrintPreviewControl"
        Me.PrintPreviewControl.NavigationPanelVisible = False
        '
        '
        '
        Me.PrintPreviewControl.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintPreviewControl.PreviewOutlineView.LineColor = System.Drawing.Color.Empty
        Me.PrintPreviewControl.PreviewOutlineView.Location = New System.Drawing.Point(0, 0)
        Me.PrintPreviewControl.PreviewOutlineView.Name = "outline"
        Me.PrintPreviewControl.PreviewOutlineView.Size = New System.Drawing.Size(165, 470)
        Me.PrintPreviewControl.PreviewOutlineView.TabIndex = 0
        '
        '
        '
        Me.PrintPreviewControl.PreviewPane.Cursor = System.Windows.Forms.Cursors.Default
        Me.PrintPreviewControl.PreviewPane.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintPreviewControl.PreviewPane.IntegrateExternalTools = True
        Me.PrintPreviewControl.PreviewPane.Location = New System.Drawing.Point(3, 0)
        Me.PrintPreviewControl.PreviewPane.Name = "PreviewPane"
        Me.PrintPreviewControl.PreviewPane.ShowRulers = C1.Win.C1Preview.ShowRulersFlags.None
        Me.PrintPreviewControl.PreviewPane.Size = New System.Drawing.Size(713, 518)
        Me.PrintPreviewControl.PreviewPane.TabIndex = 0
        '
        '
        '
        Me.PrintPreviewControl.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.PrintPreviewControl.PreviewTextSearchPanel.Location = New System.Drawing.Point(530, 0)
        Me.PrintPreviewControl.PreviewTextSearchPanel.MinimumSize = New System.Drawing.Size(200, 240)
        Me.PrintPreviewControl.PreviewTextSearchPanel.Name = "textSearchPanel"
        Me.PrintPreviewControl.PreviewTextSearchPanel.Size = New System.Drawing.Size(200, 453)
        Me.PrintPreviewControl.PreviewTextSearchPanel.TabIndex = 0
        Me.PrintPreviewControl.PreviewTextSearchPanel.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintPreviewControl.PreviewThumbnailView.Location = New System.Drawing.Point(0, 0)
        Me.PrintPreviewControl.PreviewThumbnailView.Name = "thumbnails"
        Me.PrintPreviewControl.PreviewThumbnailView.Size = New System.Drawing.Size(165, 470)
        Me.PrintPreviewControl.PreviewThumbnailView.TabIndex = 0
        Me.PrintPreviewControl.PreviewThumbnailView.UseImageAsThumbnail = False
        Me.PrintPreviewControl.Size = New System.Drawing.Size(716, 543)
        Me.PrintPreviewControl.StatusBarVisible = False
        Me.PrintPreviewControl.TabIndex = 0
        Me.PrintPreviewControl.Text = "c1PrintPreviewControl1"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.File.Open.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.File.Open.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.File.Open.Name = "btnFileOpen"
        Me.PrintPreviewControl.ToolBars.File.Open.Size = New System.Drawing.Size(32, 22)
        Me.PrintPreviewControl.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen"
        Me.PrintPreviewControl.ToolBars.File.Open.ToolTipText = "Open File"
        Me.PrintPreviewControl.ToolBars.File.Open.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.File.Reflow.Enabled = False
        Me.PrintPreviewControl.ToolBars.File.Reflow.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.File.Reflow.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.File.Reflow.Name = "btnReflow"
        Me.PrintPreviewControl.ToolBars.File.Reflow.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow"
        Me.PrintPreviewControl.ToolBars.File.Reflow.ToolTipText = "Reflow"
        Me.PrintPreviewControl.ToolBars.File.Reflow.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.File.Save.Enabled = False
        Me.PrintPreviewControl.ToolBars.File.Save.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.File.Save.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.File.Save.Name = "btnFileSave"
        Me.PrintPreviewControl.ToolBars.File.Save.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave"
        Me.PrintPreviewControl.ToolBars.File.Save.ToolTipText = "Save File"
        Me.PrintPreviewControl.ToolBars.File.Save.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Navigation.GoFirst.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.Name = "btnGoFirst"
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst"
        Me.PrintPreviewControl.ToolBars.Navigation.GoFirst.ToolTipText = "Prvá strana"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Navigation.GoLast.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.Name = "btnGoLast"
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast"
        Me.PrintPreviewControl.ToolBars.Navigation.GoLast.ToolTipText = "Posledná strana"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Navigation.GoNext.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.Name = "btnGoNext"
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext"
        Me.PrintPreviewControl.ToolBars.Navigation.GoNext.ToolTipText = "Predošlá strana"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Navigation.GoPrev.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.Name = "btnGoPrev"
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev"
        Me.PrintPreviewControl.ToolBars.Navigation.GoPrev.ToolTipText = "Nasledujúca strana"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext"
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Size = New System.Drawing.Size(32, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext"
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View"
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev"
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Size = New System.Drawing.Size(32, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev"
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View"
        Me.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Name = "lblOfPages"
        Me.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Size = New System.Drawing.Size(21, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount"
        Me.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Text = "z 0"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Navigation.LblPage.Enabled = False
        Me.PrintPreviewControl.ToolBars.Navigation.LblPage.Name = "lblPage"
        Me.PrintPreviewControl.ToolBars.Navigation.LblPage.Size = New System.Drawing.Size(40, 22)
        Me.PrintPreviewControl.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel"
        Me.PrintPreviewControl.ToolBars.Navigation.LblPage.Text = "Strana"
        Me.PrintPreviewControl.ToolBars.Navigation.ToolTipPageNo = Nothing
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Page.Continuous.Checked = True
        Me.PrintPreviewControl.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PrintPreviewControl.ToolBars.Page.Continuous.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Page.Continuous.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Page.Continuous.Name = "btnPageContinuous"
        Me.PrintPreviewControl.ToolBars.Page.Continuous.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous"
        Me.PrintPreviewControl.ToolBars.Page.Continuous.ToolTipText = "Continuous View"
        Me.PrintPreviewControl.ToolBars.Page.Continuous.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Page.Facing.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Page.Facing.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Page.Facing.Name = "btnPageFacing"
        Me.PrintPreviewControl.ToolBars.Page.Facing.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing"
        Me.PrintPreviewControl.ToolBars.Page.Facing.ToolTipText = "Pages Facing View"
        Me.PrintPreviewControl.ToolBars.Page.Facing.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Page.FacingContinuous.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous"
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous"
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View"
        Me.PrintPreviewControl.ToolBars.Page.FacingContinuous.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Page.Single.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Page.Single.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Page.Single.Name = "btnPageSingle"
        Me.PrintPreviewControl.ToolBars.Page.Single.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle"
        Me.PrintPreviewControl.ToolBars.Page.Single.ToolTipText = "Single Page View"
        Me.PrintPreviewControl.ToolBars.Page.Single.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Text.Find.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Text.Find.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Text.Find.Name = "btnFind"
        Me.PrintPreviewControl.ToolBars.Text.Find.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find"
        Me.PrintPreviewControl.ToolBars.Text.Find.ToolTipText = "Find Text"
        Me.PrintPreviewControl.ToolBars.Text.Find.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Text.Hand.Checked = True
        Me.PrintPreviewControl.ToolBars.Text.Hand.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PrintPreviewControl.ToolBars.Text.Hand.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Text.Hand.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Text.Hand.Name = "btnHandTool"
        Me.PrintPreviewControl.ToolBars.Text.Hand.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool"
        Me.PrintPreviewControl.ToolBars.Text.Hand.ToolTipText = "Hand Tool"
        Me.PrintPreviewControl.ToolBars.Text.Hand.Visible = False
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Text.SelectText.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Text.SelectText.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Text.SelectText.Name = "btnSelectTextTool"
        Me.PrintPreviewControl.ToolBars.Text.SelectText.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool"
        Me.PrintPreviewControl.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool"
        Me.PrintPreviewControl.ToolBars.Text.SelectText.Visible = False
        Me.PrintPreviewControl.ToolBars.Zoom.ToolTipZoomFactor = Nothing
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ToolTipText = "Priblížiť"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ToolTipText = "Vzdialiť"
        '
        '
        '
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintPreviewControl.ToolBars.Zoom.ZoomInTool, Me.PrintPreviewControl.ToolBars.Zoom.ZoomOutTool})
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Image = CType(resources.GetObject("CCNPrintPreview.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Image"), System.Drawing.Image)
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Size = New System.Drawing.Size(32, 22)
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool"
        Me.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Visible = False
        Me.ShowIcon = False
        CType(Me.PrintPreviewControl.PreviewPane, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Protected Sub PreviewPane_OnMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        If (e.Clicks = 2) Then

            If (e.Button = Windows.Forms.MouseButtons.Left) Then

                Me.PreviewPane.ZoomFactor += 0.1

            ElseIf (e.Button = Windows.Forms.MouseButtons.Right) Then

                Me.PreviewPane.ZoomFactor -= 0.1
            End If
        End If
    End Sub
End Class
