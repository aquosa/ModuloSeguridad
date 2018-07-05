Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Namespace Seguridad
    Partial Public Class wsSeguridad

#Region "Grupos de Tareas"
        ''' <summary>
        ''' Verifica si la tarea autorizante está en uso en un ROL
        ''' </summary>
        ''' <param name="pidTarea">ID de tarea a verificar</param>
        ''' <returns></returns>
        ''' 0 No está en uso
        ''' 1 Está usada en algún ROL
        ''' 2 Está usada en Tareas asignadas a usuarios
        ''' 3 Está usada en Tareas y en Roles
        ''' <remarks></remarks>
        ''' <history>
        ''' [JorgeI]  [miércoles, 22 de septiembre de 2010] Creado GCP-Cambios ID:9374
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function VerificaTareasAutorizantesEnUso(ByVal pidTarea As Int32) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE LAS VARIABLES LOCALES
            ' objRNTareas               : componente para el manejo de tareas
            ' intRetorno                : valor que se retornará
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNTareas As ReglasNegocio.Tarea = Nothing
            Dim intRetorno As Int32 = 0

            Try
                objRNTareas = New ReglasNegocio.Tarea
                intRetorno = objRNTareas.VerificaTareasAutorizantesEnUso(pidTarea)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                intRetorno = -1
            End Try

            Return intRetorno
        End Function

        ''' <summary>
        ''' Elimina una relacion de tareas autorizantes
        ''' </summary>
        ''' <param name="pidTarea">identificador de la tarea a eliminar</param>
        ''' <returns>cantidad de registros eliminados, 0 en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    13/03/2006            [AngelL]        Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function EliminarTareasAutorizantes(ByVal pidTarea As Int32) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE LAS VARIABLES LOCALES
            ' objRNTareas               : componente para el manejo de tareas
            ' intRetorno                : valor que se retornará
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNTareas As ReglasNegocio.Tarea = Nothing
            Dim intRetorno As Int32 = 0

            Try
                objRNTareas = New ReglasNegocio.Tarea
                intRetorno = objRNTareas.EliminarTareasAutorizantes(pidTarea)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                intRetorno = 0
            End Try

            Return intRetorno
        End Function

        ''' <summary>
        ''' Recupera los sistemas, grupos existentes, las tareas del grupo raíz
        ''' del primer sistema, los grupos excluyentes del primer grupo, 
        ''' los grupos no excluyentes del primer grupo y las tareas primitivas 
        ''' del primer grupo
        ''' </summary>
        ''' <returns>DataSet de retorno de datos</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 01/03/2005 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaABMGrupos() As dtsGrupos
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADSistema   : Componente lógico de acceso a datos que se utiliza para 
            '                 recuperar los sistemas 
            'objADGrupo     : Componente lógico de acceso a datos que se utiliza para
            '                 recuperar los grupos creados y las tareas primitivas del
            '                 primer sistema
            'dtsRet         : DataSet de retorno de datos
            'objADTareas    : Recupera las tareas primitivas que pertenecen al primer 
            '                 sistema
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As AccesoDatos.Sistema = Nothing
            Dim objADGrupo As AccesoDatos.Grupo = Nothing
            Dim dtsRet As New dtsGrupos
            Dim objADTareas As AccesoDatos.Tarea = Nothing

            Const GrupoRaiz As Short = 0

            Try
                objADSistema = New AccesoDatos.Sistema
                objADGrupo = New AccesoDatos.Grupo
                objADTareas = New AccesoDatos.Tarea
                objADSistema.CrearConexion()
                'Sistemas habilitados
                objADSistema.RecuperarSistemasHabilitados(CType(dtsRet, System.Data.DataSet))
                objADGrupo.ConexionActiva = objADSistema.ConexionActiva
                objADTareas.ConexionActiva = objADSistema.ConexionActiva
                'Grupos actuales
                objADGrupo.RecuperarGrupos(dtsRet)
                'Tareas que se encuentran en el grupo raíz, para el primer sistema
                objADTareas.RecuperarTareas(CType(dtsRet, System.Data.DataSet), dtsRet.SE_SIST_HABILITADOS(0).IDSISTEMA, GrupoRaiz)
                'Recupera las tareas del grupo
                If dtsRet.SE_GRUPO_TAREA.Rows.Count > 0 Then
                    objADGrupo.RecuperarTareas(dtsRet.SE_GRUPO_TAREA(0).IDGRUPO, dtsRet)
                    objADGrupo.RecuperarGruposExcluyentes(dtsRet.SE_GRUPO_TAREA(0).IDGRUPO, dtsRet)
                    objADGrupo.RecuperarGruposNoExcluyentes(dtsRet.SE_GRUPO_TAREA(0).IDGRUPO, dtsRet)
                End If

                Return dtsRet
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADSistema.Desconectar()
            End Try


        End Function

        ''' <summary>
        ''' Recupera las tareas y los grupos excluyentes y no excluyentes del
        ''' grupo que se recibe por parámetro
        ''' </summary>
        ''' <param name="IDGrupo">Identificador del grupo</param>
        ''' <returns>DataSet de retorno</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosDelGrupo(ByVal IDGrupo As Short) As dtsGrupos
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADGrupo : Componente lógico de acceso a datos que se utiliza para
            '             recuperar los datos del grupo
            'dtsRet     : DataSet de retorno
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADGrupo As AccesoDatos.Grupo = Nothing
            Dim dtsRet As New dtsGrupos

            Try
                objADGrupo = New AccesoDatos.Grupo
                objADGrupo.CrearConexion()
                objADGrupo.RecuperarTareas(IDGrupo, dtsRet)
                objADGrupo.RecuperarGruposExcluyentes(IDGrupo, dtsRet)
                objADGrupo.RecuperarGruposNoExcluyentes(IDGrupo, dtsRet)

                Return dtsRet
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADGrupo.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Recupera las tareas primitivas que pertenecen a un determinado grupo 
        ''' y sistema
        ''' </summary>
        ''' <param name="IDSistema">Identificador del sistema</param>
        ''' <param name="IDGrupo">Identificador del grupo</param>
        ''' <returns>DataSet de retorno</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 01/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarTareasPorSistemaYGrupo(ByVal IDSistema As Short, ByVal IDGrupo As Short) As dtsGrupos
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADTareas: Componente lógico de acceso a datos que se utiliza para 
            '             recuperar las tareas 
            'dtsRet     : DataSet de retorno
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTareas As AccesoDatos.Tarea = Nothing
            Dim dtsRet As dtsGrupos = Nothing

            Try
                objADTareas = New AccesoDatos.Tarea
                objADTareas.CrearConexion()
                dtsRet = New dtsGrupos
                objADTareas.RecuperarTareas(CType(dtsRet, System.Data.DataSet), IDSistema, IDGrupo)
                Return dtsRet
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADTareas.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Permite ingresar o modificar un grupo de tareas
        ''' </summary>
        ''' <param name="dtsDatos">DataSet que contiene los datos del grupo</param>
        ''' <returns>0 si no se pudo ingresar o actualizar el grupo. En un ingreso 
        ''' un valor distinto de cero se interpreta como el identificador del grupo
        ''' </returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function AdministrarGrupo(ByVal dtsDatos As dtsGrupos) As Short
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objGrupo   : Objeto que permite el ingreso o actualización del grupo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objGrupo As ReglasNegocio.Grupo = Nothing

            If dtsDatos Is Nothing Then Throw New ArgumentException("El DataSet no puede ser vacío")
            If dtsDatos.SE_GRUPO_TAREA.Rows.Count = 0 OrElse dtsDatos.SE_TAREAS_ASIGNADAS.Rows.Count = 0 Then Throw New ArgumentException("El DataSet no puede ser vacío")
            If dtsDatos.Auditoria.Rows.Count = 0 Then Throw New ArgumentException("El cambio o ingreso del grupo debe ser auditado")


            Try
                objGrupo = New ReglasNegocio.Grupo
                Return objGrupo.AdministrarGrupo(dtsDatos)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return 0
            End Try

        End Function

        ''' <summary>
        ''' Permite eliminar un determinado grupo de tareas
        ''' </summary>
        ''' <param name="IDGrupo">Identificador del grupo</param>
        ''' <returns>True o False</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 02/03/22006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function EliminarGrupo(ByVal IDGrupo As Short) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objGrupo   : Componente que se utiliza para eliminar el grupo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objGrupo As ReglasNegocio.Grupo = Nothing

            Try
                objGrupo = New ReglasNegocio.Grupo
                Return objGrupo.EliminarGrupo(IDGrupo)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function

#End Region

#Region "Tareas Autorizantes"
        ''' <summary>
        ''' Recupera las tareas autorizantes, los sistemas habilitados
        ''' y las tareas primitivas del primer sistema
        ''' </summary>
        ''' <returns>DataSet de retorno de datos</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 05/03/2005</history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaABMTareasAutorizantes() As dtsTareas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE VARIABLES LOCALES
            'objADSistema   : Componente lógico de acceso a datos que se 
            '                 utiliza para recuperar los sistemas
            'dtsRet         : DataSet de retorno de datos
            'objADTareas    : Componente lógico de acceso a datos que se
            '                 utiliza para recuperar las tareas primitivas
            '                 de un determinado sistema
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistema As AccesoDatos.Sistema = Nothing
            Dim dtsRet As New dtsTareas
            Dim objADTareas As AccesoDatos.Tarea = Nothing

            Try
                objADSistema = New AccesoDatos.Sistema
                objADSistema.CrearConexion()
                objADSistema.RecuperarSistemasHabilitados(dtsRet)
                objADTareas = New AccesoDatos.Tarea
                objADTareas.ConexionActiva = objADSistema.ConexionActiva
                'Recupera las tareas primitivas del sistema
                If dtsRet.SE_SIST_HABILITADOS.Rows.Count > 0 Then
                    objADTareas.RecuperarTareasSinAutorizacion(dtsRet.SE_SIST_HABILITADOS(0).IDSISTEMA, dtsRet) 'dtsRet.SE_TAREAS.Merge(objADTareas.RecuperarTareasSinAutorizacion(dtsRet.SE_SIST_HABILITADOS(0).IDSISTEMA))
                End If

                'Recupera las tareas autorizantes
                objADTareas.RecuperarTareasAutorizantes(dtsRet)

                Return dtsRet
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADSistema.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Recupera las tareas primitivas de un sistema que no tienen asociadas una tarea autorizante
        ''' </summary>
        ''' <param name="IDSistema">Identificador del sistema</param>
        ''' <returns>DataSet de retorno de datos</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 06/03/2005 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarTareasSinAutorizacion(ByVal IDSistema As Short) As dtsTareas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADTareas    : Componente lógico de acceso a datos que se utiliza para
            '                 recuperar las tareas primitivas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTareas As AccesoDatos.Tarea = Nothing
            Dim dtsRetorno As New dtsTareas

            Try
                objADTareas = New AccesoDatos.Tarea
                objADTareas.CrearConexion()
                objADTareas.RecuperarTareasSinAutorizacion(IDSistema, dtsRetorno)

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = New dtsTareas
            Finally
                objADTareas.Desconectar()
            End Try

            Return dtsRetorno

        End Function

        ''' <summary>
        ''' Recupera las tareas primitivas que requieren autorización
        ''' </summary>
        ''' <param name="IDTareaAutorizante">Identificador de la tarea autorizante</param>
        ''' <returns>DataSet de retorno de datos</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarTareasQueRequierenAutorizacion(ByVal IDTareaAutorizante As Integer) As dtsTareas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADTareas    : Componente lógico de acceso a datos que se utiliza para
            '                 recuperar las tareas que requieren autorización
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTareas As AccesoDatos.Tarea = Nothing
            Dim dtsRetorno As New dtsTareas

            Try
                objADTareas = New AccesoDatos.Tarea
                objADTareas.CrearConexion()
                objADTareas.RecuperarTareasQueRequierenAutorizacion(IDTareaAutorizante, dtsRetorno)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = Nothing
            Finally
                objADTareas.Desconectar()
            End Try

            Return dtsRetorno

        End Function

        ''' <summary>
        ''' Permite ingresar o actualizar una tarea autorizante
        ''' </summary>
        ''' <param name="dtsDatos">DataSet que contiene los datos para ingresar o actualizar la tarea</param>
        ''' <returns>Retorna cero si no se ha podido ingresar o actualizar la tarea. En el caso de alta 
        ''' el valor de retorno representa el identificador de la tarea
        ''' </returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function AdministrarTareasSupervisantes(ByVal dtsDatos As dtsTareas) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objTareas  : Objeto de reglas de negocio
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objTareas As ReglasNegocio.Tarea = Nothing

            If dtsDatos Is Nothing Then Throw New ArgumentException("El DataSet no puede estar vacío")
            If dtsDatos.Tables("TAREAS").Rows.Count = 0 Or dtsDatos.Tables("SE_TAREASAUTORIZADAS").Rows.Count = 0 Then Throw New ArgumentException("El DataSet no posee los requisitos necesarios para ingreso o actualización de datos")

            Try
                objTareas = New ReglasNegocio.Tarea
                Return objTareas.AdministrarTareasSupervisantes(dtsDatos)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function

#End Region

#Region "Roles"


        <WebMethod(enableSession:=True)> _
        Public Function EliminarRol(ByVal pidRol As Int32) As Int32
            Dim objRNRoles As New ReglasNegocio.Rol

            Try
                Return objRNRoles.EliminarRol(pidRol)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return 0
            End Try


        End Function

        ''' <summary>
        ''' Recupera los datos con los grupos de tareas
        ''' </summary>
        ''' <returns>dataset con los grupos de tareas y sus tareas</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    08/03/2006    [AngelL]        Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaArbolDeGruposDeTareas() As dtsRoles
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADRoels            : objeto de acceso adatos para el manejo de roels
            ' objRetorno            : dataset a retornar
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADRoles As New AccesoDatos.Rol
            Dim objRetorno As New dtsRoles

            Try
                objADRoles.CrearConexion()
                objADRoles.RecuperarDatosParaArbolGrupo(objRetorno, objRetorno.ArbolGrupo.TableName)

            Catch ex As Exception
                objRetorno = Nothing
                Me.PublicarExcepcion(ex)
            Finally
                objADRoles.Desconectar()
            End Try
            Return objRetorno
        End Function

        <WebMethod(enableSession:=True)> _
        Public Function VerificarRolAsignadoAUsuarios(ByVal pidRol As Int32) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADRoels            : objeto de acceso adatos para el manejo de roels
            ' objRetorno            : dataset a retornar
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADRoles As New AccesoDatos.Rol
            Dim intRta As Integer = 0

            Try
                objADRoles.CrearConexion()
                intRta = objADRoles.VerificarRolAsignadoAUsuarios(pidRol)

            Catch ex As Exception
                intRta = -1
                Me.PublicarExcepcion(ex)
            Finally
                objADRoles.Desconectar()
            End Try
            Return intRta
        End Function


        ''' <summary>
        ''' Obtine los datos necesarios para el inicio del abm de roles
        ''' </summary>
        ''' <returns>dataset con los datos necesarios</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    08/03/2006     [AngelL]     Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaABMRoles() As dtsRoles
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objADRoels            : objeto de acceso adatos para el manejo de roels
            ' objTareas             : objeto de acceso a datos para el manejo de tareas
            ' dtsDatos              : dataset a retornar
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objTareas As New AccesoDatos.Tarea
            Dim dtsDatos As New dtsRoles
            Dim objADRoles As New AccesoDatos.Rol
            Dim objADGrupos As New AccesoDatos.Grupo
            Try
                objTareas.CrearConexion()
                objADRoles.ConexionActiva = objTareas.ConexionActiva
                objADGrupos.ConexionActiva = objTareas.ConexionActiva

                objADRoles.RecuperarRoles(dtsDatos, dtsDatos.SE_ROLES.TableName)
                'recuperamos la composicion del primer rol traido en la tabla de roles
                'siempre que esta tenga roles
                If Not dtsDatos.SE_ROLES.Rows.Count.Equals(0) Then
                    objADRoles.RecuperarComposicionDeUnRol(dtsDatos.SE_ROLES(0).IDROL, dtsDatos, dtsDatos.Roles_Composicion.TableName, True)
                    objADRoles.UsuariosPorRol(dtsDatos.SE_ROLES(0).IDROL, CType(dtsDatos, System.Data.DataSet), "tblUsuariosXRoles")
                End If

                objADGrupos.RecuperarIDGruposExcluyentes(dtsDatos, dtsDatos.SE_GRUPO_EXCLUSION.TableName)
                objADRoles.RecuperarDatosParaArbolGrupo(dtsDatos, dtsDatos.ArbolGrupo.TableName)


            Catch ex As Exception
                dtsDatos = Nothing
                Me.PublicarExcepcion(ex)
            Finally
                objTareas.Desconectar()
            End Try

            Return dtsDatos
        End Function

        ''' <summary>
        ''' recupera la composicion de un rol en base a un identificador
        ''' de rol pasado por parametro
        ''' </summary>
        ''' <param name="pIdRol">identificador del rol</param>
        ''' <param name="SoloIdTareas">indica si solo se recupera el identificador
        ''' de la tarea o tambien los descriptores</param>
        ''' <returns>datset con los datos cargados</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    [AngelL]          07/03/2006   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarComposicionRol(ByVal pIdRol As Int32, ByVal SoloIdTareas As Boolean) As dtsRoles
            Dim objADRoles As New AccesoDatos.Rol
            Dim dtsDatos As New dtsRoles
            Try
                objADRoles.CrearConexion()

                objADRoles.RecuperarComposicionDeUnRol(pIdRol, dtsDatos, dtsDatos.Roles_Composicion.TableName, SoloIdTareas)
                objADRoles.UsuariosPorRol(pIdRol, CType(dtsDatos, System.Data.DataSet), "tblUsuariosXRoles")

            Catch ex As Exception
                dtsDatos = Nothing
                Me.PublicarExcepcion(ex)
            Finally
                objADRoles.Desconectar()

            End Try

            Return dtsDatos
        End Function

        ''' <summary>
        ''' administra las modificaciones o altas de un rol
        ''' </summary>
        ''' <param name="pdtsDatos">dataset con los datos a tratar</param>
        ''' <returns>Identificador de elemento dado de alta o modificado 
        ''' o 0 (cero) en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        '''    07/03/2006     [AngelL]    Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function AdministrarRoles(ByRef pdtsDatos As dtsRoles, ByVal pStrElementosEliminados As String) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DESCRIPCION DE LAS VARIABLES LOCALES
            ' objRNRoels            : componente logico de recglas de negocio para roles
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNRoles As New ReglasNegocio.Rol

            Try
                Return objRNRoles.AdministrarRoles(pdtsDatos, pStrElementosEliminados)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return 0
            End Try
        End Function
#End Region

#Region "Tareas"

        ''' <summary>
        ''' Recupera las tareas primitivas del sistema
        ''' </summary>
        ''' <returns>Dataset con los datos de las tareas</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [martes, 29 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function RecuperarTareasPrimitivas() As dtsTareas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADTareas    : Componente logico de acceso a datos de tareas
            'dtsRet         : DataSet con los datos de las tareas
            'objADTareas    : Componente logico de acceso a datos de sistemas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTareas As AccesoDatos.Tarea = Nothing
            Dim objADSist As AccesoDatos.Sistema = Nothing
            Dim dtsRet As dtsTareas

            Try
                objADTareas = New AccesoDatos.Tarea
                objADSist = New AccesoDatos.Sistema
                dtsRet = New dtsTareas
                objADTareas.CrearConexion()
                objADSist.ConexionActiva = objADTareas.ConexionActiva

                'Recupera las tareas primitivas
                objADTareas.RecuperarTareasPrimitivas(dtsRet)

                'Recupera los sistemas habilitados
                objADSist.RecuperarSistemasHabilitados(dtsRet)

                'Carga el Id de sistema mas el Codigo
                For intI As Integer = 0 To dtsRet.SE_SIST_HABILITADOS.Count - 1
                    With dtsRet.SE_SIST_HABILITADOS(intI)
                        .BeginEdit()
                        .IDCODSISTEMA = .IDSISTEMA.ToString & ". " & .CODSISTEMA
                        .EndEdit()
                    End With
                Next

                Return dtsRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADTareas IsNot Nothing Then objADTareas.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Crea una nueva tarea primitiva
        ''' </summary>
        ''' <returns>Valor positivo en caso de exito, un valor negativo en caso de error,
        ''' 0 (Cero) si no genero ninguna tarea</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [martes, 29 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function InsertarTareaPrimitiva(ByVal dtsTareas As dtsTareas) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADTareas : Componente logico de acceso a datos de tareas
            'intRet      : Valor de retorno del metodo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTareas As AccesoDatos.Tarea = Nothing
            Dim intRet As Integer = 0

            Try
                objADTareas = New AccesoDatos.Tarea
                objADTareas.IniciarTransaccion()

                For intI As Integer = 0 To dtsTareas.TAREAS.Count - 1
                    intRet += objADTareas.InsertarTareaPrimitiva(dtsTareas.TAREAS(intI))
                Next

                objADTareas.FinalizarTransaccion(True)

                Return intRet

            Catch ex As Exception
                If objADTareas IsNot Nothing Then objADTareas.FinalizarTransaccion(False)
                Me.PublicarExcepcion(ex)
                Return -1
            End Try

        End Function

        ''' <summary>
        ''' Actualizar los datos de una tarea primitiva
        ''' </summary>
        ''' <returns>Valor positivo en caso de exito, un valor negativo en caso de error,
        ''' 0 (Cero) si no genero ninguna tarea</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [miércoles, 30 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function ActualizarTareaPrimitiva(ByVal dtsDatosTareas As dtsTareas) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'intRet : Valor de retorno del metodo
            'objADTareas : Componente logico de acceso a datos de Tareas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim intRet As Integer = 0
            Dim objADTareas As AccesoDatos.Tarea = Nothing

            Try

                objADTareas = New AccesoDatos.Tarea
                objADTareas.IniciarTransaccion()

                For intI As Integer = 0 To dtsDatosTareas.TAREAS.Count - 1
                    intRet += objADTareas.ActualizarTareaPrimitiva(dtsDatosTareas.TAREAS(intI))
                Next

                objADTareas.FinalizarTransaccion(True)

                Return intRet

            Catch ex As Exception
                If objADTareas IsNot Nothing Then objADTareas.FinalizarTransaccion(False)
                Me.PublicarExcepcion(ex)
                Return -1
            End Try

        End Function

        ''' <summary>
        ''' Elimina una tarea primitiva
        ''' </summary>
        ''' <returns>Valor positivo en caso de exito, un valor negativo en caso de error,
        ''' 0 (Cero) si no genero ninguna tarea</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [miércoles, 30 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' [AndresR]           [lunes, 07 de julio de 2008]           Se agregan Verificaciones, de roles y usuarios asociados
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function EliminarTareaPrimitiva(ByVal dtsTareas As dtsTareas) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADTarea : Componente logico de acceso a datos de Tareas
            'intRet     : Valor de retorno del metodo
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTarea As AccesoDatos.Tarea = Nothing
            Dim intRet As Integer

            Try

                objADTarea = New AccesoDatos.Tarea
                objADTarea.IniciarTransaccion()

                For intI As Integer = 0 To dtsTareas.TAREAS.Count - 1

                    'Se realiza verificación de rol asociado antes de eliminar
                    If objADTarea.VerificarExistenciaRoles(dtsTareas.TAREAS(intI).IDTAREA) Then
                        objADTarea.FinalizarTransaccion(False)
                        Return -1
                    End If

                    'Se realiza verificación de usuario asociado antes de eliminar
                    If objADTarea.VerificarExistenciaUsuario(dtsTareas.TAREAS(intI).IDTAREA) Then
                        objADTarea.FinalizarTransaccion(False)
                        Return -2
                    End If

                    intRet += objADTarea.EliminarTareaPrimitiva(dtsTareas.TAREAS(intI).IDTAREA)
                Next

                objADTarea.FinalizarTransaccion(True)

                Return intRet

            Catch ex As Exception
                If objADTarea IsNot Nothing Then objADTarea.FinalizarTransaccion(False)
                Me.PublicarExcepcion(ex)
                Return -1
            End Try

        End Function

        ''' <summary>
        ''' Verifica la existencia de una tarea por su ID
        ''' </summary>
        ''' <param name="IDTarea">ID de la tarea a verificar</param>
        ''' <returns>0 (Cero) si no existe, un valor positivo si existe, 
        ''' un valor negativo en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [martes, 29 de enero de 2008]       Creado GCP-Cambios ID: 6409
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function VerificarExistenciaIDTarea(ByVal IDTarea As Integer) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objADTarea : Componente logico de acceso a datos
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTarea As AccesoDatos.Tarea = Nothing

            Try

                objADTarea = New AccesoDatos.Tarea
                objADTarea.CrearConexion()

                Return objADTarea.ExisteTarea(IDTarea)

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return -1
            Finally
                If objADTarea IsNot Nothing Then objADTarea.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Recupera los sistemas y usuarios inhabilitados
        ''' </summary>
        ''' <param name="IDSistema">Identificador del Sistema</param>
        ''' <param name="IDUsuario">Identificador del Usuario</param>
        ''' <returns>DataSet de retorno</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [martes, 06 de mayo de 2008]       Creado GCP-Cambios ID: 5814
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarSistBloqueados(ByVal IDSistema As Short, ByVal IDUsuario As Short) As dtsSistBloqueados
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADTareas: Componente lógico de acceso a datos que se utiliza para 
            '             recuperar los sistemas inhabilitados
            'dtsRet     : DataSet de retorno
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADSistemas As AccesoDatos.Sistema = Nothing
            Dim objADUsuarios As AccesoDatos.UsuarioSistema = Nothing
            Dim dtsRet As dtsSistBloqueados = Nothing

            Try
                objADSistemas = New AccesoDatos.Sistema
                objADSistemas.CrearConexion()

                objADUsuarios = New AccesoDatos.UsuarioSistema
                objADUsuarios.ConexionActiva = objADSistemas.ConexionActiva

                dtsRet = New dtsSistBloqueados

                objADSistemas.RecuperarSistBloqueados(CType(dtsRet, System.Data.DataSet), IDSistema, IDUsuario)
                objADSistemas.RecuperarSistemasHabilitados(CType(dtsRet, System.Data.DataSet))
                objADUsuarios.RetornarUsuarios(CType(dtsRet, System.Data.DataSet), "SE_USUARIOS")

                Return dtsRet

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                If objADSistemas IsNot Nothing Then objADSistemas.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Graba los sistemas bloqueados y usuarios desbloqueados 
        ''' </summary>
        ''' <param name="dtsDatosSistBloq">DataSet con los datos</param>
        ''' <returns>Un valor positivo en caso de exito, negativo en caso de error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AndresR]          [viernes, 09 de mayo de 2008]       Creado GCP-Cambios ID: 5814
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function InsertarSistBloqueados(ByVal dtsDatosSistBloq As dtsSistBloqueados) As Integer
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

                'Se eliminan todos los sistemas bloqueados
                objADSist.EliminarSistBloqueado(-1, -1)

                'Se insertan los sistemas bloqueados
                For intI As Integer = 0 To dtsDatosSistBloq.SE_SIST_BLOQUEADOS.Count - 1
                    intRet += objADSist.InsertarSistBloqueado(dtsDatosSistBloq.SE_SIST_BLOQUEADOS(intI))
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

    End Class
End Namespace