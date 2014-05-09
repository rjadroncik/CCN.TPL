Imports System.Data.Common
Imports System.Data.SqlClient
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Services.SqlProviders.Microsoft
Imports DBWrangler.Enums

Namespace Connectors

    Public Class MsConnector
        Inherits DefaultConnector

#Region "Initialization"

        Public Sub New(connection As SqlConnection)

            _connection = connection

            _sqlInfo = New MsInfo(Me)
            _sqlCreate = New MsCreate(Me)
            _sqlCopy = New MsCopy(Me)
            _sqlDelete = New MsDelete(Me)
            _sqlDrop = New MsDrop(Me)
            _sqlExists = New MsExists(Me)
            _sqlInsert = New MsInsert(Me)
            _sqlInsertFiltered = New MsInsertFiltered(Me)
            _sqlJoin = New MsJoin(Me)
            _sqlSelect = New MsSelect(Me)
            _sqlTruncate = New MsTruncate(Me)
            _sqlUpdate = New MsUpdate(Me)
        End Sub

#End Region

#Region "Properties"

        Public Overrides ReadOnly Property Vendor() As DatabaseVendor
            Get
                Return DatabaseVendor.Microsoft
            End Get
        End Property

        Private ReadOnly _connection As SqlConnection
        Public Overrides ReadOnly Property Connection As IDbConnection
            Get
                Return _connection
            End Get
        End Property

#End Region

#Region "Execution"

        Public Overrides Function CommandNew(sql As String) As IDbCommand

            Return New SqlCommand(sql, _connection)
        End Function

#End Region

#Region "Providers"

        Private ReadOnly _sqlInfo As MsInfo
        Public Overrides ReadOnly Property SqlInfo As SqlInfo
            Get
                Return _sqlInfo
            End Get
        End Property

        Private ReadOnly _sqlCreate As MsCreate
        Public Overrides ReadOnly Property SqlCreate As SqlCreate
            Get
                Return _sqlCreate
            End Get
        End Property

        Private ReadOnly _sqlCopy As MsCopy
        Public Overrides ReadOnly Property SqlCopy As SqlCopy
            Get
                Return _sqlCopy
            End Get
        End Property

        Private ReadOnly _sqlDelete As MsDelete
        Public Overrides ReadOnly Property SqlDelete As SqlDelete
            Get
                Return _sqlDelete
            End Get
        End Property

        Private ReadOnly _sqlDrop As MsDrop
        Public Overrides ReadOnly Property SqlDrop As SqlDrop
            Get
                Return _sqlDrop
            End Get
        End Property

        Private ReadOnly _sqlExists As MsExists
        Public Overrides ReadOnly Property SqlExists As SqlExists
            Get
                Return _sqlExists
            End Get
        End Property

        Private ReadOnly _sqlInsert As MsInsert
        Public Overrides ReadOnly Property SqlInsert As SqlInsert
            Get
                Return _sqlInsert
            End Get
        End Property

        Private ReadOnly _sqlInsertFiltered As MsInsertFiltered
        Public Overrides ReadOnly Property SqlInsertFiltered As SqlInsertFiltered
            Get
                Return _sqlInsertFiltered
            End Get
        End Property

        Private ReadOnly _sqlJoin As MsJoin
        Public Overrides ReadOnly Property SqlJoin As SqlJoin
            Get
                Return _sqlJoin
            End Get
        End Property

        Private ReadOnly _sqlSelect As MsSelect
        Public Overrides ReadOnly Property SqlSelect As SqlSelect
            Get
                Return _sqlSelect
            End Get
        End Property

        Private ReadOnly _sqlTruncate As MsTruncate
        Public Overrides ReadOnly Property SqlTruncate As SqlTruncate
            Get
                Return _sqlTruncate
            End Get
        End Property

        Private ReadOnly _sqlUpdate As MsUpdate
        Public Overrides ReadOnly Property SqlUpdate As SqlUpdate
            Get
                Return _sqlUpdate
            End Get
        End Property

#End Region

    End Class
End Namespace