Imports CCN.Core.VB
Imports System.Runtime.Serialization

<DataContract()>
Public Class ObjektTyp
    Inherits DynamicEnum

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)
        MyBase.New(id, nazov)
    End Sub

    Protected Shared Function Create(id As Integer, nazov As String) As ObjektTyp

        Return New ObjektTyp(id, nazov)
    End Function

#End Region

End Class