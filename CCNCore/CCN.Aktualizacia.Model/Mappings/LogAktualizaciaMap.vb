Imports CCN.Model
Imports FluentNHibernate.Mapping

Public Class LogAktualizaciaMap
    Inherits SubclassMap(Of LogAktualizacia)

    Public Sub New()

        Table("logAktualizacia")
        Extends(Of Log)()
        KeyColumn("id")

        Map(Function(x) x.VerziaStara).Nullable().Column("verziaStara").CustomType(Of NHVersion).Length(24)
        Map(Function(x) x.VerziaNova).Not.Nullable().Column("verziaNova").CustomType(Of NHVersion).Length(24)

        Map(Function(x) x.Komponent).Not.Nullable().Column("komponent").Length(32)

        Map(Function(x) x.Finished).Nullable().Column("finished")

        'HasMany(Function(x) x.Operacie).Inverse().Cascade.SaveUpdate().KeyColumn("aktualizaciaId").LazyLoad().Fetch.Subselect()
    End Sub

End Class