''' <summary>
''' Marks a property as optional, meaning it can be [null].
''' </summary>
''' <remarks>
''' Used only on reference types to specify that the property can be set to [null].
''' Nullable types must never have this attribute set.
''' This attribute is exclusive in usage with <seealso cref="RequiredAttribute" />.
''' </remarks>
<AttributeUsage(AttributeTargets.Property, Inherited:=True)>
Public Class OptionalAttribute
    Inherits Attribute

End Class