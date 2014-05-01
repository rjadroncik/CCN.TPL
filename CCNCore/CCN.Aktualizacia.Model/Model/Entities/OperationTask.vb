Imports CCN.Core.VB
Imports CCN.Model
Imports System.Reflection

Public Class OperationTask
    Inherits Operation

#Region "Properties"

    Public Property Code As String
    Public Property Language As CodeLanguage

    Public Property References As New List(Of String)

#End Region

End Class