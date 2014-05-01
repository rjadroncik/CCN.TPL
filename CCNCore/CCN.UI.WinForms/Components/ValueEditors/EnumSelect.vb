Imports C1.Win.C1FlexGrid
Imports System.Drawing.Design

Public Class EnumSelect(Of T As Structure)
    Inherits ComboBox
    Implements IC1EmbeddedEditor

    Private _selected As Boolean = False
    Public Property MustSelect As Boolean

    Protected _comboItems As List(Of KeyValuePair(Of T?, String))

    Protected Overrides Sub OnLostFocus(e As EventArgs)

        If (C1EditorValueIsValid()) Then MyBase.OnLostFocus(e)
    End Sub

    Protected Overrides Sub OnDropDownClosed(e As EventArgs)

        If (C1EditorValueIsValid()) Then MyBase.OnDropDownClosed(e)
    End Sub

    Protected Overrides Sub OnSelectionChangeCommitted(e As EventArgs)
        MyBase.OnSelectionChangeCommitted(e)
        _selected = True
    End Sub

    Private Sub Init()

        Me.DisplayMember = "Value"
        Me.ValueMember = "Key"
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.SetAutoSizeMode(AutoSizeMode.GrowOnly)
        Me.Visible = False
    End Sub

    Protected Overrides Sub OnDropDown(e As EventArgs)
        MyBase.OnDropDown(e)

        Dim width As Integer = 0

        Using g As Graphics = CreateGraphics()

            Dim vertScrollBarWidth As Integer = If(Items.Count > MaxDropDownItems, SystemInformation.VerticalScrollBarWidth, 0)
            Dim newWidth As Integer

            For Each item As KeyValuePair(Of T?, String) In Items

                newWidth = CInt(g.MeasureString(item.Value, Font).Width) + vertScrollBarWidth

                If (width < newWidth) Then width = newWidth
            Next

            DropDownWidth = If(width = 0, Me.Width, width)
        End Using
    End Sub

    Public Sub New()
        MyBase.New()
        Init()
    End Sub

    Public Sub New(values As IDictionary(Of T, String), nullable As Boolean)
        MyBase.New()
        Init()

        Fill(values, nullable)
    End Sub

    Public Sub Fill(values As IDictionary(Of T, String), nullable As Boolean)

        _comboItems = New List(Of KeyValuePair(Of T?, String))()

        If (nullable) Then _comboItems.Add(New KeyValuePair(Of T?, String)(New T?(), ""))

        For Each key As T In values.Keys

            _comboItems.Add(New KeyValuePair(Of T?, String)(key, values(key)))
        Next

        _comboItems.Sort(Function(x, y) x.Value.CompareTo(y.Value))

        For Each item In _comboItems

            Items.Add(item)
        Next
    End Sub

    Public Function C1EditorFormat(value As Object, mask As String) As String Implements IC1EmbeddedEditor.C1EditorFormat

        Return value.ToString()
    End Function

    Public Function C1EditorGetStyle() As UITypeEditorEditStyle Implements IC1EmbeddedEditor.C1EditorGetStyle

        Return Drawing.Design.UITypeEditorEditStyle.DropDown
    End Function

    Public Function C1EditorGetValue() As Object Implements IC1EmbeddedEditor.C1EditorGetValue

        Return If(SelectedIndex > -1, _comboItems(SelectedIndex).Key, Nothing)
    End Function

    Public Sub C1EditorInitialize(value As Object, editorAttributes As IDictionary) Implements IC1EmbeddedEditor.C1EditorInitialize

        Me.Font = DirectCast(editorAttributes("Font"), Font)
        Me.BackColor = DirectCast(editorAttributes("BackColor"), Color)

        Dim hodnota = DirectCast(value, T?)
        If (hodnota.HasValue) Then

            Dim item = _comboItems.Where(Function(x) x.Key.HasValue AndAlso x.Key.Value.Equals(hodnota.Value)).Single()

            SelectedIndex = _comboItems.IndexOf(item)
        Else
            SelectedIndex = -1
        End If
    End Sub

    Public Function C1EditorKeyDownFinishEdit(e As KeyEventArgs) As Boolean Implements IC1EmbeddedEditor.C1EditorKeyDownFinishEdit

        If (e.KeyCode = Keys.Escape) OrElse (e.KeyCode = Keys.Tab) OrElse (e.KeyCode = Keys.Enter) Then Return True
        Return False
    End Function

    Public Sub C1EditorUpdateBounds(rc As System.Drawing.Rectangle) Implements IC1EmbeddedEditor.C1EditorUpdateBounds
        Me.Left = rc.Left - 1
        Me.Top = rc.Top - 1
        Me.Width = rc.Width + 2
        Me.Height = rc.Height + 2
    End Sub

    Public Function C1EditorValueIsValid() As Boolean Implements IC1EmbeddedEditor.C1EditorValueIsValid

        Return (Not MustSelect) OrElse _selected
    End Function
End Class
