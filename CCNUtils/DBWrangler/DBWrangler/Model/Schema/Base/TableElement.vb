Namespace Model.Schema.Base

    Public MustInherit Class TableElement
        Inherits Element

#Region "Properties"

        Protected _table As Table
        Public ReadOnly Property Table As Table
            Get
                Return _table
            End Get
        End Property

        Protected Sub New(table As Table)

            _table = table
        End Sub

#End Region

    End Class
End Namespace