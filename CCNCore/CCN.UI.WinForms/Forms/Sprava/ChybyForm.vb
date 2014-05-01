Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services
Imports CCN.Core.VB

Public Class ChybyForm

#Region "Initialization"

    Private Sub ChybyForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UpdateGrid()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Grid.Cols("Verzia").DataType = GetType(Version)
    End Sub

#End Region

#Region "Properties"

    Public Property Session As ISession

#End Region

#Region "Loading"

    Public Sub UpdateGrid()

        With Grid

            .BeginUpdate()
            .Rows.Count = 1

            For Each chyba In Session.Query(Of LogChyba).OrderByDescending(Function(x) x.Id)

                ChybaPridaj(chyba)
            Next

            .AutoSizeCols(0)
            .EndUpdate()
        End With
    End Sub

    Protected Sub ChybaPridaj(chyba As LogChyba)

        With Grid.Rows.Add()

            .Item("Id") = chyba.Id
            .Item("Datum") = chyba.Datum
            .Item("Aplikacia") = chyba.Aplikacia
            .Item("Verzia") = chyba.Verzia
            .Item("Entita") = chyba.Entita
            .Item("Pouzivatel") = chyba.Pouzivatel
            .Item("Text") = chyba.Vynimka.AkoText.LimitTo(128)

            .UserData = chyba
        End With
    End Sub

#End Region

#Region "Event handling"

    Protected _form As New ChybaForm()

    Private Sub Grid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Grid.MouseDoubleClick

        If (Grid.MouseRow > 0) Then

            Dim chyba As LogChyba = DirectCast(Grid.Rows(Grid.MouseRow).UserData, LogChyba)

            _form.Owner = Me
            _form.Text = "Chyba è. " & chyba.Id
            _form.Sprava.Text = chyba.Vynimka.Sprava
            _form.Ikona.Image = Global.CCN.UI.WinForms.Resources.large_important

            _form.Chyba = chyba
            _form.UpdateGrid()
            _form.Show()
        End If
    End Sub

#End Region

End Class