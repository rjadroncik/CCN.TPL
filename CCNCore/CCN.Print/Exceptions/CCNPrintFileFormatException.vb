Public Class CCNPrintFileFormatException
    Inherits Exception

    Private _element As CCNPrintElement

    Public Sub New(ByVal message As String, ByVal element As CCNPrintElement)
        MyBase.New(message)

        _element = element
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class
