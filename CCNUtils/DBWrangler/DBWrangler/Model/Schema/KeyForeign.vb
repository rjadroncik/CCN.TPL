
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema

    ''' <summary>
    ''' Reprezentuje cudzi kluc. Ide o zoznam stlpcov ktore sa zucastnuju na relacii, pricom nezalezi na poradi.
    ''' </summary>
    ''' <remarks>
    ''' Dolezite je ze poradie
    ''' </remarks>
    Public Class KeyForeign
        Inherits TableElement

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

        Public Property ReferencedKey As KeyUnique

        Private ReadOnly _columns As New Dictionary(Of Column, Column)
        Public ReadOnly Property Columns() As IDictionary(Of Column, Column)
            Get
                Return _columns
            End Get
        End Property

    End Class
End Namespace