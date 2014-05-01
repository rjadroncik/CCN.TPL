Imports C1.Win.C1FlexGrid
Imports System.Runtime.InteropServices
Imports CCN.Core.VB

<System.ComponentModel.Designer(GetType(FlexGridGrouperDesigner))> _
Public Class FlexGridGrouper

#Region "Properties"

    <System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)> _
    Public Property Grid As C1FlexGrid
        Get
            Return If(Split.Panel2.Controls.Count > 0, DirectCast(Split.Panel2.Controls(0), C1FlexGrid), Nothing)
        End Get
        Set(value As C1FlexGrid)

            If (Split.Panel2.Controls.Count > 0) Then

                Dim grid As C1FlexGrid = DirectCast(Split.Panel2.Controls(0), C1FlexGrid)

                RemoveHandler grid.MouseMove, AddressOf Grid_MouseMove
                RemoveHandler grid.BeforeResizeColumn, AddressOf Grid_BeforeResizeColumn
                RemoveHandler grid.AfterResizeColumn, AddressOf Grid_AfterResizeColumn
                RemoveHandler grid.BeforeDragColumn, AddressOf Grid_BeforeDragColumn

                Split.Panel2.Controls.Remove(grid)
            End If

            If (value IsNot Nothing) Then

                value.Dock = DockStyle.Fill

                AddHandler value.MouseMove, AddressOf Grid_MouseMove
                AddHandler value.BeforeResizeColumn, AddressOf Grid_BeforeResizeColumn
                AddHandler value.AfterResizeColumn, AddressOf Grid_AfterResizeColumn
                AddHandler value.BeforeDragColumn, AddressOf Grid_BeforeDragColumn

                Split.Panel2.Controls.Add(value)

                ExpandAll.Image = value.Glyphs(GlyphEnum.Collapsed)
                CollapseAll.Image = value.Glyphs(GlyphEnum.Expanded)
            End If
        End Set
    End Property

#End Region

#Region "Fields"

    Private _groups As New List(Of FlexGridGroup)
    Private _overlay As New FlexGridOverlay

    Private _dragSource As Control
    Private _dragging As Boolean = False
    Private _resizing As Boolean = False
    Private _offset As Size

    Private _columns As List(Of Column)

#End Region

#Region "Drawing"

    Protected Const SpacingVertical As Integer = 5
    Protected Const SpacingHorizontal As Integer = 8

    Protected LightPen As New Pen(SystemColors.ButtonHighlight)
    Protected ShadowPen As New Pen(SystemColors.ButtonShadow)

    Private Sub GroupingView_Paint(sender As Object, e As PaintEventArgs) Handles GroupingView.Paint

        For i As Integer = 0 To _groups.Count - 2

            Dim first As FlexGridGroup = _groups(i)
            Dim second As FlexGridGroup = _groups(i + 1)

            e.Graphics.DrawLine(ShadowPen, first.Right - 4, first.Bottom, first.Right - 4, second.Bottom - 2)
            e.Graphics.DrawLine(LightPen, first.Right - 3, second.Bottom - 3, second.Left, second.Bottom - 3)

            e.Graphics.DrawLine(LightPen, first.Right - 5, first.Bottom, first.Right - 5, second.Bottom - 3)
            e.Graphics.DrawLine(ShadowPen, first.Right - 4, second.Bottom - 2, second.Left, second.Bottom - 2)
        Next

        If (_groups.IsEmpty()) Then

            Dim stringFormat As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

            e.Graphics.DrawString("Na zobrazenie sumárnych hodnôt chyťte a preneste stpĺce na túto plochu", _
                                  GroupingView.Font, SystemBrushes.Window, _
                                  New RectangleF(0, 0, GroupingView.ClientSize.Width, GroupingView.ClientSize.Height), stringFormat)
        End If
    End Sub

#End Region

#Region "Win32 API"

    Private Shared ReadOnly HWND_NOTOPMOST As IntPtr = New IntPtr(-2)
    Private Shared ReadOnly HWND_TOP As IntPtr = New IntPtr(0)
    Private Shared ReadOnly HWND_TOPMOST As IntPtr = New IntPtr(-1)

    Private Const SWP_NOSIZE As UInt32 = &H1
    Private Const SWP_NOMOVE As UInt32 = &H2
    Private Const SWP_NOACTIVATE As UInt32 = &H10
    Private Const SWP_SHOWWINDOW As UInt32 = &H40
    Private Const SWP_HIDEWINDOW As UInt32 = &H80

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function

#End Region

#Region "Grouping"

    Public Sub Reset()

        With Grid
            .Subtotal(AggregateEnum.Clear)

            .AllowMerging = AllowMergingEnum.Nodes

            If (_columns Is Nothing) Then

                'Keep original column order
                _columns = New List(Of Column)
                For Each column As Column In .Cols

                    _columns.Add(column)
                Next
            End If

            'Restore original column order
            Dim index As Integer = 0
            For Each column As Column In _columns

                If (column.Index <> index) Then column.Move(index)
                index += 1
            Next
        End With
    End Sub

    Public Sub UpdateGroups()

        Dim position As New Point(5, 5)

        For Each group As FlexGridGroup In _groups

            group.Location = position

            position.X += group.Width + SpacingHorizontal
            position.Y += SpacingVertical
        Next

        Split.SplitterDistance = 17 + ((_groups.Count - 1) * SpacingVertical) + 15

        GroupingView.Invalidate()

        With Grid

            .BeginUpdate()
            Reset()

            'Show/hide outline tree
            .Cols("{STROM}").Visible = Not _groups.IsEmpty()
            .Cols("{POCET}").Visible = Not _groups.IsEmpty()
            .Tree.Column = 0
            .Tree.Style = TreeStyleFlags.Simple

            Dim level As Integer = 1
            For Each group As FlexGridGroup In _groups

                group.Column.Move(level)

                If (group.Column.WidthDisplay > 0) Then group.ColumnWidthDisplay = group.Column.WidthDisplay
                group.Column.Width = 0
                level += 1
            Next

            .Cols("{POCET}").Move(.Cols.Count - 1)

            'Sort on each grouped column, in backward order to make lowest lvl group most important
            For i As Integer = _groups.Count - 1 To 0 Step -1

                .Sort(SortFlags.Ascending, _groups(i).Column.Index)
            Next

            'Add subtotals
            For Each group As FlexGridGroup In _groups

                .Subtotal(AggregateEnum.Count, group.Column.Index - 1, group.Column.Index, .Cols.Count - 1, "{0}")
            Next

            If (.Cols("{STROM}").Visible) Then

                .Tree.Show(_groups.Count + 1)
                .Subtotal(AggregateEnum.Count, -1, -1, .Cols.Count - 1, "Počet spolu:")
                .AutoSizeCol(0)
                .AutoSizeCol(.Cols.Count - 1)
            End If

            .EndUpdate()
        End With
    End Sub

#End Region

#Region "Event handlers"

    Private Sub Group_MouseMove(sender As Object, e As MouseEventArgs)

        If ((Not _resizing) AndAlso (Not _dragging) AndAlso (e.Button = MouseButtons.Left)) Then

            Dim group As FlexGridGroup = DirectCast(sender, FlexGridGroup)

            _dragSource = DirectCast(sender, Control)
            _dragging = True
            Me.Capture = True

            _offset = New Size(e.Location)
            _overlay.Column = group.Column

            SetWindowPos(_overlay.Handle, HWND_TOPMOST, MousePosition.X - _offset.Width, MousePosition.Y - _offset.Height, group.Width, group.Height, SWP_SHOWWINDOW Or SWP_NOACTIVATE)
            _overlay.Show()
        End If

    End Sub

    Private Sub Group_MouseDoubleClick(sender As Object, e As MouseEventArgs)

        If ((Not _resizing) AndAlso (Not _dragging) AndAlso (e.Button = MouseButtons.Left)) Then

            Dim group As FlexGridGroup = DirectCast(sender, FlexGridGroup)

            Grid.Tree.Show(_groups.IndexOf(group))
        End If
    End Sub

    Private Sub Grid_BeforeDragColumn(sender As Object, e As DragRowColEventArgs)

        If (e.Col < _groups.Count) Then e.Cancel = True
    End Sub

    Private Sub Grid_BeforeResizeColumn(sender As Object, e As RowColEventArgs)

        _resizing = True
    End Sub

    Private Sub Grid_AfterResizeColumn(sender As Object, e As RowColEventArgs)

        _resizing = False
    End Sub

    Private Sub Grid_MouseMove(sender As Object, e As MouseEventArgs)

        If ((Not _resizing) AndAlso (Not _dragging) AndAlso (e.Button = MouseButtons.Left) AndAlso _
            (Grid.MouseRow = 0) AndAlso (Grid.MouseCol > 0) AndAlso (Grid.MouseCol < Grid.Cols.Count - 1)) Then

            Dim rect As Rectangle = Grid.GetCellRect(Grid.MouseRow, Grid.MouseCol)

            If ((e.Location.X > rect.Left + 10) AndAlso (e.Location.Y < rect.Right - 10)) Then

                _dragSource = DirectCast(sender, Control)
                _dragging = True
                Me.Capture = True

                _offset = New Size(e.Location) - New Size(rect.Location)

                _overlay.Column = Grid.Cols(Grid.MouseCol)

                If (_offset.Width > (_overlay.Group.Width \ 2)) Then _offset.Width = _overlay.Group.Width \ 2

                SetWindowPos(_overlay.Handle, HWND_TOPMOST, MousePosition.X - _offset.Width, MousePosition.Y - _offset.Height, _overlay.Group.Width, _overlay.Group.Height, SWP_SHOWWINDOW Or SWP_NOACTIVATE)
                _overlay.Show()
            End If
        End If

    End Sub

    Private Sub FlexGridGrouper_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

        _overlay.Location = MousePosition - _offset
    End Sub

    Private Sub FlexGridGrouper_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp

        If (_dragging) Then

            _dragging = False
            _overlay.Visible = False

            If (GroupingView.DisplayRectangle.Contains(e.Location)) Then

                Dim groupOld As FlexGridGroup = Nothing
                Dim indexNew As Integer = 0

                For Each group As FlexGridGroup In _groups

                    If (group.Column Is _overlay.Column) Then groupOld = group

                    If (e.Location.X > group.Right) Then indexNew += 1
                Next

                If (groupOld IsNot Nothing) Then

                    _groups.Remove(groupOld)
                    _groups.Insert(If(indexNew > _groups.Count, _groups.Count, indexNew), groupOld)
                Else
                    Dim group As New FlexGridGroup()
                    With group
                        .AutoSize = True
                        .Column = _overlay.Column
                        .Text = _overlay.Column.Caption

                        AddHandler .MouseMove, AddressOf Me.Group_MouseMove
                        AddHandler .MouseDoubleClick, AddressOf Me.Group_MouseDoubleClick
                    End With

                    _groups.Insert(indexNew, group)
                    GroupingView.Controls.Add(group)
                End If

                UpdateGroups()

            ElseIf (Grid.DisplayRectangle.Contains(e.Location)) Then

                If (_dragSource IsNot Grid) Then

                    For Each group As FlexGridGroup In _groups

                        If (group.Column Is _overlay.Column) Then

                            If (group.Column.Width = 0) Then group.Column.Width = group.ColumnWidthDisplay

                            _groups.Remove(group)
                            GroupingView.Controls.Remove(group)

                            UpdateGroups()
                            Return
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub ExpandAll_Click(sender As Object, e As EventArgs) Handles ExpandAll.Click

        Grid.Tree.Show(_groups.Count + 1)
    End Sub

    Private Sub CollapseAll_Click(sender As Object, e As EventArgs) Handles CollapseAll.Click

        Grid.Tree.Show(0)
    End Sub


    'Private Sub FlexGridGrouper_ControlAdded(sender As System.Object, e As System.Windows.Forms.ControlEventArgs) Handles MyBase.ControlAdded

    '    If (TypeOf e.Control Is C1FlexGrid) Then

    '        If (_grid IsNot Nothing) Then

    '            RemoveHandler _grid.MouseMove, AddressOf Grid_MouseMove
    '            RemoveHandler _grid.BeforeResizeColumn, AddressOf Grid_BeforeResizeColumn
    '            RemoveHandler _grid.AfterResizeColumn, AddressOf Grid_AfterResizeColumn

    '            Controls.Remove(_grid)
    '        End If

    '        _grid = DirectCast(e.Control, C1FlexGrid)
    '        _grid.Dock = DockStyle.Fill

    '        AddHandler _grid.MouseMove, AddressOf Grid_MouseMove
    '        AddHandler _grid.BeforeResizeColumn, AddressOf Grid_BeforeResizeColumn
    '        AddHandler _grid.AfterResizeColumn, AddressOf Grid_AfterResizeColumn
    '    End If
    'End Sub

    'Private Sub FlexGridGrouper_ControlRemoved(sender As System.Object, e As System.Windows.Forms.ControlEventArgs) Handles MyBase.ControlRemoved

    '    If (TypeOf e.Control Is C1FlexGrid) Then

    '        If (_grid Is e.Control) Then

    '            RemoveHandler _grid.MouseMove, AddressOf Grid_MouseMove
    '            RemoveHandler _grid.BeforeResizeColumn, AddressOf Grid_BeforeResizeColumn
    '            RemoveHandler _grid.AfterResizeColumn, AddressOf Grid_AfterResizeColumn

    '            _grid = Nothing
    '        End If

    '    End If
    'End Sub

#End Region

End Class