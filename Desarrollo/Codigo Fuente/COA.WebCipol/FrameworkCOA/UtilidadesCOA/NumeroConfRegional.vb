Namespace Controles

    ''' <summary>
    ''' Control extendido de tipo TextBox que provee funcionalidad necesaria 
    ''' para el manejo de numeros enteros y decimales.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NumeroConfRegional
        Inherits System.Windows.Forms.TextBox

        Private mstrSepDecConfReg As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        Private mbytCantDecimales As Byte = 2
        Private mblnUsarDecimales As Boolean = True
        Private mblnPermitirNegativos As Boolean = False
        Private mstrSepMilConfReg As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        Private mblnFormatearVisualizacion As Boolean = False
        Private mstrSepDecRepresentar As String = ","c
        Private mstrSepMilRepresentar As String = "."c
        Private mintMaxLenght As Integer

        ''' <summary>
        ''' Obtiene o establece el caracter separador de decimales
        ''' </summary>
        ''' <value>Caracter separador de decimales</value>
        ''' <returns>Caracter separador de decimales</returns>
        Property RepresentarSepDecCon() As Char
            Get
                Return System.Convert.ToChar(mstrSepDecRepresentar)
            End Get
            Set(ByVal Value As Char)
                mstrSepDecRepresentar = Value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el máximo número de caracteres 
        ''' que el usuario puede tipear o pegar dentro del control text box.
        ''' </summary>
        ''' <value>Máximo número de caracteres 
        ''' que el usuario puede tipear o pegar dentro del control text box.</value>
        ''' <returns>Máximo número de caracteres 
        ''' que el usuario puede tipear o pegar dentro del control text box.</returns>
        Overrides Property MaxLength() As Integer
            Get
                Return mintMaxLenght
            End Get
            Set(ByVal Value As Integer)
                mintMaxLenght = Value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el caracter separador de miles
        ''' </summary>
        ''' <value>Caracter separador de miles</value>
        ''' <returns>Caracter separador de miles</returns>
        Property RepresentarSepMilCon() As Char
            Get
                Return System.Convert.ToChar(mstrSepMilRepresentar)
            End Get
            Set(ByVal Value As Char)
                mstrSepMilRepresentar = Value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece la cantidad de decimales permitidas dentro del control text box
        ''' </summary>
        ''' <value>Cantidad de decimales permitidas dentro del control text box</value>
        ''' <returns>Cantidad de decimales permitidas dentro del control text box</returns>
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

        ''' <summary>
        ''' Obtiene o establece la propiedad que indica si se permiten decimales o no
        ''' dentro del control text box
        ''' </summary>
        ''' <value>Indica si se permiten decimales o no dentro del control text box</value>
        ''' <returns>Indica si se permiten decimales o no dentro del control text box</returns>
        Property PermitirDecimales() As Boolean
            Get
                Return mblnUsarDecimales
            End Get
            Set(ByVal Value As Boolean)
                mblnUsarDecimales = Value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establce la propiedad que indica si se permiten valores negativos
        ''' dentro del control text box
        ''' </summary>
        ''' <value>Indica si se permiten valores negativos dentro del control text box</value>
        ''' <returns>Indica si se permiten valores negativos dentro del control text box</returns>
        Property PermitirValoresNegativos() As Boolean
            Get
                Return mblnPermitirNegativos
            End Get
            Set(ByVal Value As Boolean)
                mblnPermitirNegativos = Value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establce la propiedad que indica si se debe formatear el valor 
        ''' dentro del control text box
        ''' </summary>
        ''' <value>Indica si se debe formatear el valor dentro del control text box</value>
        ''' <returns>Indica si se debe formatear el valor dentro del control text box</returns>
        Property FormatearVisualizacion() As Boolean
            Get
                Return mblnFormatearVisualizacion
            End Get
            Set(ByVal Value As Boolean)
                mblnFormatearVisualizacion = Value
            End Set
        End Property

#Region " Código generado por el Diseñador de Windows Forms "

        Public Sub New()
            MyBase.New()

            'El Diseñador de Windows Forms requiere esta llamada.
            InitializeComponent()

            'Agregar cualquier inicialización después de la llamada a InitializeComponent()
            mstrSepDecConfReg = CType(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, Char)
            mstrSepMilConfReg = CType(System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator, Char)
        End Sub

        'UserControl1 reemplaza a Dispose para limpiar la lista de componentes.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            '
            'NumeroConfRegional
            '
            Me.Size = New System.Drawing.Size(232, 20)

        End Sub

#End Region

        ''' <summary>
        ''' Captura el evento que se dispara cuando se presiona una tecla dentro del control text box
        ''' </summary>
        ''' <param name="sender">N/A</param>
        ''' <param name="e">N/A</param>
        ''' <remarks></remarks>
        Private Sub NumeroConfRegional_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' aCant         : Array de string con el contenido dentro del textbox
            ' strContenido  : String con el contenido dentro del textbox
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim aCant() As String
            Dim strContenido As String

            'Verifico que solo se ingresen números o símbolo negativo "-"
            If InStr(1, "0123456789." & CType(IIf(mblnPermitirNegativos, "-", String.Empty), String) & Chr(System.Windows.Forms.Keys.Back) & Chr(System.Windows.Forms.Keys.Return), e.KeyChar, CompareMethod.Binary) = 0 Then
                e.Handled = True
            Else
                If e.KeyChar = "." Then ' "." del teclado numérico
                    'Si se permiten decimales, y el separador decimal de la configuración
                    'regional ya existe
                    If mblnUsarDecimales Then
                        If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) > 0 Then e.Handled = True
                    Else
                        e.Handled = True
                    End If
                End If
                'Si se ingresa símbolo negativo, y ya existen valores en la caja 
                'de texto
                If e.KeyChar = "-" And Len(MyBase.Text) > 0 Then e.Handled = True
            End If

            If Not e.Handled Then
                'Si no se presionó la tecla BackSpace o Enter
                If Not (e.KeyChar = Chr(System.Windows.Forms.Keys.Back) Or e.KeyChar = Chr(System.Windows.Forms.Keys.Return)) Then
                    ' Si se presiono la tecla "." del teclado numérico
                    If e.KeyChar = "." Then
                        MyBase.Text = MyBase.Text & mstrSepDecRepresentar
                        MyBase.SelectionStart = Len(MyBase.Text)
                        e.Handled = True
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
                                            If Len(aCant(1)) > mbytCantDecimales Then e.Handled = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        End Sub

        ''' <summary>
        ''' Evento que se produce cuando el usuario sale del control text box
        ''' </summary>
        ''' <param name="sender">N/A</param>
        ''' <param name="e">N/A</param>
        ''' <remarks></remarks>
        Private Sub NumeroConfRegional_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            'Si se ingreso una longitud máxima de digitos y la propiedad Decimales = True
            'verifica que la parte entera del número no supere la cantidad de digitos
            'permitidos - la cantidad de decimales
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            'lngLen         : Longitud de parte entera del número
            'lngPerm        : Cantidad de dígitos permitidos
            'strValor       : Valor a formatear
            'strExpNegativo : Expresion que utilizada para verificar si el numero es neg.
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim lngLen As Integer, lngPerm As Integer, strValor As String, intPosComa As Integer
            Const strExpNegativo As String = "[-]?"
            Dim blnError As Boolean = False

            If MyBase.Text.Length = 0 Then Return

            'GCP-Cambios ID: 6127
            If Not mblnUsarDecimales Then
                If Me.mblnPermitirNegativos Then
                    blnError = System.Text.RegularExpressions.Regex.IsMatch(MyBase.Text, "[^\-0-9]")
                Else
                    blnError = System.Text.RegularExpressions.Regex.IsMatch(MyBase.Text, "[^0-9]")
                End If
                If blnError Then
                    System.Windows.Forms.MessageBox.Show("Valor incorrecto", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    MyBase.Focus()
                    Return
                End If
            Else
                If Strings.InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) > 0 Then
                    If Not System.Text.RegularExpressions.Regex.IsMatch(MyBase.Text, "^" & IIf(Me.mblnPermitirNegativos, strExpNegativo, "").ToString & "[0-9]+\,[0-9]+$") Then
                        System.Windows.Forms.MessageBox.Show("Valor incorrecto", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        MyBase.Focus()
                        Return
                    End If
                    If Not System.Text.RegularExpressions.Regex.IsMatch(MyBase.Text, "^" & IIf(Me.mblnPermitirNegativos, strExpNegativo, "").ToString & "[0-9]+\,[0-9]{1," & mbytCantDecimales & "}$") Then
                        If MyBase.MaxLength > 0 Then
                            ' ---- Fragmento de codigo copiado de lo anterior ----
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
                            ' ---- Fragmento de codigo copiado de lo anterior ----
                        Else
                            intPosComa = InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary)
                            If intPosComa > 0 AndAlso mbytCantDecimales > 0 Then
                                System.Windows.Forms.MessageBox.Show("Se permiten " & mbytCantDecimales.ToString & " decimales como máximo.", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            Else
                                System.Windows.Forms.MessageBox.Show("Valor incorrecto", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            End If
                            MyBase.Focus()
                            Return
                        End If
                    End If
                Else
                    If Me.mblnPermitirNegativos Then
                        blnError = System.Text.RegularExpressions.Regex.IsMatch(MyBase.Text, "[^\-0-9]")
                    Else
                        blnError = System.Text.RegularExpressions.Regex.IsMatch(MyBase.Text, "[^0-9]")
                    End If
                    If blnError Then
                        System.Windows.Forms.MessageBox.Show("Valor incorrecto", "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        MyBase.Focus()
                        Return
                    End If
                End If
            End If

            'If MyBase.MaxLength > 0 And mblnUsarDecimales And MyBase.Text <> "" Then
            '    If InStr(1, MyBase.Text, mstrSepDecRepresentar, CompareMethod.Binary) = 0 Then
            '        lngLen = Len(MyBase.Text)
            '    Else
            '        lngLen = MyBase.Text.Substring(0, MyBase.Text.IndexOf(mstrSepDecRepresentar)).Length
            '    End If
            '    lngPerm = MyBase.MaxLength - mbytCantDecimales - 1 'Elimino 1 por el separador decimal
            '    If lngLen > lngPerm Then
            '        MsgBox("Valor incorrecto. (máx. " & ReplicarString("9", lngPerm) & mstrSepDecRepresentar & ReplicarString("9", mbytCantDecimales) & ").", MsgBoxStyle.Exclamation, "Error")
            '        MyBase.Focus()
            '        Exit Sub
            '    End If
            'End If
            'Si se debe formatear el valor
            If mblnFormatearVisualizacion Then
                strValor = MyBase.Text
                strValor = FormatearValor(strValor)
                'Agrando el maxlength de la caja de texto por el formateo
                MyBase.MaxLength = strValor.Length
                MyBase.Text = strValor
            End If
        End Sub

        ''' <summary>
        ''' Formatea el valor recibido
        ''' </summary>
        ''' <param name="pValor">Valor a formatear</param>
        ''' <returns>Número formateado</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [?]          [04/01/2003]       Creado
        ''' </history>
        Private Function FormatearValor(ByVal pValor As String) As String
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '              DESCRIPCION DE VARIABLES LOCALES
            'strEnteros : Formato de la parte entera del número
            'shtI       : Contador del For
            'shtMax     : Cantidad máxima de caracteres de la parte entera
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strEnteros As String = String.Empty, shtI As Short
            Dim shtMax As Short

            If pValor.Length = 0 Then Return ""

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
                pValor = Format(CType(pValor, Double), strEnteros & "." & ReplicarString("0", mbytCantDecimales))
            Else
                pValor = Format(CType(pValor, Double), strEnteros)
            End If
            'End If
            pValor = ReemplazarxValoresARepresentar(pValor)

            Return pValor

        End Function

        ''' <summary>
        ''' Reemplaza valores dentros del control por la configuracion regional
        ''' </summary>
        ''' <param name="pValor">Texto a reemplazar</param>
        ''' <returns>Texto con los valores reemplazados por la configuracion regional</returns>
        ''' <remarks></remarks>
        Private Function ReemplazarxValoresARepresentar(ByVal pValor As String) As String
            pValor = pValor.Replace(mstrSepDecConfReg, "x")
            'cambiamos el separador de mil de la configuracion regional por el elegido a representar
            pValor = pValor.Replace(mstrSepMilConfReg, mstrSepMilRepresentar)
            'cambiamos el separador decimal por el separador decimal a representar
            pValor = pValor.Replace("x", mstrSepDecRepresentar)

            Return pValor

        End Function

        '''<summary>Repite una cadena una determinada cantidad de veces
        ''' AL MEJOR ESTILO "REPLICATE" de Fox o Clipper</summary>
        '''<param name="Cadena">La cadena a repetir</param>
        '''<param name="Cantidad">La cantidad de veces que se repetira esa cadena</param>
        '''<returns>Devuelve una cadena de caracteres creada con la cantidad indicada 
        '''de repeticiones de la cadena de caracteres pasada por referencia</returns>
        Private Function ReplicarString(ByVal Cadena As String, ByVal Cantidad As Integer) As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' sb   : StringBuilder que contiene el string replicado
            ' lngI : Contador del for
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim sb As New System.Text.StringBuilder(Cadena.Length * Cantidad)
            Dim lngI As Integer

            For lngI = 1 To Cantidad
                sb.Append(Cadena)
            Next
            Return sb.ToString()
            sb = Nothing
        End Function

        ''' <summary>
        ''' Formatea el numero a su forma sin separadores de miles cuando el control recive foco
        ''' </summary>
        ''' <param name="sender">N/A</param>
        ''' <param name="e">N/A</param>
        ''' <remarks></remarks>
        Private Sub NumeroConfRegional_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                DESCRIPCION DE LAS VARIABLES LOCALES
            ' strValor      : string que contendra el valor del campo de texto
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strValor As String

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

        End Sub

        ''' <summary>
        ''' Obtiene o establece el texto del control text box con la particularidad de:
        ''' - Al leer el texto del control se quita todo separador de miles
        '''   y retorna el valor de acuerdo a lo soportado por la configuracion regional.
        ''' - Al establecer el valor verifica que cumpla con el formato correcto.
        ''' </summary>
        ''' <value>Texto del control text box.</value>
        ''' <returns>Texto del control text box.</returns>
        Public Overrides Property Text() As String
            Get
                'Cuando queremos leer el control, quitamos todo separador de mil
                'y retornamos el valor de acuerdo a lo soportado por la conf. regional
                Dim strValor As String
                strValor = MyBase.Text
                strValor = (strValor.Replace(mstrSepMilRepresentar, ""))
                strValor = strValor.Replace(mstrSepDecRepresentar, mstrSepDecConfReg)
                Return strValor
            End Get
            Set(ByVal Value As String)

                MyBase.BackColor = Drawing.Color.White
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
                    MyBase.Text = Value
                End If

            End Set
        End Property

        ''' <summary>
        ''' Obtiene el valor del text box formateado
        ''' </summary>
        ''' <value>Valor del text box formateado</value>
        ''' <returns>Valor del text box formateado</returns>
        Public ReadOnly Property Text_ConFormato() As String
            Get
                If Me.mblnFormatearVisualizacion Then
                    Return Me.FormatearValor(MyBase.Text)
                Else
                    Return MyBase.Text
                End If
            End Get
        End Property

    End Class

End Namespace