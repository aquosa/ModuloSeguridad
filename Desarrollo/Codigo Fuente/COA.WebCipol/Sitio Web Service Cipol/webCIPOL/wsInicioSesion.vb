Option Explicit On
Option Strict On

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Runtime
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports EntidadesEmpresariales

Namespace Seguridad.IniciarSesion

    '<Microsoft.Web.Services3.Policy("PoliticaSeguridad")> _

    <WebService(Namespace:="http://RGP/CIPOL/InicioSesion")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class wsInicioSesion
        Inherits PadreSistema

        ''' <summary>
        ''' Inicia una sesion de cipol, en base al nombre de usuario y la terminal
        ''' </summary>
        ''' <param name="pUsuario">nombre de login de usuario compuesto con el dominio</param>
        ''' <param name="pTerminal">nombre de la terminal desde la que el usuario se loguea</param>
        ''' <param name="pError">Mensaje de error</param>
        ''' <param name="pPassword">Clave del usuario</param>
        ''' <param name="pTerminal_ActualizacionLAN">Indica si la terminal se actualiza a través de un servidor de la LAN o remoto</param>
        ''' <returns>retorna una serialización del objeto de seguridad</returns>
        ''' <remarks>
        ''' [AngelL] 27/02/2006 - Creado.
        ''' 	[gustavom]	martes, 23 de octubre de 2007	GCP-Cambios ID: 5992    
        ''' 	[gustavom]	20/09/2005	GCP-Cambios ID: 7801
        ''' </remarks>
        <WebMethod(enableSession:=True)> _
        Public Function IniciarSesion(ByVal pUsuario As String, ByVal pTerminal As String, ByRef pError As String, ByVal pPassword As String, ByRef pTerminal_ActualizacionLAN As Boolean) As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DECLARACION DE VARIABLES LOCALES
            ' objSerializador   : objeto serializador para cipol
            ' objFlujo          : flujo de memoria para serializar
            ' objEnc            : Algoritmo de encriptación RSA
            ' bytCipol          : Array que contiene el objeto CIPOL serializado
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objSerializador As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim objFlujo As New System.IO.MemoryStream
            Dim objEnc As System.Security.Cryptography.RSACryptoServiceProvider
            Dim bytCipol() As Byte

            Dim objCipol As New Fachada.CCipolCliente

            Try
                'Desencripta con la clave privada del algoritmo asimétrico RSA
                objEnc = New System.Security.Cryptography.RSACryptoServiceProvider()
                objEnc.ImportCspBlob(CType(Sesion.getInstance("RSAServ"), Byte()))
                pPassword = System.Text.UTF8Encoding.UTF8.GetString(objEnc.Decrypt(System.Convert.FromBase64String(pPassword), False))

                pTerminal_ActualizacionLAN = False
                objCipol.OtrosDatos("usuario", pUsuario)
                objCipol.OtrosDatos("clave.usuario", pPassword)
                objCipol.OtrosDatos("terminal", pTerminal)
                objCipol.OtrosDatos("fecha.servidor", String.Format("{0:dd/MM/yyyy}", FechaServidor.Date))

                If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
                    System.Web.HttpContext.Current.Session("objCipol") = objCipol
                Else
                    System.Threading.Thread.CurrentPrincipal = objCipol
                End If

                If objCipol.IniciarSesion(pUsuario, pPassword, pTerminal, pTerminal_ActualizacionLAN) Then
                Else
                    pError = objCipol.MensajeError
                    Return ""
                End If

                'serializo
                'todo: martinv -> ver el tema de la key.
                ''Dim objRetorno As Fachada.CCipolCliente = objCipol.NuevoPadreCipolCliente()
                Dim objRetorno As EntidadesEmpresariales.PadreCipolCliente = objCipol.NuevoPadreCipol()

                'Encripta con la clave pública del cliente datos criticos que se retornan con el objeto 
                'CIPOL
                objEnc.ImportCspBlob(CType(Sesion.getInstance("RSACli"), Byte()))

                pPassword = objCipol.OtrosDatos("clave.usuario")
                objRetorno.OtrosDatos("clave.usuario", System.Convert.ToBase64String(objEnc.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(pPassword), False)))
                objRetorno.Key = System.Convert.ToBase64String(objEnc.Encrypt(System.Convert.FromBase64String(objCipol.Key), False))
                objRetorno.IV = System.Convert.ToBase64String(objEnc.Encrypt(System.Convert.FromBase64String(objCipol.IV), False))
                objRetorno.FechaServidor = Me.FechaServidor()
                objSerializador.Serialize(objFlujo, objRetorno)


                objCipol.IDSistemaActual = 1

                Sesion.getInstance("objCipol") = objCipol


                'Retorna el objeto CIPOL serializado
                bytCipol = objFlujo.ToArray()
                Return System.Convert.ToBase64String(bytCipol)

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                pError += ex.Message
                Return Nothing
            End Try


        End Function

        ''' <summary>
        ''' Retorna la clave pública del algoritmo asimétrico utilizado desde el servidor
        ''' y recibe la clave pública del algoritmo asimétrico utilizado por el cliente
        ''' </summary>
        ''' <returns>Clave publica que se retorna</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Gustavom]            [viernes, 26 de marzo de 2010]        GCP-Cambios ID: 8891
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function GetClavePublica(ByVal ClavePublicaCliente() As Byte) As Byte()
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objRSA     : Objeto de encriptación de algoritmo asimétrico utilizado por el servidor
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRSA As System.Security.Cryptography.RSACryptoServiceProvider


            Try
                objRSA = New System.Security.Cryptography.RSACryptoServiceProvider()
                Sesion.getInstance("RSAServ") = objRSA.ExportCspBlob(True)

                Sesion.getInstance("RSACli") = ClavePublicaCliente

                Return objRSA.ExportCspBlob(False)

            Catch ex As Exception
                Me.FachadaCOA.PublicarExcepcion(ex)
                Return Nothing
            End Try

        End Function

        ''' <summary>
        ''' Cierra la sesión del usuario
        ''' </summary>
        ''' <remarks>
        ''' [Gustavom] 06/04/2006 - Creado.
        ''' </remarks>
        <WebMethod(enableSession:=True)> _
        Public Sub CerrarSesion()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DECLARACION DE VARIABLES LOCALES
            'objADCipol : Componente lógico de acceso a datos que se utiliza para 
            '             cerrar la sesión
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objCipol As Fachada.CCipolCliente = Nothing


            Try
                objCipol = New Fachada.CCipolCliente
                objCipol.CerrarSesion()
                Sesion.getInstance.Abandon()
            Catch ex As Exception
            End Try

        End Sub

        ''' <summary>
        ''' Permite autenticar un usuario
        ''' </summary>
        ''' <param name="Usuario">Login del usuario</param>
        ''' <param name="Clave">Clave actual del usuario</param>
        ''' <param name="MensajeError">Mensaje de error a mostrar cuando la clave no es correcta</param>
        ''' <returns>True o False</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Gustavom]            [martes, 03 de marzo de 2009]        GCP-Cambios ID: 7844
        ''' [Gustavom]            [miércoles, 18 de marzo de 2009]     GCP-Cambios ID: 7907
        ''' [Gustavom]            [lunes, 18 de mayo de 2009]          GCP-Cambios ID: 8107
        ''' [Gustavom]            [lunes, 18 de mayo de 2009]          GCP-Cambios ID: 8923
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function ValidarContraseña(ByVal Usuario As String, ByVal Clave As String, ByRef MensajeError As String) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DECLARACION DE VARIABLES LOCALES
            'intID      : Identificador del usuario
            'objRSA     : Algoritmo RSA que contiene la clave privada
            'strClave   : Clave del usuario
            'bytKey     : Clave asimétrica
            'blnRet     : Valor de retorno
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objUsuario As New Fachada.CCipolCliente
            Dim intRet As Integer
            Dim objRSA As System.Security.Cryptography.RSACryptoServiceProvider
            Dim strClave As String
            Dim bytKey() As Byte
            Dim blnRet As Boolean

            Try
                objRSA = New System.Security.Cryptography.RSACryptoServiceProvider()
                bytKey = CType(Sesion.getInstance("RSAServ"), Byte())
                objRSA.ImportCspBlob(bytKey)
                strClave = System.Text.UTF8Encoding.UTF8.GetString(objRSA.Decrypt(System.Convert.FromBase64String(Clave), False))

                intRet = objUsuario.ClaveCorrecta(Usuario, strClave)
                If intRet = -99 Then
                    MensajeError = objUsuario.MensajeError
                    blnRet = False
                Else
                    blnRet = True
                End If

                Return blnRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                MensajeError = String.Empty
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Registra el mensaje de auditoría de seguridad que se recibe
        ''' </summary>
        ''' <param name="MensajeAuditoria">Mensaje de auditoría a grabar</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <WebMethod(EnableSession:=True)> _
        Public Function Auditar(ByVal MensajeAuditoria As String) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DECLARACION DE VARIABLES LOCALES
            'objAuditar : Componente que se utiliza para grabar la auditoria
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objAuditar As ReglasNegocio.Sistema

            Try
                objAuditar = New ReglasNegocio.Sistema
                Return objAuditar.RegistrarAuditoriaSeguridad(MensajeAuditoria)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Cambia la contraseña de un usuario dado
        ''' </summary>
        ''' <param name="CantidadContraseniasAlmacenadas">Histórico de contraseñas que debe mantenerse</param>
        ''' <param name="pIdUsuario">identificador del usuario</param>
        ''' <param name="Usuario">Login del usuario</param>
        ''' <param name="MensajeAuditoria">mensaje de auditoria en caso que sea neccesario</param>
        ''' <param name="NuevaContrasenia">Contrasenia nueva.</param>
        ''' <param name="DuracionContrasenia">duracion de la contraseña antes que vensa (en días)</param>
        ''' <param name="mbytObligatorio">indica si el cambio de contraseña es obligatorio.</param>
        ''' ç<param name="ContraseñaActual">Clave actual del usuario</param>
        ''' <param name="MensajeError">Mensaje de error</param>
        ''' <returns>-2 ocurrio excepción, -1 si todo salio bien, o el numero de mensaje a mostrar en caso contrario.</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    10/03/2006      [AngelL]        Creado.
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function CambiarContrasenia(ByVal CantidadContraseniasAlmacenadas As Int32, _
                ByVal pIdUsuario As Int32, ByVal Usuario As String, ByVal MensajeAuditoria As String, ByVal NuevaContrasenia As String, _
                ByVal DuracionContrasenia As Int32, ByVal mbytObligatorio As Byte, ByVal ContraseñaActual As String, ByRef MensajeError As String, _
                ByVal TiempoEnDiasNoPermitirCambiarContrasenia As Integer) As Boolean
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DEFINICION DE VARIABLES LOCALES
            'objRNUsuarios : Objeto de reglas de negocio para el cambio de contraseña
            'strMensaje    : Mensaje a retornar cuando la contraseña no es válida
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNUsuarios As New ReglasNegocio.UsuarioSistema
            'Dim strMensaje As String = String.Empty
            Dim objRSA As System.Security.Cryptography.RSACryptoServiceProvider
            Dim bytKey() As Byte
            Dim strClaveNueva As String


            Try

                objRSA = New System.Security.Cryptography.RSACryptoServiceProvider()
                bytKey = CType(Sesion.getInstance("RSAServ"), Byte())
                objRSA.ImportCspBlob(bytKey)
                strClaveNueva = System.Text.UTF8Encoding.UTF8.GetString(objRSA.Decrypt(System.Convert.FromBase64String(NuevaContrasenia), False))

                If Me.ValidarContraseña(Usuario, ContraseñaActual, MensajeError) Then
                    Return objRNUsuarios.CambiarContrasenia(CantidadContraseniasAlmacenadas, pIdUsuario, MensajeAuditoria, strClaveNueva, DuracionContrasenia, mbytObligatorio, TiempoEnDiasNoPermitirCambiarContrasenia, MensajeError)
                Else
                    Return False
                End If
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                MensajeError = "ocurrio error"
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Supervisa una tarea primitiva
        ''' </summary>
        ''' <param name="Usuario">Login del usuario</param>
        ''' <param name="Clave">Clave actual del usuario</param>
        ''' <param name="IDTarea">Identificador de la tarea primitiva que se supervisa</param>
        ''' <param name="MensajeError">Mensaje de error a mostrar cuando la clave no es correcta</param>
        ''' <returns>True o False</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Gustavom]            [lunes, 18 de mayo de 2009]          GCP-Cambios ID: 8923
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function SupervisarUsuario(ByVal Usuario As String, ByVal Clave As String, ByVal IDTarea As Integer, ByRef MensajeError As String) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DECLARACION DE VARIABLES LOCALES
            'blnRet     : Valor de retorno
            'objRSA     : Algoritmo RSA que contiene la clave privada
            'strClave   : Clave del usuario
            'bytKey     : Clave asimétrica
            'intID      : Identificador del usuario
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objUsuario As New Fachada.CCipolCliente
            Dim blnRet As Boolean = False
            Dim strClave As String
            Dim intID As Integer

            Try
#If StandAlone Then
                strClave = Clave
#Else
                Dim objRSA As System.Security.Cryptography.RSACryptoServiceProvider
                Dim bytKey() As Byte

                objRSA = New System.Security.Cryptography.RSACryptoServiceProvider()
                bytKey = CType(Sesion.getInstance("RSAServ"), Byte())
                objRSA.ImportCspBlob(bytKey)
                strClave = System.Text.UTF8Encoding.UTF8.GetString(objRSA.Decrypt(System.Convert.FromBase64String(Clave), False))
#End If

                intID = objUsuario.ClaveCorrecta(Usuario, strClave)
                If intID = -99 Then
                    MensajeError = objUsuario.MensajeError
                    blnRet = False
                Else
                    objUsuario.AuditarSupervision(intID, IDTarea)
                    blnRet = True
                End If

                Return blnRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                MensajeError = String.Empty
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Obtiene la Fecha y Hora del Servidor de Base de Datos
        ''' </summary>
        ''' <returns>Fecha y Hora actual del Servidor de base de datos</returns>
        ''' <remarks></remarks>
        Private Function FechaServidor() As Date
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DEFINICION DE VARIABLES LOCALES
            'cadPadre : Clase de Acceso a Datos padre
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cadPadre As New AccesoDatos.PadreAccesoDatos
            Try
                cadPadre.CrearConexion()
                Return cadPadre.FechaServidor()
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
            Finally
                cadPadre.Desconectar()
            End Try

        End Function
        ''' <summary>
        ''' Recupera el nombre del dominio donde se va a validar el usuario
        ''' </summary>
        ''' <returns></returns>
        ''' String con el nombre del Dominio 
        ''' Si tiene un * significa que es SEGURIDAD MIXTA y No es SOLO DOMINIO
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 21 de mayo de 2009]   Creado
        ''' </history>
        <WebMethod()> _
        Public Function RecuperarNombreDominio() As String
            Dim oCipol As New ReglasNegocio.CIPOL
            Return oCipol.NombreDominio.Trim + CType(IIf(oCipol.Seguridad_SoloDominio, "", "*"), String)
        End Function

        ''' <summary>
        ''' Recupera los usuarios habilitados de un sistema indicado
        ''' </summary>
        ''' <param name="IDSistema">Id del sistema que se quiere recuperar los usuarios</param>
        ''' <returns></returns>
        ''' String con los usuarios habilitados para el sistema indicado
        ''' <remarks></remarks>
        <WebMethod(enableSession:=True)> _
        Public Function UsuariosXSistema(ByVal IDSistema As Int32) As String
            Dim objRegla As New ReglasNegocio.UsuarioSistema
            Try
                Return objRegla.Recuperar_UsuariosXSistema(IDSistema)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return ""
            End Try
        End Function

        ''' <summary>
        ''' Recupera los usuarios habilitados de un sistema indicado
        ''' </summary>
        ''' <param name="IDSistema">Id del sistema que se quiere recuperar los usuarios</param>
        ''' <returns></returns>
        ''' Dataset con los usuarios habilitados para el sistema indicado
        ''' <remarks></remarks>
        <WebMethod(enableSession:=True)> _
        Public Function Recuperar_UsuariosXSistema(ByVal IDSistema As Int32, ByRef dtsRetorno As dtsUsuarios) As String
            Dim objRegla As New ReglasNegocio.UsuarioSistema
            Try
                Return objRegla.Recuperar_UsuariosXSistema(IDSistema, dtsRetorno)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return ""
            End Try
        End Function

        ''' <summary>
        ''' Permite registrar la expiracion de sesion
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [jueves, 6 de abril de 2017]    Creado 
        ''' </history>
        <WebMethod()> _
        Public Sub RegistrarExpiroSesion()
            Dim objFIS As Global.Fachada.PadreFachada = Nothing
            Try
                objFIS = New Global.Fachada.PadreFachada
                objFIS.RegistrarExpiroSesion()
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Throw New SoapException("Ocurrió un error al intentar registrar la expiración de la sesión", System.Xml.XmlQualifiedName.Empty)
            End Try
        End Sub

    End Class

End Namespace
