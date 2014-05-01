Imports NHibernate.Transform
Imports CCN.Core.VB

Public Class ToClrTableResultTransformer
    Implements IResultTransformer

#Region "Properties"

    Protected _table As ClrTable
    Public ReadOnly Property Table As ClrTable
        Get
            Return _table
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Sub New()

        _table = New ClrTable()
    End Sub

    Public Sub New(table As ClrTable)

        If (table.Initialized) Then Throw New InvalidOperationException("Cannot use transformer on an already initialized table!")

        _table = table
    End Sub

#End Region

#Region "IResultTransformer"

    Public Function TransformList(collection As IList) As IList Implements IResultTransformer.TransformList

        Return collection
    End Function

    Public Function TransformTuple(tuple() As Object, aliases() As String) As Object Implements IResultTransformer.TransformTuple

        If (Not Table.Initialized) Then

            For i As Integer = 0 To tuple.Length - 1

                Table.ColumnAdd(aliases(i), If(tuple(i) Is Nothing, GetType(Object), tuple(i).GetType()))
            Next
        End If

        Dim row = Table.RowAdd()

        For i As Integer = 0 To tuple.Length - 1

            row(aliases(i)) = tuple(i)
        Next

        Return row
    End Function

#End Region

End Class
