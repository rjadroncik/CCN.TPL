Imports CCN.Model

<VersionBreak("8.1.19.6174")>
Public Class LogAktualizaciaOperaciaProgram
    Inherits LogAktualizaciaOperacia

#Region "Initialization"

    Protected Sub New()
    End Sub

    Public Shared Function Create(datum As DateTime, akcia As ProgramAction) As LogAktualizaciaOperaciaProgram

        Dim log As New LogAktualizaciaOperaciaProgram()

        log.Datum = datum
        log.Action = akcia

        Return log
    End Function

#End Region

#Region "Properties"

    <Permanent()>
    Public Overridable Property Action As ProgramAction
    <Permanent()>
    Public Overridable Property Path As String
    <Permanent()>
    Public Overridable Property Process As String

    <Permanent()>
    Public Overridable Property Arguments As String

#End Region

End Class
