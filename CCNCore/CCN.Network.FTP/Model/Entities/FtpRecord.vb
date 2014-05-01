Imports CCN.Model

Public Class FtpRecord

#Region "Properties"

    Protected _name As String
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Protected _path As String
    Public ReadOnly Property Path() As String
        Get
            Return _path
        End Get
    End Property

    <Derived()>
    Public ReadOnly Property PathFull() As String
        Get
            Return _path & _name
        End Get
    End Property

#End Region

End Class
