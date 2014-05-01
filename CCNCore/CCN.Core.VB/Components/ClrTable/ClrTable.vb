Public Class ClrTable

#Region "Properties"

    Protected _columns As IDictionary(Of String, ClrColumn)
    Public ReadOnly Property Columns As IEnumerable(Of ClrColumn)
        Get
            Return _columns.Values
        End Get
    End Property

    Public ReadOnly Property Column(name As String) As ClrColumn
        Get
            Return _columns(name)
        End Get
    End Property

    Public ReadOnly Property ColumnCount As Integer
        Get
            Return _columns.Count
        End Get
    End Property

    Protected _rows As IList(Of ClrRow)
    Public ReadOnly Property Rows As IEnumerable(Of ClrRow)
        Get
            Return _rows
        End Get
    End Property

    Default Public ReadOnly Property Row(index As Integer) As ClrRow
        Get
            Return _rows(index)
        End Get
    End Property

    Public ReadOnly Property RowCount As Integer
        Get
            Return _rows.Count
        End Get
    End Property

    Public ReadOnly Property Initialized() As Boolean
        Get
            Return (Not _columns.IsEmpty()) OrElse (Not _rows.IsEmpty())
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New()

        _columns = New Dictionary(Of String, ClrColumn)()
        _rows = New List(Of ClrRow)()
    End Sub

#End Region

#Region "Modification"

    Public Sub ColumnAdd(name As String, type As Type)

        If (Not _rows.IsEmpty()) Then Throw New InvalidOperationException("Columns can be added only while table is empty!")

        _columns.Add(name, New ClrColumn(Me, name, type, _columns.Count))
    End Sub

    Public Function RowAdd() As ClrRow

        Dim row = New ClrRow(Me)

        _rows.Add(row)

        Return row
    End Function

    Public Sub RowRemoveAt(index As Integer)

        _rows.RemoveAt(index)
    End Sub

    Public Sub RowRemove(row As ClrRow)

        _rows.Remove(row)
    End Sub

#End Region

#Region "Info"

    Public Function ColumnExists(name As String) As Boolean

        Return _columns.ContainsKey(name)
    End Function

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String

        Return _columns.Values.ToStringAll()
    End Function

#End Region

End Class
