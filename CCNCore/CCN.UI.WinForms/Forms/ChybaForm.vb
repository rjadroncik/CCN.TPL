Imports System.ComponentModel
Imports CCN.Model
Imports CCN.Services

Public Class ChybaForm

#Region "Properties"

    Public Chyba As LogChyba

#End Region

#Region "Initialization"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        icon = Icons.Application

        With Grid
            With .Styles.Add("Data")
                .BackColor = Color.BlanchedAlmond
            End With

            With .Styles.Add("Head0")
                .BackColor = Color.FromArgb(255, 220, 220, 220)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With

            With .Styles.Add("Head1")
                .BackColor = Color.FromArgb(255, 192, 192, 192)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With

            With .Styles.Add("Head2")
                .BackColor = Color.FromArgb(255, 192, 192, 192)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With

            With .Styles.Add("Head3")
                .BackColor = Color.FromArgb(255, 192, 192, 192)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With
        End With
    End Sub

#End Region

#Region "Loading"

    Public Sub UpdateGrid()

        With Grid

            .BeginUpdate()
            .Rows.Count = 1

            HodnotaPridaj("Aplikacia", Chyba.Aplikacia, 1)
            HodnotaPridaj("Verzia", Chyba.Verzia.ToString(), 1)
            HodnotaPridaj("Entita", Chyba.Entita, 1)
            HodnotaPridaj("Pouzivatel", If(Chyba.Pouzivatel IsNot Nothing, Chyba.Pouzivatel.Login, Nothing), 1)

            HodnotaPridaj("Trieda", Chyba.Trieda, 1)
            HodnotaPridaj("Metoda", Chyba.Metoda, 1)

            VynimkaPridaj(Chyba.Vynimka, 0)

            .Tree.Show(2)
            .AutoSizeCols(0)
            .AutoSizeRows()
            .EndUpdate()
        End With
    End Sub

    Protected Sub HodnotaPridaj(nazov As String, hodnota As String, uroven As Integer)

        With Grid.Rows.Add()

            .Item("Nazov") = nazov
            .Item("Hodnota") = hodnota

            .IsNode = True
            .Node.Level = uroven + 1

            Grid.SetCellStyle(.Index, Grid.Cols("Hodnota").Index, Grid.Styles("Data"))
        End With
    End Sub

    Protected Sub VynimkaPridaj(vynimka As LogVynimka, uroven As Integer)

        With Grid.Rows.Add()

            .Item("Nazov") = vynimka.Typ
            .Item("Hodnota") = vynimka.Typ

            .IsNode = True
            .Node.Level = uroven
            .AllowMerging = True

            .Style = Grid.Styles("Head" & uroven)
        End With

        HodnotaPridaj("Message", vynimka.Sprava, uroven + 1)
        HodnotaPridaj("Source", vynimka.Zdroj, uroven + 1)
        HodnotaPridaj("TargetSite.DeclaringType", vynimka.Trieda, uroven + 1)
        HodnotaPridaj("TargetSite.Name", vynimka.Metoda, uroven + 1)
        HodnotaPridaj("StackTrace", vynimka.StackTrace, uroven + 1)

        For Each udaj As LogVynimkaUdaj In vynimka.Udaje

            HodnotaPridaj(udaj.Kluc, udaj.Hodnota, uroven + 1)
        Next

        If (vynimka.Vnutorna IsNot Nothing) Then VynimkaPridaj(vynimka.Vnutorna, uroven + 1)
    End Sub

#End Region

#Region "Event handling"

    Private Sub Groupbox_Expand(sender As Object, e As EventArgs) Handles Groupbox.Expand

        DetailyPreTechnickuPodporu(Groupbox.Expanded)
    End Sub

    Private Sub ChybaForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (Chyba IsNot Nothing) Then UpdateGrid()

        DetailyPreTechnickuPodporu((Globals.Pouzivatel IsNot Nothing) AndAlso (Prava.Over(Prava.Debug)))
    End Sub

    Private Sub ChybaForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub Ok_Click(sender As System.Object, e As System.EventArgs) Handles Ok.Click

        Me.Hide()
    End Sub

#End Region

#Region "BL"

    Private Sub DetailyPreTechnickuPodporu(visible As Boolean)

        Grid.Visible = visible
        Groupbox.Expanded = visible

        If (visible) Then

            Me.Size = New Size(500, 400)
        Else
            Me.Size = New Size(500, 400 - 240)
        End If
    End Sub

#End Region

End Class