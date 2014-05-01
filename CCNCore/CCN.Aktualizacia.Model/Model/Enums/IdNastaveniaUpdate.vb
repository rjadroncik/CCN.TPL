Imports CCN.Core.VB
Imports CCN.Model

Public MustInherit Class IdNastaveniaUpdate
    Inherits IdNastavenia

#Region "Properties"

    Public Shared ReadOnly UpdateFtpServer As New IdNastavenia(21, "Update - ftp server")
    Public Shared ReadOnly UpdateFtpLogin As New IdNastavenia(22, "Update - ftp login")
    Public Shared ReadOnly UpdateFtpPassword As New IdNastavenia(23, "Update - ftp password")
    Public Shared ReadOnly UpdateFtpPath As New IdNastavenia(24, "Update - ftp path")
    Public Shared ReadOnly UpdateProxyEnabled As New IdNastavenia(101, "Update - proxy enabled")
    Public Shared ReadOnly UpdateProxyServer As New IdNastavenia(102, "Update - proxy server")
    Public Shared ReadOnly UpdateProxyPort As New IdNastavenia(103, "Update - proxy port")
    Public Shared ReadOnly UpdateProxyLogin As New IdNastavenia(104, "Update - proxy login")
    Public Shared ReadOnly UpdateProxyPassword As New IdNastavenia(105, "Update - proxy password")

#End Region

End Class
