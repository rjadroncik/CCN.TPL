Imports System.Reflection
Imports System.Runtime.Serialization

<DataContract()>
Public MustInherit Class DynamicEnum
    Implements IComparable(Of DynamicEnum)
    Implements IConvertible

#Region "Properties"

    <DataMember()>
    Public Overridable Property Id As Integer
    <DataMember()>
    Public Overridable Property Nazov As String

#End Region

#Region "Initialization"

    Protected Sub New()
    End Sub

    Protected Sub New(id As Integer, nazov As String)

        _Id = id
        _Nazov = nazov

        If (Not _valuesAll.ContainsKey(Me.GetType())) Then

            Dim values As New Dictionary(Of Integer, DynamicEnum)()
            values.Add(id, Me)

            _valuesAll.Add(Me.GetType(), values)
        Else
            _valuesAll(Me.GetType()).Add(id, Me)
        End If
    End Sub

#End Region

#Region "Comparing"

    Public Function CompareTo(other As DynamicEnum) As Integer Implements IComparable(Of DynamicEnum).CompareTo

        If (other Is Nothing) Then Return 1

        If (Me.GetType() Is other.GetType()) Then Me.Id.CompareTo(other.Id)

        Return 1
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        If (obj Is Nothing) Then Return False

        If (Me.GetType() Is obj.GetType()) Then Return Me.Id = DirectCast(obj, DynamicEnum).Id

        Return False
    End Function

    Public Shared Operator =(a As DynamicEnum, b As DynamicEnum) As Boolean

        Return a.Equals(b)
    End Operator

    Public Shared Operator <>(a As DynamicEnum, b As DynamicEnum) As Boolean

        Return Not a.Equals(b)
    End Operator

    Public Overrides Function GetHashCode() As Integer
        Return Id
    End Function

#End Region

#Region "Shared - value mapping/enumeration"

    Protected Shared _valuesAll As New Dictionary(Of Type, IDictionary(Of Integer, DynamicEnum))

    Public Shared Function Values(Of Init As DynamicEnum, Target As DynamicEnum)() As IEnumerable(Of Target)

        ForceInitialization(Of Init)()

        Return _valuesAll(GetType(Target)).Values.OfType(Of Target)()
    End Function

    Public Shared Function Value(Of Init As DynamicEnum, Target As DynamicEnum)(id As Integer) As Target

        ForceInitialization(Of Init)()

        Return DirectCast(_valuesAll(GetType(Target))(id), Target)
    End Function

    Protected Shared Sub ForceInitialization(Of T As DynamicEnum)()

        For Each field In GetType(T).GetFields(BindingFlags.Static Or BindingFlags.Public Or BindingFlags.FlattenHierarchy)

            field.GetValue(Nothing)
        Next
    End Sub

#End Region

#Region "Overridden"

    Public Overrides Function ToString() As String
        Return Nazov
    End Function

#End Region

    Public Function GetTypeCode() As System.TypeCode Implements System.IConvertible.GetTypeCode

        Return TypeCode.Int32
    End Function

    Public Function ToBoolean(provider As System.IFormatProvider) As Boolean Implements System.IConvertible.ToBoolean

        Throw New InvalidCastException()
    End Function

    Public Function ToByte(provider As System.IFormatProvider) As Byte Implements System.IConvertible.ToByte

        Throw New InvalidCastException()
    End Function

    Public Function ToDateTime(provider As System.IFormatProvider) As Date Implements System.IConvertible.ToDateTime

        Throw New InvalidCastException()
    End Function

    Public Function ToDecimal(provider As System.IFormatProvider) As Decimal Implements System.IConvertible.ToDecimal

        Return Id
    End Function

    Public Function ToDouble(provider As System.IFormatProvider) As Double Implements System.IConvertible.ToDouble

        Throw New InvalidCastException()
    End Function

    Public Function ToChar(provider As System.IFormatProvider) As Char Implements System.IConvertible.ToChar

        Throw New InvalidCastException()
    End Function

    Public Function ToInt16(provider As System.IFormatProvider) As Short Implements System.IConvertible.ToInt16

        Throw New InvalidCastException()
    End Function

    Public Function ToInt32(provider As System.IFormatProvider) As Integer Implements System.IConvertible.ToInt32

        Return Id
    End Function

    Public Function ToInt64(provider As System.IFormatProvider) As Long Implements System.IConvertible.ToInt64

        Return Id
    End Function

    Public Function ToSByte(provider As System.IFormatProvider) As SByte Implements System.IConvertible.ToSByte

        Throw New InvalidCastException()
    End Function

    Public Function ToSingle(provider As System.IFormatProvider) As Single Implements System.IConvertible.ToSingle

        Throw New InvalidCastException()
    End Function

    Public Function ToStringConvertible(provider As System.IFormatProvider) As String Implements System.IConvertible.ToString

        Return Id.ToString()
    End Function

    Public Function ToType(conversionType As System.Type, provider As System.IFormatProvider) As Object Implements System.IConvertible.ToType

        If (conversionType Is GetType(Integer)) Then Return Id
        If (conversionType Is GetType(Decimal)) Then Return CDec(Id)
        If (conversionType Is GetType(Int64)) Then Return CType(Id, Int64)
        If (conversionType Is GetType(String)) Then Return Id.ToString()

        Throw New InvalidCastException()
    End Function

    Public Function ToUInt16(provider As System.IFormatProvider) As UShort Implements System.IConvertible.ToUInt16

        Throw New InvalidCastException()
    End Function

    Public Function ToUInt32(provider As System.IFormatProvider) As UInteger Implements System.IConvertible.ToUInt32

        Throw New InvalidCastException()
    End Function

    Public Function ToUInt64(provider As System.IFormatProvider) As ULong Implements System.IConvertible.ToUInt64

        Throw New InvalidCastException()
    End Function
End Class
