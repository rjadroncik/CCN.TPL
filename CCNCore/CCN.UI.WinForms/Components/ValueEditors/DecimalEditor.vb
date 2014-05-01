Public Class DecimalEditor
    Inherits NumericEditor(Of Decimal)

    Protected Overrides Function StringToValue(text As String) As Decimal

        Return CDec(text)
    End Function
End Class
