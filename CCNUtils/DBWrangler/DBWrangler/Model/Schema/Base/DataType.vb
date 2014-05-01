
Imports DBWrangler.Connectors

Namespace Model.Schema.Base

    Public MustInherit Class DataType

#Region "BL"

        Public Overridable Function ReadToSql(reader As IDataReader, column As Column, connector As IConnector) As String

            Return column.DataType.ValueToSql(column.DataType.Read(reader, column, connector), connector)
        End Function

#End Region

#Region "Prescribed"

        Public MustOverride Function Read(reader As IDataReader, column As Column, connector As IConnector) As Object

        Public MustOverride Function ValueToSql(hodnota As Object, connector As IConnector) As String

        Public MustOverride Function ToSql(connector As IConnector) As String

#End Region

#Region "Properties"

        Public MustOverride ReadOnly Property Type As System.Type

#End Region

#Region "Overridden"

        Public Overrides Function ToString() As String

            Dim underlyingType As Type = Nullable.GetUnderlyingType(Type)

            Return If(underlyingType Is Nothing, Type, underlyingType).Name
        End Function

#End Region

    End Class
End Namespace