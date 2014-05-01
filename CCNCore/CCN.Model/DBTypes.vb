Imports CCN.Core.VB

Public Class DBTypes

#Region "Public"

    Public Shared StringMax As Operation(Of String) = AddressOf MsSqlStringMax
    Public Shared StringFixed As Operation1(Of String, Integer) = AddressOf MsSqlStringFixed

    Public Shared StringMaxASCII As Operation(Of String) = AddressOf MsSqlStringMaxASCII
    Public Shared StringFixedASCII As Operation1(Of String, Integer) = AddressOf MsSqlStringFixedASCII

    Public Shared Int As Operation(Of String) = AddressOf MsSqlInt
    Public Shared Dec As Operation2(Of String, Integer, Integer) = AddressOf MsSqlDec

    Public Shared DateOnly As Operation(Of String) = AddressOf MsSqlDateOnly

#End Region

#Region "Protected"

    Protected Shared Function MsSqlStringMax() As String

        Return "NVARCHAR(MAX)"
    End Function

    Public Shared Function MsSqlStringFixed(length As Integer) As String

        Return "NCHAR(" & length & ")"
    End Function

    Protected Shared Function MsSqlStringMaxASCII() As String

        Return "VARCHAR(MAX)"
    End Function

    Public Shared Function MsSqlStringFixedASCII(length As Integer) As String

        Return "CHAR(" & length & ")"
    End Function

    Protected Shared Function MsSqlInt() As String

        Return "INT"
    End Function

    Public Shared Function MsSqlDec(precision As Integer, scale As Integer) As String

        Return "DECIMAL(" & precision & "," & scale & ")"
    End Function

    Protected Shared Function MsSqlDateOnly() As String

        Return "SMALLDATETIME"
    End Function

#End Region

End Class
