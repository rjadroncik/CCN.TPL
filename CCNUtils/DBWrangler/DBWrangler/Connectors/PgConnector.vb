Imports System.Data.Common
Imports CCN.Core.VB
Imports DBWrangler.Model.Schema.Datatypes
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Enums
Imports Npgsql
Imports DBWrangler.Services.SqlProviders.Postgre

Namespace Connectors

    Public Class PgConnector
        Inherits DefaultConnector

#Region "Initialization"

        Public Sub New(connection As NpgsqlConnection)

            _connection = connection

            _sqlInfo = New PgInfo(Me)
            _sqlCreate = New PgCreate(Me)
            _sqlCopy = New PgCopy(Me)
            _sqlDelete = New PgDelete(Me)
            _sqlDrop = New PgDrop(Me)
            _sqlExists = New PgExists(Me)
            _sqlInsert = New PgInsert(Me)
            _sqlInsertFiltered = New PgInsertFiltered(Me)
            _sqlJoin = New PgJoin(Me)
            _sqlSelect = New PgSelect(Me)
            _sqlTruncate = New PgTruncate(Me)
            _sqlUpdate = New PgUpdate(Me)
        End Sub

#End Region

#Region "Properties"

        Public Overrides ReadOnly Property Vendor() As DatabaseVendor
            Get
                Return DatabaseVendor.Postgre
            End Get
        End Property

        Private ReadOnly _connection As NpgsqlConnection
        Public Overrides ReadOnly Property Connection As IDbConnection
            Get
                Return _connection
            End Get
        End Property

#End Region

#Region "Execution"

        Public Overrides Function CommandNew(sql As String) As IDbCommand

            Return New NpgsqlCommand(sql, _connection)
        End Function

#End Region

#Region "Datatypes"

        Public Overrides Function ToSql(typ As DtInt32) As String

            Return "INT"
        End Function

        Public Overrides Function ToSql(typ As DTChar) As String

            Return "CHAR(1)"
        End Function

        Public Overrides Function ToSql(typ As DtString) As String

            With typ
                If (.Size > If(.Unicode, 4000, 8000)) Then Return "TEXT"

                Return If(.SizeFixed, "", "VAR") & "CHAR(" & If(.Size = -1, "MAX", .Size.ToStringInvariant()) & ")"
            End With
        End Function

#End Region

#Region "Providers"

        Private ReadOnly _sqlInfo As PgInfo
        Public Overrides ReadOnly Property SqlInfo As SqlInfo
            Get
                Return _sqlInfo
            End Get
        End Property

        Private ReadOnly _sqlCreate As PgCreate
        Public Overrides ReadOnly Property SqlCreate As SqlCreate
            Get
                Return _sqlCreate
            End Get
        End Property

        Private ReadOnly _sqlCopy As PgCopy
        Public Overrides ReadOnly Property SqlCopy As SqlCopy
            Get
                Return _sqlCopy
            End Get
        End Property

        Private ReadOnly _sqlDelete As PgDelete
        Public Overrides ReadOnly Property SqlDelete As SqlDelete
            Get
                Return _sqlDelete
            End Get
        End Property

        Private ReadOnly _sqlDrop As PgDrop
        Public Overrides ReadOnly Property SqlDrop As SqlDrop
            Get
                Return _sqlDrop
            End Get
        End Property

        Private ReadOnly _sqlExists As PgExists
        Public Overrides ReadOnly Property SqlExists As SqlExists
            Get
                Return _sqlExists
            End Get
        End Property

        Private ReadOnly _sqlInsert As PgInsert
        Public Overrides ReadOnly Property SqlInsert As SqlInsert
            Get
                Return _sqlInsert
            End Get
        End Property

        Private ReadOnly _sqlInsertFiltered As PgInsertFiltered
        Public Overrides ReadOnly Property SqlInsertFiltered As SqlInsertFiltered
            Get
                Return _sqlInsertFiltered
            End Get
        End Property

        Private ReadOnly _sqlJoin As PgJoin
        Public Overrides ReadOnly Property SqlJoin As SqlJoin
            Get
                Return _sqlJoin
            End Get
        End Property

        Private ReadOnly _sqlSelect As PgSelect
        Public Overrides ReadOnly Property SqlSelect As SqlSelect
            Get
                Return _sqlSelect
            End Get
        End Property

        Private ReadOnly _sqlTruncate As PgTruncate
        Public Overrides ReadOnly Property SqlTruncate As SqlTruncate
            Get
                Return _sqlTruncate
            End Get
        End Property

        Private ReadOnly _sqlUpdate As PgUpdate
        Public Overrides ReadOnly Property SqlUpdate As SqlUpdate
            Get
                Return _sqlUpdate
            End Get
        End Property

#End Region

#Region "Utils"

        Public Shared Function BuildConnectionString(server As String, database As String, user As String, password As String, _
                                                     Optional port As Integer? = 5432, Optional schema As String = Nothing) As String

            Return "Server=" & server & If(port Is Nothing, "", ";Port=" & port.Value) &
                   If(database Is Nothing, "", ";Database=" & database) & _
                   ";User ID=" & user & ";Password=" & password & If(schema Is Nothing, "", ";SearchPath=" & schema)
        End Function

#End Region

    End Class
End Namespace