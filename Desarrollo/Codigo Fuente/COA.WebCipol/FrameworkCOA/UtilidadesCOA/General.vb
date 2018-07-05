Option Explicit On 
Option Strict On

''' <summary>
''' Contiene valores del entorno de la aplicacion, configuracion y funciones
''' de apoyo.
''' </summary>
Public Class General


#Region "Variables Privadas"
    Private mintIDSistemaCIPOL As Short
    Private mstrUsuarioCIPOL As String
    Private mstrNombreSucursal As String = ""
    Private mdtmHoy As Date = Now.Date
    Private mstrTitulo As String = ""
    Private mstrVersion As String = ""
    Private mstrRutaAplicacion As String = ""
    Private mstrRutaIniLocal As String = ""
    Private mstrAcercaDeDescripcion As String
    Private mstrAcercaDeDetalle As String
    Private mstrAcercaDeLogo As String
    Private mstrAcercaDeCliente As String
    Private mstrAcercaDeIcono As String

    Private Cifra As String

#End Region

#Region "Propiedades"

    Private Shared mobjSerializar As New Serializar
    Public Shared ReadOnly Property Serializar() As COA.Serializar
        Get
            Return mobjSerializar
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de hoy
    ''' </summary>
    ''' <value>Fecha de hoy</value>
    ''' <returns>Fecha de hoy</returns>
    Public Property Hoy() As Date
        Get
            Return mdtmHoy
        End Get
        Set(ByVal Value As Date)
            mdtmHoy = Value.Date
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ID del sistema CIPOL
    ''' </summary>
    ''' <value>Valor que representa el ID del sistema CIPOL</value>
    ''' <returns>ID del sistema CIPOL</returns>
    Public Property IDSistemaCIPOL() As Short
        Get
            Return Me.mintIDSistemaCIPOL
        End Get
        Set(ByVal Value As Short)
            Me.mintIDSistemaCIPOL = Value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el usuario del Login de CIPOL
    ''' </summary>
    ''' <value>Valor que representa el usuario CIPOL del Login</value>
    ''' <returns>Usuario CIPOL del Login</returns>
    Public Property UsuarioCIPOL() As String
        Get
            Return Me.mstrUsuarioCIPOL
        End Get
        Set(ByVal Value As String)
            Me.mstrUsuarioCIPOL = Value.Trim
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el nombre del sistema
    ''' </summary>
    ''' <returns>Nombre del sistema actual</returns>
    Public Property NombreSistema() As String
        Get
            Return mstrTitulo
        End Get
        Set(ByVal value As String)
            mstrTitulo = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el nombre NetBIOS de la Computadora
    ''' </summary>
    ''' <returns>Nombre NetBIOS de la Computadora</returns>
    Public ReadOnly Property Computadora() As String
        Get
            Return System.Environment.MachineName
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la version del producto
    ''' </summary>
    ''' <returns>Version del producto</returns>
    Public ReadOnly Property Version() As String
        Get
            Return mstrVersion
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el nombre de usuario de la persona que esta actualmente logeada en 
    ''' el Sistema Operativo Windows
    ''' </summary>
    ''' <returns>Nombre de usuario de la persona que esta actualmente logeada en el Sistema Operativo Windows</returns>
    Public ReadOnly Property LoginRed() As String
        Get
            Return System.Environment.UserName
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la ruta de la aplicación que se esta ejecutando
    ''' </summary>
    ''' <returns>Ruta de la aplicación que se esta ejecutando</returns>
    Public ReadOnly Property RutaAplicacion() As String
        Get
            Return Me.mstrRutaAplicacion
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la ruta donde se encuentran los reportes relacionados con la aplicación
    ''' </summary>
    ''' <returns>Ruta donde se encuentran los reportes relacionados con la aplicación</returns>
    Public ReadOnly Property RutaReportes() As String
        Get
            Return Me.mstrRutaAplicacion & "Reportes\"
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la ruta de los archivos de la aplicación
    ''' </summary>
    ''' <returns>Ruta de los archivos de la aplicación</returns>
    Public ReadOnly Property RutaArchivos() As String
        Get
            Return Me.mstrRutaAplicacion & "Archivos\"
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la ruta inicial local
    ''' </summary>
    ''' <returns>Ruta inicial local</returns>
    Public Property RutaIniLocal() As String
        Get
            If mstrRutaIniLocal.Equals(String.Empty) Then
                Return Me.mstrRutaAplicacion
            Else
                Return Me.mstrRutaIniLocal
            End If
        End Get
        Set(ByVal Value As String)
            If System.IO.File.Exists(Value) Then
                Me.mstrRutaIniLocal = Value
            Else
                Throw New Exception("Ruta del archivo incorrecta." & vbCrLf & Value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción de la aplicación
    ''' </summary>
    ''' <value>Descripción de la aplicación</value>
    ''' <returns>Descripción de la aplicación</returns>
    Public Property AcercaDe_Descripcion() As String
        Get
            Return Me.mstrAcercaDeDescripcion
        End Get
        Set(ByVal Value As String)
            Me.mstrAcercaDeDescripcion = Value.Trim
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción detallada de la aplicación
    ''' </summary>
    ''' <value>Descripción detallada de la aplicación</value>
    ''' <returns>Descripción detallada de la aplicación</returns>
    Public Property AcercaDe_Detalle() As String
        Get
            Return Me.mstrAcercaDeDetalle
        End Get
        Set(ByVal Value As String)
            Me.mstrAcercaDeDetalle = Value.Trim
        End Set
    End Property

    ''' <summary>
    ''' Obienene o establece el logo que representa el aplicativo
    ''' </summary>
    ''' <value>Logo que representa el aplicativo</value>
    ''' <returns>Logo que representa el aplicativo</returns>
    Public Property AcercaDe_Logo() As String
        Get
            Return Me.mstrAcercaDeLogo
        End Get
        Set(ByVal Value As String)
            If System.IO.File.Exists(Value) Then
                Me.mstrAcercaDeLogo = Value
            Else
                Throw New Exception("Ruta del archivo incorrecta." & vbCrLf & Value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los datos del cliente
    ''' </summary>
    ''' <value>Datos del cliente</value>
    ''' <returns>Datos del cliente</returns>
    Public Property AcercaDe_Cliente() As String
        Get
            Return Me.mstrAcercaDeCliente
        End Get
        Set(ByVal Value As String)
            Me.mstrAcercaDeCliente = Value.Trim
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el icono que representa la aplicación
    ''' </summary>
    ''' <value>Icono que representa la aplicación</value>
    ''' <returns>Icono que representa la aplicación</returns>
    Public Property AcercaDe_Icono() As String
        Get
            Return Me.mstrAcercaDeIcono
        End Get
        Set(ByVal Value As String)
            If System.IO.File.Exists(Value) Then
                Me.mstrAcercaDeIcono = Value
            Else
                Throw New Exception("Ruta del archivo incorrecta." & vbCrLf & Value)
            End If
        End Set
    End Property

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Retorna un cadena string con el pie de pagina estandar,
    ''' seteado a la formula del reporte rptPiePagina
    ''' Compatibilidad: Debe estar definida en el proyecto la variable gobjSql (instancia de la clase AccesoDB)
    ''' </summary>
    ''' <returns>Formula de Pie de página</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [02/09/2004]       Creado
    ''' </history>
    Public Function rptPiePagina() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE VARIABLES LOCALES
        'sblPiePag  : String Builder para armar el pie de página
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblPiePag As New System.Text.StringBuilder

        With sblPiePag
            .Append(mstrVersion) : .Append(" - ")
            .Append(Me.mstrUsuarioCIPOL) : .Append(" - ")
            .Append(System.Environment.MachineName) : .Append(" - ")
            .Append(String.Format("{0:dd/MM/yyyy} {1:HH:mm:ss}", Me.mdtmHoy, Now))

            Return .ToString
        End With

        sblPiePag = Nothing

    End Function

    ''' <summary>
    ''' Determina si el exe ya se encuentra ejecutando
    ''' </summary>
    ''' <returns>True si existe una instancia previa de la aplicacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [02/09/2004]       Creado
    ''' </history>
    Public Function PrevInstance() As Boolean

        If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Retorna la lista de los procesos activos
    ''' </summary>
    ''' <returns>Array de procesos</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [31/01/2003]       Creado
    ''' [GustavoM]          [26/07/2004]       Reemplazo de APIs x .NET Framework
    ''' </history>
    Public Function CrearListaProcesos() As String()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                  DESCRIPCION DE VARIABLES LOCALES
        'objProcesos    : Matriz que se utiliza para retornar los nombres
        '                 de los procesos activos
        'strNombreProc  : Array que contiene la lista de procesos a retornar
        'intI           : Contador del For
        'intHasta       : Límite final del contador For
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objProcesos() As System.Diagnostics.Process
        Dim strNombreProc(0) As String, intI As Integer, intHasta As Integer

        objProcesos = System.Diagnostics.Process.GetProcesses
        intHasta = objProcesos.GetUpperBound(0)
        ReDim strNombreProc(intHasta)

        For intI = 0 To intHasta
            strNombreProc(intI) = objProcesos(intI).ProcessName
        Next

        Return strNombreProc

    End Function

    ''' <summary>
    ''' Verifica si un fecha es valida
    ''' </summary>
    ''' <param name="Fecha">Fecha que debe verificarse</param>
    ''' <returns>True si la fecha es correcta, False en caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [15/08/2001]       Creado
    ''' </history>
    Public Function FechaCorrecta(ByVal Fecha As String) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE VARIABLES LOCALES
        'intDia : Dia de la fecha
        'intMes : Mes de la fecha
        'intAño : Año de la fecha
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim intDia, intMes As Integer
        Dim intAño As Integer

        FechaCorrecta = False
        Fecha = Trim(Fecha)

        If Fecha = "__/__/____" Then Exit Function
        If Fecha = "" Then Exit Function
        If Len(Fecha) < 10 Then Exit Function
        If Mid(Fecha, 3, 1) <> "/" Then Exit Function
        If Mid(Fecha, 6, 1) <> "/" Then Exit Function
        If InStr(1, Fecha, "_", CompareMethod.Binary) > 0 Then Exit Function

        intDia = CInt(Mid(Fecha, 1, 2))
        intMes = CInt(Mid(Fecha, 4, 2))
        intAño = CInt(Mid(Fecha, 7, 4))
        Select Case intMes
            Case 1 To 12
            Case Else : Exit Function
        End Select
        Select Case intAño
            Case Is < 1753 : Exit Function
        End Select
        Select Case intDia
            Case 1 To 31
            Case Else : Exit Function
        End Select
        'Si la conversión de la fecha retorna un día posterior, se debe a que 
        'la fecha es incorrecta. Por ej. 31/09/2006 retorna 01/10/2006
        If String.Format("{0:dd/MM/yyyy}", DateSerial(intAño, intMes, intDia)) <> Fecha Then
            Exit Function
        End If

        FechaCorrecta = True

    End Function

    ''' <summary>
    ''' Indica si la Computadora posee impresoras instaladas
    ''' </summary>
    ''' <returns>True si la computadora posee impresoras instaladas, False en caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [viernes, 14 de noviembre de 2008]       Creado
    ''' </history>
    Public Function ImpresorasInstaladas() As Boolean
        If System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Verifica si un año es bisiesto
    ''' </summary>
    ''' <param name="Numero">Año a verificar</param>
    ''' <returns>True si el año es bisiesto, False en caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [15/08/2001]       Creado
    ''' </history>
    Private Function Bisiesto(ByVal Numero As Integer) As Boolean

        If Numero Mod 100 = 0 Then
            If Numero Mod 400 = 0 Then Return True
        Else
            If Numero Mod 4 = 0 Then Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' Obtiente la ruta del directorio de Windows
    ''' </summary>
    ''' <returns>Directorio local Windows</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          Creado
    ''' </history>
    Public Function GetWindowsDir() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        ' strRet : Valor de retorno del metodo
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strRet As String

        strRet = System.Environment.GetFolderPath(Environment.SpecialFolder.System)
        strRet = strRet.Substring(0, strRet.LastIndexOf("\"c))

        Return strRet

    End Function

    ''' <summary>
    ''' Obtiene el path del directorio System de Windows
    ''' </summary>
    ''' <returns>Path del directorio System de Windows</returns>
    ''' <remarks></remarks>
    Public Function GetWindowsSysDir() As String
        Return System.Environment.GetFolderPath(Environment.SpecialFolder.System)
    End Function

    ''' <summary>
    ''' Obtiene el nombre de usuario logueado en la máquina
    ''' </summary>
    ''' <returns>Nombre del usuario, vacío si no se pudo obtener</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [31/08/2001]       Creado
    ''' [GustavoM]          [26/07/2004]       Reemplazo de API x .NET Framework
    ''' </history>
    Private Function ObtenerUsuario() As String
        Return System.Environment.UserName
    End Function

    ''' <summary>
    ''' Determina el sistema operativo de 32 bits instalado
    ''' </summary>
    ''' <returns>Sistema operativo. Blanco si no se pudo recuperar el sistema operativo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [29/08/2001]       Creado
    ''' </history>
    Public Function SistemaOperativo() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'strRet : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strRet As String = String.Empty

        Select Case System.Environment.OSVersion.Platform
            Case PlatformID.Win32NT
                Select Case System.Environment.OSVersion.Version.Minor
                    Case 3
                        strRet = "Windows NT 3.51"
                    Case 4
                        strRet = "Windows NT 4.0"
                    Case 5
                        strRet = "Windows 2000"
                End Select
            Case PlatformID.Win32Windows
                Select Case System.Environment.OSVersion.Version.Minor
                    Case 0
                        strRet = "Windows 95"
                    Case 10
                        strRet = "Windows 98"
                    Case 90
                        strRet = "Windows Millennium"
                End Select
        End Select

        Return strRet


    End Function

    ''' <summary>
    ''' Obtiene el nombre de la computadora
    ''' </summary>
    ''' <returns>Nombre de la computadora</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          
    ''' </history>
    Private Function ObtenerNombreComputadora() As String
        Return System.Environment.MachineName
    End Function

    ''' <summary>
    ''' Incializa los valores de las propiedades
    ''' </summary>
    ''' <param name="Ensamblado"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [02/09/2004]       Creado
    ''' </history>
    Public Sub New(ByVal Ensamblado As System.Reflection.Assembly)
        MyBase.New()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objAtributo : Atributo del ensamlado que se utiliza para obtener el título
        '              del sistema
        'sblVersion  : String Builder de Version
        'strPath    : Ubicación del ensamblado que inicia el proceso
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objAtributo As System.Attribute, sblVersion As New System.Text.StringBuilder
        Dim strPath As String

        objAtributo = System.Reflection.AssemblyTitleAttribute.GetCustomAttribute(Ensamblado, GetType(System.Reflection.AssemblyTitleAttribute))
        mstrTitulo = CType(objAtributo, System.Reflection.AssemblyTitleAttribute).Title

        strPath = Ensamblado.Location

        mdtmHoy = Now.Date
        With sblVersion
            .Append(mstrTitulo)
            .Append(" v. ")
            .Append(System.Diagnostics.FileVersionInfo.GetVersionInfo(strPath).FileVersion)
            Me.mstrVersion = .ToString
        End With
        sblVersion = Nothing

        Me.mstrRutaAplicacion = strPath.Substring(0, strPath.LastIndexOf("\"c) + 1)


    End Sub

    ''' <summary>
    ''' Obtiene los caracteres de corte
    ''' </summary>
    ''' <returns>Caracteres de corte</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FormatearString_CaracteresDeCorte() As String
        Get
            Return "; :/-º.;§"
        End Get
    End Property

    ''' <summary>
    ''' Formatea un string de acuerdo a un ancho determinado y a un simbolo determinado
    ''' </summary>
    ''' <param name="tcStrFormatear">String a convertir</param>
    ''' <param name="tnAncho">Ancho de linea</param>
    ''' <param name="tcSimb">Simbolo de finalizacion de linea</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM] Creado
    ''' </history>
    Public Sub FormatearString(ByRef tcStrFormatear As String, _
                               ByVal tnAncho As Byte, ByVal tcSimb As Char)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ' lnI            : Contador del For
        ' laRenglon      : Array que va a contener los renglones del string
        '                  recibido
        ' laLinea        : Array de lineas formateadas
        ' laCant         : Cantidad de Enter encontrados en el string
        ' lnJ            : Contador del For que se utiliza para armar las lineas
        '                  de acuerdo al ancho prefijado
        ' lnCont         : Contador de caracteres
        ' lnPosEspLinea  : Posicion del ultimo caracter de corte en la linea
        ' lnPosEsp       : Posicion del ultimo caracter de corte en el renglon
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim lnI As Integer, laRenglon() As String, laLinea() As String
        Dim laCant() As String, lnJ As Integer, lnCont As Byte
        Dim lnPosEspLinea As Integer, lnPosEsp As Integer

        Dim CaracteresdeCorte As String = FormatearString_CaracteresDeCorte

        tcStrFormatear = Trim(tcStrFormatear)
        ReDim laLinea(1)
        'Obtengo la cantidad de Enter y de acuerdo a eso determino
        'lo renglones que posee el string
        laCant = Split(tcStrFormatear, vbNewLine)
        ReDim laRenglon(UBound(laCant, 1) + 1)
        For lnI = 1 To UBound(laRenglon, 1)
            If InStr(tcStrFormatear, vbNewLine) <> 0 Then
                laRenglon(lnI) = Trim(Left(tcStrFormatear, InStr(tcStrFormatear, vbNewLine) - 1))
                tcStrFormatear = Trim(Mid(tcStrFormatear, InStr(tcStrFormatear, vbNewLine) + 2))
            Else
                laRenglon(lnI) = Trim(tcStrFormatear)
            End If
        Next lnI
        ' Recorro los renglones para formar las lineas
        For lnJ = 1 To UBound(laRenglon, 1)
            lnCont = 1
            For lnI = 1 To Len(laRenglon(lnJ))
                If lnCont = tnAncho Then
                    ' Verifico si el siguiente caracter es
                    ' espacio en blanco
                    If InStr(1, CaracteresdeCorte, Mid(laRenglon(lnJ), lnI, 1), vbTextCompare) > 0 Or InStr(1, CaracteresdeCorte, Mid(laRenglon(lnJ), lnI + 1, 1), vbTextCompare) > 0 Then

                        laLinea(UBound(laLinea, 1)) = Trim(laLinea(UBound(laLinea, 1)) & Mid(laRenglon(lnJ), lnI, 1))

                    Else
                        Select Case Mid(laRenglon(lnJ), lnI + 1, 1)
                            Case "" ' Si es igual al ancho
                                laLinea(UBound(laLinea, 1)) = Trim(laLinea(UBound(laLinea, 1)) & Mid(laRenglon(lnJ), lnI, 1))
                            Case " "
                                laLinea(UBound(laLinea, 1)) = Trim(laLinea(UBound(laLinea, 1)) & Mid(laRenglon(lnJ), lnI, 1))
                            Case Else
                                laLinea(UBound(laLinea, 1)) = Trim(Left(laLinea(UBound(laLinea, 1)), lnPosEspLinea))
                                lnI = lnPosEsp
                        End Select
                    End If
                    ReDim Preserve laLinea(UBound(laLinea, 1) + 1)
                    lnCont = 1
                Else
                    ' Verifico que no haya ningun principio de linea
                    laLinea(UBound(laLinea, 1)) = laLinea(UBound(laLinea, 1)) & Mid(laRenglon(lnJ), lnI, 1)
                    If InStr(1, CaracteresdeCorte, Mid(laRenglon(lnJ), lnI, 1), vbTextCompare) > 0 Then
                        lnPosEspLinea = Len(laLinea(UBound(laLinea, 1)))
                        lnPosEsp = lnI
                    End If
                    lnCont += CType(1, Byte)
                End If
            Next lnI
            ReDim Preserve laLinea(UBound(laLinea, 1) + 1)
        Next lnJ
        If Trim(laLinea(UBound(laLinea, 1))) = "" Then ReDim Preserve laLinea(UBound(laLinea, 1) - 1)
        ' Establezco los simbolos de terminación a las lineas
        ' y retorno el string
        tcStrFormatear = ""
        For lnI = 1 To UBound(laLinea, 1)
            If Trim(laLinea(lnI)) <> "" Then
                If Len(laLinea(lnI)) < tnAncho Then
                    laLinea(lnI) = Trim(laLinea(lnI))
                    laLinea(lnI) = laLinea(lnI) & New String(tcSimb, tnAncho - Len(laLinea(lnI)))
                End If
                tcStrFormatear &= laLinea(lnI) & vbNewLine
            End If
        Next lnI

    End Sub

    ''' <summary>
    ''' Transforma un string de formato tipo fecha a Date
    ''' </summary>
    ''' <param name="Fecha">String a convertir</param>
    ''' <returns>String que representa la Fecha</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [10/06/2003]       Creado
    ''' </history>
    Public Function StrFechaADate(ByVal Fecha As String) As Date
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'intI   : Contador del For
        'strHora: Parte de la hora a convertir
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim intI As Integer, strHora() As String

        If Fecha.Length < 10 Then Throw New Exception("El formato debe ser dd/MM/yyyy")

        StrFechaADate = DateSerial(CShort(Mid(Fecha, 7, 4)), CShort(Mid(Fecha, 4, 2)), CShort(Left(Fecha, 2)))
        'Si posee hora, minutos y segundos
        If Fecha.Length > 10 Then
            Fecha = Fecha.Substring(10)
            strHora = Split(Fecha, ":")
            For intI = 0 To strHora.GetUpperBound(0)
                Select Case intI
                    Case 0
                        StrFechaADate = StrFechaADate.AddHours(CType(strHora(intI), Double))
                    Case 1
                        StrFechaADate = StrFechaADate.AddMinutes(CType(strHora(intI), Double))
                    Case 2
                        StrFechaADate = StrFechaADate.AddSeconds(CType(strHora(intI), Double))
                    Case 3
                        StrFechaADate = StrFechaADate.AddMilliseconds(CType(strHora(intI), Double))
                End Select
            Next
        End If

    End Function

    ''' <summary>
    ''' Obtiene el literal de un numero
    ''' </summary>
    ''' <param name="Numero">Numero que se desea obtener el literal</param>
    ''' <returns>Literal que representa el numero</returns>
    ''' <remarks></remarks>
    Public Function PasarNroALiteral(ByVal Numero As Double) As String

        Dim COC As Double, Resto As Double, intVal As Integer
        Dim cent As String, vder As String, vizq As String
        Dim Separador() As String

        Cifra = ""

        If Numero < 0 Then
            Cifra = "menos "
            Numero = System.Math.Abs(Numero)
        End If

        intVal = CType(Int(Numero), Integer)

        Cifra = Cifra & CType(IIf(intVal > 0, "", "cero"), String)

        Resto = intVal Mod 1000000

        If intVal >= 1000000 Then
            COC = Int(intVal / 1000000)
            Call F1000(COC)
            If COC = 1 Then
                Cifra = Cifra + "millón "
            Else
                Cifra = Cifra + "millones "
            End If
        End If
        COC = Int(Resto / 1000)
        Resto = Resto Mod 1000

        Call F1000(COC)

        If COC > 0 Then
            Cifra = Cifra + "mil "
        End If

        Call F1000(Resto)

        If Resto Mod 10 = 1 And Resto <> 11 Then
            Cifra = Mid(Cifra, 1, Len(Cifra) - 1)
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Nueva Version (25/11/04): 
        ' Tomo los decimales en la variable cent.
        Separador = Split(CStr(Numero), System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
        If Separador.GetUpperBound(0) = 0 Then
            vizq = Separador(0)
            cent = "0"

        Else
            vizq = Separador(0)
            vder = Separador(1)
            cent = Left((vder + "0"), 2)

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Version anterior (25/11/04): Tenia problemas con cifras de un solo digito
        ' ej: "5" y con mas de dos decimales.
        'cent = Mid(CStr(Numero), Len(CStr(Numero)) - 1, 2)

        'vder = Right(cent, 1)
        'vizq = Left(cent, 1)
        'If vizq = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator Then
        '    cent = vder + "0"
        'End If

        If Numero > intVal Then
            Cifra = Cifra + " con " + cent + " ctvos "
        End If

        Cifra = Mid(Cifra, 1, Len(Cifra) - 1) + "."
        PasarNroALiteral = UCase(Cifra)

    End Function

    Private Sub F1000(ByVal Numero As Double)
        Dim COC As Integer, Resto As Double
        COC = CType(Int(Numero / 100), Integer)
        Resto = Numero Mod 100
        If COC > 0 Then
            If Resto > 0 OrElse COC > 1 Then
                Call Decenas(COC + 99)
            Else
                Cifra = Cifra + " cien "
            End If
        End If

        Call Decenas(CType(Resto, Integer))

    End Sub

    ''' <summary>
    ''' Obtiene el string que representa la decena
    ''' </summary>
    ''' <param name="Indice">Indice del arreglo para obtener la decena</param>
    ''' <remarks></remarks>
    Private Sub Decenas(ByVal Indice As Integer)
        Dim s(109) As String

        s(1) = "un  "
        s(2) = "dos "
        s(3) = "tres "
        s(4) = "cuatro "
        s(5) = "cinco "
        s(6) = "seis "
        s(7) = "siete "
        s(8) = "ocho "
        s(9) = "nueve "
        s(10) = "diez "
        s(11) = "once "
        s(12) = "doce "
        s(13) = "trece "
        s(14) = "catorce "
        s(15) = "quince "
        s(16) = "dieciseis "
        s(17) = "diecisiete "
        s(18) = "dieciocho "
        s(19) = "diecinueve "
        s(20) = "veinte "
        s(21) = "veintiun "
        s(22) = "veintidos "
        s(23) = "veintitres "
        s(24) = "veinticuatro "
        s(25) = "veinticinco "
        s(26) = "veintiseis "
        s(27) = "veintisiete "
        s(28) = "veintiocho "
        s(29) = "veintinueve "
        s(30) = "treinta "
        s(31) = "treinta y uno "
        s(32) = "treinta y dos "
        s(33) = "treinta y tres "
        s(34) = "treinta y cuatro "
        s(35) = "treinta y cinco "
        s(36) = "treinta y seis "
        s(37) = "treinta y siete "
        s(38) = "treinta y ocho "
        s(39) = "treinta y nueve "
        s(40) = "cuarenta "
        s(41) = "cuarenta y uno "
        s(42) = "cuarenta y dos "
        s(43) = "cuarenta y tres "
        s(44) = "cuarenta y cuatro "
        s(45) = "cuarenta y cinco "
        s(46) = "cuarenta y seis "
        s(47) = "cuarenta y siete "
        s(48) = "cuarenta y ocho "
        s(49) = "cuarenta y nueve "
        s(50) = "cincuenta "
        s(51) = "cincuenta y uno "
        s(52) = "cincuenta y dos "
        s(53) = "cincuenta y tres "
        s(54) = "cincuenta y cuatro "
        s(55) = "cincuenta y cinco "
        s(56) = "cincuenta y seis "
        s(57) = "cincuenta y siete "
        s(58) = "cincuenta y ocho "
        s(59) = "cincuenta y nueve "
        s(60) = "sesenta "
        s(61) = "sesenta y uno "
        s(62) = "sesenta y dos "
        s(63) = "sesenta y tres "
        s(64) = "sesenta y cuatro "
        s(65) = "sesenta y cinco "
        s(66) = "sesenta y seis "
        s(67) = "sesenta y siete "
        s(68) = "sesenta y ocho "
        s(69) = "sesenta y nueve "
        s(70) = "setenta "
        s(71) = "setenta y uno "
        s(72) = "setenta y dos "
        s(73) = "setenta y tres "
        s(74) = "setenta y cuatro "
        s(75) = "setenta y cinco "
        s(76) = "setenta y seis "
        s(77) = "setenta y siete "
        s(78) = "setenta y ocho "
        s(79) = "setenta y nueve "
        s(80) = "ochenta "
        s(81) = "ochenta y uno "
        s(82) = "ochenta y dos "
        s(83) = "ochenta y tres "
        s(84) = "ochenta y cuatro "
        s(85) = "ochenta y cinco "
        s(86) = "ochenta y seis "
        s(87) = "ochenta y siete "
        s(88) = "ochenta y ocho "
        s(89) = "ochenta y nueve "
        s(90) = "noventa "
        s(91) = "noventa y uno "
        s(92) = "noventa y dos "
        s(93) = "noventa y tres "
        s(94) = "noventa y cuatro "
        s(95) = "noventa y cinco "
        s(96) = "noventa y seis "
        s(97) = "noventa y siete "
        s(98) = "noventa y ocho "
        s(99) = "noventa y nueve "
        s(100) = "ciento "
        s(101) = "doscientos "
        s(102) = "trescientos "
        s(103) = "cuatrocientos "
        s(104) = "quinientos "
        s(105) = "seiscientos "
        s(106) = "setecientos "
        s(107) = "ochocientos "
        s(108) = "novecientos "
        s(109) = "mil "

        If Indice > 0 Then
            Cifra = Cifra + s(Indice)
        End If

    End Sub

#Region "mcm"


    ''' <summary>
    ''' Calcula el minimo comun multiplo (mcm) de un array de numeron enteros. El
    ''' algoritmo que utiliza es: Factorizar todos los numero y como resultado
    ''' toma todos los factores de mayor potencia y los multiplica.
    ''' 
    ''' EJEMPLO : mcm(12,24,10) 
    ''' Factores : 12 = 2^2 * 3
    '''            24 = 2^3 * 3
    '''            10 = 2   * 5
    ''' Entonces el mcm es => 2^3 * 3 * 5 = 120
    ''' 
    ''' </summary>
    ''' <param name="Numeros">Arreglo con los numero para calcular el mcm</param>
    ''' <returns>mcm resultante de procesar los numeros</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [lunes, 17 de marzo de 2008]       Creado GCP-Cambios ID: 6683
    ''' </history>
    Public Function mcm(ByVal Numeros() As Int64) As Int64
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'intNumeroTmp : Entero temporal que se utiliza para el calculo       
        'intDivisor   : Divisor de la factorizacion del numero procesado
        'objEnum      : Se utiliza para recorrer la coleccion de factores 
        'objItem      : Contiene los datos del factor con su potencia
        'intTemp      : Se utiliza para el calculo temporal
        'objFactores  : Contiene el factor y las veces que se repite (potencia)
        '               de un numero
        'objNumeros   : Coleccion de numeros filtrada
        'objNumerosFiltrados : Arreglo con los numero filtrados
        'objFactoresDefinitivos : Contiene los factores y las veces que se repiten
        '                         global a todos los numeros.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objFactores As New Generic.Dictionary(Of Int64, Int64)
        Dim objFactoresDefinitivos As New Generic.Dictionary(Of Int64, Int64)
        Dim intDivisor As Int64
        Dim intNumeroTmp As Int64
        Dim objEnum As IEnumerator
        Dim objItem As Generic.KeyValuePair(Of Int64, Int64)
        Dim intTemp As Int64
        Dim objNumeros As New Collections.Generic.Dictionary(Of Object, Nullable)

        'Filtra los numeros repetidos para evitar su procesamiento y cargar el proceso
        For intNum As Integer = 0 To Numeros.GetUpperBound(0)
            'Si el numero ya esta o es menor o igual que 1 no lo agregar para su procesamiento
            If Not objNumeros.ContainsKey(Numeros(intNum)) Then
                objNumeros.Add(Numeros(intNum), Nothing)
            End If
        Next

        Dim objNumerosFiltrados(objNumeros.Count - 1) As Object
        objNumeros.Keys.CopyTo(objNumerosFiltrados, 0)
        'Fin Filtra los numeros repetidos para evitar su procesamiento y cargar el proceso

        For intI As Integer = 0 To objNumerosFiltrados.GetUpperBound(0)

            intTemp = Convert.ToInt64(objNumerosFiltrados(intI))

            'Solo toma los naturales mayores que 1
            If intTemp <= 1 Then Continue For

            intDivisor = 1
            intNumeroTmp = intTemp
            objFactores.Clear()

            Do ' Para todos los numeros

                'Realiza la factorizacion
                Do
                    'Busca el proximo numero para dividir. El mismo sera un factor.
                    intDivisor = ObtenerProximoNumeroPrimo(intDivisor)
                Loop Until intNumeroTmp Mod intDivisor = 0

                If objFactores.ContainsKey(intDivisor) Then
                    'Si el factor ya existia sube en uno el numero de veces que lo divide
                    objFactores(intDivisor) += 1
                Else
                    'Agrega el numero que lo divide a los factores que lo dividen
                    objFactores.Add(intDivisor, 1)
                End If

                intNumeroTmp = Math.DivRem(intNumeroTmp, intDivisor, New Int64)
                intDivisor = 1

            Loop Until intNumeroTmp = 1

            'Obtiene el numerador para recorrer la coleccion
            objEnum = objFactores.GetEnumerator

            'Toma los factores del numero procesado y actualiza la lista
            'con los factores definitivos con potencias maximas
            While objEnum.MoveNext
                objItem = CType(objEnum.Current, Generic.KeyValuePair(Of Int64, Int64))
                If objFactoresDefinitivos.ContainsKey(objItem.Key) Then
                    If objFactoresDefinitivos(objItem.Key) < objItem.Value Then
                        objFactoresDefinitivos(objItem.Key) = objItem.Value
                    End If
                Else
                    objFactoresDefinitivos.Add(objItem.Key, objItem.Value)
                End If
            End While

        Next

        'Calcula el mcm que es la multiplicacion de los factores de los numero que
        'poseen la mayor potencia.
        objEnum = objFactoresDefinitivos.GetEnumerator
        intTemp = 1
        While objEnum.MoveNext
            objItem = CType(objEnum.Current, Generic.KeyValuePair(Of Int64, Int64))
            intTemp = Convert.ToInt64(intTemp * Math.Pow(Convert.ToInt32(objItem.Key), Convert.ToInt32(objItem.Value)))
        End While

        Return intTemp

    End Function

    ''' <summary>
    ''' Obtiene el proximo numero primo de un numero dado
    ''' </summary>
    ''' <param name="Numero">Numero a partir del cual se oltiene el proximo numero primo</param>
    ''' <returns>Proximo numero primo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [viernes, 14 de marzo de 2008]       Creado GCP-Cambios ID: 6683
    ''' </history>
    Public Shared Function ObtenerProximoNumeroPrimo(ByVal Numero As Int64) As Int64
        Do
            Numero += 1
        Loop Until EsPrimo(Numero)
        Return Numero
    End Function

    ''' <summary>
    ''' Indica si un numero es primo
    ''' </summary>
    ''' <param name="Numero">Numero del cual se desea saber si es primo</param>
    ''' <returns>True si es primo, False en caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [viernes, 14 de marzo de 2008]       Creado GCP-Cambios ID: 6683
    ''' </history>
    Public Shared Function EsPrimo(ByVal Numero As Int64) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'intNumTmp    : Se utiliza para el calculo temporal
        'intNumLimite : Se utiliza para el calculo temporal
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim intNumTmp As Long
        Dim intNumLimite As Long

        If Numero = 2 Then Return True
        If Numero Mod 2 = 0 Then Return False

        intNumTmp = 3
        intNumLimite = Numero

        Do While intNumLimite > intNumTmp
            If Numero Mod intNumTmp = 0 Then Return False
            intNumLimite = Numero \ intNumTmp
            intNumTmp = intNumTmp + 2
        Loop

        Return True

    End Function

#End Region

#Region "Serialización de Objetos"


    ''' <summary>
    ''' Serializa un objeto y lo retorna en Xml 
    ''' </summary>
    ''' <param name="ObjetoASerializar">Objeto que se desea serializar</param>
    ''' <returns> Xml del objeto serializado </returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [GustavoM]          [19/05/2005]       Creado
    ''' </history>
    Public Function SerializaAXml(ByVal ObjetoASerializar As Object) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objSerial      : Serialización Xml
        'objMemStream   : Objeto MemoryStream que se utiliza para serializar el objeto
        'objUtf8        : Objeto que se utiliza para recuperar el string
        'strObjeto      : Objeto serializado
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objSerial As New System.Xml.Serialization.XmlSerializer(ObjetoASerializar.GetType)
        Dim objMemStream As New System.IO.MemoryStream
        Dim objUtf8 As New System.Text.UTF8Encoding
        Dim strObjeto As String

        objSerial.Serialize(objMemStream, ObjetoASerializar)
        strObjeto = objUtf8.GetString(objMemStream.GetBuffer())
        objMemStream.Close()

        Return strObjeto

    End Function

    ''' <summary> 
    ''' Desserializa el objeto
    ''' </summary>
    '''<param name="TipoSerializacion">Tipo del objeto a desserializar</param>
    '''<param name="Xml">Representación del objeto en formato Xml</param>
    '''<returns>Objeto que se retorna </returns>
    ''' <history>
    ''' [GustavoM]          [19/05/2005]       Creado
    ''' </history>
    Public Function SerializaAXml(ByVal TipoSerializacion As Type, ByVal Xml As String) As Object
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objSerial      : Serialización Xml
        'objMemStream   : Objeto MemoryStream que se utiliza para desserializar el objeto
        'objUtf8        : Objeto que se utiliza para recuperar el string
        'strObjeto      : Objeto serializado
        'dtsRet         : Objeto que se retorna
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objUtf8 As New System.Text.UTF8Encoding
        Dim objMemStream As New System.IO.MemoryStream(objUtf8.GetBytes(Xml))
        Dim objRet As Object
        Dim objSerial As New System.Xml.Serialization.XmlSerializer(TipoSerializacion)

        objRet = objSerial.Deserialize(objMemStream)
        objMemStream.Close()

        Return objRet

    End Function

#End Region

#Region "Serialización DataSet"

    ''' <summary>
    ''' Convierte un dataset en una cadena serializada en base 64 y la comprime
    ''' </summary>
    ''' <param name="pdtsDataset">DataSet a serializar y comprimir</param>
    ''' <returns>DataSet serializado</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [jueves, 12 de abril de 2007]        Creado
    ''' </history>
    Public Shared Function ConvertirDatasetEnXMLAdapter(ByRef pdtsDataset As System.Data.DataSet) As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DECRIPCION DE LAS VARIABLES LOCALES
        ' objUtil			: objeto de utilidades COA para serializar
        ' objCompresion		: objeto de compresion de las utilidades COA
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objUtil As New COA.SerializarPorXML
        Dim objCompresion As New COA.Compresion.GZip

        Dim objRetorno() As Byte
        objRetorno = objUtil.ConvertirDatasetEnXML(pdtsDataset)


        Return System.Convert.ToBase64String(objCompresion.Compactar(objRetorno).GetBuffer)

    End Function

    ''' <summary>
    ''' A partir de un dataset serializado y comprimido, vuelve a retornar el dataset original
    ''' </summary>
    ''' <param name="SchemaXML">Esquema del dataset a retornar</param>
    ''' <param name="DatosXML">Cadena que representa el dataset serializado</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]   [jueves, 12 de abril de 2007]     Creado
    ''' </history>
    Public Shared Function ConvertirXMLEnDatasetAdapter(ByVal SchemaXML As String, ByVal DatosXML As String) As System.Data.DataSet
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DECRIPCION DE LAS VARIABLES LOCALES
        ' objUtil				: objeto de utilidades COA para serializar
        ' objDeCompresion		: objeto de decompresion de las utilidades COA
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objUtil As New COA.SerializarPorXML
        Dim objDecompresion As COA.Compresion.GZip

        objDecompresion = New COA.Compresion.GZip
        Dim objFlujo As New System.IO.MemoryStream(System.Convert.FromBase64String(DatosXML))

        Return objUtil.ConvertirXMLEnDataset(SchemaXML, System.Convert.ToBase64String(objDecompresion.Descompactar(objFlujo).GetBuffer))

    End Function
#End Region

    ''' <summary>
    ''' Almacena una valor en la sección AppSetting
    ''' </summary>
    ''' <param name="PathConfig">Path completo del archivo de configuración</param>
    ''' <param name="Key">Clave dentro de la sección en la cual se almacenará el valor</param>
    ''' <param name="Value">Valor a almacenar</param>
    ''' <remarks></remarks>
    ''' <history> 
    '''  [Gustavo]	[28/05/2006]	Created
    ''' </history>
    Public Sub GuardarValorAppSettings(ByVal PathConfig As String, ByVal Key As String, ByVal Value As String)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'strArchivo : Nombre del archivo de configuración
        'XmlDocument: Objeto que representa el archivo de configuración
        'Node       : Objeto node que se utiliza para recorrer las secciones
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strArchivo As String = PathConfig
        Dim XmlDocument As System.Xml.XmlDocument
        Dim Node As System.Xml.XmlNode

        Key = Key.ToLower
        'Carga el archivo de configuración
        XmlDocument = New System.Xml.XmlDocument()
        XmlDocument.Load(strArchivo)

        'Recorre los atributos de la sección appSettings
        For Each Node In XmlDocument.Item("configuration").Item("appSettings")
            If Node.Name = "add" Then
                Select Case Node.Attributes.GetNamedItem("key").Value.ToLower
                    Case Key
                        Node.Attributes.GetNamedItem("value").Value = Value
                End Select
            End If
        Next Node

        'Graba el archivo de configuración
        XmlDocument.Save(strArchivo)

    End Sub

#End Region

End Class