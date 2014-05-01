Public NotInheritable Class Servisy

#Region "Properties"

    Private Shared _pouzivatelia As IPouzivatelia
    Public Shared ReadOnly Property Pouzivatelia As IPouzivatelia
        Get
            Return _pouzivatelia
        End Get
    End Property

    Private Shared _role As IRole
    Public Shared ReadOnly Property Role As IRole
        Get
            Return _role
        End Get
    End Property

    Private Shared _prava As IPrava
    Public Shared ReadOnly Property Prava As IPrava
        Get
            Return _prava
        End Get
    End Property

    Private Shared _pravaObjektove As IPravaObjektove
    Public Shared ReadOnly Property PravaObjektove As IPravaObjektove
        Get
            Return _pravaObjektove
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Shared Sub Initialize(pouzivatelia As IPouzivatelia, _
                                 Optional role As IRole = Nothing, _
                                 Optional prava As IPrava = Nothing, _
                                 Optional pravaObjektove As IPravaObjektove = Nothing)

        _pouzivatelia = pouzivatelia
        _role = role
        _prava = prava
        _pravaObjektove = pravaObjektove

    End Sub

#End Region

#Region "Private"

    Private Sub New()
    End Sub

#End Region

End Class
