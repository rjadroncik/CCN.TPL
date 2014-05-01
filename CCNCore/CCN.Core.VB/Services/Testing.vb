Public Class Testing

    Public Shared Function IsNullable(obj As Object) As Boolean

        Return (Nullable.GetUnderlyingType(obj.GetType()) IsNot Nothing)
    End Function

    Public Shared Function HasValue(obj As Object) As Boolean

        Return (obj IsNot Nothing) AndAlso (Not Testing.IsNullable(obj) OrElse DirectCast(obj.GetType().GetProperty("HasValue").GetValue(obj, Nothing), Boolean))
    End Function

End Class
