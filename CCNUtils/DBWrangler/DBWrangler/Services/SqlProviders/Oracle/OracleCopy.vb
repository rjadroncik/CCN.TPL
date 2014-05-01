Imports System.Data.SqlClient
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Oracle

    Public Class OracleCopy
        Inherits SqlCopy

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _scriptStart = "BEGIN" + Environment.NewLine
            _scriptEnd = Environment.NewLine + "END;"
        End Sub

        Public Overrides Sub Execute(table As Table, reader As IDataReader)

            Using bcp As New SqlBulkCopy(DirectCast(_connector.Connection, SqlConnection), SqlBulkCopyOptions.TableLock, Nothing)

                bcp.DestinationTableName = table.Name
                bcp.BatchSize = 0
                bcp.BulkCopyTimeout = 100000

                Dim dt As New DataTable(table.Name)

                For Each column As Column In table.Columns

                    bcp.ColumnMappings.Add(New SqlBulkCopyColumnMapping(column.Name, column.Name))
                    dt.Columns.Add(column.Name, column.DataType.Type)
                Next

                While (reader.Read())

                    Dim row As DataRow = dt.NewRow()

                    For Each column As Column In table.Columns

                        Dim index As Integer = reader.GetOrdinal(column.Name)

                        If (reader.IsDBNull(index)) Then

                            row(column.Name) = DBNull.Value
                        Else
                            row(column.Name) = column.DataType.Read(reader, column, _connector)
                        End If
                    Next

                    dt.Rows.Add(row)

                    If (dt.Rows.Count >= 10000) Then

                        bcp.WriteToServer(dt)
                        dt.Rows.Clear()
                    End If
                End While

                If (dt.Rows.Count > 0) Then

                    bcp.WriteToServer(dt)
                    dt.Rows.Clear()
                End If

            End Using
        End Sub
    End Class
End Namespace