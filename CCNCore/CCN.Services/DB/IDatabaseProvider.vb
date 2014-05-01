Imports CCN.Model
Imports FluentNHibernate.Cfg
Imports NHibernate
Imports NHibernate.Context
Imports NHibernate.Cfg

Public Interface IDatabaseProvider
    Inherits IDisposable

#Region "Properties"

    ReadOnly Property SessionFactory As ISessionFactory

    ReadOnly Property FluentConfiguration As FluentConfiguration
    ReadOnly Property Configuration As Configuration

    ReadOnly Property Assemblies As IAssemblyProvider

#End Region

#Region "BL"

    ''' <summary>
    ''' Creates a new configuration object based on a 
    ''' </summary>
    ''' <param name="connectionString"></param>
    ''' <param name="assemblies"></param>
    ''' <remarks></remarks>
    Sub Configure(connectionString As String, assemblies As IAssemblyProvider, persistConfig As Boolean)

    Function GetDate(session As ISession) As Date
    Function GetVersion(session As ISession) As Version

    Function NewSession() As ISession
    Function NewSession(connection As IDbConnection) As ISession

    Sub Close()

#End Region

End Interface
