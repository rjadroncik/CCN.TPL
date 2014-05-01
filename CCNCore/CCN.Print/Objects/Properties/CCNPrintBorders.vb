Imports System.ComponentModel

Public Class CCNPrintBorders

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(ByVal source As CCNPrintBorders)

        _left = New CCNPrintBorder(source._left)
        _top = New CCNPrintBorder(source._top)
        _right = New CCNPrintBorder(source._right)
        _bottom = New CCNPrintBorder(source._bottom)
    End Sub

#End Region

#Region "Properties"

    Protected _left As New CCNPrintBorder
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie ľavého okraja objektu."), _
    DisplayName("Ľavý"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Left() As CCNPrintBorder
        Get
            Return _left
        End Get
    End Property

    Protected _top As New CCNPrintBorder
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie horného okraja objektu."), _
    DisplayName("Horný"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Top() As CCNPrintBorder
        Get
            Return _top
        End Get
    End Property

    Protected _right As New CCNPrintBorder
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie pravého okraja objektu."), _
    DisplayName("Pravý"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Right() As CCNPrintBorder
        Get
            Return _right
        End Get
    End Property

    Protected _bottom As New CCNPrintBorder
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie spodného okraja objektu."), _
    DisplayName("Spodný"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Bottom() As CCNPrintBorder
        Get
            Return _bottom
        End Get
    End Property

#End Region

End Class