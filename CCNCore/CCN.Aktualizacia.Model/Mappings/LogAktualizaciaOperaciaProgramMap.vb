Imports CCN.Model
Imports FluentNHibernate.Mapping

Public Class LogAktualizaciaOperaciaProgramMap
    Inherits SubclassMap(Of LogAktualizaciaOperaciaProgram)

    Public Sub New()

        Table("logAktualizaciaOperaciaProgram")
        Extends(Of Log)()
        KeyColumn("id")

        Map(Function(x) x.Finished).Nullable().Column("finished")
        References(Function(x) x.Aktualizacia).Not.Nullable().Column("aktualizaciaId").LazyLoad()

        Map(Function(x) x.Action).Not.Nullable().Column("akcia").CustomType(Of FileAction).CustomSqlType(DBTypes.Int())
        Map(Function(x) x.Path).Nullable().Column("cesta").Length(2048)
        Map(Function(x) x.Process).Nullable().Column("proces").Length(256)
        Map(Function(x) x.Arguments).Nullable().Column("argumenty").Length(4096)
    End Sub

End Class
