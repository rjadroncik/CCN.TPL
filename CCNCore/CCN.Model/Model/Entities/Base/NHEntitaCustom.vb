Imports System.Runtime.Serialization

<DataContract(IsReference:=True)>
Public MustInherit Class NHEntitaCustom

#Region "Overridden"

    Public Overrides Function Equals(obj As Object) As Boolean

        Throw New InvalidOperationException("Unimplemented Equals in type: " & Me.GetType().FullName)
    End Function

    Public Overrides Function GetHashCode() As Integer

        Throw New InvalidOperationException("Unimplemented GetHashCode in type: " & Me.GetType().FullName)
    End Function

#End Region

#Region "Utils"

    Protected Shared Function EqualsDefault(Of T As IConvertible)(first As IIdentifiable(Of T), second As IIdentifiable(Of T), notSetId As T) As Boolean

        If (first Is Nothing) Then

            If (second Is Nothing) Then Return True

            Return False
        Else
            If (second Is Nothing) Then Return False
        End If

#If SILVERLIGHT Then
        If (first.GetType() IsNot second.GetType()) Then Return False
#Else
        If (NHEntita.EntityType(first) IsNot NHEntita.EntityType(second)) Then Return False
#End If

        If ((first.Id.Equals(notSetId)) AndAlso (second.Id.Equals(notSetId))) Then

            Return first Is second
        End If

        Return first.Id.Equals(second.Id)
    End Function

    Protected Shared Function EqualsDefault(Of T)(first As Object, second As Object, comparer As IEqualityComparer(Of T)) As Boolean

        If (first Is Nothing) Then

            If (second Is Nothing) Then Return True

            Return False
        Else
            If (second Is Nothing) Then Return False
        End If

#If SILVERLIGHT Then
        If (first.GetType() IsNot second.GetType()) Then Return False
#Else
        If (NHEntita.EntityType(first) IsNot NHEntita.EntityType(second)) Then Return False
#End If

        Return comparer.Equals(DirectCast(first, T), DirectCast(second, T))
    End Function

#End Region

End Class
