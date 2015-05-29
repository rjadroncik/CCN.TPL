Imports DBWrangler.Connectors
Imports DBWrangler.Services.SqlProviders.Common
Imports DBWrangler.Model.Schema
Imports System.Text

Namespace Services.SqlProviders.Oracle

    Public Class OracleInsert
        Inherits SqlInsert

        Public Sub New(connector As IConnector)
            MyBase.New(connector)

            _scriptStart = "BEGIN" + Environment.NewLine
            _scriptEnd = Environment.NewLine + "END;"
        End Sub

        Public Overrides Function InsertTable(table As Table, skipIdentities As Boolean, reader As IDataReader, limit As Integer?, context As QueryContext) As String

            Dim sql As New StringBuilder()

            Dim count = 0

            While ((Not limit.HasValue) OrElse (count < limit))

                If (Not reader.Read()) Then reader.Close() : Exit While

                If (count > 0) Then sql.Append("," & Environment.NewLine)

                sql.Append(InsertBegin(table, skipIdentities))
                sql.Append(RowValues(table, skipIdentities, reader, context))

                count += 1
            End While

            If (count > 0) Then

                sql.Append(";" & Environment.NewLine)
                Return sql.ToString()
            End If

            Return String.Empty
        End Function

    End Class
End Namespace