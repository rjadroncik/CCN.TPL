Public Class FlexGridStlpec
    Inherits NHEntitaCustom

    Protected Sub New()
    End Sub

    Public Shared Function Create() As FlexGridStlpec

        Return New FlexGridStlpec()
    End Function

    <Permanent()>
    Public Overridable Property Sirky As FlexGridSirky

    <Permanent()>
    Public Overridable Property Name As String

    Public Overridable Property Width As Integer

#Region "Equals"

    Public Overrides Function Equals(obj As Object) As Boolean

        If (obj Is Nothing) Then Return False

        If (NHEntita.EntityType(obj) IsNot NHEntita.EntityType(Me)) Then Return False

        With NHEntita.Unproxy(Of FlexGridStlpec)(obj)

            Return (Sirky.Id = .Sirky.Id) AndAlso (Name = .Name)
        End With
    End Function

    Public Overrides Function GetHashCode() As Integer

        Return Sirky.Id + Name.GetHashCode() \ 2
    End Function

#End Region

End Class
