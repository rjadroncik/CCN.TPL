Public Class ClrColumn
    Inherits ClrTableElement

#Region "Properties"

    Protected _name As String
    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Protected _dataType As Type
    Public ReadOnly Property DataType As Type
        Get
            Return _dataType
        End Get
    End Property

    Protected _index As Integer
    Public ReadOnly Property Index As Integer
        Get
            Return _index
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New(name As String, dataType As Type)

        _name = name
        _dataType = dataType
    End Sub

    Sub New(table As ClrTable, name As String, dataType As Type, index As Integer)

        _table = table
        _name = name
        _dataType = dataType
        _index = index
    End Sub

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String

        Return _name & " [" & _dataType.Name & "]"
    End Function

#End Region

End Class

