Public Interface IObjektyVyber(Of T As Class)
    Inherits IObjektyZoznam(Of T)

    Event ObjektPridany As Action(Of T)
    Event ObjektOdobrany As Action(Of T)

    Property Zvolene As IEnumerable(Of T)

End Interface
