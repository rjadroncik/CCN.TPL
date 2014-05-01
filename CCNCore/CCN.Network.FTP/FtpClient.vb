Imports System.Diagnostics
Imports System.Data
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions

''' <summary>
''' A wrapper class for .NET 2.0 FTP
''' </summary>
''' <remarks>
''' This class does not hold open an FTP connection but
''' instead is stateless: for each FTP request it
''' connects, performs the request and disconnects.
''' </remarks>    
Public Class FtpClient

#Region "Initialization"

    ''' <summary>
    ''' Constructor just taking the hostname
    ''' </summary>
    ''' <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
    ''' <remarks></remarks>
    Public Sub New(Hostname As String)
        _hostname = Hostname
    End Sub

    ''' <summary>
    ''' Constructor taking hostname, username and password
    ''' </summary>
    ''' <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
    ''' <param name="Username">Leave blank to use 'anonymous' but set password to your email</param>
    ''' <param name="Password"></param>
    ''' <param name="useSsl"></param>
    ''' <remarks></remarks>
    Public Sub New(Hostname As String, Username As String, Password As String, _
                   Optional useSsl As Boolean = False)

        _hostname = Hostname
        _username = Username

        Me.Password = Password
        Me.UseSSL = useSsl
    End Sub

#End Region

#Region "Directory functions"

    ''' <summary>
    ''' Return a detailed directory listing
    ''' </summary>
    ''' <param name="path">Directory to list, e.g. /pub/etc</param>
    ''' <returns>A FtpDirectory object</returns>
    Public Function DirectoryRead(path As String) As FtpDirectory

        Dim name As String = path.Substring(path.Substring(0, path.Length - 1).LastIndexOf("/") + 1)

        Dim result As New FtpDirectory(name, path.Substring(0, path.Length - name.Length))

        Dim ftp As FtpWebRequest = GetRequest(GetDirectory(path))
        ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails

        FtpParser.ParseDirectory(result, GetStringResponse(ftp).Replace(ControlChars.CrLf, ControlChars.Cr).TrimEnd(ControlChars.Cr))

        Return result
    End Function

#End Region

#Region "Upload: File transfer TO ftp server"

    ''' <summary>
    ''' Copy a local file to the FTP server
    ''' </summary>
    ''' <param name="localFilename">Full path of the local file</param>
    ''' <param name="targetFilename">Target filename, if required</param>
    ''' <returns></returns>
    ''' <remarks>If the target filename is blank, the source filename is used
    ''' (assumes current directory). Otherwise use a filename to specify a name
    ''' or a full path and filename if required.</remarks>
    Public Function Upload(localFilename As String, targetFilename As String) As Boolean
        '1. check source
        If Not File.Exists(localFilename) Then
            Throw (New ApplicationException("File " & localFilename & " not found"))
        End If
        'copy to FI
        Dim fi As New FileInfo(localFilename)
        Return Upload(fi, targetFilename)
    End Function

    ''' <summary>
    ''' Upload a local file to the FTP server
    ''' </summary>
    ''' <param name="fi">Source file</param>
    ''' <param name="targetFilename">Target filename (optional)</param>
    ''' <returns></returns>
    Public Function Upload(fi As FileInfo, targetFilename As String) As Boolean
        'copy the file specified to target file: target file can be full path or just filename (uses current dir)

        '1. check target
        Dim target As String
        If targetFilename.Trim() = "" Then
            'Blank target: use source filename & current dir
            target = Me.CurrentDirectory + fi.Name
        ElseIf targetFilename.Contains("/") Then
            'If contains / treat as a full path
            target = AdjustDir(targetFilename)
        Else
            'otherwise treat as filename only, use current directory
            target = CurrentDirectory + targetFilename
        End If

        Dim URI As String = Hostname + target
        'perform copy
        Dim ftp As FtpWebRequest = GetRequest(URI)

        'Set request to upload a file in binary
        ftp.Method = WebRequestMethods.Ftp.UploadFile
        ftp.UseBinary = True

        'Notify FTP of the expected size
        ftp.ContentLength = fi.Length

        'create byte array to store: ensure at least 1 byte!
        Const BufferSize As Integer = 2048
        Dim content As Byte() = New Byte(BufferSize - 1) {}
        Dim dataRead As Integer

        'open file for reading
        Using fs As FileStream = fi.OpenRead()
            Try
                'open request to send
                Using rs As Stream = ftp.GetRequestStream()
                    Do
                        dataRead = fs.Read(content, 0, BufferSize)
                        rs.Write(content, 0, dataRead)
                    Loop While Not (dataRead < BufferSize)
                    rs.Close()
                End Using

            Catch generatedExceptionName As Exception
            End Try
        End Using

        ftp = Nothing
        Return True

    End Function

#End Region

#Region "Download: File transfer FROM ftp server"

    ''' <summary>
    ''' Copy a file from FTP server to local
    ''' </summary>
    ''' <param name="sourceFilename">Target filename, if required</param>
    ''' <param name="localFilename">Full path of the local file</param>
    ''' <returns></returns>
    ''' <remarks>Target can be blank (use same filename), or just a filename
    ''' (assumes current directory) or a full path and filename</remarks>
    Public Function Download(sourceFilename As String, localFilename As String, PermitOverwrite As Boolean) As Boolean
        '2. determine target file
        Dim fi As New FileInfo(localFilename)
        Return Me.Download(sourceFilename, fi, PermitOverwrite)
    End Function

    'Version taking an FtpFile
    Public Function Download(file As FtpFile, localFilename As String, PermitOverwrite As Boolean) As Boolean
        Return Me.Download(file.PathFull, localFilename, PermitOverwrite)
    End Function

    'Another version taking FtpFile and FileInfo
    Public Function Download(file As FtpFile, localFI As FileInfo, PermitOverwrite As Boolean) As Boolean
        Return Me.Download(file.PathFull, localFI, PermitOverwrite)
    End Function

    'Version taking string/FileInfo
    Public Function Download(sourceFilename As String, targetFI As FileInfo, PermitOverwrite As Boolean) As Boolean
        '1. check target
        If (targetFI.Exists AndAlso (Not PermitOverwrite)) Then
            Throw (New ApplicationException("Target file already exists"))
        End If

        If (targetFI.Exists) Then targetFI.Delete()

        '2. check source
        Dim target As String
        If sourceFilename.Trim() = "" Then
            Throw (New ApplicationException("File not specified"))
        ElseIf sourceFilename.Contains("/") Then
            'treat as a full path
            target = AdjustDir(sourceFilename)
        Else
            'treat as filename only, use current directory
            target = CurrentDirectory + sourceFilename
        End If

        Dim URI As String = Hostname + target

        '3. perform copy
        Dim ftp As FtpWebRequest = GetRequest(URI)

        'Set request to download a file in binary mode
        ftp.Method = WebRequestMethods.Ftp.DownloadFile
        ftp.UseBinary = True

        'open request and get response stream
        Using response As FtpWebResponse = DirectCast(ftp.GetResponse(), FtpWebResponse)
            Using responseStream As Stream = response.GetResponseStream()
                'loop to read & write to file
                Using fs As FileStream = targetFI.OpenWrite()
                    Try
                        Dim buffer As Byte() = New Byte(4095) {}

                        Dim read As Integer = 0
                        Do
                            read = responseStream.Read(buffer, 0, buffer.Length)
                            fs.Write(buffer, 0, read)
                        Loop While Not (read = 0)
                        responseStream.Close()

                        fs.Flush()
                        fs.Close()
                    Catch generatedExceptionName As Exception
                        'catch error and delete file only partially downloaded
                        fs.Close()
                        targetFI.Delete()
                        Throw
                    End Try
                End Using
            End Using
        End Using

        Return True
    End Function

#End Region

#Region "Other functions: Delete rename etc."

    ''' <summary>
    ''' Delete remote file
    ''' </summary>
    ''' <param name="filename">filename or full path</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(filename As String) As Boolean

        'Determine if file or full path
        Dim URI As String = Hostname + GetFullPath(filename)
        Dim ftp As FtpWebRequest = GetRequest(URI)

        ftp.Method = WebRequestMethods.Ftp.DeleteFile

        Try
            'get response but ignore it
            GetStringResponse(ftp)
        Catch generatedExceptionName As Exception
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Determine if file exists on remote FTP site
    ''' </summary>
    ''' <param name="filename">Filename (for current dir) or full path</param>
    ''' <returns></returns>
    ''' <remarks>Note this only works for files</remarks>
    Public Function FileExists(filename As String) As Boolean

        'Try to obtain filesize: if we get error msg containing "550" the file does not exist
        Try
            Dim size As Long = GetFileSize(filename)
            Return True

        Catch ex As Exception
            'only handle expected not-found exception
            If TypeOf ex Is WebException Then
                'file does not exist/no rights error = 550
                If ex.Message.Contains("550") Then
                    'clear
                    Return False
                Else
                    Throw
                End If
            Else
                Throw
            End If
        End Try
    End Function

    ''' <summary>
    ''' Determine size of remote file
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <returns></returns>
    ''' <remarks>Throws an exception if file does not exist</remarks>
    Public Function GetFileSize(filename As String) As Long

        Dim path As String

        If filename.Contains("/") Then

            path = AdjustDir(filename)
        Else
            path = Me.CurrentDirectory + filename
        End If

        Dim URI As String = Me.Hostname + path
        Dim ftp As FtpWebRequest = GetRequest(URI)

        'Try to get info on file/dir?
        ftp.Method = WebRequestMethods.Ftp.GetFileSize

        GetStringResponse(ftp)

        Return GetSize(ftp)
    End Function

    Public Function Rename(file As String, newName As String) As Boolean

        'Does file exist?
        Dim source As String = GetFullPath(file)
        If Not FileExists(source) Then
            Throw (New FileNotFoundException("File " & source & " not found"))
        End If

        'build target name, ensure it does not exist
        Dim target As String = GetFullPath(newName)
        If target = source Then
            Throw (New ApplicationException("Source and target are the same"))
        ElseIf FileExists(target) Then
            Throw (New ApplicationException("Target file " & target & " already exists"))
        End If

        'perform rename
        Dim URI As String = Me.Hostname + source

        Dim ftp As FtpWebRequest = GetRequest(URI)
        'Set request to delete
        ftp.Method = WebRequestMethods.Ftp.Rename
        ftp.RenameTo = target

        Try
            'get response but ignore it
            GetStringResponse(ftp)
        Catch generatedExceptionName As Exception
            Return False
        End Try

        Return True
    End Function

    Public Function CreateDirectory(path As String) As Boolean

        Dim URI As String = Me.Hostname + AdjustDir(path)
        Dim ftp As FtpWebRequest = GetRequest(URI)

        ftp.Method = WebRequestMethods.Ftp.MakeDirectory

        Try
            'Get response but ignore it
            GetStringResponse(ftp)

        Catch generatedExceptionName As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteDirectory(path As String) As Boolean

        Dim URI As String = Me.Hostname + AdjustDir(path)
        Dim ftp As FtpWebRequest = GetRequest(URI)

        ftp.Method = WebRequestMethods.Ftp.RemoveDirectory

        Try
            'Get response but ignore it
            GetStringResponse(ftp)

        Catch generatedExceptionName As Exception
            Return False
        End Try

        Return True
    End Function

#End Region

#Region "private supporting fns"

    'Get the basic FtpWebRequest object with the common settings and security
    Private Function GetRequest(URI As String) As FtpWebRequest

        Dim result = DirectCast(FtpWebRequest.Create(URI), FtpWebRequest)
        With result
            .Credentials = GetCredentials()
            .EnableSsl = UseSSL

            If (ProxyEnabled) Then
                .Proxy = New WebProxy(ProxyHostName, ProxyPort)
                .Proxy.Credentials = New NetworkCredential(ProxyUserName, ProxyPassword)
            End If

            .KeepAlive = False
        End With
        Return result
    End Function

    ''' <summary>
    ''' Get the credentials from username/password
    ''' </summary>
    Private Function GetCredentials() As ICredentials

        Return New NetworkCredential(Username, Password)
    End Function

    ''' <summary>
    ''' returns a full path using CurrentDirectory for a relative file reference
    ''' </summary>
    Private Function GetFullPath(file As String) As String

        If file.Contains("/") Then

            Return AdjustDir(file)
        Else
            Return CurrentDirectory + file
        End If
    End Function

    ''' <summary>
    ''' Amend an FTP path so that it always starts with /
    ''' </summary>
    ''' <param name="path">Path to adjust</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AdjustDir(path As String) As String

        Return If(path.StartsWith("/"), "", "/").ToString() + path
    End Function

    Private Function GetDirectory(directory As String) As String

        If (directory = "") Then

            Return Hostname + CurrentDirectory
        Else
            If (Not directory.StartsWith("/")) Then Throw (New ApplicationException("Directory should start with /"))

            Return Hostname + directory
        End If
    End Function

    ''' <summary>Obtains a response stream as a string.</summary>
    ''' <param name="ftp">current FTP request</param>
    ''' <returns>String containing response</returns>
    ''' <remarks>FTP servers typically return strings with CR and not CRLF. Use respons.Replace(vbCR, vbCRLF) to convert to an MSDOS string</remarks>
    Private Function GetStringResponse(ftp As FtpWebRequest) As String

        Using response As FtpWebResponse = DirectCast(ftp.GetResponse(), FtpWebResponse)

            Using datastream As Stream = response.GetResponseStream()

                Using sr As New StreamReader(datastream)
                    Return sr.ReadToEnd()
                End Using
            End Using
        End Using
    End Function

    ''' <summary> Gets the size of an FTP request.</summary>
    ''' <param name="ftp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSize(ftp As FtpWebRequest) As Long

        Using response As FtpWebResponse = DirectCast(ftp.GetResponse(), FtpWebResponse)
            Return response.ContentLength
        End Using
    End Function

#End Region

#Region "Properties"

    Private _hostname As String
    ''' <summary>
    ''' Hostname
    ''' </summary>
    ''' <value></value>
    ''' <remarks>Hostname can be in either the full URL format
    ''' ftp://ftp.myhost.com or just ftp.myhost.com
    ''' </remarks>
    Public Property Hostname As String
        Get
            If _hostname.StartsWith("ftp://") Then
                Return _hostname
            Else
                Return "ftp://" & _hostname
            End If
        End Get
        Set(value As String)
            _hostname = value
        End Set
    End Property

    Private _username As String
    ''' <summary>
    ''' Username property
    ''' </summary>
    ''' <value></value>
    ''' <remarks>Can be left blank, in which case 'anonymous' is returned</remarks>
    Public Property Username As String
        Get
            Return If(_username = Nothing, "anonymous", _username)
        End Get
        Set(value As String)
            _username = value
        End Set
    End Property

    Public Property Password As String
    Public Property UseSSL As Boolean

    ''' <summary>
    ''' The CurrentDirectory value
    ''' </summary>
    ''' <remarks>Defaults to the root '/'</remarks>
    Private _currentDirectory As String = "/"
    Public Property CurrentDirectory As String
        Get
            'return directory, ensure it ends with /
            Return _currentDirectory + If(_currentDirectory.EndsWith("/"), "", "/").ToString()
        End Get
        Set(value As String)

            If (Not value.StartsWith("/")) Then Throw New ApplicationException("Directory should start with /")

            _currentDirectory = value
        End Set
    End Property

    Public Property ProxyEnabled As Boolean
    Public Property ProxyHostName As String
    Public Property ProxyPort As Integer
    Public Property ProxyUserName As String
    Public Property ProxyPassword As String

#End Region

End Class
