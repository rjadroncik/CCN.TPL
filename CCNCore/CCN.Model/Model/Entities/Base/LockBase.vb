Public MustInherit Class LockBase(Of T)
    <Permanent()>
    Public Overridable Property Id As T

    <Permanent()>
    Public Overridable Property Objekt As IIdentifiable(Of T)
    <Permanent()>
    Public Overridable Property Pouzivatel As IPouzivatel

    <Permanent()>
    Public Overridable Property Zdroj As IIdentifiable(Of T)

    <Permanent()>
    Public Overridable Property Created As DateTime
    <Permanent()>
    Public Overridable Property Expires As DateTime

End Class
