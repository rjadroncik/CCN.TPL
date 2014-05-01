Imports System.Diagnostics
Imports System.Data
Imports System.Collections
Imports System.Collections.Generic
Imports System
Imports System.Net
Imports System.IO
Imports CCN.Model

''' <summary>
''' Represents a file or directory entry from an FTP listing
''' </summary>
''' <remarks>
''' This class is used to parse the results from a detailed
''' directory list from FTP. It supports most formats of
''' </remarks>
Public Class FtpFile
    Inherits FtpRecord

#Region "Properties"

    Private _size As Long
    Public ReadOnly Property Size() As Long
        Get
            Return _size
        End Get
    End Property

    Protected _timestamp As DateTime
    Public ReadOnly Property Timestamp() As DateTime
        Get
            Return _timestamp
        End Get
    End Property

#End Region

#Region "Propertied - derived"

    <Derived()>
    Public ReadOnly Property Extension() As String
        Get
            Dim i As Integer = Name.LastIndexOf(".")

            If ((i >= 0) AndAlso i < (Name.Length - 1)) Then

                Return Name.Substring(i + 1)
            Else
                Return ""
            End If
        End Get
    End Property

    <Derived()>
    Public ReadOnly Property NameWithoutExtension() As String
        Get
            Dim i As Integer = Name.LastIndexOf(".")
            If (i > 0) Then
                Return Name.Substring(0, i)
            Else
                Return Name
            End If
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New(name As String, path As String, timestamp As DateTime, size As Long)

        _name = name
        _path = path
        _timestamp = timestamp
        _size = size
    End Sub

#End Region

End Class
