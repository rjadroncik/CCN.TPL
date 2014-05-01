Imports System.Runtime.CompilerServices

Public Module ExtensionMethods

#Region "Enums"

    <Extension()> _
    Public Function GetDescription(enumValue As [Enum]) As String

        Return Formatting.GetDescription(enumValue)
    End Function

#End Region

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

    <Extension()> _
    Public Function ToStringAll(Of T)(values As IEnumerable(Of T), _
                                      Optional separator As String = ", ") As String

        If (values.Count() = 0) Then Return String.Empty

        Return values.Select(Function(x) x.ToString()).Aggregate(Function(x, y) x & separator & y)
    End Function

    <Extension()> _
    Public Function ToStringAll(Of T)(values As IEnumerable(Of T), _
                                      toString As CustomToString(Of T), _
                                      Optional separator As String = ", ") As String

        If (values.Count() = 0) Then Return String.Empty

        Return values.Select(Function(x) toString(x)).Aggregate(Function(x, y) x & separator & y)
    End Function

    <Extension()> _
    Public Function ToList(Of TSource, TList As {IList(Of TSource), New})(source As IEnumerable(Of TSource)) As TList

        Dim result As New TList()

        For Each element As TSource In source

            result.Add(element)
        Next

        Return result
    End Function

    <Extension()>
    Public Function ToHashSet(Of TSource)(source As IEnumerable(Of TSource)) As HashSet(Of TSource)

        Dim result As New HashSet(Of TSource)

        For Each value In source

            result.Add(value)
        Next

        Return result
    End Function

    <Extension()> _
    Public Function ToDictionaryOfLists(Of TSource, TKey)(source As IEnumerable(Of TSource), _
                                                          keySelector As Func(Of TSource, TKey)) As IDictionary(Of TKey, IList(Of TSource))

        Dim result As New Dictionary(Of TKey, IList(Of TSource))()

        For Each element As TSource In source

            Dim key As TKey = keySelector(element)

            If (result.ContainsKey(key)) Then

                result(key).Add(element)
            Else
                Dim value As New List(Of TSource)()
                value.Add(element)

                result.Add(key, value)
            End If
        Next

        Return result
    End Function

    <Extension()> _
    Public Function ToDictionaryOfLists(Of TSource, TKey, TList As {IList(Of TSource), New})(source As IEnumerable(Of TSource), _
                                                                                             keySelector As Func(Of TSource, TKey)) As IDictionary(Of TKey, TList)

        Dim result As New Dictionary(Of TKey, TList)()

        For Each element As TSource In source

            Dim key As TKey = keySelector(element)

            If (result.ContainsKey(key)) Then

                result(key).Add(element)
            Else
                Dim value As New TList()
                value.Add(element)

                result.Add(key, value)
            End If
        Next

        Return result
    End Function

    <Extension()> _
    Public Function ToDictionaryOfLists(Of TSource, TKey, TValue)(source As IEnumerable(Of TSource), _
                                                                  keySelector As Func(Of TSource, TKey), _
                                                                  valueSelector As Func(Of TSource, TValue)) As IDictionary(Of TKey, IList(Of TValue))

        Dim result As New Dictionary(Of TKey, IList(Of TValue))()

        For Each element As TSource In source

            Dim key As TKey = keySelector(element)

            If (result.ContainsKey(key)) Then

                result(key).Add(valueSelector(element))
            Else
                Dim value As New List(Of TValue)
                value.Add(valueSelector(element))

                result.Add(key, value)
            End If
        Next

        Return result
    End Function

    <Extension()> _
    Public Function ToDictionaryOfListsDistinct(Of TSource, TKey, TValue)(source As IEnumerable(Of TSource), _
                                                                          keySelector As Func(Of TSource, TKey), _
                                                                          valueSelector As Func(Of TSource, TValue)) As IDictionary(Of TKey, IList(Of TValue))

        Dim result As New Dictionary(Of TKey, IList(Of TValue))()

        For Each element As TSource In source

            Dim key As TKey = keySelector(element)

            If (result.ContainsKey(key)) Then

                Dim value As TValue = valueSelector(element)

                If (Not result(key).Contains(value)) Then result(key).Add(value)
            Else
                Dim value As New List(Of TValue)
                value.Add(valueSelector(element))

                result.Add(key, value)
            End If
        Next

        Return result
    End Function

    <Extension()> _
    Public Function ToDictionary(Of TSource, TKey, TValue)(source As IEnumerable(Of TSource), _
                                                           keySelector As Func(Of TSource, TKey), _
                                                           valueSelector As Func(Of TSource, TValue), _
                                                           conflictResolver As Func(Of TValue, TValue, TValue)) As IDictionary(Of TKey, TValue)

        Dim result As New Dictionary(Of TKey, TValue)()

        For Each element As TSource In source

            Dim key As TKey = keySelector(element)

            If (result.ContainsKey(key)) Then

                Dim valueOld As TValue = result(key)
                Dim valueNew As TValue = conflictResolver(valueOld, valueSelector(element))

                If (Not valueNew.Equals(valueOld)) Then

                    result.Remove(key)
                    result.Add(key, valueNew)
                End If
            Else
                result.Add(key, valueSelector(element))
            End If
        Next

        Return result
    End Function

#End Region

#Region "ICollection" 'Abandoned ISet in favour of ICollection, because of .NET 3.5 compatibility

    <Extension()> _
    Public Sub AddAll(Of T)(target As ICollection(Of T), source As IEnumerable(Of T))

        For Each value As T In source

            target.Add(value)
        Next
    End Sub

    <Extension()> _
    Public Sub AddAllNew(Of T)(target As ICollection(Of T), source As IEnumerable(Of T))

        For Each value As T In source

            If (Not target.Contains(value)) Then target.Add(value)
        Next
    End Sub

#End Region

#Region "IList"

    <Extension()> _
    Public Sub AddAll(Of T)(target As IList(Of T), source As IEnumerable(Of T))

        For Each value As T In source

            target.Add(value)
        Next
    End Sub

    <Extension()> _
    Public Sub AddAllNew(Of T)(target As IList(Of T), source As IEnumerable(Of T))

        For Each value As T In source

            If (Not target.Contains(value)) Then target.Add(value)
        Next
    End Sub

#End Region

#Region "IDictionary"

    <Extension()> _
    Public Sub AddAll(Of TKey, TValue)(target As IDictionary(Of TKey, TValue), source As IDictionary(Of TKey, TValue))

        For Each pair As KeyValuePair(Of TKey, TValue) In source

            target.Add(pair.Key, pair.Value)
        Next
    End Sub

    <Extension()> _
    Public Sub AddAllNew(Of TKey, TValue)(target As IDictionary(Of TKey, TValue), source As IDictionary(Of TKey, TValue))

        For Each pair As KeyValuePair(Of TKey, TValue) In source

            If (Not target.ContainsKey(pair.Key)) Then target.Add(pair.Key, pair.Value)
        Next
    End Sub

#End Region

#Region "System.Version"

    <Extension()> _
    Public Function ToStringFile(value As Version) As String

        Return value.ToString().Replace(".", "_")
    End Function

#End Region

#Region "Reflection"

    <Extension()> _
    Public Function IsNullable(type As Type) As Boolean

        Return Nullable.GetUnderlyingType(type) IsNot Nothing
    End Function

#End Region

#Region "String"

    <Extension()> _
    Public Function IsEmptyOrNothing(value As String) As Boolean

        Return (value Is Nothing) OrElse (value.Equals(String.Empty))
    End Function

    <Extension()> _
    Public Function LimitTo(value As String, length As Integer) As String

        Return If(value.Length <= length, value, value.Substring(0, length))
    End Function

#End Region

End Module
