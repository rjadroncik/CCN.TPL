Imports System.ComponentModel

Public Class GenericComponentResourceManager
    Inherits ComponentResourceManager

    Public Sub New(type As Type)
        MyBase.New(type)
    End Sub

    Protected Overrides Function GetResourceFileName(culture As System.Globalization.CultureInfo) As String

        If (MyBase.GetResourceFileName(culture).Contains("`")) Then

            Dim result = MyBase.GetResourceFileName(culture)

            Return result.Remove(result.IndexOf("`")) & result.Substring(result.IndexOf("."))
        Else
            Return MyBase.GetResourceFileName(culture)
        End If

    End Function

End Class
