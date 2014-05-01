Public Class Draggable
    Inherits Control

#Region "Properties"

    Public Property Vertical As Boolean = False
    Public Property DragPadding As Integer = 2
    Public Property DragSpacing As Integer = 4
    Public Property DragCursor As Cursor = Cursors.Hand

    Public Property DragStart As Point
    Public Property DragCurrent As Point

    Public Property ColorLight As Color
        Get
            Return _penLight.Color
        End Get
        Set(value As Color)
            _penLight = New Pen(value)
        End Set
    End Property

    Public Property ColorDark As Color
        Get
            Return _penDark.Color
        End Get
        Set(value As Color)
            _penDark = New Pen(value)
        End Set
    End Property

#End Region

#Region "Painting"

    Protected _penLight As Pen = New Pen(SystemColors.ButtonHighlight)
    Protected _penDark As Pen = New Pen(SystemColors.ButtonShadow)

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        MyBase.OnPaintBackground(pevent)

        With pevent.Graphics

            If (Vertical) Then

                For x As Integer = DragPadding To Me.Width - DragPadding Step DragSpacing

                    .DrawLine(_penLight, x, DragPadding, x, Me.Height - DragPadding - 1)
                    .DrawLine(_penDark, x + 1, DragPadding, x + 1, Me.Height - DragPadding - 1)
                Next
            Else
                For y As Integer = DragPadding To Me.Height - DragPadding Step DragSpacing

                    .DrawLine(_penLight, DragPadding, y, Me.Width - DragPadding - 1, y)
                    .DrawLine(_penDark, DragPadding, y + 1, Me.Width - DragPadding - 1, y + 1)
                Next
            End If
        End With

    End Sub

#End Region

#Region "Event handling"

    Public Event DragStarted As Action(Of Draggable)
    Public Event Dragging As Action(Of Draggable)
    Public Event DragEnded As Action(Of Draggable)

    Protected _dragging As Boolean = False
    Protected _cursor As Cursor

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        If (Not _dragging) Then

            _cursor = Cursor
            _dragging = True
            Me.Capture = True

            Cursor = DragCursor
            DragStart = System.Windows.Forms.Cursor.Position
            DragCurrent = System.Windows.Forms.Cursor.Position

            RaiseEvent DragStarted(Me)
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        If (_dragging) Then OnDragEnded()
    End Sub

    Protected _lastScreenPos As Point = System.Windows.Forms.Cursor.Position
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If (_dragging AndAlso _
            (System.Windows.Forms.Cursor.Position <> _lastScreenPos)) Then

            _lastScreenPos = System.Windows.Forms.Cursor.Position

            DragCurrent = System.Windows.Forms.Cursor.Position

            RaiseEvent Dragging(Me)
        End If
    End Sub

    Protected Overrides Sub OnMouseCaptureChanged(e As EventArgs)
        MyBase.OnMouseCaptureChanged(e)

        If (Me.Capture = False) Then OnDragEnded()
    End Sub

    Protected Sub OnDragEnded()

        _dragging = False
        Me.Capture = False
        Cursor = _cursor

        RaiseEvent DragEnded(Me)
    End Sub

#End Region

End Class
