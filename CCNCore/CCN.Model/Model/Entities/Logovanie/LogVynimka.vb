Imports System.Runtime.Serialization

<DataContract()>
Public Class LogVynimka

    Protected Sub New()
    End Sub

    Public Shared Function Create() As LogVynimka

        Return New LogVynimka() With {.Udaje = New List(Of LogVynimkaUdaj)}
    End Function

    <DataMember(), Permanent()>
    Public Overridable Property Id As Integer

    <DataMember(), Permanent()>
    Public Overridable Property Typ As String

    <DataMember(), Permanent()>
    Public Overridable Property Sprava As String
    <DataMember(), Permanent()>
    Public Overridable Property AkoText As String

    <DataMember(), Permanent()>
    Public Overridable Property Zdroj As String
    <DataMember(), Permanent()>
    Public Overridable Property Trieda As String
    <DataMember(), Permanent()>
    Public Overridable Property Metoda As String

    <DataMember(), Permanent()>
    Public Overridable Property StackTrace As String

    <DataMember(), Permanent()>
    Public Overridable Property Vnutorna As LogVynimka

    <DataMember(), Permanent()>
    Public Overridable Property Udaje As IList(Of LogVynimkaUdaj)
End Class
