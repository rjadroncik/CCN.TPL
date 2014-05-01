Public Class NastavenieSkupina

#Region "Initialization"

    Public Shared Function Create() As NastavenieSkupina

        Dim skupina As New NastavenieSkupina()

        skupina.Deti = New List(Of NastavenieSkupina)
        skupina.Hodnoty = New List(Of NastavenieHodnota)

        Return skupina
    End Function

    Private Sub New()
    End Sub

#End Region

#Region "Properties"

    Public Overridable Property Id As Integer
    Public Overridable Property Nazov As String

    Public Overridable Property Uroven As Integer
    Public Overridable Property Poradie As Integer

    Public Overridable Property Rodic As NastavenieSkupina
    Public Overridable Property Deti As IList(Of NastavenieSkupina)

    Public Overridable Property Hodnoty As IList(Of NastavenieHodnota)

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String
        Return Nazov
    End Function

#End Region

End Class
