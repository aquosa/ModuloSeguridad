Option Strict On
Option Explicit On 
Imports EntidadesEmpresariales
Imports System.Diagnostics
Imports System.Configuration

''' -----------------------------------------------------------------------------
''' Project	 : Fachada
''' Class	 : _PadreFachada
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase base de la capa Fachada la cual es común a todos los productos del
''' proyecto.
''' Centraliza además la auditoría de errores enviados desde las capas
''' inferiores en el Registro del Visor de Sucesos definido. La definición
''' del Registro de Eventos en la cual debe auditar se debe definir en el 
''' archivo Web.config o App.config dependiendo si la capa de presentación es
''' de ambiente Web o Win32.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	30/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> _
Public Class PadreFachada
    Inherits Aplicacion

#Region "Auditoría de Excepciones"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''   Permite publicar una excepción en el Visor de Sucesos.
    ''' </summary>
    ''' <param name="ex">Excepción a publicar</param>
    ''' <param name="OtrosDatos">Datos adicionales que desean ser auditados. Solo se aplica cuando el proceso de auditoría es local.</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub PublicarExcepcion(ByVal ex As Exception, Optional ByVal OtrosDatos As System.Collections.Specialized.NameValueCollection = Nothing)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'sblMasDatos: Datos que se incluyen en la auditoría
        'intI       : Contador del For
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblMasDatos As New System.Text.StringBuilder
        Dim intI As Integer = 0
        Dim objSesion As Sesion

        '#If DEBUG Then
        '        Return
        '#End If
        objSesion = EntidadesEmpresariales.Sesion.getInstance
        If System.Configuration.ConfigurationManager.AppSettings("AuditarExcepciones").Trim.ToUpper = "NO" Then Exit Sub

        With sblMasDatos
            .Append(vbCrLf)
            .Append("Fecha y Hora Auditoría: ")
            .Append(String.Format("{0:dd/MM/yyyy hh:mm:ss}", System.DateTime.Now))
            .Append(vbCrLf)
            .Append(vbCrLf)
            'Define de donde tomar el objeto cipol.
            If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
                If TryCast(objSesion("objCipol"), System.Security.Principal.IPrincipal) IsNot Nothing Then
                    .Append("Usuario: ")
                    .Append(CType(CType(objSesion("objCipol"), System.Security.Principal.IPrincipal), EntidadesEmpresariales.ICIPOL).Login)
                End If
            Else
                If TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente) IsNot Nothing Then
                    .Append("Usuario: ")
                    .Append(CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente).Login)
                End If
            End If

           

            If OtrosDatos IsNot Nothing Then
                For intI = 0 To OtrosDatos.Keys.Count - 1
                    .Append(vbCrLf)
                    .Append(OtrosDatos.Keys(intI))
                    .Append(": ")
                    .Append(OtrosDatos.Item(intI))
                Next
            End If
            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append("Mensaje del error: ")
            .Append(ex.Message)
            .Append(vbCrLf)
            .Append("StackTrace:")
            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append(ex.StackTrace)
        End With


        System.Diagnostics.EventLog.WriteEntry(System.Configuration.ConfigurationManager.AppSettings("OrigenRegistroEventos"), sblMasDatos.ToString, EventLogEntryType.Error)

    End Sub

    ''' <summary>
    ''' Permite recupera un mensaje de auditoria 
    ''' </summary>
    ''' <param name="Login">Identificador unico del usuario que intento el Login</param>
    ''' <returns>Mensaje recuperado de SE_Mensajes</returns>
    ''' <history>
    '''[LucianoP]          [jueves, 13 de julio de 2017]    Creado 
    '''</history>
    Public Function AuditarIntentoInicioSesionConSesionActiva(ByVal Login As String) As String
        Dim rnCipol As ReglasNegocio.CIPOL = Nothing
        Try
            rnCipol = New ReglasNegocio.CIPOL
            Return rnCipol.AuditarIntentoInicioSesionConSesionActiva(Login)
        Catch ex As Exception
            PublicarExcepcion(ex)
            Throw
        End Try
    End Function

#End Region

#Region "Utilidades"
    '''<summary> Retorna la fecha actual del servidor </summary>
    '''<returns> Fecha y hora actual </returns>
    Public Function FechaServidor() As DateTime
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Autor: gustavom
        'Fecha de Creación: 11/05/2005
        'Modificaciones:
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objEnt : Componente lógico de acceso a datos que se utiliza para recuperar
        '         la fehca y hora del servidor
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objEnt As AccesoDatos.PadreAccesoDatos = Nothing

        Try
            objEnt = New AccesoDatos.PadreAccesoDatos
            objEnt.CrearConexion()
            Return objEnt.FechaServidor
        Finally
            objEnt.Desconectar()
        End Try


    End Function

#End Region

#Region "Impersonate"
    Private mobjUsuario As COA.SO_Windows.UsuarioDominio

    ''' <summary>
    ''' Cambia el contexto de seguridad del usuario ASPNET a un usuario del dominio
    ''' </summary>
    ''' <param name="Usuario">Login del usuario</param>
    ''' <param name="Clave">Clave del usuario</param>
    ''' <param name="Dominio">Nombre del dominio</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[gustavom]	30/01/2009	GCP-Cambios ID: 7750
    ''' </history>
    Public Function CambiarContextoSeguridad(ByVal Usuario As String, ByVal Clave As String, ByVal Dominio As String) As Boolean
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        mobjUsuario = New COA.SO_Windows.UsuarioDominio()
        If mobjUsuario.Autenticar(Usuario, Clave, Dominio, COA.SO_Windows.UsuarioDominio.TipoAutenticacion.RecursosRemotos) Then
            mobjUsuario.Impersonate()
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' Cambia el contexto de seguridad a partir del objeto CIPOL
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [30/01/2009]        GCP-Cambios ID: 7750
    ''' [Gustavom]            [24/06/2010]        
    ''' </history>
    Public Sub CambiarContextoSeguridad()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'objCipol   : Objeto CIPOL
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objCipol As EntidadesEmpresariales.PadreCipolCliente

        'Si no lo puede recuperar del currenprincipal lo toma de la sesión web
        objCipol = TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        If objCipol Is Nothing Then
            objCipol = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        End If

        mobjUsuario = New COA.SO_Windows.UsuarioDominio()
        If mobjUsuario.Autenticar(objCipol.OtrosDatos("usuario"), objCipol.OtrosDatos("clave.usuario"), objCipol.NombreDominio, COA.SO_Windows.UsuarioDominio.TipoAutenticacion.RecursosRemotos) Then
            mobjUsuario.Impersonate()
        End If

    End Sub

    ''' <summary>
    ''' Finaliza el cambio de contexto de seguridad del proceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [30/01/2009]        GCP-Cambios ID: 7750
    ''' </history>
    Public Sub FinalizarCambioConextoSeguridad()
        mobjUsuario.UndoImpersonate()
    End Sub

#End Region

#Region "Manejo de sesion"

    ''' <summary>
    ''' Registra el evento de expiracion/cierre de sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [jueves, 6 de abril de 2017]    Creado 
    ''' </history>
    Public Sub RegistrarExpiroSesion()
        Dim objCladCipol As AccesoDatos.cladCipol = Nothing
        Dim dtsMensajes As DataSet

        Try
            objCladCipol = New AccesoDatos.cladCipol
            objCladCipol.CrearConexion()
            dtsMensajes = objCladCipol.RecuperarMensajes()

            objCladCipol.Auditar(2, CCipolCliente.MensajeAuditoria(dtsMensajes, EntidadesEmpresariales.Sesion.IP.GetIPAddress(), 2), "")

        Catch ex As Exception
        Finally
            If objCladCipol IsNot Nothing Then objCladCipol.Desconectar()
        End Try
    End Sub

#End Region

End Class
