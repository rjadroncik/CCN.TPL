Imports NHibernate.Cfg
Imports NHibernate.Context
Imports NHibernate
Imports CCN.Model
Imports FluentNHibernate.Cfg
Imports FluentNHibernate.Cfg.Db
Imports CCN.Core.VB
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class DatabaseMSWeb
    Inherits DatabaseMS

    Public Overrides Sub Configure(connectionString As String, assemblies As IAssemblyProvider, persistConfig As Boolean)
        MyBase.Configure(connectionString, assemblies, persistConfig)

        CreateFluentConfiguration(connectionString, assemblies)
        _fluentConfiguration.CurrentSessionContext("web")

        CreateConfiguration(persistConfig)

        _sessionFactory = _configuration.BuildSessionFactory()
    End Sub

    Public Overrides Function NewSession() As ISession

        If (Not CurrentSessionContext.HasBind(_sessionFactory)) Then

            CurrentSessionContext.Bind(_sessionFactory.OpenSession())
        End If

        Return SessionFactory.GetCurrentSession()
    End Function

    Public Overrides Function NewSession(connection As IDbConnection) As ISession

        Throw New NotSupportedException("Web applications have implicit session management.")
    End Function

End Class
