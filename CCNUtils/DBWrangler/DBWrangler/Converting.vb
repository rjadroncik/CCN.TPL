Imports System.Text

Public Delegate Function CustomToString(Of In T)(value As T) As String

Public Class Converting

#Region "Lists - IEnumerable"

    Public Shared Function Values2String(Of T)(values As IEnumerable(Of T), _
                                               toString As CustomToString(Of T), _
                                               Optional separator As String = ", ", _
                                               Optional expectedLength As Integer = 100) As String

        If (values Is Nothing) Then Return Nothing

        Dim result As New StringBuilder(expectedLength)

        Dim first As Boolean = True
        For Each value As T In values

            If (Not first) Then result.Append(separator)

            result.Append(toString(value))
            first = False
        Next

        Return result.ToString()
    End Function

    Public Shared Function Values2String(Of T)(values As IEnumerable(Of T), _
                                               Optional separator As String = ", ", _
                                               Optional expectedLength As Integer = 100) As String

        If (values Is Nothing) Then Return Nothing

        Dim result As New StringBuilder(expectedLength)

        Dim first As Boolean = True
        For Each value As T In values

            If (Not first) Then result.Append(separator)

            result.Append(value.ToString())
            first = False
        Next

        Return result.ToString()
    End Function

    Public Shared Function Values2StringInvariant(Of T)(values As IEnumerable(Of T), _
                                                        Optional separator As String = ", ", _
                                                        Optional expectedLength As Integer = 100) As String
        If (values Is Nothing) Then Return Nothing

        Dim result As New StringBuilder(expectedLength)

        Dim first As Boolean = True
        For Each value As T In values

            If (Not first) Then result.Append(separator)

            If (TypeOf value Is IFormattable) Then

                result.Append(DirectCast(value, IFormattable).ToString(Nothing, Globalization.CultureInfo.InvariantCulture))
            Else
                result.Append(value.ToString())
            End If
            first = False
        Next

        Return result.ToString()
    End Function

    Public Shared Function String2Values(value As String, _
                                         Optional separator As String = ",") As IEnumerable(Of String)

        If (value Is Nothing) Then Return Nothing

        Return value.Split(New String() {separator}, StringSplitOptions.None).Select(Function(x) x.Trim()).ToList()
    End Function

#End Region

#Region "Enum"

    Public Shared Function Enum2ValueString(value As [Enum]) As String

        Return CInt(DirectCast(value, Object)).ToString()
    End Function

    Public Shared Function String2Enum(Of T As {Structure, IConvertible})(value As String) As T

        Try
            Return DirectCast([Enum].Parse(GetType(T), value, False), T)

        Catch ex As Exception

            Throw New UnrecognizedEnumValueException()
        End Try
    End Function

    Public Shared Function Enum2Values(Of T As {Structure, IConvertible})() As IEnumerable(Of T)

        Return [Enum].GetValues(GetType(T)).Cast(Of T)()
    End Function

#End Region

#Region "Version"

    Public Shared Function String2Version(version As String) As Version

        If version.Contains(".") Then
            Dim v() As String = version.Split("."c)
            Return New Version(Integer.Parse(v(0)), Integer.Parse(v(1)), Integer.Parse(v(2)), Integer.Parse(v(3)))
        ElseIf version.Contains("-") Then
            Dim v() As String = version.Split("-"c)
            Return New Version(Integer.Parse(v(0)), Integer.Parse(v(1)), Integer.Parse(v(2)), Integer.Parse(v(3)))
        ElseIf version.Contains("_") Then
            Dim v() As String = version.Split("_"c)
            Return New Version(Integer.Parse(v(0)), Integer.Parse(v(1)), Integer.Parse(v(2)), Integer.Parse(v(3)))
        End If

        Throw New InvalidCastException("Neplatný formát reťazca verzie!")
    End Function

#End Region

#Region "String"

    Public Shared Function TrimOrEmpty(value As String) As String

        Return If(value Is Nothing, String.Empty, value.Trim())
    End Function

    Public Shared Function TrimOrNothing(value As String) As String

        Return If(value Is Nothing, Nothing, value.Trim())
    End Function

    Public Shared Function ToStringOrNothing(value As Object) As String

        Return If(value Is Nothing, Nothing, value.ToString())
    End Function

    Public Shared Function ToStringInvariant(value As IFormattable) As String

        Return If(value Is Nothing, String.Empty, value.ToStringInvariant())
    End Function

    Public Shared Function ToStringInvariantOrNothing(value As IFormattable) As String

        Return If(value Is Nothing, Nothing, value.ToStringInvariant())
    End Function

    Public Shared Function ToStringTrimOrEmpty(value As Object) As String

        Return If(value Is Nothing, String.Empty, ToStringTrim(value))
    End Function

    Public Shared Function ToStringTrimOrNothing(value As Object) As String

        Return If(value Is Nothing, Nothing, ToStringTrim(value))
    End Function

    Public Shared Function ToStringTrim(value As Object) As String

        Return value.ToString().Trim()
    End Function

    Public Shared Function QuoteSingle(Of T)(value As T) As String

        Return "'" & value.ToString() & "'"
    End Function

    Public Shared Function QuoteDouble(Of T)(value As T) As String

        Return """" & value.ToString() & """"
    End Function

#End Region

#Region "XML"

    Public Shared Function ToXml(value As Boolean) As String

        Return value.ToString().ToLower()
    End Function

    Public Shared Function FromXmlBoolean(value As String) As Boolean

        Return (value = "true") OrElse (value = "True") OrElse (value = "1")
    End Function

#End Region

End Class
