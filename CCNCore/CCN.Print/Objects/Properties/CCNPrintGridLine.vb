Imports System.ComponentModel

Public Class CCNPrintGridLine

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(ByVal source As CCNPrintGridLine)

        _changed = source._changed
        _thickness = source._thickness
        _dashStyle = source._dashStyle
        _color = source._color
    End Sub

#End Region

#Region "Properties"

    Private _changed As Boolean = False
    <Browsable(False)> _
    Public ReadOnly Property Changed() As Boolean
        Get
            Return _changed
        End Get
    End Property

    Private _thickness As Double = 0.0
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Hrúbka použitej čiary."), _
    DisplayName("Hrúbka"), _
    TypeConverter(GetType(CCNFloatConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property Thickness() As Double
        Get
            Return _thickness
        End Get
        Set(ByVal value As Double)
            _thickness = value

            _changed = True
        End Set
    End Property

    Private _dashStyle As Drawing.Drawing2D.DashStyle = Drawing.Drawing2D.DashStyle.Solid
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Druh vzoru použitej čiary."), _
    DisplayName("Druh"), _
    TypeConverter(GetType(CCNDashStyleConverter)), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property DashStyle() As Drawing.Drawing2D.DashStyle
        Get
            Return _dashStyle
        End Get
        Set(ByVal value As Drawing.Drawing2D.DashStyle)
            _dashStyle = value

            _changed = True
        End Set
    End Property

    Protected _color As Drawing.Color = Drawing.Color.Black
    <Category("Vzhľad"), _
    Browsable(True), _
    [ReadOnly](False), _
    Bindable(False), _
    DesignOnly(False), _
    Description("Farba použitej čiary."), _
    DisplayName("Farba"), _
    EditorBrowsable(EditorBrowsableState.Always)> _
    Public Overridable Property Color() As Drawing.Color
        Get
            Return _color
        End Get
        Set(ByVal value As Drawing.Color)
            _color = value

            _changed = True
        End Set
    End Property

#End Region

End Class
