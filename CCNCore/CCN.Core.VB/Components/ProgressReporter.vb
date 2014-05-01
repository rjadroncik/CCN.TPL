''' <summary>
''' Umoznuje informovanie klienta volajuceho sluzbu servisu o priebehu jej
''' vykonavania. Zaroven poskytuje mechanizmus pre monitorovanie priebehu
''' viacerych po sebe iducich operacii.
''' </summary>
Public Class ProgressReporter

#Region "Delegates"

    ''' <summary>
    ''' Callback na notifikovanie klienta o priebehu vykonavania operacie
    ''' </summary>
    Public Delegate Sub ProgressDelegate(progress As Integer)

#End Region

#Region "Initialization"

    Public Sub New()

        _progressDelegate = Nothing
    End Sub

    Public Sub New(progressDelegate As ProgressDelegate)

        _progressDelegate = progressDelegate
    End Sub

#End Region

#Region "BL"

    Private _progressLast As Single = 0
    Private _progressDelegate As ProgressDelegate
    Private _progressWeight As Single = 100

    Public Function Weight(progressWeight As Single) As ProgressReporter

        _progressLast += (_progress * _progressWeight / 100.0F)

        _progressWeight = progressWeight
        _progress = 0
        Return Me
    End Function

    Public Sub ReportProgress()

        If (_progressDelegate IsNot Nothing) Then

            Dim result As Integer = CInt(_progressLast + (_progress * _progressWeight / 100.0F))

            If (result < 0) Then result = 0
            If (result > 100) Then result = 100

            _progressDelegate(result)
        End If
    End Sub

#End Region

#Region "Properties"

    Private _progress As Single = 0
    Public Property Progress As Single
        Get
            Return _progress
        End Get
        Set(value As Single)

            _progress = value

            ReportProgress()
        End Set
    End Property

#End Region

End Class