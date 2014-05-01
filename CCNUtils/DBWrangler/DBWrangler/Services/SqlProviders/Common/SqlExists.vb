
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public Class SqlExists
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overridable Function ExistsTable(table As Table) As String

            Return ExistsTable(table.Name)
        End Function

        Protected Overridable Function ExistsTable(name As String) As String

            Return "IF OBJECT_ID('" & name & "','U') IS NOT NULL "
        End Function

        Public Overridable Function Execute(table As Table) As Boolean

            Dim reader As IDataReader = _connector.CommandNew(ExistsTable(table) & " SELECT 1").ExecuteReader()
            Dim exist As Boolean = reader.Read()

            If (Not reader.IsClosed) Then reader.Close()

            Return exist
        End Function
    End Class
End Namespace