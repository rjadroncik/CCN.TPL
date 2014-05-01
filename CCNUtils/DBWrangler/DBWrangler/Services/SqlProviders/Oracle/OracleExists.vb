
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Oracle

    Public Class OracleExists
        Inherits SqlExists

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overrides Function ExistsTable(name As String) As String

            Return String.Format("SELECT table_name FROM user_tables WHERE table_name = '{0}'", name.ToUpper())
        End Function

        Public Overrides Function Execute(table As Table) As Boolean

            Dim reader As IDataReader = _connector.CommandNew(ExistsTable(table)).ExecuteReader()
            Dim exist As Boolean = reader.Read()

            If (Not reader.IsClosed) Then reader.Close()

            Return exist
        End Function
    End Class
End Namespace