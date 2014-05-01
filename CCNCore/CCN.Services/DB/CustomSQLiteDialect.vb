Imports NHibernate
Imports NHibernate.Dialect
Imports NHibernate.Dialect.Function

Public Class CustomSQLiteDialect
    Inherits SQLiteDialect

    Public Sub New()

        RegisterFunction("LIKE_CI_AI", New SQLFunctionTemplate(NHibernateUtil.Boolean, "?1 LIKE ?2"))
    End Sub

End Class
