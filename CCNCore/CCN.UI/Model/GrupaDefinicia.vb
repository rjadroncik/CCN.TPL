Imports CCN.Core.VB

Public Class GrupaDefinicia(Of T As Class)

    Public Property Zoradenie As Zoradenie = Zoradenie.Vzostupne

    Public Property KlucGetter As PropertyGetter(Of T)
    Public Property KlucFormatter As CustomToString(Of Object) = Function(x) x.ToString()
    Public Property KlucComparer As Compare(Of Object) = Function(x, y) DirectCast(x, IComparable).CompareTo(y)

    Public Property Uroven As Integer

End Class
