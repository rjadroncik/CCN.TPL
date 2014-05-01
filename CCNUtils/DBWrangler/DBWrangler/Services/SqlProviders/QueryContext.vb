
Imports CCN.Core.VB
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Filtering

Namespace Services.SqlProviders

    Public Class QueryContext

        Public Property Ids As IDictionary(Of Table, ISet(Of DbValues)) = New Dictionary(Of Table, ISet(Of DbValues))

        Public Sub IdAdd(table As Table, id As DbValues)

            If (Ids.ContainsKey(table)) Then

                Dim list = Ids(table)
                list.Add(id)
            Else
                Dim list = New HashSet(Of DbValues)()
                list.Add(id)

                Ids.Add(table, list)
            End If
        End Sub

        Public Function KeyExists(table As Table, id As DbValues) As Boolean

            If (Ids.ContainsKey(table)) Then

                For Each existingId In Ids(table)

                    If (existingId.Equals(id)) Then Return True
                Next

            End If

            Return False
        End Function

    End Class
End Namespace