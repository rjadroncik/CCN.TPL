Imports CCN.Core.VB
Imports NUnit.Framework
Imports System.Globalization

<TestFixture()>
Public Class ConvertingTest

#Region "String"

    <Test()>
    Public Shared Sub TestTrimOrEmpty()

        Assert.AreEqual("", Converting.TrimOrEmpty(Nothing))
        Assert.AreEqual("test", Converting.TrimOrEmpty(" test "))
    End Sub

    <Test()>
    Public Shared Sub TestTrimOrNothing()

        Assert.AreEqual(Nothing, Converting.TrimOrNothing(Nothing))
        Assert.AreEqual("test", Converting.TrimOrEmpty(" test "))
    End Sub

    <Test()>
    Public Shared Sub TestToStringOrNothing()

        Dim objects = SampleObjects()
        Dim values = SampleObjectsToStringOrNothing()

        For i = 0 To objects.Count() - 1

            Assert.AreEqual(values(i), Converting.ToStringOrNothing(objects(i)))
        Next
    End Sub

    <Test()>
    Public Shared Sub TestToStringInvariant()

        Dim objects = SampleFormattables()
        Dim values = SampleFormattablesToStringInvariant()

        For i = 0 To objects.Count() - 1

            Assert.AreEqual(values(i), Converting.ToStringInvariant(objects(i)))
        Next
    End Sub

    <Test()>
    Public Shared Sub TestToStringInvariantOrNothing()

        Dim objects = SampleFormattables()
        Dim values = SampleFormattablesToStringInvariantOrNothing()

        For i = 0 To objects.Count() - 1

            Assert.AreEqual(values(i), Converting.ToStringInvariantOrNothing(objects(i)))
        Next
    End Sub

    <Test()>
    Public Shared Sub TestToStringTrimOrEmpty()

        Dim objects = SampleObjects()
        Dim values = SampleObjectsToStringTrimOrEmpty()

        For i = 0 To objects.Count() - 1

            Assert.AreEqual(values(i), Converting.ToStringTrimOrEmpty(objects(i)))
        Next
    End Sub

#End Region

#Region "Lists - IEnumerable"

    <Test()>
    Public Shared Sub TestValues2String()

        Assert.AreEqual("1.00, 2000.50, 0.00", Converting.Values2String(New Decimal() {1, 2000.5D, 0}, Function(x) x.ToString("0.00", CultureInfo.InvariantCulture)))
        Assert.AreEqual("1.00|2000.50|0.00", Converting.Values2String(New Decimal() {1, 2000.5D, 0}, Function(x) x.ToString("0.00", CultureInfo.InvariantCulture), "|"))
    End Sub

    <Test()>
    Public Shared Sub TestValues2StringInvariant()

        Assert.AreEqual("1, 2000.5", Converting.Values2StringInvariant(New Decimal() {1, 2000.5D}))
        Assert.AreEqual("1|2000.5", Converting.Values2StringInvariant(New Decimal() {1, 2000.5D}, "|"))
    End Sub

#End Region

#Region "Sample data"

    Private Enum SampleEnum

        EnumValue1 = 1
        EnumValue2 = 2
    End Enum

    Private Shared Function SampleObjects() As Object()

        Return New Object() {Nothing, 1, 1.1D, String.Empty, "test", " test ", SampleEnum.EnumValue1, New DateTime(2001, 2, 3, 4, 5, 6, 7)}
    End Function

    Private Shared Function SampleObjectsToStringInvariant() As String()

        Return New String() {Nothing, "1", "1.1", String.Empty, "test", " test ", "EnumValue1", New DateTime(2001, 2, 3, 4, 5, 6, 7).ToStringInvariant()}
    End Function

    Private Shared Function SampleObjectsToStringOrNothing() As String()

        Return New String() {Nothing, 1.ToString(), 1.1D.ToString(), String.Empty, "test", " test ", "EnumValue1", New DateTime(2001, 2, 3, 4, 5, 6, 7).ToString()}
    End Function

    Private Shared Function SampleObjectsToStringTrimOrEmpty() As String()

        Return New String() {String.Empty, 1.ToString(), 1.1D.ToString(), String.Empty, "test", "test", "EnumValue1", New DateTime(2001, 2, 3, 4, 5, 6, 7).ToString()}
    End Function

    Private Shared Function SampleFormattables() As IFormattable()

        Return New IFormattable() {Nothing, 1, 1.1D, SampleEnum.EnumValue1, New DateTime(2001, 2, 3, 4, 5, 6, 7)}
    End Function

    Private Shared Function SampleFormattablesToStringInvariant() As String()

        Return New String() {String.Empty, "1", "1.1", "EnumValue1", New DateTime(2001, 2, 3, 4, 5, 6, 7).ToStringInvariant()}
    End Function

    Private Shared Function SampleFormattablesToStringInvariantOrNothing() As String()

        Return New String() {Nothing, "1", "1.1", "EnumValue1", New DateTime(2001, 2, 3, 4, 5, 6, 7).ToStringInvariant()}
    End Function

#End Region

End Class
