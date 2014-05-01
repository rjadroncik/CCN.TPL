Imports System.Reflection
Imports CCN.Core.VB
Imports CCN.Model
Imports CCN.Services
Imports FluentNHibernate
Imports FluentNHibernate.Cfg
Imports FluentNHibernate.Cfg.Db
Imports NHibernate
Imports NHibernate.Cfg
Imports NHibernate.Dialect
Imports NHibernate.Tool.hbm2ddl

Public MustInherit Class NHTestFixtureBase

    Protected Sub NHSetUp(ParamArray assemblies As Assembly())

        Dim provider = New DatabaseSQLiteTest()
        provider.Configure(DatabaseSQLite.BuildConnectionStringInMemory(), LambdaAssemblyProvider.Create(Function() assemblies), False)

        Service.Initialize(provider)
    End Sub

    Protected Sub NHTearDown()

        Service.Close()
    End Sub

End Class