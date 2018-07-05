Option Strict On
Option Explicit On

Imports EntidadesEmpresariales
Imports System.DirectoryServices
Imports AccesoDatos

''' -----------------------------------------------------------------------------
''' Project	 : ReglasNegocio
''' Class	 : CIPOL
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase de seguridad del sistema
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	30/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class CIPOL

    Public Structure EstContrasenia
        Dim Cambiar As Boolean
        Dim Mensaje As String
        Dim SeDebePreguntar As Boolean
    End Structure

    Private Const mstrSepParam As String = "[SC]"
    Private mblnSaltearValidacionTerminal As Boolean = False


#Region "Propiedades"
    Public ReadOnly Property MensajeError() As String
        Get
            Return Me.mstrMensajeError
        End Get
    End Property


    Public ReadOnly Property IDUsuario() As Integer
        Get
            Return Me.mshtIDUsuario
        End Get
    End Property

    Public ReadOnly Property Login() As String
        Get
            Return Me.mstrLogin
        End Get
    End Property

    Public ReadOnly Property NombreApellido() As String
        Get
            Return Me.mstrNombre
        End Get
    End Property

    Public ReadOnly Property NroSucursal() As Short
        Get
            Return Me.mshtIDArea
        End Get
    End Property

    Public ReadOnly Property NombreSucursal() As String
        Get
            Return Me.mstrNombreArea
        End Get
    End Property

    Public ReadOnly Property SistemaActual() As Short
        Get
            Return Me.mshtSistemaActual
        End Get
    End Property

    Public ReadOnly Property Contrasenia() As EstContrasenia
        Get
            Return mudtContraseña
        End Get
    End Property

    Public ReadOnly Property DatosDelUsuario() As System.Data.DataSet
        Get
            Return Me.mdtsDatosUsuario
        End Get
    End Property

    Public ReadOnly Property DatosTareasAutorizantes() As System.Data.DataSet
        Get
            Return Me.mdtsTareasAutoriz
        End Get
    End Property

    Public ReadOnly Property NombreDominio() As String
        Get
            Return mstrNombreDominio
        End Get
    End Property

    Public ReadOnly Property NombreOrganizacion() As String
        Get
            Return Me.mstrNombreOrganizacion
        End Get
    End Property

    Public ReadOnly Property TiempoInactividad() As Short
        Get
            Return Me.mshtParam(0)
        End Get
    End Property

    Public ReadOnly Property IV() As String
        Get
            Return Me.mstrIV
        End Get
    End Property

    Public ReadOnly Property Key() As String
        Get
            Return Me.mstrKey
        End Get
    End Property

    Public ReadOnly Property AliasUsuario() As String
        Get
            Return Me.mstrAliasUsuario
        End Get
    End Property

    Public ReadOnly Property Seguridad_SoloDominio() As Boolean
        Get
            Return Me.mblnSoloDominio
        End Get
    End Property

    Public Shared ReadOnly Property ContraseniaIntegrado() As String
        Get
            Return "CIPOL Integrado" 'Repetida en frmUsuario
        End Get
    End Property

    Public ReadOnly Property UsuarioDelDominio() As Boolean
        Get
            Return mblnUsuarioDominio 'GCP-Cambios ID: 9068
        End Get
    End Property

#End Region

#Region "Variables privadadas publicadas"
    Private mstrMensajeError As String
    Private mstrNombre As String
    Private mshtIDArea As Short
    Private mstrNombreArea As String
    Private mshtSistemaActual As Short = -1
    Private mudtContraseña As EstContrasenia
    Private mdtsTareasAutoriz As System.Data.DataSet
    Private mdtsDatosUsuario As System.Data.DataSet
    Private mstrLogin As String
    Private mstrNombreDominio As String = String.Empty
    Private mstrNombreOrganizacion As String = String.Empty
    Private mstrKey As String
    Private mstrIV As String
    Private mstrAliasUsuario As String
    Private mblnSoloDominio As Boolean = False
    Private mblnUsuarioDominio As Boolean = False

#End Region

#Region "Variables privadas no publicadas"
    Private mdtsMensajes As System.Data.DataSet
    Private mshtParam(11) As Short
    Private mshtIDUsuario As Integer
    Private mstrClaveHash As String
    Private mstrTerminal As String
    Private mstrClaveUsuario_Cdo_Integrado_Dominio As String
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Inicia la sesión del usuario y retorna los datos del mismo
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario</param>
    '''<param name="Clave">Clave hash del usuarioClave</param>
    '''<param name="Terminal">Nombre de la terminal donde el usuario inicia la sesión</param>
    ''' <param name="Terminal_ActualizacionLAN">Indica si la terminal se actualiza a través de un servidor de la LAN o remoto</param>
    '''<returns> True o False </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' 	[gustavom]	martes, 23 de octubre de 2007	GCP-Cambios ID: 5992    
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function IniciarSesion(ByVal IDUsuario As Integer, ByVal Clave As String, ByVal Terminal As String, ByRef Terminal_ActualizacionLAN As Boolean) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objADCipol  : Componente lógico de acceso a datos
        'dtsInit    : DataSet que se utiliza para verificar si se esta inicializando
        '             CIPOL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As New AccesoDatos.cladCipol
        Dim dtsInit As System.Data.DataSet

        Try
            mshtIDUsuario = IDUsuario
            Me.mstrClaveUsuario_Cdo_Integrado_Dominio = COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, ContraseniaIntegrado, IDUsuario.ToString)
            mstrLogin = Login
            mstrClaveHash = Clave
            mstrTerminal = Terminal
            'Inicio conexión con la base de datos
            objADCipol.CrearConexion()
            If objADCipol.RecuperarCantidadTerminales > 0 Then
                Return Me.ContinuarLogin(objADCipol, Terminal_ActualizacionLAN)
            Else
                'Si no existen terminales, verifico si se está 
                'inicializando el sistema
                dtsInit = objADCipol.RecuperarIDUsuarios
                Select Case dtsInit.Tables(0).Rows.Count
                    Case 0
                        MensajeUsuario(15)
                        objADCipol.Auditar(100, MensajeAuditoria(100), Me.mstrLogin)
                        Return False
                    Case 1
                        'Si es el usuario master
                        If System.Convert.ToInt16(dtsInit.Tables("Usuarios").Rows(0).Item("IDUsuario")) = 0 Then
                            mblnSaltearValidacionTerminal = True
                            Return Me.ContinuarLogin(objADCipol, Terminal_ActualizacionLAN)
                        Else
                            MensajeUsuario(15)
                            objADCipol.Auditar(100, MensajeAuditoria(100), Me.mstrLogin)
                            Return False
                        End If
                    Case Else
                        MensajeUsuario(15)
                        objADCipol.Auditar(100, MensajeAuditoria(100), Me.mstrLogin)
                        Return False
                End Select
            End If
        Finally
            objADCipol.Desconectar()
        End Try

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera el Dominio utilizado por CIPOL para autenticar el usuario
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    '''     [gustavom]	07/02/2008	GCP-Cambios ID: 6410
    '''     [gustavom]	21/09/2009	GCP-Cambios ID: 8448
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub RecuperarDominio()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objADCipol  : Componente logico de acceso a datos que se utiliza para 
        '             verificar si CIPOL utiliza integración con el dominio
        'objEnc     : Objeto de desencriptacion que se utiliza para verificar si
        '             CIPOL fue configurado para estar integrado al Dominio
        'strDatos   : Array que contiene los datos del dominio
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As AccesoDatos.cladCipol = Nothing
        Dim objEnc As New COA.CifrarDatos.TresDES, strDom As String
        Dim strDatos() As String

        Try
            objADCipol = New AccesoDatos.cladCipol
            objADCipol.CrearConexion()
            strDom = objADCipol.RecuperarDominio
            objEnc.IV = Me.mstrIV
            objEnc.Key = Me.mstrKey
            strDom = objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, strDom)
            If strDom = "X" Then
                mstrNombreDominio = "X"
            Else
                strDatos = strDom.Split("æ"c)
                Me.mstrNombreOrganizacion = strDatos(1)
                'Si no esta integrado al dominio
                mstrNombreDominio = strDatos(0)
                mblnSoloDominio = (strDatos.Length = 2)

            End If
        Finally
            objADCipol.Desconectar()
        End Try

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si la clave del usuario es correcta
    ''' </summary>
    '''<param name="ClaveDelUsuario">Clave del usuario</param>
    '''<returns> True o False </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    '''     [gustavom]  04/12/2008  GCP-Cambios ID: 7587
    '''     [IvanR]  20/05/2010     GCP-Cambios ID: 8965
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ClaveCorrecta(ByVal ClaveDelUsuario As String) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objADCipol : Componente lógico de acceso a datos que se utiliza para 
        '             recuperar la clave del usuario  
        'dtsDatos   : DataSet que se utiliza para recuperar la clave del usuario
        'blnRet     : Valor de retorno de la función
        'blnUsDom   : Señal que indica si el usuario se encuentra integrado al 
        '             dominio
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As AccesoDatos.cladCipol = Nothing
        Dim dtsDatos As System.Data.DataSet = Nothing
        Dim blnRet As Boolean = False
        Dim blnUsDom As Boolean = False

        If ClaveDelUsuario.Equals(String.Empty) Then
            Return False
        End If
        Try
            objADCipol = New AccesoDatos.cladCipol
            objADCipol.CrearConexion()
            Me.mstrClaveHash = ClaveDelUsuario
            dtsDatos = objADCipol.RecuperarDatosUsuario(Me.mstrLogin)
            Me.mshtIDUsuario = Convert.ToInt16(dtsDatos.Tables(Me.mstrLogin).Rows(0)("IDUsuario")) 'GCP-Cambios ID: 8965
            Me.mstrClaveUsuario_Cdo_Integrado_Dominio = COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, ContraseniaIntegrado, dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("IDUsuario").ToString)
            blnRet = ValidarClave(Me.mstrLogin, ClaveDelUsuario, dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("Sinonimo").ToString, blnUsDom)
            If Not blnRet Then
                MensajeUsuario(50)
                objADCipol.Auditar(180, MensajeAuditoria(180, Me.mstrLogin), Me.mstrLogin)
            End If
        Catch ex As Exception
            Throw
        Finally
            If objADCipol IsNot Nothing Then objADCipol.Desconectar()
        End Try

        Return blnRet

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Continúa con el proceso de login
    ''' </summary>
    '''<param name="cladCipol">Conexión a la base de datos</param>
    ''' <param name="Terminal_ActualizacionLAN">Indica si la terminal se actualiza a través de un servidor de la LAN o remoto</param>
    '''<returns> True o False </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    '''     [gustavom]  04/12/2006  GCP-Cambios ID: 4586
    ''' 	[gustavom]	14/06/2007	GCP-Cambios ID: 5208
    ''' 	[gustavom]	martes, 23 de octubre de 2007	GCP-Cambios ID: 5992
    ''' 	[gustavom]	martes, 12 de febrero de 2008	GCP-Cambios ID: 6410
    ''' 	[AndresR]   lunes, 05 de mayo de 2008		GCP-Cambios ID: 4255
    ''' 	[gustavom]	viernes, 21 de mayo de 2010	    GCP-Cambios ID: 9068
    '''     [MartinV]   [miércoles, 06 de noviembre de 2013] GCP-Cambios 14460
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function ContinuarLogin(ByVal cladCipol As AccesoDatos.cladCipol, ByRef Terminal_ActualizacionLAN As Boolean) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsDatos   : Objeto DataSet que contiene los datos de un usuario para 
        '             el proceso de Login
        'objEnc     : Objeto que se utiliza para desencriptar los datos
        'dtmFecha   : Fecha actual del sistema
        'shtFallidos: Cantidad de intentos fallidos
        'dtsTerm    : DataSet que se utiliza para verificar si la terminal se
        '             encuentra habilitada
        'shtIDTerm  : Identificador de la terminal donde el usuario inicia sesión
        'intDifDias : Cantidad de días de diferencia que existe entre el vencimiento
        '             de la clave y la fecha actual
        'blnUsDom   : Señal que indica que el usuario se encuentra integrado al
        '             dominio
        'strNombreNetBios : Se utiliza para saber si el usuario tiene otra sesion iniciada
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsDatos As System.Data.DataSet
        Dim objEnc As New COA.CifrarDatos.TresDES
        Dim dtmFecha As Date, shtFallidos As Short
        Dim dtsTerm As System.Data.DataSet, shtIDTerm As Short
        Dim intDifDias As Integer
        Dim blnUsDom As Boolean
        Dim strNombreNetBios As String = ""

        objEnc.Key = Me.mstrKey
        objEnc.IV = Me.mstrIV


        If Not mblnSaltearValidacionTerminal Then
            dtsTerm = cladCipol.RecuperarDatosTerminal(mstrTerminal)
            'Si la terminal está permitida
            If dtsTerm.Tables("Terminal").Rows.Count = 0 Then
                MensajeUsuario(15)
                cladCipol.Auditar(100, MensajeAuditoria(100), Me.mstrLogin)
                Return False
            Else
                'Si la terminal existe, verifica si esta habilitada
                If objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(dtsTerm.Tables("Terminal").Rows(0).Item("UsoHabilitado"), String)).Equals("0") Then
                    MensajeUsuario(10)
                    cladCipol.Auditar(100, MensajeAuditoria(100), Me.mstrLogin)
                    Return False
                Else
                    shtIDTerm = CType(dtsTerm.Tables("Terminal").Rows(0).Item("IDTerminal"), Short)
                    Terminal_ActualizacionLAN = (dtsTerm.Tables("Terminal").Rows(0).Item("ORIGENACTUALIZACION").ToString.Trim.ToUpper = "L")
                End If
            End If
            dtsTerm = Nothing
        End If

        dtmFecha = cladCipol.FechaServidor
        dtsDatos = cladCipol.RecuperarDatosUsuario(Me.mstrLogin)
        'si no hay datos del usuario, retornamos falso
        If dtsDatos.Tables(mstrLogin).Rows.Count.Equals(0) Then
            mstrMensajeError = "Usuario no encontrado o dado de baja."
            Return False
        End If
        'Verifico si los hash son iguales
        If ValidarClave(mstrLogin, Me.mstrClaveHash, dtsDatos.Tables(mstrLogin).Rows(0)("sinonimo").ToString, blnUsDom) Then
            mblnUsuarioDominio = blnUsDom
            Me.mstrNombre = CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("Nombres"), String).TrimEnd
            Me.mshtIDArea = CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("IDArea"), Short)
            Me.mstrNombreArea = CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("NombreArea"), String).TrimEnd
            Me.mstrAliasUsuario = dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("ALIAS_USUARIO").ToString.Trim
            'Si existe un sistema por defecto sobre el cual inicia el usuario
            If Not dtsDatos.Tables(Me.mstrLogin).Rows(0).IsNull("UltimoSistema") Then
                Me.mshtSistemaActual = CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("UltimoSistema"), Short)
            End If
            'Si el usuario es el master
            If Me.mstrLogin.Equals("master") Then
                ConfigurarContextoSeguridad(cladCipol) '     Gustavom    22/09/2010 GCP-Cambios ID: 9385
                ' JorgeI-[miércoles, 23 de enero de 2013] GCP-Cambios ID: 13249
                '--[ Se agrega registro de auditoria de loguin exitoso ]-- 
                AuditarProcesoDeLoginExitoso(mstrLogin.Trim(), cladCipol)
                Return True
            Else
                'Si el usuario no es el master
                'Si la cuenta esta bloqueada
                If objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("CtaBloqueada"), String)).Equals("1") Then
                    'Gcp-Cambios 14460 -> Cantidad de días admitidos sin inicio de sesión por parte de un usuario:(Transcurrido el lapso se bloquea la cuenta.)
                    'Se deja la fecha de bloqueo en null.
                    If (IsDBNull(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaBloqueo"))) Then
                        Me.MensajeUsuario(17)
                        cladCipol.Auditar(130, Me.MensajeAuditoria(130, Me.mstrLogin), Me.mstrLogin)
                        Return False
                    End If
                    'Verifica a que se debe
                    Select Case Format(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaBloqueo"), "mm:ss")
                        Case "00:01"
                            Me.MensajeUsuario(17)
                            cladCipol.Auditar(130, Me.MensajeAuditoria(130, Me.mstrLogin), Me.mstrLogin)
                            Return False
                        Case "00:02"
                            'Si expiró, elimina el bloqueo
                            If CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaBloqueo"), Date).Date < dtmFecha.Date Then
                                cladCipol.DesbloquearCta(Me.mshtIDUsuario, objEnc.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "0"))
                            Else
                                'Falta auditar con otro código , por ahora uso el mismo que 00:00:01
                                Me.MensajeUsuario(17)
                                cladCipol.Auditar(130, Me.MensajeAuditoria(130, Me.mstrLogin), Me.mstrLogin)
                                Return False
                            End If
                        Case Else
                            'Si expiró, elimina el bloqueo
                            'Gcp - Cambios 14460 - martinv - cuando el parámetro "Cantidad de tiempo, en minutos, a partir del cual se desbloquea la cuenta de usuario"
                            'es igual a cero se debe dejar bloqueado el usuario indefinidamente.
                            If (Not CType(Me.mshtParam(2), Double) = 0) AndAlso CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaBloqueo"), Date).AddMinutes(CType(Me.mshtParam(2), Double)) <= dtmFecha Then
                                cladCipol.DesbloquearCta(Me.mshtIDUsuario, objEnc.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "0"))
                            Else
                                Me.MensajeUsuario(17)
                                cladCipol.Auditar(120, Me.MensajeAuditoria(130, Me.mstrLogin), Me.mstrLogin)
                                Return False
                            End If
                    End Select
                End If

                'gcp cambios 14460 - 
                'Cantidad de días admitidos sin inicio de sesión por parte de un usuario:(Transcurrido el lapso se bloquea la cuenta.)
                If Not CType(Me.mshtParam(8), Double) = 0 Then
                    'Si la fecha de desbloqueo es null se realiza la validación
                    If IsDBNull(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FECHADESBLOQUEO")) Then
                        If ((Not dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FECHAULTUSOCTA") Is Nothing) And (Not IsDBNull(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FECHAULTUSOCTA")))) Then
                            If dtmFecha.AddDays(-CType(Me.mshtParam(8), Double)).Date > CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FECHAULTUSOCTA"), Date).Date Then
                                'Se bloquea la cuenta por inactividad.
                                cladCipol.Auditar(220, Me.MensajeAuditoria(220, Me.mstrLogin), Me.mstrLogin)
                                cladCipol.BloquearCta(Me.mshtIDUsuario, objEnc.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "1"), Nothing)
                                Return False
                            End If
                        End If
                    End If
                End If

                If Not blnUsDom Then    'Si el usuario no pertenece al dominio
                    If Not dtsDatos.Tables(Me.mstrLogin).Rows(0).IsNull("CantIntInvUsoCta") Then
                        If objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("CantIntInvUsoCta"), String)).Equals("-1") Then
                            Me.MensajeUsuario(17)
                            cladCipol.Auditar(130, Me.MensajeAuditoria(130, Me.mstrLogin), Me.mstrLogin)
                            Return False
                        End If
                    End If

                End If
                'Actualiza la fecha y hora de último uso de la cuenta
                'e inicializa la cantidad de intentos fallidos
                cladCipol.ActualizarCuenta(Me.mshtIDUsuario, objEnc.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "0"))

                If cladCipol.ExisteSesionActiva(Me.mstrLogin, Me.mstrTerminal) Then
                    cladCipol.Auditar(3, MensajeAuditoria(3, Me.mstrLogin), Me.mstrLogin)
                End If

                'Si el usuario puede iniciar sesión en la terminal
                If cladCipol.PuedeIniciarSesionEnTerminal(Me.mshtIDUsuario, shtIDTerm) Then
                    'Si no esta permitido el horario para el usuario
                    If Not cladCipol.HorarioPermitido(Me.mshtIDUsuario, dtmFecha) Then
                        MensajeUsuario(45)
                        cladCipol.Auditar(240, MensajeAuditoria(240, Me.mstrLogin), Me.mstrLogin)
                        Return False
                    End If
                Else
                    'Si el usuario no puede iniciar sesión en la terminal actual
                    MensajeUsuario(40)
                    cladCipol.Auditar(230, MensajeAuditoria(230, Me.mstrLogin), Me.mstrLogin)
                    Return False
                End If

                'Si el usuario ya posee un inicio de sesión en otra PC
                strNombreNetBios = ""
                If System.Configuration.ConfigurationManager.AppSettings("GrabarTerminalInicioSesion").Trim().ToUpper() = "S" Then
                    strNombreNetBios = cladCipol.PuedeIniciarSesion(Me.mstrLogin, Me.mstrTerminal)
                End If
                If strNombreNetBios.Equals("") Then
                    ConfigurarContextoSeguridad(cladCipol)
                Else
                    Me.MensajeUsuario(18, strNombreNetBios)
                    Return False
                End If

                If Not blnUsDom Then    'Si el usuario no pertenece al dominio
                    'Si la clave no tiene vencimiento
                    If System.Convert.IsDBNull(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaVencimiento")) OrElse CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaVencimiento"), Date).Date = #1/1/1900# Then
                        'Verifica si se debe forzar el cambio de contraseña
                        If objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("ForzarCambio"), String)) = "1" Then
                            MensajeUsuario(60)
                            Me.mudtContraseña.Cambiar = True
                            Me.mudtContraseña.Mensaje = Me.mstrMensajeError
                            Me.mstrMensajeError = String.Empty
                        End If
                    Else
                        'Si la clave tiene vencimiento


                        intDifDias = CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("FechaVencimiento"), Date).Date.Subtract(dtmFecha.Date).Days
                        'Si la clave está vencida 
                        If (intDifDias <= 0) Then
                            Me.mudtContraseña.Cambiar = True
                            MensajeUsuario(35)
                            Me.mudtContraseña.Mensaje = Me.mstrMensajeError
                            Me.mstrMensajeError = String.Empty
                        End If

                        'Si la diferencia en días llega a la cantidad de
                        'dias de antelación con que debe preguntarse el cambio de clave
                        If intDifDias <= Me.mshtParam(6) And intDifDias > 0 Then
                            If Not (CType(Me.mshtParam(6), Byte) = 0) Then
                                Me.mudtContraseña.Cambiar = True
                                Select Case intDifDias
                                    Case 1
                                        MensajeUsuario(25)
                                        Me.mudtContraseña.SeDebePreguntar = True
                                    Case Is > 1
                                        MensajeUsuario(30, intDifDias.ToString)
                                        Me.mudtContraseña.SeDebePreguntar = True
                                End Select
                                Me.mudtContraseña.Mensaje = Me.mstrMensajeError
                                Me.mstrMensajeError = String.Empty
                            End If
                        Else
                            'Verifica si se debe forzar el cambio de contraseña
                            If objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("ForzarCambio"), String)) = "1" Then
                                MensajeUsuario(60)
                                Me.mudtContraseña.Cambiar = True
                                Me.mudtContraseña.Mensaje = Me.mstrMensajeError
                                Me.mstrMensajeError = String.Empty
                            End If
                        End If
                    End If

                End If

                ' JorgeI-[miércoles, 23 de enero de 2013] GCP-Cambios ID: 13249
                '--[ Se agrega registro de auditoria de loguin exitoso ]-- 
                AuditarProcesoDeLoginExitoso(mstrLogin.Trim(), cladCipol)
                Return True 'Inicio se sesión satisfactorio

            End If
        Else
            'Si la política de intentos fallidos está activa y el usuario no pertenece al dominio
            If mshtParam(1) > 0 AndAlso Not blnUsDom Then
                If dtsDatos.Tables(Me.mstrLogin).Rows(0).IsNull("CantIntInvUsoCta") Then
                    shtFallidos = 1
                Else
                    shtFallidos = CType(objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(dtsDatos.Tables(Me.mstrLogin).Rows(0).Item("CantIntInvUsoCta"), String)), Short)
                    shtFallidos += 1S
                End If
                'Actualiza los intentos fallidos
                cladCipol.IntentosFallidos(Me.mshtIDUsuario, objEnc.Criptografia(COA.CifrarDatos.Accion.Encriptacion, shtFallidos.ToString))

                'Si el usuario ha alcanzado la cantidad de intentos fallidos definidos
                'por política de seguridad
                If Me.mshtParam(1) = shtFallidos Then
                    MensajeUsuario(17)
                    cladCipol.Auditar(150, MensajeAuditoria(150, Me.mstrLogin), Me.mstrLogin)
                    cladCipol.BloquearCta(Me.mshtIDUsuario, objEnc.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "1"), cladCipol.FechaServidor()) 'GCP-Cambio ID: 8965
                    Return False
                Else
                    MensajeUsuario(20)
                    cladCipol.Auditar(140, MensajeAuditoria(140, Me.mstrLogin), Me.mstrLogin)
                    Return False
                End If
            Else
                MensajeUsuario(20)
                cladCipol.Auditar(140, MensajeAuditoria(140, Me.mstrLogin), Me.mstrLogin)
                Return False
            End If
        End If

    End Function

    ''' <summary> Graba el registro de inicio exitoso
    ''' </summary>
    ''' <param name="Usuario">Nombre del usuario conectado</param>
    ''' <param name="cladCipol">Objeto de Acceso a Datos</param>
    ''' <remarks></remarks><history>
    ''' [JorgeI]  [miércoles, 23 de enero de 2013] Creado GCP-Cambios ID:13249
    ''' [LucianoP]          [miércoles, 5 de abril de 2017]    Adaptaciones a login
    ''' </history>
    Private Sub AuditarProcesoDeLoginExitoso(ByVal Usuario As String, ByVal cladCipol As AccesoDatos.cladCipol)
        Dim strMsj As String = cladCipol.RecuperarMensajeAuditoria(0, -1, "")
        cladCipol.Auditar(0, Me.MensajeAuditoria(0, Me.mstrLogin) + ". " + strMsj, Me.mstrLogin)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si la clave del usuario es correcta
    ''' </summary>
    ''' <param name="Usuario"> Usuario actual</param>
    ''' <param name="ClaveDelUsuario">Clave del usuario ingresada</param>
    ''' <param name="ClaveEnBD">Clave del usuario almacenada en base de datos</param>
    ''' <param name="UsuarioIntegradoAlDominio">Retorna si el usuario se encuentra integrado al dominio</param>
    ''' <returns>True o False</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	20/09/2005	Created
    '''     [gustavom]	07/02/2008	GCP-Cambios ID: 6410
    ''' [Gustavom]          [viernes, 04 de abril de 2008]      GCP-Cambios ID: 6666
    ''' </history>
    ''' -----------------------------------------------------------------------------   
    Private Function ValidarClave(ByVal Usuario As String, ByVal ClaveDelUsuario As String, ByVal ClaveEnBD As String, ByRef UsuarioIntegradoAlDominio As Boolean) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'token  : Token del usuario retornado por la API
        'blnDon : Señal que indica si se debe validar contra el dominio
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim token As IntPtr
        Dim blnDom As Boolean = Not ClaveEnBD.Equals(Me.mstrClaveUsuario_Cdo_Integrado_Dominio)

        If Usuario.Trim.Equals("master") Then
            UsuarioIntegradoAlDominio = False
            mstrClaveHash = COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, Me.mstrClaveHash, Me.mshtIDUsuario.ToString)
            Return Me.mstrClaveHash.Equals(ClaveEnBD)
        Else
            If Me.Seguridad_SoloDominio OrElse ClaveEnBD.Equals(Me.mstrClaveUsuario_Cdo_Integrado_Dominio) Then
                UsuarioIntegradoAlDominio = True
                Return LogonUser(Usuario, mstrNombreDominio, ClaveDelUsuario, LOGON32_LOGON_NETWORK, _
                    LOGON32_PROVIDER_DEFAULT, token)
            Else
                UsuarioIntegradoAlDominio = False
                mstrClaveHash = COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, Me.mstrClaveHash, Me.mshtIDUsuario.ToString)
                Return Me.mstrClaveHash.Equals(ClaveEnBD)
            End If
        End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Configura, los permisos que el usuario posee, y los sistemas que tiene 
    ''' permitido acceder para que puedan ser recuperados por la Fachada
    ''' </summary>
    '''<param name="cladCipol">Conexion a la base de datos</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub ConfigurarContextoSeguridad(ByVal cladCipol As AccesoDatos.cladCipol)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'rowFila     : Objeto DataRow que se utiliza para recorrer la colección
        '             de filas
        'objEnc     : Objeto de cifrado de datos
        'strUsrExist: String que se utiliza para identificar si el usuario autorizante
        '             existe duplicado porque la tarea se encuentra en mas de un rol
        'strValor   : Valor que se concatena en strUsrExist
        'rowTareas  : Array que contiene los objetos DataRow de las tareas asignadas
        '             a nivel de menú
        'rowTareaPer: Objeto Data Row que se utiliza para verificar si el usuario
        '             posee una tarea permitida
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowFila As System.Data.DataRow, objEnc As New COA.CifrarDatos.TresDES
        Dim strUsrExist As String = String.Empty, strValor As String
        Dim rowTareas() As System.Data.DataRow, rowTareaPer() As System.Data.DataRow

        objEnc.IV = Me.mstrIV
        objEnc.Key = Me.mstrKey

        'Recupero las tareas primitivas y los usuarios autorizantes
        Me.mdtsTareasAutoriz = cladCipol.RecuperarTareasPrimitivas(Me.mshtIDUsuario)
        For Each rowFila In Me.mdtsTareasAutoriz.Tables("Autorizantes").Rows
            'Verifico si el usuario autorizante no posee la tarea inhibida
            If objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(rowFila.Item("TareaInhibida"), String)).Equals("S") Then
                rowFila.Delete()
            Else
                'Si el usuario ya existe. Puede suceder que la tarea autorizante
                'este en más de un rol
                strValor = " "c & CType(rowFila.Item("Usuario"), String) & "*"c & CType(rowFila.Item("IdTareaPrimitiva"), String)
                If strUsrExist.IndexOf(strValor) >= 0 Then
                    rowFila.Delete()
                Else
                    strUsrExist &= strValor
                End If
            End If
        Next

        'Desencripto los datos de la tarea
        For Each rowFila In Me.mdtsTareasAutoriz.Tables("Tareas").Rows
            rowFila.BeginEdit()
            rowFila.Item("TareaInhibida") = objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(rowFila.Item("TareaInhibida"), String))
            rowFila.Item("RequiereAuditoria") = objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, CType(rowFila.Item("RequiereAuditoria"), String))
            rowFila.EndEdit()
        Next

        Me.mdtsTareasAutoriz.AcceptChanges()

        'Recupero los sistemas de los cuales el usuario posee tareas
        'y los menúes permitidos 
        Me.mdtsDatosUsuario = cladCipol.RecuperarSistemasMenu(Me.mshtIDUsuario)

        'Si el usuario no posee un sistema de ingreso por defecto, establezco el primero
        If Me.mshtSistemaActual = -1 Then
            If Me.mdtsDatosUsuario.Tables("SE_SIST_HABILITADOS").Rows.Count > 0 Then
                Me.mshtSistemaActual = CType(Me.mdtsDatosUsuario.Tables("SE_SIST_HABILITADOS").Rows(0).Item("IDSistema"), Short)
            End If
        End If

        'Obtengo las filas que poseen IDTarea
        rowTareas = Me.mdtsDatosUsuario.Tables("SE_MENUES").Select("IDTarea > 0")
        For Each rowFila In rowTareas
            'Verifico si la tarea no está permitida
            rowTareaPer = Me.mdtsTareasAutoriz.Tables("Tareas").Select("IDTarea = " & CType(rowFila.Item("IDTarea"), String))
            'Si la tarea que posee el menú no la tiene el usuario, la elimino
            If rowTareaPer.GetUpperBound(0) = -1 Then
                rowFila.Delete()
            Else
                'Si la tarea no está habilitada
                If CType(rowTareaPer(0).Item("TareaInhibida"), String).TrimEnd.Equals("S") Then
                    rowFila.Delete()
                End If
            End If
        Next
        Me.mdtsDatosUsuario.AcceptChanges()

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna el mensaje a auditar 
    ''' </summary>
    '''<param name="CodMensaje">Codigo del mensaje</param>
    '''<param name="Usuario">Usuario del cual se realiza la auditoria</param>
    '''<param name="UsuarioAdm">Usuario que actua como administrador del cambio que se audita</param>
    '''<param name="NuevoValor">Cambio realizado</param>
    '''<returns> Mensaje a auditar </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function MensajeAuditoria(ByVal CodMensaje As Short, Optional ByVal Usuario As String = "", Optional ByVal UsuarioAdm As String = "", Optional ByVal NuevoValor As String = "") As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                  DESCRIPCION DE VARIABLES LOCALES
        'strMensaje : Mensaje que se obtiene cuando se reemplazan las valores
        '             dinámicos (@)
        'dtrMsj     : Objeto DataRow que representa el mensaje a auditar
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strMensaje As String
        Dim dtrMsj() As System.Data.DataRow

        dtrMsj = Me.mdtsMensajes.Tables("Auditoria").Select("CodAuditoria = " & CodMensaje.ToString)
        If dtrMsj.GetUpperBound(0).Equals(-1) Then
            strMensaje = "No existe el mensaje de auditoría para el código: " & CodMensaje.ToString
        Else
            strMensaje = CType(dtrMsj(0).Item("TextoAuditoria"), String)
            Select Case CodMensaje
                Case 0, 1, 3 'Login/LogOut Existoso. Desconexión involuntaria
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                Case 4 'Intento de inicio de sesion por 2da vez
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                Case 100 'Proceso de Login
                    strMensaje = strMensaje.Replace("@", mstrTerminal)
                Case 110, 230, 250 'Proceso de Login
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                Case 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 240 'Proceso de Login
                    strMensaje = strMensaje.Replace("@", Usuario)
                Case 260, 270 'Supervision
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                Case 400, 410, 420, 430, 440, 450, 460, 470, 480, 490, 500, 510 'Políticas de Seguridad
                    strMensaje = Replace(strMensaje, "@", UsuarioAdm, , 1, vbTextCompare)
                    strMensaje = Replace(strMensaje, "@", NuevoValor, , 1, vbTextCompare)
                Case 600, 610, 620, 750, 760, 770 'Administracion de Usuarios
                    strMensaje = Replace(strMensaje, "@", UsuarioAdm, , 1, vbTextCompare)
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, vbTextCompare)
                Case 700, 710, 720, 730, 740 'ABM de Seguridad
                    strMensaje = strMensaje.Replace("@", UsuarioAdm)
            End Select
        End If

        Return strMensaje

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Si el login del usuario existe, retorna el identificador del mismo
    ''' </summary>
    '''<param name="Login">Nombre de inicio de sesión</param>
    '''<returns> Identificador del usuario </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ExisteUsuario(ByVal Login As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objADCipol  : Componente lógico de acceso a datos que se utiliza para 
        '              verificar si el usuario existe
        'shtRet      : Valor de retormo
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As New AccesoDatos.cladCipol
        Dim shtRet As Integer

        Try
            objADCipol.CrearConexion()
            'Si existe el dominio en el usuario
            shtRet = objADCipol.ExisteUsuario(Login.ToLower)

            If shtRet = -1 Then
                Me.MensajeUsuario(20)
                objADCipol.Auditar(110, Me.MensajeAuditoria(110, Login), Me.mstrLogin)
            Else
                Me.mstrLogin = Login
            End If
            Return shtRet
        Finally
            objADCipol.Desconectar()
        End Try

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Muestra el mensaje de error al usuario
    ''' </summary>
    ''' <param name="CodMensaje">Código del mensaje que se debe buscar y mostrar al usuarioCodMensaje</param>
    ''' <param name="Reemplazar">Texto por el cual se reemplaza una variable en el mensaje de error</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [gustavom]	       30/08/2005	Created
    ''' [AndresR]          [lunes, 05 de mayo de 2008]       GCP-Cambios ID: 4255
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub MensajeUsuario(ByVal CodMensaje As Short, Optional ByVal Reemplazar As String = "")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'rowMsj : Objeto DataRow que representa el mensaje a retornar
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowMsj() As System.Data.DataRow

        rowMsj = Me.mdtsMensajes.Tables("Usuario").Select("CodMensaje = " & CodMensaje.ToString)
        If rowMsj.GetUpperBound(0).Equals(-1) Then
            Me.mstrMensajeError = "Imposible iniciar una sesión para el usuario especificado"
        Else
            'Si el texto contiene el caracter "@" es reemplazado por el parametro "Reemplazar"
            Me.mstrMensajeError = CType(rowMsj(0).Item("TextoMensaje"), String).TrimEnd.Replace("@", Reemplazar)
        End If

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los valores de auditoría y mensaje
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objADCipol  : Componente lógico de acceso a datos que se utiliza para
        '              recuperar los mensajes a mostrar al usuario y los mensaje
        '              de auditoria
        'dtsKey      : Objeto DataSet que se utiliza para recuperar datos del
        '              usuario
        'strRet      : Retorno de la función de obtención de parámetros
        'strParam    : Array que contiene los distintos parámetros de seguridad
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As AccesoDatos.cladCipol = Nothing
        Dim dtsKey As System.Data.DataSet
        Dim strRet As String, strParam() As String

        Me.mudtContraseña.Cambiar = False
        Me.mudtContraseña.Mensaje = String.Empty
        Me.mudtContraseña.SeDebePreguntar = False

        Try
            objADCipol = New AccesoDatos.cladCipol
            objADCipol.CrearConexion()
            mdtsMensajes = objADCipol.RecuperarMensajes
            dtsKey = objADCipol.RecuperarDatos
            Me.mstrKey = CType(dtsKey.Tables(0).Rows(0).Item(0), String)
            Me.mstrIV = CType(dtsKey.Tables(0).Rows(0).Item(1), String)
            'Recupero los parámetros
            strRet = objADCipol.RecuperarParametros
            If Not strRet.Equals(String.Empty) Then
                Dim objEnc As New COA.CifrarDatos.TresDES
                objEnc.IV = Me.mstrIV
                objEnc.Key = Me.mstrKey
                strRet = objEnc.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, strRet)
                objEnc = Nothing

                strParam = Split(strRet, mstrSepParam)
                mshtParam(0) = CType(strParam(0), Short)
                mshtParam(1) = CType(strParam(1), Short)
                mshtParam(2) = CType(strParam(2), Short)
                mshtParam(3) = CType(strParam(3), Short)
                mshtParam(4) = CType(strParam(4), Short)
                mshtParam(5) = CType(strParam(5), Short)
                mshtParam(6) = CType(strParam(6), Short)
                mshtParam(7) = CType(strParam(7), Short)
                mshtParam(8) = CType(strParam(8), Short)
                mshtParam(9) = CType(strParam(9), Short)
                mshtParam(10) = CType(strParam(10), Short)
                mshtParam(11) = CType(strParam(11), Short)
            End If
            RecuperarDominio()
        Catch ex As Exception
            Throw
        Finally
            '#If Not Debug Then
            objADCipol.Desconectar()
            '#End If
        End Try

    End Sub

    ''' <summary>
    ''' Audita el evento que se produce cuando un usuario intenta iniciar sesion por segunda vez
    ''' </summary>
    ''' <param name="Login">Id del login del usuario asociado al evento</param>
    ''' <returns>Mensaje recuperado para mostrar al usuario</returns>
    ''' <history>
    '''[LucianoP]          [jueves, 13 de julio de 2017]    Creado 
    '''</history>
    Public Function AuditarIntentoInicioSesionConSesionActiva(Login As String) As String
        Dim objADCipol As cladCipol = Nothing
        Dim strMensajeUsuario As String
        Const intCodMsg As Integer = 4

        Try
            objADCipol = New cladCipol()
            objADCipol.CrearConexion()

            'Recupera el mensaje a mostrar al usuario
            strMensajeUsuario = objADCipol.RecuperarMensajeAuditoria(intCodMsg, 0, "")

            'Audita el evento de intento de inicio de sesion por 2da vez
            objADCipol.Auditar(intCodMsg, MensajeAuditoria(intCodMsg, Login), Login)

            Return strMensajeUsuario

        Catch ex As Exception
            Throw
        Finally
            If objADCipol IsNot Nothing Then objADCipol.Desconectar()
        End Try
    End Function



#Region "Llamadas a la API LogOnUser"
    ' Declare the API 
    Private Declare Auto Function LogonUser Lib "advapi32.dll" ( _
      ByVal lpszUsername As String, _
      ByVal lpszDomain As String, _
      ByVal lpszPassword As String, _
      ByVal dwLogonType As Integer, _
      ByVal dwLogonProvider As Integer, _
      ByRef phToken As IntPtr) As Boolean

    ' Declare the logon types as constants 
    Const LOGON32_LOGON_INTERACTIVE As Long = 2
    Const LOGON32_LOGON_NETWORK As Long = 3

    ' Declare the logon providers as constants 
    Const LOGON32_PROVIDER_DEFAULT As Long = 0
    Const LOGON32_PROVIDER_WINNT50 As Long = 3
    Const LOGON32_PROVIDER_WINNT40 As Long = 2
    Const LOGON32_PROVIDER_WINNT35 As Long = 1


#End Region

End Class
