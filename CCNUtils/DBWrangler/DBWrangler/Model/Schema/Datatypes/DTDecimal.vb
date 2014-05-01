
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtDecimal
        Inherits DataType

#Region "Properties"

        Public Property Precision As Integer?
        Public Property Scale As Integer?

        Public Property Money As Boolean

#End Region

#Region "Initialization"

        Public Sub New(precision As Integer?, scale As Integer?, Optional money As Boolean = False)

            _Precision = precision
            _Scale = scale
            _Money = money
        End Sub

        Public Sub New()
        End Sub

#End Region

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadDecimal(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Return connector.ToSql(DirectCast(hodnota, Decimal?))
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(Decimal?)
            End Get
        End Property

#End Region

    End Class
End Namespace