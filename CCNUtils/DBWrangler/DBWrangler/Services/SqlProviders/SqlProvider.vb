
Imports System.Text
Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema

Namespace Services.SqlProviders

    Public MustInherit Class SqlProvider

#Region "Fields"

        Protected _escStart As String = String.Empty
        Protected _escEnd As String = String.Empty

        Protected _scriptStart As String = String.Empty
        Protected _scriptEnd As String = String.Empty

        Protected _commandStart As String = String.Empty
        Protected _commandEnd As String = ";" & Environment.NewLine

#End Region

#Region "Properties"

        Protected _connector As IConnector
        Public ReadOnly Property Connector As IConnector
            Get
                Return _connector
            End Get
        End Property

        Public ReadOnly Property EscStart As String
            Get
                Return _escStart
            End Get
        End Property

        Public ReadOnly Property EscEnd As String
            Get
                Return _escEnd
            End Get
        End Property

        Public ReadOnly Property ScriptStart As String
            Get
                Return _scriptStart
            End Get
        End Property
        Public ReadOnly Property ScriptEnd As String
            Get
                Return _scriptEnd
            End Get
        End Property

        Public ReadOnly Property CommandStart As String
            Get
                Return _commandStart
            End Get
        End Property
        Public ReadOnly Property CommandEnd As String
            Get
                Return _commandEnd
            End Get
        End Property

#End Region

#Region "Initialization"

        Protected Sub New(connector As IConnector)
            _connector = connector
        End Sub

#End Region

#Region "Execution"

        Protected Function ExecuteNonQuery(sql As StringBuilder) As Integer

            If ((sql Is Nothing) OrElse (sql.Length = 0)) Then Return 0

            sql.Insert(0, _scriptStart)
            sql.Append(_scriptEnd)

            Return _connector.ExecuteNonQuery(sql.ToString())
        End Function

        Protected Function ExecuteNonQuery(sql As String) As Integer

            If ((sql Is Nothing) OrElse (sql.Length = 0)) Then Return 0

            Return _connector.ExecuteNonQuery(_scriptStart & sql & _scriptEnd)
        End Function

        Protected Function ExecuteScalar(sql As StringBuilder) As Object

            If ((sql Is Nothing) OrElse (sql.Length = 0)) Then Return Nothing

            sql.Insert(0, _scriptStart)
            sql.Append(_scriptEnd)

            Return _connector.ExecuteScalar(sql.ToString())
        End Function

        Protected Function ExecuteScalar(sql As String) As Object

            If ((sql Is Nothing) OrElse (sql.Length = 0)) Then Return Nothing

            Return _connector.ExecuteScalar(_scriptStart & sql & _scriptEnd)
        End Function

#End Region

#Region "Columns"

        Protected Function ColumnList(columns As IEnumerable(Of Column)) As String

            Return ColumnList(columns, False)
        End Function

        Protected Function ColumnList(columns As IEnumerable(Of Column), skipIdentities As Boolean) As String

            Dim sql As String = ""

            Dim first As Boolean = True
            For Each column As Column In columns

                If (skipIdentities AndAlso column.Identity) Then Continue For

                If (Not first) Then sql &= ", "

                sql &= _escStart & column.Name & _escEnd

                first = False
            Next

            Return sql
        End Function

        Protected Function ColumnList(columns As IEnumerable(Of Column), tableName As String) As String

            Dim sql As String = ""

            Dim first As Boolean = True
            For Each column As Column In columns

                If (Not first) Then sql &= ", "

                sql &= tableName & "." & column.Name

                first = False
            Next

            Return sql
        End Function

        Protected Function ColumnList(columns As IEnumerable(Of Column), tableNames As IDictionary(Of Table, String)) As String

            Dim sql As String = ""

            Dim first As Boolean = True
            For Each column As Column In columns

                If (Not first) Then sql &= ", "

                sql &= tableNames(column.Table) & "." & column.Name

                first = False
            Next

            Return sql
        End Function

#End Region

    End Class
End Namespace