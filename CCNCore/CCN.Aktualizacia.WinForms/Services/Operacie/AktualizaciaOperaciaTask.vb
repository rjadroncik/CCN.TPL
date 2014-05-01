Imports System.CodeDom
Imports Microsoft.CSharp
Imports System.IO
Imports System.Environment
Imports System.CodeDom.Compiler
Imports CCN.Services
Imports CCN.Core.VB
Imports System.Reflection
Imports CCN.Aktualizacia.Model
Imports CCN.Model
Imports Microsoft.VisualBasic

Public Class AktualizaciaOperaciaTask

#Region "BL"

    Public Shared Function Perform(operacia As OperationTask, release As Release, logAktualizacia As LogAktualizacia) As OperationResult

        Dim kod As Assembly = CompileCode(operacia)
        If (kod IsNot Nothing) Then

            For Each typ As Type In kod.GetExportedTypes().Where(Function(x) x.GetInterfaces().Contains(GetType(ITask)))

                Dim task As ITask = DirectCast(Activator.CreateInstance(typ), ITask)
                task.Execute()
            Next
        End If

        Return New OperationResult(True)
    End Function

    Protected Shared Function CodeProvider(operacia As OperationTask) As CodeDomProvider

        Select Case operacia.Language

            Case CodeLanguage.CSharp
                Return New CSharpCodeProvider()

            Case CodeLanguage.VB
                Return New VBCodeProvider()

            Case Else
                Throw New FatalServiceException(String.Format("Nepodporovaný skriptovací jazyk: {0}.", operacia.Language))
        End Select
    End Function

    Public Shared Function CompileCode(operacia As OperationTask) As Assembly

        Dim params As New CompilerParameters()
        With params
            .GenerateInMemory = True

            .TreatWarningsAsErrors = False
            .GenerateExecutable = False
            .CompilerOptions = "/optimize"

            For Each reference As String In operacia.References

                .ReferencedAssemblies.Add(reference)
            Next
        End With

        Using provider As CodeDomProvider = CodeProvider(operacia)

            Dim compile As CompilerResults = provider.CompileAssemblyFromSource(params, operacia.Code)

            If (compile.Errors.HasErrors) Then Throw New FatalServiceException(Converting.Values2String(compile.Errors.OfType(Of Object)))

            Return compile.CompiledAssembly
        End Using
    End Function

#End Region

End Class
