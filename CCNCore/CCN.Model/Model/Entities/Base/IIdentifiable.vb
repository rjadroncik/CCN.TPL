Imports System.Runtime.Serialization

Public Interface IIdentifiable(Of T)

    <Permanent(), DataMember()>
    Property Id As T

    ReadOnly Property IsNew As Boolean

End Interface
