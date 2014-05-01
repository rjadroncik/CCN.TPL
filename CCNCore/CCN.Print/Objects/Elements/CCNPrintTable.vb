Imports C1.C1Preview

Public Class CCNPrintTable
    Inherits CCNPrintRectangle

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Throw New NotSupportedException("Kopírovanie tabuliek ešte nie je implementované!")
    End Function

#End Region

#Region "Initialization"

    Protected _capacityCols As Integer
    Public Sub New(ByVal document As CCNPrintDocument, _
                   Optional capacityRows As Integer = 100, _
                   Optional capacityCols As Integer = 10)

        MyBase.New(document)

        _header = New CCNPrintTableHeader(Me)
        _rows = New List(Of CCNPrintTableRow)()

        _capacityCols = capacityCols
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As RenderObject

        Dim table As New RenderTable()
        _c1Object = table

        ApplyRectangleProperties(table)

        table.SplitVertBehavior = SplitBehaviorEnum.SplitIfNeeded

        '--------------------------- COLUMNS ---------------------------------

        table.Cols.Count = _columns.Count

        For i As Integer = 0 To _columns.Count - 1

            Dim column As CCNPrintTableColumn = _columns(i)

            column.ApplyStyle(table.Cols(i))
        Next

        '--------------------------- HEADER ----------------------------------

        table.Rows.Count = _rows.Count + _header.RowsCount

        For i As Integer = 0 To _header.RowsCount - 1

            Dim row As CCNPrintTableRow = _header.Rows(i)

            row.ApplyStyle(table.Rows(i))

            For j As Integer = 0 To row.CellCount - 1

                Dim cell As CCNPrintTableCell = row.Cells(j)

                table.Rows(i).Item(j).Text = cell.Text

                cell.ApplyStyle(table.Rows(i).Item(j))
            Next
        Next

        If (_header.RowsCount > 0) Then table.RowGroups(0, _header.RowsCount).Header = If(_header.OnEachPage, TableHeaderEnum.All, TableHeaderEnum.None)

        '----------------------------- ROWS ----------------------------------

        For i As Integer = _header.RowsCount To _rows.Count + _header.RowsCount - 1

            Dim row As CCNPrintTableRow = _rows(i - _header.RowsCount)

            row.ApplyStyle(table.Rows(i))

            For j As Integer = 0 To row.CellCount - 1

                Dim cell As CCNPrintTableCell = row.Cells(j)

                table.Rows(i).Item(j).Text = cell.Text

                cell.ApplyStyle(table.Rows(i).Item(j))
            Next
        Next

        If (Not IsNothing(_parent)) Then

            _parent.C1Object.Children.Add(table)
        Else
            _document.C1Document.Body.Children.Add(table)
        End If

        If ((_MinTableWrapRows > 0) AndAlso (table.Rows.Count > _MinTableWrapRows)) Then

            table.Rows(table.Rows.Count - _MinTableWrapRows).PageBreakBehavior = PageBreakBehaviorEnum.PreferredBreak
        End If

        Return table
    End Function

#End Region

#Region "Properties"

    Private _header As CCNPrintTableHeader
    Public Property Header() As CCNPrintTableHeader
        Get
            Return _header
        End Get
        Set(ByVal value As CCNPrintTableHeader)
            _header = Header
        End Set
    End Property

    Private _columnsByName As New Dictionary(Of String, Integer)
    Public ReadOnly Property Columns(ByVal columnId As String) As CCNPrintTableColumn
        Get
            Return _columns(_columnsByName(columnId))
        End Get
    End Property

    Private _columns As New List(Of CCNPrintTableColumn)
    Public ReadOnly Property Columns(ByVal columnIndex As Integer) As CCNPrintTableColumn
        Get
            Return _columns(columnIndex)
        End Get
    End Property

    Public ReadOnly Property ColumnCount() As Integer
        Get
            Return _columns.Count
        End Get
    End Property

    Private _rows As List(Of CCNPrintTableRow)
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

    Public Property MinTableWrapRows() As Integer = 2

#End Region

#Region "Editing"

    Public Function RowAdd() As CCNPrintTableRow

        Dim row As New CCNPrintTableRow(Me, _capacityCols)

        _rows.Add(row)

        Return row
    End Function

    Public Function ColumnAdd(ByVal name As String) As CCNPrintTableColumn

        If (_columnsByName.ContainsKey(name)) Then Throw New ArgumentException("Stlpec s nazvom: " & name & " uz existuje")

        Dim column As New CCNPrintTableColumn(Me, name)

        _columnsByName.Add(name, _columns.Count)
        _columns.Add(column)

        For Each row As CCNPrintTableRow In _rows

            row.CellAdd(name, column)
        Next

        For i As Integer = 0 To _header.RowsCount - 1

            _header.Rows(i).CellAdd(name, column)
        Next

        Return column
    End Function

#End Region

End Class
