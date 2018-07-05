Option Strict On
Option Explicit On
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'                   DESCRIPCION DE VARIABLES LOCALES
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports EE = Fachada.Seguridad

Public Class Tarea
    Inherits PadreSistema


    ''' <summary>
    ''' Da de baja una tarea.
    ''' </summary>
    ''' <param name="pidTarea">identificadord e la tarea.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [angell]	10/11/2005	Created
    ''' [Gustavom]            [lunes, 10 de julio de 2006]        GCP-Cambios ID: 3966
    ''' </history>
    Public Function BajaTarea(ByVal pidTarea As Decimal) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar(" DELETE FROM SE_TAREAS_USUARIO WHERE IdTarea = " & objConexion.XtoStr(pidTarea))
        objConexion.Ejecutar(" DELETE FROM SE_Comp_Roles WHERE IdTarea = " & objConexion.XtoStr(pidTarea))
        Return objConexion.Ejecutar(" DELETE FROM SE_Tareas WHERE IdTarea = " & objConexion.XtoStr(pidTarea))

    End Function

    ''' <summary>
    ''' Elimina un registro den la tabla se_rel_autoriz
    ''' </summary>
    ''' <param name="pidTareaPrimitiva">identificador de la tarea primitiva</param>
    ''' <param name="pidTareaAutor">identificador del la tarea autor</param>
    ''' <returns>antidad de filas insertadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	10/11/2005	Created
    ''' </history>
    Public Function BajaRelacionAutorizacion(Optional ByVal pidTareaPrimitiva As Int32 = -1, Optional ByVal pidTareaAutor As Decimal = -1D) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql        : String de sentencia sql. 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("DELETE FROM SE_Rel_Autoriz WHERE 1=1")

        If Not pidTareaPrimitiva.Equals(-1) Then _
            sblSql.Append("  AND IdTareaPrimitiva = " + objConexion.XtoStr(pidTareaPrimitiva))

        If Not pidTareaAutor.Equals(-1) Then _
                sblSql.Append(" AND IdTareaAutor = " + objConexion.XtoStr(pidTareaAutor))

        Return objConexion.Ejecutar(sblSql.ToString())
    End Function


    ''' <summary>
    ''' actualiza una linea en la tabla de tareas
    ''' </summary>
    ''' <param name="pidAutorizacion">codigo de autorización. </param>
    ''' <param name="pidTarea">identificador de la tarea a ser modificada</param>
    ''' <returns>retorna la cantidad de filas afectadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	09/11/2005	Created
    ''' </history>
    Public Function ModificarIdAutorizacionDeTarea(ByVal pidAutorizacion As Int32, ByVal pidTarea As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de sentencia sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" UPDATE SE_Tareas SET ")
        sblSql.Append(" IdAutorizacion = " + objConexion.XtoStr(pidAutorizacion))
        sblSql.Append(" WHERE idTarea = " + objConexion.XtoStr(pidTarea))

        objConexion.Ejecutar(sblSql.ToString())
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' retorna datos de la tabla se_rel_autoriz
    ''' </summary>
    ''' <param name="dtsDataset">dataset que cotnendrá alt abla con los datos</param>
    ''' <param name="strNomreTabla">tabla que contendrá los resultados de lac onsulta</param>
    ''' <param name="pFiltroIdTareaAutor">filtro opcional para el id del autor de la tarea</param>
    ''' <returns>cantidad de filas retornadas por la tabla.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	10/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetornarRelacionAutorizacion(ByVal dtsDataset As DataSet, ByVal strNomreTabla As String, Optional ByVal pFiltroIdTareaAutor As Int32 = -1) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de sentencia sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT * FROM SE_Rel_Autoriz WHERE 1=1 ")

        If Not pFiltroIdTareaAutor.Equals(-1) Then
            sblSql.Append(" and IdTareaAutor = " & objConexion.XtoStr(pFiltroIdTareaAutor))
        End If

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNomreTabla)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' da de alta una tarea de usuario 
    ''' </summary>
    ''' <param name="idUsuario">identificador del usuario</param>
    ''' <param name="idrol">identificador del rol de usuario</param>
    ''' <param name="idtarea">identificador de la tarea de usuario</param>
    ''' <param name="tareainhibida">indica si la tarea está inhibida o no</param>
    ''' <returns>cantidad de filas afectadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	27/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function AltaTareasUsuario(ByVal idUsuario As Int32, ByVal idrol As Int32, ByVal idtarea As Int32, ByVal tareainhibida As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        Dim objUsuarioCipol As EntidadesEmpresariales.PadreCipolCliente
        If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            objUsuarioCipol = CType(System.Web.HttpContext.Current.Session("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        Else
            objUsuarioCipol = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        End If

        sblSql.Append(" INSERT INTO SE_Tareas_Usuario( IdUsuario, IdRol, IdTarea,  ")
        sblSql.Append("     TareaInhibida, fechaultmodif, idusuarioultmodif ) VALUES ( ")
        sblSql.Append(objConexion.XtoStr(idUsuario))
        sblSql.Append(", " + objConexion.XtoStr(idrol))
        sblSql.Append(", " + objConexion.XtoStr(idtarea))
        sblSql.Append(", " + objConexion.XtoStr(tareainhibida))
        sblSql.Append(", " + objConexion.XtoStr(objConexion.FechaServidor))
        sblSql.Append(", " + objConexion.XtoStr(objUsuarioCipol.IDUsuario))
        sblSql.Append(" )")

        Return objConexion.Ejecutar(sblSql.ToString())

    End Function

    ''' <summary>
    ''' Recupera las tareas primitivas de acuerdo a un sistema/grupo
    ''' </summary>
    ''' <param name="dtsDatos">DataSet de retorno</param>
    ''' <param name="IDSistema">Identificador del sistema</param>
    ''' <param name="IDGrupo">Identificador del grupo</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 01/03/2006 </history>
    Public Sub RecuperarTareas(ByRef dtsDatos As System.Data.DataSet, Optional ByVal IDSistema As Short = -1, Optional ByVal IDGrupo As Short = -1)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        With sblsql
            .Append("SELECT IdTarea, DescripcionTarea FROM SE_Tareas WHERE 1 = 1")
            If IDGrupo >= 0 Then .Append(" AND IDGrupo = ") : .Append(IDGrupo)
            If IDSistema >= 0 Then .Append(" AND IDSistema = ") : .Append(IDSistema)
            .Append(" ORDER BY DescripcionTarea")
        End With

        objConexion.Ejecutar(sblsql.ToString, CType(dtsDatos, System.Data.DataSet), "SE_TAREAS")

    End Sub

    ''' <summary>
    ''' Recupera las tareas primitivas de acuerdo a un sistema/grupo
    ''' </summary>
    ''' <returns>DataTable de de retorno de datos</returns>
    ''' <param name="IDSistema">Identificador del sistema</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
    Public Function RecuperarTareasSinAutorizacion(ByVal IDSistema As Short, ByVal dtsDataset As DataSet) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String builder que se utiliza para armar la sentencia SQL
        'dtsRet : DataSet de retorno de datos
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        sblsql.Append(" SELECT SE_Tareas.IdTarea, DescripcionTarea FROM SE_Tareas ")
        sblsql.Append(" WHERE SE_Tareas.IdSistema = ")
        sblsql.Append(IDSistema)
        sblsql.Append(" AND IdAutorizacion = 0 ")
        sblsql.Append(" ORDER BY DescripcionTarea")

        Return objConexion.Ejecutar(sblsql.ToString, dtsDataset, "SE_TAREAS")

    End Function


    ''' <summary>
    ''' Recupera las tareas autorizantes de las taras primitivas
    ''' </summary>
    ''' <param name="dtsTareas">DataSet de retorno</param>
    ''' <remarks></remarks>
    Public Sub RecuperarTareasAutorizantes(ByRef dtsTareas As EE.dtsTareas)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '              DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia
        '         SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" SELECT SE_Tareas.IdTarea, DescSistema, DescripcionTarea, CodigoTarea, SE_Sist_Habilitados.IdSistema, RequiereAuditoria ")
        sblSql.Append(" FROM SE_Sist_Habilitados, SE_Tareas ")
        sblSql.Append(" WHERE SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema AND ")
        sblSql.Append("         SE_Tareas.IdAutorizacion IS NULL")

        objConexion.Ejecutar(sblSql.ToString, CType(dtsTareas, System.Data.DataSet), dtsTareas.TAREAS.ToString)

    End Sub

    ''' <summary>
    ''' Recupera las tareas primitivas que requieren autorización
    ''' </summary>
    ''' <param name="IDTareaAutorizante">Identificador de la tarea autorizante</param>
    ''' <returns>DataSet de retorno de datos</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
    Public Function RecuperarTareasQueRequierenAutorizacion(ByVal IDTareaAutorizante As Integer, ByVal dtsDataset As DataSet) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        'dtsRet : DataSet de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("SELECT SE_Tareas.IdTarea, DescripcionTarea, IdAutorizacion ")
            .Append(" FROM SE_Tareas, SE_Rel_Autoriz ")
            .Append(" WHERE SE_Tareas.IdTarea = SE_Rel_Autoriz.IdTareaPrimitiva")
            .Append(" AND SE_Rel_Autoriz.IdTareaAutor = ")
            .Append(IDTareaAutorizante)
        End With

        Return objConexion.Ejecutar(sblSql.ToString, dtsDataset, "SE_TAREASAUTORIZADAS")

    End Function

    ''' <summary>
    ''' Ingresa una nueva tarea autorizante y retorna el identificador
    ''' </summary>
    ''' <param name="dtsDatos">DataSet que contiene los datos a ingresar</param>
    ''' <returns>Identificador de la tarea autorizante</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
    Public Function IngresarTareaAutorizante(ByVal dtsDatos As EE.dtsTareas) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'objRetorno : Identificador de la tarea
        'intID      : Valor de retorno
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objRetorno As Object
        Dim intID As Integer = 0, sblSql As New System.Text.StringBuilder

        objRetorno = objConexion.EjecutarEscalar("SELECT MAX(idtarea) As Cantidad FROM SE_Tareas " & _
                    " WHERE IDSistema = " & objConexion.XtoStr(dtsDatos.TAREAS(0).IDSISTEMA))
        If objRetorno Is Nothing Then
            intID = 0
        Else
            intID = CType(objRetorno, Integer) + 1
        End If

        sblSql.Append("INSERT INTO SE_Tareas(IdTarea, CodigoTarea, DescripcionTarea, IdAutorizacion, IdSistema ")
        sblSql.Append(", IdGrupo, RequiereAuditoria ) VALUES ( ")
        sblSql.Append(intID)
        sblSql.Append(",")
        sblSql.Append(objConexion.XtoStr(dtsDatos.TAREAS(0).CODIGOTAREA.Trim))
        sblSql.Append(",")
        sblSql.Append(objConexion.XtoStr(dtsDatos.TAREAS(0).DESCRIPCIONTAREA.Trim))
        sblSql.Append(",")
        sblSql.Append("NULL")
        sblSql.Append(",")
        sblSql.Append(dtsDatos.TAREAS(0).IDSISTEMA)
        sblSql.Append(",")
        sblSql.Append(0)
        sblSql.Append(",")
        sblSql.Append(objConexion.XtoStr(dtsDatos.TAREAS(0).REQUIEREAUDITORIA))
        sblSql.Append(" )")

        Me.objConexion.Ejecutar(sblSql.ToString)

        Return intID

    End Function

    ''' <summary>
    ''' Permite establecer la relación entre la tarea autorizante y la primitiva
    ''' </summary>
    ''' <param name="IDTareaAutorizante">Identificador de la tarea autorizante</param>
    ''' <param name="IDTareaPrimitiva">Identificaodr de la tarea primitiva</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
    Public Sub IngresarRelacionConTareaAutorizante(ByVal IDTareaAutorizante As Integer, ByVal IDTareaPrimitiva As Integer)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ' sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("INSERT INTO SE_Rel_Autoriz( IdTareaPrimitiva, IdTareaAutor ) VALUES ( ")
        sblSql.Append(IDTareaPrimitiva)
        sblSql.Append(",")
        sblSql.Append(IDTareaAutorizante)
        sblSql.Append(" )")

        objConexion.Ejecutar(sblSql.ToString())

    End Sub

    ''' <summary>
    ''' Elimina la relación existe
    ''' </summary>
    ''' <param name="IDTareaAutorizante"></param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
    Public Sub EliminarRelacionConTareaAutorizante(ByVal IDTareaAutorizante As Integer)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'intIDTarea : Identificador de la tarea primitiva asociada
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim intIDTarea As Integer = 0

        intIDTarea = CType(Me.objConexion.EjecutarEscalar("SELECT IDTareaPrimitiva FROM SE_Rel_Autoriz WHERE IdTareaAutor = " & IDTareaAutorizante.ToString), Integer)
        objConexion.Ejecutar("DELETE FROM SE_Rel_Autoriz WHERE IDTareaPrimitiva = " & intIDTarea.ToString & " AND IDTareaAutor = " & IDTareaAutorizante)
        objConexion.Ejecutar("UPDATE SE_Tareas SET IDAutorizacion = 0 WHERE IDTarea = " & intIDTarea.ToString)

    End Sub

    ''' <summary>
    ''' Permite actualizar una tarea autorizante
    ''' </summary>
    ''' <param name="dtsDatos">DataSet de retorno de datos</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    Public Function ActualizarTareaAutorizante(ByVal dtsDatos As EE.dtsTareas) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de sentencia sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" UPDATE SE_Tareas SET ")
        sblSql.Append(" CodigoTarea = ")
        sblSql.Append(objConexion.XtoStr(dtsDatos.TAREAS(0).CODIGOTAREA.Trim))
        sblSql.Append(", DescripcionTarea = ")
        sblSql.Append(objConexion.XtoStr(dtsDatos.TAREAS(0).DESCRIPCIONTAREA.Trim))
        sblSql.Append(", RequiereAuditoria =")
        sblSql.Append(objConexion.XtoStr(dtsDatos.TAREAS(0).REQUIEREAUDITORIA))
        sblSql.Append(" WHERE idTarea = ")
        sblSql.Append(dtsDatos.TAREAS(0).IDTAREA)

        Return objConexion.Ejecutar(sblSql.ToString())

    End Function

    ''' <summary>
    ''' Recupera las tareas primitivas del sistema
    ''' </summary>
    ''' <param name="dtsDatos">DataSet con los datos de las tareas</param>
    ''' <returns>Numero de filas afectadas en la operacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [martes, 29 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' </history>
    Public Function RecuperarTareasPrimitivas(ByVal dtsDatos As System.Data.DataSet) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql : StringBuilder que contiene la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append(" SELECT T.IDTAREA, T.CODIGOTAREA, T.DESCRIPCIONTAREA, T.REQUIEREAUDITORIA, ")
            .Append(" T.IDAUTORIZACION, T.IDSISTEMA, T.IDGRUPO, H.CODSISTEMA, RTRIM(H.DESCSISTEMA) AS DESCSISTEMA")
            .Append(" FROM SE_TAREAS T")
            .Append(" INNER JOIN SE_SIST_HABILITADOS H ON H.IDSISTEMA = T.IDSISTEMA")
            .Append(" WHERE NOT T.IDAUTORIZACION IS NULL")
            .Append("       AND H.SISTEMAHABILITADO = " & objConexion.XtoStr("S"))

            '[MiguelP]          22/10/2014      Cambios GCP - Se cambia el ordenamiento
            '.Append(" ORDER BY T.DESCRIPCIONTAREA")
            .Append(" ORDER BY T.IDTAREA")
        End With

        Return objConexion.Ejecutar(sblSql.ToString, dtsDatos, "TAREAS")

    End Function

    ''' <summary>
    ''' Crea una tarea primitiva
    ''' </summary>
    ''' <returns>Cantidad de filas afectadas en la operacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [martes, 29 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' [AndresR]           [viernes, 20 de junio de 2008]      GCP-Cambios ID: 7035
    ''' </history>
    Public Function InsertarTareaPrimitiva(ByVal rowTarea As System.Data.DataRow) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql : StringBuilder que contiene la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append(" INSERT INTO SE_TAREAS (IDTAREA, CODIGOTAREA, DESCRIPCIONTAREA, ")
            .Append(" REQUIEREAUDITORIA, IDAUTORIZACION, IDSISTEMA, IDGRUPO) VALUES(")
            .Append(rowTarea.Item("IDTAREA").ToString & ", ")
            .Append(objConexion.XtoStr(rowTarea.Item("CODIGOTAREA").ToString) & ", ")
            .Append(objConexion.XtoStr(rowTarea.Item("DESCRIPCIONTAREA").ToString) & ", ")
            .Append(objConexion.XtoStr(rowTarea.Item("REQUIEREAUDITORIA").ToString) & ", ")
            .Append("0, ") 'IDAUTORIZACION
            .Append(rowTarea.Item("IDSISTEMA").ToString & ", ")
            .Append(rowTarea.Item("IDGRUPO").ToString & ") ") 'IDGRUPO
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function

    ''' <summary>
    ''' Actualiza los datos de las tareas primitivas
    ''' </summary>
    ''' <returns>Numero de filas afectadas por la operacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [miércoles, 30 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' [JorgeI]  [viernes, 02 de enero de 2009]   Modificado GCP-Cambios ID:7656
    ''' </history>
    Public Function ActualizarTareaPrimitiva(ByVal rowTarea As System.Data.DataRow) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql : StringBuilder que contiene la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append(" UPDATE SE_TAREAS SET")
            .Append("  CODIGOTAREA = " & objConexion.XtoStr(rowTarea.Item("CODIGOTAREA").ToString) & ", ")
            .Append("  DESCRIPCIONTAREA = " & objConexion.XtoStr(rowTarea.Item("DESCRIPCIONTAREA").ToString) & ", ")
            .Append("  REQUIEREAUDITORIA = " & objConexion.XtoStr(rowTarea.Item("REQUIEREAUDITORIA").ToString) & ", ")
            If rowTarea.IsNull("IDAUTORIZACION") Then
                .Append(" IDAUTORIZACION = NULL, ")
            Else
                .Append(" IDAUTORIZACION = " & rowTarea.Item("IDAUTORIZACION").ToString & ", ")
            End If
            If rowTarea.IsNull("IDGRUPO") Then
                .Append(" IDGRUPO = NULL, ")
            Else
                ' [JorgeI]  Modificado GCP-Cambios ID:7656
                .Append(" IDGRUPO = " & rowTarea.Item("IDGRUPO").ToString & ", ")
            End If
            .Append(" IDSISTEMA = " & rowTarea.Item("IDSISTEMA").ToString)
            .Append(" WHERE IDTAREA = " & rowTarea.Item("IDTAREA").ToString)
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function

    ''' <summary>
    ''' Elimina una tarea primitiva
    ''' </summary>
    ''' <returns>Numero de filas afectadas en la operacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [miércoles, 30 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' </history>
    Public Function EliminarTareaPrimitiva(ByVal IDTarea As Integer) As Integer
        Return objConexion.Ejecutar("DELETE FROM SE_TAREAS WHERE IDTAREA = " & IDTarea.ToString())
    End Function

    ''' <summary>
    ''' Verifica si existen roles asociados a la tarea
    ''' </summary>
    ''' <param name="IDTarea">Identificador de Tarea</param>
    ''' <returns>True si existen roles, False caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [lunes, 07 de julio de 2008]       Creado
    ''' </history>
    Public Function VerificarExistenciaRoles(ByVal IDTarea As Integer) As Boolean
        Return CType(objConexion.EjecutarEscalar("SELECT COUNT(IDTAREA) AS Tareas FROM SE_COMP_ROLES WHERE IDTAREA =" & IDTarea.ToString()), Integer) > 0
    End Function


    ''' <summary>
    ''' Verifica si existen usuarios asociados a la tarea
    ''' </summary>
    ''' <param name="IDTarea">Identificador de Tarea</param>
    ''' <returns>True si existen usuarios, False caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [lunes, 07 de julio de 2008]       Creado
    ''' </history>
    Public Function VerificarExistenciaUsuario(ByVal IDTarea As Integer) As Boolean
        Return CType(objConexion.EjecutarEscalar("SELECT COUNT(IDTAREA) AS Tareas FROM SE_TAREAS_USUARIO WHERE IDTAREA = " & IDTarea.ToString()), Integer) > 0
    End Function


    ''' <summary>
    ''' Verifica si la tarea autorizante está en uso en un ROL
    ''' </summary>
    ''' <param name="idTarea">ID de tarea a verificar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [miércoles, 22 de septiembre de 2010] Creado GCP-Cambios ID:9374
    ''' </history>
    Public Function VerificaTareasAutorizantesEnUso(ByVal IDTarea As Integer) As Integer
        Dim intRol As Integer = 0
        Dim intUsr As Integer = 0
        Dim strRta As String = "00"
        intRol = Convert.ToInt32(objConexion.EjecutarEscalar("SELECT COUNT(IDTAREA) AS Tareas FROM SE_COMP_ROLES WHERE IDTAREA = " & IDTarea.ToString))
        intUsr = Convert.ToInt32(objConexion.EjecutarEscalar("SELECT COUNT(IDTAREA) AS Tareas FROM SE_TAREAS_USUARIO WHERE IDTAREA = " & IDTarea.ToString))
        If intRol > 0 Then
            intRol = 1
        End If
        If intUsr > 0 Then
            intUsr = 1
        End If
        strRta = intRol.ToString().Trim() + intUsr.ToString().Trim()
        Select Case strRta
            Case "00"
                Return 0 'No está en uso
            Case "10"
                Return 1 'Está en uso en Roles
            Case "01"
                Return 2 'Está en uso en Usuarios
            Case "11"
                Return 3 'Está en uso en los dos
        End Select
    End Function


    ''' <summary>
    ''' Verifica si existe una tarea
    ''' </summary>
    ''' <param name="IDTarea">ID de la tarea a verificar</param>
    ''' <returns>0 (Cero) si no existe. Un valor positivo si existe</returns>
    ''' <remarks></remarks>
    Public Function ExisteTarea(ByVal IDTarea As Integer) As Integer
        Return Convert.ToInt32(objConexion.EjecutarEscalar("SELECT COUNT(*) FROM SE_TAREAS WHERE IDTAREA = " & IDTarea.ToString))
    End Function

    ''' <summary>
    ''' Retorna el maximo ID de la tarea de un sistema
    ''' </summary>
    ''' <param name="IDSistema">ID del sistema al cual pertenece la tarea</param>
    ''' <returns>ID maximo de la tarea perteneciente al sistema</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [miércoles, 30 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' </history>
    Public Function ObtenerMAXIDTareaPorSistema(ByVal IDSistema As Integer) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'objRet : Retorno de la consulta SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objRet As Object

        objRet = objConexion.EjecutarEscalar("SELECT NVL(MAX(IDTAREA),0) FROM SE_TAREAS WHERE IDSISTEMA = " & IDSistema.ToString)

        If objRet Is Nothing Then Return -1

        Return Convert.ToInt32(objRet)

    End Function

End Class