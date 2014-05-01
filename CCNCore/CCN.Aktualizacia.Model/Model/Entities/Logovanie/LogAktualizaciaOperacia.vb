Imports CCN.Model

<VersionBreak("8.1.19.6174")>
Public MustInherit Class LogAktualizaciaOperacia
    Inherits Log

#Region "Properties"

    Public Overridable Property Aktualizacia As LogAktualizacia

    Public Overridable Property Finished As DateTime?

#End Region

End Class
