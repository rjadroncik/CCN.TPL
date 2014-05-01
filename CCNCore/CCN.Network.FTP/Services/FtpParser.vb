Imports System.Text.RegularExpressions

Public Class FtpParser

#Region "Public"

    Public Shared Sub ParseLine(line As String, directory As FtpDirectory)

        Dim match As Match = MatchLine(line)
        If (match IsNot Nothing) Then

            Dim name As String = match.Groups("name").Value
            'Dim permission As String = match.Groups("permission").Value

            Dim timestamp As DateTime
            DateTime.TryParse(match.Groups("timestamp").Value, timestamp)

            Dim dir As String = match.Groups("dir").Value
            If ((dir <> "") AndAlso (dir <> "-")) Then

                directory.Directories.Add(New FtpDirectory(name, directory.PathFull))
            Else
                Dim size As Long
                Int64.TryParse(match.Groups("size").Value, size)

                directory.Files.Add(New FtpFile(name, directory.PathFull, timestamp, size))
            End If
        Else
            Throw (New ApplicationException("Unable to parse line: " & line))
        End If
    End Sub

    ''' <summary>
    ''' Create list from a (detailed) directory string
    ''' </summary>
    ''' <param name="listing">directory listing string</param>
    ''' <param name="directory">an existing <seealso cref="FtpDirectory" /> object</param>
    ''' <remarks></remarks>
    Public Shared Sub ParseDirectory(directory As FtpDirectory, listing As String)

        For Each line As String In listing.Replace(ControlChars.Lf, "").Split(ControlChars.Cr)

            If (line <> Nothing) Then ParseLine(line, directory)
        Next
    End Sub

#End Region

#Region "Private"

    Protected Shared Function MatchLine(line As String) As Match

        For i As Integer = 0 To _parseFormats.Length - 1

            Dim regEx As New Regex(_parseFormats(i))
            Dim match As Match = regEx.Match(line)

            If (match.Success) Then Return match
        Next

        Return Nothing
    End Function

#End Region

#Region "Regular expressions for parsing LIST results"

    ''' <summary>
    ''' List of REGEX formats for different FTP server listing formats.
    ''' </summary>
    ''' <remarks>
    ''' The first three are various UNIX/LINUX formats, fourth is for MS FTP
    ''' in detailed mode and the last for MS FTP in 'DOS' mode.
    ''' </remarks>
    Protected Shared _parseFormats As String() = New String() {"(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<name>.+)", _
                                                               "(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{4})\s+(?<name>.+)", _
                                                               "(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\d+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<name>.+)", _
                                                               "(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})\s+\d+\s+\w+\s+\w+\s+(?<size>\d+)\s+(?<timestamp>\w+\s+\d+\s+\d{1,2}:\d{2})\s+(?<name>.+)", _
                                                               "(?<dir>[\-d])(?<permission>([\-r][\-w][\-xs]){3})(\s+)(?<size>(\d+))(\s+)(?<ctbit>(\w+\s\w+))(\s+)(?<size2>(\d+))\s+(?<timestamp>\w+\s+\d+\s+\d{2}:\d{2})\s+(?<name>.+)", _
                                                               "(?<timestamp>\d{2}\-\d{2}\-\d{2}\s+\d{2}:\d{2}[Aa|Pp][mM])\s+(?<dir>\<\w+\>){0,1}(?<size>\d+){0,1}\s+(?<name>.+)"}
#End Region

End Class
