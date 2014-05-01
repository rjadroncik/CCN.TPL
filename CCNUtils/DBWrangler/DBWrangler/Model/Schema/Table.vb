Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema

    Public Class Table
        Inherits Element

        Public Sub New(schema As Schema)

            _schema = schema
        End Sub

        Protected _schema As Schema
        Public ReadOnly Property Schema() As Schema
            Get
                Return _schema
            End Get
        End Property

        Public Sub New(name As String)
            Me.Name = name
        End Sub

        Protected _columns As New List(Of Column)
        Public ReadOnly Property Columns As List(Of Column)
            Get
                Return _columns
            End Get
        End Property

        Public Property PrimaryKey As KeyPrimary

        Protected _indexes As New List(Of Index)
        Public ReadOnly Property Indexes As List(Of Index)
            Get
                Return _indexes
            End Get
        End Property

        Protected _uniqueKeys As New List(Of KeyUnique)
        Public ReadOnly Property UniqueKeys As List(Of KeyUnique)
            Get
                Return _uniqueKeys
            End Get
        End Property

        Protected _foreignKeys As New List(Of KeyForeign)
        Public ReadOnly Property ForeignKeys As List(Of KeyForeign)
            Get
                Return _foreignKeys
            End Get
        End Property

        Public Function ColumnNamed(column As String) As Column

            Return _columns.SingleOrDefault(Function(x) x.Name = column)
        End Function

        Public Function ForeignKeyNamed(key As String) As KeyForeign

            Return _foreignKeys.SingleOrDefault(Function(x) x.Name = key)
        End Function

    End Class
End Namespace