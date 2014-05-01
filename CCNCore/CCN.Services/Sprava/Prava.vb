Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model

Public MustInherit Class Prava
    Inherits Service
    Implements IPrava

#Region "IPrava"

    Public Function Vsetky(session As ISession) As IList(Of IPravo) Implements IPrava.Vsetky

        Return session.Query(Of IPravo).OrderBy(Function(x) x.Nazov).ToList()
    End Function

#Region "Pouzivatelia"

    Public MustOverride Function PouzivateliaPre(pravo As IPravo, session As ISession) As IList(Of IPouzivatel) Implements IPrava.PouzivateliaPre

    Public MustOverride Sub PouzivatelOdober(pravo As IPravo, pouzivatel As IPouzivatel, session As ISession) Implements IPrava.PouzivatelOdober

    Public MustOverride Sub PouzivatelPridaj(pravo As IPravo, pouzivatel As IPouzivatel, session As ISession) Implements IPrava.PouzivatelPridaj

#End Region

#Region "Role"

    Public MustOverride Function RolePre(pravo As IPravo, session As ISession) As IList(Of IRola) Implements IPrava.RolePre

    Public MustOverride Sub RolaOdober(pravo As IPravo, rola As IRola, session As ISession) Implements IPrava.RolaOdober

    Public MustOverride Sub RolaPridaj(pravo As IPravo, rola As IRola, session As ISession) Implements IPrava.RolaPridaj

#End Region

#End Region

#Region "Properties"

    'Nastavenia
    Public Shared ReadOnly Property Nastavenia As IPravo
        Get
            Return PodlaId(IdPrava.Nastavenia)
        End Get
    End Property

    'Editácia užívateľa
    Public Shared ReadOnly Property EditaciaPouzivatela As IPravo
        Get
            Return PodlaId(IdPrava.EditaciaPouzivatela)
        End Get
    End Property

    'Debugovacie nástroje
    Public Shared ReadOnly Property Debug As IPravo
        Get
            Return PodlaId(IdPrava.Debug)
        End Get
    End Property

#End Region

#Region "Nacitavanie"

    Protected Shared _prava As New Dictionary(Of IdPrava, IPravo)

    Protected Shared Function PodlaId(id As IdPrava) As IPravo

        If (Not Globals.Features.Prava) Then Return Nothing

        If (Not _prava.ContainsKey(id)) Then

            Using session As ISession = NewSession()

                _prava.Add(id, session.Query(Of IPravo).Where(Function(x) x.Id = id).Single())
            End Using
        End If

        Return _prava(id)
    End Function

#End Region

#Region "Overovanie"

    Public Shared Function Over(pravo As IPravo) As Boolean

        If (Not Globals.Features.Prava) Then Return True

        Return Globals.Pouzivatel.Prava.Contains(pravo)
    End Function

#End Region

End Class
