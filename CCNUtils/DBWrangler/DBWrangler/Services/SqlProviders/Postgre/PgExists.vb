
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema
Imports DBWrangler.Services.SqlProviders.Common

Namespace Services.SqlProviders.Postgre

    Public Class PgExists
        Inherits SqlExists

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Protected Overrides Function ExistsTable(name As String) As String

            Return String.Format("SELECT relname FROM pg_class WHERE relname = '{0}' AND relkind='r'", name.ToLower())
        End Function

        Public Overrides Function Execute(table As Table) As Boolean

            Dim reader As IDataReader = _connector.CommandNew(ExistsTable(table)).ExecuteReader()
            Dim exist As Boolean = reader.Read()

            If (Not reader.IsClosed) Then reader.Close()

            Return exist
        End Function
    End Class
End Namespace