Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports CCN.Model
Imports CCN.Services

Public Class DataTask
    Inherits Service

#Region "Initialization"

    Protected Shared _target As SqlConnection
    Protected Shared _name As String

    Public Shared Sub Start(destination As String, name As String)

        If (_target IsNot Nothing) Then Throw New InvalidOperationException()

        _name = name
        LogSubor.WriteLine("Starting task: " & _name)

        _target = New SqlConnection(destination)
        _target.Open()
    End Sub

    Public Shared Sub Finish()

        _target.Close()
        _target.Dispose()
        _target = Nothing

        LogSubor.WriteLine("Finished task: " & _name)
    End Sub

#End Region

#Region "Table properties"

    Protected Shared Sub IdentityInsert(connection As SqlConnection, table As String, enable As Boolean)

        Try
            Using command As New SqlCommand("SET IDENTITY_INSERT " & table & If(enable, " ON", " OFF"), connection)

                command.ExecuteNonQuery()
            End Using

        Catch ex As Exception

            LogSubor.WriteException(ex)
        End Try
    End Sub

    Protected Shared Sub Constraints(connection As SqlConnection, table As String, enable As Boolean)

        Using command As New SqlCommand("ALTER TABLE " & table & If(enable, " ", " NO") & "CHECK CONSTRAINT ALL", connection)

            command.ExecuteNonQuery()
        End Using
    End Sub

#End Region

#Region "SQL execution"

    Protected Shared Sub ExecuteFile(filename As String)

        ExecuteFile(_target, filename)
    End Sub

    Protected Shared Sub ExecuteFile(connection As SqlConnection, filename As String)

        LogSubor.WriteLine("Starting executing file " & filename & ".")

        Using command As New SqlCommand(File.ReadAllText(filename), connection)

            command.ExecuteNonQuery()
        End Using

        LogSubor.WriteLine("Finished executing file " & filename & ".")
    End Sub

#End Region

End Class
