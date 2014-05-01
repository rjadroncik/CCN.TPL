''' <summary>
''' Derived -> means the value of the property is dependent on other poperties on the entity an possibly on other entities.
''' </summary>
''' <remarks>The value of such a property can be changed, but should only be assigned a value derived based on some BL (possibly encapsulated in an appropriate service).</remarks>
<AttributeUsage(AttributeTargets.Property, Inherited:=True)>
Public Class DerivedAttribute
    Inherits Attribute

End Class
