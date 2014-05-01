Namespace Model.Schema

    Public Class Index
        Inherits ColumnList

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

        Public Property Unique As Boolean
        Public Property Clustered As Boolean
    End Class
End Namespace