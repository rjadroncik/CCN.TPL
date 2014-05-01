
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtByteArray
        Inherits DataType

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadByteArray(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Throw New NotImplementedException("Can't generate SQL for a byte array field!")
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(Byte())
            End Get
        End Property

#End Region

    End Class
End Namespace