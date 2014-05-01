''' <summary>
''' Means the value of the property is used only in the BL or UI layer and is never persisted into the DB.
''' </summary>
''' <remarks>
''' These properties tend to contain additional information usefull to the user or the application 
''' during execution on a per user-session(logon) basis.
''' Can be used with <seealso cref="PermanentAttribute" /> aswell as <seealso cref="DerivedAttribute" />.
''' </remarks>
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Class, Inherited:=True)>
Public Class TransientAttribute
    Inherits Attribute

End Class