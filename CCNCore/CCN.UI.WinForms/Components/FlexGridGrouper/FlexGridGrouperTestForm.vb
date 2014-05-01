Public Class FlexGridGrouperTestForm

    Private Sub FlexGridGrouperTestForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Grid.Rows.Count = 1

        With Grid.Rows.Add()

            .Item("A") = "TEST"
            .Item("B") = 1
            .Item("C") = 1
            .Item("D") = "1"
            .Item("E") = "TEST2"
        End With

        With Grid.Rows.Add()

            .Item("A") = "TEST"
            .Item("B") = 2
            .Item("C") = 1
            .Item("D") = "1"
            .Item("E") = "TEST2"
        End With

        With Grid.Rows.Add()

            .Item("A") = "TEST"
            .Item("B") = 3
            .Item("C") = 2
            .Item("D") = "2"
            .Item("E") = "TEST2"
        End With

        With Grid.Rows.Add()

            .Item("A") = "TEST"
            .Item("B") = 4
            .Item("C") = 2
            .Item("D") = "2"
            .Item("E") = "TEST2"
        End With

    End Sub
End Class