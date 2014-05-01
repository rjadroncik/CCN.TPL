Imports C1.Win.C1FlexGrid

Public Class FlexGrid

    Public Shared Sub RestoreSelection(grid As C1FlexGrid, selectedBefore As Integer)

        With grid
            If (selectedBefore >= grid.Rows.Fixed) AndAlso (selectedBefore < .Rows.Count) Then

                .RowSel = selectedBefore
                .Row = selectedBefore
            Else

                For Each row As Row In .Rows

                    If ((row.Index >= grid.Rows.Fixed) AndAlso row.Visible) Then

                        .RowSel = row.Index
                        .Row = row.Index
                        Exit For
                    End If
                Next
            End If
        End With
    End Sub

    Public Shared Function ColumnsWidth(grid As C1FlexGrid) As Integer

        Dim width As Integer = 0

        For Each column As Column In grid.Cols

            width += column.WidthDisplay
        Next

        Return width
    End Function

    Public Shared Sub ColumnsGenerate(Of T As Class)(grid As C1FlexGrid, stlpce As IEnumerable(Of Stlpec(Of T)))

        grid.Cols.Count = 0

        For Each stlpec As Stlpec(Of T) In stlpce

            With grid.Cols.Add()

                .Name = stlpec.Nazov
                .Caption = stlpec.Titulok
                .Format = stlpec.Format

                Select Case (stlpec.Zoradenie)
                    Case Zoradenie.Ziadne
                        .Sort = SortFlags.None
                    Case Zoradenie.Vzostupne
                        .Sort = .Sort Or SortFlags.Ascending
                    Case Zoradenie.Zostupne
                        .Sort = .Sort Or SortFlags.Descending
                End Select

                Select Case (stlpec.Zarovnanie)
                    Case Zarovnanie.Ziadne
                        .TextAlign = TextAlignEnum.GeneralCenter
                    Case Zarovnanie.LavyOkraj
                        .TextAlign = TextAlignEnum.LeftCenter
                    Case Zarovnanie.Stred
                        .TextAlign = TextAlignEnum.CenterCenter
                    Case Zarovnanie.PravyOkraj
                        .TextAlign = TextAlignEnum.RightCenter
                End Select

                .DataType = stlpec.DataType
                .ImageMap = stlpec.ImageMap
            End With
        Next
    End Sub

    Public Shared Sub Sort(Of T As Class)(grid As C1FlexGrid, stlpce As IEnumerable(Of Stlpec(Of T)))

        With grid
            For Each stlpec In stlpce.OrderByDescending(Function(x) .Cols(x.Nazov).Index)

                Select Case stlpec.Zoradenie

                    Case Zoradenie.Vzostupne
                        .Sort(SortFlags.Ascending, .Cols(stlpec.Nazov).Index)
                    Case Zoradenie.Zostupne
                        .Sort(SortFlags.Descending, .Cols(stlpec.Nazov).Index)
                End Select
            Next
        End With
    End Sub

End Class
