Imports System.IO
Imports CCN.Network.FTP
Imports CCN.Services
Imports CCN.Core.VB
Imports CCN.Aktualizacia.Model
Imports CCN.Model

Public Class ReleaseProvider

#Region "Initialization"

    Private Function CreateFtpClient() As FtpClient

        Dim ftp As New FtpClient(NastaveniaAktualizacia.UpdateFtpServer, NastaveniaAktualizacia.UpdateFtpLogin, NastaveniaAktualizacia.UpdateFtpPassword)
        With ftp
            .ProxyEnabled = NastaveniaAktualizacia.UpdateProxyEnabled
            .ProxyHostName = NastaveniaAktualizacia.UpdateProxyServer
            .ProxyPort = NastaveniaAktualizacia.UpdateProxyPort
            .ProxyUserName = NastaveniaAktualizacia.UpdateProxyLogin
            .ProxyPassword = NastaveniaAktualizacia.UpdateProxyPassword
        End With
        Return ftp
    End Function

#End Region

#Region "BL - finding"

    Public Function ReleaseFind(checkFtp As Boolean) As Release

        If (checkFtp) Then

            Dim result As Release = CheckForUpdateOnFtp()
            If (result IsNot Nothing) Then Return result
        End If

        Return CheckForUpdateLocally()
    End Function

    Private Function CheckForUpdateLocally() As Release
        Try
            If (Not Directory.Exists(Aktualizacia.AdresarRelease)) Then Directory.CreateDirectory(Aktualizacia.AdresarRelease)

            For Each file As String In SelectViableFiles(Directory.GetFiles(Aktualizacia.AdresarRelease, "*." & Aktualizacia.ReleaseExtensionVersion).Select(Function(x) Path.GetFileName(x)))

                Dim release As Release = XmlRelease.Read(Aktualizacia.AdresarRelease & Path.DirectorySeparatorChar & file)

                If (MatchesEntities(release)) Then Return release
            Next

            Return Nothing

        Catch ex As Exception

            Throw New FatalServiceException("Pozor, nepodarilo sa vyhľadať aktualizčné súbory na disku. Kontaktujte servis.", ex)
        End Try
    End Function

    Private Function CheckForUpdateOnFtp() As Release
        Try
            If (Not Directory.Exists(Aktualizacia.AdresarRelease)) Then Directory.CreateDirectory(Aktualizacia.AdresarRelease)

            Dim ftpClient As FtpClient = CreateFtpClient()
            Dim ftpDir As FtpDirectory = ftpClient.DirectoryRead(NastaveniaAktualizacia.UpdateFtpPath)

            For Each file As String In SelectViableFiles(ftpDir.Files.Where(Function(x) x.Extension = Aktualizacia.ReleaseExtensionVersion).Select(Function(x) x.Name))

                Dim targetFile As String = Aktualizacia.AdresarRelease & "\" & file
                ftpClient.Download(NastaveniaAktualizacia.UpdateFtpPath & file, targetFile, True)

                Dim release As Release = XmlRelease.Read(targetFile)

                If (MatchesEntities(release)) Then Return release
            Next

            Return Nothing

        Catch ex As Exception

            Throw New FatalServiceException("Pozor, nepodarilo sa spojiť s aktualizačným serverom. Kontaktujte servis.", ex)
        End Try
    End Function

    Private Function SelectViableFiles(files As IEnumerable(Of String)) As IEnumerable(Of String)

        Dim result As New List(Of KeyValuePair(Of Version, String))

        For Each file As String In files

            For Each typ As ComponentType In [Enum].GetValues(GetType(ComponentType))

                If (file.Contains(Aktualizacia.ReleasePrefix(typ))) Then

                    Dim version As Version = Converting.String2Version(file.Substring(Aktualizacia.ReleasePrefix(typ).Length).Replace("_", "."))

                    If (version > Globals.Verzia(typ)) Then result.Add(New KeyValuePair(Of Version, String)(version, file))
                End If
            Next
        Next

        Return result.OrderBy(Function(x) x.Key.Revision).Select(Function(x) x.Value)
    End Function

    Private Function MatchesEntities(release As Release) As Boolean

        With release
            If (.EntitiesAll) Then Return True

            For Each entita As String In .Entities

                If (entita.ToLower() = Globals.Entita.ToLower()) Then Return True
            Next
        End With

        Return False
    End Function

#End Region

#Region "BL - downloading"

    Public Sub ReleaseDownload(release As Release)

        Try
            Dim ftpClient As FtpClient = CreateFtpClient()

            ftpClient.Download(NastaveniaAktualizacia.UpdateFtpPath & Aktualizacia.ReleaseFileUpdate(release), Aktualizacia.ReleaseFilePath(release), True)

        Catch ex As Exception

            Throw New FatalServiceException("Pozor, nepodarilo sa stiahnúť aktualizáciu programu. Kontaktujte servis.", ex)
        End Try
    End Sub

#End Region

End Class
