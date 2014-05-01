Imports System.ComponentModel
Imports System.Globalization
Imports C1.C1Preview
Imports System.Drawing.Drawing2D

Public Class CCNDashStyleConverter
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

        If (destinationType Is GetType(String) AndAlso _
            (TypeOf value Is DashStyle)) Then

            Select Case DirectCast(value, DashStyle)

                Case DashStyle.Solid
                    Return "Plná"
                Case DashStyle.Dash
                    Return "Čiarkovaná"
                Case DashStyle.DashDot
                    Return "Bokdočiarkovaná"
                Case DashStyle.Dot
                    Return "Bodkovaná"
            End Select

            Throw New InvalidOperationException()
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function

    Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean

        If (sourceType Is GetType(String)) Then Return True

        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object

        If (TypeOf value Is String) Then

            Select Case DirectCast(value, String)

                Case "Plná"
                    Return DashStyle.Solid
                Case "Čiarkovaná"
                    Return DashStyle.Dash
                Case "Bokdočiarkovaná"
                    Return DashStyle.DashDot
                Case "Bodkovaná"
                    Return DashStyle.Dot
            End Select
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection

        Return New StandardValuesCollection(New DashStyle() {DashStyle.Solid, DashStyle.Dash, DashStyle.DashDot, DashStyle.Dot})
    End Function

    Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return False
    End Function

    Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
End Class
