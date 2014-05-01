Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public Class PravaObjektoveRoleForm

#Region "Initialization"

    Private Sub PravaObjektoveRoleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (Service.Initialized) Then

            RoleVyber.Vsetky = Servisy.Role.Vsetky(Session)
            UpdateGrids()
            UpdateRole()
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

        PravoVyber.Vsetky = Servisy.PravaObjektove.Vsetky(Session)
        PravoVyber.UpdateGrid()
    End Sub

    Protected Sub UpdateRole()

        If (PravoVyber.Zvoleny IsNot Nothing) Then

            RoleVyber.Zvolene = Servisy.PravaObjektove.RolePre(PravoVyber.Zvoleny, Session)
            RoleVyber.UpdateGrids()
        End If
    End Sub

#End Region

#Region "Event handling"

    Private Sub PravoVyber_ObjektZvoleny(pravo As IPravoObjektove) Handles PravoVyber.ObjektZvoleny

        UpdateRole()
    End Sub

    Private Sub PravoVyber_ObjektDoubleClick(pravo As IPravoObjektove) Handles PravoVyber.ObjektDoubleClick

        If (pravo IsNot Nothing) Then Edituj(pravo)
    End Sub

    Private Sub UpravitButton_Click(sender As Object, e As EventArgs) Handles UpravitButton.Click

        If (PravoVyber.Zvoleny IsNot Nothing) Then Edituj(PravoVyber.Zvoleny) : UpdateGrids()
    End Sub

    Private Sub ZmazatButton_Click(sender As System.Object, e As System.EventArgs) Handles ZmazatButton.Click

        If (PravoVyber.Zvoleny IsNot Nothing) Then Servisy.PravaObjektove.Zmaz(PravoVyber.Zvoleny, Session) : UpdateGrids()
    End Sub

    Private Sub NoveButton_Click(sender As Object, e As EventArgs) Handles NoveButton.Click

        Edituj(Nothing)
    End Sub

    Private Sub RoleVyber_ObjektPridany(rola As IRola) Handles RoleVyber.ObjektPridany

        Servisy.PravaObjektove.RolaPridaj(PravoVyber.Zvoleny, rola, Session)
    End Sub

    Private Sub RoleVyber_ObjektOdobrany(rola As IRola) Handles RoleVyber.ObjektOdobrany

        Servisy.PravaObjektove.RolaOdober(PravoVyber.Zvoleny, rola, Session)
    End Sub

#End Region

#Region "BL"

    Protected Overridable Sub Edituj(pravo As IPravoObjektove)

        Using form = New PravoObjektoveForm()

            form.Owner = Me
            form.Pravo = pravo
            form.Session = Session
            form.ShowDialog()
        End Using

        UpdateGrids()
    End Sub

#End Region

End Class