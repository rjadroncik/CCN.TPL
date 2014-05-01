
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtTimestamp
        Inherits DataType

#Region "Properties"

        Public Property Precision As Integer?

#End Region

#Region "Initialization"

        Public Sub New(precision As Integer?)

            _Precision = precision
        End Sub

        Public Sub New()
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

            Return connector.ToSql(DirectCast(hodnota, DateTime?), Precision)
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(DateTime?)
            End Get
        End Property

        Public Overrides Function ToString() As String

            Return "Timestamp"
        End Function

#End Region

    End Class
End Namespace