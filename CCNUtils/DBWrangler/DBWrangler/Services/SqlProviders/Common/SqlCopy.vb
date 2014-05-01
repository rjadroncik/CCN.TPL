Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders.Common

    Public MustInherit Class SqlCopy
        Inherits SqlProvider

        Public Sub New(connector As IConnector)
            MyBase.New(connector)
        End Sub

        Public Overridable Sub Execute(source As Table, destination As Table)

            ExecuteNonQuery("Insert INTO " & destination.Name & "(" & ColumnList(destination.Columns) & _
                            ") SELECT " & ColumnList(source.Columns) & " FROM " & source.Name)
        End Sub

        Public MustOverride Sub Execute(table As Table, reader As IDataReader)

    End Class
End Namespace