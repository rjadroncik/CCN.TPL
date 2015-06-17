Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.VisualBasic

Public Module Database

#Region "Utils"

    Public Function BuildConnectionStringNoDB(server As String, user As String, password As String, _
                                                     Optional mars As Boolean = False) As String

        Return BuildConnectionString(server, Nothing, user, password, mars)
    End Function

    Public Function BuildConnectionString(server As String, database As String, user As String, password As String, _
                                                 Optional mars As Boolean = False) As String

        Return "Data Source=" & server & If(database Is Nothing, "", ";Initial Catalog=" & database) & _
            ";Persist Security Info=False;User ID=" & user & ";Password=" & password & _
            If(mars, ";MultipleActiveResultSets=True", "")
    End Function

    Public Function DatabaseExists(connection As SqlConnection, database As String) As Boolean

        Using command As New SqlCommand("IF EXISTS(SELECT * FROM sys.databases WHERE name = '" & database & "') SELECT 1", connection)

            Return CType(command.ExecuteScalar(), Boolean)
        End Using
    End Function

    Public Sub DatabaseCreate(connection As SqlConnection, database As String, _
                                     Optional collation As String = "SQL_Latin1_General_Cp1250_CI_AS", _
                                     Optional simpleRecovery As Boolean = False)

        Dim sql = "CREATE DATABASE " & database & " COLLATE " & collation & ";" & System.Environment.NewLine & _
                  "ALTER DATABASE " & database & " MODIFY FILE (NAME = " & database & ", FILEGROWTH  = 10MB);" & System.Environment.NewLine & _
                  "ALTER DATABASE " & database & " MODIFY FILE (NAME = " & database & "_log, FILEGROWTH  = 10MB);"

        If (simpleRecovery) Then sql &= System.Environment.NewLine & "ALTER DATABASE " & database & " SET RECOVERY SIMPLE;"

        Using command As New SqlCommand(sql, connection)

            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub DatabaseDrop(connection As SqlConnection, database As String)

        Using command As New SqlCommand("DROP DATABASE " & database & ";", connection)

            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub DatabaseSelect(connection As SqlConnection, database As String)

        Using command As New SqlCommand("USE " & database & ";", connection)

            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ExecuteScript(connectionString As String, script As String)

        Using connection = New SqlConnection(connectionString)
            connection.Open()

            For Each part In script.Split(New String() {"GO" & ControlChars.Lf, "GO" & ControlChars.CrLf}, StringSplitOptions.RemoveEmptyEntries)

                Using command As New SqlCommand(part, connection)

                    command.CommandTimeout = 60 * 15
                    command.ExecuteNonQuery()
                End Using
            Next
        End Using
    End Sub

#End Region

End Module