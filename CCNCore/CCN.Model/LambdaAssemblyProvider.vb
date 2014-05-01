Imports System.Reflection

Public Class LambdaAssemblyProvider
    Implements IAssemblyProvider

#Region "IAssemblyProvider"

    Public Function Assemblies() As IEnumerable(Of Assembly) Implements IAssemblyProvider.Assemblies

        Return _values()
    End Function

#End Region

#Region "Initialization"

    Private _values As Func(Of IEnumerable(Of Assembly))

    Friend Sub New(values As Func(Of IEnumerable(Of Assembly)))

        _values = values
    End Sub

    Public Shared Function Create(values As Func(Of IEnumerable(Of Assembly))) As IAssemblyProvider

        Return New LambdaAssemblyProvider(values)
    End Function

#End Region

End Class
