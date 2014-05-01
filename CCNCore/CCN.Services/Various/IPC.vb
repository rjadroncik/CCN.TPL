Imports System.IO

Public Class IPC

    Public Shared Function ProcessIsRunning(name As String) As Boolean

        Return Process.GetProcessesByName(name).Length > 0
    End Function

    Public Shared Function ProcessTryKill(name As String, waitTotal As Integer, Optional waitInterval As Integer = 1) As Boolean

        'Wait for process(es) to finish
        For i As Integer = 1 To waitTotal \ waitInterval

            If (ProcessIsRunning(name)) Then Threading.Thread.Sleep(waitInterval * 1000)
        Next

        'Kill 'em all
        For Each proces As Process In Process.GetProcessesByName(name)

            proces.Kill()
            Threading.Thread.Sleep(waitInterval * 1000)
        Next

        Return Not ProcessIsRunning(name)
    End Function

End Class
