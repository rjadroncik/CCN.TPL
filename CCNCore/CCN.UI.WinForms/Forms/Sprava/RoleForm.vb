Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public Class RoleForm

#Region "Initialization"

    Private Sub RoleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (Service.Initialized) Then

            PouzivateliaVyber.Vsetky = Servisy.Pouzivatelia.Vsetci(Session)
            UpdateGrids() : UpdatePouzivatelov()
        End If
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

    Public Sub UpdateGrids()

        With GridRole
            Dim selected = .RowSel

            .BeginUpdate()
            .Rows.Count = 1

            For Each rola As IRola In Servisy.Role.Vsetky(Session)

                RolaPridaj(rola)
            Next

            FlexGrid.RestoreSelection(GridRole, selected)

            .EndUpdate()
        End With
    End Sub

    Protected Sub RolaPridaj(rola As IRola)

        With GridRole.Rows.Add()

            .Item("Nazov") = rola.Nazov

            .UserData = rola
        End With
    End Sub

    Protected Sub UpdatePouzivatelov()

        Dim rola = DirectCast(GridRole.Rows(GridRole.Row).UserData, IRola)
        If (rola IsNot Nothing) Then

            PouzivateliaVyber.Zvolene = Servisy.Role.PouzivateliaPre(rola, Session)
            PouzivateliaVyber.UpdateGrids()
        End If
    End Sub

#End Region

#Region "Event handling"

    Private Sub GridRole_SelChange(sender As Object, e As EventArgs) Handles GridRole.SelChange

        If (GridRole.Row >= GridRole.Rows.Fixed) Then UpdatePouzivatelov()
    End Sub

    Private Sub GridRole_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles GridRole.MouseDoubleClick

        If (GridRole.MouseRow > 0) Then Edituj(DirectCast(GridRole.Rows(GridRole.MouseRow).UserData, IRola))
    End Sub

    Private Sub UpravitButton_Click(sender As Object, e As EventArgs) Handles UpravitButton.Click

        If (GridRole.Row > 0) Then Edituj(DirectCast(GridRole.Rows(GridRole.Row).UserData, IRola)) : UpdateGrids()
    End Sub

    Private Sub ZmazatButton_Click(sender As Object, e As EventArgs) Handles ZmazatButton.Click

        If (GridRole.Row > 0) Then Servisy.Role.Zmaz(DirectCast(GridRole.Rows(GridRole.Row).UserData, IRola), Session) : UpdateGrids()
    End Sub

    Private Sub NovaButton_Click(sender As Object, e As EventArgs) Handles NovaButton.Click

        Edituj(Nothing)
    End Sub

    Private Sub PouzivateliaVyber_ObjektPridany(pouzivatel As IPouzivatel) Handles PouzivateliaVyber.ObjektPridany

        Servisy.Role.PouzivatelPridaj(DirectCast(GridRole.Rows(GridRole.Row).UserData, IRola), pouzivatel, Session)
    End Sub

    Private Sub PouzivateliaVyber_ObjektOdobrany(pouzivatel As IPouzivatel) Handles PouzivateliaVyber.ObjektOdobrany

        Servisy.Role.PouzivatelOdober(DirectCast(GridRole.Rows(GridRole.Row).UserData, IRola), pouzivatel, Session)
    End Sub

#End Region

#Region "BL"

    Protected Overridable Sub Edituj(rola As IRola)

        Using form = New RolaForm()

            form.Owner = Me
            form.Rola = rola
            form.Session = Session
            form.ShowDialog()
        End Using

        UpdateGrids()
    End Sub

#End Region

End Class