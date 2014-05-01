Imports DBWrangler.Connectors
Imports DBWrangler.Model.Schema.Base
Imports DBWrangler.Model.Schema

Namespace Model.Filtering

    Public Class Variable
        Inherits TableElement

#Region "Initialization"

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

#End Region

#Region "Properties"

        Public ReadOnly Property Value(connector As IConnector) As String
            Get
                Return _Resolver(_Key, connector)
            End Get
        End Property

        Public Property Key As String

#End Region

#Region "Properties - static"

        Public Delegate Function Resolve(key As String, connector As IConnector) As String

        Public Shared Property Resolver As Resolve

#End Region

    End Class
End Namespace