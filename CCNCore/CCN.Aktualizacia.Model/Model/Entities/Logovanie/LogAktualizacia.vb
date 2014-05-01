Imports CCN.Model

Public Class LogAktualizacia
    Inherits Log

#Region "Initialization"

    Protected Sub New()
    End Sub

    Public Shared Function Create(datum As DateTime) As LogAktualizacia

        Dim log As New LogAktualizacia()

        log.Datum = datum
        'log.Operacie = New List(Of LogAktualizaciaOperacia)()

        Return log
    End Function

#End Region

#Region "Properties"

    <Permanent()>
    Public Overridable Property VerziaStara As Version
    <Permanent()>
    Public Overridable Property VerziaNova As Version

    <Permanent()>
    Public Overridable Property Komponent As String

    <VersionBreak("8.1.19.6174")>
    Public Overridable Property Finished As DateTime?

    'Public Overridable Property Operacie As IList(Of LogAktualizaciaOperacia)

#End Region

End Class
