Namespace Model.Schema

    Public Class KeyUnique
        Inherits ColumnList

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

        Public Property Clustered As Boolean

    End Class
End Namespace