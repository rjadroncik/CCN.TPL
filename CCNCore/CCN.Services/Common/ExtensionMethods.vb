Imports NHibernate
Imports CCN.Core.VB
Imports System.Runtime.CompilerServices

Public Module ExtensionMethods

    <Extension()>
    Public Function ListClrRows(session As ISession, sql As String) As IList(Of ClrRow)

        Return session.CreateSQLQuery(sql).SetResultTransformer(New ToClrTableResultTransformer()).List(Of ClrRow)()
    End Function

    <Extension()>
    Public Function QueryClrRows(session As ISession, sql As String) As ISQLQuery

        Return DirectCast(session.CreateSQLQuery(sql).SetResultTransformer(New ToClrTableResultTransformer()), ISQLQuery)
    End Function

End Module
