Imports CCN.Model
Imports CCN.Core.VB
Imports C1.Win.C1FlexGrid
Imports System.ComponentModel

Public Class ObjektyVyberCheck(Of T As Class)
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

            With Grid.Cols.Add()
                .DataType = GetType(Boolean)
            End With

            FlexGrid.ColumnsGenerate(Grid, _stlpce)

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

    Private Sub Grid_AfterEdit(sender As Object, e As RowColEventArgs) Handles Grid.AfterEdit

        With Grid.Rows(Grid.Row)

            Dim objekt = DirectCast(.UserData, T)

            If (DirectCast(.Item(0), Boolean) = True) Then

                _zvolene.Add(objekt)
                RaiseEvent ObjektPridany(objekt)
            Else

                _zvolene.Remove(objekt)
                RaiseEvent ObjektOdobrany(objekt)
            End If

        End With
    End Sub

#End Region

#Region "Updating"

    Public Sub UpdateGrids()

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
                Grid.Cols(1).Width = 20
            End If

            FlexGrid.RestoreSelection(Grid, selected)

            .EndUpdate()
        End With
    End Sub

    Protected Sub ObjektPridaj(objekt As T)

        If (_stlpceVlastne) Then

            With Grid.Rows.Add()

                .Item(0) = _zvolene.Contains(objekt)

                For Each stlpec As Stlpec(Of T) In _stlpce

                    .Item(stlpec.Nazov) = stlpec.Getter(objekt)
                Next
                .UserData = objekt
            End With
        Else
            With Grid.Rows.Add()

                .Item(0) = _zvolene.Contains(objekt)
                .Item(1) = objekt
                .UserData = objekt
            End With
        End If
    End Sub

#End Region

End Class
