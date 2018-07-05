Imports System.Drawing
Namespace Controles
    Public Class TextBoxCoa_Decimal
        Inherits System.Windows.Forms.TextBox


        Private clrBackColorEnabled As Drawing.Color = Color.White
        Private mblnPermitirNegativos As Boolean = False
        Private mbytCantDecimales As Byte = 2
        Private mblnUsarDecimales As Boolean = True
        Private mstrSepDecRepresentar As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator

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

        <System.ComponentModel.Category("COA")> _
        <System.ComponentModel.Description("Cantidad de decimales a utilizar en números.")> _
        Property CantidadDecimales() As Byte
            Get
                Return mbytCantDecimales
            End Get
            Set(ByVal Value As Byte)
                If Value > 15 Then
                    MsgBox("Valor incorrecto. (max. 15 decimales).", MsgBoxStyle.Information, "Error")
                    Exit Property
                Else
                    mbytCantDecimales = Value
                End If
            End Set
        End Property

        <System.ComponentModel.Category("COA")> _
                <System.ComponentModel.Description("Indica si se permite el ingreso de números decimales.")> _
        Property PermitirDecimales() As Boolean
            Get
                Return mblnUsarDecimales
            End Get
            Set(ByVal Value As Boolean)
                mblnUsarDecimales = Value
            End Set
        End Property

        Private Sub TextBoxCoa_Decimal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
            Dim aCant() As String
            Dim strContenido As String

            If InStr(1, "0123456789." & CType(IIf(mblnPermitirNegativos, "-", String.Empty), String) & Chr(System.Windows.Forms.Keys.Back) & Chr(System.Windows.Forms.Keys.Return), e.KeyChar, CompareMethod.Binary) = 0 Then
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

            ElseIf e.KeyChar.Equals("."c) Then ' "." del teclado numérico
                'Si se permiten decimales, y el separador decimal de la configuración
                'regional ya existe
                If mblnUsarDecimales Then
                    If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) > 0 Then
                        e.Handled = True
                    Else
                        MyBase.Text &= mstrSepDecRepresentar
                        MyBase.SelectionStart = Len(MyBase.Text)
                        e.Handled = True
                    End If
                Else
                    e.Handled = True
                End If
            Else
                If MyBase.SelectionLength = 0 Then
                    'Verifico si ya se ha ingresado el símbolo decimal
                    If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) > 0 Then
                        'Verifico si el número que deseo ingresar se encuentra antes
                        'o después del símbolo decimal
                        If MyBase.SelectionStart > InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) Then
                            'Si se desea ingresar un símbolo decimal, verifico
                            'si no se ha llegado al límite de decimales establecidos
                            ' Si se debe comprobar la cantidad de decimales
                            If mblnUsarDecimales Then
                                If mbytCantDecimales > 0 Then
                                    If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) > 0 Then
                                        strContenido = MyBase.Text & e.KeyChar
                                        aCant = Split(strContenido, mstrSepDecRepresentar)
                                        If Len(aCant(1)) > mbytCantDecimales Then e.Handled = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'TextBoxCoa_Decimal
            '
            Me.ResumeLayout(False)

        End Sub
    End Class
End Namespace