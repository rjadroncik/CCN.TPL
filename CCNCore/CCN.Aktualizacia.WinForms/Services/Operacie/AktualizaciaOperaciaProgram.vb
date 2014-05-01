Imports CCN.Services
Imports CCN.Aktualizacia.Model
Imports CCN.Model
Imports System.IO

Public Class AktualizaciaOperaciaProgram
    Inherits Aktualizacia

#Region "BL"

    Public Shared Function Perform(operacia As OperationProgram, release As Release, logAktualizacia As LogAktualizacia) As OperationResult

        Dim log = LogDBAktualizacia.ZalogujOperaciuProgramStart(operacia, logAktualizacia)

        Try
            Select Case (operacia.Action)

                Case ProgramAction.Kill

                    'Ak nas blokuje program a neda sa zatvorit, musime koncit
                    If (Not IPC.ProcessTryKill(operacia.Process, operacia.Timeout)) Then

                        Return New OperationResult(False, "Inštalátoru aktualizácie sa nepodarilo ukončiť bežiaci systém," & Environment.NewLine & _
                                                   " aktualizácia nemôže pokračovať.", MessageType.Warning)
                    End If

                Case ProgramAction.StartAndExit

                    Dim target As String = Aktualizacia.AdresarAplikacia & Path.DirectorySeparatorChar & operacia.Path

                    If (File.Exists(target)) Then

                        Diagnostics.Process.Start(target, If(operacia.Arguments Is Nothing, "", operacia.Arguments))
                        _exitAfterUpdate = True
                    Else
                        Return New OperationResult(False, "Systém nenašiel súbor " & operacia.Path & Environment.NewLine & _
                                                   " potrebný na vykonanie aktualizácie, zavolajte servis!", MessageType.Warning)
                    End If
            End Select

        Catch ex As Exception

            LogDB.ZalogujChybu(ex)
            Return New OperationResult(ex)
        End Try

        LogDBAktualizacia.ZalogujOperaciuKoniec(Of LogAktualizaciaOperaciaProgram)(log)

        Return New OperationResult(True)
    End Function

#End Region

End Class
