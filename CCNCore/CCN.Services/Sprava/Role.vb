Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public MustInherit Class Role(Of T As IRola)
    Inherits Service
    Implements IRole

#Region "IRole"

    Public MustOverride Function Create() As IRola Implements IRole.Create

    Public MustOverride Sub Zmaz(rola As IRola, session As ISession) Implements IRole.Zmaz

    Public MustOverride Sub UlozZmeny(rola As IRola, session As ISession) Implements IRole.UlozZmeny

    Public Function Vsetky(session As ISession) As IList(Of IRola) Implements IRole.Vsetky

        Return session.Query(Of IRola).OrderBy(Function(x) x.Nazov).ToList()
    End Function

    Public MustOverride Function PouzivateliaPre(rola As IRola, session As ISession) As IList(Of IPouzivatel) Implements IRole.PouzivateliaPre

    Public MustOverride Sub PouzivatelOdober(rola As IRola, pouzivatel As IPouzivatel, session As ISession) Implements IRole.PouzivatelOdober

    Public MustOverride Sub PouzivatelPridaj(rola As IRola, pouzivatel As IPouzivatel, session As ISession) Implements IRole.PouzivatelPridaj

#End Region

End Class
