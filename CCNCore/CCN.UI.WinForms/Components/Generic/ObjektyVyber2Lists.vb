Imports CCN.Model
Imports CCN.Core.VB
Imports C1.Win.C1FlexGrid
Imports System.ComponentModel

Public Class ObjektyVyber2Lists(Of T As Class)
    Implements IObjektyVyber(Of T)

#Region "Properties"

    Public Property Vsetky As IEnumerable(Of T) Implements IObjektyVyber(Of T).Vsetky

    Protected _zvolene As New HashSet(Of T)
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Zvolene As IEnumerable(Of T) Implements IObjektyVyber(Of T).Zvolene
        Get
            Return _zvolene.ToList()
        End Get
        Set(value As IEnumerable(Of T))

            _zvolene.Clear()
            _zvolene.AddAll(value)
        End Set
    End Property

    Protected _stlpce As New List(Of Stlpec(Of T))
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Stlpce As IEnumerable(Of Stlpec(Of T)) Implements IObjektyVyber(Of T).Stlpce
        Get
            Return _stlpce
        End Get
        Set(value As IEnumerable(Of Stlpec(Of T)))

            _stlpce.Clear()
            _stlpce.AddAll(value)

            FlexGrid.ColumnsGenerate(GridVsetko, _stlpce)
            FlexGrid.ColumnsGenerate(GridZvolene, _stlpce)

            _stlpceVlastne = True
        End Set
    End Property

    Protected _stlpceVlastne As Boolean = False

#End Region

#Region "Events"

    Public Event ObjektPridany As Action(Of T) Implements IObjektyVyber(Of T).ObjektPridany
    Public Event ObjektOdobrany As Action(Of T) Implements IObjektyVyber(Of T).ObjektOdobrany

#End Region

#Region "Event handling"

    Private Sub MoveLeft_Click(sender As Object, e As EventArgs) Handles MoveLeft.Click

        If (GridVsetko.Row >= GridVsetko.Rows.Fixed) Then

            Dim objekt = DirectCast(GridVsetko.Rows(GridVsetko.Row).UserData, T)
            _zvolene.Add(objekt)

            ObjektOdober(GridVsetko, objekt)
            ObjektPridaj(GridZvolene, objekt)

            RaiseEvent ObjektPridany(objekt)
        End If
    End Sub

    Private Sub MoveRight_Click(sender As Object, e As EventArgs) Handles MoveRight.Click

        If (GridZvolene.Row >= GridZvolene.Rows.Fixed) Then

            Dim objekt = DirectCast(GridZvolene.Rows(GridZvolene.Row).UserData, T)
            _zvolene.Remove(objekt)

            ObjektOdober(GridZvolene, objekt)
            ObjektPridaj(GridVsetko, objekt)

            RaiseEvent ObjektOdobrany(objekt)
        End If
    End Sub

#End Region

#Region "Updating"

    Public Sub UpdateGrids()

        With GridVsetko
            Dim selected = .Row

            .BeginUpdate()
            .Rows.Count = 1

            For Each objekt As T In Vsetky

                If (Not _zvolene.Contains(objekt)) Then ObjektPridaj(GridVsetko, objekt)
            Next

            If (_stlpceVlastne) Then

                FlexGrid.Sort(GridVsetko, Stlpce)
                GridVsetko.AutoSizeCols()
            End If

            FlexGrid.RestoreSelection(GridVsetko, selected)

            .EndUpdate()
        End With

        With GridZvolene
            Dim selected = .Row

            .BeginUpdate()
            .Rows.Count = 1

            For Each objekt As T In Zvolene

                ObjektPridaj(GridZvolene, objekt)
            Next

            If (_stlpceVlastne) Then

                FlexGrid.Sort(GridZvolene, Stlpce)
                GridZvolene.AutoSizeCols()
            End If

            FlexGrid.RestoreSelection(GridZvolene, selected)

            .EndUpdate()
        End With
    End Sub

    Protected Sub ObjektPridaj(grid As C1FlexGrid, objekt As T)

        If (_stlpceVlastne) Then

            With grid.Rows.Add()

                For Each stlpec As Stlpec(Of T) In _stlpce

                    .Item(stlpec.Nazov) = stlpec.Getter(objekt)
                Next
                .UserData = objekt
            End With
        Else
            With grid.Rows.Add()

                .Item(0) = objekt
                .UserData = objekt
            End With
        End If
    End Sub

    Protected Sub ObjektOdober(grid As C1FlexGrid, objekt As T)

        For Each row As Row In grid.Rows

            If (row.UserData Is objekt) Then grid.Rows.Remove(row.Index) : Return
        Next
    End Sub

#End Region

End Class
