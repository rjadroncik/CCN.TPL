Imports CCN.Services
Imports CCN.Aktualizacia.Model
Imports CCN.Model
Imports C1.C1Zip
Imports System.IO
Imports System.Reflection

Public Class AktualizaciaOperaciaSubor
    Inherits Aktualizacia

#Region "BL - private"

    Protected Shared Function BackupFile(operacia As OperationFile, release As Release) As String

        Return BackupDirectory(release) & System.IO.Path.DirectorySeparatorChar & System.IO.Path.GetFileName(operacia.Path)
    End Function

    Protected Shared Function TargetFile(operacia As OperationFile) As String

        Return Aktualizacia.AdresarAplikacia & System.IO.Path.DirectorySeparatorChar & operacia.Path
    End Function

    Protected Shared Function TargetDirectory(operacia As OperationFile) As String

        Return Path.GetDirectoryName(TargetFile(operacia))
    End Function

    Protected Shared Function Zalohuj(operacia As OperationFile, release As Release) As Boolean

        Dim path As String = BackupDirectory(release)

        Try
            If (Not Directory.Exists(path)) Then Directory.CreateDirectory(path)

            File.Copy(TargetFile(operacia), BackupFile(operacia, release), True)

        Catch ex As Exception

            LogDB.ZalogujChybu(ex)
            Return False
        End Try

        Return True
    End Function

#End Region

#Region "BL - public"

    Protected Shared Function VerziaStara(operacia As OperationFile, target As String) As Version

        If (New String() {".dll", ".exe"}.Contains(Path.GetExtension(target)) AndAlso _
            File.Exists(target) AndAlso _
            (operacia.Version IsNot Nothing)) Then Return AssemblyName.GetAssemblyName(target).Version

        Return Nothing
    End Function

    Protected Shared Function ZipPath(operacia As OperationFile) As String

        If (operacia.Path.Contains("\") OrElse operacia.Path.Contains("/")) Then

            Return "/" & operacia.Path.Replace("\", "/")
        Else
            Return operacia.Path
        End If
    End Function

    Public Shared Function Perform(operacia As OperationFile, release As Release, logAktualizacia As LogAktualizacia) As OperationResult

        Dim target As String = TargetFile(operacia)
        Dim targetDir As String = TargetDirectory(operacia)

        If (Not Directory.Exists(targetDir)) Then Directory.CreateDirectory(targetDir)

        Dim version As Version = If(operacia.Action <> FileAction.Add, VerziaStara(operacia, target), Nothing)

        Dim log = LogDBAktualizacia.ZalogujOperaciuSuborStart(operacia, logAktualizacia, version)

        Try
            Select Case (operacia.Action)

                Case FileAction.Add

                    If (File.Exists(target)) Then Return New OperationResult(False, "Duplicitný súbor na disku: " & target, MessageType.Error)

                    Using zip As New C1ZipFile(Aktualizacia.ReleaseFilePath(release))

                        zip.Entries(ZipPath(operacia)).Extract(target)
                    End Using

                Case FileAction.Delete

                    If (Not Zalohuj(operacia, release)) Then Return New OperationResult(False, "Nie je možné zálohovať súbor na disku: " & target, MessageType.Error)

                    If (Not File.Exists(target)) Then Return New OperationResult(False, "Chýba súbor na disku: " & target, MessageType.Error)

                    System.IO.File.Delete(target)

                Case FileAction.Replace

                    If (Not Zalohuj(operacia, release)) Then Return New OperationResult(False, "Nie je možné zálohovať súbor na disku: " & target, MessageType.Error)

                    If (Not File.Exists(target)) Then Return New OperationResult(False, "Chýba súbor na disku: " & target, MessageType.Error)

                    File.Delete(target)

                    Using zip As New C1ZipFile(Aktualizacia.ReleaseFilePath(release))

                        zip.Entries(ZipPath(operacia)).Extract(target)
                    End Using
            End Select

        Catch ex As Exception

            LogDB.ZalogujChybu(ex)
            Return New OperationResult(ex)
        End Try

        LogDBAktualizacia.ZalogujOperaciuKoniec(Of LogAktualizaciaOperaciaSubor)(log)

        Return New OperationResult(True)
    End Function

    Public Shared Function Revert(operacia As OperationFile, release As Release, logAktualizacia As LogAktualizacia) As OperationResult

        Try
            Select Case (operacia.Action)

                Case FileAction.Add
                    File.Delete(TargetFile(operacia))

                Case FileAction.Delete
                    File.Copy(BackupFile(operacia, release), TargetFile(operacia))

                Case FileAction.Replace
                    File.Copy(BackupFile(operacia, release), TargetFile(operacia), True)
            End Select

        Catch ex As Exception

            LogDB.ZalogujChybu(ex)
            Return New OperationResult(ex)
        End Try

        Return New OperationResult(True)
    End Function

#End Region

End Class
