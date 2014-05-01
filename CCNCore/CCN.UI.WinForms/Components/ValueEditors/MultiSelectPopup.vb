Imports System.Text
Imports C1.Win.C1FlexGrid

Public Class MultiSelectPopup
    Implements MultiSelect.IEmbeddableDropDown

    Public Function GetValue() As Object Implements MultiSelect.IEmbeddableDropDown.GetValue

        Dim result As New StringBuilder()
        Dim first As Boolean = True

        For Each row As Row In Grid.Rows

            If (row.Index < Grid.Rows.Fixed) Then Continue For

            If (DirectCast(row("Selected"), Boolean)) Then

                If (Not first) Then result.Append(", ")

                result.Append(row("Value"))

                first = False
            End If
        Next

        Return result.ToString()
    End Function

    Public Sub SetValue(value As Object) Implements MultiSelect.IEmbeddableDropDown.SetValue

        With Grid
            .BeginUpdate()

            For Each row As Row In .Rows

                row("Selected") = False
            Next

            If (value IsNot Nothing) Then

                For Each selected As String In value.ToString().Split(","c)

                    Dim trimmed = selected.Trim()
                    If (trimmed.Length = 0) Then Continue For

                    Dim row As Row = .Rows(.FindRow(trimmed, 0, 1, True, True, False))

                    row("Selected") = True
                Next
            End If

            .EndUpdate()
        End With
    End Sub

    Public Sub Fill(values As IDictionary(Of String, String))

        With Grid
            .BeginUpdate()

            .Rows.Count = 0
            .Rows.Count = values.Count

            Dim i As Integer = 0
            For Each key As String In values.Keys

                .Rows(i)("Selected") = False
                .Rows(i)("Value") = key
                .Rows(i)("Description") = values(key)
                i += 1
            Next

            .AutoSizeCols(0)
            .EndUpdate()
        End With
    End Sub

    Public Property MaxVisibleRows As Integer = 2

    Public Function GetPreferredBounds() As Size Implements MultiSelect.IEmbeddableDropDown.GetPreferredBounds

        With Grid
            Dim width As Integer = 0
            Dim height As Integer = 0

            For Each col As Column In Grid.Cols

                width += col.WidthDisplay + col.StyleDisplay.Border.Width
            Next

            Dim count = 0
            For Each row As Row In Grid.Rows

                If (row.Index < Grid.Rows.Fixed) Then Continue For

                If (count = MaxVisibleRows) Then Exit For

                height += row.HeightDisplay + row.StyleDisplay.Border.Width
                count += 1
            Next

            Me.Width = width + If(Grid.Rows.Count > MaxVisibleRows, SystemInformation.VerticalScrollBarWidth, 0)
            Me.Height = height - 1

            Me.Width += Grid.Width - Grid.ClientSize.Width

            Return New Size(Me.Width, Me.Height)
        End With
    End Function

    Private Sub MultiSelectPopup_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load

        Grid.Styles("Focus").DefinedElements = StyleElementFlags.None
        Grid.Styles("Highlight").DefinedElements = StyleElementFlags.None
    End Sub
End Class
