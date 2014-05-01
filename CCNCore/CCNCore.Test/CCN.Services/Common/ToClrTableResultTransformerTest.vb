Imports CCN.Core.VB
Imports CCN.Model
Imports CCN.Services
Imports NUnit.Framework
Imports System.Globalization
Imports FluentNHibernate.Mapping

<TestFixture()>
Public Class ToClrTableResultTransformerTest
    Inherits NHTestFixtureBase

#Region "Test model"

    Protected Enum SampleEnum

        EnumValue1 = 1
        EnumValue2 = 2
    End Enum

    Protected Class TestClass
        Inherits NHEntita

        Public Overridable Property Datum As DateTime

        Public Overridable Property Text As String
        Public Overridable Property Cislo As Decimal?

        Public Overridable Property Ciselnik As SampleEnum
    End Class

    Protected Class TestClassMap
        Inherits ClassMap(Of TestClass)

        Public Sub New()

            Table("objTest")
            Id(Function(x) x.Id).GeneratedBy.Native().Column("id")

            Map(Function(x) x.Datum).Not.Nullable().Column("datum")
            Map(Function(x) x.Text).Not.Nullable().Column("text").Length(64)
            Map(Function(x) x.Cislo).Nullable().Column("cislo")

            Map(Function(x) x.Ciselnik).Nullable().Column("ciselnik").CustomType(Of SampleEnum).CustomSqlType(DBTypes.Int())
        End Sub
    End Class

#End Region

#Region "Setup/TearDown"

    <TestFixtureSetUp()>
    Public Sub SetUp()

        NHSetUp(GetType(TestClass).Assembly)
    End Sub

    <TestFixtureTearDown()>
    Public Sub TearDown()

        NHTearDown()
    End Sub

#End Region

    <Test()>
    Public Sub TestListClrRows()

        Using session = Service.NewSession()

            Dim datum = New DateTime(2003, 4, 5, 6, 7, 8)

            Dim objektWritten = New TestClass()
            With objektWritten

                .Datum = datum
                .Text = "test"
                .Cislo = 1002.3D
                .Ciselnik = SampleEnum.EnumValue1
            End With

            Using transaction = session.BeginTransaction()

                session.Save(objektWritten)
                transaction.Commit()
            End Using

            For Each row In session.ListClrRows("SELECT * FROM objTest")

                Dim objektRead = New TestClass()
                With objektRead

                    .Id = CInt(row("id"))
                    .Datum = CDate(row("datum"))
                    .Text = CStr(row("text"))
                    .Cislo = CDec(row("cislo"))
                    .Ciselnik = CType(row("ciselnik"), SampleEnum)
                End With

                Assert.AreEqual(objektWritten.Id, objektRead.Id)
                Assert.AreEqual(objektWritten.Datum, objektRead.Datum)
                Assert.AreEqual(objektWritten.Text, objektRead.Text)
                Assert.AreEqual(objektWritten.Cislo, objektRead.Cislo)
                Assert.AreEqual(objektWritten.Ciselnik, objektRead.Ciselnik)
            Next
        End Using
    End Sub

End Class
