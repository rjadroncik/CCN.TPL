Imports System.ComponentModel
Imports System.Globalization

Public Class CCNBorderConverter
    Inherits ExpandableObjectConverter

    Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, _
                                                     ByVal destinationType As Type) As Boolean

        If (destinationType Is GetType(String)) Then Return True

        Return MyBase.CanConvertFrom(context, destinationType)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, _
                                                  ByVal culture As CultureInfo, _
                                                  ByVal value As Object, _
                                                  ByVal destinationType As System.Type) As Object

        If (destinationType Is GetType(String) AndAlso _
            (TypeOf value Is CCNPrintBorder)) Then

            'Dim border As CCNPrintBorder = value

            Return "" '& border.Thickness
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class
