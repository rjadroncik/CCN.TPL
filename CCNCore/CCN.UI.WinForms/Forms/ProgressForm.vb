Imports System.ComponentModel
Imports CCN.Services

Public Class ProgressForm

#Region "Events"

    Public Event RunCode As Action(Of Boolean)

#End Region

#Region "Properties"

    Public Exception As Exception

    Public Property FullRebuild As Boolean
#End Region

#Region "Initialization"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Icon = Icons.Application
    End Sub

    Private Sub StartupCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BackgroundWorker.RunWorkerAsync()

        AddHandler LogSubor.LineWritten, AddressOf Report.GridAddMessage
    End Sub

#End Region

#Region "BL"

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker.DoWork

        Try
            RaiseEvent RunCode(FullRebuild)

        Catch ex As Exception

            LogSubor.WriteException(ex)
            e.Result = ex
            Return
        End Try

    End Sub

#End Region

#Region "Event handling"

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted

        If (e.Result IsNot Nothing) Then

            Exception = DirectCast(e.Result, Exception)

            Report.GridAddMessage("Error: " & Exception.Message)
            Report.GridAddMessage("Rebuild process failed, execution cannot continue!")
        Else
            Report.GridAddMessage("Close this window to continue...")
        End If
    End Sub

    Private Sub StartupCheckForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        If (Exception IsNot Nothing) Then

            DialogResult = Windows.Forms.DialogResult.Abort
        Else
            DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub StartupCheckForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If (BackgroundWorker.IsBusy) Then e.Cancel = True
    End Sub

#End Region

End Class