Imports CCN.Core.VB

Public Class TypUdalosti
    Inherits DynamicEnum

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)
        MyBase.New(id, nazov)
    End Sub

#End Region

#Region "Shared fields"

    Public Shared ReadOnly AplikaciaStart As New TypUdalosti(1, "Aplikácia - štart")
    Public Shared ReadOnly AplikaciaBezi As New TypUdalosti(2, "Aplikácia - beží")
    Public Shared ReadOnly AplikaciaUkoncenie As New TypUdalosti(3, "Aplikácia - ukončenie")

    Public Shared ReadOnly PouzivatelPrihlasenieNeuspesne As New TypUdalosti(4, "Používateľ - prihlásenie (neúspešné)")
    Public Shared ReadOnly PouzivatelPrihlasenie As New TypUdalosti(5, "Používateľ - prihlásenie")
    Public Shared ReadOnly PouzivatelOdhlasenie As New TypUdalosti(6, "Používateľ - odhlásenie")

    Public Shared ReadOnly PouzivatelZmenaHesla As New TypUdalosti(7, "Používateľ - zmena hela (úspešná)")
    Public Shared ReadOnly PouzivatelZmenaHeslaNeuspesna As New TypUdalosti(8, "Používateľ - zmena hela (neúspešná)")

#End Region

End Class
