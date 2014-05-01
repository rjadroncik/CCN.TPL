Public Class NastavenieNenamapovaneException
    Inherits Exception

    Protected _id As Integer
    Public ReadOnly Property Id As Integer
        Get
            Return _id
        End Get
    End Property

    Public Sub New(id As Integer)
        _id = id
    End Sub
End Class
