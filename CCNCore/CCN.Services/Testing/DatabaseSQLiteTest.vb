Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports CCN.Services
Imports CCN.Model
Imports FluentNHibernate
Imports FluentNHibernate.Cfg
Imports FluentNHibernate.Cfg.Db
Imports FluentNHibernate.Conventions.Helpers
Imports NHibernate.Cfg
Imports NHibernate.Tool.hbm2ddl
Imports NHibernate
Imports NHibernate.Dialect
Imports NHibernate.Context
Imports CCN.Core.VB

Public Class DatabaseSQLiteTest
    Inherits DatabaseSQLite

#Region "Overridden"

    Private _connection As IDbConnection

    Public Overrides Sub Configure(connectionString As String, assemblies As IAssemblyProvider, persistConfig As Boolean)
        MyBase.Configure(connectionString, assemblies, persistConfig)

        CreateFluentConfiguration(connectionString, assemblies)

        _configuration = _fluentConfiguration.BuildConfiguration()
        _configuration.SetProperty(Environment.ReleaseConnections, "on_close")

        _sessionFactory = _configuration.BuildSessionFactory()
    End Sub

    Public Overrides Function NewSession() As ISession

        If (_connection Is Nothing) Then

            Dim session = MyBase.NewSession()

            _connection = session.Connection
            Database.NHibernateBuildSchema(_configuration, _connection)

            Return session
        Else
            Return MyBase.NewSession(_connection)
        End If
    End Function

#End Region

End Class