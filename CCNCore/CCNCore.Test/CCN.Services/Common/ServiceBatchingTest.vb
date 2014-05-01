Imports CCN.Core.VB
Imports CCN.Model
Imports CCN.Services
Imports NUnit.Framework

<TestFixture()>
Public Class ServiceBatchingTest

#Region "Common"

    Private Shared _values As New List(Of Integer)(1000)

    Private Shared _sum As Integer
    Private Shared _sum1 As Integer
    Private Shared _sum2 As Integer
    Private Shared _sum3 As Integer
    Private Shared _sum4 As Integer

    Private Shared _sumCurrent As Integer

#End Region

#Region "Tests"

    Protected Shared Sub SumValues(values As IEnumerable(Of Integer))

        _sumCurrent += values.Sum()
    End Sub

    Protected Shared Sub SumValues1(values As IEnumerable(Of Integer), a As Integer)

        _sumCurrent += values.Aggregate(0, Function(x, y) x + y + a)
    End Sub

    Protected Shared Sub SumValues2(values As IEnumerable(Of Integer), a As Integer, b As Integer)

        _sumCurrent += values.Aggregate(0, Function(x, y) x + y + a + b)
    End Sub

    Protected Shared Sub SumValues3(values As IEnumerable(Of Integer), a As Integer, b As Integer, c As Integer)

        _sumCurrent += values.Aggregate(0, Function(x, y) x + y + a + b + c)
    End Sub

    Protected Shared Sub SumValues4(values As IEnumerable(Of Integer), a As Integer, b As Integer, c As Integer, d As Integer)

        _sumCurrent += values.Aggregate(0, Function(x, y) x + y + a + b + c + d)
    End Sub

    <Test()>
    Public Shared Sub TestExecuteInBatches()

        _sumCurrent = 0
        Service.ExecuteInBatches(_values, AddressOf SumValues)
        Assert.AreEqual(_sum, _sumCurrent)

        _sumCurrent = 0
        Service.ExecuteInBatches(_values, AddressOf SumValues1, 1)
        Assert.AreEqual(_sum1, _sumCurrent)

        _sumCurrent = 0
        Service.ExecuteInBatches(_values, AddressOf SumValues2, 1, 1)
        Assert.AreEqual(_sum2, _sumCurrent)

        _sumCurrent = 0
        Service.ExecuteInBatches(_values, AddressOf SumValues3, 1, 1, 1)
        Assert.AreEqual(_sum3, _sumCurrent)

        _sumCurrent = 0
        Service.ExecuteInBatches(_values, AddressOf SumValues4, 1, 1, 1, 1)
        Assert.AreEqual(_sum4, _sumCurrent)
    End Sub

#End Region

#Region "SetUp/TearDown"

    <TestFixtureSetUp()>
    Public Shared Sub SetUp()

        _values.Clear()
        _sum = 0
        _sum1 = 0
        _sum2 = 0
        _sum3 = 0
        _sum4 = 0

        For i = 1 To 1000

            _values.Add(i)

            _sum += i
            _sum1 += i + 1
            _sum2 += i + 2
            _sum3 += i + 3
            _sum4 += i + 4
        Next
    End Sub

#End Region

End Class
