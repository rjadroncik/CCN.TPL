Imports CCN.Core.VB
Imports System.Runtime.Serialization

<DataContract()>
Public Class IdPrava
    Inherits DynamicEnum

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)
        MyBase.New(id, nazov)
    End Sub

    Protected Shared Function Create(id As Integer, nazov As String) As IdPrava

        Return New IdPrava(id, nazov)
    End Function

#End Region

#Region "Shared fields"

    Public Shared ReadOnly Nastavenia As New IdPrava(15, "Nastavenia")

    Public Shared ReadOnly EditaciaPouzivatela As New IdPrava(99, "Editácia užívateľa")

    Public Shared ReadOnly Debug As New IdPrava(101, "Debugovacie nástroje")

#End Region

End Class