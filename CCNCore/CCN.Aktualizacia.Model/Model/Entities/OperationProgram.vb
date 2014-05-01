Imports System.IO
Imports CCN.Model

Public Class OperationProgram
    Inherits Operation

#Region "Properties"

    Public Property Action As ProgramAction
    Public Property Path As String
    Public Property Process As String

    ''' <summary>
    ''' Timeout for the operation (in seconds).
    ''' </summary>
    Public Property Timeout As Integer = 10

    Public Property Arguments As String

#End Region

End Class
