Imports CCN.Core.VB
Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Oracle
Imports DBWrangler.Services.IO
Imports System.IO
Imports Oracle.DataAccess.Client
Imports Npgsql
Imports DBWrangler.Model.Schema
Imports System.Xml.Schema
Imports System.Text
Imports DBWrangler.Services.SqlProviders

<TestClass()>
Public Class TestOracleExport

    <TestMethod()>
    Public Sub SecurityServiceDataMigration()

        Dim connection = New OracleConnection(OracleConnector.BuildConnectionString("IDATLAS", Nothing, "accountisolation", "sporting"))
        connection.Open()

        Dim tables = New String() {"AI_PERMISSION", "AI_ROLE", "AI_ROLE_PERMISSION"}

        Dim schema As Schema = OracleExport.Execute(tables, "ACCOUNTISOLATION", New ProgressReporter(), New OracleConnector(connection))

        Dim connectorOracle = New OracleConnector(connection)
        Dim connectorPg = New PgConnector(Nothing)

        For Each table As Object In schema.Tables

            Dim result As New StringBuilder
            Dim context As New QueryContext

            connectorPg.SqlInsert.Sql(table, False, connectorOracle.SqlSelect.Execute(table), result, context, 1000)

            Using writer = New StreamWriter(table.Name & ".sql")

                writer.Write(result.ToString())
            End Using
        Next
    End Sub

    <TestMethod()>
    Public Sub TestExportOracle()

        Dim connection = New OracleConnection(OracleConnector.BuildConnectionString("IDATLAS", Nothing, _
                                                                                    "accountisolation", "sporting"))
        connection.Open()

        Dim tables = New String() {"STAGE_AI_ACCOUNT", "STAGE_AI_BET", "STAGE_AI_BET_HISTORY", "STAGE_AI_BET_JOURNAL", "STAGE_COMMITS"}

        Dim schema As Schema = OracleExport.Execute(tables, "ACCOUNTISOLATION", New ProgressReporter(), New OracleConnector(connection))

        Dim fileExported As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test_exported.xml"

        SchemaXmlWriter.Write(schema, fileExported)

        Dim schemaReconstructed As Schema = SchemaXmlReader.Read(fileExported)

        Dim connector = New PgConnector(Nothing)

        Dim sql = connector.SqlCreate.Sql(schemaReconstructed, True, True, True, True, True)

    End Sub

    <TestMethod()>
    Public Sub TestExportOracleImportPostgre()

        Dim connection = New OracleConnection(OracleConnector.BuildConnectionString("IDATLAS", Nothing, _
                                                                                    "accountisolation", "sporting"))
        connection.Open()

        Dim tables = New String() {"AI_BET", "AI_BET_HISTORY", "AI_BET_JOURNAL", "AI_ACCOUNT"} '{"AI_BET"} '{"AI_PERMISSION", "AI_ROLE", "AI_ROLE_PERMISSION", "AI_SECURITY_LOG"}

        Dim schema As Schema = OracleExport.Execute(tables, "ACCOUNTISOLATION", New ProgressReporter(), New OracleConnector(connection))

        Dim fileExported As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test_exported.xml"

        SchemaXmlWriter.Write(schema, fileExported)

        Dim schemaReconstructed As Schema = SchemaXmlReader.Read(fileExported)

        'DSK-ENG-02398
        Dim pgConnection = New NpgsqlConnection(PgConnector.BuildConnectionString("localhost", "SPIN.Security", "postgres", "postgres"))
        pgConnection.Open()

        Dim connector = New PgConnector(pgConnection)

        connector.SqlCreate.Execute(schemaReconstructed, True, True, True, True, True)
    End Sub

    '<TestMethod()>
    'Public Sub TestExportPg()

    '    Dim connection = New NpgsqlConnection(PgConnector.BuildConnectionString("DSK-ENG-02398", "SPIN.Security", _
    '                                                                            "accountisolation", "sporting"))
    '    connection.Open()

    '    Dim schema As Schema = OrExport.Execute(Nothing, New ProgressReporter(), New MsConnector(connection))

    '    Dim fileExported As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test_exported.xml"

    '    SchemaXmlWriter.Write(schema, fileExported)

    '    Dim schemaReconstructed As Schema = SchemaXmlReader.Read(fileExported)

    'End Sub

End Class
