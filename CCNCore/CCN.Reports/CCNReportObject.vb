Imports System.Drawing
Imports System.ComponentModel
Imports CCN.Print

Public Class CCNReportObject

#Region "Properties"

    Protected _x As Single
    <Category("Pozícia a ve¾kos"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Vzdialenos ¾aveho okraja objektu od ¾avého okraja dokumentu v milimetroch."), _
    TypeConverter(GetType(CCNFloatConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property X() As Single
        Get
            Return _x
        End Get
        Set(ByVal value As Single)
            _x = value
        End Set
    End Property

    Protected _y As Single
    <Category("Pozícia a ve¾kos"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Vzdialenos horného okraja objektu od horného okraja dokumentu v milimetroch."), _
    TypeConverter(GetType(CCNFloatConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property Y() As Single
        Get
            Return _y
        End Get
        Set(ByVal value As Single)
            _y = value
        End Set
    End Property

    Protected _w As Single
    <Category("Pozícia a ve¾kos"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Šírka objektu v milimetroch."), _
    DisplayName("Šírka"), _
    TypeConverter(GetType(CCNFloatConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property W() As Single
        Get
            Return _w
        End Get
        Set(ByVal value As Single)
            _w = value
        End Set
    End Property

    Protected _h As Single
    <Category("Pozícia a ve¾kos"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Výška objektu v milimetroch."), _
    DisplayName("Výška"), _
    TypeConverter(GetType(CCNFloatConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property H() As Single
        Get
            Return _h
        End Get
        Set(ByVal value As Single)
            _h = value
        End Set
    End Property

    Protected _name As String = ""
    <Category("Hlavné"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Názov objektu jedineèný v rámci dokumentu."), _
    DisplayName("Názov"), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

#End Region

#Region "Properties - Runtine"

    Protected _hovered As Boolean
    <Browsable(False)> _
    Public Property Hovered() As Boolean
        Get
            Return _hovered
        End Get
        Set(ByVal value As Boolean)
            _hovered = value
        End Set
    End Property

    Protected _selected As Boolean
    <Browsable(False)> _
    Public Property Selected() As Boolean
        Get
            Return _selected
        End Get
        Set(ByVal value As Boolean)
            _selected = value
        End Set
    End Property

#End Region

#Region "Initialization"

    Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
        _x = x
        _y = y
        _w = w
        _h = h
    End Sub

#End Region

#Region "Rendering"

    Protected Shared _brushWhiteSemiTransparent As New SolidBrush(Color.FromArgb(160, 255, 255, 255))

    Public Overridable Sub Draw(ByRef g As Graphics)

        Dim p As Pen

        If (Not _hovered) Then
            If (Not _selected) Then
                p = New Pen(Color.FromArgb(64, 0, 0, 0), 0.1)
            Else
                p = New Pen(Color.Red, 0.1)
            End If
        Else
            If (Not _selected) Then
                p = New Pen(Color.Blue, 0.1)
            Else
                p = New Pen(Color.Red, 0.1)
            End If
        End If

        g.FillRectangle(_brushWhiteSemiTransparent, _x, _y, _w, _h)
        g.DrawRectangle(p, _x, _y, _w, _h)
    End Sub

    Public Overridable Function CointainsPoint(ByVal x As Integer, ByVal y As Integer) As Boolean

        If ((x < _x) OrElse (x > (_x + _w))) Then Return False
        If ((y < _y) OrElse (y > (_y + _h))) Then Return False

        Return True
    End Function

#End Region

End Class
