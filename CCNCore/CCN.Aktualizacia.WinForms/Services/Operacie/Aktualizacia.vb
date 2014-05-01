Imports CCN.Services
Imports CCN.Core.VB
Imports System.IO
Imports CCN.Model
Imports CCN.Aktualizacia.Model
Imports CCN.UI.WinForms

Public Class Aktualizacia
    Inherits Service

#Region "Properties"

    Public Shared Property AdresarAplikacia As String

    <Derived()>
    Public Shared ReadOnly Property AdresarRelease As String
        Get
            Return AdresarAplikacia & Path.DirectorySeparatorChar & "Release"
        End Get
    End Property

    <Derived()>
    Public Shared ReadOnly Property AdresarBackup As String
        Get
            Return AdresarAplikacia & Path.DirectorySeparatorChar & "Backup"
        End Get
    End Property

    <Derived()>
    Public Shared ReadOnly Property AdresarUpdater As String
        Get
            Return AdresarAplikacia & Path.DirectorySeparatorChar & "Updater"
        End Get
    End Property

    Protected Shared _releasePrefixes As New Dictionary(Of ComponentType, String)
    Public Shared Property ReleasePrefix(typ As ComponentType) As String
        Get
            Return If(_releasePrefixes.ContainsKey(typ), _releasePrefixes(typ), Nothing)
        End Get
        Set(value As String)
            _releasePrefixes.Add(typ, value)
        End Set
    End Property

    Public Shared ReadOnly ReleaseExtensionUpdate As String = "update"
    Public Shared ReadOnly ReleaseExtensionVersion As String = "version2"

    <Derived()>
    Public Shared ReadOnly Property ReleaseFileUpdate(release As Release) As String
        Get
            Return ReleasePrefix(release.Komponent) & release.Version.ToStringFile() & "." & ReleaseExtensionUpdate
        End Get
    End Property

    <Derived()>
    Public Shared ReadOnly Property ReleaseFilePath(release As Release) As String
        Get
            Return Aktualizacia.AdresarRelease & Path.DirectorySeparatorChar & ReleaseFileUpdate(release)
        End Get
    End Property

#End Region

#Region "BL"

    Public Shared Function Vykonaj(onMessage As SubMessage) As Boolean

        Try
            Dim provider As New ReleaseProvider()

            onMessage("Vyhľadávam aktualizácie")
            Dim release As Release = provider.ReleaseFind(True)
            If (release IsNot Nothing) Then

                Dim aktualizacia = LogDBAktualizacia.ZalogujAktualizaciuStart(release, Globals.Verzia(release.Komponent))

                onMessage("Sťahujem aktualizáciu")
                provider.ReleaseDownload(release)

                Dim operacie As IEnumerable(Of Operation)

                Select Case Globals.AplikaciaBeziaca

                    Case ComponentType.Application
                        operacie = release.OperationsAplikacia
                    Case ComponentType.Updater
                        operacie = release.OperationsUpdater
                    Case Else
                        Throw New InvalidCastException("Nepodporovaný typ aplikácie")
                End Select

                onMessage(String.Format("Prebieha aktualizácia na verziu: {0}", release.Version))
                Dim result As OperationResult = OperacieVykonaj(release, operacie, aktualizacia)
                If (result.Success) Then

                    onMessage("Aktualizácia úspešná")
                    LogDBAktualizacia.ZalogujAktualizaciuKoniec(aktualizacia)
                    Return _exitAfterUpdate
                Else
                    Throw New FatalServiceException(result.Message, result.Exception)
                End If
            End If

            Return False

        Catch ex As Exception

            If (TypeOf ex Is FatalServiceException) Then

                Upozornenia.Chyba(DirectCast(ex, FatalServiceException), "Chyba pri aktualizácii")
            Else
                Try
                    Throw New FatalServiceException("Došlo k nečakanej chybe pri aktualizácii, kontktujte servis.", ex)

                Catch ex2 As FatalServiceException

                    Upozornenia.Chyba(ex2, "Chyba pri aktualizácii")
                End Try
            End If
        End Try

        Return _exitAfterUpdate
    End Function

    Protected Shared _exitAfterUpdate As Boolean = False

    Protected Shared Function BackupDirectory(release As Release) As String

        Return Aktualizacia.AdresarBackup & System.IO.Path.DirectorySeparatorChar & release.Version.ToStringFile()
    End Function

    Protected Shared Sub ZmazZalohu()

        For Each filename As String In Directory.GetFiles(AdresarBackup)
            Try
                If (filename.ToLower().EndsWith("." & Aktualizacia.ReleaseExtensionUpdate) OrElse _
                    filename.ToLower().EndsWith(".backup") OrElse _
                    filename.ToLower().EndsWith("." & Aktualizacia.ReleaseExtensionVersion)) Then

                    File.Delete(filename)
                End If
            Catch
            End Try
        Next
    End Sub

    Protected Shared Function OperacieVykonaj(release As Release, operacie As IEnumerable(Of Operation), logAktualizacia As LogAktualizacia) As OperationResult

        Dim vykonane As New List(Of Operation)()

        For Each operation As Operation In operacie

            Dim result As OperationResult

            If (operation.GetType() Is GetType(OperationFile)) Then

                result = AktualizaciaOperaciaSubor.Perform(DirectCast(operation, OperationFile), release, logAktualizacia)

            ElseIf (operation.GetType() Is GetType(OperationProgram)) Then

                result = AktualizaciaOperaciaProgram.Perform(DirectCast(operation, OperationProgram), release, logAktualizacia)

            ElseIf (operation.GetType() Is GetType(OperationTask)) Then

                result = AktualizaciaOperaciaTask.Perform(DirectCast(operation, OperationTask), release, logAktualizacia)
            Else
                Throw New InvalidOperationException("Nepodporovaná aktualizačná operácia")
            End If

            If (Not result.Success) Then

                OperacieRevert(release, vykonane, logAktualizacia)
                Return result
            End If

            vykonane.Insert(0, operation)
        Next

        Return New OperationResult(True)
    End Function

    Protected Shared Function OperacieRevert(release As Release, operacie As IEnumerable(Of Operation), logAktualizacia As LogAktualizacia) As OperationResult

        For Each operation As Operation In operacie

            Dim result As OperationResult

            If (operation.GetType() Is GetType(OperationFile)) Then

                result = AktualizaciaOperaciaSubor.Perform(DirectCast(operation, OperationFile), release, logAktualizacia)

            ElseIf (operation.GetType() Is GetType(OperationProgram)) Then

                result = New OperationResult(True)

            ElseIf (operation.GetType() Is GetType(OperationTask)) Then

                result = New OperationResult(True)
            Else
                Throw New InvalidOperationException("Nepodporovaná aktualizačná operácia")
            End If

            If (Not result.Success) Then Return result
        Next

        Return New OperationResult(True)
    End Function

#End Region

End Class
