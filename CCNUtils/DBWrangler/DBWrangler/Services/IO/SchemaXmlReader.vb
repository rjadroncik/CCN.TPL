
Imports System.IO
Imports System.Xml.Schema
Imports CCN.Services
Imports DBWrangler.Model.Filtering
Imports DBWrangler.Model.Schema
Imports DBWrangler.Model.Schema.Base
Imports DBWrangler.Model.Schema.Datatypes

Namespace Services.IO

    Public MustInherit Class SchemaXmlReader
        Inherits XmlService

        Protected Shared _suffix As String
        Protected Shared _schema As Schema
        Protected Shared _allKeys As Dictionary(Of String, KeyUnique)

        Public Shared Function Read(xmlFile As String) As Schema

            Return Read(xmlFile, Nothing)
        End Function

        Public Shared Function Read(xmlFile As String, postfix As String) As Schema

            _suffix = If(postfix Is Nothing, "", postfix)

            _allKeys = New Dictionary(Of String, KeyUnique)

            Dim xDoc As XDocument = XDocument.Load(xmlFile)

            Dim xmlSchema As XmlSchema = xmlSchema.Read(New StringReader(My.Resources.Schema), AddressOf ValidationEventHandler)

            Dim xmlSchemaSet As New XmlSchemaSet()
            xmlSchemaSet.Add(xmlSchema)

            xDoc.Validate(xmlSchemaSet, AddressOf ValidationEventHandler)

            Dim xSchema As XElement = xDoc.Root

            If (ReadText(xSchema.Attribute("version_file")) <> "1.0") Then Throw New VersionNotFoundException("Currently only version 1.0 is supported")

            Return ReadSchema(xSchema)
        End Function

        Protected Shared Sub ValidationEventHandler(sender As Object, e As ValidationEventArgs)

            If (e.Severity = XmlSeverityType.Error) Then Throw e.Exception
        End Sub

        Private Shared Function ReadSchema(xSchema As XElement) As Schema

            _schema = New Schema()
            _schema.Version = Version.Parse(ReadText(xSchema.Attribute("version_db")))

            For Each item As XElement In xSchema.Elements

                If (item.Name.LocalName = "table") Then

                    _schema.Tables.Add(ReadTable(item))
                End If
            Next

            For Each item As XElement In xSchema.Elements

                If (item.Name.LocalName = "table") Then

                    ReadTableForeignKeys(_schema.TableNamed(ReadText(item.Attribute("name")) & _suffix), item)
                End If
            Next

            Return _schema
        End Function

        Private Shared Sub ReadTableForeignKeys(table As Table, xTable As XElement)

            For Each item As XElement In xTable.Elements

                If (item.Name.LocalName = "foreign_key") Then table.ForeignKeys.Add(ReadKeyForeign(item, table))
            Next
        End Sub

        Private Shared Function ReadTable(xTable As XElement) As Table

            Dim table As New Table(_schema)

            ReadElementAttributes(table, xTable)
            table.Name &= _suffix

            For Each item As XElement In xTable.Elements

                If (item.Name.LocalName = "column") Then table.Columns.Add(ReadColumn(item, table))
                If (item.Name.LocalName = "index") Then table.Indexes.Add(ReadIndex(item, table))

                If (item.Name.LocalName = "primary_key") Then table.PrimaryKey = ReadKeyPrimary(item, table)
                If (item.Name.LocalName = "unique_key") Then table.UniqueKeys.Add(ReadKeyUnique(item, table))
            Next

            Return table
        End Function

        Private Shared Function ReadColumn(xColumn As XElement, table As Table) As Column

            Dim column As New Column(table, ReadDataType(xColumn.Element(xColumn.Name.Namespace + "datatype")))

            ReadElementAttributes(column, xColumn)

            column.Nullable = ReadBooleanOptional(xColumn.Attribute("nullable")).GetValueOrDefault(True)
            column.Identity = ReadBooleanOptional(xColumn.Attribute("identity")).GetValueOrDefault()

            column.IdentitySeed = ReadIntegerOptional(xColumn.Attribute("identity_seed")).GetValueOrDefault()
            column.IdentityIncrement = ReadIntegerOptional(xColumn.Attribute("identity_increment")).GetValueOrDefault()

            Return column
        End Function

        Private Shared Sub ReadColumnList(xColumnList As XElement, columnList As ColumnList, table As Table)

            For Each item As XElement In xColumnList.Elements

                Dim name As String = ReadText(item)
                columnList.Columns.Add(table.Columns.Where(Function(x) x.Name = name).Single())
            Next
        End Sub

        Private Shared Sub ReadColumnPairs(xColumnList As XElement, key As KeyForeign, table As Table)

            For Each item As XElement In xColumnList.Elements

                Dim nameFK As String = ReadText(item.Attribute("key"))
                Dim nameRK As String = ReadText(item.Attribute("value"))

                key.Columns.Add(table.Columns.Where(Function(x) x.Name = nameFK).Single(), _
                                key.ReferencedKey.Table.Columns.Where(Function(x) x.Name = nameRK).Single())
            Next
        End Sub

        Private Shared Function ReadKeyPrimary(xPrimaryKey As XElement, table As Table) As KeyPrimary

            Dim primaryKey As New KeyPrimary(table)

            ReadElementAttributes(primaryKey, xPrimaryKey)
            primaryKey.Name &= _suffix

            primaryKey.Clustered = ReadBooleanOptional(xPrimaryKey.Attribute("clustered")).GetValueOrDefault()

            ReadColumnList(xPrimaryKey, primaryKey, table)

            _allKeys.Add(primaryKey.Name, primaryKey)
            Return primaryKey
        End Function

        Private Shared Function ReadKeyUnique(xUniqueKey As XElement, table As Table) As KeyUnique

            Dim uniqueKey As New KeyUnique(table)

            ReadElementAttributes(uniqueKey, xUniqueKey)
            uniqueKey.Name &= _suffix

            uniqueKey.Clustered = ReadBooleanOptional(xUniqueKey.Attribute("clustered")).GetValueOrDefault()

            ReadColumnList(xUniqueKey, uniqueKey, table)

            _allKeys.Add(uniqueKey.Name, uniqueKey)
            Return uniqueKey
        End Function

        Private Shared Function ReadKeyForeign(xForeignKey As XElement, table As Table) As KeyForeign

            Dim foreignKey As New KeyForeign(table)

            ReadElementAttributes(foreignKey, xForeignKey)
            foreignKey.Name &= _suffix

            foreignKey.ReferencedKey = _allKeys(ReadText(xForeignKey.Attribute("referenced_key")) & _suffix)

            ReadColumnPairs(xForeignKey, foreignKey, table)

            Return foreignKey
        End Function

        Private Shared Function ReadIndex(xIndex As XElement, table As Table) As Index

            Dim index As New Index(table)

            ReadElementAttributes(index, xIndex)
            index.Name &= _suffix

            index.Unique = ReadBooleanOptional(xIndex.Attribute("unique")).GetValueOrDefault()
            index.Clustered = ReadBooleanOptional(xIndex.Attribute("clustered")).GetValueOrDefault()

            ReadColumnList(xIndex, index, table)

            Return index
        End Function

        Private Shared Function ReadDataType(xDataType As XElement) As DataType

            Select Case ReadText(xDataType.Attributes.Where(Function(x) x.Name.LocalName = "type").Single())
                Case "cdw:dt_string"
                    Return New DTString(ReadInteger(xDataType.Attribute("size")), _
                                        ReadBooleanOptional(xDataType.Attribute("size_fixed")).GetValueOrDefault(),
                                        ReadBooleanOptional(xDataType.Attribute("unicode")).GetValueOrDefault(True))
                Case "cdw:dt_char"
                    Return New DTChar(ReadBooleanOptional(xDataType.Attribute("unicode")).GetValueOrDefault(True))
                Case "cdw:dt_int16"
                    Return New DTInt16()
                Case "cdw:dt_int32"
                    Return New DTInt32()
                Case "cdw:dt_int64"
                    Return New DTInt64()

                Case "cdw:dt_guid"
                    Return New DTGuid()

                Case "cdw:dt_decimal"
                    Return New DTDecimal(ReadIntegerOptional(xDataType.Attribute("precision")), _
                                         ReadIntegerOptional(xDataType.Attribute("scale")), _
                                         ReadBooleanOptional(xDataType.Attribute("money")).GetValueOrDefault())
                Case "cdw:dt_single"
                    Return New DTSingle()
                Case "cdw:dt_double"
                    Return New DTDouble()
                Case "cdw:dt_date"
                    Return New DTDate(ReadBooleanOptional(xDataType.Attribute("low_precision")).GetValueOrDefault())
                Case "cdw:dt_boolean"
                    Return New DTBoolean()
                Case "cdw:dt_byte"
                    Return New DTByte()
                Case "cdw:dt_bytearray"
                    Return New DTByteArray()
                Case "cdw:dt_variant"
                    Return New DTVariant()
                Case "cdw:dt_timestamp"
                    Return New DTTimestamp(ReadIntegerOptional(xDataType.Attribute("precision")))
            End Select

            Throw New ArgumentOutOfRangeException("Unsupported DB type: '" & ReadText(xDataType.Attribute("db_type")).ToString() & "'.")
        End Function

        Private Shared Function ReadVariable(xVariable As XElement, table As Table) As Variable

            Dim variable As New Variable(table)

            ReadElementAttributes(variable, xVariable)
            variable.Key = ReadText(xVariable.Attribute("key"))

            Return variable
        End Function

        Private Shared Sub ReadElementAttributes(element As Element, xElement As XElement)

            element.Name = ReadText(xElement.Attribute("name"))
        End Sub

    End Class
End Namespace