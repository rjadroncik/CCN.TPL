Imports NHibernate
Imports CCN.Model

Public Class LogDB
    Inherits Service

#Region "Vynimky - protected"

    Protected Shared Sub NaplnVynimku(log As LogVynimka, vynimka As Exception)

        log.Typ = vynimka.GetType().Namespace & "." & vynimka.GetType().Name
        log.Sprava = vynimka.Message
        log.AkoText = vynimka.ToString()
        log.Zdroj = vynimka.Source
        log.StackTrace = vynimka.StackTrace

        log.Trieda = If((vynimka.TargetSite IsNot Nothing) AndAlso _
                        (vynimka.TargetSite.DeclaringType IsNot Nothing), vynimka.TargetSite.DeclaringType.Namespace & "." _
                                                                        & vynimka.TargetSite.DeclaringType.Name, Nothing)

        log.Metoda = If(vynimka.TargetSite IsNot Nothing, vynimka.TargetSite.Name, Nothing)

        For Each key As Object In vynimka.Data.Keys

            Dim logUdaj As LogVynimkaUdaj = LogVynimkaUdaj.Create()

            logUdaj.Kluc = key.ToString()
            logUdaj.Hodnota = vynimka.Data(key).ToString()

            logUdaj.Vynimka = log
            log.Udaje.Add(logUdaj)
        Next

        If (vynimka.InnerException IsNot Nothing) Then

            log.Vnutorna = LogVynimka.Create()
            NaplnVynimku(log.Vnutorna, vynimka.InnerException)
        End If
    End Sub

    Protected Shared Sub UlozVynimku(vynimka As LogVynimka, session As ISession)

        If (vynimka.Vnutorna IsNot Nothing) Then UlozVynimku(vynimka.Vnutorna, session)

        session.Save(vynimka)
    End Sub

#End Region

#Region "Chyby"

    Public Shared Function ZalogujChybu(vynimka As Exception, Optional stackLevel As Integer = 1) As LogChyba

        Dim chyba As LogChyba = LogChyba.Create(Now)

        Try
            Dim trace As New StackTrace(True)
            Dim frame As StackFrame = trace.GetFrame(stackLevel)

            chyba.Aplikacia = Globals.Aplikacia(Globals.AplikaciaBeziaca)
            chyba.Verzia = Globals.Verzia(Globals.AplikaciaBeziaca)

            chyba.Entita = Globals.Entita
            chyba.Pouzivatel = Globals.Pouzivatel

            chyba.Trieda = frame.GetMethod.ReflectedType.Namespace & "." & frame.GetMethod.ReflectedType.Name
            chyba.Metoda = frame.GetMethod().Name

            chyba.Vynimka = LogVynimka.Create()
            NaplnVynimku(chyba.Vynimka, vynimka)

        Catch ex As Exception

            LogSubor.StartError()
            LogSubor.WriteLine("Nebolo možné zozbierať všetky informácie o výnimke.")
            LogSubor.WriteException(ex)
            LogSubor.Close()
        End Try

        Try
            Using session As ISession = NewSession(), transaction = session.BeginTransaction()

                UlozVynimku(chyba.Vynimka, session)
                session.Save(chyba)

                transaction.Commit()
            End Using

        Catch ex As Exception

            LogSubor.StartError()
            LogSubor.WriteLine("Nebolo možné zapísať informácie o výnimke do databázy.")

            LogSubor.WriteLine("Aplikacia: " & If(chyba.Aplikacia IsNot Nothing, chyba.Aplikacia, ""))
            LogSubor.WriteLine("Verzia: " & If(chyba.Verzia IsNot Nothing, chyba.Verzia.ToString(), ""))

            LogSubor.WriteLine("Entita: " & If(chyba.Entita IsNot Nothing, chyba.Entita, ""))
            LogSubor.WriteLine("Pouzivatel: " & If(chyba.Pouzivatel IsNot Nothing, chyba.Pouzivatel.Login, ""))

            LogSubor.WriteLine("Trieda: " & If(chyba.Trieda IsNot Nothing, chyba.Trieda, ""))
            LogSubor.WriteLine("Metoda: " & If(chyba.Metoda IsNot Nothing, chyba.Metoda, ""))

            LogSubor.WriteException(ex)
            LogSubor.Close()
        End Try

        Return chyba
    End Function

#End Region

End Class
