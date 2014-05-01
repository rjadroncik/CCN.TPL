Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class CsvReader
    Implements IDisposable

#Region "Initialization"

    Public Sub New(ByVal path As String, Optional ByVal separator As String = ";")

        _separator = separator
        Open(path)
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property IsOpen As Boolean
        Get
            Return _parser IsNot Nothing
        End Get
    End Property

#End Region

#Region "Fields"

    Protected _separator As String
    Protected _parser As TextFieldParser
    Protected _currentValues As String()

#End Region

#Region "BL"

    Public Sub Open(path As String)

        If (IsOpen) Then Throw New InvalidOperationException("Source is already open.")

        _parser = New TextFieldParser(path)

        _parser.CommentTokens = New String() {"#"}
        _parser.SetDelimiters(New String() {_separator})
        _parser.HasFieldsEnclosedInQuotes = True
    End Sub

    Public Function NextRecord() As Boolean

        If (Not IsOpen) Then Throw New InvalidOperationException("Can't read while source is not open.")

        If (_parser.EndOfData) Then Return False

        _currentValues = _parser.ReadFields()
        Return True
    End Function

    Public Function GetValue(index As Integer) As String

        If (Not IsOpen) Then Throw New InvalidOperationException("Can't read while source is not open.")

        Return _currentValues(index)
    End Function

    Public Function GetValues() As String()

        If (Not IsOpen) Then Throw New InvalidOperationException("Can't read while source is not open.")

        Return _currentValues
    End Function

#End Region

#Region "IDisposable"

    Public Sub Dispose() Implements System.IDisposable.Dispose

        If (_parser IsNot Nothing) Then

            _parser.Dispose()
            _parser = Nothing
        End If
    End Sub

#End Region

End Class
