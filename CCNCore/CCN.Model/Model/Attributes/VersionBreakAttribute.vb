''' <summary>
''' Marks a version breaking change point in the object/class hierarchy, starting at the specified version.
''' </summary>
''' <remarks>
''' Classes deriving from a clas marked with this attribute will be version-specific and should be prefixed in a form ClassNameV{Number}.
''' </remarks>
<AttributeUsage(AttributeTargets.All, AllowMultiple:=True, Inherited:=True)>
Public Class VersionBreakAttribute
    Inherits Attribute

    Protected _version As Version
    Public ReadOnly Property Version As Version
        Get
            Return _version
        End Get
    End Property

    Public Sub New(version As String)

        _version = New Version(version)
    End Sub
End Class
