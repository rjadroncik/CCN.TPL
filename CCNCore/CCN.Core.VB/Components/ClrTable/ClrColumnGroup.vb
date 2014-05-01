Public Class ClrColumnGroup
    Inherits ClrTableElement
#Region "Properties"

    Protected _name As String
    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Protected _columns As IDictionary(Of String, ClrColumn)
    Public ReadOnly Property Columns As IEnumerable(Of ClrColumn)
        Get
            Return _columns.Values
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New(table As ClrTable, name As String)

        _name = name
        _table = table

        _columns = New Dictionary(Of String, ClrColumn)

    End Sub

#End Region

    Public Sub ColumnAdd(column As ClrColumn)
        _columns.Add(column.Name, column)
    End Sub


End Class
