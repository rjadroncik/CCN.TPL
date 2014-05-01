Imports System.ComponentModel
Imports System.Globalization

Public Class CCNFloatConverter
    Inherits ExpandableObjectConverter

    Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, _
                                                     ByVal destinationType As Type) As Boolean

        If (destinationType Is GetType(String)) Then Return True

        Return MyBase.CanConvertTo(context, destinationType)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, _
                                                  ByVal culture As CultureInfo, _
                                                  ByVal value As Object, _
                                                  ByVal destinationType As System.Type) As Object

        If (destinationType Is GetType(String)) Then

            If (TypeOf value Is Single) Then Return Math.Round(DirectCast(value, Single), 2).ToString()
            If (TypeOf value Is Decimal) Then Return Math.Round(DirectCast(value, Decimal), 2).ToString()
            If (TypeOf value Is Double) Then Return Math.Round(DirectCast(value, Double), 2).ToString()
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean

        If (sourceType Is GetType(String)) Then Return True

        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object

        If (TypeOf value Is String) Then

            Dim result As Single

            If (Single.TryParse(value, result)) Then
                Return result
            Else
                Return Nothing
            End If
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return False
    End Function
End Class
