Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public Class UdalostiForm

#Region "Initialization"

    Private Sub UdalostiForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        UpdateGrid()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Grid.Cols("Verzia").DataType = GetType(Version)
        Grid.Cols("Typ").DataType = GetType(TypUdalosti)
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

            For Each udalost In Session.Query(Of LogUdalost).OrderByDescending(Function(x) x.Id)

                UdalostPridaj(udalost)
            Next

            .AutoSizeCols(0)
            .EndUpdate()
        End With
    End Sub

    Protected Sub UdalostPridaj(udalost As LogUdalost)

        With Grid.Rows.Add()

            .Item("Id") = udalost.Id
            .Item("Datum") = udalost.Datum
            .Item("Aplikacia") = udalost.Aplikacia
            .Item("Verzia") = udalost.Verzia
            .Item("Entita") = udalost.Entita
            .Item("Pouzivatel") = udalost.Pouzivatel
            .Item("Typ") = udalost.Typ

            .UserData = udalost
        End With
    End Sub

#End Region

End Class