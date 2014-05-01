Public Class OperationResult

#Region "Properties"

    Protected _success As Boolean
    Public ReadOnly Property Success As Boolean
        Get
            Return _success
        End Get
    End Property

    Protected _message As String
    Public ReadOnly Property Message As String
        Get
            Return _message
        End Get
    End Property

    Protected _messageType As MessageType
    Public ReadOnly Property MessageType As MessageType
        Get
            Return _messageType
        End Get
    End Property

    Protected _exception As Exception
    Public ReadOnly Property Exception As Exception
        Get
            Return _exception
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New(exception As Exception)

        _exception = exception
        _messageType = MessageType.Error
    End Sub

    Public Sub New(success As Boolean, Optional message As String = Nothing, Optional messageType As MessageType = Model.MessageType.Info)

        _success = success
        _message = message
        _messageType = messageType
    End Sub

#End Region

End Class
