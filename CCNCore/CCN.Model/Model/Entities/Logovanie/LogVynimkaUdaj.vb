Imports System.Runtime.Serialization

<DataContract()>
Public Class LogVynimkaUdaj

    Protected Sub New()
    End Sub

    Public Shared Function Create() As LogVynimkaUdaj

        Return New LogVynimkaUdaj()
    End Function

    <DataMember(), Permanent()>
    Public Overridable Property Id As Integer
    <DataMember(), Permanent()>
    Public Overridable Property Vynimka As LogVynimka

    <DataMember(), Permanent()>
    Public Overridable Property Kluc As String
    <DataMember(), Permanent()>
    Public Overridable Property Hodnota As String
End Class
