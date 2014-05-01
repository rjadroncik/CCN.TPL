
<AttributeUsage(AttributeTargets.All, Inherited:=True, allowMultiple:=True)>
Public Class KBAttribute
    Inherits Attribute

    Private _id As String
    Public ReadOnly Property Id As String
        Get
            Return _id
        End Get
    End Property

    Public Sub New(id As String)
        _id = id
    End Sub
End Class
