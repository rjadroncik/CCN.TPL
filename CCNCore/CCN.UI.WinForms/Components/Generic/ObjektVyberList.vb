Imports CCN.Model
Imports CCN.Core.VB
Imports C1.Win.C1FlexGrid
Imports System.ComponentModel

Public Class ObjektVyberList(Of T As Class)
    Implements IObjektVyber(Of T)

#Region "Properties"

    Public Property Vsetky As IEnumerable(Of T) Implements IObjektVyber(Of T).Vsetky

    Protected _zvoleny As T
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Zvoleny As T Implements IObjektVyber(Of T).Zvoleny
        Get
            Return _zvoleny
        End Get
        Set(value As T)

            Zvol(value)
        End Set
    End Property

    Protected _stlpce As New List(Of Stlpec(Of T))
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Stlpce As IEnumerable(Of Stlpec(Of T)) Implements IObjektVyber(Of T).Stlpce
        Get
            Return _stlpce
        End Get
        Set(value As IEnumerable(Of Stlpec(Of T)))

            _stlpce.Clear()
            _stlpce.AddAll(value)

            FlexGrid.ColumnsGenerate(Grid, _stlpce)
            FlexGrid.ColumnsGenerate(GridZvoleny, _stlpce)

            _stlpceVlastne = True
        End Set
    End Property

    Protected _stlpceVlastne As Boolean = False

#End Region

#Region "Events"

    Public Event ObjektZvoleny As Action(Of T) Implements IObjektVyber(Of T).ObjektZvoleny

#End Region

#Region "Event handling"

    Private Sub Grid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Grid.MouseDoubleClick

        If (Grid.Row >= Grid.Rows.Fixed) Then Zvol(DirectCast(Grid.Rows(Grid.Row).UserData, T))
    End Sub

#End Region

#Region "Updating"

    Public Sub UpdateGrid()

        With Grid
            Dim selected = .Row

            .BeginUpdate()
            .Rows.Count = 1

            For Each objekt As T In Vsetky

                ObjektPridaj(Grid, objekt)
            Next

            If (_stlpceVlastne) Then

                FlexGrid.Sort(Grid, Stlpce)
                .AutoSizeCols()
            End If

            FlexGrid.RestoreSelection(Grid, selected)

            .EndUpdate()
        End With

        If (_zvoleny IsNot Nothing) Then

            If (Vsetky.Contains(_zvoleny)) Then
                Zvol(_zvoleny)
            Else
                _zvoleny = Nothing
                GridZvoleny.Rows.Count = 0
            End If
        End If
    End Sub

    Protected Sub ObjektPridaj(grid As C1FlexGrid, objekt As T)

        If (_stlpceVlastne) Then

            With grid.Rows.Add()

                If (objekt IsNot Nothing) Then

                    For Each stlpec As Stlpec(Of T) In _stlpce

                        .Item(stlpec.Nazov) = stlpec.Getter(objekt)
                    Next
                End If
                .UserData = objekt
            End With
        Else
            With grid.Rows.Add()

                .Item(0) = objekt
                .UserData = objekt
            End With
        End If
    End Sub

    Protected Sub Zvol(objekt As T)

        _zvoleny = objekt

        GridZvoleny.Rows.Count = 0
        ObjektPridaj(GridZvoleny, objekt)

        GridZvoleny.AutoSizeCols()

        RaiseEvent ObjektZvoleny(objekt)
    End Sub

#End Region

End Class
