Public Class FlexGridSirky

    Protected Sub New()
    End Sub

    Public Shared Function Create() As FlexGridSirky

        Dim sirky As New FlexGridSirky()

        sirky.Stlpce = New Dictionary(Of String, FlexGridStlpec)

        Return sirky
    End Function

    <Permanent(), MaxHod()>
    Public Overridable Property Id As Integer

    <Permanent()>
    Public Overridable Property Form As String
    <Permanent()>
    Public Overridable Property Control As String

    <Permanent()>
    Public Overridable Property Login As String

    Public Overridable Property Stlpce As IDictionary(Of String, FlexGridStlpec)
End Class
