Option Strict On
Option Explicit On

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Convert

Namespace Seguridad
    Partial Public Class wsSeguridad

        <WebMethod(enableSession:=True)> _
        Public Function AdministrarAbmUsuarios(ByVal pdtsDatos As dtsUsuariosABM, ByRef pstrError As String, ByVal MensajeAuditoria As String, ByVal CantidadMaximaHistorialContraseniaa As Int32) As Int32
            Try
                Dim objRNUsuarios As New ReglasNegocio.UsuarioSistema

                Return objRNUsuarios.AdministrarAbmUsuarios(pdtsDatos, pstrError, MensajeAuditoria, CantidadMaximaHistorialContraseniaa)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return -1
            End Try
        End Function

        ''' <summary>
        ''' Elimina un usuario
        ''' </summary>
        ''' <param name="pidUsuario">identificador del usuario</param>
        ''' <param name="strMensajeAuditoria">mensaje a auditar</param>
        ''' <returns>fecha de baja del usuario</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    09/03/2006         [AngelL]      Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function EliminarUsuario(ByVal pidUsuario As Int32, ByVal strMensajeAuditoria As String) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objRNUsuarios     : logica de negocios para el manejo de usurios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNUsuarios As New ReglasNegocio.UsuarioSistema

            Try
                Return objRNUsuarios.EliminarUsuarios(pidUsuario, strMensajeAuditoria)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return 0
            End Try

        End Function

        ''' <summary>
        ''' Activa un usuario
        ''' </summary>
        ''' <param name="pidUsuario">identificador del usuario</param>
        ''' <param name="strMensajeAuditoria">mensaje a auditar</param>
        ''' <returns>Cantidad de filas afectadas</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [miércoles, 18 de junio de 2008]       Creado GCP-Cambios ID: 6995
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function ActivarUsuario(ByVal pidUsuario As Int32, ByVal strMensajeAuditoria As String) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objRNUsuarios     : logica de negocios para el manejo de usurios
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNUsuarios As New ReglasNegocio.UsuarioSistema

            Try
                Return objRNUsuarios.ActivarUsuarios(pidUsuario, strMensajeAuditoria)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return 0
            End Try

        End Function

        ''' <summary>
        ''' recupera los datos necesarios pare realizar alguna accion de alta, baja o modificacion de un usuario
        ''' </summary>
        ''' <param name="pIdUsuario">identificador del usuario a recuperar</param>
        ''' <returns>datset con los datos necesarios para el usuario</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''   09/03/2006            [AngelL]       Creado
        '''   11/02/2007            [Gustavom]     GCP-Cambios ID: 6410
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaABMUsuarios(ByVal pIdUsuario As Int32) As dtsUsuariosABM
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            'objADUsuario  : logica de negocios para el manejo de usurios
            'strClave      : Clave actual del usuario 
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim objADRoles As New AccesoDatos.Rol
            Dim dtsRetorno As New dtsUsuariosABM
            Dim strClave As String

            'Levanto la tabla de usuarios
            Try
                objADUsuarios.CrearConexion()
                objADRoles.ConexionActiva = objADUsuarios.ConexionActiva
                objADUsuarios.RetornarUsuarios(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, pIdUsuario)
                strClave = objADUsuarios.RecuperarContraseniaActual(pIdUsuario)
                dtsRetorno.Sist_Usuarios(0).BeginEdit()
                dtsRetorno.Sist_Usuarios(0).IntegradoAlDominio = strClave.Equals(COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, ReglasNegocio.CIPOL.ContraseniaIntegrado, pIdUsuario.ToString))
                dtsRetorno.Sist_Usuarios(0).EndEdit()
                dtsRetorno.Sist_Usuarios(0).AcceptChanges()
                'Obtengo las tareas que posee el usuario
                objADRoles.RecuperarRolesComposicion(dtsRetorno, dtsRetorno.Roles_Composicion.TableName, pIdUsuario)
                'Obtengo las terminales prohibidas para el usuario
                objADUsuarios.RecuperarTerminalesProhibidasAUsuarios(dtsRetorno, dtsRetorno.SE_Term_Usuario.TableName, pIdUsuario)
                'Obtengo los horarios permitidos al usuario
                objADUsuarios.RecuperarHorariosUsuario(dtsRetorno, dtsRetorno.SE_Horarios_Usuario.TableName, pIdUsuario)
                'recuperamos las contraseñias en "SE_historial_usuario"
                objADUsuarios.RecuperarContrasenias(dtsRetorno, pIdUsuario)
                Return dtsRetorno
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADUsuarios.Desconectar()
            End Try
        End Function

        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaGrillaABMUsuarios() As dtsUsuarios
            Dim objADGrupos As New AccesoDatos.Grupo
            Dim objADSistema As New AccesoDatos.Sistema
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim objadTerminales As New AccesoDatos.Terminal
            Dim objADRoles As New AccesoDatos.Rol
            Dim dtsRetorno As New dtsUsuarios
            Try
                objADGrupos.CrearConexion()
                objADSistema.ConexionActiva = objADGrupos.ConexionActiva
                objADUsuarios.ConexionActiva = objADGrupos.ConexionActiva
                objadTerminales.ConexionActiva = objADGrupos.ConexionActiva
                objADRoles.ConexionActiva = objADGrupos.ConexionActiva
                'Cargo los grupos excluyentes que se utilizan en el formulario de asignación
                'de roles
                objADGrupos.RecuperarIDGruposExcluyentes(dtsRetorno, dtsRetorno.SE_GRUPO_EXCLUSION.TableName)
                'Levanto la tabla de usuarios
                objADUsuarios.RetornarUsuarios(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName)
                objADSistema.RetornarParametrosColumna4(dtsRetorno, "recParam")
                objADSistema.RecuperarTiposDocumentos(dtsRetorno, dtsRetorno.KDocumentos.TableName)
                'Cargo las areas
                'Angel Lubenov - Gcp Cambios ID:3304 - Se filtra por areasNO FICTICIAS
                'y no se filtra por areas < 100.
                objADSistema.RecuperarAreasNOFicticias(dtsRetorno, dtsRetorno.KAREAS.TableName)
                'recupero las terminales
                objadTerminales.RecuperarTerminales(dtsRetorno, True) 'GCP-Cambio ID:9287
                'obtengo la composicion de roles
                objADRoles.RecuperarRolesComposicion(dtsRetorno, "ComposicionDeRoles")
                'recupera los horarios predeterminados 
                objADUsuarios.RecuperarHorariosUsuario(dtsRetorno, dtsRetorno.SE_Horarios_Usuario.TableName)

                Return dtsRetorno
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADGrupos.Desconectar()
            End Try
        End Function

        ''[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo este nuevo metodo para no modificar el original RecuperarDatosParaGrillaABMUsuarios()
        '' Para poder recuperar los usuarios segun la pantalla de ABMUsuarios
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaGrillaABMUsuariosConParametros(ByVal pFiltro As String, _
                                     ByVal pblnArea As Boolean, _
                                     ByVal pblnNombre As Boolean, _
                                     ByVal pblnSubCadena As Boolean, _
                                     ByVal pblnUsu As Boolean, _
                                     ByVal pFiltroBaja As String) As dtsUsuarios
            Dim objADGrupos As New AccesoDatos.Grupo
            Dim objADSistema As New AccesoDatos.Sistema
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim objadTerminales As New AccesoDatos.Terminal
            Dim objADRoles As New AccesoDatos.Rol
            Dim dtsRetorno As New dtsUsuarios
            Try
                objADGrupos.CrearConexion()
                objADSistema.ConexionActiva = objADGrupos.ConexionActiva
                objADUsuarios.ConexionActiva = objADGrupos.ConexionActiva
                objadTerminales.ConexionActiva = objADGrupos.ConexionActiva
                objADRoles.ConexionActiva = objADGrupos.ConexionActiva
                'Cargo los grupos excluyentes que se utilizan en el formulario de asignación
                'de roles
                objADGrupos.RecuperarIDGruposExcluyentes(dtsRetorno, dtsRetorno.SE_GRUPO_EXCLUSION.TableName)
                'Levanto la tabla de usuarios
                objADUsuarios.RetornarUsuariosConParametros(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, pFiltro, pblnArea, pblnNombre, pblnSubCadena, pblnUsu, pFiltroBaja)
                objADSistema.RetornarParametrosColumna4(dtsRetorno, "recParam")
                objADSistema.RecuperarTiposDocumentos(dtsRetorno, dtsRetorno.KDocumentos.TableName)
                'Cargo las areas
                'Angel Lubenov - Gcp Cambios ID:3304 - Se filtra por areasNO FICTICIAS
                'y no se filtra por areas < 100.
                objADSistema.RecuperarAreasNOFicticias(dtsRetorno, dtsRetorno.KAREAS.TableName)
                'recupero las terminales
                objadTerminales.RecuperarTerminales(dtsRetorno, True) 'GCP-Cambio ID:9287
                'obtengo la composicion de roles
                objADRoles.RecuperarRolesComposicion(dtsRetorno, "ComposicionDeRoles")
                'recupera los horarios predeterminados 
                objADUsuarios.RecuperarHorariosUsuario(dtsRetorno, dtsRetorno.SE_Horarios_Usuario.TableName)

                Return dtsRetorno
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADGrupos.Desconectar()
            End Try
        End Function

        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaGrillaABMUsuariosFiltro(ByVal Filtro As String) As dtsUsuarios
            Dim objADGrupos As New AccesoDatos.Grupo
            Dim objADSistema As New AccesoDatos.Sistema
            Dim objADUsuarios As New AccesoDatos.UsuarioSistema
            Dim objadTerminales As New AccesoDatos.Terminal
            Dim objADRoles As New AccesoDatos.Rol
            Dim dtsRetorno As New dtsUsuarios
            Try
                objADGrupos.CrearConexion()
                objADSistema.ConexionActiva = objADGrupos.ConexionActiva
                objADUsuarios.ConexionActiva = objADGrupos.ConexionActiva
                objadTerminales.ConexionActiva = objADGrupos.ConexionActiva
                objADRoles.ConexionActiva = objADGrupos.ConexionActiva
                'Cargo los grupos excluyentes que se utilizan en el formulario de asignación
                'de roles
                'objADGrupos.RecuperarIDGruposExcluyentes(dtsRetorno, dtsRetorno.SE_GRUPO_EXCLUSION.TableName)
                'Levanto la tabla de usuarios
                objADUsuarios.RetornarUsuarios(dtsRetorno, dtsRetorno.Sist_Usuarios.TableName, , , , , , , Filtro)
                'objADSistema.RetornarParametrosColumna4(dtsRetorno, "recParam")
                'objADSistema.RecuperarTiposDocumentos(dtsRetorno, dtsRetorno.KDocumentos.TableName)
                'Cargo las areas
                'Angel Lubenov - Gcp Cambios ID:3304 - Se filtra por areasNO FICTICIAS
                'y no se filtra por areas < 100.
                'objADSistema.RecuperarAreasNOFicticias(dtsRetorno, dtsRetorno.KAREAS.TableName)
                'recupero las terminales
                'objadTerminales.RecuperarTerminales(dtsRetorno, True) 'GCP-Cambio ID:9287
                'obtengo la composicion de roles
                'objADRoles.RecuperarRolesComposicion(dtsRetorno, "ComposicionDeRoles")
                'recupera los horarios predeterminados 
                'objADUsuarios.RecuperarHorariosUsuario(dtsRetorno, dtsRetorno.SE_Horarios_Usuario.TableName)

                Return dtsRetorno
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADGrupos.Desconectar()
            End Try
        End Function
    End Class
End Namespace