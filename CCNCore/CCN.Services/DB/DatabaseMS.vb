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

Public Class DatabaseMS
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
    End Sub

#End Region

#Region "Configuration"

    Protected Sub CreateFluentConfiguration(connectionString As String, assemblies As IAssemblyProvider)

        _assemblies = assemblies

        _fluentConfiguration = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008 _
                                                   .Dialect(Of CustomMsSql2008Dialect)() _
                                                   .ConnectionString(connectionString) _
                                                   .AdoNetBatchSize(48)) '.Diagnostics(Sub(diag) diag.Enable().OutputToConsole())

        For Each assembly As Reflection.Assembly In _assemblies.Assemblies()

            Dim tmp As Reflection.Assembly = assembly
            _fluentConfiguration.Mappings(Function(m) m.FluentMappings.AddFromAssembly(tmp)) '.ExportTo("Mappings"))
        Next

        _fluentConfiguration.Mappings(Sub(m)
                                          m.FluentMappings.Conventions.Add(New NamingConventions())
                                      End Sub) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("use_proxy_validator", "false")) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("show_sql", Converting.ToXml(Globals.ShowSql))) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("prepare_sql", Converting.ToXml(True))) _
              .ExposeConfiguration(Sub(cfg As Configuration) cfg.Properties.Add("generate_statistics", Converting.ToXml(Globals.ShowSql)))
    End Sub

    Protected Sub CreateConfiguration(persistConfig As Boolean)

        If (Not persistConfig) Then

            _configuration = _fluentConfiguration.BuildConfiguration()
        Else
            If (ConfigurationIsValid()) Then

                ConfigurationLoad()
            Else
                _configuration = _fluentConfiguration.BuildConfiguration()

                ConfigurationSave()
            End If
        End If
    End Sub

#End Region

#Region "IDatabaseProvider"

    Public Overridable Sub Configure(connectionString As String, assemblies As IAssemblyProvider, persistConfig As Boolean) Implements IDatabaseProvider.Configure

        CreateFluentConfiguration(connectionString, assemblies)
        CreateConfiguration(persistConfig)

        _sessionFactory = _configuration.BuildSessionFactory()
    End Sub

    Public Overridable Function GetDate(session As ISession) As Date Implements IDatabaseProvider.GetDate

        Return CDate(session.CreateSQLQuery("SELECT GetDate()").UniqueResult())
    End Function

    Public Overridable Function GetVersion(session As ISession) As Version Implements IDatabaseProvider.GetVersion

        Dim result = session.CreateSQLQuery("SELECT value FROM sys.extended_properties WHERE class = 0 AND name = N'db_version'").AddScalar("value", NHibernateUtil.String).UniqueResult()
        If (result Is Nothing) Then Return Nothing

        Return Converting.String2Version(DirectCast(result, String))
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

        SqlConnection.ClearAllPools()

        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

#End Region

#Region "Configuration serialization"

    Protected _configFile As String = Globals.StartupPath & "\NHConfig.scf"

    Private Sub ConfigurationSave()

        Using stream = File.Open(_configFile, FileMode.Create)

            Dim formatter = New BinaryFormatter()
            formatter.Serialize(stream, _configuration)
        End Using
    End Sub

    Private Sub ConfigurationLoad()

        Using stream = File.Open(_configFile, FileMode.Open)

            Dim formatter = New BinaryFormatter()
            _configuration = DirectCast(formatter.Deserialize(stream), Configuration)
        End Using
    End Sub

    Private Function ConfigurationIsValid() As Boolean

        Dim assemblyLastWrite As DateTime

        For Each assembly In _assemblies.Assemblies()

            If (assembly.Location Is Nothing) Then Return False

            Dim writeTime = New FileInfo(assembly.Location).LastWriteTime

            assemblyLastWrite = If(writeTime > assemblyLastWrite, writeTime, assemblyLastWrite)
        Next

        Dim configInfo = New FileInfo(_configFile)

        Return (configInfo.LastWriteTime > assemblyLastWrite)
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
