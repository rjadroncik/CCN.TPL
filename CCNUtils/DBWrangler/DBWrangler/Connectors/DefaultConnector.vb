Imports System.Data.Common
Imports System.Data.SqlTypes
Imports CCN.Core.VB
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Datatypes
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Enums

Namespace Connectors

    Public MustInherit Class DefaultConnector
        Implements IConnector

#Region "Values"

        Protected Function DateTimePrecisionString(precision As Integer?) As String

            If (Not precision.HasValue) Then Return ""

            Return "." & String.Join("", Enumerable.Repeat("f", precision.Value))
        End Function

        Public MustOverride ReadOnly Property Vendor() As DatabaseVendor Implements IConnector.Vendor

        Public Overridable Function ToSql(hodnota As Date?, precision As Integer?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return "'" & If(hodnota.Value < SqlDateTime.MinValue.Value, _
                            SqlDateTime.MinValue.Value, hodnota.Value).ToString("yyyy-MM-dd HH:mm:ss" & DateTimePrecisionString(precision)) & "'"
        End Function

        Public Overridable Function ToSql(hodnota As Decimal?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Single?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Double?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Char?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return "'" & hodnota.Value.ToString() & "'"
        End Function

        Public Overridable Function ToSql(hodnota As Byte?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Int16?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Int32?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Int64?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As Guid?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return hodnota.Value.ToStringInvariant()
        End Function

        Public Overridable Function ToSql(hodnota As String) As String Implements IConnector.ToSql

            If (hodnota Is Nothing) Then Return "NULL"

            Return "'" & hodnota.ToString().Replace("'", "''") & "'"
        End Function

        Public Overridable Function ToSql(hodnota As Boolean?) As String Implements IConnector.ToSql

            If (Not hodnota.HasValue) Then Return "NULL"

            Return If(hodnota.Value, "1", "0")
        End Function

        Public Overridable Function ToSql(hodnota As Object) As String Implements IConnector.ToSql

            If (hodnota Is Nothing) Then Return "NULL"

            'TODO: [DBWrangler] DtVariant - ToSql
            Return hodnota.ToString()
        End Function

#End Region

#Region "Datatypes"

        Public Overridable Function ToSql(typ As DtDate) As String Implements IConnector.ToSql

            Return If(typ.LowPrecision, "SMALLDATETIME", "DATETIME")
        End Function

        Public Overridable Function ToSql(typ As DtDecimal) As String Implements IConnector.ToSql

            With typ
                If (.Money) Then Return "MONEY"

                If (Not .Precision.HasValue) Then

                    Return "DECIMAL" & If(Not .Scale.HasValue, "", "(0, " & .Scale & ")")
                Else
                    Return "DECIMAL(" & .Precision & If(Not .Scale.HasValue, ")", ", " & .Scale & ")")
                End If
            End With
        End Function

        Public Overridable Function ToSql(typ As DtSingle) As String Implements IConnector.ToSql

            Return "REAL"
        End Function

        Public Overridable Function ToSql(typ As DtDouble) As String Implements IConnector.ToSql

            Return "FLOAT"
        End Function

        Public Overridable Function ToSql(typ As DtChar) As String Implements IConnector.ToSql

            Return If(typ.Unicode, "N", "") & "CHAR(1)"
        End Function

        Public Overridable Function ToSql(typ As DtByte) As String Implements IConnector.ToSql

            Return "TINYINT"
        End Function

        Public Overridable Function ToSql(typ As DtInt16) As String Implements IConnector.ToSql

            Return "SMALLINT"
        End Function

        Public Overridable Function ToSql(typ As DtInt32) As String Implements IConnector.ToSql

            Return "INT"
        End Function

        Public Overridable Function ToSql(typ As DtInt64) As String Implements IConnector.ToSql

            Return "BIGINT"
        End Function

        Public Overridable Function ToSql(typ As DtGuid) As String Implements IConnector.ToSql

            Return "UNIQUEIDENTIFIER"
        End Function

        Public Overridable Function ToSql(typ As DtString) As String Implements IConnector.ToSql

            With typ
                If (.Size > If(.Unicode, 4000, 8000)) Then Return If(.Unicode, "NTEXT", "TEXT")

                Return If(.Unicode, "N", "") & If(.SizeFixed, "", "VAR") & "CHAR(" & If(.Size = -1, "MAX", .Size.ToStringInvariant()) & ")"
            End With
        End Function

        Public Overridable Function ToSql(typ As DtBoolean) As String Implements IConnector.ToSql

            Return "BIT"
        End Function

        Public Overridable Function ToSql(typ As DtByteArray) As String Implements IConnector.ToSql

            Return "VARBINARY(MAX)"
        End Function

        Public Overridable Function ToSql(typ As DtVariant) As String Implements IConnector.ToSql

            Return "SQL_VARIANT"
        End Function

        Public Overridable Function ToSql(typ As DtTimestamp) As String Implements IConnector.ToSql

            Return "TIMESTAMP" & If(typ.Precision.HasValue, "(" & typ.Precision.Value & ")", "")
        End Function

#End Region

#Region "Read"

        Public Overridable Function ReadDate(reader As IDataReader, column As Column) As Date? Implements IConnector.ReadDate

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Dim value As DateTime = reader.GetDateTime(index)

            Return If(value <= SqlDateTime.MinValue.Value, SqlDateTime.MinValue.Value, value)
        End Function

        Public Overridable Function ReadDecimal(reader As IDataReader, column As Column) As Decimal? Implements IConnector.ReadDecimal

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetDecimal(index)
        End Function

        Public Overridable Function ReadSingle(reader As IDataReader, column As Column) As Single? Implements IConnector.ReadSingle

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetFloat(index)
        End Function

        Public Overridable Function ReadDouble(reader As IDataReader, column As Column) As Double? Implements IConnector.ReadDouble

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetDouble(index)
        End Function

        Public Overridable Function ReadChar(reader As IDataReader, column As Column) As Char? Implements IConnector.ReadChar

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetString(index).First()
        End Function

        Public Overridable Function ReadByte(reader As IDataReader, column As Column) As Byte? Implements IConnector.ReadByte

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetByte(index)
        End Function

        Public Overridable Function ReadInt16(reader As IDataReader, column As Column) As Int16? Implements IConnector.ReadInt16

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetInt16(index)
        End Function

        Public Overridable Function ReadInt32(reader As IDataReader, column As Column) As Int32? Implements IConnector.ReadInt32

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetInt32(index)
        End Function

        Public Overridable Function ReadInt64(reader As IDataReader, column As Column) As Int64? Implements IConnector.ReadInt64

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetInt64(index)
        End Function

        Public Overridable Function ReadGuid(reader As IDataReader, column As Column) As Guid? Implements IConnector.ReadGuid

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetGuid(index)
        End Function

        Public Overridable Function ReadString(reader As IDataReader, column As Column) As String Implements IConnector.ReadString

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetString(index)
        End Function

        Public Overridable Function ReadBoolean(reader As IDataReader, column As Column) As Boolean? Implements IConnector.ReadBoolean

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetBoolean(index)
        End Function

        Public Overridable Function ReadByteArray(reader As IDataReader, column As Column) As Byte() Implements IConnector.ReadByteArray

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            'TODO: [DBWrangler] test reading of byte arrays
            Return DirectCast(reader.GetValue(index), Byte())
        End Function

        Public Overridable Function ReadVariant(reader As IDataReader, column As Column) As Object Implements IConnector.ReadVariant

            Dim index As Integer = reader.GetOrdinal(column.Name)
            If (reader.IsDBNull(index)) Then Return Nothing

            Return reader.GetValue(index)
        End Function

#End Region

#Region "Execution"

        Public MustOverride Function CommandNew(sql As String) As IDbCommand Implements IConnector.CommandNew

        Public MustOverride ReadOnly Property Connection As IDbConnection Implements IConnector.Connection

        Public Overridable Function ExecuteNonQuery(sql As String) As Integer Implements IConnector.ExecuteNonQuery

            If ((sql Is Nothing) OrElse (sql.Length = 0)) Then Return 0

            Using command = CommandNew(sql)

                command.CommandTimeout = 60 * 15
                Return command.ExecuteNonQuery()
            End Using
        End Function

        Public Overridable Function ExecuteScalar(sql As String) As Object Implements IConnector.ExecuteScalar

            If ((sql Is Nothing) OrElse (sql.Length = 0)) Then Return Nothing

            Using command = CommandNew(sql)

                command.CommandTimeout = 60 * 15
                Return command.ExecuteScalar()
            End Using
        End Function

#End Region

#Region "Providers"

        Public MustOverride ReadOnly Property SqlInfo As SqlInfo Implements IConnector.SqlInfo
        Public MustOverride ReadOnly Property SqlCreate As SqlCreate Implements IConnector.SqlCreate
        Public MustOverride ReadOnly Property SqlCopy As SqlCopy Implements IConnector.SqlCopy
        Public MustOverride ReadOnly Property SqlDelete As SqlDelete Implements IConnector.SqlDelete
        Public MustOverride ReadOnly Property SqlDrop As SqlDrop Implements IConnector.SqlDrop
        Public MustOverride ReadOnly Property SqlExists As SqlExists Implements IConnector.SqlExists
        Public MustOverride ReadOnly Property SqlInsert As SqlInsert Implements IConnector.SqlInsert
        Public MustOverride ReadOnly Property SqlInsertFiltered As SqlInsertFiltered Implements IConnector.SqlInsertFiltered
        Public MustOverride ReadOnly Property SqlJoin As SqlJoin Implements IConnector.SqlJoin
        Public MustOverride ReadOnly Property SqlSelect As SqlSelect Implements IConnector.SqlSelect
        Public MustOverride ReadOnly Property SqlTruncate As SqlTruncate Implements IConnector.SqlTruncate
        Public MustOverride ReadOnly Property SqlUpdate As SqlUpdate Implements IConnector.SqlUpdate

#End Region

    End Class
End Namespace