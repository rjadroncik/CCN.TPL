Imports C1.C1Preview

Public Class CCNPrintTableRow
    Inherits CCNPrintTableVector

#Region "Initialization"

    Public Sub New(ByVal table As CCNPrintTable, Optional capacity As Integer = 10)
        MyBase.New(table)

        _cellsByName = New Dictionary(Of String, Integer)(capacity)
        _cells = New List(Of CCNPrintTableCell)(capacity)

        For i As Integer = 0 To _table.ColumnCount - 1

            Dim cell As New CCNPrintTableCell(_table)

            _cellsByName.Add(_table.Columns(i).Name, _cells.Count)
            _cells.Add(cell)
        Next
    End Sub

#End Region

#Region "Properties"

    Private _cellsByName As Dictionary(Of String, Integer)
    Public ReadOnly Property Cells(ByVal columnId As String) As CCNPrintTableCell
        Get
            Return _cells(_cellsByName(columnId))
        End Get
    End Property

    Private _cells As List(Of CCNPrintTableCell)
    Public ReadOnly Property Cells(ByVal columnIndex As Integer) As CCNPrintTableCell
        Get
            Return _cells(columnIndex)
        End Get
    End Property

    Public ReadOnly Property CellCount() As Integer
        Get
            Return _cells.Count
        End Get
    End Property

    Protected _height As Double = 0
    Public Overridable Property Height() As Double
        Get
            Return _height
        End Get
        Set(ByVal value As Double)
            _height = value

            If (Not _propertiesChanged.Contains(Properties.Height)) Then _propertiesChanged.Add(Properties.Height)
        End Set
    End Property

#End Region

#Region "Editing"

    Public Sub CellAdd(ByVal columnName As String, ByVal column As CCNPrintTableColumn)

        Dim cell As New CCNPrintTableCell(_table)

        _cellsByName.Add(columnName, _cells.Count)
        _cells.Add(cell)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Sub ApplyStyle(ByVal vector As TableVector)

        If (_propertiesChanged.Contains(Properties.Height)) Then

            vector.Size = New Unit(_height, UnitTypeEnum.Mm)
        Else
            vector.SizingMode = TableSizingModeEnum.Auto
        End If

        MyBase.ApplyStyle(vector)

        If (_propertiesChanged.Contains(Properties.BackgroundColor)) Then vector.CellStyle.BackColor = _backgroundColor
    End Sub

#End Region

End Class
