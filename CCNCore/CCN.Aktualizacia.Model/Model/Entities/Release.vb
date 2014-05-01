Imports CCN.Model
Imports System.IO
Imports CCN.Core.VB
Imports System.Reflection

Public Class Release

#Region "Properties"

    Public Property Komponent As ComponentType
    Public Property Version As Version
    Public Property MainExe As String

    Public Property Entities As New List(Of String)
    Public Property EntitiesAll As Boolean

    Public Property OperationsAplikacia As New List(Of Operation)
    Public Property OperationsUpdater As New List(Of Operation)

#End Region

End Class
