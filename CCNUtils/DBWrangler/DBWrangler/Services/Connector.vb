Imports Npgsql
Imports Oracle.DataAccess.Client
Imports DBWrangler.Connectors
Imports DBWrangler.Enums
Imports System.Data.SqlClient

Namespace Services

    Public Class Connector

        Public Shared Function Create(vendor As DatabaseVendor, connectionString As String) As IConnector

            Select (vendor)
                Case DatabaseVendor.Oracle

                    Dim connection As New OracleConnection(connectionString)
                    connection.Open()

                    Return New OracleConnector(connection)

                Case DatabaseVendor.Postgre

                    Dim connection As New NpgsqlConnection(connectionString)
                    connection.Open()

                    Return New PgConnector(connection)

                Case DatabaseVendor.Microsoft

                    Dim connection As New SqlConnection(connectionString)
                    connection.Open()

                    Return New MsConnector(connection)
            End Select

            Throw New ArgumentException()
        End Function

        Public Shared Function CreateConnection(vendor As DatabaseVendor, connectionString As String) As IDbConnection

            Select Case (vendor)
                Case DatabaseVendor.Oracle

                    Dim connection As New OracleConnection(connectionString)
                    connection.Open()

                    Return connection

                Case DatabaseVendor.Postgre

                    Dim connection As New NpgsqlConnection(connectionString)
                    connection.Open()

                    Return connection

                Case DatabaseVendor.Microsoft

                    Dim connection As New SqlConnection(connectionString)
                    connection.Open()

                    Return connection
            End Select

            Throw New ArgumentException()
        End Function

    End Class

End Namespace