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
''' Stores a list of files and directories from an FTP result
''' </summary>
''' <remarks></remarks>
Public Class FtpDirectory
    Inherits FtpRecord

#Region "Properties"

    Protected _files As New List(Of FtpFile)
    Public ReadOnly Property Files() As IList(Of FtpFile)
        Get
            Return _files
        End Get
    End Property

    Protected _directories As New List(Of FtpDirectory)
    Public ReadOnly Property Directories() As IList(Of FtpDirectory)
        Get
            Return _directories
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(name As String, path As String)

        _name = name
        _path = path
    End Sub

#End Region

    Public Function FileExists(filename As String) As Boolean

        Return (_files.Where(Function(x) x.Name = filename).Count() = 1)
    End Function

    'Private Const slash As Char = "/"c

    'Public Shared Function GetParentDirectory(dir As String) As String

    '    Dim tmp As String = dir.TrimEnd(slash)
    '    Dim i As Integer = tmp.LastIndexOf(slash)

    '    If i > 0 Then
    '        Return tmp.Substring(0, i - 1)
    '    Else
    '        Throw (New ApplicationException("No parent for root"))
    '    End If

    'End Function

End Class
