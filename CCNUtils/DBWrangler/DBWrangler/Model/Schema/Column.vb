Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema

    Public Class Column
        Inherits TableElement

#Region "Initialization"

        Public Sub New(table As Table, dataType As DataType, Optional nullable As Boolean = True)
            MyBase.New(table)

            _DataType = dataType
            _Nullable = nullable
        End Sub

        Public Sub New(name As String)
            MyBase.New(Nothing)

            Me.Name = name
        End Sub

#End Region

#Region "Properties"

        Public Property DataType As DataType

        Public Property Nullable As Boolean = True
        Public Property Identity As Boolean

        Public Property IdentitySeed As Integer
        Public Property IdentityIncrement As Integer

#End Region

#Region "Overridden"

        Public Overrides Function ToString() As String

            Return MyBase.ToString() & " " & DataType.ToString() & If(Nullable, " NULL", "") & _
                   If(Identity, " Identity(" & IdentitySeed & ", " & IdentityIncrement & ")", "")
        End Function

#End Region

    End Class
End Namespace