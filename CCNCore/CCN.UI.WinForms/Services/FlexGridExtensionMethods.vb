Imports System.Runtime.CompilerServices
Imports C1.Win.C1FlexGrid

Public Module FlexGridExtensionMethods

    <ExtensionAttribute()> _
    Public Function TextFarba(styl As CellStyle, farba As Color) As CellStyle

        styl.ForeColor = farba
        styl.DefinedElements = styl.DefinedElements Or StyleElementFlags.ForeColor

        Return styl
    End Function

    <ExtensionAttribute()> _
    Public Function TextStyl(styl As CellStyle, stylFontu As FontStyle) As CellStyle

        styl.Font = New Font(styl.Font, stylFontu)
        styl.DefinedElements = styl.DefinedElements Or StyleElementFlags.Font

        Return styl
    End Function

    <ExtensionAttribute()> _
    Public Function ToEnumerable(rows As RowCollection) As IEnumerable(Of Row)

        Return New RowsEnumerable(rows)
    End Function

    Private Class RowsEnumerable
        Implements IEnumerable(Of Row)

        Protected _rows As RowCollection

        Public Sub New(rows As RowCollection)

            _rows = rows
        End Sub

        Public Function GetEnumerator() As IEnumerator(Of Row) Implements IEnumerable(Of Row).GetEnumerator

            Return New RowsEnumerator(_rows)
        End Function

        Public Function GetEnumeratorOld() As IEnumerator Implements IEnumerable.GetEnumerator

            Return New RowsEnumerator(_rows)
        End Function
    End Class

    Private Class RowsEnumerator
        Implements IEnumerator(Of Row)

        Protected _index As Integer
        Protected _rows As RowCollection

        Public Sub New(rows As RowCollection)

            _rows = rows
            _index = rows.Fixed - 1
        End Sub

        Public ReadOnly Property Current As Row Implements IEnumerator(Of Row).Current
            Get
                Return _rows(_index)
            End Get
        End Property

        Public ReadOnly Property CurrentOld As Object Implements IEnumerator.Current
            Get
                Return _rows(_index)
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext

            If (_index + 1 = _rows.Count) Then Return False

            _index += 1
            Return True
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset

            _index = _rows.Fixed - 1
        End Sub

#Region "IDisposable Support"

        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region

    End Class

End Module
