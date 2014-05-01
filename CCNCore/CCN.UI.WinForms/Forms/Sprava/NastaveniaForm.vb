Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services
Imports CCN.Core.VB

Public Class NastaveniaForm

#Region "Initialization"

    Private Sub NastaveniaForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Editor.Skupiny = Session.Query(Of NastavenieSkupina).Where(Function(x) x.Rodic Is Nothing).ToList()
        Editor.UpdateGrid()
    End Sub

#End Region

#Region "Properties"

    Public Property Session As ISession

#End Region


#Region "Event handling"

    Private Sub UlozButton_Click(sender As Object, e As EventArgs) Handles UlozButton.Click

        UlozZmeny()
        Nastavenia.ClearCache()

        _closed = True
        DialogResult = Windows.Forms.DialogResult.None
        Me.Close()
    End Sub

    Private Sub ZrusButton_Click(sender As Object, e As EventArgs) Handles ZrusButton.Click

        TryClose()
    End Sub

    Private Sub NastaveniaForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = Not TryClose()
    End Sub

#End Region

#Region "BL"

    Private Sub UlozZmeny()

        Using transaction As ITransaction = Session.BeginTransaction()

            For Each nastavenie In Editor.Zmenene

                Session.Update(nastavenie)
            Next

            transaction.Commit()
        End Using
    End Sub

    Private _closed As Boolean = False
    Private Function TryClose() As Boolean

        If ((Not _closed) AndAlso ((Editor.Zmenene.IsEmpty()) OrElse Upozornenia.OtazkaZatvorenieOkna())) Then

            _closed = True
            DialogResult = Windows.Forms.DialogResult.None
            Me.Close()
            Return True
        End If

        Return _closed
    End Function

#End Region

End Class