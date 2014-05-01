Imports C1.Win.C1Input
Imports System.Drawing.Design
Imports C1.Win.C1FlexGrid

Public Class BooleanEditor
    Inherits CheckBox
    Implements IC1EmbeddedEditor

    Public Sub New()

        ThreeState = True
        SetAutoSizeMode(AutoSizeMode.GrowOnly)
        Visible = False
    End Sub

    Public Shadows Function C1EditorFormat(value As Object, mask As String) As String Implements IC1EmbeddedEditor.C1EditorFormat

        Return value.ToString()
    End Function

    Public Shadows Function C1EditorGetStyle() As UITypeEditorEditStyle Implements IC1EmbeddedEditor.C1EditorGetStyle

        Return Drawing.Design.UITypeEditorEditStyle.None
    End Function

    Public Shadows Function C1EditorGetValue() As Object Implements IC1EmbeddedEditor.C1EditorGetValue

        If (CheckState = CheckState.Indeterminate) Then Return Nothing

        Return Checked
    End Function

    Public Shadows Sub C1EditorInitialize(value As Object, editorAttributes As IDictionary) Implements IC1EmbeddedEditor.C1EditorInitialize

        Font = DirectCast(editorAttributes("Font"), Font)
        BackColor = DirectCast(editorAttributes("BackColor"), Color)

        If (value Is Nothing) Then

            CheckState = CheckState.Indeterminate
        Else
            Checked = CBool(value)
        End If
    End Sub

    Public Shadows Function C1EditorKeyDownFinishEdit(e As KeyEventArgs) As Boolean Implements IC1EmbeddedEditor.C1EditorKeyDownFinishEdit

        If (e.KeyCode = Keys.Escape) OrElse (e.KeyCode = Keys.Enter) Then Return True
        Return False
    End Function

    Public Shadows Sub C1EditorUpdateBounds(rc As Rectangle) Implements IC1EmbeddedEditor.C1EditorUpdateBounds
        Left = rc.Left - 1
        Top = rc.Top - 1
        Width = rc.Width + 2
        Height = rc.Height + 2
    End Sub

    Public Shadows Function C1EditorValueIsValid() As Boolean Implements IC1EmbeddedEditor.C1EditorValueIsValid

        Return True
    End Function
End Class
