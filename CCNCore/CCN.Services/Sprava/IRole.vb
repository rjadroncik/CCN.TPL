Imports CCN.Model
Imports NHibernate

Public Interface IRole

#Region "Vseobecne"

    Function Create() As IRola

    Sub Zmaz(rola As IRola, session As ISession)

    Sub UlozZmeny(rola As IRola, session As ISession)

    Function Vsetky(session As ISession) As IList(Of IRola)

#End Region

#Region "Pouzivatelia"

    Function PouzivateliaPre(rola As IRola, session As ISession) As IList(Of IPouzivatel)

    Sub PouzivatelPridaj(rola As IRola, pouzivatel As IPouzivatel, session As ISession)
    Sub PouzivatelOdober(rola As IRola, pouzivatel As IPouzivatel, session As ISession)

#End Region

End Interface
