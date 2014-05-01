Imports C1.Win.C1FlexGrid
Imports CCN.Core.VB
Imports CCN.Model
Imports CCN.Services

Public Class NastaveniaEditor

#Region "Properties"

    Public Skupiny As IEnumerable(Of NastavenieSkupina)

    Private _zmenene As New List(Of NastavenieHodnota)
    Public ReadOnly Property Zmenene As IEnumerable(Of NastavenieHodnota)
        Get
            Return _zmenene
        End Get
    End Property

#End Region

#Region "Intitialization"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        With Grid
            With .Styles.Add("Aktivne")
                .BackColor = Color.BlanchedAlmond
                .DataType = GetType(Boolean)
                .ImageAlign = ImageAlignEnum.CenterCenter
            End With
            With .Styles.Add("Data")
                .BackColor = Color.BlanchedAlmond
            End With

            With .Styles.Add("Head0")
                .BackColor = Color.FromArgb(255, 220, 220, 220)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With

            With .Styles.Add("Head1")
                .BackColor = Color.FromArgb(255, 192, 192, 192)
                .Font = New Font(Grid.Font, FontStyle.Bold)
            End With

            For Each typ In Converting.Enum2Values(Of DatovyTyp)()

                With .Styles.Add("Data" & typ, "Data")

                    Select Case typ

                        Case DatovyTyp.Retazec
                            .DataType = GetType(String)

                        Case DatovyTyp.CisloCele
                            .DataType = GetType(Integer)
                            .TextAlign = TextAlignEnum.RightCenter

                        Case DatovyTyp.CisloDesatinne
                            .DataType = GetType(Decimal)
                            .TextAlign = TextAlignEnum.RightCenter

                        Case DatovyTyp.AnoNie
                            .DataType = GetType(Boolean)
                            .ImageAlign = ImageAlignEnum.CenterCenter

                        Case DatovyTyp.DatumCas
                            .DataType = GetType(DateTime)
                            .Editor = New DateSelect()
                    End Select
                End With
            Next
        End With
    End Sub

#End Region

#Region "Loading"

    Public Sub UpdateGrid()

        With Grid

            .BeginUpdate()
            .Rows.Count = 1

            For Each skupina As NastavenieSkupina In Skupiny

                If (skupina.Nazov.Equals(Nastavenia.Pouzivatelske) OrElse Prava.Over(Prava.Nastavenia)) Then

                    SkupinaPridaj(skupina)
                End If
            Next

            .Tree.Show(2)
            .AutoSizeCol(0)
            .AutoSizeCol(2)
            .EndUpdate()
        End With
    End Sub

    Protected Sub SkupinaPridaj(skupina As NastavenieSkupina)

        With Grid.Rows.Add()

            .Item("Nazov") = skupina.Nazov.Replace("/", "")
            .Item("Aktivne") = skupina.Nazov.Replace("/", "")
            .Item("Hodnota") = skupina.Nazov.Replace("/", "")

            .IsNode = True
            .Node.Level = skupina.Uroven
            .AllowMerging = True

            .Style = Grid.Styles("Head" & skupina.Uroven)
            .UserData = skupina
        End With

        For Each nastavenie As NastavenieHodnota In skupina.Hodnoty.OrderBy(Function(x) x.Nastavenie.Nazov)

            With Grid.Rows.Add()

                Grid.SetCellStyle(.Index, Grid.Cols("Hodnota").Index, Grid.Styles("Data" & nastavenie.Nastavenie.DatovyTyp))

                .Item("Nazov") = nastavenie.Nastavenie.Nazov

                If (nastavenie.Nastavenie.Upravitelne AndAlso (Not nastavenie.Entita.Equals(Nastavenia.Globalne))) Then

                    Grid.SetCellStyle(.Index, Grid.Cols("Aktivne").Index, Grid.Styles("Aktivne"))
                    .Item("Aktivne") = nastavenie.Aktivna
                End If

                If (Not nastavenie.Hodnota.IsEmptyOrNothing()) Then

                    Select Case nastavenie.Nastavenie.DatovyTyp

                        Case DatovyTyp.Retazec
                            .Item("Hodnota") = nastavenie.Hodnota

                        Case DatovyTyp.CisloCele
                            .Item("Hodnota") = Integer.Parse(nastavenie.Hodnota, Globalization.CultureInfo.InvariantCulture)

                        Case DatovyTyp.CisloDesatinne
                            .Item("Hodnota") = Decimal.Parse(nastavenie.Hodnota, Globalization.CultureInfo.InvariantCulture)

                        Case DatovyTyp.AnoNie
                            .Item("Hodnota") = Boolean.Parse(nastavenie.Hodnota)

                        Case DatovyTyp.DatumCas
                            .Item("Hodnota") = Date.Parse(nastavenie.Hodnota)
                    End Select
                End If

                .IsNode = True
                .Node.Level = skupina.Uroven + 1
                .UserData = nastavenie
            End With
        Next

        For Each dieta As NastavenieSkupina In skupina.Deti.OrderBy(Function(x) x.Nazov)

            If ((Globals.Features.Pouzivatelia AndAlso _
                 dieta.Nazov.Equals(Nastavenia.EntitaPouzivatel(Globals.Pouzivatel))) _
                OrElse Prava.Over(Prava.Nastavenia)) Then

                SkupinaPridaj(dieta)
            End If
        Next
    End Sub

#End Region

#Region "Editing"

    Private Sub Grid_BeforeEdit(ByVal sender As Object, ByVal e As RowColEventArgs) Handles Grid.BeforeEdit

        If (e.Row < Grid.Rows.Fixed) Then e.Cancel = True : Return

        If (e.Col = Grid.Cols("Hodnota").Index) Then

            If (TypeOf (Grid.Rows(e.Row).UserData) Is NastavenieSkupina) Then e.Cancel = True

        ElseIf (e.Col = Grid.Cols("Aktivne").Index) Then

            If (TypeOf (Grid.Rows(e.Row).UserData) Is NastavenieSkupina) Then e.Cancel = True

            If (Not DirectCast(Grid.Rows(e.Row).UserData, NastavenieHodnota).Nastavenie.Upravitelne) Then e.Cancel = True
        Else
            e.Cancel = True
        End If
    End Sub

    'Private Sub Grid_ValidateEdit(sender As Object, e As ValidateEditEventArgs) Handles Grid.ValidateEdit

    '    With Grid
    '        Dim nastavenie As Nastavenie = DirectCast(.Rows(e.Row).UserData, Nastavenie)
    '        Try
    '            Select Case nastavenie.DatovyTyp

    '                Case DatovyTyp.AnoNie
    '                    'Prerobene na editor cez stanovenie datoveho typu Boolean.Parse(GuiUtil.TrimmedText(.Editor))

    '                Case DatovyTyp.CisloCele
    '                    Integer.Parse(GuiUtil.TrimmedText(.Editor), Globalization.NumberFormatInfo.InvariantInfo)

    '                Case DatovyTyp.CisloDesatinne
    '                    Double.Parse(GuiUtil.TrimmedText(.Editor), Globalization.NumberFormatInfo.InvariantInfo)

    '                Case DatovyTyp.Retazec

    '                Case DatovyTyp.DatumCas
    '                    DateTime.Parse(GuiUtil.TrimmedText(.Editor))
    '            End Select

    '        Catch ex As Exception
    '            e.Cancel = True
    '        End Try
    '    End With
    'End Sub

    Private Sub Grid_AfterEdit(sender As Object, e As RowColEventArgs) Handles Grid.AfterEdit

        Dim hodnota = DirectCast(Grid.Rows(e.Row).UserData, NastavenieHodnota)

        If (e.Col = Grid.Cols("Hodnota").Index) Then

            hodnota.Hodnota = Converting.ToStringOrNothing(Grid.Rows(e.Row)(e.Col))

        ElseIf (e.Col = Grid.Cols("Aktivne").Index) Then

            hodnota.Aktivna = DirectCast(Grid.Rows(e.Row)(e.Col), Boolean)
        End If

        If (Not _zmenene.Contains(hodnota)) Then _zmenene.Add(hodnota)
    End Sub

#End Region

End Class
