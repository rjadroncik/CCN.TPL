Public Class CCNPrintTableHeader

#Region "Properties"

    Protected _table As CCNPrintTable
    Public Sub New(ByVal table As CCNPrintTable)

        _table = table
    End Sub

    Public Property OnEachPage As Boolean

    Private _rows As New List(Of CCNPrintTableRow)
    Public ReadOnly Property Rows(ByVal rowIndex As Integer) As CCNPrintTableRow
        Get
            Return _rows(rowIndex)
        End Get
    End Property

    Public ReadOnly Property RowsCount() As Integer
        Get
            Return _rows.Count
        End Get
    End Property

#End Region

#Region "Editing"

    Public Function RowAdd() As CCNPrintTableRow

        Dim row As New CCNPrintTableRow(_table)

        _rows.Add(row)

        Return row
    End Function

#End Region

End Class
