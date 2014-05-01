Imports C1.Win.C1FlexGrid

Public Class FlexGridOverlay

    Private _column As Column
    Public Property Column As Column
        Get
            Return _column
        End Get
        Set(value As Column)

            _column = value
            Group.Text = _column.Caption
        End Set
    End Property

End Class