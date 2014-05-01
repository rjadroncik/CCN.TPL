
Imports System.IO
Imports CCN.Services
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Slice

Namespace Services.IO

    Public MustInherit Class SliceXmlReader
        Inherits XmlService

        Public Shared Function Read(xmlFile As String) As Slice

            Return Read(xmlFile, Nothing)
        End Function

        Public Shared Function Read(xmlFile As String, postfix As String) As Slice

            Dim xmlDoc As XDocument = XDocument.Load(xmlFile)

            Dim xmlSlice As XElement = xmlDoc.Root

            If (ReadText(xmlSlice.Attribute("version")) <> "1.0") Then Throw New VersionNotFoundException("Currently only version 1.0 is supported")

            Dim info As New FileInfo(xmlFile)

            Return ReadSlice(xmlSlice, info.DirectoryName)
        End Function

        Private Shared Function ReadSlice(xmlSlice As XElement, directory As String) As Slice

            Dim slice As New Slice()

            slice.SourceFile = ReadText(xmlSlice.Attribute("source_file"))

            slice.Source = SchemaXmlReader.Read(directory & "/" & slice.SourceFile)

            For Each item As XElement In xmlSlice.Elements

                'If (item.Name.LocalName = "query_simple_custom") Then slice.Queries.Add(ReadQuerySimpleCustom(item, slice)) : Continue For
                'If (item.Name.LocalName = "query_simple") Then slice.Queries.Add(ReadQuerySimple(item, slice)) : Continue For
                'If (item.Name.LocalName = "query_join") Then slice.Queries.Add(ReadQueryJoin(item, slice)) : Continue For
                'If (item.Name.LocalName = "query_join_custom") Then slice.Queries.Add(ReadQueryJoinCustom(item, slice)) : Continue For
            Next

            Return slice
        End Function

        'Private Shared Function ReadQuerySimpleCustom(xmlQuery As XElement, slice As Slice) As QuerySimple

        '    Dim query As New QueryCondition()

        '    query.Source = slice.Source.TableNamed(ReadText(xmlQuery.Attribute("source")))

        '    query.Limit = ReadIntegerOptional(xmlQuery.Attribute("limit")).GetValueOrDefault()

        '    For Each item As XElement In xmlQuery.Elements

        '        If (item.Name.LocalName = "condition") Then query.Condition = ReadCondition(query.Source, item)
        '    Next

        '    Return query
        'End Function

        Private Shared Function ReadCondition(table As Table, xmlCondition As XElement) As Condition

            Dim condition As New Condition()

            condition.Expression = ReadText(xmlCondition.Attribute("expression"))

            For Each item As XElement In xmlCondition.Elements

                condition.Variables.Add(ReadVariable(item, table))
            Next

            Return condition
        End Function

        Private Shared Function ReadVariable(xmlVariable As XElement, table As Table) As Variable

            Dim variable As New Variable(table)

            variable.Name = ReadText(xmlVariable.Attribute("name"))
            variable.Key = ReadText(xmlVariable.Attribute("key"))

            Return variable
        End Function

        'Private Shared Function ReadQuerySimple(xmlQuery As XElement, slice As Slice) As QuerySimple

        '    Dim query As New QuerySimple()

        '    query.Source = slice.Source.TableNamed(ReadText(xmlQuery.Attribute("source")))
        '    query.Limit = ReadIntegerOptional(xmlQuery.Attribute("limit")).GetValueOrDefault()

        '    Return query
        'End Function

        'Private Shared Function ReadQueryJoin(xmlQuery As XElement, slice As Slice) As QueryJoinKey

        '    Dim query As New QueryJoinKey()

        '    query.Source = slice.Source.TableNamed(ReadText(xmlQuery.Attribute("source")))
        '    query.Target = slice.Source.TableNamed(ReadText(xmlQuery.Attribute("target")))
        '    query.ForeignKey = query.Source.ForeignKeyNamed(ReadText(xmlQuery.Attribute("foreign_key")))

        '    Return query
        'End Function

        'Private Shared Function ReadQueryJoinCustom(xmlQuery As XElement, slice As Slice) As QueryJoinKeyCustom

        '    Dim query As New QueryJoinKeyCustom()

        '    query.Source = slice.Source.TableNamed(ReadText(xmlQuery.Attribute("source")))
        '    query.Target = slice.Source.TableNamed(ReadText(xmlQuery.Attribute("target")))

        '    For Each item As XElement In xmlQuery.Elements

        '        If (item.Name.LocalName = "column_pair") Then

        '            query.ColumnMap.Add(query.Source.ColumnNamed(ReadText(item.Attribute("source"))), _
        '                                query.Target.ColumnNamed(ReadText(item.Attribute("target"))))
        '        End If
        '    Next

        '    Return query
        'End Function
    End Class
End Namespace