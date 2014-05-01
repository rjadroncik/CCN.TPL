Namespace Model.Schema.Base

    Public MustInherit Class Element

#Region "Properties"

        Public Property Name As String

#End Region

#Region "Overridden"

        Public Overrides Function ToString() As String

            Return Name
        End Function

#End Region

    End Class
End Namespace