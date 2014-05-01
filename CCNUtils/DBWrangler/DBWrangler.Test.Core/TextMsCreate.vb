Imports System.Text
Imports System.Data.SqlClient
Imports CCN.Services
Imports System.IO

<TestClass()>
Public Class TextMsCreate

    <TestMethod()>
    Public Sub TestCreate()

        Dim fileExported As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test_exported.xml"

        Dim schema As Schema = SchemaXmlReader.Read(fileExported)

        Dim connection As SqlConnection = New SqlConnection(Database.BuildConnectionString("ROMANPC\SQL2008R2", "dali_lieky_new", _
                                                                                   "dali", ".C0sm0s.", True))
        connection.Open()

        Dim connector As New MsConnector(connection)

        Dim sql As String = connector.SqlCreate.Sql(schema, True, True, True, True, True)

        Dim fileSql As String = System.AppDomain.CurrentDomain.BaseDirectory & "\Data\Schema\test.sql"

        Using writer As New StreamWriter(fileSql)

            writer.Write(sql)
        End Using
    End Sub

End Class
