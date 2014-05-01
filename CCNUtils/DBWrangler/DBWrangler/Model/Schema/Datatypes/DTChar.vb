
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtChar
        Inherits DataType

#Region "Properties"

        Public Property Unicode As Boolean = True

#End Region

#Region "Initialization"

        Public Sub New(Optional unicode As Boolean = True)

            _Unicode = unicode
        End Sub

#End Region

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadChar(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Return connector.ToSql(DirectCast(hodnota, Char?))
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(Char?)
            End Get
        End Property

#End Region

    End Class
End Namespace