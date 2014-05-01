Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public Class PouzivateliaForm

#Region "Initialization"

    Private Sub PouzivateliaForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (Service.Initialized) Then UpdateGrid()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    Public Property Session As ISession

#End Region

#Region "Loading"

    Public Sub UpdateGrid()

        With Grid
            Dim selected = .RowSel

            .BeginUpdate()
            .Rows.Count = 1

            For Each pouzivatel As IPouzivatel In Servisy.Pouzivatelia.Vsetci(Session)

                PouzivatelPridaj(pouzivatel)
            Next

            FlexGrid.RestoreSelection(Grid, selected)

            .AutoSizeCols(0)
            .EndUpdate()
        End With
    End Sub

    Protected Sub PouzivatelPridaj(pouzivatel As IPouzivatel)

        With Grid.Rows.Add()

            .Item("Login") = pouzivatel.Login
            .Item("Meno") = pouzivatel.Meno
            .Item("Priezvisko") = pouzivatel.Priezvisko
            .Item("Aktivny") = pouzivatel.Aktivny

            .UserData = pouzivatel
        End With
    End Sub

#End Region

#Region "Event handling"

    Private Sub Grid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Grid.MouseDoubleClick

        If (Grid.MouseRow > 0) Then Edituj(DirectCast(Grid.Rows(Grid.MouseRow).UserData, IPouzivatel))
    End Sub

    Private Sub UpravitButton_Click(sender As Object, e As EventArgs) Handles UpravitButton.Click

        If (Grid.Row > 0) Then Edituj(DirectCast(Grid.Rows(Grid.Row).UserData, IPouzivatel)) : UpdateGrid()
    End Sub

    Private Sub NovyButton_Click(sender As Object, e As EventArgs) Handles NovyButton.Click

        Edituj(Nothing)
    End Sub

#End Region

#Region "BL"

    Protected Overridable Sub Edituj(pouzivatel As IPouzivatel)

        Using form = New PouzivatelForm()

            form.Owner = Me
            form.Pouzivatel = pouzivatel
            form.Session = Session
            form.ShowDialog()
        End Using

        UpdateGrid()
    End Sub

#End Region

End Class