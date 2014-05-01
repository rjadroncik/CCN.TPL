Imports System.Runtime.Serialization

<DataContract(IsReference:=True)>
Public MustInherit Class Objekt
    Inherits NHEntita

#Region "Properties"

    <Permanent(), DataMember()>
    Public Overridable Property Created As DateTime
    <Permanent(), DataMember()>
    Public Overridable Property CreatedBy As IPouzivatel

    <DataMember()>
    Public Overridable Property Changed As DateTime?
    <DataMember()>
    Public Overridable Property ChangedBy As IPouzivatel

#End Region

End Class
