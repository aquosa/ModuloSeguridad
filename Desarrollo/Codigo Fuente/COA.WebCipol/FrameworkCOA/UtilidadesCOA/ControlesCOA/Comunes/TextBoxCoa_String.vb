Imports System.Drawing
Namespace Controles
    Public Class TextBoxCoa_String
        Inherits System.Windows.Forms.TextBox

        Private clrBackColorEnabled As Drawing.Color = Color.White

        <System.ComponentModel.Category("COA")> _
        <System.ComponentModel.Description("Setea o recupera el color de fondo del control cuando está deshabilitado.")> _
        Public Property BackColorDeshabilitado() As System.Drawing.Color
            Get
                Return clrBackColorEnabled
            End Get
            Set(ByVal value As System.Drawing.Color)
                clrBackColorEnabled = value
            End Set
        End Property

        Public Overloads Property Enabled() As Boolean
            Get
                Return MyBase.Enabled
            End Get
            Set(ByVal value As Boolean)
                If Not value Then
                    MyBase.BackColor = clrBackColorEnabled
                Else
                    MyBase.BackColor = Color.White
                End If
                MyBase.Enabled = value
            End Set
        End Property
    End Class
End Namespace