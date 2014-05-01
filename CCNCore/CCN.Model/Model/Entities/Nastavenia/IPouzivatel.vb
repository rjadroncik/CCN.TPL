Public Interface IPouzivatel
    Inherits IIdentifiable(Of Integer)

#Region "Properties"

    Property Meno As String
    Property Priezvisko As String

    Property Login As String
    Property Heslo As String

    Property Prava As IList(Of IPravo)

    Property Aktivny As Boolean

#End Region

End Interface
