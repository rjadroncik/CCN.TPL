﻿
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base

Namespace Model.Schema.Datatypes

    Public Class DtInt64
        Inherits DataType

#Region "Overridden"

        Public Overrides Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

            Return connector.ReadInt64(reader, column)
        End Function

        Public Overrides Function ToSql(connector As IConnector) As String

            Return connector.ToSql(Me)
        End Function

        Public Overrides Function ValueToSql(hodnota As Object, connector As IConnector) As String

            Return connector.ToSql(DirectCast(hodnota, Int64?))
        End Function

        Public Overrides ReadOnly Property Type As System.Type
            Get
                Return GetType(Int64?)
            End Get
        End Property

#End Region

    End Class
End Namespace