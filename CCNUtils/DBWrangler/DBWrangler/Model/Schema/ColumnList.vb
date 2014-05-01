Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema

    Public MustInherit Class ColumnList
        Inherits TableElement

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

        Protected _columns As New List(Of Column)
        Public ReadOnly Property Columns() As IList(Of Column)
            Get
                Return _columns
            End Get
        End Property
    End Class
End Namespace