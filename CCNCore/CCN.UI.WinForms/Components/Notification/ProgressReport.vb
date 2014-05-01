Public Class ProgressReport

#Region "Event handling"

    Private Sub ProgressReport_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        If (_initialized) Then GridAdjust()
    End Sub

#End Region

#Region "Initialization"

    Private _initialized As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _initialized = True
    End Sub

#End Region

#Region "Internal"

    Private Delegate Sub MessageDelegate(status As String)

    Private _gridAddMessage As New MessageDelegate(AddressOf GridAddMessageInternal)
    Private _gridUpdateMessage As New MessageDelegate(AddressOf GridUpdateMessageInternal)

    Private Sub GridUpdateMessageInternal(message As String)

        With Grid
            .BeginUpdate()

            .Rows(.Rows.Count - 1)(0) = message

            GridAdjust()

            .EndUpdate()
            .Select(.Rows.Count - 1, 0, .Rows.Count - 1, 0, True)
        End With
    End Sub

    Private Sub GridAddMessageInternal(message As String)

        With Grid
            .BeginUpdate()

            .AddItem(message)
            .Rows(.Rows.Count - 1)(1) = DateTime.Now

            GridAdjust()

            .EndUpdate()
            .Select(.Rows.Count - 1, 0, .Rows.Count - 1, 0, True)
        End With
    End Sub

    Private Sub GridAdjust()

        With Grid
            .BeginUpdate()

            .Cols("Operacia").Width = .ClientSize.Width - 108
            .Cols("Cas").Width = 108

            .AutoSizeRows()

            .EndUpdate()
        End With
    End Sub

#End Region

#Region "Public"

    Public Sub GridAddMessage(message As String)

        Me.Invoke(_gridAddMessage, New Object() {message})
    End Sub

    Public Sub GridUpdateMessage(message As String)

        Me.Invoke(_gridUpdateMessage, New Object() {message})
    End Sub

#End Region

End Class
