Imports CCN.Core.VB
Imports CCN.Model
Imports CCN.Services
Imports System.Reflection
Imports System.IO

Public MustInherit Class FileTestFixtureBase

    Protected Class ResourceFile

        Private _resourceName As String
        Public ReadOnly Property ResourceName As String
            Get
                Return _resourceName
            End Get
        End Property

        Private _filePath As String
        Public ReadOnly Property FilePath As String
            Get
                Return _filePath
            End Get
        End Property

        Public Sub New(resourceName As String, filePath As String)

            _resourceName = resourceName
            _filePath = filePath
        End Sub

    End Class

    Private _resourceFiles As IEnumerable(Of ResourceFile)

    Protected Sub TestSetUp(assembly As Assembly, ParamArray resourceFiles As ResourceFile())

        _resourceFiles = resourceFiles

        For Each resourceFile In _resourceFiles

            Using stream = assembly.GetManifestResourceStream(assembly.GetName().Name & "." & resourceFile.ResourceName)

                Using reader = New StreamReader(stream)

                    Dim fullPath = Path.GetDirectoryName(assembly.Location) & Path.DirectorySeparatorChar & resourceFile.FilePath

                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath))

                    Using writer = File.CreateText(fullPath)

                        writer.Write(reader.ReadToEnd())
                    End Using
                End Using
            End Using
        Next
    End Sub

    Protected Sub TestTearDown(assembly As Assembly)

        For Each resourceFile In _resourceFiles

            Dim fullPath = Path.GetDirectoryName(assembly.Location) & Path.DirectorySeparatorChar & resourceFile.FilePath

            File.Delete(fullPath)
        Next
    End Sub

End Class
