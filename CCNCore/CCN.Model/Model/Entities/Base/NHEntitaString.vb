Imports System.Runtime.Serialization
Imports System.Runtime.CompilerServices

#If SILVERLIGHT Then
#Else
Imports NHibernate.Proxy
#End If

<DataContract(IsReference:=True)>
Public MustInherit Class NHEntitaString
    Implements IIdentifiable(Of String)

#Region "Properties"

    <Permanent(), DataMember()>
    Public Overridable Property Id As String Implements IIdentifiable(Of String).Id

#End Region

#Region "Overridden"

    Public Overrides Function Equals(obj As Object) As Boolean

        '[DLL-40] Nesmie sa porovnavat typ objektu pretoze NHibernate vracia aj Proxy objekty
        If (obj Is Nothing) Then Return False

#If SILVERLIGHT Then
        If (obj.GetType() IsNot Me.GetType()) Then Return False
#Else
        If (NHEntita.EntityType(obj) IsNot NHEntita.EntityType(Me)) Then Return False
#End If
        Dim other = DirectCast(obj, NHEntitaString)
        If ((Id Is Nothing) AndAlso (other.Id Is Nothing)) Then

            Return Me Is obj
        End If

        Return Id = other.Id
    End Function

    Public Overrides Function GetHashCode() As Integer
#If SILVERLIGHT Then
        If (Id is nothing) Then Return RuntimeHelpers.GetHashCode(Me)
#Else
        If (Id Is Nothing) Then Throw New InvalidOperationException("GetHashCode na transient entite typu: " & Me.GetType().FullName)
#End If
        Return Id.GetHashCode()
    End Function

#End Region

#Region "Properties - transient"

    Public Overridable ReadOnly Property IsNew As Boolean Implements IIdentifiable(Of String).IsNew
        Get
            Return Id Is Nothing
        End Get
    End Property

#End Region

End Class
