
Namespace Services.IO

    Public MustInherit Class XmlService

#Region "Nacitavanie atomickych hodnot - elementy"

        Protected Shared Function ReadText(element As XElement, Optional maxLength As Integer = 255) As String

            Dim text As String = element.Value

            Return If(text.Length > maxLength, text.Substring(0, maxLength), text)
        End Function

        Protected Shared Function ReadDecimal(element As XElement) As Decimal

            Return Decimal.Parse(ReadText(element), Globalization.NumberFormatInfo.InvariantInfo)
        End Function

        Protected Shared Function ReadDouble(element As XElement) As Double

            Return Double.Parse(ReadText(element), Globalization.NumberFormatInfo.InvariantInfo)
        End Function

        Protected Shared Function ReadInteger(element As XElement) As Integer

            Return Integer.Parse(ReadText(element), Globalization.NumberFormatInfo.InvariantInfo)
        End Function

        Protected Shared Function ReadBoolean(element As XElement) As Boolean

            Return Converting.FromXmlBoolean(ReadText(element))
        End Function

        Protected Shared Function ReadDatum(element As XElement) As Date

            Return Date.Parse(ReadText(element))
        End Function

        Protected Shared Function ReadVersion(element As XElement) As Version

            Return Converting.String2Version(ReadText(element))
        End Function

        Protected Shared Function ReadEnum(Of T As {Structure, IConvertible})(element As XElement) As T

            Return Converting.String2Enum(Of T)(ReadText(element))
        End Function

#End Region

#Region "Nacitavanie atomickych hodnot - elementy - optional"

        Protected Shared Function ReadTextOptional(element As XElement, Optional maxLength As Integer = 255) As String

            If (element Is Nothing) Then Return Nothing

            Return ReadText(element, maxLength)
        End Function

        Protected Shared Function ReadDecimalOptional(element As XElement) As Decimal?

            If (element Is Nothing) Then Return Nothing

            Return ReadDecimal(element)
        End Function

        Protected Shared Function ReadDoubleOptional(element As XElement) As Double?

            If (element Is Nothing) Then Return Nothing

            Return ReadDouble(element)
        End Function

        Protected Shared Function ReadIntegerOptional(element As XElement) As Integer?

            If (element Is Nothing) Then Return Nothing

            Return ReadInteger(element)
        End Function

        Protected Shared Function ReadBooleanOptional(element As XElement) As Boolean?

            If (element Is Nothing) Then Return Nothing

            Return ReadBoolean(element)
        End Function

        Protected Shared Function ReadDatumOptional(element As XElement) As Date?

            If (element Is Nothing) Then Return Nothing

            Return ReadDatum(element)
        End Function

        Protected Shared Function ReadVersionOptional(element As XElement) As Version

            If (element Is Nothing) Then Return Nothing

            Return ReadVersion(element)
        End Function

        Protected Shared Function ReadEnumOptional(Of T As {Structure, IConvertible})(element As XElement) As T?

            If (element Is Nothing) Then Return Nothing

            Return ReadEnum(Of T)(element)
        End Function

#End Region

#Region "Nacitavanie atomickych hodnot - atributy"

        Protected Shared Function ReadText(attribute As XAttribute, Optional maxLength As Integer = 255) As String

            Dim text As String = attribute.Value

            Return If(text.Length > maxLength, text.Substring(0, maxLength), text)
        End Function

        Protected Shared Function ReadDecimal(attribute As XAttribute) As Decimal

            Return Decimal.Parse(ReadText(attribute), Globalization.NumberFormatInfo.InvariantInfo)
        End Function

        Protected Shared Function ReadDouble(attribute As XAttribute) As Double

            Return Double.Parse(ReadText(attribute), Globalization.NumberFormatInfo.InvariantInfo)
        End Function

        Protected Shared Function ReadInteger(attribute As XAttribute) As Integer

            Return Integer.Parse(ReadText(attribute), Globalization.NumberFormatInfo.InvariantInfo)
        End Function

        Protected Shared Function ReadBoolean(attribute As XAttribute) As Boolean

            Return Converting.FromXmlBoolean(ReadText(attribute))
        End Function

        Protected Shared Function ReadDatum(attribute As XAttribute) As Date

            Return Date.Parse(ReadText(attribute))
        End Function

        Protected Shared Function ReadVersion(attribute As XAttribute) As Version

            Return Converting.String2Version(ReadText(attribute))
        End Function

        Protected Shared Function ReadEnum(Of T As {Structure, IConvertible})(attribute As XAttribute) As T

            Return Converting.String2Enum(Of T)(ReadText(attribute))
        End Function

#End Region

#Region "Nacitavanie atomickych hodnot - atributy - optional"

        Protected Shared Function ReadTextOptional(attribute As XAttribute, Optional maxLength As Integer = 255) As String

            If (attribute Is Nothing) Then Return Nothing

            Return ReadText(attribute, maxLength)
        End Function

        Protected Shared Function ReadDecimalOptional(attribute As XAttribute) As Decimal?

            If (attribute Is Nothing) Then Return Nothing

            Return ReadDecimal(attribute)
        End Function

        Protected Shared Function ReadDoubleOptional(attribute As XAttribute) As Double?


            If (attribute Is Nothing) Then Return Nothing

            Return ReadDouble(attribute)
        End Function

        Protected Shared Function ReadIntegerOptional(attribute As XAttribute) As Integer?

            If (attribute Is Nothing) Then Return Nothing

            Return ReadInteger(attribute)
        End Function

        Protected Shared Function ReadBooleanOptional(attribute As XAttribute) As Boolean?

            If (attribute Is Nothing) Then Return Nothing

            Return ReadBoolean(attribute)
        End Function

        Protected Shared Function ReadDatumOptional(attribute As XAttribute) As Date?

            If (attribute Is Nothing) Then Return Nothing

            Return ReadDatum(attribute)
        End Function

        Protected Shared Function ReadVersionOptional(attribute As XAttribute) As Version

            If (attribute Is Nothing) Then Return Nothing

            Return ReadVersion(attribute)
        End Function

        Protected Shared Function ReadEnumOptional(Of T As {Structure, IConvertible})(attribute As XAttribute) As T?

            If (attribute Is Nothing) Then Return Nothing

            Return ReadEnum(Of T)(attribute)
        End Function

#End Region
    End Class
End Namespace