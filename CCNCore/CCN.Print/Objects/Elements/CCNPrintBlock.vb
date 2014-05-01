Imports C1.C1Preview

Public Class CCNPrintBlock
    Inherits CCNPrintContainer

#Region "Copying"

    Public Overrides Function Copy() As CCNPrintElement

        Dim result As New CCNPrintBlock(_document)

        CopyElementProperties(result)
        CopyRectangleProperties(result)
        CopyContainerProperties(result)

        Return result
    End Function

#End Region

#Region "Initialization"

    Public Sub New(ByVal document As CCNPrintDocument)
        MyBase.New(document)
    End Sub

#End Region

#Region "Rendering"

    Public Overrides Function Render() As RenderObject

        Return MyBase.Render()
    End Function

#End Region

End Class
