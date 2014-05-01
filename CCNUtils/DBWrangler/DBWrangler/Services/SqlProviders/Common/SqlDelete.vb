
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlDelete
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overridable Function DeleteTable(table As Table) As String

            Return "DELETE FROM " & table.Name
        End Function

        Public Overridable Function DeleteTable(table As Table, condition As Condition) As String

            Return DeleteTable(table) & " " & condition.ToSql(_connector)
        End Function

        Public Overridable Sub Execute(table As Table)

            ExecuteNonQuery(DeleteTable(table) & _commandEnd)
        End Sub

        Public Overridable Sub Execute(table As Table, condition As Condition)

            ExecuteNonQuery(DeleteTable(table, condition) & _commandEnd)
        End Sub

        'Protected Shared Comparer As IEqualityComparer(Of Table) = Comparing.EqualityComparer(Of Table)(Function(x, y) x.Name.Equals(y.Name), _
        '                                                                                                Function(x) x.Name.GetHashCode())
        'Protected Shared Function Compare(first As Table, second As Table) As Integer

        '    If (first Is second) Then Return 0

        '    Dim firstReferences = first.ForeignKeys.Select(Function(x) x.ReferencedKey.Table).ToList()
        '    Dim secondReferences = second.ForeignKeys.Select(Function(x) x.ReferencedKey.Table).ToList()

        '    If ((first.ForeignKeys IsNot Nothing) AndAlso _
        '         first.ForeignKeys.Select(Function(x) x.ReferencedKey.Table.Name).Contains(second.Name)) Then Return -1

        '    If ((second.ForeignKeys IsNot Nothing) AndAlso _
        '         second.ForeignKeys.Select(Function(x) x.ReferencedKey.Table.Name).Contains(first.Name)) Then Return 1

        '    Return 0
        'End Function

        'Dim comparer = Comparing.Comparer(Of Table)(Function(x, y) Less(x, y), Function(x, y) Equal(x, y))
        '.OrderBy(Function(x) x, Comparing.Comparer(Of Table)(AddressOf Compare))

        Public Overridable Sub Execute(schema As Schema, Optional tables As IEnumerable(Of String) = Nothing)

            Dim tablesFiltered = schema.Tables.Where(Function(x) (tables Is Nothing) OrElse tables.Contains(x.Name)).ToList()

            Dim sql As String = ""

            For Each table As Table In tablesFiltered

                sql &= "ALTER TABLE " & table.Name & " NOCHECK CONSTRAINT all;" & Environment.NewLine
            Next

            For Each table As Table In tablesFiltered

                sql &= DeleteTable(table) & _commandEnd
            Next

            For Each table As Table In tablesFiltered

                sql &= "ALTER TABLE " & table.Name & " WITH CHECK CHECK CONSTRAINT all;" & Environment.NewLine
            Next

            ExecuteNonQuery(sql)
        End Sub
    End Class
End Namespace