Imports System.Drawing
Namespace Controles
    Public Class TextBoxCoa_Entero
        Inherits System.Windows.Forms.TextBox

        Private clrBackColorEnabled As Drawing.Color = Color.White
        Private mblnPermitirNegativos As Boolean = False

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

        <System.ComponentModel.Category("COA")> _
        <System.ComponentModel.Description("Indica si se permiten valores negativos.")> _
        Public Property PermitirNegativos() As Boolean
            Get
                Return mblnPermitirNegativos
            End Get
            Set(ByVal value As Boolean)
                mblnPermitirNegativos = value
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

        Private Sub TextBoxCoa_Entero_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
            If InStr(1, "0123456789" & CType(IIf(mblnPermitirNegativos, "-", String.Empty), String) & Chr(System.Windows.Forms.Keys.Back) & Chr(System.Windows.Forms.Keys.Return), e.KeyChar, CompareMethod.Binary) = 0 Then
                e.Handled = True
                Exit Sub
            End If

            'Si permite negativos
            If mblnPermitirNegativos AndAlso e.KeyChar = "-"c Then
                'Si la posición del cursor no es la primera
                If MyBase.SelectionStart > 0 Then
                    e.Handled = True
                End If
                'Si ya existe el simbolo negativo
                If MyBase.Text.IndexOf("-"c) >= 0 Then
                    e.Handled = True
                End If
            End If

        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'TextBoxCoa_Entero
            '
            Me.ResumeLayout(False)

        End Sub
    End Class
End Namespace