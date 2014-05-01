''' <summary>
''' Means the value of the property is assigned only once, afterwards it is never reassigned.
''' </summary>
''' <remarks>
''' The value is mostly set during object constuction or during initialization (immediately) after construction.
''' This is a BL definition, implying that the BL can rely on (and possibly cache) the value of the property. 
''' </remarks>
<AttributeUsage(AttributeTargets.Property, Inherited:=True)>
Public Class PermanentAttribute
    Inherits Attribute

End Class
