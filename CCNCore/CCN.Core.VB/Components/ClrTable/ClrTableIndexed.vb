Public Class ClrTableIndexed(Of T)
    Inherits ClrTable

#Region "Properties"

    Private _rowsBy As IDictionary(Of T, ClrRow)

    Public ReadOnly Property RowBy(index As T) As ClrRow
        Get
            Return _rowsBy(index)
        End Get
    End Property

    Public ReadOnly Property Contains(index As T) As Boolean
        Get
            Return _rowsBy.ContainsKey(index)
        End Get
    End Property

#End Region

#Region "Tasks"

    Public Sub BuildIndex(keySelector As Func(Of ClrRow, T))

        _rowsBy = New Dictionary(Of T, ClrRow)()

        For Each riadok In _rows

            _rowsBy.Add(keySelector(riadok), riadok)
        Next
    End Sub

#End Region

End Class
