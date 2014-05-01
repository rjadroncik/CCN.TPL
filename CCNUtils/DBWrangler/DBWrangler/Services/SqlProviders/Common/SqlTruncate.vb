
Imports System.Text
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlTruncate
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overridable Function TruncateTable(table As Table) As String

            Return _commandStart & "TRUNCATE TABLE " & table.Name & _commandEnd
        End Function

        Public Overridable Function Sql(schema As Schema, result As StringBuilder, Optional tables As ICollection(Of String) = Nothing) As StringBuilder

            Dim tablesFiltered = schema.Tables.Where(Function(x) (tables Is Nothing) OrElse tables.Contains(x.Name)).ToList()

            For Each table As Table In tablesFiltered

                result.AppendLine(_connector.SqlDrop.DropForeignKeys(table))
            Next

            For Each table As Table In tablesFiltered

                result.AppendLine(TruncateTable(table))
            Next

            For Each table As Table In tablesFiltered

                result.AppendLine(_connector.SqlCreate.CreateForeignKeys(table))
            Next

            Return result
        End Function

        Public Overridable Sub Execute(schema As Schema, Optional tables As ICollection(Of String) = Nothing)

            ExecuteNonQuery(Sql(schema, New StringBuilder(), tables))
        End Sub

        Public Overridable Sub Execute(table As Table)

            ExecuteNonQuery(TruncateTable(table))
        End Sub

    End Class
End Namespace