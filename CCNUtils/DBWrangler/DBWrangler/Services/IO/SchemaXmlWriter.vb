Imports System.Xml
Imports CCN.Core.VB
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Base
Imports DBWrangler.Model.Schema.Datatypes

Namespace Services.IO

    Public MustInherit Class SchemaXmlWriter

        Private Shared _writer As XmlWriter

        Private Shared Sub WriteStartElement(name As String)

            _writer.WriteStartElement("cdw", name, "http://ccn.sk/utils/db_wrangler/v1.0")
        End Sub

        Public Shared Sub Write(schema As Schema, xmlFile As String)

            Dim settings As New XmlWriterSettings()
            settings.Indent = True
            settings.NewLineOnAttributes = True

            _writer = XmlTextWriter.Create(xmlFile, settings)
            WriteSchema(schema)
            _writer.Close()
        End Sub

        Private Shared Sub WriteSchema(schema As Schema)

            _writer.WriteStartDocument()
            WriteStartElement("schema")
            _writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
            _writer.WriteAttributeString("version_file", "1.0")
            _writer.WriteAttributeString("version_db", schema.Version.ToString())

            For Each table As Table In schema.Tables
                WriteTable(table)
            Next

            _writer.WriteEndElement()
            _writer.WriteEndDocument()
        End Sub

        Private Shared Sub WriteTable(table As Table)

            WriteStartElement("table")
            WriteElementAttributes(table)

            For Each column As Column In table.Columns
                WriteColumn(column)
            Next

            If ((table.PrimaryKey IsNot Nothing) AndAlso table.PrimaryKey.Columns.Any()) Then WriteKeyPrimary(table.PrimaryKey)

            For Each uniqueKey As KeyUnique In table.UniqueKeys
                WriteKeyUnique(uniqueKey)
            Next

            For Each foreignKey As KeyForeign In table.ForeignKeys
                WriteKeyForeign(foreignKey)
            Next

            For Each index As Index In table.Indexes
                WriteIndex(index)
            Next

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteColumn(column As Column)

            WriteStartElement("column")
            WriteElementAttributes(column)

            If (Not column.Nullable) Then _writer.WriteAttributeString("nullable", Converting.ToXml(column.Nullable))
            If (column.Identity) Then

                _writer.WriteAttributeString("identity", Converting.ToXml(column.Identity))
                _writer.WriteAttributeString("identity_seed", column.IdentitySeed.ToStringInvariant())
                _writer.WriteAttributeString("identity_increment", column.IdentityIncrement.ToStringInvariant())
            End If

            WriteDataType(column.DataType)

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteColumnList(columnList As ColumnList)

            For Each column As Column In columnList.Columns

                _writer.WriteElementString("cdw", "column", "http://ccn.sk/utils/db_wrangler/v1.0", column.Name)
            Next
        End Sub

        Private Shared Sub WriteColumnPairs(key As KeyForeign)

            For Each column As Column In key.Columns.Keys

                Dim nameFK As String = column.Name
                Dim nameRK As String = key.Columns(column).Name

                WriteStartElement("column")
                _writer.WriteAttributeString("key", nameFK)
                _writer.WriteAttributeString("value", nameRK)
                _writer.WriteEndElement()
            Next
        End Sub

        Private Shared Sub WriteKeyPrimary(primaryKey As KeyPrimary)

            WriteStartElement("primary_key")
            WriteElementAttributes(primaryKey)

            If (primaryKey.Clustered) Then _writer.WriteAttributeString("clustered", Converting.ToXml(primaryKey.Clustered))

            WriteColumnList(primaryKey)

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteKeyUnique(uniqueKey As KeyUnique)

            WriteStartElement("unique_key")
            WriteElementAttributes(uniqueKey)

            If (uniqueKey.Clustered) Then _writer.WriteAttributeString("clustered", Converting.ToXml(uniqueKey.Clustered))

            WriteColumnList(uniqueKey)

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteKeyForeign(foreignKey As KeyForeign)

            WriteStartElement("foreign_key")
            WriteElementAttributes(foreignKey)

            _writer.WriteAttributeString("referenced_key", foreignKey.ReferencedKey.Name)

            WriteColumnPairs(foreignKey)

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteIndex(index As Index)

            WriteStartElement("index")
            WriteElementAttributes(index)

            If (index.Unique) Then _writer.WriteAttributeString("unique", Converting.ToXml(index.Unique))
            If (index.Clustered) Then _writer.WriteAttributeString("clustered", Converting.ToXml(index.Clustered))

            WriteColumnList(index)

            _writer.WriteEndElement()
        End Sub

#Region "Datatypes"

        Private Shared Sub WriteDataType(datatype As DataType)

            If (TypeOf datatype Is DtString) Then WriteString(DirectCast(datatype, DtString)) : Return
            If (TypeOf datatype Is DTChar) Then WriteChar(DirectCast(datatype, DTChar)) : Return
            If (TypeOf datatype Is DtDecimal) Then WriteDecimal(DirectCast(datatype, DtDecimal)) : Return
            If (TypeOf datatype Is DtSingle) Then WriteSingle(DirectCast(datatype, DtSingle)) : Return
            If (TypeOf datatype Is DtDouble) Then WriteDouble(DirectCast(datatype, DtDouble)) : Return
            If (TypeOf datatype Is DtDate) Then WriteDate(DirectCast(datatype, DtDate)) : Return
            If (TypeOf datatype Is DtInt16) Then WriteInt16(DirectCast(datatype, DtInt16)) : Return
            If (TypeOf datatype Is DtInt32) Then WriteInt32(DirectCast(datatype, DtInt32)) : Return
            If (TypeOf datatype Is DtInt64) Then WriteInt64(DirectCast(datatype, DtInt64)) : Return
            If (TypeOf datatype Is DtGuid) Then WriteGuid(DirectCast(datatype, DtGuid)) : Return
            If (TypeOf datatype Is DtBoolean) Then WriteBoolean(DirectCast(datatype, DtBoolean)) : Return
            If (TypeOf datatype Is DtByte) Then WriteByte(DirectCast(datatype, DtByte)) : Return
            If (TypeOf datatype Is DtByteArray) Then WriteByteArray(DirectCast(datatype, DtByteArray)) : Return
            If (TypeOf datatype Is DtVariant) Then WriteVariant(DirectCast(datatype, DtVariant)) : Return
            If (TypeOf datatype Is DtTimestamp) Then WriteTimestamp(DirectCast(datatype, DtTimestamp)) : Return
        End Sub

        Private Shared Sub WriteString(datatype As DtString)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_string")
            If (datatype.SizeFixed) Then _writer.WriteAttributeString("size_fixed", Converting.ToXml(datatype.SizeFixed))
            _writer.WriteAttributeString("size", datatype.Size.ToStringInvariant())
            If (Not datatype.Unicode) Then _writer.WriteAttributeString("unicode", Converting.ToXml(datatype.Unicode))
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteChar(datatype As DTChar)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_char")
            If (Not datatype.Unicode) Then _writer.WriteAttributeString("unicode", Converting.ToXml(datatype.Unicode))
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteInt16(datatype As DtInt16)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_int16")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteInt32(datatype As DtInt32)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_int32")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteInt64(datatype As DtInt64)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_int64")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteGuid(datatype As DtGuid)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_guid")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteVariant(datatype As DtVariant)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_variant")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteDecimal(datatype As DtDecimal)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_decimal")

            If (datatype.Precision.HasValue) Then _writer.WriteAttributeString("precision", datatype.Precision.Value.ToStringInvariant())
            If (datatype.Scale.HasValue) Then _writer.WriteAttributeString("scale", datatype.Scale.Value.ToStringInvariant())
            If (datatype.Money) Then _writer.WriteAttributeString("money", Converting.ToXml(datatype.Money))
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteSingle(datatype As DtSingle)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_single")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteDouble(datatype As DtDouble)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_double")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteDate(datatype As DtDate)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_date")
            If (datatype.LowPrecision) Then _writer.WriteAttributeString("low_precision", Converting.ToXml(datatype.LowPrecision))
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteBoolean(datatype As DtBoolean)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_boolean")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteByte(datatype As DtByte)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_byte")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteByteArray(datatype As DtByteArray)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_bytearray")
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteTimestamp(datatype As DtTimestamp)

            WriteStartElement("datatype")
            _writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "cdw:dt_timestamp")

            If (datatype.Precision.HasValue) Then _writer.WriteAttributeString("precision", datatype.Precision.Value.ToStringInvariant())

            _writer.WriteEndElement()
        End Sub

#End Region

        Private Shared Sub WriteCondition(condition As Condition, type As String)

            WriteStartElement(type)
            _writer.WriteAttributeString("expression", condition.Expression)

            For Each variable As Variable In condition.Variables
                WriteVariable(variable)
            Next

            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteVariable(value As Variable)

            WriteStartElement("variable")
            WriteElementAttributes(value)

            _writer.WriteAttributeString("key", value.Key)
            _writer.WriteEndElement()
        End Sub

        Private Shared Sub WriteElementAttributes(element As Element)

            _writer.WriteAttributeString("name", element.Name)
        End Sub

    End Class
End Namespace