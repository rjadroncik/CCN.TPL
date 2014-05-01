Imports CCN.Core.VB
Imports System.Runtime.Serialization

<DataContract()>
Public Class Cinnost
    Inherits DynamicEnum

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)
        MyBase.New(id, nazov)
    End Sub

    Protected Shared Function Create(id As Integer, nazov As String) As Cinnost

        Return New Cinnost(id, nazov)
    End Function

#End Region

#Region "Shared fields"

    Public Shared ReadOnly Zobrazovanie As New Cinnost(1, "Zobrazovanie")

    Public Shared ReadOnly Editovanie As New Cinnost(2, "Editovanie")

    Public Shared ReadOnly Vytvaranie As New Cinnost(3, "Vytváranie")

#End Region

End Class