Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgDrop
        Inherits SqlDrop

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overrides Function DropIndex(index As Index) As String

            Return "DROP INDEX IF EXISTS " & index.Name & _commandEnd
        End Function

        Public Overrides Function DropUniqueKey(uniqueKey As KeyUnique) As String

            Return "ALTER TABLE " & uniqueKey.Table.Name & " DROP CONSTRAINT IF EXISTS " & uniqueKey.Name & _commandEnd
        End Function

        Public Overrides Function DropPrimaryKey(key As KeyPrimary) As String

            If ((key.Table.PrimaryKey Is Nothing) OrElse (key.Table.PrimaryKey.Columns.IsEmpty())) Then Return ""

            Return "ALTER TABLE " & key.Table.Name & " DROP CONSTRAINT IF EXISTS " & key.Table.PrimaryKey.Name & _commandEnd
        End Function

        Public Overrides Function DropForeignKey(foreignKey As KeyForeign) As String

            Return "ALTER TABLE " & foreignKey.Table.Name & " DROP CONSTRAINT IF EXISTS " & foreignKey.Name & _commandEnd
        End Function

        Public Overrides Function DropTable(table As Table) As String

            Return "DROP TABLE IF EXISTS " & table.Name & _commandEnd
        End Function

    End Class
End Namespace