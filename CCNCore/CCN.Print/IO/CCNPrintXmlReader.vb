Imports System.Xml
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.IO

Public Class CCNPrintXmlReader

    Private _document As CCNPrintDocument

    Public Function DocumentFromTemplate(ByVal xmlFile As String) As CCNPrintDocument

        Try
            Dim _xmlDoc As XmlDocument
            _xmlDoc = New XmlDocument()
            _xmlDoc.Load(xmlFile)

            Return ReadDocument(_xmlDoc)

        Catch ex As IO.FileNotFoundException

            MessageBox.Show("Súbor so šablonou pre tlač nebol nájdený.")
            Return Nothing

        Catch ex As Xml.XmlException

            MessageBox.Show("Šablona pre tlač obsahuje syntaktické chyby.")
            Return Nothing

        Catch ex As Exception

            MessageBox.Show("Pri načítavaní šablony pre tlač cenoviek došlo k nečakanej chybe.")
            Return Nothing
        End Try
    End Function

    Private Function ReadDocument(ByVal xmlDoc As XmlDocument) As CCNPrintDocument

        If ((xmlDoc.ChildNodes.Count < 2) OrElse xmlDoc.ChildNodes(1).Name <> "document") Then Throw New CCNPrintFileFormatException("Chyba element 'document'")

        Dim xmlDocument As XmlElement = DirectCast(xmlDoc.ChildNodes(1), XmlElement)

        If (xmlDocument.Attributes("version").Value <> "1.0") Then Throw New VersionNotFoundException("Currently only version 1.0 is supported")

        _document = New CCNPrintDocument()

        If (Not IsNothing(xmlDocument.Attributes("page_width"))) Then _document.PageWidth = xmlDocument.Attributes("page_width").Value
        If (Not IsNothing(xmlDocument.Attributes("page_height"))) Then _document.PageHeight = xmlDocument.Attributes("page_height").Value

        If (Not IsNothing(xmlDocument.Attributes("margins"))) Then

            Dim margins() As String = xmlDocument.Attributes("margins").Value.Split(" ")

            If (margins.Count = 4) Then

                _document.Margins.Left.Ammount = Val(margins(0))
                _document.Margins.Top.Ammount = Val(margins(1))
                _document.Margins.Right.Ammount = Val(margins(2))
                _document.Margins.Bottom.Ammount = Val(margins(3))
            End If
        End If

        If (Not IsNothing(xmlDocument("header"))) Then ReadHeader(_document.Header, xmlDocument("header"))
        If (Not IsNothing(xmlDocument("table"))) Then ReadTable(_document.Table, xmlDocument("table"))
        If (Not IsNothing(xmlDocument("footer"))) Then ReadFooter(_document.Footer, xmlDocument("footer"))

        For Each element As XmlElement In xmlDocument

            If (element.Name = "layer") Then _document.Layers.Add(ReadLayer(element))
        Next

        Return _document
    End Function

    Private Function ReadLayer(ByVal xmlLayer As XmlElement) As CCNPrintLayer

        Dim layer As New CCNPrintLayer()

        For Each element As XmlElement In xmlLayer

            layer.Items.Add(_document.ElementsById(element.Attributes("id").Value()))
        Next

        Return layer
    End Function

    Private Sub ReadContainerContents(ByVal container As CCNPrintContainer, ByVal xmlContainer As XmlElement)

        If (Not IsNothing(xmlContainer.Attributes("padding"))) Then

            Dim padding() As String = xmlContainer.Attributes("padding").Value.Split(" ")

            If (padding.Count = 4) Then

                container.Padding.Left.Ammount = Val(padding(0))
                container.Padding.Top.Ammount = Val(padding(1))
                container.Padding.Right.Ammount = Val(padding(2))
                container.Padding.Bottom.Ammount = Val(padding(3))
            End If
        End If

        If (Not IsNothing(xmlContainer.Attributes("stacking"))) Then container.Stacking = xmlContainer.Attributes("stacking").Value
        If (Not IsNothing(xmlContainer.Attributes("split_vertical"))) Then container.SplitVertical = xmlContainer.Attributes("split_vertical").Value

        For Each xmlElement As XmlElement In xmlContainer.ChildNodes.OfType(Of XmlElement)()

            Dim element As CCNPrintElement = ReadElement(xmlElement)
            If (Not IsNothing(element)) Then element.Parent = container
        Next
    End Sub

    Private Function ReadElement(ByVal xmlElement As XmlElement) As CCNPrintElement

        Select Case (xmlElement.Name)
            Case "line"
                Return ReadLine(xmlElement)
            Case "image"
                Return ReadImage(xmlElement)
            Case "rectangle"
                Return ReadRectangle(xmlElement)
            Case "textfield_literal"
                Return ReadTextFieldLiteral(xmlElement)
            Case "textfield_bound"
                Return ReadTextFieldBound(xmlElement)
            Case "table"
                Return ReadTable(xmlElement)
            Case "block"
                Return ReadBlock(xmlElement)
        End Select

        Return Nothing
    End Function

    Private Function ReadColorAttributes(ByVal prefix As String, ByVal xmlElement As XmlElement) As Drawing.Color

        Dim rgba() As String = xmlElement.Attributes(prefix & "color").Value.Split(" ")

        Return Drawing.Color.FromArgb(rgba(3), rgba(0), rgba(1), rgba(2))
    End Function

    Private Sub ReadElementAttributes(ByVal element As CCNPrintElement, ByVal xmlElement As XmlElement)

        If (Not IsNothing(xmlElement.Attributes("id"))) Then element.Id = xmlElement.Attributes("id").Value
        If (Not IsNothing(xmlElement.Attributes("value_id"))) Then element.ValueId = xmlElement.Attributes("value_id").Value

        If (Not IsNothing(xmlElement.Attributes("x"))) Then element.X = Val(xmlElement.Attributes("x").Value)
        If (Not IsNothing(xmlElement.Attributes("y"))) Then element.Y = Val(xmlElement.Attributes("y").Value)
        If (Not IsNothing(xmlElement.Attributes("width"))) Then element.Width = Val(xmlElement.Attributes("width").Value)
        If (Not IsNothing(xmlElement.Attributes("height"))) Then element.Height = Val(xmlElement.Attributes("height").Value)

        If (Not IsNothing(xmlElement.Attributes("positioning"))) Then element.Positioning = xmlElement.Attributes("positioning").Value

        If (Not IsNothing(xmlElement.Attributes("spacing"))) Then

            Dim spacing() As String = xmlElement.Attributes("spacing").Value.Split(" ")

            element.Spacing.Left.Ammount = Val(spacing(0))
            element.Spacing.Top.Ammount = Val(spacing(1))
            element.Spacing.Right.Ammount = Val(spacing(2))
            element.Spacing.Bottom.Ammount = Val(spacing(3))
        End If
    End Sub

    Private Function ReadLine(ByVal xmlLine As XmlElement) As CCNPrintLine

        Dim line As New CCNPrintLine(_document)

        If (Not IsNothing(xmlLine.Attributes("id"))) Then line.Id = xmlLine.Attributes("id").Value
        If (Not IsNothing(xmlLine.Attributes("positioning"))) Then line.Positioning = xmlLine.Attributes("positioning").Value

        If (Not IsNothing(xmlLine.Attributes("spacing"))) Then

            Dim spacing() As String = xmlLine.Attributes("spacing").Value.Split(" ")

            line.Spacing.Left.Ammount = Val(spacing(0))
            line.Spacing.Top.Ammount = Val(spacing(1))
            line.Spacing.Right.Ammount = Val(spacing(2))
            line.Spacing.Bottom.Ammount = Val(spacing(3))
        End If

        If (Not IsNothing(xmlLine.Attributes("x1"))) Then line.X1 = Val(xmlLine.Attributes("x1").Value)
        If (Not IsNothing(xmlLine.Attributes("y1"))) Then line.Y1 = Val(xmlLine.Attributes("y1").Value)
        If (Not IsNothing(xmlLine.Attributes("x2"))) Then line.X2 = Val(xmlLine.Attributes("x2").Value)
        If (Not IsNothing(xmlLine.Attributes("y2"))) Then line.Y2 = Val(xmlLine.Attributes("y2").Value)

        If (Not IsNothing(xmlLine.Attributes("color"))) Then line.Color = ReadColorAttributes("", xmlLine)

        If (Not IsNothing(xmlLine.Attributes("dashstyle"))) Then line.DashStyle = xmlLine.Attributes("dashstyle").Value
        If (Not IsNothing(xmlLine.Attributes("thickness"))) Then line.Thickness = Val(xmlLine.Attributes("thickness").Value)

        Return line
    End Function

    Private Sub ReadBorderAttributes(ByVal border As CCNPrintBorder, ByVal side As String, ByVal xmlBorder As XmlElement)

        If (Not IsNothing(xmlBorder.Attributes("border_" & side & "_dashstyle"))) Then border.DashStyle = xmlBorder.Attributes("border_" & side & "_dashstyle").Value
        If (Not IsNothing(xmlBorder.Attributes("border_" & side & "_thickness"))) Then border.Thickness = Val(xmlBorder.Attributes("border_" & side & "_thickness").Value)

        If (Not IsNothing(xmlBorder.Attributes("border_" & side & "_color"))) Then border.Color = ReadColorAttributes("border_" & side & "_", xmlBorder)
    End Sub

    Private Sub ReadRectangleAttributes(ByVal rectangle As CCNPrintRectangle, ByVal xmlRectangle As XmlElement)

        If (Not IsNothing(xmlRectangle.Attributes("background_color"))) Then rectangle.BackgroundColor = ReadColorAttributes("background_", xmlRectangle)

        ReadBorderAttributes(rectangle.Borders.Left, "left", xmlRectangle)
        ReadBorderAttributes(rectangle.Borders.Top, "top", xmlRectangle)
        ReadBorderAttributes(rectangle.Borders.Right, "right", xmlRectangle)
        ReadBorderAttributes(rectangle.Borders.Bottom, "bottom", xmlRectangle)
    End Sub

    Private Function ReadImage(ByVal xmlImage As XmlElement) As CCNPrintImage

        Dim rectangle As New CCNPrintImage(_document)

        ReadElementAttributes(rectangle, xmlImage)
        ReadRectangleAttributes(rectangle, xmlImage)

        rectangle.Image = Image.FromStream(New MemoryStream(Convert.FromBase64String(xmlImage.ChildNodes(0).Value)))

        Return rectangle
    End Function

    Private Function ReadRectangle(ByVal xmlRectangle As XmlElement) As CCNPrintRectangle

        Dim rectangle As New CCNPrintRectangle(_document)

        ReadElementAttributes(rectangle, xmlRectangle)
        ReadRectangleAttributes(rectangle, xmlRectangle)

        Return rectangle
    End Function

    Private Function ReadBlock(ByVal xmlBlock As XmlElement) As CCNPrintRectangle

        Dim block As New CCNPrintBlock(_document)

        ReadElementAttributes(block, xmlBlock)
        ReadRectangleAttributes(block, xmlBlock)
        ReadContainerContents(block, xmlBlock)

        Return block
    End Function

    Private Sub ReadTextFieldAttributes(ByVal textField As CCNPrintTextField, ByVal xmlTextField As XmlElement)

        If (Not IsNothing(xmlTextField.Attributes("padding"))) Then

            Dim padding() As String = xmlTextField.Attributes("padding").Value.Split(" ")

            If (padding.Count = 4) Then

                textField.Padding.Left.Ammount = Val(padding(0))
                textField.Padding.Top.Ammount = Val(padding(1))
                textField.Padding.Right.Ammount = Val(padding(2))
                textField.Padding.Bottom.Ammount = Val(padding(3))
            End If
        End If

        If (Not IsNothing(xmlTextField.Attributes("alignment_horizontal"))) Then textField.AlignmentHorizontal = xmlTextField.Attributes("alignment_horizontal").Value
        If (Not IsNothing(xmlTextField.Attributes("alignment_vertical"))) Then textField.AlignmentVertical = xmlTextField.Attributes("alignment_vertical").Value

        If (Not IsNothing(xmlTextField.Attributes("font_name"))) Then textField.FontName = xmlTextField.Attributes("font_name").Value
        If (Not IsNothing(xmlTextField.Attributes("font_size"))) Then textField.FontSize = Val(xmlTextField.Attributes("font_size").Value)
        If (Not IsNothing(xmlTextField.Attributes("font_bold"))) Then textField.FontBold = xmlTextField.Attributes("font_bold").Value
        If (Not IsNothing(xmlTextField.Attributes("font_italic"))) Then textField.FontItalic = xmlTextField.Attributes("font_italic").Value
        If (Not IsNothing(xmlTextField.Attributes("font_underline"))) Then textField.FontUnderline = xmlTextField.Attributes("font_underline").Value

        If (Not IsNothing(xmlTextField.Attributes("text_color"))) Then textField.TextColor = ReadColorAttributes("text_", xmlTextField)
    End Sub

    Private Function ReadTextFieldLiteral(ByVal xmlTextField As XmlElement) As CCNPrintTextFieldLiteral

        Dim textField As New CCNPrintTextFieldLiteral(_document)

        ReadElementAttributes(textField, xmlTextField)
        ReadRectangleAttributes(textField, xmlTextField)
        ReadTextFieldAttributes(textField, xmlTextField)

        If (xmlTextField.ChildNodes.Count) Then textField.Text = xmlTextField.ChildNodes(0).Value

        Return textField
    End Function

    Private Function ReadTextFieldBound(ByVal xmlTextField As XmlElement) As CCNPrintTextFieldBound

        Dim textField As New CCNPrintTextFieldBound(_document)

        ReadElementAttributes(textField, xmlTextField)
        ReadRectangleAttributes(textField, xmlTextField)
        ReadTextFieldAttributes(textField, xmlTextField)

        Return textField
    End Function

    Private Sub ReadHeader(ByVal header As CCNPrintHeader, ByVal xmlHeader As XmlElement)

        ReadElementAttributes(header, xmlHeader)
        ReadRectangleAttributes(header, xmlHeader)
        ReadContainerContents(header, xmlHeader)
    End Sub

    Private Sub ReadTableElementAttributes(ByVal element As CCNPrintTableElement, ByVal xmlVector As XmlElement)

        If (Not IsNothing(xmlVector.Attributes("background_color"))) Then element.BackgroundColor = ReadColorAttributes("background_", xmlVector)

        ReadBorderAttributes(element.Borders.Left, "left", xmlVector)
        ReadBorderAttributes(element.Borders.Top, "top", xmlVector)
        ReadBorderAttributes(element.Borders.Right, "right", xmlVector)
        ReadBorderAttributes(element.Borders.Bottom, "bottom", xmlVector)

        If (Not IsNothing(xmlVector.Attributes("padding_left"))) Then element.Padding.Left.Ammount = Val(xmlVector.Attributes("padding_left").Value)
        If (Not IsNothing(xmlVector.Attributes("padding_top"))) Then element.Padding.Top.Ammount = Val(xmlVector.Attributes("padding_top").Value)
        If (Not IsNothing(xmlVector.Attributes("padding_right"))) Then element.Padding.Right.Ammount = Val(xmlVector.Attributes("padding_right").Value)
        If (Not IsNothing(xmlVector.Attributes("padding_bottom"))) Then element.Padding.Bottom.Ammount = Val(xmlVector.Attributes("padding_bottom").Value)

        If (Not IsNothing(xmlVector.Attributes("alignment_horizontal"))) Then element.AlignmentHorizontal = xmlVector.Attributes("alignment_horizontal").Value
        If (Not IsNothing(xmlVector.Attributes("alignment_vertical"))) Then element.AlignmentVertical = xmlVector.Attributes("alignment_vertical").Value

        If (Not IsNothing(xmlVector.Attributes("font_name"))) Then element.FontName = xmlVector.Attributes("font_name").Value
        If (Not IsNothing(xmlVector.Attributes("font_size"))) Then element.FontSize = Val(xmlVector.Attributes("font_size").Value)
        If (Not IsNothing(xmlVector.Attributes("font_bold"))) Then element.FontBold = xmlVector.Attributes("font_bold").Value
        If (Not IsNothing(xmlVector.Attributes("font_italic"))) Then element.FontItalic = xmlVector.Attributes("font_italic").Value
        If (Not IsNothing(xmlVector.Attributes("font_underline"))) Then element.FontUnderline = xmlVector.Attributes("font_underline").Value

        If (Not IsNothing(xmlVector.Attributes("text_color"))) Then element.TextColor = ReadColorAttributes("text_", xmlVector)
    End Sub

    Private Function ReadTable(ByVal xmlTable As XmlElement) As CCNPrintTable

        Dim table As New CCNPrintTable(_document)

        ReadTable(table, xmlTable)
        Return table
    End Function

    Private Sub ReadTable(ByVal table As CCNPrintTable, ByVal xmlTable As XmlElement)

        ReadElementAttributes(table, xmlTable)
        ReadRectangleAttributes(table, xmlTable)

        If (Not IsNothing(xmlTable("columns"))) Then
            For Each xmlColumn As XmlElement In xmlTable("columns").ChildNodes

                Dim column As CCNPrintTableColumn = table.ColumnAdd(xmlColumn.Attributes("name").Value)

                ReadTableElementAttributes(column, xmlColumn)
                If (Not IsNothing(xmlColumn.Attributes("width"))) Then column.Width = Val(xmlColumn.Attributes("width").Value)
            Next

            Dim xmlHeader As XmlElement = xmlTable("header")
            If (Not IsNothing(xmlHeader)) Then

                Dim row As CCNPrintTableRow = table.Header.RowAdd()

                ReadTableElementAttributes(row, xmlHeader)
                If (Not IsNothing(xmlHeader.Attributes("height"))) Then row.Height = Val(xmlHeader.Attributes("height").Value)

                For Each xmlColumn As XmlElement In xmlHeader.ChildNodes

                    If (xmlColumn.ChildNodes.Count = 1) Then

                        row.Cells(xmlColumn.Attributes("name").Value).Text = xmlColumn.ChildNodes(0).Value
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ReadFooter(ByVal footer As CCNPrintFooter, ByVal xmlFooter As XmlElement)

        ReadElementAttributes(footer, xmlFooter)
        ReadRectangleAttributes(footer, xmlFooter)
        ReadContainerContents(footer, xmlFooter)
    End Sub
End Class
