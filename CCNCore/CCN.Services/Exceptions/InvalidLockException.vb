Imports CCN.Model

Public Class InvalidLockException
    Inherits Exception

    Public Property Pouzivatel As IPouzivatel

    Public Sub New(pouzivatel As IPouzivatel)

        Me.Pouzivatel = pouzivatel
    End Sub
End Class
