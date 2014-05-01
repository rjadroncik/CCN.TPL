Public Class ClrGroupedTable
    Inherits ClrTable

    Protected _groups As IDictionary(Of String, ClrColumnGroup)
    Public ReadOnly Property Groups As IEnumerable(Of ClrColumnGroup)
        Get
            Return _groups.Values
        End Get
    End Property

    Public ReadOnly Property Group(name As String) As ClrColumnGroup
        Get
            Return _groups(name)
        End Get
    End Property

    Public Sub New()

        MyBase.New()

        _groups = New Dictionary(Of String, ClrColumnGroup)

    End Sub

    Public Sub GroupAdd(name As String)
        If (Not _rows.IsEmpty()) Then Throw New InvalidOperationException("Groups can be added only while table is empty!")

        _groups.Add(name, New ClrColumnGroup(Me, name))

    End Sub


    Public Overloads Sub ColumnAdd(name As String, group As ClrColumnGroup, type As System.Type)

        MyBase.ColumnAdd(name, type)
        group.ColumnAdd(Column(name))

    End Sub


End Class
