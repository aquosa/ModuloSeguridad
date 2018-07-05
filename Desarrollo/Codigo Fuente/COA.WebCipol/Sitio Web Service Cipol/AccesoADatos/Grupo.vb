Imports EE = Fachada.Seguridad
Public Class Grupo
    Inherits PadreSistema
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera todos los grupos excluyentes
    ''' </summary>
    ''' <param name="dtsDatos">DataSet de retorno</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	29/09/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub RecuperarIDGruposExcluyentes(ByVal dtsDatos As System.Data.DataSet, ByVal strNombreTabla As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT IdGrupoActual, IdGrupExcluyente FROM SE_Grupo_Exclusion", dtsDatos, strNombreTabla)

    End Sub

    ''' <summary>
    ''' Recupera los grupos de tareas existentes
    ''' </summary>
    ''' <param name="dtsDatos">DataSet que contiene los grupos existentes</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 01/03/2006 </history>
    Public Sub RecuperarGrupos(ByRef dtsDatos As EE.dtsGrupos)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT IdGrupo, DescGrupo FROM SE_Grupo_Tarea WHERE IdGrupo > 0", CType(dtsDatos, System.Data.DataSet), dtsDatos.SE_GRUPO_TAREA.ToString)

    End Sub

    ''' <summary>
    ''' Recupera las tareas que pertenecen a un determinado grupo
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <param name="dtsDatos">DataSet de retorno de datos</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 01/03/2005 </history>
    Public Sub RecuperarTareas(ByVal IDGrupo As Short, ByRef dtsDatos As EE.dtsGrupos)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT IdTarea, DescripcionTarea FROM SE_Tareas WHERE IdGrupo = " & IDGrupo.ToString, CType(dtsDatos, System.Data.DataSet), dtsDatos.SE_TAREAS_ASIGNADAS.ToString)

    End Sub

    ''' <summary>
    ''' Recupera los grupos que son excluyentes del grupo que se recibe por parámetro
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <param name="dtsDatos">DataSet de retorno de datos</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Sub RecuperarGruposExcluyentes(ByVal IDGrupo As Short, ByRef dtsDatos As EE.dtsGrupos)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT IDGrupo, DescGrupo FROM SE_GRUPO_TAREA WHERE IDGrupo IN ( " & _
                            "SELECT IDGRUPEXCLUYENTE  FROM SE_Grupo_Exclusion WHERE IdGrupoActual = " & IDGrupo.ToString & ")", CType(dtsDatos, System.Data.DataSet), dtsDatos.GRUPO_EXCLUYENTE.ToString)

    End Sub

    ''' <summary>
    ''' Recupera los grupos que no son excluyentes del grupo que se recibe por parámetro
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <param name="dtsDatos">DataSet de retorno de datos</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Sub RecuperarGruposNoExcluyentes(ByVal IDGrupo As Short, ByRef dtsDatos As EE.dtsGrupos)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT IDGrupo, DescGrupo FROM SE_GRUPO_TAREA WHERE IDGrupo > 0 AND IDGrupo NOT IN ( " & _
                            "SELECT IDGRUPEXCLUYENTE  FROM SE_Grupo_Exclusion WHERE IdGrupoActual = " & IDGrupo.ToString & ")", CType(dtsDatos, System.Data.DataSet), dtsDatos.GRUPO_NO_EXCLUYENTE.ToString)

    End Sub

    ''' <summary>
    ''' Permite el ingreso de un nuevo grupo
    ''' </summary>
    ''' <param name="NombreGrupo">Nombre del grupo que se ingresa</param>
    ''' <returns>Identificador del grupo</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function IngresarGrupo(ByVal NombreGrupo As String) As Short
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        'objID      : Identificador del nuevo grupo
        'shtID      : Identificador del grupo
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim objID As System.Object, shtID As Short = 0

        objID = objConexion.EjecutarEscalar("SELECT Max(IdGrupo) As Cantidad FROM SE_Grupo_Tarea")
        If System.Convert.IsDBNull(objID) Then
            shtID = 1S
        Else
            shtID = System.Convert.ToInt16(objID) + 1S
        End If
        With sblSql
            .Append("INSERT INTO SE_Grupo_Tarea( IdGrupo, DescGrupo ) VALUES ( ")
            .Append(shtID) : .Append(","c)
            .Append(objConexion.XtoStr(NombreGrupo.Trim)) : .Append(")"c)
        End With

        objConexion.Ejecutar(sblSql.ToString)

        Return shtID

    End Function

    ''' <summary>
    ''' Pemite el ingreso de una o varias tareas primitivas al grupo
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <param name="dtsDatos">DataSet que contiene las tareas primitivas</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function IngresarTareasAlGrupo(ByVal IDGrupo As Short, ByVal dtsDatos As EE.dtsGrupos) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'rowTareas  : Objeto DataRow que se utiliza para ingresar las tareas 
        '             asignadas al grupo
        'strSql     : Sentencia SQL a ejecutar
        'intRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowTareas As EE.dtsGrupos.SE_TAREAS_ASIGNADASRow
        Dim strSql As String = String.Empty
        Dim intRet As Integer = 0

        For Each rowTareas In dtsDatos.SE_TAREAS_ASIGNADAS
            strSql = String.Empty
            strSql &= "UPDATE SE_Tareas SET IdGrupo = "
            strSql &= IDGrupo.ToString
            strSql &= " WHERE IdTarea = "
            strSql &= rowTareas.IDTAREA.ToString

            intRet += objConexion.Ejecutar(strSql)
        Next

        Return intRet

    End Function

    ''' <summary>
    ''' Ingresa los grupos que son excluyentes del grupo que se recibe por parámetro
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <param name="dtsdatos">DataSet que contiene los grupos excluyentes</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' Gustavo Mazzaglia - 02/03/2006 
    ''' [AndresR]          [viernes, 11 de julio de 2008]       GCP-Cambios ID: 7111
    ''' </history>
    Public Function IngresarGruposExcluyentes(ByVal IDGrupo As Short, ByVal dtsdatos As EE.dtsGrupos) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'rowGrupEx  : Objeto DataRow que se utiliza para ingresar los grupos
        '             excluyentes
        'strSql     : Sentencia Sql
        'intRet     : Cantidad de filas afectadas
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowGrupEx As EE.dtsGrupos.GRUPO_EXCLUYENTERow
        Dim strSql As String
        Dim intRet As Integer = 0

        'Si posee grupos que deben ser excluyentes
        For Each rowGrupEx In dtsdatos.GRUPO_EXCLUYENTE

            If rowGrupEx.RowState = DataRowState.Deleted Then Continue For

            If Not VerificarExistenciaGrupoExclusion(IDGrupo, rowGrupEx.IDGRUPO) Then
                strSql = String.Empty
                strSql &= "INSERT INTO SE_Grupo_Exclusion( IdGrupoActual, IdGrupExcluyente ) VALUES ( "
                strSql &= IDGrupo.ToString
                strSql &= ","c
                strSql &= rowGrupEx.IDGRUPO.ToString
                strSql &= ")"

                intRet += objConexion.Ejecutar(strSql)
            End If

            If Not VerificarExistenciaGrupoExclusion(rowGrupEx.IDGRUPO, IDGrupo) Then
                strSql = String.Empty
                strSql &= "INSERT INTO SE_Grupo_Exclusion( IdGrupoActual, IdGrupExcluyente ) VALUES ( "
                strSql &= rowGrupEx.IDGRUPO.ToString
                strSql &= ","c
                strSql &= IDGrupo.ToString
                strSql &= ")"

                objConexion.Ejecutar(strSql)

            End If
        Next

        Return intRet

    End Function

    ''' <summary>
    ''' Verificar si Existe un Grupo de Exclusion
    ''' </summary>
    ''' <param name="GrupoActual">Identificador de Grupo Actual</param>
    ''' <param name="GrupoExcluyente">Identificador de Grupo Excluyente</param>
    ''' <returns>True si existe el registro, False si No existe</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [jueves, 10 de julio de 2008]       Creado
    ''' </history>
    Private Function VerificarExistenciaGrupoExclusion(ByVal GrupoActual As Integer, ByVal GrupoExcluyente As Integer) As Boolean

        Return CType(objConexion.EjecutarEscalar("SELECT COUNT(*) FROM se_grupo_exclusion WHERE " & _
                                     " IdGrupoActual = " & GrupoActual.ToString & _
                                     " AND IdGrupExcluyente = " & GrupoExcluyente.ToString), Integer) > 0
    End Function
    ''' <summary>
    ''' Pemite actualizar la descripción de un grupo
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <param name="NuevoNombre">Nombre del grupo</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function ActualizarDescripcion(ByVal IDGrupo As Short, ByVal NuevoNombre As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return objConexion.Ejecutar("UPDATE SE_Grupo_Tarea SET DescGrupo = " & _
                objConexion.XtoStr(NuevoNombre.Trim) & _
                " WHERE IdGrupo = " & IDGrupo.ToString)

    End Function

    ''' <summary>
    ''' Elimina la asociación de las tareas primitivas con el grupo
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function EliminarTareasDelGrupo(ByVal IDGrupo As Short) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return objConexion.Ejecutar("UPDATE SE_Tareas SET IdGrupo = 0 WHERE idGrupo = " & IDGrupo.ToString)

    End Function

    ''' <summary>
    ''' Elimina los grupos que son excluyentes con el grupo que se recibe por parámetro
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function EliminarGruposExcluyentes(ByVal IDGrupo As Short) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("DELETE FROM SE_Grupo_Exclusion WHERE IdGrupoActual = " & IDGrupo.ToString)
        Return objConexion.Ejecutar("DELETE FROM SE_Grupo_Exclusion WHERE IdGrupExcluyente = " & IDGrupo.ToString)

    End Function

    ''' <summary>
    ''' Permite eliminar un determinado grupo de tareas
    ''' </summary>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function EliminarGrupo(ByVal IDGrupo As Short) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return objConexion.Ejecutar("DELETE FROM SE_Grupo_Tarea WHERE IdGrupo = " & IDGrupo.ToString)

    End Function


End Class
