Imports CCN.Model

<VersionBreak("8.1.19.6174")>
Public Class LogAktualizaciaOperaciaSubor
    Inherits LogAktualizaciaOperacia

#Region "Initialization"

    Protected Sub New()
    End Sub

    Public Shared Function Create(datum As DateTime, akcia As FileAction) As LogAktualizaciaOperaciaSubor

        Dim log As New LogAktualizaciaOperaciaSubor()

        log.Datum = datum
        log.Action = akcia

        Return log
    End Function

#End Region

#Region "Properties"

    <Permanent()>
    Public Overridable Property Action As FileAction
    <Permanent()>
    Public Overridable Property Path As String

    <Permanent()>
    Public Overridable Property VerziaStara As Version
    <Permanent()>
    Public Overridable Property VerziaNova As Version

#End Region

End Class
