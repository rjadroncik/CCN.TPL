Imports CCN.Core.VB
Imports NUnit.Framework
Imports NUnit.Mocks
Imports System.Globalization

<TestFixture()>
Public Class ClrTableTest

    Private Enum SampleEnum

        EnumValue1 = 1
        EnumValue2 = 2
    End Enum

    <Test()>
    Public Sub TestTable()

        Dim table As New ClrTable()
        With table
            .ColumnAdd("column1", GetType(String))
            .ColumnAdd("column2", GetType(Integer))
            .ColumnAdd("column3", GetType(Date))
            .ColumnAdd("column4", GetType(Decimal))
            .ColumnAdd("column5", GetType(SampleEnum))
        End With

        Assert.AreEqual(5, table.ColumnCount)

        Assert.AreEqual(GetType(String), table.Column("column1").DataType)
        Assert.AreEqual(GetType(Integer), table.Column("column2").DataType)
        Assert.AreEqual(GetType(Date), table.Column("column3").DataType)
        Assert.AreEqual(GetType(Decimal), table.Column("column4").DataType)
        Assert.AreEqual(GetType(SampleEnum), table.Column("column5").DataType)

        With table.RowAdd()
            .Item("column1") = "test"
            .Item("column2") = 1
            .Item("column3") = New Date(2011, 2, 3, 4, 5, 6, 7)
            .Item("column4") = 1001.2D
            .Item("column5") = SampleEnum.EnumValue1
        End With

        For Each row In table.Rows

            Assert.AreEqual("test", row("column1"))
            Assert.AreEqual(1, row("column2"))
            Assert.AreEqual(New Date(2011, 2, 3, 4, 5, 6, 7), row("column3"))
            Assert.AreEqual(1001.2D, row("column4"))
            Assert.AreEqual(SampleEnum.EnumValue1, row("column5"))
        Next
    End Sub

End Class