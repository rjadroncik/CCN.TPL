Public Class WorkerRunner1(Of T)
    Implements IWorkerRunner

#Region "Properties"

    Public Property Worker As Worker1(Of T)
    Public Property Argument As T

#End Region

#Region "Initialization"

    Public Sub New()
    End Sub

    Public Sub New(worker As Worker1(Of T), argument As T)
        Me.Worker = worker
        Me.Argument = argument
    End Sub

#End Region

#Region "IWorkerRunner"

    Public Sub Execute() Implements IWorkerRunner.Execute

        Worker()(Argument)
    End Sub

#End Region

End Class
