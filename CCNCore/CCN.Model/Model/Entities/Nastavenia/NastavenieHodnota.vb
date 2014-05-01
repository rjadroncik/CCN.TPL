Imports CCN.Core.VB

Public Class NastavenieHodnota
    Inherits NHEntita

#Region "Initialization"

    Public Shared Function Create() As NastavenieHodnota

        Dim hodnota As New NastavenieHodnota()
        Return hodnota
    End Function

    Private Sub New()
    End Sub

#End Region

#Region "Properties"

    'Entita na ktoru sa nastavenie vztahuje alebo [global] - globálny parameter
    Public Overridable Property Entita As String

    Public Overridable Property Hodnota As String

    'Urcuje ci je lokalne nastavenie aplikovane, globalne su nastavenia zatial vzdy aktivne  [DLL-47]  
    Public Overridable Property Aktivna As Boolean

    Public Overridable Property Nastavenie As Nastavenie
    Public Overridable Property Skupina As NastavenieSkupina

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String

        Return Nastavenie.ToString() & " = " & Hodnota
    End Function

#End Region

End Class
