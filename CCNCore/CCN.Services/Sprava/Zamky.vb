Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Core.VB

Public Interface IZamky

    Function Vsetky(session As ISession) As IDictionary(Of IPouzivatel, IList(Of LockBase(Of Integer)))

    Sub ZmazZamku(zamka As LockBase(Of Integer))
    Sub ZmazZamky(pouzivatel As IPouzivatel)

    Sub OdomkniVyprsane()

End Interface

Public Class Zamky(Of T As LockBase(Of Integer))
    Implements IZamky

    Public Function Vsetky(session As ISession) As IDictionary(Of IPouzivatel, IList(Of LockBase(Of Integer))) Implements IZamky.Vsetky

        Return session.Query(Of LockBase(Of Integer)).ToDictionaryOfLists(Function(x) x.Pouzivatel)
    End Function

    Public Sub ZmazZamku(zamka As LockBase(Of Integer)) Implements IZamky.ZmazZamku

        Using session = Service.NewSession(),
            transaction = session.BeginTransaction()

            session.Delete(zamka)

            transaction.Commit()
        End Using
    End Sub

    Public Sub ZmazZamky(pouzivatel As IPouzivatel) Implements IZamky.ZmazZamky

        Dim zamky As IEnumerable(Of LockBase(Of Integer))

        Using session = Service.NewSession()

            zamky = session.Query(Of T).Where(Function(x) x.Pouzivatel.Login.Equals(pouzivatel.Login)).Cast(Of LockBase(Of Integer)).ToList()
        End Using

        For Each zamka In zamky

            ZmazZamku(zamka)
        Next
    End Sub

    Public Sub OdomkniVyprsane() Implements IZamky.OdomkniVyprsane

        ZamykanieCore(Of T, IPouzivatel, IIdentifiable(Of Integer)).OdomkniVyprsane()
    End Sub

End Class
