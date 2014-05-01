Imports CCN.Model
Imports CCN.Services

Public Class LoginForm(Of T As IPouzivatel)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Icon = Icons.Application
        Text = Globals.Aplikacia(Globals.AplikaciaBeziaca) & " - Prihlásenie"

        Copyright.Text = "© " & Date.Now.Year & " CCN s.r.o."
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click

        Prihlas()
    End Sub

    Protected Sub Prihlas()

        If (Prihlasovanie.Prihlas(Of T)(UsernameTextBox.Text, PasswordTextBox.Text) IsNot Nothing) Then

            DialogResult = DialogResult.OK
        Else
            Upozornenia.Upozornenie("Nesprávny login alebo heslo!", "Neúspešné prihlásenie")
        End If
    End Sub

End Class
