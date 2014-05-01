Public Class IntegerEditor
    Inherits NumericEditor(Of Integer)

    Protected Overrides Function StringToValue(text As String) As Integer

        Return CInt(text)
    End Function
End Class
