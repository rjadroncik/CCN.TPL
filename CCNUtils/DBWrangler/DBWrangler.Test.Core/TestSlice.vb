Imports CCN.Core.VB
Imports Oracle.DataAccess.Client
Imports Npgsql
Imports System.Text

<TestClass()>
Public Class TestSlice

    <TestMethod()>
    Public Sub TestSlicePgSql()

        Dim sourceConnection = New OracleConnection(OracleConnector.BuildConnectionString("IDATLAS", Nothing, "accountisolation", "sporting"))
        sourceConnection.Open()

        Dim tables = New String() {"AI_ACCOUNT", "AI_BET", "AI_BET_HISTORY", "AI_BET_JOURNAL"} '{"AI_BET"} '{"AI_PERMISSION", "AI_ROLE", "AI_ROLE_PERMISSION", "AI_SECURITY_LOG"}

        Dim schema As Schema = OracleExport.Execute(tables, "ACCOUNTISOLATION", New ProgressReporter(), New OracleConnector(sourceConnection))

        Dim targetConnection = New NpgsqlConnection(PgConnector.BuildConnectionString("localhost", "SPIN.Security", "postgres", "postgres"))
        targetConnection.Open()

        Dim sourceConnector As New OracleConnector(sourceConnection)
        Dim targetConnector As New PgConnector(targetConnection)

        'Dim slice As New Slice()

        'slice.Source = schema
        'slice.Queries.Add(New QuerySimple() With {.Source = schema.TableNamed("AI_BET"), .Limit = 10})
        'slice.Queries.Add(New QueryJoinCustom() With {.Source = schema.TableNamed("AI_BET_HISTORY"), .Target = schema.TableNamed("AI_BET"),   })

        'Dim sql = slice.Sql(sourceConnector, targetConnector, schema, "", Nothing)

        'slice.Execute(sourceConnector, targetConnector, schema, "", Nothing)

        Dim result As New StringBuilder()

        Dim aiAccount = schema.TableNamed("AI_ACCOUNT")
        Dim aiBet = schema.TableNamed("AI_BET")
        Dim aiBetHistory = schema.TableNamed("AI_BET_HISTORY")
        Dim aiBetJournal = schema.TableNamed("AI_BET_JOURNAL")

        Const whereClause As String = "WHERE ACCOUNT_ID IN ('2696314', '2707516', '2713630')"

        'Const aiAccountWhere As String = "WHERE ACCOUNT_ID IN ('2696314', '2707516', '2713630')"

        Dim context As New QueryContext()

        targetConnector.SqlInsert.Sql(aiAccount, False, sourceConnector.SqlSelect.Execute(aiAccount, New Condition() With {.Expression = whereClause}), result, context)
        
        Dim aiBetIds As New List(Of IEnumerable(Of Object))()

        'Const aiBetWhere As String = "WHERE ACCOUNT_ID IN ('2696314', '2707516', '2713630')"
        
        targetConnector.SqlInsert.Sql(aiBet, False, sourceConnector.SqlSelect.Execute(aiBet, New Condition() With {.Expression = whereClause}), result, context)

        'Dim aiBetHistoryWhere = "WHERE BET_GUID IN (" & Converting.Values2String(aiBetIds.Select(Function(x) x.First()), Function(x) "'" & x & "'") & ")"

        targetConnector.SqlInsert.Sql(aiBetHistory, False, sourceConnector.SqlSelect.Execute(aiBetHistory, New Condition() With {.Expression = whereClause}), result, context)

        'Dim aiBetJournalWhere = "WHERE BET_GUID IN (" & Converting.Values2String(aiBetIds.Select(Function(x) x.First()), Function(x) "'" & x & "'") & ")"

        targetConnector.SqlInsert.Sql(aiBetJournal, False, sourceConnector.SqlSelect.Execute(aiBetJournal, New Condition() With {.Expression = whereClause}), result, context)

        Dim sql = result.ToString()
    End Sub

    <TestMethod()>
    Public Sub TestSlicePgSqlJoin()

        Dim sourceConnection = New OracleConnection(OracleConnector.BuildConnectionString("IDATLAS", Nothing, "accountisolation", "sporting"))
        sourceConnection.Open()

        Dim tables = New String() {"AI_ACCOUNT", "AI_BET", "AI_BET_HISTORY", "AI_BET_JOURNAL"} '{"AI_BET"} '{"AI_PERMISSION", "AI_ROLE", "AI_ROLE_PERMISSION", "AI_SECURITY_LOG"}

        Dim schema As Schema = OracleExport.Execute(tables, "ACCOUNTISOLATION", New ProgressReporter(), New OracleConnector(sourceConnection))

        Dim targetConnection = New NpgsqlConnection(PgConnector.BuildConnectionString("localhost", "SPIN.Security", "postgres", "postgres"))
        targetConnection.Open()

        Dim sourceConnector As New OracleConnector(sourceConnection)
        Dim targetConnector As New PgConnector(targetConnection)

        targetConnector.SqlDrop.Execute(schema)
        targetConnector.SqlCreate.Execute(schema, True, True, True, True, True)

        Dim slice As New Slice()

        slice.Source = schema

        slice.Queries.Add(New QueryCondition() With {.Source = schema.TableNamed("AI_ACCOUNT"), .Condition = New Condition("WHERE ACCOUNT_ID IN ('2696314', '2707516', '2713630')")})

        Dim joinAccountBet = New QueryJoin() With {.Source = schema.TableNamed("AI_BET"), .Target = schema.TableNamed("AI_ACCOUNT")}
        With joinAccountBet
            .ColumnMap.Add(.Source.ColumnNamed("ACCOUNT_ID"), .Target.ColumnNamed("ACCOUNT_ID"))
        End With

        slice.Queries.Add(joinAccountBet)

        Dim joinAccountBetHistory = New QueryJoin() With {.Source = schema.TableNamed("AI_BET_HISTORY"), .Target = schema.TableNamed("AI_ACCOUNT")}
        With joinAccountBetHistory
            .ColumnMap.Add(.Source.ColumnNamed("ACCOUNT_ID"), .Target.ColumnNamed("ACCOUNT_ID"))
        End With

        slice.Queries.Add(joinAccountBetHistory)

        Dim joinAccountBetJournal = New QueryJoin() With {.Source = schema.TableNamed("AI_BET_JOURNAL"), .Target = schema.TableNamed("AI_ACCOUNT")}
        With joinAccountBetJournal
            .ColumnMap.Add(.Source.ColumnNamed("ACCOUNT_ID"), .Target.ColumnNamed("ACCOUNT_ID"))
        End With

        slice.Queries.Add(joinAccountBetJournal)

        Dim sql = slice.Sql(sourceConnector, targetConnector, Nothing, New QueryContext())

        slice.Execute(sourceConnector, targetConnector, Nothing, New QueryContext())

    End Sub

End Class
