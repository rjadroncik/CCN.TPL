Public Class CCNPrintTextFieldLiteral
    Inherits CCNPrintTextField

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintTextFieldLiteral(_document)

        CopyElementProperties(result)
        CopyRectangleProperties(result)
        CopyTextFieldProperties(result)

        result._text = _text

        Return result
    End Function

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Properties _ Overriden"

    Private _text As String
    Public Overrides Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

#End Region

End Class
