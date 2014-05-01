Imports C1.C1Preview
Imports System.Drawing

Public Class CCNPrintImage
    Inherits CCNPrintRectangle

#Region "Properties"

    Public Property Image As Image

#End Region

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintImage(_document)

        CopyElementProperties(result)

        result._Image = DirectCast(_Image.Clone(), Image)

        Return result
    End Function

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As C1.C1Preview.RenderObject

        Dim image As New RenderImage(_image, New ImageAlign(ImageAlignHorzEnum.Center, ImageAlignVertEnum.Center, True, True, False, False, False))
        _c1Object = image

        ApplyRectangleProperties(image)

        If (Not IsNothing(_parent)) Then

            _parent.C1Object.Children.Add(image)
        Else
            _document.C1Document.Body.Children.Add(image)
        End If

        Return image
    End Function

#End Region

End Class
