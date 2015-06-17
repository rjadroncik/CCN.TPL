Imports CCN.Core.VB
Imports System.Data.SqlClient
Imports DBWrangler.Model.Schema.Datatypes
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Microsoft
Imports DBWrangler.Services.IO

<TestClass()>
Public Class TestMsExport

    <TestMethod()>
    Public Sub TestExportMs()

        'Dim connection = New SqlConnection(Database.BuildConnectionString("ROMANPC\SQL2008R2", "dali_lieky_new", _
        '                                                                  "dali", ".C0sm0s.", True))

        Dim connection = New SqlConnection(Database.BuildConnectionString("ROMANPC\SQL2008R2", "objednavky", _
                                                                          "sunes", ".sunes.", True))
        connection.Open()

        Dim schema As Schema = MsExport.Execute(Nothing, New ProgressReporter(), New MsConnector(connection))

        Dim fileExported As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test_exported.xml"

        SchemaXmlWriter.Write(schema, fileExported)

        Dim schemaReconstructed As Schema = SchemaXmlReader.Read(fileExported)

    End Sub

    <TestMethod()>
    Public Sub TestFindColumnsEan()

        Dim connection As SqlConnection = New SqlConnection(Database.BuildConnectionString("ROMANPC\SQL2008R2", "objednavky", _
                                                                                           "sunes", ".sunes.", True))
        connection.Open()

        Dim connector = New MsConnector(connection)

        Dim schema As Schema = MsExport.Execute(Nothing, New ProgressReporter(), connector)

        For Each table In schema.Tables

            For Each column In table.Columns

                If ((column.Name.ToLower().Contains("ean") OrElse _
                    column.Name.ToLower().Contains("ciar") OrElse _
                    column.Name.ToLower().Contains("bar")) AndAlso _
                    column.DataType.Type Is GetType(String)) Then

                    'Console.Out.WriteLine("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2} {3}NULL;", _
                    '                      table.Name, column.Name, _
                    '                      "VARCHAR(25)", _
                    '                      If(column.Nullable, "", "NOT "))

                    'column.DataType.ToSql(connector), _

                    Console.Out.WriteLine("UPDATE [{0}] SET [{1}] = RTRIM({1})", _
                                          table.Name, column.Name)
                End If
            Next
        Next

    End Sub

    <TestMethod()>
    Public Sub TestFindColumnsFloat()

        Dim connection As SqlConnection = New SqlConnection(Database.BuildConnectionString("ROMANPC\SQL2008R2", "objednavky", _
                                                                                           "sunes", ".sunes.", True))
        connection.Open()

        Dim connector = New MsConnector(connection)

        Dim schema As Schema = MsExport.Execute(Nothing, New ProgressReporter(), connector)

        For Each table In schema.Tables

            For Each column In table.Columns

                If (TypeOf column.DataType Is DtDouble) Then

                    Console.Out.WriteLine("{0}.{1} {2}", table.Name, column.Name, column.DataType.ToSql(connector))
                End If
            Next
        Next
    End Sub

    <TestMethod()>
    Public Sub TestFindColumnsSukl()

        Dim connection As SqlConnection = New SqlConnection(Database.BuildConnectionString("ROMANPC\SQL2008R2", "objednavky", _
                                                                                           "sunes", ".sunes.", True))
        connection.Open()

        Dim connector = New MsConnector(connection)

        Dim schema As Schema = MsExport.Execute(Nothing, New ProgressReporter(), connector)

        For Each table In schema.Tables

            For Each column In table.Columns

                If (column.Name.ToLower().Contains("sukl") AndAlso _
                    column.DataType.Type Is GetType(String)) Then

                    Console.Out.WriteLine("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2} {3}NULL;", _
                              table.Name, column.Name, _
                              "VARCHAR(6)", _
                              If(column.Nullable, "", "NOT "))
                End If
            Next
        Next

        For Each table In schema.Tables

            For Each column In table.Columns

                If (column.Name.ToLower().Contains("sukl") AndAlso _
                    column.DataType.Type Is GetType(String)) Then

                    'Console.Out.WriteLine("{0} {1}", column.Name, column.DataType.ToSql(connector))

                    'column.DataType.ToSql(connector), _

                    Console.Out.WriteLine("UPDATE [{0}] SET [{1}] = LTRIM(RTRIM({1}))", _
                                          table.Name, column.Name)
                End If
            Next
        Next

    End Sub

End Class
