Public Interface IObjektyZoznam(Of T As Class)

    Property Vsetky As IEnumerable(Of T)

    Property Stlpce As IEnumerable(Of Stlpec(Of T))

End Interface
