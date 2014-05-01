Imports NHibernate
Imports NHibernate.UserTypes
Imports NHibernate.SqlTypes
Imports CCN.Core.VB

<Serializable()>
Public Class NHVersion
    Implements IUserType

    Public Function Assemble(cached As Object, owner As Object) As Object Implements IUserType.Assemble

        Return cached
    End Function

    Public Function DeepCopy(value As Object) As Object Implements IUserType.DeepCopy

        If (value Is Nothing) Then Return Nothing

        With DirectCast(value, Version)

            Return New Version(.Major, .Minor, .Build, .Revision)
        End With
    End Function

    Public Function Disassemble(value As Object) As Object Implements IUserType.Disassemble

        Return value
    End Function

    Public Shadows Function Equals(x As Object, y As Object) As Boolean Implements IUserType.Equals

        If (x Is Nothing) Then

            If (y Is Nothing) Then Return True
            Return False
        End If

        Return x.Equals(y)
    End Function

    Public Shadows Function GetHashCode(x As Object) As Integer Implements IUserType.GetHashCode

        Return x.GetHashCode()
    End Function

    Public ReadOnly Property IsMutable As Boolean Implements IUserType.IsMutable
        Get
            Return False
        End Get
    End Property

    Public Function NullSafeGet(rs As IDataReader, names() As String, owner As Object) As Object Implements IUserType.NullSafeGet

        Dim versionString As String = DirectCast(NHibernateUtil.String.NullSafeGet(rs, names(0)), String)
        If (versionString Is Nothing) Then Return Nothing

        Return Converting.String2Version(versionString)
    End Function

    Public Sub NullSafeSet(cmd As IDbCommand, value As Object, index As Integer) Implements IUserType.NullSafeSet

        If (value Is Nothing) Then

            NHibernateUtil.String.NullSafeSet(cmd, Nothing, index)
        Else
            NHibernateUtil.String.NullSafeSet(cmd, value.ToString(), index)
        End If
    End Sub

    Public Function Replace(original As Object, target As Object, owner As Object) As Object Implements IUserType.Replace

        Return original
    End Function

    Public ReadOnly Property ReturnedType As System.Type Implements IUserType.ReturnedType
        Get
            Return GetType(Version)
        End Get
    End Property

    Public ReadOnly Property SqlTypes As SqlType() Implements IUserType.SqlTypes
        Get
            Return New SqlType() {New SqlType(DbType.String)}
        End Get
    End Property

End Class
