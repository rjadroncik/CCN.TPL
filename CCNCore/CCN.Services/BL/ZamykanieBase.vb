Imports NHibernate
Imports NHibernate.Linq
Imports NHibernate.Exceptions
Imports System.Data.SqlClient
Imports CCN.Model

Public Class ZamykanieBase(Of TID, TLock As LockBase(Of TID), TPouzivatel As IPouzivatel, TLockable As IIdentifiable(Of TID))
    Inherits Service

#Region "Properties"

    Public Shared ReadOnly Timeout As Integer = 10

#End Region

#Region "General"

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

#End Region

#Region "Internal"

    Protected Shared Function IsPrimaryKeyViolationException(e As Exception) As Boolean

        Return (e.InnerException IsNot Nothing) AndAlso (DirectCast(e.InnerException, SqlException).Number = 2627)
    End Function

    Protected Shared Function Zamkni(Of TLocked As TLockable)(TLockabley As IEnumerable(Of TLocked), zdroj As TLockable,
                                                              action As Action(Of ISession, TLocked, TLockable)) As Boolean
        Try
            Using session As ISession = NewSession()
                Using transaction As ITransaction = session.BeginTransaction()

                    For Each TLockable As TLocked In TLockabley

                        action(session, TLockable, zdroj)
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

    Protected Shared Function Zamkni(Of TLocked As TLockable)(TLockable As TLocked, zdroj As TLockable,
                                                              action As Action(Of ISession, TLocked, TLockable)) As Boolean
        Try
            Using session As ISession = NewSession()
                Using transaction As ITransaction = session.BeginTransaction()

                    action(session, TLockable, If(zdroj Is Nothing, TLockable, zdroj))

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

End Class
