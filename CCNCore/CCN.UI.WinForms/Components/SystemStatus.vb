Imports System.Reflection
Imports NHibernate
Imports System.Data.SqlClient
Imports CCN.Services
Imports System.ComponentModel

Public Class SystemStatus

    Private Sub SystemStatus_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load

        UpdateTime()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As System.EventArgs) Handles Timer.Tick

        UpdateTime()
    End Sub

    Private Sub UpdateTime()

        Cas.Text = DateTime.Now.ToString("HH:mm")
        Datum.Text = DateTime.Now.ToString("dd.MM.yyyy")
    End Sub

    Public Sub UpdateData()

        If (Service.Initialized) Then

            Server.Text = "Server: " & Globals.DB.Server
            DB.Text = "DB: " & Globals.DB.Name
            Pouzivatel.Text = "Používateľ: " & If(Not Globals.Features.Pouzivatelia, "[Neprihlásený]", Globals.Pouzivatel.Login)
            Verzia.Text = "Verzia: " & Globals.Verzia(Globals.AplikaciaBeziaca).ToString()
            Entita.Text = "PC: " & Globals.Entita
        End If
    End Sub

    Public Sub ZobrazUpdateVystrahu(text As String)

        Verzia.ToolTipText = text
        Verzia.Image = Global.CCN.UI.WinForms.Resources.small_warning

        SuperTooltip.Show(Verzia.ToolTipText, Verzia, 5000)
    End Sub

    Private Sub Verzia_MouseHover(sender As System.Object, e As System.EventArgs) Handles Verzia.MouseHover

        SuperTooltip.Show(Verzia.ToolTipText, Verzia, 5000)
    End Sub

End Class
