Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports System.ComponentModel
Imports CCN.Model
Imports CCN.Services
Imports CCN.UI.WinForms

Public Class PravoObjektoveForm

#Region "Initialization"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Sub PravoObjektoveForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CinnostiVyber.Vsetky = Servisy.PravaObjektove.Cinnosti(Session)
        TypObjektu.Items.AddRange(Servisy.PravaObjektove.TypyObjektov(Session).ToArray())
        TypObjektu.SelectedIndex = 0

        AktualizujGui()
    End Sub

#End Region

#Region "Properties"

    Public Property Session As ISession

    Public Property Pravo As IPravoObjektove

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

    Private Sub PravoObjektoveForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = Not TryClose()
    End Sub

#End Region

#Region "BL"

    Protected Overridable Sub Aktualizuj()

        If (Pravo Is Nothing) Then Pravo = Servisy.PravaObjektove.Create()
        With Pravo

            .Nazov = Nazov.Text
            .Povolene = CinnostiVyber.Zvolene.ToList()
            .ObjektTyp = DirectCast(TypObjektu.SelectedItem, ObjektTyp)
        End With
    End Sub

    Protected Overridable Sub AktualizujGui()

        If (Pravo IsNot Nothing) Then
            With Pravo
                Me.Text = .Nazov

                Nazov.Text = .Nazov
                CinnostiVyber.Zvolene = .Povolene
                TypObjektu.SelectedItem = .ObjektTyp
            End With
        End If

        CinnostiVyber.UpdateGrids()
    End Sub

    Protected Overridable Sub UlozZmeny()

        Aktualizuj()
        Servisy.PravaObjektove.UlozZmeny(Pravo, Session)
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