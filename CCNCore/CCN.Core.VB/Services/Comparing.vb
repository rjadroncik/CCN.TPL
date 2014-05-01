Imports System.IO

Public Class Comparing

#Region "Nullables"

    Public Shared Function Equal(Of T As Structure)(first As Nullable(Of T), second As Nullable(Of T)) As Boolean

        If (first.HasValue) Then

            If (second.HasValue) Then

                Return first.Value.Equals(second.Value)
            Else
                Return False
            End If
        Else
            Return Not second.HasValue
        End If
    End Function

#End Region

#Region "Files"

    Public Shared Function EqualContent(ByVal file1 As String, ByVal file2 As String) As Boolean

        If (file1 = file2) Then Return True

        Using fs1 As New FileStream(file1, FileMode.Open), _
              fs2 As New FileStream(file2, FileMode.Open)

            If (fs1.Length <> fs2.Length) Then Return False

            For i = 1 To fs1.Length

                If (fs1.ReadByte() <> fs2.ReadByte()) Then Return False
            Next

            Return True
        End Using
    End Function

#End Region

#Region "Generic"

    Public Shared Function Equal(Of T As Class)(first As T, second As T) As Boolean

        If (first IsNot Nothing) Then

            If (second IsNot Nothing) Then

                Return first.Equals(second)
            Else
                Return False
            End If
        Else
            Return (second Is Nothing)
        End If
    End Function

    Public Shared Function EqualAll(Of T)(first As IList(Of T), second As IList(Of T)) As Boolean

        If (first.Count <> second.Count) Then Return False

        For i As Integer = 0 To first.Count - 1

            If (Not first(i).Equals(second(i))) Then Return False
        Next

        Return True
    End Function

    Public Shared Function EqualAll(Of T)(first() As T, second() As T) As Boolean

        If (first.Count <> second.Count) Then Return False

        For i As Integer = 0 To first.Count - 1

            If (Not first(i).Equals(second(i))) Then Return False
        Next

        Return True
    End Function

#End Region

#Region "IEqualityComparer"

    Private NotInheritable Class LambdaEqualityComparer(Of T)
        Implements IEqualityComparer(Of T)

        Private _equals As Func(Of T, T, Boolean)
        Private _getHashCode As Func(Of T, Integer)

        Friend Sub New(equals As Func(Of T, T, Boolean), _
                       getHashCode As Func(Of T, Integer))

            _equals = equals
            _getHashCode = getHashCode
        End Sub

        Public Shadows Function Equals(left As T, right As T) As Boolean Implements IEqualityComparer(Of T).Equals

            Return _equals(left, right)
        End Function

        Public Shadows Function GetHashCode(value As T) As Integer Implements IEqualityComparer(Of T).GetHashCode

            Return _getHashCode(value)
        End Function

    End Class

    Public Shared Function EqualityComparer(Of T)(equals As Func(Of T, T, Boolean), _
                                                  getHashCode As Func(Of T, Integer)) As IEqualityComparer(Of T)

        Return New LambdaEqualityComparer(Of T)(equals, getHashCode)
    End Function

#End Region

#Region "IComparer"

    Private NotInheritable Class DelegateComparer(Of T)
        Implements IComparer(Of T)

        Private _compare As Compare(Of T)

        Friend Sub New(compare As Compare(Of T))

            _compare = compare
        End Sub

        Function Compare(x As T, y As T) As Integer Implements IComparer(Of T).Compare

            Return _compare(x, y)
        End Function

    End Class

    Public Shared Function Comparer(Of T)(compare As Compare(Of T)) As IComparer(Of T)

        Return New DelegateComparer(Of T)(compare)
    End Function

#End Region

End Class
