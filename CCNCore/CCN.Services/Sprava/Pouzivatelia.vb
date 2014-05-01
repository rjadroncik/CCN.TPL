Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public MustInherit Class Pouzivatelia(Of T As IPouzivatel)
    Inherits Service
    Implements IPouzivatelia

#Region "IPouzivatelia"

    Public MustOverride Function Create() As IPouzivatel Implements IPouzivatelia.Create

    Public MustOverride Sub UlozZmeny(pouzivatel As IPouzivatel, session As ISession) Implements IPouzivatelia.UlozZmeny

    Public MustOverride Function Vsetci(session As ISession) As IList(Of IPouzivatel) Implements IPouzivatelia.Vsetci

#End Region

#Region "Public"

    Public Shared Function ZmenHeslo(login As String, hesloStare As String, hesloNove As String) As T

        Using session As ISession = NewSession()

            Dim pouzivatel As T = session.Query(Of T).Where(Function(x) x.Login.Equals(login)).SingleOrDefault()
            If ((pouzivatel Is Nothing) OrElse (hesloStare <> hesloNove)) Then

                LogUdalosti.ZmenaHeslaNeuspesna()
            Else
                If (pouzivatel.Heslo = hesloStare) Then

                    ZmenHeslo(pouzivatel, hesloNove, session)
                    Return pouzivatel
                Else
                    LogUdalosti.ZmenaHeslaNeuspesna(pouzivatel)
                End If
            End If
        End Using

        Return Nothing
    End Function

    Public Shared Sub ZmenHeslo(login As String, hesloNove As String)

        Using session As ISession = NewSession()

            Dim pouzivatel As T = session.Query(Of T).Where(Function(x) x.Login.Equals(login)).SingleOrDefault()

            ZmenHeslo(pouzivatel, hesloNove, session)
        End Using
    End Sub

#End Region

#Region "Protected"

    Protected Shared Sub ZmenHeslo(pouzivatel As T, hesloNove As String, session As ISession)

        Using transaction = session.BeginTransaction()

            pouzivatel.Heslo = hesloNove
            session.Update(pouzivatel)

            transaction.Commit()
        End Using

        LogUdalosti.ZmenaHesla()
    End Sub

    Protected Shared Sub Prihlas(pouzivatel As T, entita As String)

        Globals.PouzivatelPrilas(pouzivatel, entita)

        Dim haluz As Integer = pouzivatel.Prava.Count
    End Sub

#End Region

End Class
