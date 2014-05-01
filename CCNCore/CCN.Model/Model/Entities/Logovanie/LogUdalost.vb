Imports System.Runtime.Serialization
Imports CCN.Core.VB

<DataContract()>
Public Class LogUdalost
    Inherits Log

    Protected Sub New()
    End Sub

    Public Shared Function Create(datum As DateTime) As LogUdalost

        Dim log As New LogUdalost()

        log.Datum = datum

        Return log
    End Function

    <DataMember(), Permanent()>
    Public Overridable Property Typ As TypUdalosti

End Class
