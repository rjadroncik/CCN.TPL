
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtDate
        Inherits DataType

#Region "Overridden"

        Public Property LowPrecision As Boolean

#End Region

#Region "Initialization"

        Public Sub New(Optional lowPrecision As Boolean = False)

            _LowPrecision = lowPrecision
        End Sub

#End Region

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadDate(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Return connector.ToSql(DirectCast(hodnota, Date?))
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(Date?)
            End Get
        End Property

#End Region

    End Class
End Namespace