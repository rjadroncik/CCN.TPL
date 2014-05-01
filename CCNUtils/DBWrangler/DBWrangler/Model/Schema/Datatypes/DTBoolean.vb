
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtBoolean
        Inherits DataType

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadBoolean(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Return connector.ToSql(DirectCast(hodnota, Boolean?))
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(Boolean?)
            End Get
        End Property

#End Region

    End Class
End Namespace