Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model

Public MustInherit Class PravaObjektove
    Inherits Service
    Implements IPravaObjektove

#Region "IPravaObjektove"

    Public MustOverride Function Create() As IPravoObjektove Implements IPravaObjektove.Create

    Public MustOverride Sub Zmaz(pravo As IPravoObjektove, session As ISession) Implements IPravaObjektove.Zmaz

    Public MustOverride Sub UlozZmeny(pravo As IPravoObjektove, session As ISession) Implements IPravaObjektove.UlozZmeny

    Public Function Vsetky(session As ISession) As IList(Of IPravoObjektove) Implements IPravaObjektove.Vsetky

        Return session.Query(Of IPravoObjektove).OrderBy(Function(x) x.Nazov).ToList()
    End Function

    Public Function Cinnosti(session As ISession) As IList(Of Cinnost) Implements IPravaObjektove.Cinnosti

        Return session.Query(Of Cinnost).OrderBy(Function(x) x.Nazov).ToList()
    End Function

    Public Function TypyObjektov(session As ISession) As IList(Of ObjektTyp) Implements IPravaObjektove.TypyObjektov

        Return session.Query(Of ObjektTyp).OrderBy(Function(x) x.Nazov).ToList()
    End Function

#Region "Pouzivatelia"

    Public MustOverride Function PouzivateliaPre(pravo As IPravoObjektove, session As ISession) As IList(Of IPouzivatel) Implements IPravaObjektove.PouzivateliaPre

    Public MustOverride Sub PouzivatelOdober(pravo As IPravoObjektove, pouzivatel As IPouzivatel, session As ISession) Implements IPravaObjektove.PouzivatelOdober

    Public MustOverride Sub PouzivatelPridaj(pravo As IPravoObjektove, pouzivatel As IPouzivatel, session As ISession) Implements IPravaObjektove.PouzivatelPridaj

#End Region

#Region "Role"

    Public MustOverride Function RolePre(pravo As IPravoObjektove, session As ISession) As IList(Of IRola) Implements IPravaObjektove.RolePre

    Public MustOverride Sub RolaOdober(pravo As IPravoObjektove, rola As IRola, session As ISession) Implements IPravaObjektove.RolaOdober

    Public MustOverride Sub RolaPridaj(pravo As IPravoObjektove, rola As IRola, session As ISession) Implements IPravaObjektove.RolaPridaj

#End Region

#End Region

End Class
