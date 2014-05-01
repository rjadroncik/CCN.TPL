Imports CCN.Core.VB

''' <summary>
''' Nevyuzivaju sa hodnoty enumov, len ich presne nazvy ktore zodpovedaju nazvom v tabulke MaxHod
''' </summary>
''' <remarks></remarks>
Public Class IdMaxHod
    Inherits DynamicEnum

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)
        MyBase.New(id, nazov)
    End Sub

#End Region

End Class
