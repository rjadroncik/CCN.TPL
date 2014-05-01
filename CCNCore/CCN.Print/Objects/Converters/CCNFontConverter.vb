Imports System.Drawing
Imports System.ComponentModel

Public Class CCNFontConverter
    Inherits FontConverter

    Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return False
    End Function
End Class
