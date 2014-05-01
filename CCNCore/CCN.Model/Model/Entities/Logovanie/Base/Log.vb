Imports System.Runtime.Serialization

<DataContract(IsReference:=True)>
Public MustInherit Class Log

    <DataMember(), Permanent()>
    Public Overridable Property Id As Integer

    <DataMember(), Permanent()>
    Public Overridable Property Aplikacia As String
    <DataMember(), Permanent()>
    Public Overridable Property Verzia As Version

    <DataMember(), Permanent()>
    Public Overridable Property Datum As DateTime
    <DataMember(), Permanent()>
    Public Overridable Property Entita As String

    <DataMember(), Permanent()>
    Public Overridable Property Pouzivatel As IPouzivatel

End Class
