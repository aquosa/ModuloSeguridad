Option Strict On
Option Explicit On

Imports System.Convert
Imports EE = Fachada.Seguridad
Public Class UsuarioSistema
    Inherits PadreSistema
    ''' <summary>
    ''' genera las acciones necesarioas para el cambio de contraseña de un usuario
    ''' </summary>
    ''' <param name="CantidadContraseniasAlmacenadas"></param>
    ''' <param name="pIdUsuario"></param>
    ''' <param name="MensajeAuditoria"></param>
    ''' <param name="DuracionContrasenia"></param>
    ''' <param name="NuevaContrasenia"></param>
    ''' <param name="mbytObligatorio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''    10/03/2006     [AngelL]     Creado
    '''    [MartinV]          [jueves, 07 de noviembre de 2013]       Modificado  GCP-Cambios 14460 
    '''     "Duración mínima de tiempo, en días, de la contraseña actual del usuario:"
    ''' </history>
    Public Function CambiarContrasenia(ByVal CantidadContraseniasAlmacenadas As Int32, _
            ByVal pIdUsuario As Int32, ByVal MensajeAuditoria As String, ByVal NuevaContrasenia As String, _
            ByVal DuracionContrasenia As Int32, ByVal mbytObligatorio As Byte, ByVal TiempoEnDiasNoPermitirCambiarContrasenia As Integer, ByRef Mensaje As String) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        ' objADUsuario      : objeto de acceso a datos para el manejo de datos de usuarios
        ' objADSistema      : objeto de acceso a datos para la auditoría
        ' dtsTemporal       : dataset de uso temporal
        ' intI              : contador para el for.
        ' intMaxPWD         : cantidad maxima de contraseñas almacenadas del usuario
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADUsuario As New AccesoDatos.UsuarioSistema
        Dim objADSistema As AccesoDatos.Sistema
        Dim dtsTemporal As New EE.dtsUsuariosABM
        Dim intMaxPwd, intI As Int32
        Dim strPwdEncriptada As String
        Dim objCipol As EntidadesEmpresariales.PadreCipolCliente

        If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            objCipol = CType(System.Web.HttpContext.Current.Session("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        Else
            objCipol = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        End If

        'Si se debe mantener un historial de contraseñas
        Try
            objADUsuario.IniciarTransaccion()
            Dim strMensaje As String = ""

            'Si no es obligatorio valido los días minino de cambio de contraseña
            If Not (mbytObligatorio = 3) Then
                '"Duración mínima de tiempo, en días, de la contraseña actual del usuario:"
                If Not ValidarCantidadDiasNoCambiarContrasenia(pIdUsuario, TiempoEnDiasNoPermitirCambiarContrasenia, strMensaje) Then
                    Mensaje = strMensaje
                    Return False
                End If
            End If

            strPwdEncriptada = COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, NuevaContrasenia, pIdUsuario.ToString())

            If CantidadContraseniasAlmacenadas > 0 Then
                objADUsuario.RecuperarContrasenias(dtsTemporal, pIdUsuario)
                With dtsTemporal.SE_HISTORIAL_USUARIO
                    If Not .Rows.Count.Equals(0) Then
                        'Obtengo el orden máximo de claves
                        intMaxPwd = ToInt32(.Rows(0)("Orden"))
                        'Guardo el máximo orden de claves
                        For intI = 0 To .Rows.Count - 1
                            'Si la clave existe en el historico
                            If StrComp(.Rows(intI)("Sinonimo").ToString().Trim() _
                                    , COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, NuevaContrasenia, pIdUsuario.ToString()), CompareMethod.Binary) = 0 Then
                                .Clear()
                                objADSistema = New AccesoDatos.Sistema
                                objADSistema.CrearConexion()
                                AuditarCambios(objADSistema, MensajeAuditoria)
                                objADSistema.Desconectar()
                                objADUsuario.FinalizarTransaccion(True)
                                Mensaje = "La contraseña especificada debe ser diferente a las últimas " & CantidadContraseniasAlmacenadas & " utilizadas"
                                Return False '50  'retornamos el mensaje de usuario numero 50
                            End If
                        Next intI
                    Else
                        'Si no trae registros significa que en el historial de claves
                        'sólo existe la activa
                        intMaxPwd = 0
                    End If
                    .Clear()
                End With

                ' objADUsuario.IniciarTransaccion()

                'Actualizo el historial de claves
                Select Case intMaxPwd
                    Case CantidadContraseniasAlmacenadas - 1
                        'Si la cantidad de claves existentes es igual al límite de historia establecido
                        'elimino la clave más antigua
                        objADUsuario.EliminarDeHistorialUsuario(pIdUsuario, AccesoDatos.UsuarioSistema.TipoComparacion.Igual, intMaxPwd)
                    Case Is > CantidadContraseniasAlmacenadas - 1
                        'Si la cantidad de claves existentes es mayor al límite de historia establecido
                        'elimino las claves que sean necesarias para respetar la política de historial
                        'de clave establecido.
                        objADUsuario.EliminarDeHistorialUsuario(pIdUsuario, AccesoDatos.UsuarioSistema.TipoComparacion.Mayor_o_Igual, CantidadContraseniasAlmacenadas - 1)
                End Select
                'Incremento el orden de las claves historicas actuales
                'para guardar luego en el orden 0 la clave actual
                objADUsuario.ActualizarOrdenesHistorial(pIdUsuario)
            Else
                objADUsuario.IniciarTransaccion()
                'Si la historia de claves está deshabilitada sólo
                'debe existir la activa
                objADUsuario.EliminarDeHistorialUsuario(pIdUsuario, AccesoDatos.UsuarioSistema.TipoComparacion.Ignorar, -1)
            End If

            Dim dtmFecha As DateTime = objADUsuario.FechaServidor()
            'Ingreso la nueva clave
            objADUsuario.AltaHistorialUsuario(pIdUsuario, 0, strPwdEncriptada, dtmFecha, IIf(DuracionContrasenia.Equals(0), DBNull, dtmFecha.AddDays(DuracionContrasenia)), True)
            'Si el cambio de contraseña es forzado por el Administrador de Seguridad
            If mbytObligatorio = 3 Then
                objADUsuario.Forzar_NoForzar_CambioContrasenia(pIdUsuario, (New COA.CifrarDatos.TresDES(objCipol.IV, objCipol.Key)).Criptografia(COA.CifrarDatos.Accion.Encriptacion, "0"))
            End If
            objADUsuario.FinalizarTransaccion(True)

            Return True '-1
        Catch ex As Exception
            objADUsuario.FinalizarTransaccion(False)
            Throw
        End Try

    End Function


    ''' <summary>
    ''' Valida que desde el ultimo cambio de contraseña del usuario
    ''' al momento en que se quiere cambiar hayan pasado CantidadDiasNoCambiarContrasenia dias
    ''' </summary>
    ''' <param name="IDUsuario">ID del usuario</param>
    ''' <param name="CantidadDiasNoCambiarContrasenia">Dias en que no se debe permitir el cambio de contraseña desde el ultimo dia que se cambio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MartinV]          [jueves, 07 de noviembre de 2013]       Creado  GCP-Cambios 14460
    ''' </history>
    Private Function ValidarCantidadDiasNoCambiarContrasenia(ByVal IDUsuario As Integer, _
                                                             ByVal CantidadDiasNoCambiarContrasenia As Integer, _
                                                             ByRef Mensaje As String) As Boolean
        Dim dtmFechaUltimoCambioDeContrasenia As Nullable(Of DateTime)
        Dim dtsFechaHoy As DateTime
        Dim objCladCIPOL As AccesoDatos.UsuarioSistema = Nothing

        Try
            objCladCIPOL = New AccesoDatos.UsuarioSistema
            objCladCIPOL.CrearConexion()

            dtsFechaHoy = objCladCIPOL.FechaServidor().Date
            dtmFechaUltimoCambioDeContrasenia = objCladCIPOL.RecuperarFechaUltimoCambioContrasenia(IDUsuario)

            'Si la fecha de ultimo cambio de contraseña del usuario es nulo, 
            'significa que nunca la cambio y que puede seguir con el proceso de cambio
            If Not dtmFechaUltimoCambioDeContrasenia.HasValue Then
                Return True
            End If

            If dtsFechaHoy.Subtract(dtmFechaUltimoCambioDeContrasenia.Value.Date).Days < CantidadDiasNoCambiarContrasenia Then
                Mensaje = "Las políticas de seguridad impiden que un usuario cambie su contraseña antes de " + CantidadDiasNoCambiarContrasenia.ToString() + " días"
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            If objCladCIPOL IsNot Nothing Then objCladCIPOL.Desconectar()
        End Try
    End Function

    ''' <summary>
    ''' administra un abm de suarios dedicidendo si hace una modificacion o alta
    ''' </summary>
    ''' <param name="pdtsDatos">datos a manejar</param>
    ''' <param name="pstrError">mensaje de error retornado</param>
    ''' <param name="MensajeAuditoria">mensaje de audtoria a auditar</param>
    ''' <param name="CantidadMaximaHistorialContrasenia">parametro para desicion sobre el cambio de contraseña</param>
    ''' <returns>identificador del usuario, 0 si hubo algun error</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''    09/03/2006     [AngelL]      Creado
    ''' </history>
    Public Function AdministrarAbmUsuarios(ByVal pdtsDatos As EE.dtsUsuariosABM, ByRef pstrError As String, ByVal MensajeAuditoria As String, ByVal CantidadMaximaHistorialContrasenia As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        ' objADUsuarios     : objeto de acceso a datos para el manejo de datos de usuarios.
        ' objADTareas       : objeto de acceso a datos para el manejo de datos de tareas
        ' intI              : contador para el for.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim intI As Int32
        Dim objADTareas As New AccesoDatos.Tarea
        Dim objADUsuarios As New AccesoDatos.UsuarioSistema
        Dim objAdSistemastmp As New AccesoDatos.Sistema

        If pdtsDatos.Sist_Usuarios(0).IdUsuario.Equals(-1) Then 'es un alta
            AltaUsuario(pdtsDatos, pstrError)
        Else 'es una modificacion
            ModificarUsuario(pdtsDatos, pstrError, CantidadMaximaHistorialContrasenia)
        End If
        If Not pstrError.Trim.Equals("") Then
            Return 0
        End If
        'se da de alta los roles.
        objADTareas.CrearConexion()
        objADUsuarios.ConexionActiva = objADTareas.ConexionActiva
        Try
            For intI = 0 To pdtsDatos.Roles_Composicion.Rows.Count - 1
                objADTareas.AltaTareasUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario, _
                            pdtsDatos.Roles_Composicion(intI).IdRol _
                        , pdtsDatos.Roles_Composicion(intI).idTarea, _
                        pdtsDatos.Roles_Composicion(intI).TareaInhibida)
            Next intI

            'se da de alta los horarios
            For intI = 0 To pdtsDatos.Tables("SE_Horarios_Usuario").Rows.Count - 1
                objADUsuarios.AltaHorarios(pdtsDatos.Sist_Usuarios(0).IdUsuario, _
                    ToInt32(pdtsDatos.Tables("SE_Horarios_Usuario").Rows(intI).Item("idDia")), _
                    ToInt32(pdtsDatos.Tables("SE_Horarios_Usuario").Rows(intI).Item("IdHorario")))
            Next intI

            'alta de las terminales prohibidas
            For intI = 0 To pdtsDatos.SE_Term_Usuario.Rows.Count - 1
                objADUsuarios.AltaTerminalProhibida(pdtsDatos.Sist_Usuarios(0).IdUsuario, _
                        pdtsDatos.SE_Term_Usuario(intI).IdTerminal)
            Next intI

            'guardo los mensajes de auditoria
            objAdSistemastmp.ConexionActiva = objADUsuarios.ConexionActiva
            For intI = 0 To pdtsDatos.ParametrosDeABM.Rows.Count - 1
                If Not pdtsDatos.ParametrosDeABM(intI).MensajesAuditoria.Trim.Equals("") Then _
                    AuditarCambios(objAdSistemastmp, pdtsDatos.ParametrosDeABM(intI).MensajesAuditoria)
            Next
        Catch
            Throw
        Finally
            objADTareas.Desconectar()
        End Try


        Return pdtsDatos.Sist_Usuarios(0).IdUsuario
    End Function

    Private Function AltaUsuario(ByVal pdtsDatos As EE.dtsUsuariosABM, ByRef pstrError As String) As Int32
        Dim objADUsuarios As New AccesoDatos.UsuarioSistema
        Dim dtsTemporal As New DataSet
        Dim intId As Int32

        Try
            objADUsuarios.IniciarTransaccion()
            'Verifico que el usuario no exista en la base de datos si es un alta
            'Si se trata de una modificación, veoq ue sea distinto al id que estoy manejando
            If Not objADUsuarios.RetornarUsuarios(dtsTemporal, "Sist_usuarios", pdtsDatos.Sist_Usuarios(0).Usuario, System.Convert.ToInt32(pdtsDatos.Sist_Usuarios(0).IdUsuario)).Equals(0) Then
                pstrError = "El nombre de usuario ya existe."
                Return 0
            End If

            intId = objADUsuarios.MaximoIdUsuarioDisponible()
            pdtsDatos.Sist_Usuarios(0).IdUsuario = intId
            objADUsuarios.AltaDeUsuario(pdtsDatos)
            If pdtsDatos.Sist_Usuarios(0).ForzarCambio = "1" Then
                objADUsuarios.AltaHistorialUsuario(intId, 0, COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, pdtsDatos.SE_HISTORIAL_USUARIO(0).SINONIMO, intId.ToString()), objADUsuarios.FechaServidor(), IIf(Int32.Parse(pdtsDatos.ParametrosDeABM(0).DiasVencimientoDeClave).Equals(0), Nothing, New DateTime(1900, 1, 1)), False)
            Else
                objADUsuarios.AltaHistorialUsuario(intId, 0, COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, pdtsDatos.SE_HISTORIAL_USUARIO(0).SINONIMO, intId.ToString()), objADUsuarios.FechaServidor(), IIf(Int32.Parse(pdtsDatos.ParametrosDeABM(0).DiasVencimientoDeClave).Equals(0), Nothing, objADUsuarios.FechaServidor().AddDays(Int32.Parse(pdtsDatos.ParametrosDeABM(0).DiasVencimientoDeClave))), False)
            End If

            objADUsuarios.FinalizarTransaccion(True)
        Catch ex As Exception
            objADUsuarios.FinalizarTransaccion(False)
            Throw
        End Try

        Return intId
    End Function


    Private Function ModificarUsuario(ByVal pdtsDatos As EE.dtsUsuariosABM, ByRef pstrError As String, ByVal CantidadMaximaHistorialContraseniaa As Int32) As Int32
        Dim objADUsuarios As New AccesoDatos.UsuarioSistema
        Dim dtsTemporal As New DataSet

        'Verifico que el usuario no exista en la base de datos si es un alta
        'Si se trata de una modificación, veoq ue sea distinto al id que estoy manejando

        Try
            objADUsuarios.IniciarTransaccion()
            If Not objADUsuarios.RetornarUsuarios(dtsTemporal, "Sist_usuarios", pdtsDatos.Sist_Usuarios(0).Usuario, System.Convert.ToInt32(pdtsDatos.Sist_Usuarios(0).IdUsuario)).Equals(0) Then
                pstrError = "El nombre de usuario ya existe."
                Return 0
            End If

            objADUsuarios.EliminarTareasUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario)
            objADUsuarios.EliminarTerminalesProhibidas(pdtsDatos.Sist_Usuarios(0).IdUsuario)
            objADUsuarios.EliminarHorarios(pdtsDatos.Sist_Usuarios(0).IdUsuario)
            objADUsuarios.ActualizarUsuario(pdtsDatos)

            'si se cambió la contraseña
            If pdtsDatos.ParametrosDeABM(0).CambioContrasenia.Trim.ToUpper.Equals("SI") Then
                'Actualizo el historial de claves
                If CantidadMaximaHistorialContraseniaa > 0 Then
                    Select Case pdtsDatos.SE_HISTORIAL_USUARIO.Rows.Count
                        Case CantidadMaximaHistorialContraseniaa - 1
                            'Si la cantidad de claves existentes es igual al límite de historia establecido
                            'elimino la clave más antigua
                            objADUsuarios.EliminarDeHistorialUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario, AccesoDatos.UsuarioSistema.TipoComparacion.Igual, pdtsDatos.SE_HISTORIAL_USUARIO.Rows.Count)
                        Case Is > CantidadMaximaHistorialContraseniaa - 1
                            'Si la cantidad de claves existentes es mayor al límite de historia establecido
                            'elimino las claves que sean necesarias para respetar la política de historial
                            'de clave establecido.
                            objADUsuarios.EliminarDeHistorialUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario, AccesoDatos.UsuarioSistema.TipoComparacion.Mayor_o_Igual, CantidadMaximaHistorialContraseniaa - 1)
                    End Select
                    'Incremento el orden de las claves historicas actuales
                    'para guardar luego en el orden 0 la clave actual
                    objADUsuarios.ActualizarOrdenesHistorial(pdtsDatos.Sist_Usuarios(0).IdUsuario)
                Else
                    'Si la historia de claves está deshabilitada sólo
                    'debe existir la activa
                    objADUsuarios.EliminarDeHistorialUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario, AccesoDatos.UsuarioSistema.TipoComparacion.Ignorar, Nothing)
                End If
                If pdtsDatos.Sist_Usuarios(0).ForzarCambio = "1" Then
                    objADUsuarios.AltaHistorialUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario, 0, pdtsDatos.SE_HISTORIAL_USUARIO(0).SINONIMO, objADUsuarios.FechaServidor(), IIf(Int32.Parse(pdtsDatos.ParametrosDeABM(0).DiasVencimientoDeClave).Equals(0), Nothing, New DateTime(1900, 1, 1)), False)
                Else
                    objADUsuarios.AltaHistorialUsuario(pdtsDatos.Sist_Usuarios(0).IdUsuario, 0, pdtsDatos.SE_HISTORIAL_USUARIO(0).SINONIMO, objADUsuarios.FechaServidor(), IIf(Int32.Parse(pdtsDatos.ParametrosDeABM(0).DiasVencimientoDeClave).Equals(0), Nothing, objADUsuarios.FechaServidor().AddDays(Int32.Parse(pdtsDatos.ParametrosDeABM(0).DiasVencimientoDeClave))), False)
                End If
            End If

            objADUsuarios.FinalizarTransaccion(True)
        Catch ex As Exception
            objADUsuarios.FinalizarTransaccion(False)
            Throw
        End Try

    End Function

    ''' <summary>
    ''' elimina un usuario y audita la accion realizada
    ''' </summary>
    ''' <param name="pidUsuario">identificador del usuario</param>
    ''' <param name="strMensajeAuditoria">mensaje para la auditoria</param>
    ''' <returns>fecha de baja del usuario en formato YYYYMMDD</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''    09/03/2005       [AngelL]       Creado
    ''' </history>
    Public Function EliminarUsuarios(ByVal pidUsuario As Int32, ByVal strMensajeAuditoria As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        ' objADUsuarios     : objeto de acceso a datos para el manejo de datos de usuarios.
        ' objAdSistemas     : objeto de acceo a datos para el manejo de datos de sistemas.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADUsuarios As New AccesoDatos.UsuarioSistema
        Dim objaDsistemas As New AccesoDatos.Sistema
        Dim intRetorno As Int32


        'si la eliminacion es distinto de 1, entonces retornamos nulo indicando
        'que algun error sucedió.
        Try
            objADUsuarios.IniciarTransaccion()
            objaDsistemas.ConexionActiva = objADUsuarios.ConexionActiva
            If objADUsuarios.EliminarUsuarios(pidUsuario) <> 1 Then Return 0
            'auditamos la accion
            Me.AuditarCambios(objaDsistemas, strMensajeAuditoria)
            intRetorno = objADUsuarios.FechaServidor().Year * 10000 + objADUsuarios.FechaServidor().Month * 100 + objADUsuarios.FechaServidor.Day
            objADUsuarios.FinalizarTransaccion(True)
            Return intRetorno
        Catch ex As Exception
            objADUsuarios.FinalizarTransaccion(False)
            Throw
        End Try

    End Function

    ''' <summary>
    ''' Activa un usuario y audita la accion realizada
    ''' </summary>
    ''' <param name="pidUsuario">identificador del usuario</param>
    ''' <param name="strMensajeAuditoria">mensaje para la auditoria</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [miércoles, 18 de junio de 2008]       Creado GCP-Cambios ID: 6995
    ''' </history>
    Public Function ActivarUsuarios(ByVal pidUsuario As Int32, ByVal strMensajeAuditoria As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        ' objADUsuarios     : objeto de acceso a datos para el manejo de datos de usuarios.
        ' objAdSistemas     : objeto de acceo a datos para el manejo de datos de sistemas.
        ' intRetorno        : Cantidad de filas afectadas
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim objADUsuarios As New AccesoDatos.UsuarioSistema
        Dim objaDsistemas As New AccesoDatos.Sistema
        Dim intRetorno As Int32

        Try
            objADUsuarios.IniciarTransaccion()
            objaDsistemas.ConexionActiva = objADUsuarios.ConexionActiva

            intRetorno = objADUsuarios.ActivarUsuarios(pidUsuario)
            If intRetorno <> 1 Then Return 0

            'auditamos la accion
            Me.AuditarCambios(objaDsistemas, strMensajeAuditoria)

            objADUsuarios.FinalizarTransaccion(True)
            Return intRetorno

        Catch ex As Exception
            objADUsuarios.FinalizarTransaccion(False)
            Throw
        End Try

    End Function

    ''' <summary>
    ''' Recuperar los usuarios habilitados para un sistema indicado
    ''' </summary>
    ''' <param name="IDSistema">ID del sistema a recuperar sus usuarios</param>
    ''' <returns></returns>
    ''' String con los ID y Nombres de los usuarios recuperados para el ID de sistema indicado
    ''' Formato de String: IDUsuario1:Nombre Usuario1,IDUsuario2:Nombre Usuario2,...
    ''' <remarks></remarks>
    Public Function Recuperar_UsuariosXSistema(ByVal IDSistema As Int32) As String
        Return Recuperar_UsuariosXSistema(IDSistema, Nothing)
    End Function
    ''' <summary>
    ''' Recuperar los usuarios habilitados para un sistema indicado
    ''' </summary>
    ''' <param name="IDSistema">ID del sistema a recuperar sus usuarios</param>
    ''' <param name="dtsRetorno">DataSet de retorno con los usuarios recuperados para el ID de sistema indicado</param>
    ''' <returns></returns>
    ''' String con los ID y Nombres de los usuarios recuperados para el ID de sistema indicado
    ''' Formato de String: IDUsuario1:Nombre Usuario1,IDUsuario2:Nombre Usuario2,...
    ''' <remarks></remarks>
    Public Function Recuperar_UsuariosXSistema(ByVal IDSistema As Int32, ByRef dtsRetorno As EE.dtsUsuarios) As String
        Dim adUsuarios As New AccesoDatos.UsuarioSistema
        Dim dtsDatos As New EE.dtsUsuarios
        Dim objEncriptarNET As New COA.CifrarDatos.TresDES
        Dim strRta As String = ""
        Dim objSesion As Sesion
        Dim objCipol As EntidadesEmpresariales.PadreCipolCliente
        Try
            adUsuarios.CrearConexion()
            dtsDatos = adUsuarios.Recuperar_UsuariosXSistema(IDSistema)
            objSesion = New Sesion
            objCipol = CType(objSesion.ObtenerObjetoSesion("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
            For intI As Integer = 0 To dtsDatos.UsuariosXSistema.Rows.Count - 1
                With dtsDatos.UsuariosXSistema(intI)
                    .BeginEdit()
                    Try
                        objEncriptarNET.IV = objCipol.IV
                        objEncriptarNET.Key = objCipol.Key
                        .CTABLOQUEADA = objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, .CTABLOQUEADA)
                        If .CTABLOQUEADA.Trim.Equals("1") Then
                            .Delete()
                        Else
                            strRta = strRta + IIf(strRta = "", "", ",").ToString() + _
                            .IDUsuario.ToString().Trim() + ":" + .Nombres.Trim()
                        End If
                    Catch ex As Exception
                        Throw
                    End Try
                    .EndEdit()
                End With
            Next
            dtsRetorno = dtsDatos
            Return strRta
        Catch ex As Exception
            dtsRetorno = Nothing
            Throw
        Finally
            adUsuarios.Desconectar()
        End Try
    End Function

End Class
