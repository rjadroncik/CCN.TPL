Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services
Imports CCN.Core.VB
Imports CCN.Aktualizacia.Model

Public MustInherit Class NastaveniaAktualizacia
    Inherits Nastavenia

#Region "Properties"

    Public Shared ReadOnly Property UpdateFtpServer As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateFtpServer), String)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateFtpLogin As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateFtpLogin), String)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateFtpPassword As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateFtpPassword), String)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateFtpPath As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateFtpPath), String)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateProxyEnabled As Boolean
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateProxyEnabled), Boolean)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateProxyServer As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateProxyServer), String)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateProxyPort As Integer
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateProxyPort), Integer)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateProxyLogin As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateProxyLogin), String)
        End Get
    End Property

    Public Shared ReadOnly Property UpdateProxyPassword As String
        Get
            Return DirectCast(PodlaId(IdNastaveniaUpdate.UpdateProxyPassword), String)
        End Get
    End Property

#End Region

#Region "Default hodnoty"

    Public Shared Function DefaultPreIdAktualizacia(id As IdNastavenia, pouzivatel As IPouzivatel, entita As String, session As ISession) As NastavenieHodnota

        Try
            Return DefaultPreIdCore(id, pouzivatel, entita, session)

        Catch ex As NastavenieNenamapovaneException

            Dim skupinaGlobalne = SkupinaGlobalneNacitaj(session)
            Dim skupinaLokalne = SkupinaLokalneNacitaj(session)

            Dim skupinaEntita = SkupinaEntitaNacitajAleboVytvor(entita, skupinaLokalne, session)

            Dim hodnota As NastavenieHodnota = NastavenieHodnota.Create()
            With hodnota
                Select Case (id)

                    Case IdNastaveniaUpdate.UpdateFtpServer
                        .Entita = Globalne
                        .Hodnota = "ftp.server.com"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateFtpLogin
                        .Entita = Globalne
                        .Hodnota = "login"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateFtpPassword
                        .Entita = Globalne
                        .Hodnota = "password"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateFtpPath
                        .Entita = Globalne
                        .Hodnota = "/data/update/"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateProxyEnabled
                        .Entita = Globalne
                        .Hodnota = "False"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateProxyServer
                        .Entita = Globalne
                        .Hodnota = "proxy.server.com"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateProxyPort
                        .Entita = Globalne
                        .Hodnota = "8080"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateProxyLogin
                        .Entita = Globalne
                        .Hodnota = "login"
                        .Skupina = skupinaGlobalne

                    Case IdNastaveniaUpdate.UpdateProxyPassword
                        .Entita = Globalne
                        .Hodnota = "password"
                        .Skupina = skupinaGlobalne

                    Case Else
                        Throw New NastavenieNenamapovaneException(id.Id)
                End Select
            End With

            hodnota.Nastavenie = session.Query(Of Nastavenie).Where(Function(x) x.Id = id).Single()

            session.Save(hodnota)
            session.Update(skupinaLokalne)

            Return hodnota
        End Try
    End Function

#End Region

#Region "Initialization"

    Public Overloads Shared Sub InicializujAktualizacia()

        InicializujCore()

        Nacitaj(IdNastaveniaUpdate.UpdateFtpServer, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateFtpLogin, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateFtpPassword, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateFtpPath, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateProxyEnabled, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateProxyServer, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateProxyPort, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateProxyLogin, Globals.Pouzivatel, Globals.Entita)
        Nacitaj(IdNastaveniaUpdate.UpdateProxyPassword, Globals.Pouzivatel, Globals.Entita)
    End Sub

#End Region

End Class
