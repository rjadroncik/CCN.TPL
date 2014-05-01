Imports CCN.Core.VB
Imports NUnit.Framework
Imports NUnit.Mocks
Imports System.Globalization

<TestFixture()>
Public Class DynamicEnumTest

#Region "Model 1"

    Public Class Enum1Base
        Inherits DynamicEnum

#Region "Initialization"

        Protected Sub New()
        End Sub

        Protected Sub New(id As Integer, nazov As String)
            MyBase.New(id, nazov)
        End Sub

#End Region

#Region "Shared fields"

        Public Shared ReadOnly Enum1BaseValue1 As New Enum1Base(1, "Enum1BaseValue1")
        Public Shared ReadOnly Enum1BaseValue2 As New Enum1Base(2, "Enum1BaseValue2")

#End Region

    End Class

    Public Class Enum1Derived
        Inherits Enum1Base

#Region "Initialization"

        Protected Sub New()
        End Sub

        Protected Sub New(id As Integer, nazov As String)
            MyBase.New(id, nazov)
        End Sub

#End Region

#Region "Shared fields"

        Public Shared ReadOnly Enum1DerivedValue1 As New Enum1Base(3, "Enum1DerivedValue1")
        Public Shared ReadOnly Enum1DerivedValue2 As New Enum1Base(4, "Enum1DerivedValue2")

#End Region

    End Class

#End Region

#Region "Model 2"

    Public Class Enum2Base
        Inherits DynamicEnum

#Region "Initialization"

        Protected Sub New()
        End Sub

        Protected Sub New(id As Integer, nazov As String)
            MyBase.New(id, nazov)
        End Sub

#End Region

#Region "Shared fields"

        Public Shared ReadOnly Enum2BaseValue1 As New Enum2Base(1, "Enum2BaseValue1")

#End Region

    End Class

    Public Class Enum2Derived
        Inherits Enum2Base

#Region "Initialization"

        Protected Sub New()
        End Sub

        Protected Sub New(id As Integer, nazov As String)
            MyBase.New(id, nazov)
        End Sub

#End Region

#Region "Shared fields"

        Public Shared ReadOnly Enum2DerivedValueConficting As New Enum2Base(1, "Enum2DerivedValueConficting")

#End Region

    End Class

#End Region

#Region "Tests"

    <Test()>
    Public Sub TestDynamicEnum1Initialization()

        Assert.AreEqual(1, Enum1Derived.Enum1BaseValue1.Id)
        Assert.AreEqual(2, Enum1Derived.Enum1BaseValue2.Id)
        Assert.AreEqual(3, Enum1Derived.Enum1DerivedValue1.Id)
        Assert.AreEqual(4, Enum1Derived.Enum1DerivedValue2.Id)
    End Sub

    <Test()>
    Public Sub TestDynamicEnum2InitializationConflicting()

        Dim caught = False
        Try
            Dim unused = Enum2Derived.Enum2DerivedValueConficting

        Catch ex As TypeInitializationException

            caught = True
        End Try

        Assert.IsTrue(caught)
    End Sub

#End Region

End Class
