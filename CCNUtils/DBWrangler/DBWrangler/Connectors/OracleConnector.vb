Imports System.Data.Common
Imports CCN.Core.VB
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Datatypes
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Services.SqlProviders.Oracle
Imports DBWrangler.Enums
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Data.SqlTypes

Namespace Connectors

    Public Class OracleConnector
        Inherits DefaultConnector

#Region "Initialization"

        Public Sub New(connection As OracleConnection)

            _connection = connection

            _sqlInfo = New OracleInfo(Me)
            _sqlCreate = New OracleCreate(Me)
            _sqlCopy = New OracleCopy(Me)
            _sqlDelete = New OracleDelete(Me)
            _sqlDrop = New OracleDrop(Me)
            _sqlExists = New OracleExists(Me)
            _sqlInsert = New OracleInsert(Me)
            _sqlInsertFiltered = New OracleInsertFiltered(Me)
            _sqlJoin = New OracleJoin(Me)
            _sqlSelect = New OracleSelect(Me)
            _sqlTruncate = New OracleTruncate(Me)
            _sqlUpdate = New OracleUpdate(Me)
        End Sub

#End Region

#Region "Properties"

        Public Overrides ReadOnly Property Vendor() As DatabaseVendor
            Get
                Return DatabaseVendor.Oracle
            End Get
        End Property

        Private ReadOnly _connection As OracleConnection
        Public Overrides ReadOnly Property Connection As IDbConnection
            Get
                Return _connection
            End Get
        End Property

#End Region

#Region "Values"

        Public Overrides Function ToSql(hodnota As Date?, precision As Integer?) As String

            If (Not hodnota.HasValue) Then Return "NULL"

            Return "to_timestamp('" & If(hodnota.Value < SqlDateTime.MinValue.Value, _
                            SqlDateTime.MinValue.Value, hodnota.Value).ToString("yyyy-MM-dd HH:mm:ss" & DateTimePrecisionString(precision)) & "', 'YYYY-MM-DD HH24.MI.SSXFF')"
        End Function

#End Region

#Region "Datatypes"

        Public Overrides Function ToSql(typ As DTDate) As String

            Return If(typ.LowPrecision, "SMALLDATETIME", "DATETIME")
        End Function

        Public Overrides Function ToSql(typ As DTDecimal) As String

            With typ
                If (.Money) Then Return "MONEY"

                If (Not .Precision.HasValue) Then

                    Return "DECIMAL" & If(Not .Scale.HasValue, "", "(0, " & .Scale & ")")
                Else
                    Return "DECIMAL(" & .Precision & If(Not .Scale.HasValue, ")", ", " & .Scale & ")")
                End If
            End With
        End Function

        Public Overrides Function ToSql(typ As DTSingle) As String

            Return "REAL"
        End Function

        Public Overrides Function ToSql(typ As DTDouble) As String

            Return "FLOAT"
        End Function

        Public Overrides Function ToSql(typ As DTChar) As String

            Return If(typ.Unicode, "N", "") & "CHAR(1)"
        End Function

        Public Overrides Function ToSql(typ As DTByte) As String

            Return "TINYINT"
        End Function

        Public Overrides Function ToSql(typ As DTInt16) As String

            Return "SMALLINT"
        End Function

        Public Overrides Function ToSql(typ As DTInt32) As String

            Return "INT"
        End Function

        Public Overrides Function ToSql(typ As DTInt64) As String

            Return "BIGINT"
        End Function

        Public Overrides Function ToSql(typ As DTGuid) As String

            Return "UNIQUEIDENTIFIER"
        End Function

        Public Overrides Function ToSql(typ As DTString) As String

            With typ
                Return If(.SizeFixed, "", "VAR") & "CHAR(" & If(.Size = -1, "32767", .Size.ToStringInvariant()) & ")"
            End With
        End Function

        Public Overrides Function ToSql(typ As DTBoolean) As String

            Return "BIT"
        End Function

        Public Overrides Function ToSql(typ As DTByteArray) As String

            Return "VARBINARY(MAX)"
        End Function

        Public Overrides Function ToSql(typ As DTVariant) As String

            Return "SQL_VARIANT"
        End Function

        Public Overrides Function ToSql(typ As DTTimestamp) As String

            Return "TIMESTAMP" & If(typ.Precision.HasValue, "(" & typ.Precision.Value & ")", "")
        End Function

#End Region

#Region "Read"

        Public Overrides Function ReadDouble(reader As IDataReader, column As Column) As Double?

            Dim index As Integer = reader.GetOrdinal(column.Name)

            Return OracleDecimal.Truncate(DirectCast(reader, OracleDataReader).GetOracleDecimal(index), 10).Value()
        End Function

#End Region

#Region "Execution"

        Public Overrides Function CommandNew(sql As String) As IDbCommand

            Return New OracleCommand(sql, _connection)
        End Function

#End Region

#Region "Providers"

        Private ReadOnly _sqlInfo As OracleInfo
        Public Overrides ReadOnly Property SqlInfo As SqlInfo
            Get
                Return _sqlInfo
            End Get
        End Property

        Private ReadOnly _sqlCreate As OracleCreate
        Public Overrides ReadOnly Property SqlCreate As SqlCreate
            Get
                Return _sqlCreate
            End Get
        End Property

        Private ReadOnly _sqlCopy As OracleCopy
        Public Overrides ReadOnly Property SqlCopy As SqlCopy
            Get
                Return _sqlCopy
            End Get
        End Property

        Private ReadOnly _sqlDelete As OracleDelete
        Public Overrides ReadOnly Property SqlDelete As SqlDelete
            Get
                Return _sqlDelete
            End Get
        End Property

        Private ReadOnly _sqlDrop As OracleDrop
        Public Overrides ReadOnly Property SqlDrop As SqlDrop
            Get
                Return _sqlDrop
            End Get
        End Property

        Private ReadOnly _sqlExists As OracleExists
        Public Overrides ReadOnly Property SqlExists As SqlExists
            Get
                Return _sqlExists
            End Get
        End Property

        Private ReadOnly _sqlInsert As OracleInsert
        Public Overrides ReadOnly Property SqlInsert As SqlInsert
            Get
                Return _sqlInsert
            End Get
        End Property

        Private ReadOnly _sqlInsertFiltered As OracleInsertFiltered
        Public Overrides ReadOnly Property SqlInsertFiltered As SqlInsertFiltered
            Get
                Return _sqlInsertFiltered
            End Get
        End Property

        Private ReadOnly _sqlJoin As OracleJoin
        Public Overrides ReadOnly Property SqlJoin As SqlJoin
            Get
                Return _sqlJoin
            End Get
        End Property

        Private ReadOnly _sqlSelect As OracleSelect
        Public Overrides ReadOnly Property SqlSelect As SqlSelect
            Get
                Return _sqlSelect
            End Get
        End Property

        Private ReadOnly _sqlTruncate As OracleTruncate
        Public Overrides ReadOnly Property SqlTruncate As SqlTruncate
            Get
                Return _sqlTruncate
            End Get
        End Property

        Private ReadOnly _sqlUpdate As OracleUpdate
        Public Overrides ReadOnly Property SqlUpdate As SqlUpdate
            Get
                Return _sqlUpdate
            End Get
        End Property

#End Region

#Region "Utils"

        Public Shared Function BuildConnectionString(server As String, database As String, user As String, password As String) As String

            Return "Data Source=" & server & If(database Is Nothing, "", ";Initial Catalog=" & database) & _
                   ";Persist Security Info=False;User ID=" & user & ";Password=" & password
        End Function

#End Region

    End Class
End Namespace