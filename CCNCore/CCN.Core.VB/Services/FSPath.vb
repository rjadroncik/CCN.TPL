Public Class FSPath

#Region "Public"

    Public Shared Function NormalizeDir(path As String) As String

        Dim result As String = RemoveDoubleBackslash(RemoveQuotes(path))
        If (Not result.EndsWith("\")) Then result &= "\"

        Return result
    End Function

    Public Shared Function NormalizeFile(path As String) As String

        Return RemoveDoubleBackslash(RemoveQuotes(path))
    End Function

#End Region

#Region "Private"

    Protected Shared Function RemoveQuotes(text As String) As String

        Dim result As String = text.Trim()
        If result.StartsWith("""") AndAlso result.EndsWith("""") Then result = result.Substring(1, result.Length - 2)

        Return result
    End Function

    Protected Shared Function RemoveDoubleBackslash(text As String) As String

        Return text.Replace("\\", "\")
    End Function

#End Region

End Class
