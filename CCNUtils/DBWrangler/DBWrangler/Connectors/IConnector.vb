Imports System.Data.Common
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Datatypes
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Enums

Namespace Connectors

    Public Interface IConnector

#Region "Info"

        ReadOnly Property Vendor As DatabaseVendor

#End Region

#Region "Values"

        Function ToSql(hodnota As Date?, precision As Integer?) As String
        Function ToSql(hodnota As Decimal?) As String
        Function ToSql(hodnota As Single?) As String
        Function ToSql(hodnota As Double?) As String
        Function ToSql(hodnota As Char?) As String
        Function ToSql(hodnota As Int16?) As String
        Function ToSql(hodnota As Int32?) As String
        Function ToSql(hodnota As Int64?) As String
        Function ToSql(hodnota As Guid?) As String
        Function ToSql(hodnota As String) As String
        Function ToSql(hodnota As Boolean?) As String
        Function ToSql(hodnota As Byte?) As String
        Function ToSql(hodnota As Object) As String

#End Region

#Region "Datatypes"

        Function ToSql(typ As DtDate) As String
        Function ToSql(typ As DtDecimal) As String
        Function ToSql(typ As DtSingle) As String
        Function ToSql(typ As DtDouble) As String
        Function ToSql(typ As DtChar) As String
        Function ToSql(typ As DtInt16) As String
        Function ToSql(typ As DtInt32) As String
        Function ToSql(typ As DtInt64) As String
        Function ToSql(typ As DtGuid) As String
        Function ToSql(typ As DtString) As String
        Function ToSql(typ As DtBoolean) As String
        Function ToSql(typ As DtByte) As String
        Function ToSql(typ As DtByteArray) As String
        Function ToSql(typ As DtVariant) As String
        Function ToSql(typ As DtTimestamp) As String

#End Region

#Region "Read"

        Function ReadDate(reader As IDataReader, column As Column) As Date?
        Function ReadDecimal(reader As IDataReader, column As Column) As Decimal?
        Function ReadSingle(reader As IDataReader, column As Column) As Single?
        Function ReadDouble(reader As IDataReader, column As Column) As Double?
        Function ReadChar(reader As IDataReader, column As Column) As Char?
        Function ReadInt16(reader As IDataReader, column As Column) As Int16?
        Function ReadInt32(reader As IDataReader, column As Column) As Int32?
        Function ReadInt64(reader As IDataReader, column As Column) As Int64?
        Function ReadGuid(reader As IDataReader, column As Column) As Guid?
        Function ReadString(reader As IDataReader, column As Column) As String
        Function ReadBoolean(reader As IDataReader, column As Column) As Boolean?
        Function ReadByte(reader As IDataReader, column As Column) As Byte?
        Function ReadByteArray(reader As IDataReader, column As Column) As Byte()
        Function ReadVariant(reader As IDataReader, column As Column) As Object

#End Region

#Region "Execution"

        Function CommandNew(sql As String) As IDbCommand

        ReadOnly Property Connection As IDbConnection

        Function ExecuteNonQuery(sql As String) As Integer
        Function ExecuteScalar(sql As String) As Object

#End Region

#Region "Providers"

        ReadOnly Property SqlInfo As SqlInfo

        ReadOnly Property SqlCreate As SqlCreate
        ReadOnly Property SqlCopy As SqlCopy
        ReadOnly Property SqlDelete As SqlDelete
        ReadOnly Property SqlDrop As SqlDrop
        ReadOnly Property SqlExists As SqlExists
        ReadOnly Property SqlInsert As SqlInsert
        ReadOnly Property SqlInsertFiltered As SqlInsertFiltered
        ReadOnly Property SqlJoin As SqlJoin
        ReadOnly Property SqlSelect As SqlSelect
        ReadOnly Property SqlTruncate As SqlTruncate
        ReadOnly Property SqlUpdate As SqlUpdate

#End Region

    End Interface
End Namespace