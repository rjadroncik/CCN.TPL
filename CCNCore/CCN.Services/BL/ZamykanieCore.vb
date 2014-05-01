Imports NHibernate
Imports NHibernate.Linq
Imports NHibernate.Exceptions
Imports System.Data.SqlClient
Imports CCN.Model

Public Class ZamykanieCore(Of TLock As LockBase(Of Integer), TPouzivatel As IPouzivatel, TLockable As IIdentifiable(Of Integer))
    Inherits Service

#Region "Properties"

    Public Shared ReadOnly Timeout As Integer = 10

#End Region

#Region "General"

    Public Shared Function OdomkniZdroj(zdroj As TLockable) As Boolean

        Using session As ISession = NewSession(), transaction = session.BeginTransaction()

            OdomkniZdroj(session, zdroj)

            transaction.Commit()
        End Using

        Return True
    End Function

    Public Shared Sub OdomkniZdroj(session As ISession, zdroj As TLockable)

        For Each zamka In session.Query(Of TLock).Where(Function(x) (x.Zdroj.Id = zdroj.Id) AndAlso _
                                                                    (x.Pouzivatel Is Globals.Pouzivatel))

            session.Delete(zamka)
        Next
    End Sub

    Public Shared Sub OdomkniVyprsane(session As ISession)

        For Each zamka In session.Query(Of TLock).Where(Function(x) x.Expires < Now)

            session.Delete(zamka)
        Next
    End Sub

    Public Shared Sub OdomkniVyprsane()

        Using session As ISession = NewSession()
            Using transaction As ITransaction = session.BeginTransaction()

                OdomkniVyprsane(session)

                transaction.Commit()
            End Using
        End Using
    End Sub

    Public Shared Sub Predlz(session As ISession, pouzivatel As IPouzivatel)

        For Each zamka In session.Query(Of TLock).Where(Function(x) x.Pouzivatel.Id = pouzivatel.Id)

            zamka.Expires = Now.AddMinutes(Timeout)
            session.Update(zamka)
        Next
    End Sub

    Public Shared Sub Predlz(pouzivatel As IPouzivatel)

        Using session As ISession = NewSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Predlz(session, pouzivatel)

                transaction.Commit()
            End Using
        End Using
    End Sub

#End Region

#Region "Internal"

    Protected Shared Function IsPrimaryKeyViolationException(e As Exception) As Boolean

        Return (e.InnerException IsNot Nothing) AndAlso (DirectCast(e.InnerException, SqlException).Number = 2627)
    End Function

    Protected Shared Function Zamkni(Of TLocked As TLockable)(objekty As IEnumerable(Of TLocked), zdroj As TLockable,
                                                              action As Action(Of ISession, TLocked, TLockable)) As Boolean
        Try
            Using session As ISession = NewSession()
                Using transaction As ITransaction = session.BeginTransaction()

                    For Each objekt As TLocked In objekty

                        action(session, objekt, zdroj)
                    Next

                    transaction.Commit()
                End Using
            End Using

        Catch e As GenericADOException

            If (IsPrimaryKeyViolationException(e)) Then Return False
            Throw e
        End Try

        Return True
    End Function

    Protected Shared Function Zamkni(Of TLocked As TLockable)(objekt As TLocked, zdroj As TLockable,
                                                              action As Action(Of ISession, TLocked, TLockable)) As Boolean
        Try
            Using session As ISession = NewSession()
                Using transaction As ITransaction = session.BeginTransaction()

                    action(session, objekt, If(zdroj Is Nothing, objekt, zdroj))

                    transaction.Commit()
                End Using
            End Using

        Catch e As GenericADOException

            If (IsPrimaryKeyViolationException(e)) Then Return False
            Throw e
        End Try

        Return True
    End Function

#End Region

#Region "Pouzivatelia"

    Public Shared Function PouzivatelZamykajuci(objekt As TLockable, session As ISession) As TPouzivatel

        Return session.Query(Of TLock).Where(Function(x) x.Objekt.Id = objekt.Id) _
                                      .Select(Function(x) DirectCast(x.Pouzivatel, TPouzivatel)).Cast(Of TPouzivatel).FirstOrDefault()
    End Function

    Public Shared Function PouzivateliaZamykajuci(objekty As IEnumerable(Of TLockable), session As ISession) As IEnumerable(Of TPouzivatel)

        Dim objektyIDs As IEnumerable(Of Integer) = objekty.Select(Of Integer)(Function(x) x.Id).ToList()

        Return session.Query(Of TLock).Where(Function(x) objektyIDs.Contains(x.Objekt.Id)) _
                                      .Select(Function(x) x.Pouzivatel).Cast(Of TPouzivatel).Distinct().ToList()
    End Function

#End Region

End Class
