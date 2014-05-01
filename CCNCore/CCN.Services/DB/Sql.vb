Imports CCN.Core.VB

Public Class Sql

    Public Shared Function DBValue2Value(ByVal value As Object, Optional ByVal dbNullValue As Object = Nothing) As Object

        If (value Is DBNull.Value) Then

            Return dbNullValue

        ElseIf (TypeOf value Is String) Then

            Return DirectCast(value, String).Trim()
        End If

        Return value
    End Function

    ''' <summary>
    ''' Konvertuje SQL dátum na VB.NET dátum, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBDatetime2DateTime(ByVal value As Object) As DateTime

        If (value Is DBNull.Value) Then Return Nothing

        Return DirectCast(value, DateTime)
    End Function

    ''' <summary>
    ''' Konvertuje SQL dátum na VB.NET dátum?, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBDatetime2NullableDateTime(ByVal value As Object) As DateTime?

        If (value Is DBNull.Value) Then Return Nothing

        Return DirectCast(value, DateTime?)
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET string na SQL string, ošetruje Nothing, '
    ''' </summary>
    ''' <param name="value">String na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function String2Sql(ByVal value As String) As String

        If (value = Nothing) Then Return "NULL"

        Return "'" & value.Replace("'", "''") & "'"
    End Function

    ''' <summary>
    ''' Konvertuje SQL (n)char, (n)varchar, (n)text na VB.NET String, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBString2String(ByVal value As Object) As String

        If (value Is DBNull.Value) Then Return Nothing

        Return DirectCast(value, String).Trim()
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET dátum na SQL datetime string, ošetruje Nothing
    ''' </summary>
    ''' <param name="value">Dátum na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DateTime2Sql(ByVal value As DateTime) As String

        If (value = Nothing) Then Return "NULL"

        Return "CONVERT(DATETIME, '" & value.ToString("yyyy-MM-dd HH:mm:ss") & "', 102)"
    End Function

    ''' <summary>
    ''' Konvertuje SQL int na VB.NET Integer, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBInt2Integer(ByVal value As Object, Optional ByVal dbNullValue As Integer = 0) As Integer

        If (value Is DBNull.Value) Then Return dbNullValue

        Return DirectCast(value, Integer)
    End Function

    ''' <summary>
    ''' Konvertuje SQL int na VB.NET Integer?, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBInt2NullableInteger(ByVal value As Object) As Integer?

        If (value Is DBNull.Value) Then Return Nothing

        Return CType(value, Integer?)
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET Integer na SQL int string, ošetruje Nothing
    ''' </summary>
    ''' <param name="value">Číslo na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Integer2Sql(ByVal value As Integer, Optional ByVal dbNullValue As Integer = 0) As String

        If (value = dbNullValue) Then Return "NULL"

        Return value.ToStringInvariant()
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET Integer? na SQL int string, ošetruje Nothing
    ''' </summary>
    ''' <param name="value">Číslo na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NullableInteger2Sql(ByVal value As Integer?) As String

        If (Not value.HasValue) Then Return "NULL"

        Return value.Value.ToStringInvariant()
    End Function

    ''' <summary>
    ''' Konvertuje SQL int na VB.NET Short?, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBInt2NullableShort(ByVal value As Object) As Short?

        If (value Is DBNull.Value) Then Return Nothing

        Return CType(value, Short?)
    End Function

    ''' <summary>
    ''' Konvertuje SQL decimal na VB.NET Decimal?, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBDecimal2NullableDecimal(ByVal value As Object) As Decimal?

        If (value Is DBNull.Value) Then Return Nothing

        Return CType(value, Decimal?)
    End Function

    ''' <summary>
    ''' Konvertuje SQL bit na VB.NET Boolean?, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBBit2NullableBoolean(ByVal value As Object) As Boolean?

        If (value Is DBNull.Value) Then Return Nothing

        Return CType(value, Boolean?)
    End Function

    ''' <summary>
    ''' Konvertuje SQL float na VB.NET Double, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBFloat2Double(ByVal value As Object, Optional ByVal dbNullValue As Double = 0) As Double

        If (value Is DBNull.Value) Then Return dbNullValue

        Return DirectCast(value, Double)
    End Function

    ''' <summary>
    ''' Konvertuje SQL float na VB.NET Double?, ošetruje DBNull
    ''' </summary>
    ''' <param name="value">Objekt na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBFloat2NullableDouble(ByVal value As Object) As Double?

        If (value Is DBNull.Value) Then Return Nothing

        Return CType(value, Double?)
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET Double na SQL float string, ošetruje Nothing
    ''' </summary>
    ''' <param name="value">Číslo na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Double2Sql(ByVal value As Double, Optional ByVal dbNullValue As Double = 0) As String

        If (value = dbNullValue) Then Return "NULL"

        Return value.ToStringInvariant()
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET Double na SQL float string, ošetruje Nothing
    ''' </summary>
    ''' <param name="value">Číslo na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NullableDouble2Sql(ByVal value As Double?) As String

        If (Not value.HasValue) Then Return "NULL"

        Return value.Value.ToStringInvariant()
    End Function

    ''' <summary>
    ''' Konvertuje VB.NET Boolean na SQL bit string
    ''' </summary>
    ''' <param name="value">Číslo na konverziu</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Boolean2Sql(ByVal value As Boolean) As String

        Return If(value, "1", "0")
    End Function
End Class
