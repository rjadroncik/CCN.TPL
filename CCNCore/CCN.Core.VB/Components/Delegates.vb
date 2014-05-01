
Public Delegate Sub Worker()

Public Delegate Sub Worker1(Of In T)(param As T)
Public Delegate Sub Worker2(Of In T1, In T2)(param1 As T1, param2 As T2)
Public Delegate Sub Worker3(Of In T1, In T2, In T3)(param1 As T1, param2 As T2, param3 As T3)
Public Delegate Sub Worker4(Of In T1, In T2, In T3, In T4)(param1 As T1, param2 As T2, param3 As T3, param4 As T4)
Public Delegate Sub Worker5(Of In T1, In T2, In T3, In T4, In T5)(param1 As T1, param2 As T2, param3 As T3, param4 As T4, param5 As T5)

Public Delegate Function Operation(Of Out R)() As R

Public Delegate Function Operation1(Of Out R, In T1)(param As T1) As R
Public Delegate Function Operation2(Of Out R, In T1, In T2)(param As T1, param As T2) As R
Public Delegate Function Operation3(Of Out R, In T1, In T2, In T3)(param As T1, param As T2, param As T3) As R
Public Delegate Function Operation4(Of Out R, In T1, In T2, In T3, In T4)(param As T1, param As T2, param As T3, param As T4) As R
Public Delegate Function Operation4(Of Out R, In T1, In T2, In T3, In T4, In T5)(param As T1, param As T2, param As T3, param As T4, param5 As T5) As R

Public Delegate Function CustomToString(Of In T)(value As T) As String

Public Delegate Function Compare(Of T)(first As T, second As T) As Integer

Public Delegate Function PropertyGetter(Of In T)(objekt As T) As Object
Public Delegate Function PropertyGetter(Of In T, TResult)(objekt As T) As TResult