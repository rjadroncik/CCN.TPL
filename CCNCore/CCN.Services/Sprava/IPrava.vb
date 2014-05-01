Imports CCN.Model
Imports NHibernate

Public Interface IPrava

#Region "Vseobecne"

    Function Vsetky(session As ISession) As IList(Of IPravo)

#End Region

#Region "Pouzivatelia"

    Function PouzivateliaPre(pravo As IPravo, session As ISession) As IList(Of IPouzivatel)

    Sub PouzivatelPridaj(pravo As IPravo, pouzivatel As IPouzivatel, session As ISession)
    Sub PouzivatelOdober(pravo As IPravo, pouzivatel As IPouzivatel, session As ISession)

#End Region

#Region "Role"

    Function RolePre(pravo As IPravo, session As ISession) As IList(Of IRola)

    Sub RolaPridaj(pravo As IPravo, rola As IRola, session As ISession)
    Sub RolaOdober(pravo As IPravo, rola As IRola, session As ISession)

#End Region

End Interface
