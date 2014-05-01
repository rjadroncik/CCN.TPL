Imports System.Runtime.Serialization

<DataContract()>
Public Class Pair(Of T1, T2)

#Region "Properties"

    <DataMember()>
    Public Property First As T1
    <DataMember()>
    Public Property Second As T2

#End Region

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(first As T1, second As T2)
        _First = first
        _Second = second
    End Sub

    Public Sub New(values As Object())

        _First = DirectCast(values(0), T1)
        _Second = DirectCast(values(1), T2)
    End Sub

#End Region

#Region "Overridden"

    Public Overrides Function Equals(obj As Object) As Boolean

        If (Not TypeOf obj Is Pair(Of T1, T2)) Then Return False

        Dim other = DirectCast(obj, Pair(Of T1, T2))

        Return First.Equals(other.First) AndAlso Second.Equals(other.Second)
    End Function

    Public Overrides Function GetHashCode() As Integer

        Return (First.GetHashCode() >> 2) + (Second.GetHashCode >> 2)
    End Function

#End Region

End Class
