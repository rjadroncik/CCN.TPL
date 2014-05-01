Imports CCN.Model
Imports CCN.Core.VB
Imports C1.Win.C1FlexGrid
Imports System.ComponentModel

Public Class ObjektVyberGrid(Of T As Class)
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

            _stlpceVlastne = True
        End Set
    End Property

    Protected _stlpceVlastne As Boolean = False

#End Region

#Region "Events"

    Public Event ObjektZvoleny As Action(Of T) Implements IObjektVyber(Of T).ObjektZvoleny
    Public Event ObjektDoubleClick As Action(Of T)

#End Region

#Region "Event handling"

    Private _initalized As Boolean = False

    Private Sub Grid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Grid.MouseDoubleClick

        If ((Grid.Row >= Grid.Rows.Fixed) AndAlso _initalized) Then RaiseEvent ObjektDoubleClick(DirectCast(Grid.Rows(Grid.Row).UserData, T))
    End Sub

    Private Sub Grid_SelChange(sender As Object, e As EventArgs) Handles Grid.SelChange

        If ((Grid.Row >= Grid.Rows.Fixed) AndAlso _initalized) Then Zvol(DirectCast(Grid.Rows(Grid.Row).UserData, T))
    End Sub

#End Region

#Region "Updating"

    Public Sub UpdateGrid()

        _initalized = False
        With Grid
            Dim selected = .Row

            .BeginUpdate()
            .Rows.Count = 1

            For Each objekt As T In Vsetky

                ObjektPridaj(objekt)
            Next

            If (_stlpceVlastne) Then

                FlexGrid.Sort(Grid, Stlpce)
                Grid.AutoSizeCols()
            End If

            FlexGrid.RestoreSelection(Grid, selected)

            .EndUpdate()
            _initalized = True

            If (_zvoleny IsNot Nothing) Then

                Zvol(_zvoleny)
            Else
                Grid_SelChange(Nothing, Nothing)
            End If
        End With
    End Sub

    Protected Sub ObjektPridaj(objekt As T)

        If (_stlpceVlastne) Then

            With Grid.Rows.Add()

                For Each stlpec As Stlpec(Of T) In _stlpce

                    .Item(stlpec.Nazov) = stlpec.Getter(objekt)
                Next
                .UserData = objekt
            End With
        Else
            With Grid.Rows.Add()

                .Item(0) = objekt
                .UserData = objekt
            End With
        End If
    End Sub

    Protected Sub Zvol(objekt As T)

        If (objekt IsNot Nothing) Then

            Dim row = Grid.Rows.ToEnumerable().Where(Function(x) x.UserData Is objekt).SingleOrDefault()
            If (row IsNot Nothing) Then

                ZvolenyNastav(row.Index)
            End If
        End If

        _zvoleny = objekt
        RaiseEvent ObjektZvoleny(objekt)
    End Sub

    Protected Sub ZvolenyNastav(row As Integer)

        Grid.Row = row
        Grid.RowSel = row
    End Sub

#End Region

End Class
