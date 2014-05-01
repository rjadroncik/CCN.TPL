Imports System.Runtime.Serialization
Imports System.Runtime.CompilerServices

#If SILVERLIGHT Then
#Else
Imports NHibernate.Proxy
#End If

<DataContract(IsReference:=True)>
Public MustInherit Class NHEntita
    Implements IIdentifiable(Of Integer)

#Region "Static - public"

#If SILVERLIGHT Then
#Else
    Public Shared Function Unproxy(Of T)(entity As Object) As T

        Dim proxy = TryCast(entity, INHibernateProxy)
        If (proxy IsNot Nothing) Then

            Return DirectCast(proxy.HibernateLazyInitializer.GetImplementation(), T)
        Else
            Return DirectCast(entity, T)
        End If
    End Function

    Public Shared Function EntityType(entity As Object) As Type

        Dim proxy = TryCast(entity, INHibernateProxy)
        If (proxy IsNot Nothing) Then

            Return proxy.HibernateLazyInitializer.PersistentClass
        Else
            Return entity.GetType()
        End If
    End Function
#End If

#End Region

#Region "Properties"

    <Permanent(), DataMember()>
    Public Overridable Property Id As Integer Implements IIdentifiable(Of Integer).Id

#End Region

#Region "Overridden"

    Public Overrides Function Equals(obj As Object) As Boolean

        '[DLL-40] Nesmie sa porovnavat typ objektu pretoze NHibernate vracia aj Proxy objekty
        If (obj Is Nothing) Then Return False

#If SILVERLIGHT Then
        If (obj.GetType() IsNot Me.GetType()) Then Return False
#Else
        If (EntityType(obj) IsNot EntityType(Me)) Then Return False
#End If
        Dim other = DirectCast(obj, NHEntita)
        If ((Id = 0) AndAlso (other.Id = 0)) Then

            Return Me Is obj
        End If

        Return Id = other.Id
    End Function

    Public Overrides Function GetHashCode() As Integer
#If SILVERLIGHT Then
        If (Id = 0) Then Return RuntimeHelpers.GetHashCode(Me)
#Else
        If (Id = 0) Then Throw New InvalidOperationException("GetHashCode na transient entite typu: " & Me.GetType().FullName)
#End If
        Return Id
    End Function

#End Region

#Region "Properties - transient"

    Public Overridable ReadOnly Property IsNew As Boolean Implements IIdentifiable(Of Integer).IsNew
        Get
            Return Id = 0
        End Get
    End Property

#End Region

End Class
