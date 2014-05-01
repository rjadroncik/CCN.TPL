Imports System.Runtime.Serialization
Imports CCN.Core.VB

Public Interface IPravoObjektove
    Inherits IIdentifiable(Of Integer)

#Region "Properties"

    Property Nazov As String

    Property ObjektTyp As ObjektTyp

    Property Povolene As IList(Of Cinnost)

#End Region

End Interface
