Imports CCN.Model
Imports CCN.Core.VB

Public Class PravoObjektoveVyber
    Inherits ObjektVyberGrid(Of IPravoObjektove)

    Public Sub New()

        Stlpce = New Stlpec(Of IPravoObjektove)() { _
            New Stlpec(Of IPravoObjektove)() With {.Nazov = "Nazov", .Titulok = "Názov", .Zoradenie = Zoradenie.Vzostupne, .Getter = (Function(x) x.Nazov)},
            New Stlpec(Of IPravoObjektove)() With {.Nazov = "ObjektTyp", .Titulok = "Typ objektu", .Zoradenie = Zoradenie.Vzostupne, .Getter = (Function(x) x.ObjektTyp)},
            New Stlpec(Of IPravoObjektove)() With {.Nazov = "Prava", .Titulok = "Práva", .Zoradenie = Zoradenie.Ziadne, .Getter = (Function(x) x.Povolene.ToStringAll())}
        }

    End Sub

End Class
