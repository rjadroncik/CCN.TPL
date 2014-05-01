Imports C1.Win.C1FlexGrid
Imports C1.Win.C1Command
Imports NHibernate
Imports NHibernate.Linq
Imports CCN.Model
Imports CCN.Services
Imports CCN.Core.VB

Public Class ZamkyForm

#Region "Initialization"

    Private Sub NastaveniaForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TypFilter.Items.Add(String.Empty)

        If (TypyObjektov IsNot Nothing) Then

            For Each typ In TypyObjektov

                TypFilter.Items.Add(typ.Name)
            Next
        End If

        TypFilter.SelectedIndex = 0
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        With Grid
            With .Styles.Add("Head")
                .BackColor = Color.FromArgb(255, 220, 220, 220)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With
        End With
    End Sub

#End Region

#Region "Properties"

    Public Property ZamkyProvider As IZamky

    Public Property TypyObjektov As IEnumerable(Of System.Type)

#End Region

#Region "Loading"

    Public Sub UpdateGrid()

        With Grid

            Dim selected = Grid.RowSel

            .BeginUpdate()
            .Rows.Count = 1

            ZamkyProvider.OdomkniVyprsane()

            Using session = Service.NewSession()

                Dim zamky = ZamkyProvider.Vsetky(session)

                For Each pouzivatel As IPouzivatel In zamky.Keys

                    ZamkyPridaj(pouzivatel, zamky(pouzivatel))
                Next
            End Using

            .Tree.Show(2)
            FlexGrid.RestoreSelection(Grid, selected)
            .AutoSizeCols(0)
            .EndUpdate()
        End With
    End Sub

    Protected Sub ZamkyPridaj(pouzivatel As IPouzivatel, zamky As IEnumerable(Of LockBase(Of Integer)))

        With Grid.Rows.Add()

            .Item("Created") = pouzivatel.Login
            .Item("Expires") = pouzivatel.Login
            .Item("Typ") = pouzivatel.Login
            .Item("Objekt") = pouzivatel.Login

            .IsNode = True
            .Node.Level = 0
            .AllowMerging = True

            .Style = Grid.Styles("Head")
            .UserData = pouzivatel
        End With

        Dim zaznamy As New List(Of Object())()

        Using session = Service.NewSession()
            For Each zamka In zamky

                Dim id = zamka.Objekt.Id

                Dim objekt = session.Query(Of Objekt).Where(Function(x) x.Id = id).Single()

                Dim typ = NHEntita.EntityType(objekt).Name

                If ((TypFilter.SelectedItem Is String.Empty) OrElse _
                    (TypFilter.SelectedItem.ToString() = typ)) Then

                    zaznamy.Add(New Object() {zamka.Created, _
                                              zamka.Expires, _
                                              typ, _
                                              objekt.ToString(), _
                                              zamka})
                End If
            Next
        End Using

        For Each zaznam In zaznamy.OrderBy(Function(x) x(2)).ThenBy(Function(x) x(3))

            With Grid.Rows.Add()

                .Item("Created") = DirectCast(zaznam(0), DateTime).ToString("yyyy-MM-dd HH:mm:ss")
                .Item("Expires") = DirectCast(zaznam(1), DateTime).ToString("yyyy-MM-dd HH:mm:ss")
                .Item("Typ") = zaznam(2)
                .Item("Objekt") = zaznam(3)

                .IsNode = True
                .Node.Level = 1

                .UserData = zaznam(4)
            End With
        Next
    End Sub

#End Region

#Region "Event handling"

    Private Sub ZamkaZmazButton_Click(sender As Object, e As EventArgs) Handles ZamkaZmazButton.Click

        If (Grid.Row < Grid.Rows.Fixed) Then Return

        Try
            If (Grid.Rows(Grid.Row).Node.Level = 0) Then

                ZamkyProvider.ZmazZamky(DirectCast(Grid.Rows(Grid.Row).UserData, IPouzivatel))

            ElseIf (Grid.Rows(Grid.Row).Node.Level = 1) Then

                ZamkyProvider.ZmazZamku(DirectCast(Grid.Rows(Grid.Row).UserData, LockBase(Of Integer)))
            End If

        Catch ex As Exception

            Upozornenia.Upozornenie(e.ToString(), "Odstránenie zámky neúspešné")
        End Try

        UpdateGrid()
    End Sub

    Private Sub TypFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypFilter.SelectedIndexChanged

        UpdateGrid()
    End Sub

    Private Sub ObnovButton_Click(sender As Object, e As EventArgs) Handles ObnovButton.Click

        UpdateGrid()
    End Sub

    'Private Sub Grid_MouseDown(sender As Object, e As MouseEventArgs) Handles Grid.MouseDown

    '    If (Grid.HitTest().Type = HitTestTypeEnum.FilterIcon) Then

    '        For Each form As Form In Application.OpenForms

    '            If ((form.Name = "FilterEditorForm") AndAlso (form.GetType() Is "C1.Win.C1FlexGrid.FilterEditorForm")) Then

    '                form.Text = "TODO: noznost customizovat filtre, prilis zlozite, len ak ozaj bude potrebne"
    '            End If
    '        Next
    '    End If
    'End Sub

#End Region

End Class