Imports System.Reflection
Imports CCN.Model

Public Class Globals

#Region "Properties"

    Protected Shared _initialized As Boolean
    Public Shared ReadOnly Property Initialized As Boolean
        Get
            Return _initialized
        End Get
    End Property

    Protected Shared _aplikaciaBeziaca As ComponentType
    Public Shared ReadOnly Property AplikaciaBeziaca As ComponentType
        Get
            Return _aplikaciaBeziaca
        End Get
    End Property

    Protected Shared _aplikacie As New Dictionary(Of ComponentType, String)
    Public Shared ReadOnly Property Aplikacia(typ As ComponentType) As String
        Get
            Return If(_aplikacie.ContainsKey(typ), _aplikacie(typ), Nothing)
        End Get
    End Property

    Protected Shared _verzie As New Dictionary(Of ComponentType, Version)
    Public Shared ReadOnly Property Verzia(typ As ComponentType) As Version
        Get
            Return If(_verzie.ContainsKey(typ), _verzie(typ), Nothing)
        End Get
    End Property

    Protected Shared _showSql As Boolean
    Public Shared ReadOnly Property ShowSql As Boolean
        Get
            Return _showSql
        End Get
    End Property

    Protected Shared _startupPath As String
    Public Shared ReadOnly Property StartupPath As String
        Get
            Return _startupPath
        End Get
    End Property

    Public Class DB

        Protected Friend Shared _server As String
        Public Shared ReadOnly Property Server As String
            Get
                Return _server
            End Get
        End Property

        Protected Friend Shared _name As String
        Public Shared ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        Protected Friend Shared _user As String
        Public Shared ReadOnly Property User As String
            Get
                Return _User
            End Get
        End Property

        Protected Friend Shared _password As String
        Public Shared ReadOnly Property Password As String
            Get
                Return _password
            End Get
        End Property

    End Class

    Public Class Features

        Public Shared Property Pouzivatelia As Boolean = True
        Public Shared Property Prava As Boolean = True
    End Class

#End Region

#Region "Custom"

    Protected Shared _globals As IGlobalsCustom = New GlobalsDefault()

    Public Shared Sub PouzivatelPrilas(pouzivatel As IPouzivatel, entita As String)

        _globals.Pouzivatel = pouzivatel
        _globals.Entita = entita
    End Sub

    Public Shared ReadOnly Property Pouzivatel As IPouzivatel
        Get
            Return _globals.Pouzivatel
        End Get
    End Property

    Public Shared ReadOnly Property Entita As String
        Get
            Return _globals.Entita
        End Get
    End Property

#End Region

#Region "Initialization"

    Public Shared Sub InitAppSettings(server As String, name As String, user As String, password As String, _
                                      showSql As Boolean, startupPath As String, _
                                      defaultPreId As Nastavenia.DefaultPreIdDelegate,
                                      Optional globals As IGlobalsCustom = Nothing, _
                                      Optional entita As String = Nothing)

        If (_initialized) Then Throw New InvalidOperationException("Duplicate globals initialization!")
        _initialized = True

        If (globals IsNot Nothing) Then _globals = globals
        If (entita IsNot Nothing) Then _globals.Entita = entita

        _showSql = showSql
        _startupPath = startupPath

        ChangeConnection(server, name, user, password)

        Nastavenia.DefaultPreId = defaultPreId
    End Sub

    Public Shared Sub ChangeConnection(server As String, name As String)

        If (Not _initialized) Then Throw New InvalidOperationException("Can't change connection until globals are initialized!")

        DB._server = server
        DB._name = name
    End Sub

    Public Shared Sub ChangeConnection(server As String, name As String, user As String, password As String)

        ChangeConnection(server, name)

        DB._user = user
        DB._password = password
    End Sub

    Public Shared Sub InitAppInfo(typ As ComponentType, aplikacia As String, verzia As Version, beziaca As Boolean)

        _aplikacie.Add(typ, aplikacia)
        _verzie.Add(typ, verzia)

        If (beziaca) Then _aplikaciaBeziaca = typ
    End Sub

#End Region

End Class
