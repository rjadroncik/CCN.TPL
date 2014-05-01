Imports CCN.Model
Imports FluentNHibernate.Mapping

Public Class LogAktualizaciaOperaciaSuborMap
    Inherits SubclassMap(Of LogAktualizaciaOperaciaSubor)

    Public Sub New()

        Table("logAktualizaciaOperaciaSubor")
        Extends(Of Log)()
        KeyColumn("id")

        Map(Function(x) x.Finished).Nullable().Column("finished")
        References(Function(x) x.Aktualizacia).Not.Nullable().Column("aktualizaciaId").LazyLoad()

        Map(Function(x) x.Action).Not.Nullable().Column("akcia").CustomType(Of FileAction).CustomSqlType(DBTypes.Int())
        Map(Function(x) x.Path).Not.Nullable().Column("cesta").Length(2048)

        Map(Function(x) x.VerziaStara).Nullable().Column("verziaStara").CustomType(Of NHVersion).Length(24)
        Map(Function(x) x.VerziaNova).Nullable().Column("verziaNova").CustomType(Of NHVersion).Length(24)
    End Sub

End Class
