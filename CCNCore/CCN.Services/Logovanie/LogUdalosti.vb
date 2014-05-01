Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model

Public Class LogUdalosti
    Inherits Service

    Protected Shared Sub Zaloguj(log As LogUdalost)

        log.Aplikacia = Globals.Aplikacia(Globals.AplikaciaBeziaca)
        log.Verzia = Globals.Verzia(Globals.AplikaciaBeziaca)
        log.Entita = Globals.Entita

        If (Not Initialized) Then Throw New FatalServiceException("Nie je možné logovať do DB pokým nie je inicializovaný NHibernate!")

        Using session As ISession = NewSession(), transaction As ITransaction = session.BeginTransaction()

            session.Save(log)
            transaction.Commit()
        End Using
    End Sub

    Public Shared Sub Prihlasenie()

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.PouzivatelPrihlasenie
        log.Pouzivatel = Globals.Pouzivatel

        Zaloguj(log)
    End Sub

    Public Shared Sub PrihlasenieNeuspesne(Optional pouzivatel As IPouzivatel = Nothing)

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.PouzivatelPrihlasenieNeuspesne
        log.Pouzivatel = pouzivatel

        Zaloguj(log)
    End Sub

    Public Shared Sub ZmenaHesla(Optional pouzivatel As IPouzivatel = Nothing)

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.PouzivatelZmenaHesla
        log.Pouzivatel = pouzivatel

        Zaloguj(log)
    End Sub

    Public Shared Sub ZmenaHeslaNeuspesna(Optional pouzivatel As IPouzivatel = Nothing)

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.PouzivatelZmenaHeslaNeuspesna
        log.Pouzivatel = pouzivatel

        Zaloguj(log)
    End Sub

    Public Shared Sub Odhlasenie()

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.PouzivatelOdhlasenie
        log.Pouzivatel = Globals.Pouzivatel

        Zaloguj(log)
    End Sub

    Public Shared Sub AplikaciaStart()

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.AplikaciaStart

        Zaloguj(log)
    End Sub

    Public Shared Sub AplikaciaBezi()

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.AplikaciaBezi
        log.Pouzivatel = Globals.Pouzivatel

        Zaloguj(log)
    End Sub

    Public Shared Sub AplikaciaUkoncenie()

        Dim log As LogUdalost = LogUdalost.Create(Now)

        log.Typ = TypUdalosti.AplikaciaUkoncenie
        log.Pouzivatel = Globals.Pouzivatel

        Zaloguj(log)
    End Sub

End Class
