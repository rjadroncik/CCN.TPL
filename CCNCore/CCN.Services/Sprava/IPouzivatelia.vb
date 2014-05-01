Imports CCN.Model
Imports NHibernate

Public Interface IPouzivatelia

#Region "Vseobecne"

    Function Create() As IPouzivatel

    Sub UlozZmeny(pouzivatel As IPouzivatel, session As ISession)

    Function Vsetci(session As ISession) As IList(Of IPouzivatel)

#End Region

End Interface
