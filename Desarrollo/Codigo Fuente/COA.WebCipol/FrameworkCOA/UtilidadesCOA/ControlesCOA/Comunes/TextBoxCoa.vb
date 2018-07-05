Imports system.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Drawing
Namespace Controles
    <System.ComponentModel.DefaultBindingProperty("Text")> _
    Public Class TextBoxCoa
        Inherits System.Windows.Forms.TextBox

#Region " DECLARACIONES "

        Private enuTipoDato As TipoDatos
        Private blnEsRequerido As Boolean = False
        Private mbytCantDecimales As Byte = 2
        Private mblnUsarDecimales As Boolean = True
        Private mblnPermitirNegativos As Boolean = False
        Private mstrSepMilConfReg As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        Private mstrSepDecConfReg As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        Private mstrSepDecRepresentar As String = ","c
        Private mstrSepMilRepresentar As String = "."c
        Private mblnFormatearVisualizacion As Boolean = False
        Private mintMaxLenght As Integer
        Private enuExpresionRegularValidacion As ExpresionesRegulares = ExpresionesRegulares.Ninguna
        Private enuSeparadorDecimal As TipoSeparador
        Private enuSeparadorMiles As TipoSeparador
        Private strExpresionRegularPersonalizada As String = ""
        Private strToolTip As String
        Private tlpToolTip As New ToolTipCoa
        Private ErrorProvider As New ErrorProvider
        Private strValorMinimo As String = ""
        Private strValorMaximo As String = ""
        Private enuTipoValorNoValido As TipoValorNoValido = TipoValorNoValido.Ninguno
        Private clrBackColorEnabled As Drawing.Color = Color.White
        Private blnEnabled As Boolean
        Public Event ValorNoValido(ByVal sender As Object, ByVal Tipo As TipoValorNoValido, ByVal Valor As String)
        Public Enum TipoValorNoValido As Integer
            Ninguno = 0
            Minimo = 1
            Máximo = 2
        End Enum

        Public Enum TipoDatos As Integer
            Alfanumerico = 0
            Numerico = 1
            Fecha = 2
        End Enum
        Public Enum ExpresionesRegulares As Integer
            Ninguna = 0
            Fecha = 1
            NumerosEntero = 2
            NumerosDecimales = 3
        End Enum
        Public Enum TipoSeparador As Integer
            Coma = 0
            Punto = 1
        End Enum

#End Region

#Region " PROPIEDADES "

#Region " COA "
        <System.ComponentModel.Category("COA")> _
            <System.ComponentModel.DefaultValue(TipoDatos.Alfanumerico)> _
            <System.ComponentModel.Description("Indica el Tipo de Dato que se va a ingresar en el control. " _
            & "Los Tipos de Datos posibles son Alfanumerico (Ingreso Libre), Numérico (Números enteros o decimales " _
            & "según propiedad PermitirDecimales) o Fecha (Fechas válidas según propiedad FormatoFecha).")> _
            Public Property TipoDato() As TipoDatos
            Get
                Return enuTipoDato
            End Get
            Set(ByVal value As TipoDatos)
                enuTipoDato = value
            End Set
        End Property
        <System.ComponentModel.Category("COA")> _
           <System.ComponentModel.DefaultValue(False)> _
           <System.ComponentModel.Description("True: Ingreso Obligatorio / False: Ingreso opcional.")> _
           Public Property Requerido() As Boolean
            Get
                Return blnEsRequerido
            End Get
            Set(ByVal value As Boolean)
                blnEsRequerido = value
            End Set
        End Property
        <System.ComponentModel.Category("COA")> _
           <System.ComponentModel.Description("Expresion regular utilizada para validar el control.")> _
        Property ExpresionRegularValidacion() As ExpresionesRegulares
            Get
                Return enuExpresionRegularValidacion
            End Get
            Set(ByVal Value As ExpresionesRegulares)
                If strExpresionRegularPersonalizada <> "" Then
                    MessageBox.Show(" Ya existe una Expresión regular de validación para el control. Verifique la Propiedad ExpresionRegular", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Value = ExpresionesRegulares.Ninguna
                End If
                If Value = ExpresionesRegulares.Fecha Then
                    Me.MaxLength = 10
                End If
                enuExpresionRegularValidacion = Value
            End Set
        End Property
        <System.ComponentModel.Category("COA")> _
           <System.ComponentModel.DefaultValue("")> _
           <System.ComponentModel.Description("Expresion regular personalizada utilizada para validar el control.")> _
        Property ExpresionRegular() As String
            Get
                Return strExpresionRegularPersonalizada
            End Get
            Set(ByVal Value As String)
                Try
                    If enuExpresionRegularValidacion <> ExpresionesRegulares.Ninguna Then
                        MessageBox.Show(" Ya existe una Expresión regular de validación para el control. Verifique la propiedad ExpresionRegularValidacion", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Value = ""
                    End If
                    If Value <> "" Then
                        'Comprueba que sea una expresión regular válida intentando buscar una cadena falsa
                        Dim blnControl As Boolean = Regex.IsMatch("abcde", Value)
                    End If
                    'Si la validación no produce error, setea el valor de la expresión regular
                    strExpresionRegularPersonalizada = Value
                Catch ex As Exception
                    MessageBox.Show(" Expresión regular no válida", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                strExpresionRegularPersonalizada = Value
            End Set
        End Property
        <System.ComponentModel.Category("COA")> _
           <System.ComponentModel.DefaultValue("")> _
           <System.ComponentModel.Description("Tooltip del control.")> _
           Public Property ToolTip() As String
            Get
                Return strToolTip
            End Get
            Set(ByVal value As String)
                strToolTip = value
                Me.tlpToolTip.SetToolTip(Me, value)
            End Set
        End Property
        <System.ComponentModel.Category("COA")> _
       <System.ComponentModel.DefaultValue("")> _
       <System.ComponentModel.Description("Establece el valor mínimo permitido a ingresar en el control.")> _
        Public Property ValorMinimo() As String
            Get
                Return strValorMinimo
            End Get
            Set(ByVal value As String)
                strValorMinimo = value
            End Set
        End Property
        <System.ComponentModel.Category("COA")> _
      <System.ComponentModel.DefaultValue("")> _
      <System.ComponentModel.Description("Establece el valor máximo permitido a ingresar en el control.")> _
       Public Property ValorMaximo() As String
            Get
                Return strValorMaximo
            End Get
            Set(ByVal value As String)
                strValorMaximo = value
            End Set
        End Property

        <System.ComponentModel.Category("COA")> _
               <System.ComponentModel.Description("Propiedad MaxLength del Control.")> _
            Overrides Property MaxLength() As Integer
            Get
                Return mintMaxLenght
            End Get
            Set(ByVal Value As Integer)
                mintMaxLenght = Value
                MyBase.MaxLength = Value
            End Set
        End Property

        <System.ComponentModel.Category("COA")> _
      <System.ComponentModel.DefaultValue("")> _
      <System.ComponentModel.Description("Establece o recupera el color de fondo del control cuando está deshabilitado.")> _
       Public Property BackColorDeshabilitado() As System.Drawing.Color
            Get
                Return clrBackColorEnabled
            End Get
            Set(ByVal value As System.Drawing.Color)
                clrBackColorEnabled = value
            End Set
        End Property

#End Region

#Region " COA - ALFANUMERICO "

#End Region

#Region " COA - NUMERICO "
        <System.ComponentModel.Category("COA (Numérico)")> _
            <System.ComponentModel.DefaultValue(TipoSeparador.Coma)> _
            <System.ComponentModel.Description("Separador decimal a utilizar en el control.")> _
        Property SeparadorDecimal() As TipoSeparador
            Get
                Return enuSeparadorDecimal
            End Get
            Set(ByVal Value As TipoSeparador)
                enuSeparadorDecimal = Value
                If enuSeparadorDecimal = TipoSeparador.Coma Then
                    mstrSepDecRepresentar = ","c
                Else
                    mstrSepDecRepresentar = "."c
                End If
            End Set
        End Property
        <System.ComponentModel.Category("COA (Numérico)")> _
                <System.ComponentModel.DefaultValue(TipoSeparador.Punto)> _
                <System.ComponentModel.Description("Separador de miles a utilizar en el control.")> _
            Property SeparadorMiles() As TipoSeparador
            Get
                Return enuSeparadorMiles
            End Get
            Set(ByVal Value As TipoSeparador)
                enuSeparadorMiles = Value
                If enuSeparadorMiles = TipoSeparador.Coma Then
                    mstrSepMilRepresentar = ","c
                Else
                    mstrSepMilRepresentar = "."c
                End If
            End Set
        End Property
        <System.ComponentModel.Category("COA (Numérico)")> _
                <System.ComponentModel.Description("Indica si se permite el ingreso de números decimales.")> _
        Property PermitirDecimales() As Boolean
            Get
                Return mblnUsarDecimales
            End Get
            Set(ByVal Value As Boolean)
                mblnUsarDecimales = Value
            End Set
        End Property
        <System.ComponentModel.Category("COA (Numérico)")> _
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
        <System.ComponentModel.Category("COA (Numérico)")> _
        <System.ComponentModel.DefaultValue(False)> _
        <System.ComponentModel.Description("Indica si se permite el ingreso de número negativos.")> _
        Property PermitirValoresNegativos() As Boolean
            Get
                Return mblnPermitirNegativos
            End Get
            Set(ByVal Value As Boolean)
                mblnPermitirNegativos = Value
            End Set
        End Property
        <System.ComponentModel.Category("COA (Numérico)")> _
       <System.ComponentModel.DefaultValue(False)> _
       <System.ComponentModel.Description("Formatea los datos para su visualización.")> _
        Property FormatearVisualizacion() As Boolean
            Get
                Return mblnFormatearVisualizacion
            End Get
            Set(ByVal Value As Boolean)
                mblnFormatearVisualizacion = Value
            End Set
        End Property
#End Region

#Region " COA - FECHA "

#End Region

#End Region

#Region " EVENTOS "

        Private Sub TextBoxCoa_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
            Select Case enuTipoDato
                Case TipoDatos.Numerico
                    e.Handled = VerificarTeclaValidaNumerico(e)
                Case TipoDatos.Alfanumerico

                Case TipoDatos.Fecha
            End Select
        End Sub
        Private Sub TextBoxCoa_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            'lngLen     : Longitud de parte entera del número
            'lngPerm    : Cantidad de dígitos permitidos
            'strValor   : Valor a formatear
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Select Case enuTipoDato
                Case TipoDatos.Numerico
                    'Si se ingreso una longitud máxima de digitos y la propiedad Decimales = True
                    'verifica que la parte entera del número no supere la cantidad de digitos
                    'permitidos - la cantidad de decimales
                    Dim lngLen As Integer, lngPerm As Integer, strValor As String

                    If MyBase.Text.Length = 0 Then Return

                    If MyBase.MaxLength > 0 And mblnUsarDecimales And MyBase.Text <> "" Then
                        If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) = 0 Then
                            lngLen = Len(MyBase.Text)
                        Else
                            lngLen = MyBase.Text.Substring(0, MyBase.Text.IndexOf(mstrSepDecRepresentar)).Length
                        End If
                        lngPerm = MyBase.MaxLength - mbytCantDecimales - 1 'Elimino 1 por el separador decimal
                        If lngLen > lngPerm Then
                            MsgBox("Valor incorrecto. (máx. " & ReplicarString("9", lngPerm) & mstrSepDecRepresentar & ReplicarString("9", mbytCantDecimales) & ").", MsgBoxStyle.Exclamation, "Error")
                            MyBase.Focus()
                            Exit Sub
                        End If
                    End If
                    'Si se debe formatear el valor
                    If mblnFormatearVisualizacion Then
                        strValor = MyBase.Text
                        strValor = FormatearValor(strValor)
                        'Agrando el maxlength de la caja de texto por el formateo
                        MyBase.MaxLength = strValor.Length
                        MyBase.Text = strValor
                    End If
                Case TipoDatos.Fecha
                Case TipoDatos.Alfanumerico
            End Select
        End Sub
        '''<summary> Formatea el numero a su forma sin separadores de miles cuando el control recive foco</summary>
        Private Sub TextBoxCoa_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                DESCRIPCION DE LAS VARIABLES LOCALES
            ' strValor      : string que contendra el valor del campo de texto
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strValor As String
            Select Case enuTipoDato
                Case TipoDatos.Numerico
                    'si se eligió formatear la visualizacion, saco los separadores de mil
                    If mblnFormatearVisualizacion AndAlso MyBase.Text.Length > 0 Then
                        strValor = MyBase.Text

                        'Limpio los separadores de mil
                        strValor = strValor.Replace(mstrSepMilRepresentar, "")

                        'aqui es necesario primero limpiar el textbox y luego asignarle el valor
                        'transformado pues de otra forma no funciona :O 
                        MyBase.Text = ""
                        MyBase.Text = strValor
                    End If

                    'disminuimos el tamaño maximo del texto al especificado inicialmente en
                    'su propiedad MaxLength en tiempo de diseño
                    MyBase.MaxLength = mintMaxLenght
                Case TipoDatos.Fecha
                Case TipoDatos.Alfanumerico
            End Select
        End Sub
#End Region

#Region " METODOS "
        ''' <summary>
        ''' Verifica que la tecla ingresada sea válida para el tipo de dato numérico
        ''' </summary>
        ''' <param name="e"></param>
        ''' <returns>True - Tecla Invalida / False: Tecla válida</returns>
        ''' <remarks></remarks>
        Private Function VerificarTeclaValidaNumerico(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
            Dim aCant() As String
            Dim strContenido As String
            Dim blnHandled As Boolean = False

            'Si el caracter ingresado se corresponde con la
            'Tecla BackSpace o Enter da como válido el ingreso
            If (e.KeyChar = Chr(System.Windows.Forms.Keys.Back) OrElse _
                e.KeyChar = Chr(System.Windows.Forms.Keys.Return)) Then
                Return False
            End If
            If InStr(1, "0123456789." & CType(IIf(mblnPermitirNegativos, "-", String.Empty), String) & Chr(System.Windows.Forms.Keys.Back) & Chr(System.Windows.Forms.Keys.Return), e.KeyChar, CompareMethod.Binary) = 0 Then
                blnHandled = True
            Else
                If e.KeyChar.Equals("."c) Then ' "." del teclado numérico
                    'Si se permiten decimales, y el separador decimal de la configuración
                    'regional ya existe
                    If mblnUsarDecimales Then
                        If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) > 0 Then blnHandled = True
                    Else
                        blnHandled = True
                    End If
                End If
                'Si se ingresa símbolo negativo, y ya existen valores en la caja 
                'de texto
                If e.KeyChar = "-" And Len(MyBase.Text) > 0 Then blnHandled = True
            End If

            If Not blnHandled Then
                ' Si se presiono la tecla "." del teclado numérico
                If e.KeyChar = "." Then
                    MyBase.Text = MyBase.Text & mstrSepDecRepresentar
                    MyBase.SelectionStart = Len(MyBase.Text)
                    blnHandled = True
                End If
                ' Si no tiene foco
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
                                        If Len(aCant(1)) > mbytCantDecimales Then blnHandled = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Return blnHandled
        End Function
        ''' <summary>
        ''' Realiza Validaciones sobre el control 
        ''' </summary>
        ''' <returns>True: Datos válidos / False: Datos Inválidos</returns>
        ''' <remarks></remarks>
        Public Function Validar() As Boolean
            enuTipoValorNoValido = TipoValorNoValido.Ninguno
            Validar = True
            'Si es un campo requerido y esta vacio 
            'setea en falso la validación
            If Me.Requerido And Me.Text = "" Then
                SetearError("Valor requerido.")
                Validar = False
            End If
            'Si hay una Expresión regular valida que el valor
            'ingresado sea correcto
            If Validar AndAlso _
               (Me.enuExpresionRegularValidacion <> ExpresionesRegulares.Ninguna OrElse strExpresionRegularPersonalizada <> "") Then
                If Not Me.Requerido And Me.Text = "" Then
                    Validar = True
                Else
                    Validar = ValidarExpresionRegular()
                    SetearError("El Valor ingresado no es válido.")
                End If
            End If
            'Verfifica los valores mínimos y máximos seteados en el control
            If Validar AndAlso _
               Me.Text <> "" AndAlso _
               (strValorMinimo <> "" OrElse strValorMaximo <> "") Then
                Select Case Me.enuTipoDato
                    Case TipoDatos.Numerico
                        If strValorMinimo <> "" AndAlso Val(Me.Text) < Val(strValorMinimo) Then
                            SetearError("El Valor mínimo a ingresar es " & strValorMinimo & ". Verifique.")
                            enuTipoValorNoValido = TipoValorNoValido.Minimo
                            Validar = False
                        End If
                        If strValorMaximo <> "" AndAlso Val(Me.Text) > Val(strValorMaximo) Then
                            SetearError("El Valor máximo a ingresar es " & strValorMaximo & ". Verifique.")
                            enuTipoValorNoValido = TipoValorNoValido.Máximo
                            Validar = False
                        End If
                    Case TipoDatos.Fecha
                        If strValorMinimo <> "" AndAlso CDate(Me.Text) < CDate(strValorMinimo) Then
                            SetearError("El Valor mínimo a ingresar es " & strValorMinimo & ". Verifique.")
                            enuTipoValorNoValido = TipoValorNoValido.Minimo
                            Validar = False
                        End If
                        If strValorMaximo <> "" AndAlso CDate(Me.Text) > CDate(strValorMaximo) Then
                            SetearError("El Valor máximo a ingresar es " & strValorMaximo & ". Verifique.")
                            enuTipoValorNoValido = TipoValorNoValido.Máximo
                            Validar = False
                        End If
                End Select
            End If
            If Validar Then
                SetearError("")
            Else
                'Si el valor mínimo o máximo del control no es válido dispara el evento ValorNoValido 
                If enuTipoValorNoValido <> TipoValorNoValido.Ninguno Then
                    RaiseEvent ValorNoValido(Me, enuTipoValorNoValido, Me.Text)
                End If
            End If
        End Function
        ''' <summary>
        ''' Valida que el valor ingresado en el control sea correcto
        ''' en base a la expresión regular ingresada para validar
        ''' </summary>
        ''' <returns>True: Valor correcto / False: Valor incorrecto</returns>
        ''' <remarks></remarks>
        Private Function ValidarExpresionRegular() As Boolean
            Dim strExpresionRegular As String = ""
            Dim strValor As String = ""

            ValidarExpresionRegular = True
            strValor = Me.Text
            If strExpresionRegularPersonalizada <> "" Then
                strExpresionRegular = strExpresionRegularPersonalizada
            Else
                Select Case enuExpresionRegularValidacion
                    Case ExpresionesRegulares.Fecha
                        'Fecha en Formato DD/MM/AAAA
                        strExpresionRegular = "(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    Case ExpresionesRegulares.NumerosEntero
                        'Numeros enteros positivos y negativos según permita o no negativos el control
                        If mblnPermitirNegativos Then
                            strExpresionRegular = "(^[0]{1}$|^[-]?[1-9]{1}\d*$)"
                        Else
                            strExpresionRegular = "(^[0]{1}$|^[1-9]{1}\d*$)"
                        End If
                        If IsNumeric(Me.Text) Then
                            strValor = CStr(Val(Me.Text))
                        End If
                    Case ExpresionesRegulares.NumerosDecimales
                        strExpresionRegular = "(^-?\d*((?<=\d)|[\" & mstrSepDecRepresentar.Trim & "](?=\d))\d*$)"
                End Select
            End If
            If strExpresionRegular <> "" Then
                ValidarExpresionRegular = Regex.IsMatch(strValor, strExpresionRegular)
            End If
        End Function
        'Formatea el valor recibido
        'Parámetros de entrada. Valor a formatear
        'Parámetros de salida: Número formateado
        Private Function FormatearValor(ByVal pValor As String) As String
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Fecha de creación: 04/01/2003
            'Modificaciones:
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '              DESCRIPCION DE VARIABLES LOCALES
            'strEnteros : Formato de la parte entera del número
            'shtI       : Contador del For
            'shtMax     : Cantidad máxima de caracteres de la parte entera
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strEnteros As String = String.Empty, shtI As Short
            Dim shtMax As Short

            If pValor.Length = 0 Then Return ""
            If CDbl(pValor) = 0 Then Return "0"
            pValor = pValor.Replace(mstrSepMilRepresentar, "")
            pValor = pValor.Replace(mstrSepDecRepresentar, mstrSepDecConfReg)

            'obtengo la cantidad de posiciones enteras que tiene el número, esto nos servirá
            'porque, si hay mas de tres, habrá que agregar separadores de miles.
            shtMax = CType(InStr(1, pValor, mstrSepDecConfReg, CompareMethod.Binary), Short) - CType(1, Byte)
            If shtMax = -1 Then shtMax = CType(pValor.Length, Short)

            'si es menor o igual a 3, no se agregan separadores de miles
            'If shtMax <= 3 Then
            '    'si permitiemos el uso de decimales, formateamos el valor
            '    'a su forma con decimales (aunque deban ser cero).-
            '    If mblnUsarDecimales Then
            '        pValor = Format(CType(pValor, Double), "0." & ReplicarString("0", mbytCantDecimales))
            '    End If
            'Else
            'si el largo de enteros es mayor a 3, ns dedicamos a agregarle
            'separadores de miles.

            'para cada terna de numeros
            For shtI = shtMax To 3 Step -3
                'vamos construyendo la plantilla para los separadores de miles.
                strEnteros = strEnteros & ",###"
            Next shtI
            strEnteros = "#" & strEnteros

            ' si se decidió usar decimales, se ponen los decimales
            'necesarios aunque sean cero.
            If mblnUsarDecimales Then
                If IsNumeric(pValor) Then
                    pValor = Format(CType(pValor, Double), strEnteros & "." & ReplicarString("0", mbytCantDecimales))
                End If
            Else
                If IsNumeric(pValor) Then
                    pValor = Format(CType(pValor, Double), strEnteros)
                End If
            End If
            'End If
            pValor = ReemplazarxValoresARepresentar(pValor)

            Return pValor

        End Function

        Private Function ReemplazarxValoresARepresentar(ByVal pValor As String) As String
            pValor = pValor.Replace(mstrSepDecConfReg, "x")
            'cambiamos el separador de mil de la configuracion regional por el elegido a representar
            pValor = pValor.Replace(mstrSepMilConfReg, mstrSepMilRepresentar)
            'cambiamos el separador decimal por el separador decimal a representar
            pValor = pValor.Replace("x", mstrSepDecRepresentar)

            Return pValor
        End Function
        Public ReadOnly Property Text_ConFormato() As String
            Get
                If Me.mblnFormatearVisualizacion Then
                    Return Me.FormatearValor(MyBase.Text)
                Else
                    Return MyBase.Text
                End If
            End Get
        End Property
        '''<summary> Repite una cadena una determinada cantidad de veces
        ''' AL MEJOR ESTILO "REPLICATE" de Fox o Clipper </summary>
        '''<param name="Cadena">La cadena a repetir</param>
        '''<param name="Cantidad">La cantidad de veces que se repetira esa cadena</param>
        '''<returns> Devuelve una cadena de caracteres creada con la cantidad indicada 
        '''de repeticiones de la cadena de caracteres pasada por referencia</returns>
        Private Function ReplicarString(ByVal Cadena As String, ByVal Cantidad As Integer) As String
            ' Crea un objeto StringBuilder
            Dim sb As New System.Text.StringBuilder(Cadena.Length * Cantidad)
            Dim lngI As Integer

            For lngI = 1 To Cantidad
                sb.Append(Cadena)
            Next
            Return sb.ToString()
            sb = Nothing
        End Function
        Protected Overrides Sub OnValidating(ByVal e As System.ComponentModel.CancelEventArgs)
            If Me.Validar() Then
                'Si la validación es correcta, permite que la clase base
                'desencadene el suceso validating
                MyBase.OnValidating(e)
            Else
                e.Cancel = True
            End If
        End Sub
        Public Overloads Property Enabled() As Boolean
            Get
                Return blnEnabled
            End Get
            Set(ByVal value As Boolean)
                blnEnabled = value
                If Not value Then
                    MyBase.BackColor = clrBackColorEnabled
                Else
                    MyBase.BackColor = Color.White
                End If
                MyBase.Enabled = blnEnabled
            End Set
        End Property

        Public Overrides Property Text() As String
            Get
                'Cuando queremos leer el control, quitamos todo separador de mil
                'y retornamos el valor de acuerdo a lo soportado por la conf. regional
                Dim strValor As String
                strValor = MyBase.Text
                Select Case enuTipoDato
                    Case TipoDatos.Numerico
                        strValor = (strValor.Replace(mstrSepMilRepresentar, ""))
                        strValor = strValor.Replace(mstrSepDecRepresentar, mstrSepDecConfReg)
                    Case TipoDatos.Fecha
                    Case TipoDatos.Alfanumerico
                End Select
                Return strValor
            End Get
            Set(ByVal Value As String)
                Select Case enuTipoDato
                    Case TipoDatos.Numerico
                        If Me.Enabled Then
                            MyBase.BackColor = Drawing.Color.White
                        Else
                            MyBase.BackColor = clrBackColorEnabled
                        End If
                        'If MyBase.Enabled Then
                        '    MyBase.BackColor = Drawing.Color.White
                        'End If
                        Value = Value.Trim
                        If Value.Length = 0 Then
                            MyBase.MaxLength = Me.mintMaxLenght
                            MyBase.Text = ""
                        Else
                            If IsNumeric(Value) Then 'Viene con formato de configuracion regional
                                'Reemplazo por valores de representación 
                                Value = ReemplazarxValoresARepresentar(Value)
                            Else
                                'Si el string que se recibe no es un número, verifica si viene 
                                'con el formato de representación
                                Value = Value.Replace(mstrSepMilRepresentar, "")
                                Value = Value.Replace(mstrSepDecRepresentar, mstrSepDecConfReg)
                                If IsNumeric(Value) Then
                                    'Vuelve al estado de representación
                                    Value = ReemplazarxValoresARepresentar(Value)
                                Else
                                    'Si no es un número en lugar de tirar error, lo muestra y establece 
                                    'el backcolor en rojo
                                    If Not Value = MyBase.Name Then 'Cuando se agrega al formulario establece NumConfRegional1
                                        MyBase.BackColor = Drawing.Color.Red
                                    End If
                                End If
                            End If
                            MyBase.MaxLength = Value.Length
                        End If
                    Case TipoDatos.Fecha
                    Case TipoDatos.Alfanumerico
                End Select
                MyBase.Text = Value
            End Set
        End Property
        ''' <summary>
        ''' Retorna la expresión regular seleccionada
        ''' </summary>
        ''' <returns>Expresión regular</returns>
        ''' <remarks></remarks>
        Public Function RetornarExpresionRegular() As String
            Dim strExpresionRegular As String = ""
            Select Case enuExpresionRegularValidacion
                Case ExpresionesRegulares.Fecha
                    'Fecha en Formato DD/MM/AAAA
                    strExpresionRegular = "(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Case ExpresionesRegulares.NumerosEntero
                    'Numeros enteros positivos y negativos según permita o no negativos el control
                    If mblnPermitirNegativos Then
                        strExpresionRegular = "(^[0]{1}$|^[-]?[1-9]{1}\d*$)"
                    Else
                        strExpresionRegular = "(^[0]{1}$|^[1-9]{1}\d*$)"
                    End If
                Case ExpresionesRegulares.NumerosDecimales
                    If mblnPermitirNegativos Then
                        strExpresionRegular = "(^-?\d*((?<=\d)|[\" & mstrSepDecRepresentar.Trim & "](?=\d))\d*$)"
                    Else
                        strExpresionRegular = "(^\d*((?<=\d)|[\" & mstrSepDecRepresentar.Trim & "](?=\d))\d*$)"
                    End If
            End Select
            Return strExpresionRegular
        End Function
        Private Sub SetearError(ByVal Mensaje As String)
            ErrorProvider.SetError(Me, Mensaje)
        End Sub
#End Region

    End Class
End Namespace
