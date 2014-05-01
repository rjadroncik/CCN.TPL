Imports NHibernate
Imports NHibernate.Linq
Imports System.IO
Imports CCN.Model
Imports CCN.Core.VB

Public Class Nastavenia
    Inherits Service

#Region "Fields"

    Protected Shared _hodnoty As New Dictionary(Of IdNastavenia, Object)

#End Region

#Region "Delegates"

    Public Delegate Function DefaultPreIdDelegate(id As IdNastavenia, pouzivatel As IPouzivatel, entita As String, session As ISession) As NastavenieHodnota

    Public Shared DefaultPreId As DefaultPreIdDelegate = New DefaultPreIdDelegate(AddressOf DefaultPreIdCore)

#End Region

#Region "Constants"

    Public Const Globalne As String = "[Global]"
    Public Const Lokalne As String = "[Local]"
    Public Const Pouzivatelske As String = "[User]"

#End Region

#Region "Properties"

    Public Shared ReadOnly Property GridRefreshTimeout As Integer
        Get
            Return DirectCast(PodlaId(IdNastavenia.GridRefreshTimeout), Integer)
        End Get
    End Property

    Public Shared ReadOnly Property GridSearchTimeout As Integer
        Get
            Return DirectCast(PodlaId(IdNastavenia.GridSearchTimeout), Integer)
        End Get
    End Property

    Public Shared ReadOnly Property BGWorkTimeout As Integer
        Get
            Return DirectCast(PodlaId(IdNastavenia.BGWorkTimeout), Integer)
        End Get
    End Property

#End Region

#Region "Nacitavanie"

    Protected Shared Function PodlaId(id As IdNastavenia) As Object

        If (Not _hodnoty.ContainsKey(id)) Then Nacitaj(id)

        Return _hodnoty(id)
    End Function

    Protected Shared Sub Nacitaj(id As IdNastavenia)

        Nacitaj(id, Globals.Pouzivatel, Globals.Entita)
    End Sub

    Protected Shared Sub Nacitaj(id As IdNastavenia, pouzivatel As IPouzivatel, entita As String)

        Dim nastavenie As NastavenieHodnota = Nothing

        Dim entities As String() = {Globalne, entita}

        If (pouzivatel IsNot Nothing) Then entities = entities.Union(New String() {EntitaPouzivatel(pouzivatel)}).ToArray()

        Using session As ISession = NewSession(), transaction = session.BeginTransaction()

            Dim nastavenia = session.Query(Of NastavenieHodnota) _
                                    .Where(Function(x) (x.Nastavenie.Id = id) AndAlso entities.Contains(x.Entita)).ToList()

            Select Case nastavenia.Count()
                Case 0
                    nastavenie = DefaultPreId(id, pouzivatel, entita, session)
                    If (nastavenie.Nastavenie.Upravitelne) Then PouzivatelskeVytvor(nastavenie, pouzivatel, session)

                Case 1
                    nastavenie = nastavenia.Single()
                    If (nastavenie.Nastavenie.Upravitelne) Then PouzivatelskeVytvor(nastavenie, pouzivatel, session)

                Case 2
                    'Uprednostnime lokalne nastavenie na entitu, ak nie je bereme globalne [DLL-47]
                    nastavenie = nastavenia.Where(Function(x) Not x.Entita.Equals(Globalne)).Single()
                    If (Not nastavenie.Aktivna) Then nastavenie = nastavenia.Where(Function(x) x.Entita.Equals(Globalne)).Single()

            End Select

            transaction.Commit()
        End Using

        If (_hodnoty.ContainsKey(id)) Then _hodnoty.Remove(id)
        _hodnoty.Add(id, SkonvertujHodnotu(nastavenie))
    End Sub

#End Region

#Region "Default hodnoty"

    Public Shared Function DefaultPreIdCore(id As IdNastavenia, pouzivatel As IPouzivatel, entita As String, session As ISession) As NastavenieHodnota

        Dim skupinaGlobalne = SkupinaGlobalneNacitaj(session)
        Dim skupinaLokalne = SkupinaLokalneNacitaj(session)

        Dim skupinaEntita = SkupinaEntitaNacitajAleboVytvor(entita, skupinaLokalne, session)

        Dim hodnota As NastavenieHodnota = NastavenieHodnota.Create()
        With hodnota
            Select Case (id.Id)

                Case IdNastavenia.GridRefreshTimeout.Id
                    .Entita = entita
                    .Hodnota = "5"
                    .Skupina = skupinaEntita
                    skupinaEntita.Hodnoty.Add(hodnota)

                Case IdNastavenia.BGWorkTimeout.Id
                    .Entita = entita
                    .Hodnota = "60"
                    .Skupina = skupinaEntita
                    skupinaEntita.Hodnoty.Add(hodnota)

                Case IdNastavenia.GridSearchTimeout.Id
                    .Entita = entita
                    .Hodnota = "5"
                    .Skupina = skupinaEntita
                    skupinaEntita.Hodnoty.Add(hodnota)

                Case Else
                    Throw New NastavenieNenamapovaneException(id.Id)
            End Select
        End With

        hodnota.Aktivna = True
        hodnota.Nastavenie = session.Query(Of Nastavenie).Where(Function(x) x.Id = id).Single()

        session.Save(hodnota)
        session.Update(skupinaLokalne)

        Return hodnota
    End Function

#End Region

#Region "Initializaion"

    Public Shared Sub InicializujCore()

        Using session As ISession = NewSession()

            Dim unused As Object
            unused = GridRefreshTimeout
            unused = GridSearchTimeout
            unused = BGWorkTimeout

            For Each skupina As NastavenieSkupina In SkupinaPodskupiny(SkupinaLokalneNacitaj(session), session)

                Nacitaj(IdNastavenia.GridRefreshTimeout, Globals.Pouzivatel, skupina.Nazov)
                Nacitaj(IdNastavenia.GridSearchTimeout, Globals.Pouzivatel, skupina.Nazov)
                Nacitaj(IdNastavenia.BGWorkTimeout, Globals.Pouzivatel, skupina.Nazov)
            Next
        End Using
    End Sub

#End Region

#Region "Pouzivatelske"

    Private Shared Sub PouzivatelskeVytvor(vzor As NastavenieHodnota, pouzivatel As IPouzivatel, session As ISession)

        Dim skupinaPouzivatelske = SkupinaPouzivatelskeNacitaj(session)
        Dim skupinaPouzivatel = SkupinaPouzivatelNacitajAleboVytvor(pouzivatel, skupinaPouzivatelske, session)

        Dim nove = NastavenieHodnota.Create()
        With nove
            .Nastavenie = vzor.Nastavenie
            .Entita = EntitaPouzivatel(pouzivatel)

            .Hodnota = vzor.Hodnota
            .Skupina = skupinaPouzivatel

            .Aktivna = False
        End With

        skupinaPouzivatel.Hodnoty.Add(nove)

        session.Update(skupinaPouzivatelske)
        session.Save(nove)
    End Sub

    Public Shared Sub PouzivatelskeZmaz(id As IdNastavenia)

        Using session = NewSession(), transaction = session.BeginTransaction()

            Dim hodnota = HodnotaEntitaNacitaj(id, EntitaPouzivatel(Globals.Pouzivatel), session)
            If (hodnota IsNot Nothing) Then

                session.Delete(hodnota)
            End If

            transaction.Commit()
        End Using
    End Sub

    Public Shared Sub PouzivatelskeNastav(id As IdNastavenia, value As String)

        Using session = NewSession(), transaction = session.BeginTransaction()

            Dim skupinaPouzivatelske = SkupinaPouzivatelskeNacitaj(session)
            Dim skupinaPouzivatel = SkupinaPouzivatelNacitajAleboVytvor(Globals.Pouzivatel, skupinaPouzivatelske, session)

            Dim vzor = session.Query(Of NastavenieHodnota).Where(Function(x) (x.Nastavenie.Id = id) AndAlso _
                                                                              x.Entita.Equals(Globalne)).Single()

            Dim hodnota = HodnotaEntitaNacitaj(id, EntitaPouzivatel(Globals.Pouzivatel), session)

            If (hodnota Is Nothing) Then hodnota = NastavenieHodnota.Create()
            With hodnota

                .Entita = EntitaPouzivatel(Globals.Pouzivatel)

                .Hodnota = value
                .Skupina = skupinaPouzivatel
                .Aktivna = True
            End With

            skupinaPouzivatel.Hodnoty.Add(hodnota)

            session.Update(skupinaPouzivatelske)
            session.Save(hodnota)

            transaction.Commit()

            If (_hodnoty.ContainsKey(id)) Then _hodnoty.Remove(id)
            _hodnoty.Add(id, SkonvertujHodnotu(hodnota))
        End Using
    End Sub

#End Region

#Region "Util"

    Public Shared Sub ClearCache()

        _hodnoty.Clear()
    End Sub

    Public Shared Function EntitaPouzivatel(pouzivatel As IPouzivatel) As String

        Return "/" & pouzivatel.Login
    End Function

    Protected Shared Function SkonvertujHodnotu(hodnota As NastavenieHodnota) As Object

        If ((hodnota.Hodnota Is Nothing) OrElse (hodnota.Hodnota.Trim().Length = 0)) Then Return Nothing

        Select Case hodnota.Nastavenie.DatovyTyp

            Case DatovyTyp.AnoNie
                Return Boolean.Parse(hodnota.Hodnota)

            Case DatovyTyp.CisloCele
                Return Integer.Parse(hodnota.Hodnota, Globalization.NumberFormatInfo.InvariantInfo)

            Case DatovyTyp.CisloDesatinne
                Return Double.Parse(hodnota.Hodnota, Globalization.NumberFormatInfo.InvariantInfo)

            Case DatovyTyp.Retazec
                Return hodnota.Hodnota

            Case DatovyTyp.DatumCas
                Return DateTime.Parse(hodnota.Hodnota)
        End Select

        Throw New InvalidCastException("Datovy typ '" & hodnota.Nastavenie.DatovyTyp & "' nastavenia/parametru '" & hodnota.Nastavenie.Nazov & "' nebol rozpoznany.")
    End Function

    Protected Shared Function SkupinaEntitaNacitajAleboVytvor(entita As String, nadskupina As NastavenieSkupina, session As ISession) As NastavenieSkupina

        Dim skupina As NastavenieSkupina = SkupinaEntitaNacitaj(entita, session)
        If (skupina Is Nothing) Then

            skupina = NastavenieSkupina.Create()
            skupina.Nazov = entita
            skupina.Poradie = nadskupina.Deti.Count() + 1
            skupina.Uroven = 1
            skupina.Rodic = nadskupina
            session.Save(skupina)

            nadskupina.Deti.Add(skupina)
        End If

        Return skupina
    End Function

    Protected Shared Function SkupinaPouzivatelNacitajAleboVytvor(pouzivatel As IPouzivatel, nadskupina As NastavenieSkupina, session As ISession) As NastavenieSkupina

        Dim skupina As NastavenieSkupina = SkupinaEntitaNacitaj(EntitaPouzivatel(pouzivatel), session)
        If (skupina Is Nothing) Then

            skupina = NastavenieSkupina.Create()
            skupina.Nazov = EntitaPouzivatel(pouzivatel)
            skupina.Poradie = nadskupina.Deti.Count() + 1
            skupina.Uroven = 1
            skupina.Rodic = nadskupina
            session.Save(skupina)

            nadskupina.Deti.Add(skupina)
        End If

        Return skupina
    End Function

    Protected Shared Function SkupinaGlobalneNacitaj(session As ISession) As NastavenieSkupina

        Return session.Query(Of NastavenieSkupina).Where(Function(x) (x.Uroven = 0) AndAlso x.Nazov.Equals(Globalne)).Single()
    End Function

    Protected Shared Function SkupinaLokalneNacitaj(session As ISession) As NastavenieSkupina

        Return session.Query(Of NastavenieSkupina).Where(Function(x) (x.Uroven = 0) AndAlso x.Nazov.Equals(Lokalne)).Single()
    End Function

    Protected Shared Function SkupinaPouzivatelskeNacitaj(session As ISession) As NastavenieSkupina

        Return session.Query(Of NastavenieSkupina).Where(Function(x) (x.Uroven = 0) AndAlso x.Nazov.Equals(Pouzivatelske)).Single()
    End Function

    Protected Shared Function SkupinaPodskupiny(skupina As NastavenieSkupina, session As ISession) As IEnumerable(Of NastavenieSkupina)

        Return session.Query(Of NastavenieSkupina).Where(Function(x) (x.Uroven = 1) AndAlso (x.Rodic.Id = skupina.Id)).ToList()
    End Function

    Protected Shared Function SkupinaEntitaNacitaj(entita As String, session As ISession) As NastavenieSkupina

        Return session.Query(Of NastavenieSkupina).Where(Function(x) (x.Uroven = 1) AndAlso x.Nazov.Equals(entita)).SingleOrDefault()
    End Function

    Protected Shared Function HodnotaEntitaNacitaj(id As IdNastavenia, entita As String, session As ISession) As NastavenieHodnota

        Return session.Query(Of NastavenieHodnota).Where(Function(x) (x.Nastavenie.Id = id) AndAlso x.Entita.Equals(entita)).SingleOrDefault()
    End Function

#End Region

End Class
