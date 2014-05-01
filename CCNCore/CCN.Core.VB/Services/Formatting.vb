Imports System.ComponentModel
Imports System.Reflection

Public Class Formatting

    Public Shared Function GetDescription(enumValue As [Enum]) As String

        Dim type As Type = enumValue.GetType()
        If (Not type.IsEnum) Then Throw New ArgumentException("EnumerationValue must be of Enum type", "enumValue")

        Dim memberInfo As MemberInfo() = type.GetMember(enumValue.ToString())
        If ((memberInfo IsNot Nothing) AndAlso (memberInfo.Length > 0)) Then

            Dim attrs As Object() = memberInfo(0).GetCustomAttributes(GetType(DescriptionAttribute), False)

            If ((attrs IsNot Nothing) AndAlso (attrs.Length > 0)) Then Return DirectCast(attrs(0), DescriptionAttribute).Description
        End If

        Return enumValue.ToString()
    End Function

    Public Shared Function NumberToInvariantCulture(value As String) As String

        If (value Is Nothing) Then Return Nothing

        Return value.Replace(",", ".").Replace(Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
    End Function

    Public Shared Function NumberToCurrentCulture(value As String) As String

        If (value Is Nothing) Then Return Nothing

        Return value.Replace(".", Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
    End Function

    Public Shared Function NullableToString(Of T As Structure)(value As Nullable(Of T)) As String

        If (value.HasValue) Then Return value.Value.ToString()

        Return "NULL"
    End Function

    Public Shared Function CapitalizeStart(value As String) As String

        If (value = Nothing) Then Return value

        Return value.Substring(0, 1).ToUpper() & value.Substring(1)
    End Function

    Public Shared Function NumberToRoman(number As Integer) As String

        If ((number < 0) OrElse (number > 3999)) Then Throw New ArgumentOutOfRangeException("number", "insert value betwheen 1 and 3999")
        If (number < 1) Then Return String.Empty
        If (number >= 1000) Then Return "M" & NumberToRoman(number - 1000)
        If (number >= 900) Then Return "CM" & NumberToRoman(number - 900)
        If (number >= 500) Then Return "D" & NumberToRoman(number - 500)
        If (number >= 400) Then Return "CD" & NumberToRoman(number - 400)
        If (number >= 100) Then Return "C" & NumberToRoman(number - 100)
        If (number >= 90) Then Return "XC" & NumberToRoman(number - 90)
        If (number >= 50) Then Return "L" & NumberToRoman(number - 50)
        If (number >= 40) Then Return "XL" & NumberToRoman(number - 40)
        If (number >= 10) Then Return "X" & NumberToRoman(number - 10)
        If (number >= 9) Then Return "IX" & NumberToRoman(number - 9)
        If (number >= 5) Then Return "V" & NumberToRoman(number - 5)
        If (number >= 4) Then Return "IV" & NumberToRoman(number - 4)
        If (number >= 1) Then Return "I" & NumberToRoman(number - 1)

        Throw New ArgumentOutOfRangeException("number", "something bad happened")
    End Function

End Class
