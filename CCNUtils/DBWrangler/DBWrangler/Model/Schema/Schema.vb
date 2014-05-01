Namespace Model.Schema
    Public Class Schema

        Private ReadOnly _tables As New List(Of Table)
        Public ReadOnly Property Tables As IList(Of Table)
            Get
                Return _tables
            End Get
        End Property

        Public Property Version As New Version(1, 0, 0, 0)

        Public Function TableNamed(name As String) As Table

            For Each table As Table In _tables

                If (table.Name = name) Then Return table
            Next

            Return Nothing
        End Function
    End Class
End Namespace