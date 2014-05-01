Imports CCN.Core.VB
Imports System.Runtime.Serialization

<DataContract()>
Public Class IdNastavenia
    Inherits DynamicEnum

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)
        MyBase.New(id, nazov)
    End Sub

    Protected Shared Function Create(id As Integer, nazov As String) As IdNastavenia

        Return New IdNastavenia(id, nazov)
    End Function

#End Region

#Region "Shared fields"

    Public Shared ReadOnly GridRefreshTimeout As New IdNastavenia(201, "Grid - refresh timeout")
    Public Shared ReadOnly GridSearchTimeout As New IdNastavenia(203, "Grid - search timeout")

    Public Shared ReadOnly BGWorkTimeout As New IdNastavenia(202, "Background work timeout")

#End Region

End Class
