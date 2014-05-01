
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtString
        Inherits DataType

#Region "Properties"

        Public Property Unicode As Boolean

        Public Property Size As Integer
        Public Property SizeFixed As Boolean

#End Region

#Region "Initialization"

        Public Sub New(size As Integer, Optional sizeFixed As Boolean = False, Optional unicode As Boolean = True)

            _Size = size
            _SizeFixed = sizeFixed
            _Unicode = unicode
        End Sub

#End Region

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadString(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Return connector.ToSql(DirectCast(hodnota, String))
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(String)
            End Get
        End Property

#End Region

    End Class
End Namespace