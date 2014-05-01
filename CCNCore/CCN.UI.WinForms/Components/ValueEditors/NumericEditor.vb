Imports C1.Win.C1Input
Imports System.Drawing.Design
Imports C1.Win.C1FlexGrid

Public MustInherit Class NumericEditor(Of T As IConvertible)
    Inherits C1NumericEdit
    Implements IC1EmbeddedEditor

    Public Sub New()

        VisibleButtons = DropDownControlButtonFlags.None
        SetAutoSizeMode(AutoSizeMode.GrowOnly)
        Visible = False
        DataType = GetType(T)
    End Sub

    Public Shadows Function C1EditorFormat(value As Object, mask As String) As String Implements IC1EmbeddedEditor.C1EditorFormat

        Return String.Format(Me.CustomFormat, value)
    End Function

    Public Shadows Function C1EditorGetStyle() As UITypeEditorEditStyle Implements IC1EmbeddedEditor.C1EditorGetStyle

        Return Drawing.Design.UITypeEditorEditStyle.None
    End Function

    Public Shadows Function C1EditorGetValue() As Object Implements IC1EmbeddedEditor.C1EditorGetValue

        If (Text = Nothing) Then Return Nothing

        Return StringToValue(Text)
    End Function

    Protected MustOverride Function StringToValue(text As String) As T

    Public Shadows Sub C1EditorInitialize(value As Object, editorAttributes As IDictionary) Implements IC1EmbeddedEditor.C1EditorInitialize

        Me.Font = DirectCast(editorAttributes("Font"), Font)
        Me.BackColor = DirectCast(editorAttributes("BackColor"), Color)

        Me.FormatType = FormatTypeEnum.CustomFormat
        Me.CustomFormat = DirectCast(editorAttributes("Format"), String)

        If (value Is Nothing) Then

            Me.ValueIsDbNull = True
            Me.Text = Nothing
        Else
            Me.Value = value
        End If
    End Sub

    Public Shadows Function C1EditorKeyDownFinishEdit(e As KeyEventArgs) As Boolean Implements IC1EmbeddedEditor.C1EditorKeyDownFinishEdit

        If (e.KeyCode = Keys.Escape) OrElse (e.KeyCode = Keys.Enter) Then Return True
        Return False
    End Function

    Public Shadows Sub C1EditorUpdateBounds(rc As System.Drawing.Rectangle) Implements IC1EmbeddedEditor.C1EditorUpdateBounds
        Me.Left = rc.Left - 1
        Me.Top = rc.Top - 1
        Me.Width = rc.Width + 2
        Me.Height = rc.Height + 2
    End Sub

    Public Shadows Function C1EditorValueIsValid() As Boolean Implements IC1EmbeddedEditor.C1EditorValueIsValid

        Return True
    End Function

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)

        If (e.KeyCode = Keys.Delete) Then

            Me.Text = Nothing
            Me.ValueIsDbNull = True
        End If
    End Sub
End Class
