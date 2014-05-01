Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports System.ComponentModel
Imports CCN.Model
Imports CCN.Services
Imports CCN.UI.WinForms

Public Class RolaForm

#Region "Initialization"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Sub RolaForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AktualizujGui()
    End Sub

#End Region

#Region "Properties"

    Public Property Session As ISession

    Public Property Rola As IRola

#End Region

#Region "Event handling"

    Private Sub UlozButton_Click(sender As Object, e As EventArgs) Handles UlozButton.Click

        UlozZmeny()
        _closed = True
        DialogResult = Windows.Forms.DialogResult.None
        Me.Close()
    End Sub

    Private Sub ZrusButton_Click(sender As Object, e As EventArgs) Handles ZrusButton.Click

        TryClose()
    End Sub

    Private Sub RolaForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = Not TryClose()
    End Sub

#End Region

#Region "BL"

    Protected Overridable Sub Aktualizuj()

        If (Rola Is Nothing) Then Rola = Servisy.Role.Create()
        With Rola

            .Nazov = Nazov.Text
        End With
    End Sub

    Protected Overridable Sub AktualizujGui()

        If (Rola IsNot Nothing) Then
            With Rola
                Me.Text = .Nazov

                Nazov.Text = .Nazov
            End With
        End If
    End Sub

    Protected Overridable Sub UlozZmeny()

        Aktualizuj()
        Servisy.Role.UlozZmeny(Rola, Session)
    End Sub

    Private _closed As Boolean = False
    Private Function TryClose() As Boolean

        If ((Not _closed) AndAlso Upozornenia.OtazkaZatvorenieOkna()) Then
            _closed = True
            DialogResult = Windows.Forms.DialogResult.None
            Me.Close()
            Return True
        End If

        Return _closed
    End Function

#End Region

End Class