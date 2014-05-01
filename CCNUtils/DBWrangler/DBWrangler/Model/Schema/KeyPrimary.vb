Imports CCN.Core.VB

Namespace Model.Schema

    Public Class KeyPrimary
        Inherits KeyUnique

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

        Public ReadOnly Property HasIdentity() As Boolean
            Get
                If (_columns.IsEmpty()) Then Return False

                For Each column As Column In _columns

                    If (column.Identity) Then Return True
                Next

                Return False
            End Get
        End Property
    End Class
End Namespace