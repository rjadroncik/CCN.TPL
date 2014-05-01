Imports C1.Win.C1List
Imports C1.Win.C1FlexGrid

Public Class GuiUtil

#Region "Properties"

    Public Shared Property MainForm As Form

#End Region

#Region "Common"

    Public Shared Function TrimmedText(editor As Control) As String

        If ((editor IsNot Nothing) AndAlso (editor.Text IsNot Nothing)) Then Return editor.Text.Trim()
        Return Nothing
    End Function

#End Region

#Region "Data"

    Public Shared Function CiselnikToIdValuePairs(Of T1, T2 As IComparable)(ciselnik As IDictionary(Of T1, T2)) As IList(Of KeyValuePair(Of T1, T2))

        Dim result As New List(Of KeyValuePair(Of T1, T2))

        For Each key As T1 In ciselnik.Keys

            result.Add(New KeyValuePair(Of T1, T2)(key, ciselnik(key)))
        Next

        result.Sort(Function(x, y) x.Value.CompareTo(y.Value))

        Return result
    End Function

#End Region

#Region "ComboBox"

    Public Shared Sub Combo2ColLoad(Of T1, T2)(combo As C1Combo, values As IDictionary(Of T1, T2))

        With combo
            For Each key As T1 In values.Keys

                .AddItem(key.ToString() & .AddItemSeparator & values(key).ToString())
            Next

            Combo2ColAutoSize(combo)
        End With
    End Sub

    Public Delegate Function Formater(Of In T1, In T2)(x As T1, y As T2) As String

    Public Shared Sub InitComboCombinedDisplayValue(Of T1, T2)(combo As C1Combo, values As IDictionary(Of T1, T2), _
                                                               formater As Formater(Of T1, T2))
        With combo
            For Each key As T1 In values.Keys

                .AddItem(key.ToString() & .AddItemSeparator & values(key).ToString() & .AddItemSeparator & formater(key, values(key)))
            Next

            .Splits(0).DisplayColumns(2).Visible = False

            Combo2ColAutoSize(combo)
        End With
    End Sub

    Public Shared Sub Combo2ColAutoSize(combo As C1Combo)

        With combo
            If (.Splits(0).DisplayColumns.Count < 2) Then Return

            .Splits(0).DisplayColumns(0).AutoSize()
            .Splits(0).DisplayColumns(1).AutoSize()
            .DropDownWidth = .Splits(0).DisplayColumns(0).Width + _
                             .Splits(0).DisplayColumns(1).Width + 4

            If (.ListCount > .MaxDropDownItems) Then .DropDownWidth += .VScrollBar.Width
        End With
    End Sub

#End Region

End Class
