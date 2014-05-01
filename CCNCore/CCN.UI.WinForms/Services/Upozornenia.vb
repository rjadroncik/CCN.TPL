Imports CCN.Model
Imports CCN.Services

Public Class Upozornenia

#Region "Otazky"

    Public Shared Function OtazkaAnoNieVarovanie(sprava As String, nadpis As String) As Boolean

        Return (MessageBox.Show(sprava, nadpis, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes)
    End Function

    Public Shared Function OtazkaAnoNie(sprava As String, nadpis As String) As Boolean

        Return (MessageBox.Show(sprava, nadpis, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes)
    End Function

    Public Shared Function OtazkaZatvorenieOkna() As Boolean

        Return (MessageBox.Show("Naozaj chcete zresetovať doteraz vykonané zmeny?" & Environment.NewLine & _
                                "Vykonané zmeny sa N E U L O Ž I A !!!", "Potvrdenie zresetovania dialógu", _
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes)
    End Function

#End Region

#Region "Oznamy"

    Public Shared Sub NematePravo(pravo As IPravo)

        MessageBox.Show("Nemáte pridelené právo: [" & pravo.Nazov & "]", "Akciu nemožno vykonať", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    End Sub

    Public Shared Sub Oznamenie(sprava As String, nadpis As String)

        MessageBox.Show(sprava, nadpis, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Varuje používateľa o probléme korý mu bráni v pokračovaní.
    ''' </summary>
    ''' <param name="sprava"></param>
    ''' <param name="nadpis"></param>
    ''' <remarks></remarks>
    Public Shared Sub Upozornenie(sprava As String, nadpis As String)

        MessageBox.Show(sprava, nadpis, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    ''' <summary>
    ''' Varuje používateľa o probléme napriek ktorému mu bude umožnené pokračovat (na vlastné riziko).
    ''' </summary>
    ''' <param name="sprava"></param>
    ''' <param name="nadpis"></param>
    ''' <remarks></remarks>
    Public Shared Sub Varovanie(sprava As String, nadpis As String)

        MessageBox.Show(sprava, nadpis, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

#End Region

#Region "Chyby"

    Public Shared Sub Varovanie(vynimka As RecoverableServiceException, nadpis As String)

        Using form As New ChybaForm()

            form.Text = Globals.Aplikacia(Globals.AplikaciaBeziaca) & " - " & nadpis
            form.Sprava.Text = vynimka.Message
            form.Ikona.Image = Global.CCN.UI.Winforms.Resources.large_warning

            form.Chyba = LogDB.ZalogujChybu(vynimka, 2)
            form.ShowDialog()
        End Using
    End Sub

    Public Shared Sub Chyba(vynimka As FatalServiceException, nadpis As String)

        Using form As New ChybaForm()

            form.Text = Globals.Aplikacia(Globals.AplikaciaBeziaca) & " - " & nadpis
            form.Sprava.Text = vynimka.Message
            form.Ikona.Image = Global.CCN.UI.Winforms.Resources.large_important

            form.Chyba = LogDB.ZalogujChybu(vynimka, 2)
            form.ShowDialog()
        End Using
    End Sub

    Public Shared Sub ChybaNeosetrena(vynimka As Exception)

        Using form As New ChybaForm()

            form.Text = Globals.Aplikacia(Globals.AplikaciaBeziaca) & " - " & "Nepredvídaná chyba systému, kontaktujte technickú podporu!"
            form.Sprava.Text = vynimka.Message & Environment.NewLine & _
                               "[Po zatvorení tohto okna sa aplikácia ukončí! Urobte preto predtým prosím snímku obrazovky.]"
            form.Ikona.Image = Global.CCN.UI.Winforms.Resources.large_error

            form.Chyba = LogDB.ZalogujChybu(vynimka, 2)
            form.ShowDialog()
        End Using
    End Sub

#End Region

End Class
