Imports System.Xml
Imports C1.C1Preview
Imports System.IO
Imports System.Drawing.Imaging

Public Class CCNPrintXmlWriter

    Private _writer As XmlWriter

    Public Sub TemplateFromDocument(ByVal document As CCNPrintDocument, ByVal xmlFile As String)

        Dim settings As New XmlWriterSettings()
        settings.Indent = True
        settings.NewLineOnAttributes = True

        _writer = XmlTextWriter.Create(xmlFile, settings)

        WriteDocument(document)

        _writer.Close()
    End Sub

    Private Sub WriteDocument(ByVal document As CCNPrintDocument)

        _writer.WriteStartDocument()
        _writer.WriteStartElement("document", "ccn.print")

        _writer.WriteAttributeString("version", "1.0")

        If (document.PageWidth <> 210) Then _writer.WriteAttributeString("page_width", document.PageWidth)
        If (document.PageHeight <> 210) Then _writer.WriteAttributeString("page_height", document.PageHeight)

        If (document.Margins.Left.Changed OrElse document.Margins.Top.Changed OrElse _
            document.Margins.Right.Changed OrElse document.Margins.Bottom.Changed) Then

            _writer.WriteAttributeString("margins", document.Margins.Left.Ammount & " " & document.Margins.Top.Ammount & " " & _
                                         document.Margins.Right.Ammount & " " & document.Margins.Bottom.Ammount)
        End If

        WriteHeader(document.Header)
        WriteTable(document.Table)
        WriteFooter(document.Footer)

        For Each layer As CCNPrintLayer In document.Layers
            WriteLayer(layer)
        Next

        _writer.WriteEndElement()
        _writer.WriteEndDocument()
    End Sub

    Private Sub WriteLayer(ByVal layer As CCNPrintLayer)

        _writer.WriteStartElement("layer")

        For Each element As CCNPrintElement In layer.Items

            _writer.WriteStartElement("element_ref")
            _writer.WriteAttributeString("id", element.Id)
            _writer.WriteEndElement()
        Next

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteContainerContents(ByVal container As CCNPrintContainer)

        If (container.Padding.Left.Changed OrElse container.Padding.Top.Changed OrElse _
            container.Padding.Right.Changed OrElse container.Padding.Bottom.Changed) Then

            _writer.WriteAttributeString("padding", container.Padding.Left.Ammount & " " & container.Padding.Top.Ammount & " " & _
                                         container.Padding.Right.Ammount & " " & container.Padding.Bottom.Ammount)
        End If

        If (container.Stacking <> StackingRulesEnum.BlockTopToBottom) Then _writer.WriteAttributeString("stacking", container.Stacking)
        If (container.SplitVertical <> SplitBehaviorEnum.SplitIfNeeded) Then _writer.WriteAttributeString("split_vertical", container.SplitVertical)

        For Each element As CCNPrintElement In container.Children

            WriteElement(element)
        Next
    End Sub

    Private Sub WriteElement(ByVal element As CCNPrintElement)

        If (TypeOf (element) Is CCNPrintLine) Then Me.WriteLine(DirectCast(element, CCNPrintLine)) : Return
        If (TypeOf (element) Is CCNPrintTextFieldLiteral) Then Me.WriteTextFieldLiteral(DirectCast(element, CCNPrintTextFieldLiteral)) : Return
        If (TypeOf (element) Is CCNPrintTextFieldBound) Then Me.WriteTextFieldBound(DirectCast(element, CCNPrintTextFieldBound)) : Return
        If (TypeOf (element) Is CCNPrintTable) Then Me.WriteTable(DirectCast(element, CCNPrintTable)) : Return
        If (TypeOf (element) Is CCNPrintBlock) Then Me.WriteBlock(DirectCast(element, CCNPrintBlock)) : Return
        If (TypeOf (element) Is CCNPrintImage) Then Me.WriteImage(DirectCast(element, CCNPrintImage)) : Return
        If (TypeOf (element) Is CCNPrintRectangle) Then Me.WriteRectangle(DirectCast(element, CCNPrintRectangle)) : Return
    End Sub

    Private Sub WriteColorAttributes(ByVal color As Drawing.Color, ByVal prefix As String)

        _writer.WriteAttributeString(prefix & "color", color.R & " " & color.G & " " & color.B & " " & color.A)
    End Sub

    Private Sub WriteElementAttributes(ByVal element As CCNPrintElement)

        If (element.Id.Trim.Length > 0) Then _writer.WriteAttributeString("id", element.Id)
        If (element.ValueId.Trim.Length > 0) Then _writer.WriteAttributeString("value_id", element.ValueId)

        If (element.Positioning <> CCNPrintElement.Positionings.Flow) Then

            If (element.X <> 0.0) Then _writer.WriteAttributeString("x", Str(element.X).TrimStart)
            If (element.Y <> 0.0) Then _writer.WriteAttributeString("y", Str(element.Y).TrimStart)
        End If

        If (element.Width <> 0.0) Then _writer.WriteAttributeString("width", Str(element.Width).TrimStart)
        If (element.Height <> 0.0) Then _writer.WriteAttributeString("height", Str(element.Height).TrimStart)

        If (element.Positioning <> CCNPrintElement.Positionings.Flow) Then _writer.WriteAttributeString("positioning", element.Positioning)

        If (element.Spacing.Left.Changed OrElse element.Spacing.Top.Changed OrElse _
            element.Spacing.Right.Changed OrElse element.Spacing.Bottom.Changed) Then

            _writer.WriteAttributeString("spacing", element.Spacing.Left.Ammount & " " & element.Spacing.Top.Ammount & " " & _
                                         element.Spacing.Right.Ammount & " " & element.Spacing.Bottom.Ammount)
        End If

    End Sub

    Private Sub WriteLine(ByVal line As CCNPrintLine)

        _writer.WriteStartElement("line")

        If (line.Id.Trim.Length > 0) Then _writer.WriteAttributeString("id", line.Id)

        If (line.Positioning <> CCNPrintElement.Positionings.Flow) Then _writer.WriteAttributeString("positioning", line.Positioning)

        If (line.Spacing.Left.Changed OrElse line.Spacing.Top.Changed OrElse _
            line.Spacing.Right.Changed OrElse line.Spacing.Bottom.Changed) Then

            _writer.WriteAttributeString("spacing", line.Spacing.Left.Ammount & " " & line.Spacing.Top.Ammount & " " & _
                                         line.Spacing.Right.Ammount & " " & line.Spacing.Bottom.Ammount)
        End If

        _writer.WriteAttributeString("x1", Str(line.X1).TrimStart)
        _writer.WriteAttributeString("y1", Str(line.Y1).TrimStart)
        _writer.WriteAttributeString("x2", Str(line.X2).TrimStart)
        _writer.WriteAttributeString("y2", Str(line.Y2).TrimStart)

        If (line.DashStyle <> Drawing.Drawing2D.DashStyle.Solid) Then _writer.WriteAttributeString("dashstyle", line.DashStyle)
        If (line.Thickness <> 0.5) Then _writer.WriteAttributeString("thickness", Str(line.Thickness).TrimStart)

        If (line.Color <> Drawing.Color.Black) Then WriteColorAttributes(line.Color, "")

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteBorderAttributes(ByVal border As CCNPrintBorder, ByVal side As String)

        If (border.DashStyle <> Drawing.Drawing2D.DashStyle.Solid) Then _writer.WriteAttributeString("border_" & side & "_dashstyle", border.DashStyle)

        _writer.WriteAttributeString("border_" & side & "_thickness", Str(border.Thickness).TrimStart)

        If (border.Color <> Drawing.Color.Black) Then WriteColorAttributes(border.Color, "border_" & side & "_")
    End Sub

    Private Sub WriteRectangleAttributes(ByVal rectangle As CCNPrintRectangle)

        If (rectangle.BackgroundColor <> Drawing.Color.Transparent) Then WriteColorAttributes(rectangle.BackgroundColor, "background_")

        If (rectangle.Borders.Left.Changed) Then WriteBorderAttributes(rectangle.Borders.Left, "left")
        If (rectangle.Borders.Left.Changed) Then WriteBorderAttributes(rectangle.Borders.Top, "top")
        If (rectangle.Borders.Left.Changed) Then WriteBorderAttributes(rectangle.Borders.Right, "right")
        If (rectangle.Borders.Left.Changed) Then WriteBorderAttributes(rectangle.Borders.Bottom, "bottom")
    End Sub

    Private Sub WriteImage(ByVal image As CCNPrintImage)

        _writer.WriteStartElement("image")

        WriteElementAttributes(image)
        WriteRectangleAttributes(image)

        If (Not IsNothing(image.Image)) Then

            Dim stream As New MemoryStream()
            image.Image.Save(stream, image.Image.RawFormat)

            _writer.WriteValue(Convert.ToBase64String(stream.GetBuffer(), 0, stream.Length))
        End If

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteRectangle(ByVal rectangle As CCNPrintRectangle)

        _writer.WriteStartElement("rectangle")

        WriteElementAttributes(rectangle)
        WriteRectangleAttributes(rectangle)

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteBlock(ByVal block As CCNPrintBlock)

        _writer.WriteStartElement("block")

        WriteElementAttributes(block)
        WriteRectangleAttributes(block)
        WriteContainerContents(block)

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteTextFieldAttributes(ByVal textField As CCNPrintTextField)

        If (textField.Padding.Left.Changed OrElse textField.Padding.Top.Changed OrElse _
            textField.Padding.Right.Changed OrElse textField.Padding.Bottom.Changed) Then

            _writer.WriteAttributeString("padding", textField.Padding.Left.Ammount & " " & textField.Padding.Top.Ammount & " " & _
                                         textField.Padding.Right.Ammount & " " & textField.Padding.Bottom.Ammount)
        End If

        If (textField.AlignmentHorizontal <> AlignHorzEnum.Left) Then _writer.WriteAttributeString("alignment_horizontal", textField.AlignmentHorizontal)
        If (textField.AlignmentVertical <> AlignVertEnum.Top) Then _writer.WriteAttributeString("alignment_vertical", textField.AlignmentVertical)

        If (textField.FontName <> "Arial") Then _writer.WriteAttributeString("font_name", textField.FontName)
        If (textField.FontSize <> 4.23) Then _writer.WriteAttributeString("font_size", textField.FontSize)
        If (textField.FontBold <> False) Then _writer.WriteAttributeString("font_bold", textField.FontBold)
        If (textField.FontItalic <> False) Then _writer.WriteAttributeString("font_italic", textField.FontItalic)
        If (textField.FontUnderline <> False) Then _writer.WriteAttributeString("font_underline", textField.FontUnderline)

        If (textField.TextColor <> Drawing.Color.Black) Then WriteColorAttributes(textField.TextColor, "text_")
    End Sub

    Private Sub WriteTextFieldLiteral(ByVal textField As CCNPrintTextFieldLiteral)

        _writer.WriteStartElement("textfield_literal")

        WriteElementAttributes(textField)
        WriteRectangleAttributes(textField)
        WriteTextFieldAttributes(textField)

        If (Not IsNothing(textField.Text)) Then _writer.WriteValue(textField.Text)

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteTextFieldBound(ByVal textField As CCNPrintTextFieldBound)

        _writer.WriteStartElement("textfield_bound")

        WriteElementAttributes(textField)
        WriteRectangleAttributes(textField)
        WriteTextFieldAttributes(textField)

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteHeader(ByVal header As CCNPrintHeader)

        _writer.WriteStartElement("header")

        WriteElementAttributes(header)
        WriteRectangleAttributes(header)
        WriteContainerContents(header)

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteTableElementAttributes(ByVal element As CCNPrintTableElement)

        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.BackgroundColor)) Then WriteColorAttributes(element.BackgroundColor, "background_")

        If (element.Borders.Left.Changed) Then WriteBorderAttributes(element.Borders.Left, "left")
        If (element.Borders.Top.Changed) Then WriteBorderAttributes(element.Borders.Top, "top")
        If (element.Borders.Right.Changed) Then WriteBorderAttributes(element.Borders.Right, "right")
        If (element.Borders.Bottom.Changed) Then WriteBorderAttributes(element.Borders.Bottom, "bottom")

        If (element.Padding.Left.Changed) Then _writer.WriteAttributeString("padding_left", element.Padding.Left.Ammount)
        If (element.Padding.Top.Changed) Then _writer.WriteAttributeString("padding_top", element.Padding.Top.Ammount)
        If (element.Padding.Right.Changed) Then _writer.WriteAttributeString("padding_right", element.Padding.Right.Ammount)
        If (element.Padding.Bottom.Changed) Then _writer.WriteAttributeString("padding_bottom", element.Padding.Bottom.Ammount)

        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.AlignmentHorizontal)) Then _writer.WriteAttributeString("alignment_horizontal", element.AlignmentHorizontal)
        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.AlignmentVertical)) Then _writer.WriteAttributeString("alignment_vertical", element.AlignmentVertical)

        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.FontName)) Then _writer.WriteAttributeString("font_name", element.FontName)
        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.FontSize)) Then _writer.WriteAttributeString("font_size", element.FontSize)
        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.FontBold)) Then _writer.WriteAttributeString("font_bold", element.FontBold)
        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.FontItalic)) Then _writer.WriteAttributeString("font_italic", element.FontItalic)
        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.FontUnderline)) Then _writer.WriteAttributeString("font_underline", element.FontUnderline)

        If (element.PropertiesChanged.Contains(CCNPrintTableElement.Properties.TextColor)) Then WriteColorAttributes(element.TextColor, "text_")
    End Sub

    Private Sub WriteTable(ByVal table As CCNPrintTable)

        _writer.WriteStartElement("table")

        WriteElementAttributes(table)
        WriteRectangleAttributes(table)

        _writer.WriteStartElement("columns")
        For i As Integer = 0 To table.ColumnCount - 1

            _writer.WriteStartElement("column")
            _writer.WriteAttributeString("name", table.Columns(i).Name)

            WriteTableElementAttributes(table.Columns(i))
            If (table.Columns(i).PropertiesChanged.Contains(CCNPrintTableVector.Properties.Width)) Then _writer.WriteAttributeString("width", Str(table.Columns(i).Width))

            _writer.WriteEndElement()
        Next
        _writer.WriteEndElement()

        If (table.Header.RowsCount > 0) Then
            _writer.WriteStartElement("header")

            Dim row As CCNPrintTableRow = table.Header.Rows(0)

            WriteTableElementAttributes(row)
            If (row.PropertiesChanged.Contains(CCNPrintTableVector.Properties.Height)) Then _writer.WriteAttributeString("height", Str(row.Height))

            For i As Integer = 0 To row.CellCount - 1

                Dim cell As CCNPrintTableCell = row.Cells(i)

                _writer.WriteStartElement("column")
                _writer.WriteAttributeString("name", table.Columns(i).Name)
                _writer.WriteValue(cell.Text)
                _writer.WriteEndElement()
            Next
            _writer.WriteEndElement()
        End If

        _writer.WriteEndElement()
    End Sub

    Private Sub WriteFooter(ByVal footer As CCNPrintFooter)

        _writer.WriteStartElement("footer")

        WriteElementAttributes(footer)
        WriteRectangleAttributes(footer)
        WriteContainerContents(footer)

        _writer.WriteEndElement()
    End Sub
End Class
