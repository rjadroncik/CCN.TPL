Imports System.Runtime.CompilerServices

Public Module ExtensionMethods

#Region "Value types"

    <Extension()> _
    Public Function ToStringInvariant(Of T As IFormattable)(value As T) As String

        Return value.ToString(Nothing, Globalization.CultureInfo.InvariantCulture)
    End Function

#End Region

#Region "IEnumerable"

    <Extension()> _
    Public Function IsEmpty(Of T)(values As IEnumerable(Of T)) As Boolean

        For Each value In values

            Return False
        Next

        Return True
    End Function

    <Extension()> _
    Public Function IsEmpty(Of T)(values As IList(Of T)) As Boolean

        Return values.Count = 0
    End Function

#End Region

End Module
