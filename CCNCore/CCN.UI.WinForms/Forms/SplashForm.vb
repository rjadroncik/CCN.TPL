Imports CCN.Model
Imports CCN.Services
Imports CCN.UI.WinForms
Imports System.Threading
Imports CCN.Core.VB

Public Class SplashForm

#Region "Events"

    Public Shared ReadyEvent As New AutoResetEvent(False)

#End Region

#Region "Initialization"

    Private Sub MainSplash_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        UpdateVerzia()
    End Sub

    Public Sub New(message As String)

        ' This call is required by the designer.
        InitializeComponent()

        Show()
        Akcia.Text = message
        Copyright.Text = "© " & Date.Now.Year & " CCN s.r.o."
        Hide()

        ReadyEvent.Set()
    End Sub

#End Region

#Region "BL"

    Protected Sub UpdateVerzia()

        If (Globals.Verzia(Globals.AplikaciaBeziaca) IsNot Nothing) Then Verzia.Text = "Verzia: " & Globals.Verzia(Globals.AplikaciaBeziaca).ToString()
    End Sub

    Public Sub OnMessage(message As String)

        If (Me.IsDisposed) Then Return

        If (Me.InvokeRequired) Then

            Me.Invoke(New SubMessage(AddressOf OnMessage), New Object() {message})
        Else
            Akcia.Text = message
        End If
    End Sub

    Public Sub OnFinished()

        If (Me.IsDisposed) Then Return

        If (Me.InvokeRequired) Then

            Me.Invoke(New Worker(AddressOf OnFinished))
        Else
            Me.Hide()
            Me.Close()
            Me.Dispose()
        End If
    End Sub

#End Region

End Class
