Imports CCN.Core.VB

Public Class Stlpec(Of T As Class)

    Public Property Nazov As String
    Public Property Titulok As String

    Public Property Zoradenie As Zoradenie = Zoradenie.Vzostupne

    Public Property Getter As PropertyGetter(Of T)

    Public Property Format As String
    Public Property Zarovnanie As Zarovnanie = Zarovnanie.LavyOkraj

    Public Property DataType As Type = GetType(Object)
    Public Property ImageMap As IDictionary
End Class
