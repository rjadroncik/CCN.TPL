Imports CCN.UI.WinForms
Imports CCN.Services
Imports CCN.Aktualizacia.Model
Imports System.ComponentModel

Public Class SystemStatusAktualizacie
    Inherits SystemStatus

    Public Sub New()

        InitializeComponent()

        If (Not Service.Initialized) Then Return

        BGTimer.Interval = Nastavenia.BGWorkTimeout * 60 * 1000
        BGTimer.Start()
    End Sub

#Region "BG work"

    Private _aktualizaciaRelease As Release

    Private _exception As Exception

    Private Sub BGTimer_Tick(sender As Object, e As EventArgs) Handles BGTimer.Tick

        If (Not BGWorker.IsBusy) Then BGWorker.RunWorkerAsync()
    End Sub

    Private Sub BGWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGWorker.DoWork

        Try
            LogUdalosti.AplikaciaBezi()

            With New ReleaseProvider()

                _aktualizaciaRelease = .ReleaseFind(True)
            End With

        Catch ex As Exception

            _exception = ex
            LogDB.ZalogujChybu(ex, 1)
        End Try
    End Sub

    Private Sub BGWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGWorker.RunWorkerCompleted

        If (_aktualizaciaRelease IsNot Nothing) Then

            ZobrazUpdateVystrahu("Je dostupná aktualizácia na verziu: " & _aktualizaciaRelease.Version.ToString() & ". Reštartujte prosím systém.")
        End If

        If (_exception IsNot Nothing) Then

            ZobrazUpdateVystrahu(_exception.Message())
            Return
        End If
    End Sub

#End Region

    Private components As System.ComponentModel.IContainer

    Friend WithEvents BGWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGTimer As System.Windows.Forms.Timer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.BGWorker = New System.ComponentModel.BackgroundWorker()
        Me.BGTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'BGWorker
        '
        '
        'BGTimer
        '
        '
        'SystemStatusAktualizacie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Name = "SystemStatusAktualizacie"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
