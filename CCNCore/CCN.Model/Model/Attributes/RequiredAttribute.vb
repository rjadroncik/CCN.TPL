''' <summary>
''' Marks a property of an entity as required.
''' </summary>
''' <remarks>
''' Used only on reference types to specify that the property should never be set to [null].
''' Nullable types must never have this attribute set.
''' This attribute is exclusive in usage with <seealso cref="OptionalAttribute" />.
''' </remarks>
<AttributeUsage(AttributeTargets.Property, Inherited:=True)>
Public Class RequiredAttribute
    Inherits Attribute

End Class