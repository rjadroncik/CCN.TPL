Public Class InvalidVersionException
    Inherits Exception

    Public Property VerzieOcakavane As String
    Public Property VerziaNajdena As String

    Public Sub New(verzieOcakavane As String, verziaNajdena As String)

        Me.VerzieOcakavane = verzieOcakavane
        Me.VerziaNajdena = verziaNajdena
    End Sub
End Class
