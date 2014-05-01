Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading

Public Class LogSubor

#Region "Events"

    Public Shared Event LineWritten As Action(Of String)

#End Region

#Region "Fields"

    Protected Shared _writer As StreamWriter

#End Region

#Region "Public"

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub StartProgress()

        Close()

        If (Not Directory.Exists(Globals.StartupPath & Path.DirectorySeparatorChar & "Logs")) Then Directory.CreateDirectory(Globals.StartupPath & Path.DirectorySeparatorChar & "Logs")

        _writer = New StreamWriter(Globals.StartupPath & Path.DirectorySeparatorChar & "Logs" & Path.DirectorySeparatorChar & "ProgressLog" & DateTime.Now.ToString("yyyyMMdd-HHmmss") & ".txt")
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub StartError()

        Close()

        If (Not Directory.Exists(Globals.StartupPath & Path.DirectorySeparatorChar & "Logs")) Then Directory.CreateDirectory(Globals.StartupPath & Path.DirectorySeparatorChar & "Logs")

        _writer = New StreamWriter(Globals.StartupPath & Path.DirectorySeparatorChar & "Logs" & Path.DirectorySeparatorChar & "ErrorLog" & DateTime.Now.ToString("yyyyMMdd-HHmmss") & ".txt")
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub WriteLine(line As String)

        If (_writer IsNot Nothing) Then _writer.WriteLine(Prefix() & line) : _writer.Flush()

        RaiseEvent LineWritten(line)
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub WriteLine()

        If (_writer IsNot Nothing) Then _writer.WriteLine() : _writer.Flush()
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub WriteException(exception As Exception)

        Dim current As Exception = exception
        While (current IsNot Nothing)

            WriteLine(current.Message)
            WriteLine(current.StackTrace)
            WriteLine()

            current = current.InnerException
        End While
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub Close()

        If (_writer IsNot Nothing) Then _writer.Close() : _writer.Dispose() : _writer = Nothing
    End Sub

#End Region

#Region "Private"

    Private Shared Function Prefix() As String

        Return Thread.CurrentThread().ManagedThreadId & " " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " "
    End Function

#End Region

End Class
