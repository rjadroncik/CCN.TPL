
Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlDrop
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overridable Function DropIndex(index As Index) As String

            Return "IF EXISTS (SELECT name FROM sys.indexes WHERE name = '" & index.Name & "') DROP INDEX " & index.Name & " ON " & index.Table.Name & _commandEnd
        End Function

        Public Overridable Function DropIndexes(table As Table) As String

            Dim sql As String = ""

            For Each index As Index In table.Indexes

                sql &= DropIndex(index)
            Next

            Return sql
        End Function

        Public Overridable Function DropUniqueKey(uniqueKey As KeyUnique) As String

            Return "IF OBJECT_ID('" & uniqueKey.Name & "','UQ') IS NOT NULL ALTER TABLE " & uniqueKey.Table.Name & " DROP CONSTRAINT " & uniqueKey.Name & _commandEnd
        End Function

        Public Overridable Function DropUniqueKeys(table As Table) As String

            Dim sql As String = ""

            For Each uniqueKey As KeyUnique In table.UniqueKeys

                sql &= DropUniqueKey(uniqueKey)
            Next

            Return sql
        End Function

        Public Overridable Function DropPrimaryKey(key As KeyPrimary) As String

            If ((key.Table.PrimaryKey Is Nothing) OrElse (key.Table.PrimaryKey.Columns.IsEmpty())) Then Return ""

            Return "IF OBJECT_ID('" & key.Table.PrimaryKey.Name & "','PK') IS NOT NULL ALTER TABLE " & key.Table.Name & " DROP CONSTRAINT " & key.Table.PrimaryKey.Name & _commandEnd
        End Function

        Public Overridable Function DropForeignKey(foreignKey As KeyForeign) As String

            Return "IF OBJECT_ID('" & foreignKey.Name & "','F') IS NOT NULL ALTER TABLE " & foreignKey.Table.Name & " DROP CONSTRAINT " & foreignKey.Name & _commandEnd
        End Function

        Public Overridable Function DropForeignKeys(table As Table) As String

            Dim sql As String = ""

            For Each foreignKey As KeyForeign In table.ForeignKeys

                sql &= DropForeignKey(foreignKey)
            Next

            Return sql
        End Function

        Public Overridable Function DropTable(table As Table) As String

            Return "IF OBJECT_ID('" & table.Name & "','U') IS NOT NULL DROP TABLE " & table.Name & _commandEnd
        End Function

        Public Overridable Function Sql(schema As Schema) As String

            Dim result As String = ""

            For Each table As Table In schema.Tables

                result &= DropForeignKeys(table)
            Next

            For Each table As Table In schema.Tables

                result &= DropTable(table)
            Next

            Return result
        End Function

        Public Overridable Sub Execute(schema As Schema)

            ExecuteNonQuery(Sql(schema))
        End Sub

        Public Overridable Sub Execute(table As Table)

            ExecuteNonQuery(DropTable(table))
        End Sub

        Public Overridable Sub Execute(key As KeyPrimary)

            ExecuteNonQuery(DropPrimaryKey(key))
        End Sub

        Public Overridable Sub Execute(key As KeyForeign)

            ExecuteNonQuery(DropForeignKey(key))
        End Sub

        Public Overridable Sub Execute(key As KeyUnique)

            ExecuteNonQuery(DropUniqueKey(key))
        End Sub

        Public Overridable Sub Execute(key As Index)

            ExecuteNonQuery(DropIndex(key))
        End Sub

    End Class
End Namespace