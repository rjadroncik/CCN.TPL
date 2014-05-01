Imports NHibernate
Imports NHibernate.Dialect
Imports NHibernate.Dialect.Function

Public Class CustomMsSql2008Dialect
    Inherits MsSql2008Dialect

    Public Sub New()

        RegisterFunction("LIKE_CI_AI", New SQLFunctionTemplate(NHibernateUtil.Boolean, "?1 LIKE ?2 COLLATE Slovak_CI_AI"))
    End Sub

End Class
