Public Class CCNPrintTextFieldBound
    Inherits CCNPrintTextField

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintTextFieldBound(_document)

        CopyElementProperties(result)
        CopyRectangleProperties(result)
        CopyTextFieldProperties(result)

        Return result
    End Function

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Properties - Overrriden"

    Public Overrides Property Text As String
        Get
            Dim result As String = _document.Values(ValueIdFull).ToString()

            If (result = "") Then result = Document.Values(_valueId).ToString()

            Return result
        End Get
        Set(ByVal value As String)
            Throw New InvalidOperationException("Cannot directly set the text value of a bound text-field!")
        End Set
    End Property

#End Region

End Class
