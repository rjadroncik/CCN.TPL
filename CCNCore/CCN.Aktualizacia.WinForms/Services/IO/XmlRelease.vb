Imports CCN.Services
Imports CCN.Core.VB
Imports CCN.Aktualizacia.Model
Imports System.Xml.Schema
Imports System.Xml.Linq
Imports System.IO
Imports CCN.Model

Public Class XmlRelease
    Inherits XmlService

    Public Shared Function Read(subor As String) As Release

        Dim xDoc As XDocument = XDocument.Load(subor)

        Dim xmlSchema As XmlSchema = xmlSchema.Read(New StringReader(Global.CCN.Aktualizacia.Model.My.Resources.Release), AddressOf ValidationEventHandler)

        Dim xmlSchemaSet As New XmlSchemaSet()
        xmlSchemaSet.Add(xmlSchema)

        xDoc.Validate(xmlSchemaSet, AddressOf ValidationEventHandler)

        Return ReadRelease(xDoc.Root)
    End Function

    Protected Shared Sub ValidationEventHandler(sender As Object, e As ValidationEventArgs)

        If (e.Severity = XmlSeverityType.Error) Then Throw e.Exception
    End Sub

    Protected Shared Function ReadRelease(release As XElement) As Release

        Dim result As New Release()
        With result

            .Komponent = ReadEnum(Of ComponentType)(release.Attribute("component"))
            .Version = ReadVersion(release.Attribute("version"))
            .MainExe = ReadText(release.Attribute("main_exe"))

            With release.Element(release.Name.Namespace + "entities")

                result.EntitiesAll = ReadBoolean(.Attribute("all"))

                For Each entity As XElement In .Elements()

                    result.Entities.Add(entity.Value)
                Next
            End With

            For Each operations In release.Elements(release.Name.Namespace + "operations")
                With operations
                    Select Case (ReadEnum(Of ComponentType)(.Attribute("target")))

                        Case ComponentType.Application
                            ReadOperations(.Elements(), result.OperationsAplikacia)
                        Case ComponentType.Updater
                            ReadOperations(.Elements(), result.OperationsUpdater)
                    End Select
                End With
            Next
        End With
        Return result
    End Function

    Protected Shared Sub ReadOperations(operations As IEnumerable(Of XElement), result As IList(Of Operation))

        With result
            For Each operation As XElement In operations

                Select Case (Converting.String2Enum(Of OperationType)(Formatting.CapitalizeStart(operation.Name.LocalName)))

                    Case OperationType.Program
                        .Add(ReadOperationProgram(operation))
                    Case OperationType.File
                        .Add(ReadOperationFile(operation))
                    Case OperationType.Task
                        .Add(ReadOperationTask(operation))
                End Select
            Next
        End With
    End Sub

    Protected Shared Function ReadOperationProgram(operation As XElement) As OperationProgram

        Dim result As New OperationProgram()
        With result

            .Action = ReadEnum(Of ProgramAction)(operation.Attribute("action"))
            .Path = ReadTextOptional(operation.Attribute("path"))
            .Process = ReadTextOptional(operation.Attribute("process"))
            If (operation.Attribute("timeout") IsNot Nothing) Then .Timeout = ReadInteger(operation.Attribute("timeout"))
            .Arguments = ReadTextOptional(operation.Attribute("arguments"))
        End With
        Return result
    End Function

    Protected Shared Function ReadOperationFile(operation As XElement) As OperationFile

        Dim result As New OperationFile()
        With result

            .Action = ReadEnum(Of FileAction)(operation.Attribute("action"))
            .Path = ReadText(operation.Attribute("path"))
            If (operation.Attribute("version") IsNot Nothing) Then .Version = ReadVersion(operation.Attribute("version"))

        End With
        Return result
    End Function

    Protected Shared Function ReadOperationTask(operation As XElement) As OperationTask

        Dim result As New OperationTask()
        With result

            .Language = ReadEnum(Of CodeLanguage)(operation.Attribute("language"))

            .Code = ReadText(operation.Element(operation.Name.Namespace + "code"), 1024 * 1024)

            Dim xReferences = operation.Element(operation.Name.Namespace + "references")
            If (xReferences IsNot Nothing) Then

                For Each reference As XElement In xReferences.Elements(operation.Name.Namespace + "reference")

                    .References.Add(ReadText(reference))
                Next
            End If

        End With
        Return result
    End Function
End Class
