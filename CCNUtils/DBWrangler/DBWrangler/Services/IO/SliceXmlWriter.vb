Imports System.Xml
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Slice

Namespace Services.IO

    Public MustInherit Class SliceXmlWriter

        Private Shared _writer As XmlWriter

        Public Shared Sub Write(slice As Slice, xmlFile As String)

            Dim settings As New XmlWriterSettings()
            settings.Indent = True
            settings.NewLineOnAttributes = True

            _writer = XmlTextWriter.Create(xmlFile, settings)
            WriteSlice(slice)
            _writer.Close()
        End Sub

        Private Shared Sub WriteSlice(slice As Slice)

            _writer.WriteStartDocument()
            _writer.WriteStartElement("slice", "irs.db")

            _writer.WriteAttributeString("version", "1.0")
            _writer.WriteAttributeString("source_file", slice.SourceFile)

            For Each query As Query In slice.Queries

                'If (TypeOf query Is QuerySimpleCondition) Then WriteQuerySimpleCustom(DirectCast(query, QuerySimpleCondition)) : Continue For
                'If (TypeOf query Is QuerySimple) Then WriteQuerySimple(DirectCast(query, QuerySimple)) : Continue For
                'If (TypeOf query Is QueryJoinKeyCustom) Then WriteQueryJoinCustom(DirectCast(query, QueryJoinKeyCustom)) : Continue For
                'If (TypeOf query Is QueryJoinKey) Then WriteQueryJoin(DirectCast(query, QueryJoinKey)) : Continue For
            Next

            _writer.WriteEndElement()
            _writer.WriteEndDocument()
        End Sub

        'Private Shared Sub WriteQuerySimpleCustom(query As QueryCondition)

        '    _writer.WriteStartElement("query_simple_custom")

        '    _writer.WriteAttributeString("source", query.Source.Name)
        '    If (query.Limit > 0) Then _writer.WriteAttributeString("limit", query.Limit.ToStringInvariant())

        '    If (query.Condition IsNot Nothing) Then WriteCondition(query.Condition)

        '    _writer.WriteEndElement()
        'End Sub

        Private Shared Sub WriteCondition(condition As Condition)

            _writer.WriteStartElement("condition")

            _writer.WriteAttributeString("expression", condition.Expression)

            For Each variable As Variable In condition.Variables
                WriteVariable(variable)
            Next

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteVariable(value As Variable)

            _writer.WriteStartElement("variable")
            _writer.WriteAttributeString("name", value.Name)
            _writer.WriteAttributeString("key", value.Key)
            _writer.WriteEndElement()
        End Sub

        'Private Shared Sub WriteQuerySimple(query As QuerySimple)

        '    _writer.WriteStartElement("query_simple")

        '    _writer.WriteAttributeString("source", query.Source.Name)
        '    _writer.WriteAttributeString("limit", query.Limit.ToStringInvariant())

        '    _writer.WriteEndElement()
        'End Sub

        'Private Shared Sub WriteQueryJoin(query As QueryJoinKey)

        '    _writer.WriteStartElement("query_join")

        '    _writer.WriteAttributeString("source", query.Source.Name)
        '    _writer.WriteAttributeString("target", query.Target.Name)
        '    _writer.WriteAttributeString("foreign_key", query.ForeignKey.Name)

        '    _writer.WriteEndElement()
        'End Sub

        'Private Shared Sub WriteQueryJoinCustom(query As QueryJoinKeyCustom)

        '    _writer.WriteStartElement("query_join_custom")

        '    _writer.WriteAttributeString("source", query.Source.Name)
        '    _writer.WriteAttributeString("target", query.Target.Name)

        '    For Each source As Column In query.ColumnMap.Keys

        '        _writer.WriteStartElement("column_pair")

        '        _writer.WriteAttributeString("source", source.Name)
        '        _writer.WriteAttributeString("target", query.ColumnMap(source).Name)

        '        _writer.WriteEndElement()
        '    Next

        '    _writer.WriteEndElement()
        'End Sub

    End Class
End Namespace