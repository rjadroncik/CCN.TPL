Imports System.Runtime.Serialization

<DataContract()>
Public Class Triplet(Of T1, T2, T3)

#Region "Properties"

    <DataMember()>
    Public Property First As T1
    <DataMember()>
    Public Property Second As T2
    <DataMember()>
    Public Property Third As T3

#End Region

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(first As T1, second As T2, third As T3)
        _First = first
        _Second = second
        _Third = third
    End Sub


    Public Sub New(values As Object())

        _First = DirectCast(values(0), T1)
        _Second = DirectCast(values(1), T2)
        _Third = DirectCast(values(2), T3)
    End Sub

#End Region

#Region "Overridden"

    Public Overrides Function Equals(obj As Object) As Boolean

        If (Not TypeOf obj Is Triplet(Of T1, T2, T3)) Then Return False

        Dim other = DirectCast(obj, Triplet(Of T1, T2, T3))

        Return First.Equals(other.First) AndAlso Second.Equals(other.Second) AndAlso Third.Equals(other.Third)
    End Function

    Public Overrides Function GetHashCode() As Integer

        Return (First.GetHashCode() >> 3) + (Second.GetHashCode >> 3) + (Third.GetHashCode >> 3)
    End Function

#End Region

End Class
