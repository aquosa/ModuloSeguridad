Imports EE = Fachada.Seguridad

Public Class Grupo
    Inherits PadreSistema

    ''' <summary>
    ''' Permite ingresar o modificar un grupo de tareas
    ''' </summary>
    ''' <param name="dtsDatos">DataSet que contiene los datos del grupo</param>
    ''' <returns>0 si no se pudo ingresar o actualizar el grupo. En un ingreso 
    ''' un valor distinto de cero se interpreta como el identificador del grupo
    ''' </returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' Gustavo Mazzaglia - 02/03/2006 
    ''' [AndresR]          [viernes, 11 de julio de 2008]       GCP-Cambios ID: 7111
    ''' </history>
    Public Function AdministrarGrupo(ByVal dtsDatos As EE.dtsGrupos) As Short
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'objADGrupo     : Componente lógico de acceso a datos que se utiliza para 
        '                 ingresar o actualizar el grupo
        'objADSistema   : Componente lógico de acceso a datos que se utiliza para
        '                 auditar el cambio realizado en el grupo
        'shtRet         : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADGrupo As AccesoDatos.Grupo = Nothing, objADSistema As AccesoDatos.Sistema = Nothing
        Dim shtRet As Short = 0

        Try
            objADGrupo = New AccesoDatos.Grupo
            objADGrupo.IniciarTransaccion()
            objADSistema = New AccesoDatos.Sistema
            objADSistema.ConexionActiva = objADGrupo.ConexionActiva
            'Si se trata de un nuevo grupo
            If dtsDatos.SE_GRUPO_TAREA(0).IDGRUPO = -1 Then
                shtRet = objADGrupo.IngresarGrupo(dtsDatos.SE_GRUPO_TAREA(0).DESCGRUPO)
                With dtsDatos.SE_GRUPO_TAREA(0)
                    .BeginEdit()
                    .IDGRUPO = shtRet
                    .EndEdit()
                End With
            Else
                shtRet = dtsDatos.SE_GRUPO_TAREA(0).IDGRUPO
                objADGrupo.ActualizarDescripcion(dtsDatos.SE_GRUPO_TAREA(0).IDGRUPO, dtsDatos.SE_GRUPO_TAREA(0).DESCGRUPO)
                objADGrupo.EliminarTareasDelGrupo(dtsDatos.SE_GRUPO_TAREA(0).IDGRUPO)
                objADGrupo.EliminarGruposExcluyentes(dtsDatos.SE_GRUPO_TAREA(0).IDGRUPO)
            End If
            objADGrupo.IngresarTareasAlGrupo(shtRet, dtsDatos)
            If dtsDatos.GRUPO_EXCLUYENTE.Rows.Count > 0 Then objADGrupo.IngresarGruposExcluyentes(shtRet, dtsDatos)
            Me.AuditarCambios(objADSistema, dtsDatos.Auditoria(0).SQLAuditar)

            objADGrupo.FinalizarTransaccion(True)

            Return shtRet
        Catch ex As Exception
            objADGrupo.FinalizarTransaccion(False)
            Throw
        End Try

    End Function

    ''' <summary>
    ''' Permite eliminar un determinado grupo de tareas
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/22006 </history>
    Public Function EliminarGrupo(ByVal IDGrupo As Short) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'objADGrupo : Componente lógico de acceso a datos que se utiliza para 
        '             eliminar el grupo
        'blnRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADGrupo As AccesoDatos.Grupo = Nothing
        Dim blnRet As Boolean = False

        Try
            objADGrupo = New AccesoDatos.Grupo
            objADGrupo.IniciarTransaccion()
            objADGrupo.EliminarTareasDelGrupo(IDGrupo)
            objADGrupo.EliminarGruposExcluyentes(IDGrupo)
            blnRet = objADGrupo.EliminarGrupo(IDGrupo) > 0
            objADGrupo.FinalizarTransaccion(True)

            Return blnRet
        Catch ex As Exception
            objADGrupo.FinalizarTransaccion(False)
            Throw
        End Try


    End Function



End Class
