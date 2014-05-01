Imports NHibernate
Imports CCN.Model
Imports System.Data.SqlClient
Imports CCN.Core.VB

Public Delegate Sub ExecutorBatch0(Of T)(values As IEnumerable(Of T))
Public Delegate Sub ExecutorBatch1(Of T, T1)(values As IEnumerable(Of T), param1 As T1)
Public Delegate Sub ExecutorBatch2(Of T, T1, T2)(values As IEnumerable(Of T), param1 As T1, param2 As T2)
Public Delegate Sub ExecutorBatch3(Of T, T1, T2, T3)(values As IEnumerable(Of T), param1 As T1, param2 As T2, param3 As T3)
Public Delegate Sub ExecutorBatch4(Of T, T1, T2, T3, T4)(values As IEnumerable(Of T), param1 As T1, param2 As T2, param3 As T3, param4 As T4)

Public MustInherit Class Service

#Region "Constants"

    Protected Const NHTimeout As Integer = 180

#End Region

#Region "NHibernate"

    Private Shared _initialized As Boolean
    Public Shared ReadOnly Property Initialized As Boolean
        Get
            Return _initialized
        End Get
    End Property

    Private Shared _dbProvider As IDatabaseProvider
    Public Shared ReadOnly Property DBProvider As IDatabaseProvider
        Get
            Return _dbProvider
        End Get
    End Property

    Public Shared Sub Initialize(database As IDatabaseProvider)

        If (_initialized) Then Throw New InvalidOperationException("Duplicate service initialization!")

        _dbProvider = database
        _initialized = True
    End Sub

    Public Shared Function NewSession() As ISession

        If (Not _initialized) Then Throw New InvalidOperationException("Service not initialized!")

        Return _dbProvider.NewSession()
    End Function

    Public Shared Function NewSession(connection As IDbConnection) As ISession

        If (Not _initialized) Then Throw New InvalidOperationException("Service not initialized!")

        Return _dbProvider.NewSession(connection)
    End Function

    Public Shared Sub Close()

        If (Not _initialized) Then Throw New InvalidOperationException("Invalid service reset!")
        _initialized = False

        _dbProvider.Close()
    End Sub

#End Region

#Region "Database time"

    Protected Shared _lastTimeLocal As DateTime
    Protected Shared _lastTimeDB As DateTime

    Public Shared ReadOnly Property Now As DateTime
        Get
            Dim nowLocal As DateTime = DateTime.Now

            If (Not Initialized) Then Return nowLocal

            If ((nowLocal - _lastTimeLocal).TotalSeconds > 10) Then

                Using session As ISession = NewSession()

                    _lastTimeLocal = nowLocal
                    _lastTimeDB = _dbProvider.GetDate(session)
                End Using
            End If

            Return _lastTimeDB
        End Get
    End Property

#End Region

#Region "Batching"

    Protected Shared BatchSize As Integer = 100

    Private Shared Sub ExecuteBatch0(Of T)(executor As [Delegate], values As IEnumerable(Of T), params As Object())

        DirectCast(executor, ExecutorBatch0(Of T))(values)
    End Sub

    Private Shared Sub ExecuteBatch1(Of T, T1)(executor As [Delegate], values As IEnumerable(Of T), params As Object())

        DirectCast(executor, ExecutorBatch1(Of T, T1))(values, DirectCast(params(0), T1))
    End Sub

    Private Shared Sub ExecuteBatch2(Of T, T1, T2)(executor As [Delegate], values As IEnumerable(Of T), params As Object())

        DirectCast(executor, ExecutorBatch2(Of T, T1, T2))(values, DirectCast(params(0), T1), _
                                                                   DirectCast(params(1), T2))
    End Sub

    Private Shared Sub ExecuteBatch3(Of T, T1, T2, T3)(executor As [Delegate], values As IEnumerable(Of T), params As Object())

        DirectCast(executor, ExecutorBatch3(Of T, T1, T2, T3))(values, DirectCast(params(0), T1), _
                                                                       DirectCast(params(1), T2), _
                                                                       DirectCast(params(2), T3))
    End Sub

    Private Shared Sub ExecuteBatch4(Of T, T1, T2, T3, T4)(executor As [Delegate], values As IEnumerable(Of T), params As Object())

        DirectCast(executor, ExecutorBatch4(Of T, T1, T2, T3, T4))(values, DirectCast(params(0), T1), _
                                                                           DirectCast(params(1), T2), _
                                                                           DirectCast(params(2), T3), _
                                                                           DirectCast(params(3), T4))
    End Sub

    Private Delegate Sub ExecutorExecutor(Of T)(executor As [Delegate], values As IEnumerable(Of T), params As Object())

    Private Shared Sub ExecuteInBatchesInternal(Of T)(executorExecutor As ExecutorExecutor(Of T), _
                                                      executorBatch As [Delegate], _
                                                      values As IEnumerable(Of T), params As Object())
        Dim batch As New List(Of T)(BatchSize)

        For Each value As T In values

            batch.Add(value)

            If (batch.Count = BatchSize) Then

                executorExecutor(executorBatch, batch, params)
                batch.Clear()
            End If
        Next

        If (Not batch.IsEmpty()) Then executorExecutor(executorBatch, batch, params)
    End Sub

#End Region

#Region "Batching - public"

    Public Shared Sub ExecuteInBatches(Of T)(values As IEnumerable(Of T), executor As ExecutorBatch0(Of T))

        ExecuteInBatchesInternal(AddressOf ExecuteBatch0(Of T), executor, values, Nothing)
    End Sub

    Public Shared Sub ExecuteInBatches(Of T, T1)(values As IEnumerable(Of T), executor As ExecutorBatch1(Of T, T1), param1 As T1)

        ExecuteInBatchesInternal(AddressOf ExecuteBatch1(Of T, T1), executor, values, New Object() {param1})
    End Sub

    Public Shared Sub ExecuteInBatches(Of T, T1, T2)(values As IEnumerable(Of T), executor As ExecutorBatch2(Of T, T1, T2), param1 As T1, param2 As T2)

        ExecuteInBatchesInternal(AddressOf ExecuteBatch2(Of T, T1, T2), executor, values, New Object() {param1, param2})
    End Sub

    Public Shared Sub ExecuteInBatches(Of T, T1, T2, T3)(values As IEnumerable(Of T), executor As ExecutorBatch3(Of T, T1, T2, T3), param1 As T1, param2 As T2, param3 As T3)

        ExecuteInBatchesInternal(AddressOf ExecuteBatch3(Of T, T1, T2, T3), executor, values, New Object() {param1, param2, param3})
    End Sub

    Public Shared Sub ExecuteInBatches(Of T, T1, T2, T3, T4)(values As IEnumerable(Of T), executor As ExecutorBatch4(Of T, T1, T2, T3, T4), param1 As T1, param2 As T2, param3 As T3, param4 As T4)

        ExecuteInBatchesInternal(AddressOf ExecuteBatch4(Of T, T1, T2, T3, T4), executor, values, New Object() {param1, param2, param3, param4})
    End Sub

#End Region

End Class
