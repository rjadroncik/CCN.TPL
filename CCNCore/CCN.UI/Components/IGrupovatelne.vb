Public Interface IGrupovatelne(Of T As Class)

    ReadOnly Property GrupyDefinicie As IList(Of GrupaDefinicia(Of T))

    ReadOnly Property Grupy As IEnumerable(Of Grupa(Of T))

End Interface
