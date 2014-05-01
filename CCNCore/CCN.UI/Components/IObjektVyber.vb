Public Interface IObjektVyber(Of T As Class)

    Event ObjektZvoleny As Action(Of T)

    Property Vsetky As IEnumerable(Of T)
    Property Zvoleny As T

    Property Stlpce As IEnumerable(Of Stlpec(Of T))

End Interface
