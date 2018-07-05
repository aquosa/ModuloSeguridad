Option Explicit On
Option Strict On

Imports EntidadesEmpresariales
Imports System.Configuration
Imports System.Data.EntityClient

''' -----------------------------------------------------------------------------
''' Project	 : AccesoDatos
''' Class	 : _PadreAccesoDatos
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase Padre de todos los accesos a datos de cada uno de los productos que genere
''' el proyecto. 
''' Contiene los atributos e intefases de conexión al origen de datos.
''' Se pueden incluir atributos e interfases que son utilizados por todos los 
''' productos.
''' Hereda de la clase _stdAplicacion de la capa EntidadesEmpresariales
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	30/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class PadreAccesoDatos
    Inherits Aplicacion

#Region "Variables Privadas"

    'Tablas que no se auditan
    Private mstrTablasNOAuditar As String = System.Configuration.ConfigurationSettings.AppSettings("TablasNOAuditar").Trim.ToUpper

    'Path físico desde el cual se leen los datos de conexión
    Private ArchivoConexion As String = System.Configuration.ConfigurationSettings.AppSettings("ArchivoConexion")

    Protected objConexionActiva As COA.Datos.ConexionAlOrigen

    'Instancia de la conexión con la base de datos
    Protected ReadOnly Property objConexion() As COA.Datos.Origenes.AccesoDB
        Get
            Return objConexionActiva.Motor
        End Get
    End Property

#End Region

#Region "Propiedades y Variables Públicas (fields)"
    'Retorna o setea el objeto Conexión que posee la instancia actual de la clase
    'Se utiliza para mantener una misma conexión entre instancias de clases
    Public Property ConexionActiva() As COA.Datos.ConexionAlOrigen
        Get
            Return objConexionActiva
        End Get
        Set(ByVal Value As COA.Datos.ConexionAlOrigen)
            objConexionActiva = Value
        End Set
    End Property
#End Region

#Region "Métodos de Conexión"
    ''' <summary>
    ''' Establece conexion a la base de datos e inicia transaccion
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub IniciarTransaccion()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                      DESCRIPCION DE VARIABLES LOCALES
        ' objConexion       : Conexion compartida por todas las clases que heredan de esta.
        ' sblnConexionCreada: indica si se esta en una transaccion simple o encadenada.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not objConexionActiva Is Nothing Then
            If objConexionActiva.Motor.EnTransaccion Then Exit Sub
            Desconectar()
        End If

        objConexionActiva = New COA.Datos.ConexionAlOrigen
        objConexionActiva.ConectarConArchivoXml = Me.ArchivoConexion
        If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            If Sesion.getInstance("objCipol") Is Nothing OrElse TryCast(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        Else
            If TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        End If
        HabilitarAuditoria()
        objConexionActiva.Motor.TransaccionIniciar()

    End Sub

    ''' <summary>
    ''' Establece conexion a la base de datos e inicia transaccion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [lunes, 04 de agosto de 2008]       Creado GCP-Cambios ID: 7353
    ''' </history>
    Public Sub IniciarTransaccion(ByVal IDConexion As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                      DESCRIPCION DE VARIABLES LOCALES
        ' objConexion       : Conexion compartida por todas las clases que heredan de esta.
        ' sblnConexionCreada: indica si se esta en una transaccion simple o encadenada.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not objConexionActiva Is Nothing Then
            If objConexionActiva.Motor.EnTransaccion Then Exit Sub
            Desconectar()
        End If

        objConexionActiva = New COA.Datos.ConexionAlOrigen
        objConexionActiva.ConectarConArchivoXml(IDConexion) = Me.ArchivoConexion
        If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            If Sesion.getInstance("objCipol") Is Nothing OrElse TryCast(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        Else
            If TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        End If
        HabilitarAuditoria()
        objConexionActiva.Motor.TransaccionIniciar()

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Finaliza una transacción
    ''' </summary>
    ''' <param name="HacerCommit">True si deben confirmarse los datos, False si se desea deshacer la transacción</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub FinalizarTransaccion(ByVal HacerCommit As Boolean)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If HacerCommit Then
            Me.objConexionActiva.Motor.TransaccionFinalizar(True)
        Else
            Me.objConexionActiva.Motor.TransaccionFinalizar(False)
        End If
        Me.Desconectar()

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establace conexión con el origen de datos
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub CrearConexion()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                      DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not objConexionActiva Is Nothing Then
            If objConexionActiva.Motor.EnTransaccion Then Exit Sub
            Me.Desconectar()
        End If

        objConexionActiva = New COA.Datos.ConexionAlOrigen
        objConexionActiva.ConectarConArchivoXml = Me.ArchivoConexion
        If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            If Sesion.getInstance("objCipol") Is Nothing OrElse TryCast(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        Else
            If TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        End If
        HabilitarAuditoria()

    End Sub

    ''' <summary>
    ''' Establace conexión con el origen de datos sobre el ID de conexion
    ''' </summary>
    ''' <param name="IDConexion">ID de la conexion que se utiliza 
    ''' para establecer la conexion</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [lunes, 04 de agosto de 2008]       Creado GCP-Cambios ID: 7353
    ''' </history>
    Public Sub CrearConexion(ByVal IDConexion As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                      DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not objConexionActiva Is Nothing Then
            If objConexionActiva.Motor.EnTransaccion Then Exit Sub
            Me.Desconectar()
        End If

        objConexionActiva = New COA.Datos.ConexionAlOrigen
        objConexionActiva.ConectarConArchivoXml(IDConexion) = Me.ArchivoConexion
        If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            If Sesion.getInstance("objCipol") Is Nothing OrElse TryCast(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        Else
            If TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objConexionActiva.TerminalUsuario = ""
            Else
                objConexionActiva.TerminalUsuario = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")
            End If
        End If
        HabilitarAuditoria()

    End Sub

    ''' <summary>
    ''' Crea la conexion del contexto Entity Framework 
    ''' </summary>
    ''' <returns>Conexion para un contexto de tipo Entity Framework</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [miércoles, 16 de abril de 2014]       Creado
    ''' </history>
    Public Function CrearConexion_EntityFramework(ByVal asm As System.Reflection.Assembly, Optional ByVal ConexionPermanente As Boolean = True) As EntityConnection
        Return objConexion.CrearConexion_EntityFramework_SQL(asm)
    End Function

#Region "Operaciones por ID de conexión"

    ''' <summary>
    ''' De aqui en adelante todas las conexiones se hacen de acuerdo al ID de conexion solicitado
    ''' </summary>
    ''' <param name="IDConexion">ID de la conexion con la cual se desea operar</param>
    ''' <history> 
    ''' LucianoP          [lunes, 21 de septiembre de 2015 10:18:29]       Creado
    ''' </history>
    Public Sub CambiarIDConexion(ByVal IDConexion As String)
        Dim strNombreDB As String
        strNombreDB = objConexionActiva.RecuperarNombreDBxIDConexion(IDConexion, Me.ArchivoConexion)
        objConexionActiva.Motor.CambiarConexion(strNombreDB)
    End Sub

    ''' <summary>
    ''' Restaura la conexion a la conexion original
    ''' </summary>
    ''' <history> 
    ''' LucianoP          [lunes, 21 de septiembre de 2015 10:18:37]       Creado
    ''' </history>
    Public Sub RestaurarConexion()
        Dim strNombreDB As String
        strNombreDB = objConexionActiva.RecuperarNombreDBDefault(Me.ArchivoConexion)
        objConexionActiva.Motor.CambiarConexion(strNombreDB)
    End Sub

    ''' <summary>
    ''' Recuperar el Servidor y la base de datos por ID de conexion
    ''' </summary>
    ''' <param name="IDConexion">ID de la conexion con la cual se desea operar</param>
    ''' <param name="Postfijo">String que se concatena al final de la cadena recuperada</param>
    ''' <returns>[SERVER].DB + Postfijo </returns>
    '''<history> 
    ''' LucianoP          [lunes, 21 de septiembre de 2015 10:20:26]       Creado
    ''' </history>
    Public Function RecuperarSrvYDBxIDConexion(ByVal IDConexion As String, ByVal Postfijo As String) As String
        Dim strBD As String = objConexionActiva.RecuperarNombreDBxIDConexion(IDConexion, Me.ArchivoConexion)
        Dim strServ As String = objConexionActiva.RecuperarServidorxIDConexion(IDConexion, Me.ArchivoConexion)

        Return "[" + strServ + "].[" + strBD + "]" + Postfijo
    End Function

#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Realiza la desconexion a la base de datos. 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Desconectar()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexionActiva.Desconectar()
        objConexionActiva = Nothing

    End Sub
#End Region

#Region "Métodos de Auditoría"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si el proceso de inicio de sesión del usuario ha finalizado
    ''' para auditar las sentencias SQL en la tabla SIST_EVENTOS
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    '''     Gustavom    22/09/2010 GCP-Cambios ID: 9385
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub HabilitarAuditoria()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'strUsuario  : Usuario activo
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strUsuario As String = String.Empty
        Dim objCipol As EntidadesEmpresariales.PadreCipolCliente = Nothing

        If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            If Not Sesion.getInstance("objCipol") Is Nothing AndAlso Not TryCast(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente) Is Nothing Then
                objCipol = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
                strUsuario = objCipol.Login
                Me.objConexionActiva.Motor.HabilitarAuditoria(mstrTablasNOAuditar, strUsuario)
            End If
        Else
            If Not TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.ICIPOL) Is Nothing Then
                objCipol = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
                strUsuario = objCipol.Login
                Me.objConexionActiva.Motor.HabilitarAuditoria(mstrTablasNOAuditar, strUsuario)
            End If
        End If
        If objCipol IsNot Nothing Then
            Me.objConexionActiva.Motor.Sistema = objCipol.SistemaActual
        End If

    End Sub
#End Region

#Region "Utilidades"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera la fecha y hora del servidor
    ''' </summary>
    ''' <returns>Fecha y hora actual</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function FechaServidor() As Date
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return Me.objConexionActiva.Motor.FechaServidor

    End Function
#End Region

End Class