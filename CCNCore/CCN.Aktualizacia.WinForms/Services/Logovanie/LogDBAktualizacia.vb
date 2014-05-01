Imports CCN.Services
Imports CCN.Model
Imports CCN.Aktualizacia.Model
Imports NHibernate
Imports NHibernate.Linq

Public Class LogDBAktualizacia
    Inherits LogDB

#Region "Private"

    Protected Shared Sub ZalogujChybuDoSuboru(ex As Exception)

        LogSubor.StartError()
        LogSubor.WriteLine("Nebolo možné zapísať informácie o aktualizácii do databázy.")
        LogSubor.WriteException(ex)
        LogSubor.Close()
    End Sub

    Protected Shared Sub NaplnLog(log As Log)

        With log
            .Aplikacia = Globals.Aplikacia(Globals.AplikaciaBeziaca)
            .Verzia = Globals.Verzia(Globals.AplikaciaBeziaca)
            .Entita = Globals.Entita
            .Pouzivatel = Globals.Pouzivatel
        End With
    End Sub

#End Region

#Region "Aktualizacia"

    Public Shared Function ZalogujAktualizaciuStart(release As Release, verziaStara As Version) As LogAktualizacia

        Try
            Using session As ISession = NewSession(), transaction = session.BeginTransaction()

                Dim log As LogAktualizacia = LogAktualizacia.Create(Now)
                With log
                    NaplnLog(log)

                    .VerziaStara = verziaStara
                    .VerziaNova = release.Version
                    .Komponent = release.Komponent.ToString()
                End With

                session.Save(log)
                transaction.Commit()

                Return log
            End Using
        Catch ex As Exception

            ZalogujChybuDoSuboru(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Sub ZalogujAktualizaciuKoniec(aktualizacia As LogAktualizacia)

        Try
            Using session As ISession = NewSession(), transaction = session.BeginTransaction()

                Dim log = session.Query(Of LogAktualizacia).Where(Function(x) x.Id = aktualizacia.Id).Single()
                log.Finished = Now

                session.Update(log)
                transaction.Commit()
            End Using
        Catch ex As Exception

            ZalogujChybuDoSuboru(ex)
        End Try
    End Sub

#End Region

#Region "Operacie"

    Public Shared Function ZalogujOperaciuProgramStart(operacia As OperationProgram, logAktualizacia As LogAktualizacia) As LogAktualizaciaOperaciaProgram

        Try
            Using session As ISession = NewSession(), transaction = session.BeginTransaction()

                Dim log = LogAktualizaciaOperaciaProgram.Create(Now, operacia.Action)
                With log
                    NaplnLog(log)

                    .Aktualizacia = logAktualizacia

                    .Path = operacia.Path
                    .Process = operacia.Process
                    .Arguments = operacia.Arguments
                End With

                session.Save(log)
                transaction.Commit()

                Return log
            End Using
        Catch ex As Exception

            ZalogujChybuDoSuboru(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function ZalogujOperaciuSuborStart(operacia As OperationFile, logAktualizacia As LogAktualizacia, verziaStara As Version) As LogAktualizaciaOperaciaSubor

        Try
            Using session As ISession = NewSession(), transaction = session.BeginTransaction()

                Dim log = LogAktualizaciaOperaciaSubor.Create(Now, operacia.Action)
                With log
                    NaplnLog(log)

                    .Aktualizacia = logAktualizacia
                    .Path = operacia.Path

                    .VerziaStara = verziaStara
                    .VerziaNova = operacia.Version
                End With

                session.Save(log)
                transaction.Commit()

                Return log
            End Using
        Catch ex As Exception

            ZalogujChybuDoSuboru(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Sub ZalogujOperaciuKoniec(Of T As LogAktualizaciaOperacia)(operacia As LogAktualizaciaOperacia)

        Try
            Using session As ISession = NewSession(), transaction = session.BeginTransaction()

                Dim log = session.Query(Of T).Where(Function(x) x.Id = operacia.Id).Single()
                log.Finished = Now

                session.Update(log)
                transaction.Commit()
            End Using
        Catch ex As Exception

            ZalogujChybuDoSuboru(ex)
        End Try
    End Sub

#End Region

End Class
