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

Public Class DatabaseSQLite
    Implements IDatabaseProvider

#Region "Properties"

    Protected _sessionFactory As ISessionFactory
    Public ReadOnly Property SessionFactory As ISessionFactory Implements IDatabaseProvider.SessionFactory
        Get
            Return _sessionFactory
        End Get
    End Property

    Protected _fluentConfiguration As FluentConfiguration
    Public ReadOnly Property FluentConfiguration As FluentConfiguration Implements IDatabaseProvider.FluentConfiguration
        Get
            Return _fluentConfiguration
        End Get
    End Property

    Protected _configuration As Configuration
    Public ReadOnly Property Configuration As Configuration Implements IDatabaseProvider.Configuration
        Get
            Return _configuration
        End Get
    End Property

    Protected _assemblies As IAssemblyProvider
    Public ReadOnly Property Assemblies As IAssemblyProvider Implements IDatabaseProvider.Assemblies
        Get
            Return _assemblies
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New()

        DBTypes.StringMax = AddressOf SqliteStringMax
    End Sub

#End Region

#Region "SQL types"

    Protected Shared Function SqliteStringMax() As String
        Return "NVARCHAR(65536)"
    End Function

#End Region

#Region "Configuration"

    Protected Sub CreateFluentConfiguration(connectionString As String, assemblies As IAssemblyProvider)

        _assemblies = assemblies

        _fluentConfiguration = Fluently.Configure().Database(SQLiteConfiguration.Standard.Dialect(Of CustomSQLiteDialect).ConnectionString(connectionString).AdoNetBatchSize(48))

        For Each assembly As Assembly In _assemblies.Assemblies()
            Dim tmp = assembly
            _fluentConfiguration.Mappings(Function(m) m.FluentMappings.AddFromAssembly(tmp))
        Next

        _fluentConfiguration.Mappings(Function(m) m.FluentMappings.Conventions.Add(New NamingConventions())) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("use_proxy_validator", "false")) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("show_sql", Converting.ToXml(Globals.ShowSql))) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("generate_statistics", Converting.ToXml(Globals.ShowSql)))
    End Sub
#End Region

#Region "IDatabaseProvider"

    Public Overridable Sub Configure(connectionString As String, assemblies As IAssemblyProvider, persistConfig As Boolean) Implements IDatabaseProvider.Configure

        CreateFluentConfiguration(connectionString, assemblies)

        _configuration = _fluentConfiguration.BuildConfiguration()
        _sessionFactory = _configuration.BuildSessionFactory()
    End Sub

    Public Overridable Function GetDate(session As ISession) As DateTime Implements IDatabaseProvider.GetDate

        Return DateTime.Parse(session.CreateSQLQuery("SELECT datetime('now')").UniqueResult(Of String)())
    End Function

    Public Overridable Function GetVersion(session As ISession) As Version Implements IDatabaseProvider.GetVersion
        Return Nothing
    End Function

    Public Overridable Function NewSession() As ISession Implements IDatabaseProvider.NewSession

        Dim session As ISession = _sessionFactory.OpenSession()
        session.FlushMode = FlushMode.Commit

        Return session
    End Function

    Public Overridable Function NewSession(connection As IDbConnection) As ISession Implements IDatabaseProvider.NewSession

        Dim session As ISession = _sessionFactory.OpenSession(connection)
        session.FlushMode = FlushMode.Commit

        Return session
    End Function

    Public Overridable Sub Close() Implements IDatabaseProvider.Close

        _sessionFactory.Close()
        _sessionFactory.Dispose()
        _sessionFactory = Nothing

        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

#End Region

#Region "Utils"

    Public Shared Function BuildConnectionString(file As String) As String

        Return [String].Format("Data Source={0};Version=3;New=True;", file)
    End Function

    Public Shared Function BuildConnectionStringInMemory() As String

        Return BuildConnectionString(":memory:")
    End Function

#End Region

#Region "IDisposable"

    Private disposedValue As Boolean ' To detect redundant calls

    Protected Overridable Sub Dispose(disposing As Boolean)
        If (Not disposedValue) Then
            If disposing Then Close()
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class