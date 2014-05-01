Imports CCN.Model
Imports CCN.Core.VB
Imports C1.Win.C1FlexGrid
Imports System.ComponentModel

Public Class ObjektyZoznam(Of T As Class)
    Implements IObjektyZoznam(Of T)

#Region "Properties"

    Public Property Vsetky As IEnumerable(Of T) Implements IObjektyZoznam(Of T).Vsetky

    Protected _stlpce As New List(Of Stlpec(Of T))
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Stlpce As IEnumerable(Of Stlpec(Of T)) Implements IObjektyZoznam(Of T).Stlpce
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

        ObjektAktualizuj(objekt, Grid.Rows.Add())
    End Sub

    Protected Sub ObjektOdober(index As Integer)

        Grid.Rows.Remove(index)
    End Sub

    Protected Sub ObjektAktualizuj(objekt As T)

        ObjektAktualizuj(objekt, Grid.Rows.ToEnumerable().Where(Function(x) x.UserData Is objekt).Single())
    End Sub

    Protected Sub ObjektAktualizuj(objekt As T, row As Row)

        If (_stlpceVlastne) Then

            With row

                For Each stlpec As Stlpec(Of T) In _stlpce

                    .Item(stlpec.Nazov) = stlpec.Getter(objekt)
                Next
                .UserData = objekt
            End With
        Else
            With row

                .Item(1) = objekt
                .UserData = objekt
            End With
        End If
    End Sub

#End Region

End Class
