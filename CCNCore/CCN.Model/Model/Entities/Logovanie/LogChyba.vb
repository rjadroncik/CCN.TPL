Imports System.Runtime.Serialization

<DataContract()>
Public Class LogChyba
    Inherits Log

    Protected Sub New()
    End Sub

    Public Shared Function Create(datum As DateTime) As LogChyba

        Dim log As New LogChyba()

        log.Datum = datum

        Return log
    End Function

    <DataMember(), Permanent()>
    Public Overridable Property Trieda As String
    <DataMember(), Permanent()>
    Public Overridable Property Metoda As String

    <DataMember(), Permanent()>
    Public Overridable Property Vynimka As LogVynimka
End Class
