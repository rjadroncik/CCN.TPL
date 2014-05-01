Imports System.Drawing
Imports System.Drawing.Text

Public Class CCNGraphics

    Public Shared Sub DrawLine(ByVal g As Graphics, ByVal color As Drawing.Color, ByVal w As Single, ByVal x1 As Single, ByVal y1 As Single, ByVal x2 As Single, ByVal y2 As Single)
        Using P As New Pen(color, w)
            g.DrawLine(P, x1, y1, x2, y2)
        End Using
    End Sub

    Public Shared Sub DrawRectangle(ByVal g As Graphics, ByVal color As Drawing.Color, ByVal w As Single, ByVal x1 As Single, ByVal y1 As Single, ByVal x2 As Single, ByVal y2 As Single)
        Using P As New Pen(color, w)
            g.DrawRectangle(P, x1, y1, x2 - x1, y2 - y1)
        End Using
    End Sub

    Public Shared Sub DrawString(ByVal g As Graphics, ByVal color As Drawing.Color, ByVal text As String, ByVal h As Single, ByVal style As Drawing.FontStyle, ByVal x As Single, ByVal y As Single, ByVal fontname As String, ByVal alignment As Long, Optional ByVal angle As Single = 0)

        Using f As New Font(fontname, h, style, GraphicsUnit.Millimeter, 0)

            Using B As New SolidBrush(color)

                Using fo As New StringFormat

                    Select Case alignment
                        Case 0
                            fo.Alignment = StringAlignment.Near
                        Case 1
                            fo.Alignment = StringAlignment.Center
                        Case 2
                            fo.Alignment = StringAlignment.Far
                    End Select

                    g.TextRenderingHint = TextRenderingHint.AntiAlias
                    If (angle <> 0) Then

                        g.TranslateTransform(x, y)
                        g.RotateTransform(angle)
                        g.DrawString(text, f, B, 0, 0, fo)
                        g.RotateTransform(-angle)
                        g.TranslateTransform(-x, -y)
                    Else
                        g.DrawString(text, f, B, x, y, fo)
                    End If
                End Using
            End Using
        End Using
    End Sub
End Class
