Imports CCN.Core.VB

Public Class Nastavenie
    Inherits NHEntitaCustom

#Region "Initialization"

    Public Shared Function Create() As Nastavenie

        Dim nastavenie As New Nastavenie()
        Return nastavenie
    End Function

    Private Sub New()
    End Sub

#End Region

#Region "Properties"

    <Permanent()>
    Public Overridable Property Id As IdNastavenia

    Public Overridable Property Nazov As String

    Public Overridable Property DatovyTyp As DatovyTyp

    'Urcuje ci je dane globalne nastavene upravitelne pre jednolivych pouzivatelov, 
    'lokalne nastavenie ktore upravuje niektore globalne ma tiez nastavene na True [DLL-47]
    Public Overridable Property Upravitelne As Boolean

#End Region

#Region "Equals"

    Public Overrides Function Equals(obj As Object) As Boolean

        If (obj Is Nothing) Then Return False

        If (NHEntita.EntityType(obj) IsNot NHEntita.EntityType(Me)) Then Return False

        With NHEntita.Unproxy(Of Nastavenie)(obj)

            Return (Id.Id = .Id.Id)
        End With
    End Function

    Public Overrides Function GetHashCode() As Integer

        Return Id.Id
    End Function

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String
        Return Nazov
    End Function

#End Region

End Class
