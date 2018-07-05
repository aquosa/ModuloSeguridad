Option Strict On
Option Explicit On

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Convert
Imports EntidadesEmpresariales

Namespace Seguridad
    Partial Public Class wsSeguridad
        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de 
        ''' roles por usuario.
        ''' </summary>
        ''' <param name="pIdUsuario">identificador del usuario</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''  10/03/2006        [AngelL]           Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteRolesXUsuario(ByVal pIdUsuario As Int32) As dtsRolesXUsuarios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADSistema          : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : datset que se retornara
            ' dtsTareasUsuario      : dataset temporal de apoyo al dataset de retorno
            ' intI                  : contador para el While
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As New AccesoDatos.Sistema
            Dim dtsRetorno As New dtsRolesXUsuarios
            Dim dtsTareasUsuario As New dtsUsuarios
            Dim intI As Int32

            Try
                objADSistema.CrearConexion()
                objADSistema.RetornarRolesXUsuarios(dtsRetorno, dtsRetorno.RolesXUsuarios.TableName, , pIdUsuario)
                'ahora verificamos los roles que
                'no estan completos

                'HACK:ANGEL - analizar si esto se pasa a reglas de negocio
                'solo es una desencriptacion de datos, no es nada significante
                'a la lógica del reporte.
                With dtsRetorno.RolesXUsuarios.DefaultView
                    'filtramos por los roles completos indicados como completos
                    'porque en instancias de la consutla SQL, se detectan
                    'los roles que no tienen todas las tareas asignadas a un
                    'usuario, ahora loq ue haremos es ver, de los usuarios
                    'que tienen todas las tareas asignadas, cuales de ellos
                    'tienen tareas inhibidas
                    .RowFilter = "Completo = 'S'"

                    'para cada usuario/rol
                    While intI <= .Count - 1
                        'obtenemos la cantidad de tareas NO INHIBIDAS
                        'que posee el usuario para ese rol y verificamos
                        'si ésta es menor que la cantidad de tareas que tiene un ROL
                        If objADSistema.RetornarTareasXUsuarios(dtsTareasUsuario, dtsTareasUsuario.SE_TAREAS_USUARIO.TableName, ToInt32(.Item(intI)("idUsuario")), ToInt32(.Item(intI)("idRol"))) < ToInt32(.Item(intI)("CANTTAREASROL")) Then
                            'La cantidad de tareas inhibidas es menor que la del rol
                            'por lo tanto, el rol no esta completo
                            .Item(intI)("Completo") = "N"
                            dtsRetorno.RolesXUsuarios.AcceptChanges()
                        End If
                        intI += 1
                    End While

                    'For intI = 0 To .Count - 1
                    'Next intI

                    .RowFilter = ""
                End With
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = New dtsRolesXUsuarios
            Finally
                objADSistema.Desconectar()
            End Try
            'finalizado el chequeo, retornamos el dataset
            Return dtsRetorno
        End Function


        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de roles por usuario Detallado
        ''' </summary>
        ''' <param name="pIdUsuario">identificador del usuario</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [jueves, 15 de noviembre de 2012] Creado GCP-Cambios ID:12853
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteRolesXUsuarioDetalle(ByVal pIdUsuario As Int32, ByRef dtsRetorno As dtsRolesXUsuarios) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADSistema          : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : datset que se retornara
            ' dtsTareasUsuario      : dataset temporal de apoyo al dataset de retorno
            ' intI                  : contador para el While
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As New AccesoDatos.Sistema
            Dim intI As Int32 = 0
            Try
                objADSistema.CrearConexion()
                If objADSistema.RetornarRolesXUsuarioDatosUsr(pIdUsuario, CType(dtsRetorno, Data.DataSet)) > 0 Then
                    intI = objADSistema.RetornarRolesXUsuarioDetalle(pIdUsuario, CType(dtsRetorno, Data.DataSet))
                End If
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = Nothing
                intI = -1
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
            Return intI
        End Function

        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de 
        ''' roles por usuario.
        ''' </summary>
        ''' <param name="pIdRol">identificador del rol</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [jueves, 15 de noviembre de 2012] Creado GCP-Cambios ID:12852
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteUsuariosXRolDetalle(ByVal pIdRol As Int32, ByRef dtsRetorno As dtsRolesXUsuarios) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADSistema          : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : datset que se retornara
            ' dtsTareasUsuario      : dataset temporal de apoyo al dataset de retorno
            ' intI                  : contador para el While
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As New AccesoDatos.Sistema
            Dim intI As Int32 = 0
            Try
                objADSistema.CrearConexion()
                If objADSistema.RetornarUsuariosXRolDatosRol(pIdRol, CType(dtsRetorno, Data.DataSet)) > 0 Then
                    intI = objADSistema.RetornarUsuariosXRolDetalle(pIdRol, CType(dtsRetorno, Data.DataSet))
                End If
                If intI <= 0 Then
                    dtsRetorno = Nothing
                End If
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = Nothing
                intI = -1
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
            Return intI
        End Function


        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de 
        ''' usuarios por rol.
        ''' </summary>
        ''' <param name="pidRol">identificador del rol</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''  10/03/2006        [AngelL]           Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteUsuariosXRol(ByVal pidRol As Int32) As dtsRolesXUsuarios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADSistema          : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : datset que se retornara
            ' dtsTareasUsuario      : dataset temporal de apoyo al dataset de retorno
            ' intI                  : contador para el While
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As New AccesoDatos.Sistema
            Dim dtsRetorno As New dtsRolesXUsuarios
            Dim dtsTareasUsuario As New dtsUsuarios
            Dim intI As Int32

            Try
                objADSistema.CrearConexion()
                objADSistema.RetornarRolesXUsuarios(dtsRetorno, dtsRetorno.RolesXUsuarios.TableName, pidRol)
                'ahora verificamos los roles que
                'no estan completos

                'HACK:ANGEL - analizar si esto se pasa a reglas de negocio
                'solo es una desencriptacion de datos, no es nada significante
                'a la lógica del reporte.
                With dtsRetorno.RolesXUsuarios.DefaultView
                    'filtramos por los roles completos indicados como completos
                    'porque en instancias de la consutla SQL, se detectan
                    'los roles que no tienen todas las tareas asignadas a un
                    'usuario, ahora loq ue haremos es ver, de los usuarios
                    'que tienen todas las tareas asignadas, cuales de ellos
                    'tienen tareas inhibidas
                    .RowFilter = "Completo = 'S'"

                    'para cada usuario/rol
                    While intI <= .Count - 1
                        'obtenemos la cantidad de tareas NO INHIBIDAS
                        'que posee el usuario para ese rol y verificamos
                        'si ésta es menor que la cantidad de tareas que tiene un ROL
                        If objADSistema.RetornarTareasXUsuarios(dtsTareasUsuario, dtsTareasUsuario.SE_TAREAS_USUARIO.TableName, ToInt32(.Item(intI)("idUsuario")), ToInt32(.Item(intI)("idRol"))) < ToInt32(.Item(intI)("CANTTAREASROL")) Then
                            'La cantidad de tareas inhibidas es menor que la del rol
                            'por lo tanto, el rol no esta completo
                            .Item(intI)("Completo") = "N"
                            dtsRetorno.RolesXUsuarios.AcceptChanges()
                        End If
                        intI += 1
                    End While

                    'For intI = 0 To .Count - 1
                    'Next intI

                    .RowFilter = ""
                End With

                'finalizado el chequeo, retornamos el dataset
            Catch ex As Exception

                Me.PublicarExcepcion(ex)
                dtsRetorno = New dtsRolesXUsuarios
            Finally
                objADSistema.Desconectar()
            End Try

            Return dtsRetorno
        End Function

        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de  usuarios por área.
        ''' </summary>
        ''' <param name="IdArea">identificador del Área</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [viernes, 02 de noviembre de 2007]       Creado GCP-Cambios ID: 6001
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteUsuariosXArea(ByVal IdArea As Int32) As dtsUsuariosXAreas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADSistema          : objeto de acceso a datos para el manejo los datos del reporte
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As AccesoDatos.Sistema = Nothing
            Dim dtsRetorno As New dtsUsuariosXAreas

            Try
                objADSistema = New AccesoDatos.Sistema
                objADSistema.CrearConexion()

                objADSistema.RetornarUsuariosXAreas(IdArea, dtsRetorno)

                Return dtsRetorno

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de 
        ''' composicion de roles
        ''' </summary>
        ''' <param name="pidRol">identificador del rol</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''  10/03/2006        [AngelL]           Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteRolesComposicion(ByVal pIdrol As Int32) As dtsRoles
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADRoles            : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : datset que se retornara
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsRetorno As New dtsRoles
            Dim objADRoles As New AccesoDatos.Rol

            Try
                objADRoles.CrearConexion()
                objADRoles.RecuperarRolesComposicion(dtsRetorno, dtsRetorno.Roles_Composicion.TableName, -1, pIdrol)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = New dtsRoles
            Finally
                objADRoles.Desconectar()
            End Try

            Return dtsRetorno
        End Function
        ''' <summary>
        ''' recupera los datos necesris para la generacion de los reportes de
        ''' usuarios por roles
        ''' </summary>
        ''' <returns>el dataset c on los datos necesrios para comenzar la 
        ''' generacion del reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 10/03/006     [AngelL]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaReporteUsuariosXRoles() As dtsRoles
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objaDRoles            : objeto de acceso a datos para el manejo de roels
            ' dtsRetorno            : datset que se retornara
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADRoles As New AccesoDatos.Rol
            Dim dtsRetorno As New dtsRoles
            Try
                objADRoles.CrearConexion()
                objADRoles.RecuperarRoles(dtsRetorno, dtsRetorno.SE_ROLES.TableName)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = New dtsRoles
            Finally
                objADRoles.Desconectar()
            End Try

            Return dtsRetorno
        End Function
        ''' <summary>
        ''' recupera los datos necesris para la generacion de los reportes de
        ''' roles por usuario
        ''' </summary>
        ''' <returns>el dataset con los datos necesrios para comenzar la 
        ''' generacion del reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 10/03/006     [AngelL]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaReporteRolesXUsuarios() As dtsUsuarios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADUsuarios         : objeto de acceso a datos para el manejo de usuarios
            ' dtsRetorno            : datset que se retornara
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim dtsRetorno As New dtsUsuarios

            Try
                objADUsuarios.CrearConexion()
                objADUsuarios.RetornarUsuarios(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, , , True, , , , , True)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = New dtsUsuarios
            Finally
                objADUsuarios.Desconectar()
            End Try

            Return dtsRetorno
        End Function

        ''' <summary>
        ''' Recupera las terminales sin el uso de hablitacion encriptado
        ''' </summary>
        ''' <param name="Habilitadas">Me indica si deseo las habilitadas o no</param>
        ''' <param name="IDArea">ID del area que deseo obtener las terminales</param>
        ''' <param name="TodasLasAreas">Me indica si deseo todas las areas</param>
        ''' <returns>Dataset con las terminales recuperadas</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [viernes, 27 de octubre de 2006]       Creado
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function RecuperarTerminalesParaReporte(ByVal IDArea As Integer, ByVal Habilitadas As String, _
                                                       ByVal TodasLasAreas As Boolean) As dtsReportes
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            '
            'objADTerm:         Componente logico de aaceso a datos
            'objADSist:         Idem anterior
            'dtsRet:            Dataset que guardo las terminales con los datos
            '                   desencriptados
            'rowTerminal:       Fila que utilizo para recorrer todas las terminales
            'objEncriptarNET:   Lo utilizo para encriptar/desencriptar el valor que 
            '                   me indica si una terminal esta habilitada o no
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim objADTerm As AccesoDatos.Terminal = Nothing
            Dim dtsRet As New dtsReportes
            Dim rowTerminal As dtsReportes.SE_TERMINALESRow
            Dim objEncriptarNET As COA.CifrarDatos.TresDES

            Try

                objADTerm = New AccesoDatos.Terminal
                objADTerm.CrearConexion()

                objEncriptarNET = New COA.CifrarDatos.TresDES
                objEncriptarNET.IV = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).IV
                objEncriptarNET.Key = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).Key

                If Habilitadas <> "" Then
                    objADTerm.RecuperarTerminalesPorAreaYHabilitadas(dtsRet, IDArea, objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Encriptacion, Habilitadas), TodasLasAreas)
                Else
                    objADTerm.RecuperarTerminalesPorAreaYHabilitadas(dtsRet, IDArea, "", TodasLasAreas)
                End If

                ' Para todas las terminales recuperadas desencripto el valor de si esta
                ' habilitada o no.
                For Each rowTerminal In dtsRet.SE_TERMINALES.Rows
                    rowTerminal.BeginEdit()
                    rowTerminal.USOHABILITADOSINENC = objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, rowTerminal.USOHABILITADO)
                    rowTerminal.EndEdit()
                Next

                Return dtsRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADTerm IsNot Nothing Then objADTerm.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de Usuarios
        ''' </summary>
        ''' <param name="pIdUsuario">Identificador del usuario</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [lunes, 05 de noviembre de 2007]       Creado GCP-Cambios ID: 4256
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteUsuario(ByVal pIdUsuario As Int32) As dtsUsuarios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADSistema          : objeto de acceso a datos para el manejo los datos del reporte
            ' objADUsuarios         : objeto de acceso a datos para el manejo los datos del reporte
            ' objADTerminales       : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : dataset que se retornara
            ' dtsTareasUsuario      : dataset temporal de apoyo al dataset de retorno
            ' dtsHorario            : dataset en el cual se recuperan los horarios del usuario
            ' intI                  : contador para el While
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim objADSistema As New AccesoDatos.Sistema
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim objADTerminales As New AccesoDatos.Terminal
            Dim dtsRetorno As New dtsUsuarios
            Dim dtsHorario As New dtsUsuarios
            Dim dtsTareasUsuario As New dtsUsuarios
            Dim intI As Int32

            Try

                objADSistema.CrearConexion()
                objADUsuarios.ConexionActiva = objADSistema.ConexionActiva
                objADTerminales.ConexionActiva = objADSistema.ConexionActiva

                'Se recupera los datos del usuario
                objADUsuarios.RetornarUsuarios(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, pFiltroIdUsuario:=pIdUsuario)

                'Se recuperan todas las terminales
                objADTerminales.RecuperarTerminales(dtsRetorno, True) 'GCP-Cambio ID:9287

                'Se recuperan todas las terminales para el usuario
                objADTerminales.RecuperarTerminalesPermitidasUsuario(dtsRetorno, pIdUsuario)

                'Se recuperan todos los Horarios posibles usando el usuario supervisor
                objADUsuarios.RecuperarHorariosUsuario(dtsRetorno, dtsRetorno.SE_Horarios_Usuario.TableName, 0)
                'Se recuperan los Horarios del usuario 
                objADUsuarios.RecuperarHorariosUsuario(dtsHorario, dtsHorario.SE_Horarios_Usuario.TableName, pIdUsuario)
                'Se juntan los datos recuperados
                dtsRetorno.SE_Horarios_Usuario.Merge(dtsHorario.SE_Horarios_Usuario, False, Data.MissingSchemaAction.Ignore)

                '--- Inicio: "Seccion Rol del reporte tomada RecuperarReporteRolesXUsuario()"
                objADSistema.RetornarRolesXUsuarios(dtsRetorno, dtsRetorno.RolesXUsuarios.TableName, , pIdUsuario)

                'ahora verificamos los roles que no estan completos
                'HACK:ANGEL - analizar si esto se pasa a reglas de negocio
                'solo es una desencriptacion de datos, no es nada significante
                'a la lógica del reporte.
                With dtsRetorno.RolesXUsuarios.DefaultView
                    'filtramos por los roles completos indicados como completos
                    'porque en instancias de la consutla SQL, se detectan
                    'los roles que no tienen todas las tareas asignadas a un
                    'usuario, ahora loq ue haremos es ver, de los usuarios
                    'que tienen todas las tareas asignadas, cuales de ellos
                    'tienen tareas inhibidas
                    .RowFilter = "Completo = 'S'"

                    'para cada usuario/rol
                    While intI <= .Count - 1
                        'obtenemos la cantidad de tareas NO INHIBIDAS
                        'que posee el usuario para ese rol y verificamos
                        'si ésta es menor que la cantidad de tareas que tiene un ROL
                        If objADSistema.RetornarTareasXUsuarios(dtsTareasUsuario, dtsTareasUsuario.SE_TAREAS_USUARIO.TableName, ToInt32(.Item(intI)("idUsuario")), ToInt32(.Item(intI)("idRol"))) < ToInt32(.Item(intI)("CANTTAREASROL")) Then
                            'La cantidad de tareas inhibidas es menor que la del rol
                            'por lo tanto, el rol no esta completo
                            .Item(intI)("Completo") = "N"
                            dtsRetorno.RolesXUsuarios.AcceptChanges()
                        End If
                        intI += 1
                    End While

                    .RowFilter = ""
                End With

                '---Fin: "Seccion Rol del reporte tomada RecuperarReporteRolesXUsuario()"

                'Se retorna el dataset
                Return dtsRetorno

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' genera los datos necesarios para emitir el reporte de Usuarios sin Acceso
        ''' </summary>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [lunes, 05 de noviembre de 2007]       Creado GCP-Cambios ID: 6070
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteUsuarioSinAcceso() As dtsUsuarios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADUsuarios         : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : dataset que se retornara
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim dtsRetorno As New dtsUsuarios

            Try

                objADUsuarios.CrearConexion()

                'Se recupera los datos del usuario
                objADUsuarios.RetornarUsuarios(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, pblnFiltroSinAcceso:=True)

                'Se retorna el dataset
                Return dtsRetorno

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADUsuarios IsNot Nothing Then objADUsuarios.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Genera los datos necesarios para emitir el reporte de Control de Inactividad
        ''' </summary>
        ''' <param name="FechaUltimoUsoCta">Es la fecha desde la cual se traen los usuarios que no iniciaron sesión</param>
        ''' <param name="LapsoInactividad">String que indica si traer FechaUltUsoCta menor a 30 dias o mayor</param>
        ''' <returns>dataset con los datos para el reporte</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [lunes, 05 de noviembre de 2007]       Creado GCP-Cambios ID: 6071
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarReporteControlInactividad(ByVal FechaUltimoUsoCta As Date, ByVal LapsoInactividad As String) As dtsCtrlInactividad
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADUsuarios         : objeto de acceso a datos para el manejo los datos del reporte
            ' dtsRetorno            : dataset que se retornara
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim dtsRetorno As New dtsCtrlInactividad

            Try
                objADUsuarios.CrearConexion()

                'Se recupera los datos del usuario
                objADUsuarios.RetornarUsuariosFechaUltUsoCta(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, FechaUltimoUsoCta, LapsoInactividad)

                'Se retorna el dataset
                Return dtsRetorno

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADUsuarios IsNot Nothing Then objADUsuarios.Desconectar()
            End Try

        End Function

    End Class
End Namespace