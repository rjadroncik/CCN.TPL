Imports C1.C1Preview

Public Class CCNPrintTableColumn
    Inherits CCNPrintTableVector

#Region "Initialization"

    Public Sub New(ByVal table As CCNPrintTable, ByVal name As String)
        MyBase.New(table)
        _name = name
    End Sub

#End Region

#Region "Properties"

    Private _name As String
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Protected _width As Double = 0
    Public Overridable Property Width() As Double
        Get
            Return _width
        End Get
        Set(ByVal value As Double)
            _width = value

            If (Not _propertiesChanged.Contains(Properties.Width)) Then _propertiesChanged.Add(Properties.Width)
        End Set
    End Property

#End Region

#Region "Rendering"

    Public Overrides Sub ApplyStyle(ByVal vector As TableVector)

        If (_propertiesChanged.Contains(Properties.Width)) Then

            vector.Size = New Unit(_width, UnitTypeEnum.Mm)
        Else
            vector.SizingMode = TableSizingModeEnum.Auto
        End If

        MyBase.ApplyStyle(vector)
    End Sub

#End Region

End Class
