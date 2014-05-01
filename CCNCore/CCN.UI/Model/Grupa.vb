Imports CCN.Core.VB

Public Class Grupa(Of T As Class)

    Public Property Definicia As GrupaDefinicia(Of T)
    Public Property Kluc As Object

    Public Property Hodnoty As New List(Of T)
    Public Property Rodic As Grupa(Of T)

    Public Property Deti As New List(Of Grupa(Of T))

    'Public Sub New(definicia As GrupaDefinicia(Of T), kluc As Object, hodnoty As IList(Of T))

    '    _definicia = definicia
    '    _kluc = kluc
    '    _hodnoty = hodnoty
    'End Sub

    'Protected _definicia As GrupaDefinicia(Of T)
    'Public ReadOnly Property Definicia As GrupaDefinicia(Of T)
    '    Get
    '        Return _definicia
    '    End Get
    'End Property

    'Protected _kluc As Object
    'Public ReadOnly Property Kluc As Object
    '    Get
    '        Return _definicia
    '    End Get
    'End Property

    'Protected _hodnoty As IList(Of T)
    'Public ReadOnly Property Hodnoty As IList(Of T)
    '    Get
    '        Return _hodnoty
    '    End Get
    'End Property

    'Protected _rodic As Grupa(Of T)
    'Public ReadOnly Property Rodic As Grupa(Of T)
    '    Get
    '        Return _rodic
    '    End Get
    'End Property

    'Protected _deti As IList(Of Grupa(Of T))
    'Public ReadOnly Property Deti As IList(Of Grupa(Of T))
    '    Get
    '        Return _deti
    '    End Get
    'End Property

End Class
