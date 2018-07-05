Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports EntidadesEmpresariales

Namespace Seguridad

    <WebService(Namespace:="http://RGP/CIPOL/Seguridad")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Public Class wsSeguridad
        Inherits PadreSistema

        Private Const VersionDelSitio As String = "2.4.1.0"

        ''' <summary>
        ''' Se usa para mantener activa la sesion en el sitio y recuperar información de 
        ''' versiones
        ''' </summary>
        ''' <param name="Codigo">Códigos posibles: 
        ''' 1. Versión del sitio
        ''' 2. Alias de Conexión a la base de datos
        ''' </param>
        ''' <returns>Valor de retorno de acuerdo al código recibido. Si el código no existe se retorna cadena vacía</returns>
        ''' <remarks></remarks>
        <WebMethod(enableSession:=True)> _
        Public Function Ping(ByVal Codigo As Integer) As String
            Select Case Codigo
                Case 1
                    Return VersionDelSitio
                Case 2
                    Try
                        Dim objArchivo As New COA.Datos.AdmConexion(System.Configuration.ConfigurationManager.AppSettings("ArchivoConexion"))
                        If objArchivo.Buscar(objArchivo.ObtenerIDConexiones()(0)) Then
                            Return objArchivo.AliasConexion
                        Else
                            Return ""
                        End If
                    Catch ex As Exception
                        'Si no se pudo recuperar el alias 
                        Return ""
                    End Try
                Case 3
                    If Sesion.getInstance("UltimoError") Is Nothing Then
                        Return ""
                    Else
                        Dim strError As String = Sesion.getInstance("UltimoError").ToString
                        Sesion.getInstance.Remove("UltimoError")
                        Return strError
                    End If
                Case Else
                    Return ""
            End Select

        End Function

        ''' <summary>
        ''' Recupera las políticas de seguridad existentes
        ''' </summary>
        ''' <returns>DataSet con las políticas de seguridad</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 27/02/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarPoliticasGenerales() As System.Data.DataSet
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADParam : Componente lógico de acceso a datos que se utiliza
            '             para recuperar las políticas generales
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADParam As AccesoDatos.Sistema = Nothing

            Try
                objADParam = New AccesoDatos.Sistema
                objADParam.CrearConexion()
                Return objADParam.RecuperarPoliticasGenerales()
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADParam.Desconectar()
            End Try


        End Function

        ''' <summary>
        ''' Recupera las políticas de seguridad y los mensajes de auditoría y de usuarios
        ''' </summary>
        ''' <returns>DataSet de retorno de datos</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 10/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaEntornoCIPOLAdministrador() As System.Data.DataSet
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'dtsRet      : DataSet de retorno de datos
            'objADSistema: Componente lógico de acceso a datos que se utiliza para 
            '              recuperar los datos necesarios para configurar el entorno
            '              de CIPOL Administrador
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsRet As System.Data.DataSet = Nothing
            Dim objADSistema As AccesoDatos.Sistema = Nothing

            Try
                objADSistema = New AccesoDatos.Sistema
                objADSistema.CrearConexion()
                dtsRet = objADSistema.RecuperarPoliticasGenerales()
                objADSistema.RecuperarCodigosAuditoria(dtsRet)
                objADSistema.RecuperarMensajesDeAuditoria(dtsRet)

                Return dtsRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADSistema.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Permite el ingreso o actualización de las políticas generales
        ''' </summary>
        ''' <param name="dtsDatos">Dataset que contiene las políticas generales a ingresar o actualizar</param>
        ''' <returns>True o False</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function AdministrarPoliticasGenerales(ByVal dtsDatos As System.Data.DataSet) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objSistema : Componente de regla de negocio que se utiliza
            '             para ingresar o actualizar las políticas de 
            '             seguridad
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objSistema As ReglasNegocio.Sistema = Nothing

            If dtsDatos Is Nothing Then Throw New ArgumentException("El dataset no puede estar vacío")
            If dtsDatos.Tables.Count = 0 OrElse dtsDatos.Tables(0).Rows.Count = 0 Then Throw New ArgumentException("El dataset no puede estar vacío")
            Try
                objSistema = New ReglasNegocio.Sistema
                Return objSistema.AdministrarPoliticasGenerales(dtsDatos)

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Recupera los datos necesarios para trabajar en el formolario del visor de su
        ''' sucesos de seguridad
        ''' </summary>
        ''' <returns>dataset con los datos mencionados</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    13/03/2006             [AngelL]          Creado.
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaVisorSucesosSeguridad() As dtsSucesosSeguridad
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' dtsRetorno            : dataset que se retornara
            ' objADSistema          : objeto de acceso adatos para el manejo de datos referentes al sistema
            ' objADUsuarios         : objeto de acceso adatos para el manejo de datos referentes al usuario
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsRetorno As New dtsSucesosSeguridad
            Dim objADSistema As AccesoDatos.Sistema = Nothing
            Dim objADUsuarios As AccesoDatos.UsuarioSistema = Nothing


            Try
                objADSistema = New AccesoDatos.Sistema
                objADUsuarios = New AccesoDatos.UsuarioSistema

                objADSistema.CrearConexion()
                objADUsuarios.ConexionActiva = objADSistema.ConexionActiva

                objADSistema.RecuperarCodigosAuditoria(dtsRetorno)
                objADUsuarios.RecuperarUsuariosConTareasCipol(dtsRetorno)
                objADUsuarios.RecuperarUsuariosConTareas(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName)

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = Nothing
            Finally
                objADSistema.Desconectar()
            End Try

            Return dtsRetorno
        End Function

        ''' <summary>
        ''' Recupera los datos necesarios para las sesiones el monitor de actividades
        ''' </summary>
        ''' <returns>dataset con los datos necesarios</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''  13/03/2006      [AngelL]            Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaMonitorActividades() As System.Data.DataSet
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' dtsRetorno            : dataset que se retornara
            ' objADSistema          : objeto de acceso adatos para el manejo de datos referentes al sistema
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistemas As AccesoDatos.Sistema = Nothing
            Dim dtsREtorno As New System.Data.DataSet

            Try
                objADSistemas = New AccesoDatos.Sistema
                objADSistemas.CrearConexion()
                objADSistemas.RetornarSesionesActivas(dtsREtorno, "Sist_sesionesactivas")

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsREtorno = Nothing
            Finally
                objADSistemas.Desconectar()
            End Try

            Return dtsREtorno
        End Function

        ''' <summary>
        ''' Recupera los datos necesarios para las sesiones el monitor de actividades
        ''' </summary>
        ''' <returns>1 en caso de éxito, 0 en caso de error.</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''  13/03/2006      [AngelL]            Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function EliminarSesionActiva(ByVal dtsCriterio As System.Data.DataSet) As Int32
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' intRetorno            : valorq eu se retornara
            ' objADSistema          : objeto de acceso adatos para el manejo de datos referentes al sistema
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistemas As AccesoDatos.Sistema = Nothing
            Dim intRetorno As Int32 = 0

            Try
                objADSistemas = New AccesoDatos.Sistema
                objADSistemas.IniciarTransaccion()

                For intI As Integer = 0 To dtsCriterio.Tables("Sist_sesionesactivas").Rows.Count - 1
                    With dtsCriterio.Tables("Sist_sesionesactivas").Rows(intI)
                        objADSistemas.EliminarSesionActiva(.Item("Usuario").ToString.Trim, .Item("Terminal").ToString.Trim)
                    End With
                Next

                intRetorno = 1
                objADSistemas.FinalizarTransaccion(True)
            Catch ex As Exception
                objADSistemas.FinalizarTransaccion(False)
                Me.PublicarExcepcion(ex)
                intRetorno = 0
            End Try

            Return intRetorno
        End Function

        ''' <summary>
        ''' retorna el log de auditoria en base a los parametros pasados
        ''' </summary>
        ''' <param name="fechadesde">fecha desde la que se pedira el log</param>
        ''' <param name="fechahasta">fecha hasta la que se pedira el log</param>
        ''' <param name="UsuarioActuante">nombre del usuario actuante </param>
        ''' <param name="usuarioafectado">nombre del usuario arfectado</param>
        ''' <param name="CodigoEvento">codigo de evento a listar</param>
        ''' <returns>dataset con los datos solicitados</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    13/03/2006     [AngelL]          Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RetornarLogAuditoria(ByVal fechadesde As DateTime, ByVal fechahasta As DateTime, ByVal UsuarioActuante As String, ByVal usuarioafectado As String, ByVal CodigoEvento As String) As dtsSucesosSeguridad
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' dtsRetorno            : dataset que se retornara
            ' objADSistema          : objeto de acceso adatos para el manejo de datos referentes al sistema
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsRetorno As New dtsSucesosSeguridad
            Dim objADSistema As AccesoDatos.Sistema = Nothing

            Try
                objADSistema = New AccesoDatos.Sistema()
                objADSistema.CrearConexion()
                objADSistema.RetornarLogAuditoria(fechadesde, fechahasta, UsuarioActuante, usuarioafectado, CodigoEvento, dtsRetorno, dtsRetorno.SE_AUDITORIA.TableName)

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = Nothing
            Finally
                objADSistema.Desconectar()
            End Try


            Return dtsRetorno
        End Function


        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Almacena el tipo de seguridad a utilizar
        ''' </summary>
        ''' <param name="TipoSeguridad">
        '''     Tipo de Seguridad (administrada por CIPOL o integrada al Dominio)
        '''     y el nombre de la organización que implementa el CIPOL
        ''' </param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[gustavom]	20/09/2005	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        <WebMethod(enableSession:=True)> _
        Public Function ActualizarTipoSeguridad(ByVal TipoSeguridad As String) As Boolean
            Dim objADSistema As AccesoDatos.Sistema = Nothing
            Dim blnRetorno As Boolean

            Try
                objADSistema = New AccesoDatos.Sistema()
                objADSistema.CrearConexion()
                objADSistema.ActualizarTipoSeguridad(TipoSeguridad)
                blnRetorno = True
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                blnRetorno = False
            Finally
                objADSistema.Desconectar()
            End Try

            Return blnRetorno
        End Function

#Region "ABM Sistemas Habilitados"

        ''' <summary>
        ''' Recupera los sistemas Habilitados
        ''' </summary>
        ''' <returns>DataSet con los sistemas habilitados en caso de exito,
        ''' Nothing en caso contrario</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [jueves, 24 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function RecuperarSistemasHabilitados() As dtsSistHabilitados
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADSist  : Componente logico de acceso a datos de sistema
            'dtsRet     : DataSet con los datos de las terminales
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSist As AccesoDatos.Sistema = Nothing
            Dim dtsRet As dtsSistHabilitados
            Const strSE_SIST_HABILITADOS As String = "SE_SIST_HABILITADOS"

            Try
                objADSist = New AccesoDatos.Sistema
                objADSist.CrearConexion()
                dtsRet = New dtsSistHabilitados

                objADSist.RetornarSistemasHabilitados(dtsRet, strSE_SIST_HABILITADOS)

                Return dtsRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSist IsNot Nothing Then objADSist.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Crea un sistema habilitado
        ''' </summary>
        ''' <param name="dtsDatosSistHab">DataSet con los datos del nuevo sistema habilitado</param>
        ''' <returns>Un valor positivo en caso de exito, negativo en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [viernes, 25 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function InsertarSistemaHabilitado(ByVal dtsDatosSistHab As dtsSistHabilitados) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADSist  : Componente logico de acceso a datos
            'intRet     : Valor de retorno del metodo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSist As AccesoDatos.Sistema = Nothing
            Dim intRet As Integer

            Try
                objADSist = New AccesoDatos.Sistema
                objADSist.IniciarTransaccion()

                For intI As Integer = 0 To dtsDatosSistHab.SE_SIST_HABILITADOS.Count - 1
                    intRet += objADSist.InsertarSistemaHabilitado(dtsDatosSistHab.SE_SIST_HABILITADOS(intI))
                Next

                objADSist.FinalizarTransaccion(True)

                Return intRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                If objADSist IsNot Nothing Then objADSist.FinalizarTransaccion(False)
                Return -1
            End Try

        End Function

        ''' <summary>
        ''' Actualiza los datos de un sistema habilitado
        ''' </summary>
        ''' <param name="dtsDatosSistHab">DataSet con los datos del nuevo sistema habilitado</param>
        ''' <returns>Un valor positivo en caso de exito, negativo en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [viernes, 25 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function ActualizarSistemaHabilitado(ByVal dtsDatosSistHab As dtsSistHabilitados) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADSist  : Componente logico de acceso a datos
            'intRet     : Valor de retorno del metodo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSist As AccesoDatos.Sistema = Nothing
            Dim intRet As Integer

            Try
                objADSist = New AccesoDatos.Sistema
                objADSist.IniciarTransaccion()

                For intI As Integer = 0 To dtsDatosSistHab.SE_SIST_HABILITADOS.Count - 1
                    intRet += objADSist.ActualizarSistemaHabilitado(dtsDatosSistHab.SE_SIST_HABILITADOS(intI))
                Next

                objADSist.FinalizarTransaccion(True)

                Return intRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                If objADSist IsNot Nothing Then objADSist.FinalizarTransaccion(False)
                Return -1
            End Try

        End Function

        ''' <summary>
        ''' Elimina un sistema habilitado
        ''' </summary>
        ''' <param name="dtsDatosSistHab">DataSet con los datos del sistema a eliminar</param>
        ''' <returns>Un valor positivo en caso de exito, negativo en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [viernes, 25 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' [AndresR]           [lunes, 07 de julio de 2008]         Se agrega verificación, si existe tarea primitiva
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function EliminarSistemaHabilitado(ByVal dtsDatosSistHab As dtsSistHabilitados) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADSist  : Componente logico de acceso a datos
            'intRet     : Valor de retorno del metodo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSist As AccesoDatos.Sistema = Nothing
            Dim intRet As Integer

            Try
                objADSist = New AccesoDatos.Sistema
                objADSist.IniciarTransaccion()

                For intI As Integer = 0 To dtsDatosSistHab.SE_SIST_HABILITADOS.Count - 1

                    'Se verifica si existe una tarea primitiva
                    If objADSist.VerificarExistenciaTareaPrimitiva(dtsDatosSistHab.SE_SIST_HABILITADOS(intI).IDSISTEMA) Then
                        objADSist.FinalizarTransaccion(False)
                        Return -1
                    End If

                    intRet += objADSist.EliminarSistemaHabilitado(dtsDatosSistHab.SE_SIST_HABILITADOS(intI).IDSISTEMA)
                Next

                objADSist.FinalizarTransaccion(True)

                Return intRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                If objADSist IsNot Nothing Then objADSist.FinalizarTransaccion(False)
                Return -1
            End Try
        End Function

#End Region

#Region "ANALISIS DE AUDITORIA"

        ''' <summary>
        ''' Recupera los Sistemas Habilitados
        ''' </summary>
        ''' <returns>DataSet con los datos recuperados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarSIST_Habilitados() As dtsAuditoriaEventos
            Dim objADSistema As New AccesoDatos.Auditoria
            Dim dtsEventos As dtsAuditoriaEventos
            Try
                objADSistema.CrearConexion()
                dtsEventos = objADSistema.RecuperarSistemasHabilitados()
                Return dtsEventos
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Recupera los Usuarios del Sistemas 
        ''' </summary>
        ''' <returns>DataSet con los datos recuperados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarUsuarios() As dtsAuditoriaEventos
            Dim objADSistema As New AccesoDatos.Auditoria
            Dim dtsEventos As dtsAuditoriaEventos
            Try
                objADSistema.CrearConexion()
                dtsEventos = objADSistema.RecuperarUsuarios()
                Return dtsEventos
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Recupera las Tablas del Sistemas 
        ''' </summary>
        ''' <returns>DataSet con los datos recuperados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperaTablasDeSistemas() As dtsAuditoriaEventos
            Dim objADSistema As New AccesoDatos.Auditoria
            Dim dtsEventos As dtsAuditoriaEventos
            Try
                objADSistema.CrearConexion()
                dtsEventos = objADSistema.RecuperaTablasDeSistemas()
                Return dtsEventos
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Recupera las Terminales del Sistema 
        ''' </summary>
        ''' <returns>DataSet con los datos recuperados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperaTerminales() As dtsAuditoriaEventos
            Dim objADSistema As New AccesoDatos.Auditoria
            Dim dtsEventos As dtsAuditoriaEventos
            Try
                objADSistema.CrearConexion()
                dtsEventos = objADSistema.RecuperaTerminales()
                Return dtsEventos
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Recupera la Fecha minima
        ''' </summary>
        ''' <returns>Date con la 1º fecha ingresada</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarFechaMinima() As Date
            Dim objADSistema As New AccesoDatos.Auditoria
            Try
                objADSistema.CrearConexion()
                Return objADSistema.RecuperarFechaMinima()
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return New Date(1900, 1, 1, 0, 0, 0)
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Pueba una sentencia SQL 
        ''' </summary>
        ''' <param name="NewStringSQL">String a probar la sintaxis</param>
        ''' <returns>True si es correcta</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [lunes, 05 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function ProbarStringSQL(ByVal NewStringSQL As String) As Boolean
            Dim objADSistema As New AccesoDatos.Auditoria
            Try
                objADSistema.CrearConexion()
                Return objADSistema.ProbarStringSQL(NewStringSQL)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Recupera los datos de la tabla SIST_EVENTOS
        ''' </summary>
        ''' <param name="dtsFiltros">DataSet con los filtros a aplicar</param>
        ''' <returns>dataset con los datos seleccionados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [lunes, 05 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarEventosAuditoria(ByVal dtsFiltros As dtsAuditoriaEventos) As dtsAuditoriaEventos
            Dim objADSistema As New AccesoDatos.Auditoria
            Dim dtsRet As dtsAuditoriaEventos
            Try
                objADSistema.CrearConexion()
                dtsRet = objADSistema.RecuperarEventos(dtsFiltros)
                Return dtsRet
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Retorna el String SQL a ejecutar
        ''' </summary>
        ''' <param name="dtsFiltros">DataSet con los filtros a aplicar</param>
        ''' <returns>dataset con los datos seleccionados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [lunes, 05 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RetornaStringSQL(ByVal dtsFiltros As dtsAuditoriaEventos) As String
            Dim objADSistema As New AccesoDatos.Auditoria
            Try
                objADSistema.CrearConexion()
                Return objADSistema.RecuperarStringSQL(dtsFiltros)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' Recupera los Usuarios del Sistemas 
        ''' </summary>
        ''' <returns>DataSet con los datos recuperados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarSupervisores() As dtsAuditoriaEventos
            Dim objADSistema As New AccesoDatos.Auditoria
            Dim dtsEventos As dtsAuditoriaEventos
            Try
                objADSistema.CrearConexion()
                dtsEventos = objADSistema.RecuperarSupervisores()
                Return dtsEventos
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistema IsNot Nothing Then objADSistema.Desconectar()
            End Try
        End Function

        <WebMethod()> _
        Public Function AuditarIntentoInicioSesionConSesionActiva(ByVal Login As String) As String
            Dim fcP As Fachada.PadreFachada = Nothing
            Dim strMensaje As String = ""

            Try
                fcP = New Fachada.PadreFachada

                strMensaje = fcP.AuditarIntentoInicioSesionConSesionActiva(Login)

                Return strMensaje

            Catch ex As Exception
                PublicarExcepcion(ex)
                Throw
            End Try
        End Function

#End Region

    End Class
End Namespace