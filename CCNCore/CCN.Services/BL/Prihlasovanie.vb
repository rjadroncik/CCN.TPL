Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services

Public Class Prihlasovanie
    Inherits Service

    Public Shared Function Prihlas(Of T As IPouzivatel)(login As String, heslo As String, Optional entita As String = Nothing) As T

        Using session As ISession = NewSession()

            Dim pouzivatel As T = session.Query(Of T).Where(Function(x) x.Login.Equals(login)).SingleOrDefault()
            If (pouzivatel Is Nothing) Then

                LogUdalosti.PrihlasenieNeuspesne()
            Else

                If (OverHeslo(pouzivatel, heslo)) Then

                    Prihlas(pouzivatel, If(entita Is Nothing, Globals.Entita, entita))
                    LogUdalosti.Prihlasenie()

                    Return pouzivatel
                Else
                    LogUdalosti.PrihlasenieNeuspesne(pouzivatel)
                End If
            End If
        End Using

        Return Nothing
    End Function

    Protected Shared Function OverHeslo(pouzivatel As IPouzivatel, heslo As String) As Boolean

        Return (pouzivatel.Heslo Is Nothing) OrElse (pouzivatel.Heslo = heslo)
    End Function

    Protected Shared Sub Prihlas(pouzivatel As IPouzivatel, entita As String)

        Globals.PouzivatelPrilas(pouzivatel, entita)

        Dim haluz As Integer = pouzivatel.Prava.Count
    End Sub

End Class
