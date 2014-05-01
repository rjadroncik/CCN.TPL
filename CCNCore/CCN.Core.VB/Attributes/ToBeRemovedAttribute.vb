''' <summary>
''' Flags a piece of code as unused and to be removed in time.
''' </summary>
''' <remarks>
''' This is the same as Obsolete but without the compiler warnings. Used when we don't know when something will be removed.
''' </remarks>
<AttributeUsage(AttributeTargets.All, Inherited:=True)>
Public Class ToBeRemovedAttribute
    Inherits Attribute
End Class
