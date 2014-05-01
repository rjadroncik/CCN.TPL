Imports System.ComponentModel

Public Class CCNPrintGridLines

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(ByVal source As CCNPrintGridLines)

        _left = New CCNPrintGridLine(source._left)
        _top = New CCNPrintGridLine(source._top)
        _right = New CCNPrintGridLine(source._right)
        _bottom = New CCNPrintGridLine(source._bottom)
        _vertical = New CCNPrintGridLine(source._vertical)
        _horizontal = New CCNPrintGridLine(source._horizontal)
    End Sub

#End Region

#Region "Properties"

    Protected _left As New CCNPrintGridLine
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie ľavého okraja objektu."), _
    DisplayName("Ľavý"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Left As CCNPrintGridLine
        Get
            Return _left
        End Get
    End Property

    Protected _top As New CCNPrintGridLine
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie horného okraja objektu."), _
    DisplayName("Horný"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Top As CCNPrintGridLine
        Get
            Return _top
        End Get
    End Property

    Protected _right As New CCNPrintGridLine
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie pravého okraja objektu."), _
    DisplayName("Pravý"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Right As CCNPrintGridLine
        Get
            Return _right
        End Get
    End Property

    Protected _bottom As New CCNPrintGridLine
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie spodného okraja objektu."), _
    DisplayName("Spodný"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Bottom As CCNPrintGridLine
        Get
            Return _bottom
        End Get
    End Property

    Protected _vertical As New CCNPrintGridLine
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie vertikálnych čiar tabuľky."), _
    DisplayName("Spodný"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Vertical As CCNPrintGridLine
        Get
            Return _vertical
        End Get
    End Property

    Protected _horizontal As New CCNPrintGridLine
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Nastavenie horizontálnych čiar tabuľky."), _
    DisplayName("Spodný"), _
    TypeConverter(GetType(CCNBorderConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public ReadOnly Property Horizontal As CCNPrintGridLine
        Get
            Return _horizontal
        End Get
    End Property

#End Region

End Class