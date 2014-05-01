''' <summary>
''' Designates a property as part of the primary key for an entity.
''' </summary>
''' <remarks>
''' Mostly used to clarify composite primary key definitions in legacy code.
''' </remarks>
<AttributeUsage(AttributeTargets.Property, Inherited:=True)>
Public Class PrimaryKeyAttribute
    Inherits Attribute

End Class