Imports CCN.Model
Imports NHibernate

Public Interface IPravaObjektove

#Region "Vseobecne"

    Function Create() As IPravoObjektove

    Sub Zmaz(pravo As IPravoObjektove, session As ISession)

    Sub UlozZmeny(pravo As IPravoObjektove, session As ISession)

    Function Vsetky(session As ISession) As IList(Of IPravoObjektove)

#End Region

#Region "Ciselniky"

    Function Cinnosti(session As ISession) As IList(Of Cinnost)

    Function TypyObjektov(session As ISession) As IList(Of ObjektTyp)

#End Region

#Region "Pouzivatelia"

    Function PouzivateliaPre(pravo As IPravoObjektove, session As ISession) As IList(Of IPouzivatel)

    Sub PouzivatelPridaj(pravo As IPravoObjektove, pouzivatel As IPouzivatel, session As ISession)
    Sub PouzivatelOdober(pravo As IPravoObjektove, pouzivatel As IPouzivatel, session As ISession)

#End Region

#Region "Role"

    Function RolePre(pravo As IPravoObjektove, session As ISession) As IList(Of IRola)

    Sub RolaPridaj(pravo As IPravoObjektove, rola As IRola, session As ISession)
    Sub RolaOdober(pravo As IPravoObjektove, rola As IRola, session As ISession)

#End Region

End Interface
