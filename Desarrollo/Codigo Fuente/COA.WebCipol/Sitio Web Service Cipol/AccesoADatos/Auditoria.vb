Option Strict On
Option Explicit On

Imports EE = Fachada.Seguridad
Imports System.Text

Public Class Auditoria
    Inherits PadreSistema

#Region "RECUPERAR"

    ''' <summary>
    ''' Obtiene la fecha minima de los regitros de auditoria
    ''' </summary>
    ''' <returns>DateTime Fecha minima</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
    ''' </history>
    Public Function RecuperarFechaMinima() As Date
        Dim dtmFecha As Date = CType(objConexion.EjecutarEscalar("SELECT MIN(FECHAHORALOG) FROM SE_SIST_EVENTOS"), Date)
        Return dtmFecha
    End Function

    ''' <summary>
    ''' Recupera las tablas del sistema
    ''' </summary>
    ''' <returns>DataSet con los nombre de las tabla del sistema</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
    ''' </history>
    Public Function RecuperaTablasDeSistemas() As EE.dtsAuditoriaEventos
        Dim strSQL As String = ""
        Dim dtsEventos As New EE.dtsAuditoriaEventos
        dtsEventos = sumaRegTODOS(dtsEventos, "TablasDeSistema", "(TODAS)")
        If objConexion.ConectadoA = "Oracle" Then
            strSQL = "SELECT TABLE_NAME AS NombreTabla FROM USER_TABLES ORDER BY TABLE_NAME"
        Else
            strSQL = "SELECT TABLE_NAME AS NombreTabla FROM Information_schema.Tables WHERE Table_type = 'BASE TABLE' ORDER BY TABLE_NAME"
        End If
        objConexion.Ejecutar(strSQL, CType(dtsEventos, System.Data.DataSet), "TablasDeSistema")
        Return dtsEventos
    End Function

    ''' <summary>
    ''' Recupera los nombre de las terminales habilitadas 
    ''' </summary>
    ''' <returns>Dataset con las terminales habilitadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
    ''' </history>
    Public Function RecuperaTerminales() As EE.dtsAuditoriaEventos
        Dim dtsEventos As New EE.dtsAuditoriaEventos
        dtsEventos = sumaRegTODOS(dtsEventos, "SE_TERMINALES", "(TODAS)")
        objConexion.Ejecutar("SELECT NOMBRENETBIOS AS CODTERMINAL FROM SE_TERMINALES ORDER BY NOMBRENETBIOS", _
                           CType(dtsEventos, System.Data.DataSet), "SE_TERMINALES")
        Return dtsEventos
    End Function

    ''' <summary>
    ''' recupera todos los usuario del sistema para aplicar en los filtros
    ''' </summary>
    ''' <returns>DataSet con los usuarios</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
    ''' </history>
    Public Function RecuperarUsuarios() As EE.dtsAuditoriaEventos
        Dim dtsEventos As New EE.dtsAuditoriaEventos
        dtsEventos = sumaRegTODOS(dtsEventos, "SE_USUARIOS", "(TODOS)", "NOMBRES")
        objConexion.Ejecutar("SELECT USUARIO, NOMBRES FROM SE_USUARIOS ORDER BY NOMBRES", CType(dtsEventos, System.Data.DataSet), "SE_USUARIOS")
        Return dtsEventos
    End Function

    ''' <summary>
    ''' Recupera la lista de sistema habilitados
    ''' </summary>
    ''' <returns>DataSet con los sistemas habilitados</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 02 de enero de 2009]   Creado
    ''' </history>
    Public Function RecuperarSistemasHabilitados() As EE.dtsAuditoriaEventos
        Dim dtsEventos As New EE.dtsAuditoriaEventos
        dtsEventos = sumaRegTODOS(dtsEventos, "SE_SIST_HABILITADOS")
        objConexion.Ejecutar("SELECT DESCSISTEMA FROM SE_SIST_HABILITADOS ORDER BY DESCSISTEMA", CType(dtsEventos, System.Data.DataSet), "SE_SIST_HABILITADOS")
        Return dtsEventos
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
    Public Function ProbarStringSQL(ByVal NewStringSQL As String) As Boolean
        Try
            objConexion.TransaccionIniciar()
            objConexion.Ejecutar(NewStringSQL)
            objConexion.TransaccionFinalizar(False)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Recupera los datos de la tabla SIST_EVENTOS
    ''' </summary>
    ''' <param name="dtsFiltros">Dataset con los filtros a aplicar</param>
    ''' <returns>dataset con los datos seleccionados</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [lunes, 05 de enero de 2009]   Creado
    ''' [LucianoP]          [jueves, 1 de junio de 2017]    Calcula la cantidad de registros recuperados
    ''' </history>
    Public Function RecuperarEventos(ByVal dtsFiltros As EE.dtsAuditoriaEventos) As EE.dtsAuditoriaEventos
        Dim dtsEventos As New EE.dtsAuditoriaEventos
        If dtsFiltros Is Nothing Then Return Nothing

        Dim sblSQL As StringBuilder = Nothing, sblSQL_WHERE As StringBuilder = Nothing

        Dim StringSQL As String = RetornaStringSQL(dtsFiltros.dtFiltros, sblSQL, sblSQL_WHERE)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '1. CALCULA LA CANTIDAD DE REGISTROS RECUPERADOS
        dtsEventos.ResultadoConsulta.AddResultadoConsultaRow(0) '0: Sin limite de registros

        If Not dtsFiltros.dtFiltros(0).IsCantidadRegistrosDefaultNull Then
            Dim intCantReg As Integer
            If Integer.TryParse(dtsFiltros.dtFiltros(0).CantidadRegistrosDefault, intCantReg) AndAlso intCantReg > 0 Then
                'Calcula la cantidad de registros reales de la consulta
                Dim sblCantidadDeRegistrosRecuperados As New StringBuilder
                sblCantidadDeRegistrosRecuperados.Append("SELECT COUNT(*) FROM SE_SIST_EVENTOS ")
                sblCantidadDeRegistrosRecuperados.Append(sblSQL_WHERE.ToString())

                Dim intRegTotal As Integer = Convert.ToInt32(objConexion.EjecutarEscalar(sblCantidadDeRegistrosRecuperados.ToString()))

                dtsEventos.ResultadoConsulta(0).CantidadRegistros = intRegTotal
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '2. EJECUTA LA CONSULTA
        If StringSQL.Trim().Length > 0 Then
            objConexion.Ejecutar(StringSQL, CType(dtsEventos, System.Data.DataSet), "SIST_EVENTOS")
            Return dtsEventos
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Recupera el String SQL a ejecutar
    ''' </summary>
    ''' <param name="dtsFiltros">Dataset con los filtros a aplicar</param>
    ''' <returns>dataset con los datos seleccionados</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [martes, 06 de enero de 2009]   Creado
    ''' </history>
    Public Function RecuperarStringSQL(ByVal dtsFiltros As EE.dtsAuditoriaEventos) As String
        Dim dtsEventos As New EE.dtsAuditoriaEventos

        If dtsFiltros Is Nothing Then Return Nothing

        Dim strSQL As New StringBuilder()
        Dim strSQL_WHERE As New StringBuilder()

        Dim StringSQL As String = RetornaStringSQL(dtsFiltros.dtFiltros, strSQL, strSQL_WHERE)

        Return StringSQL
    End Function

    ''' <summary>
    ''' Recupera todos los supervisores del sistema para aplicar en los filtros (Supervisores que no han sido dados de baja GCP-Cambio ID: 9219)
    ''' </summary>
    ''' <returns>DataSet con los usuarios</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 06 de marzo de 2009]   Creado
    ''' [IvanR]   [Miercoles, 28 de julio de 2010]  modificado GCP-Cambio ID: 9219
    ''' </history>
    Public Function RecuperarSupervisores() As EE.dtsAuditoriaEventos
        Dim dtsEventos As New EE.dtsAuditoriaEventos
        Dim strSQL As String = "SELECT DISTINCT Usuario, Nombres " ', SE_USUARIOS.IdUsuario,  IdTareaPrimitiva, TareaInhibida "
        strSQL += " FROM SE_USUARIOS"
        strSQL += " INNER JOIN SE_Tareas_Usuario ON"
        strSQL += " SE_USUARIOS.IdUsuario = SE_Tareas_Usuario.IdUsuario"
        strSQL += " INNER JOIN SE_Rel_Autoriz ON"
        strSQL += " SE_Tareas_Usuario.IdTarea = IdTareaAutor"
        strSQL += " WHERE  SE_USUARIOS.Fechabaja IS NULL" 'GCP-Cambio ID: 9219
        strSQL += " ORDER BY Nombres"
        dtsEventos = sumaRegTODOS(dtsEventos, "dtSupervisores", "(TODOS)", "NOMBRES")
        If objConexion.Ejecutar(strSQL, CType(dtsEventos, System.Data.DataSet), "dtSupervisores") <= 0 Then
            If dtsEventos Is Nothing Then dtsEventos = New EE.dtsAuditoriaEventos
            If dtsEventos.dtSupervisores.Rows.Count <= 0 Then
                dtsEventos = sumaRegTODOS(dtsEventos, "dtSupervisores", "(TODOS)", "NOMBRES")
            End If
        End If
        Return dtsEventos
    End Function

#End Region
#Region "FUNCIONES Y METODOS"

#Region "PRIVADOS"

    ''' <summary>
    ''' Retorna el string SQL a ejecutar
    ''' </summary>
    ''' <param name="dttFiltros">DataTable con los filtros a aplicar</param>
    ''' <param name="strSQL">Contiene el cuerpo de la consulta SQL</param>
    ''' <param name="strSQL_WHERE">Contiene el where de la consulta SQL</param>
    ''' <returns>String SQL a ejecutar</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [lunes, 05 de enero de 2009]   Creado
    ''' [LucianoP]          [jueves, 1 de junio de 2017]    Se agrega filtro para el tope maximo de registros
    ''' </history>
    Private Function RetornaStringSQL(ByVal dttFiltros As EE.dtsAuditoriaEventos.dtFiltrosDataTable, _
                                      ByRef strSQL As StringBuilder, ByRef strSQL_WHERE As StringBuilder) As String
        If dttFiltros Is Nothing Then Return ""
        If dttFiltros.Rows.Count <= 0 Then Return ""
        Dim rowFiltro As EE.dtsAuditoriaEventos.dtFiltrosRow = CType(dttFiltros.Rows(0), EE.dtsAuditoriaEventos.dtFiltrosRow)

        strSQL = New StringBuilder()
        strSQL_WHERE = New StringBuilder()

        If rowFiltro.IsCantidadRegistrosDefaultNull Then
            rowFiltro.CantidadRegistrosDefault = "5000"
        End If

        Dim strTopeRegistros As String = rowFiltro.CantidadRegistrosDefault
        If objConexion.ConectadoA.Trim.ToUpper = "ORACLE" Then
            strSQL.Append("SELECT * FROM (")
        End If
        strSQL.Append("SELECT ")

        If Not objConexion.ConectadoA.Trim.ToUpper = "ORACLE" Then
            Dim intCantReg As Integer
            If Not String.IsNullOrEmpty(strTopeRegistros) AndAlso Integer.TryParse(strTopeRegistros, intCantReg) AndAlso intCantReg > 0 Then
                strSQL.Append(" TOP " + strTopeRegistros + " ")
            End If
        End If
        strSQL.Append(" FECHAHORALOG,")
        strSQL.Append(" CASE OPERACION")
        strSQL.Append(" WHEN 'A' THEN 'ALTA'")
        strSQL.Append(" WHEN 'B' THEN 'BAJA'")
        strSQL.Append(" WHEN 'M' THEN 'MODIFICACION'")
        strSQL.Append(" END AS OPERACION, ")
        strSQL.Append(" TABLA, STRINGSQL, USUARIO, SUPERVISOR, NOMBREPC, SISTEMA")
        If objConexion.ConectadoA.Trim.ToUpper = "ORACLE" Then
            strSQL.Append(", ROWNUM")
        End If
        strSQL.Append(" FROM SE_SIST_EVENTOS")
        strSQL_WHERE.Append(" WHERE ")
        strSQL_WHERE.Append(" FECHAHORALOG BETWEEN ")
        strSQL_WHERE.Append(objConexion.XtoStr(rowFiltro.FECHADESDE))
        strSQL_WHERE.Append(" AND ")
        strSQL_WHERE.Append(objConexion.XtoStr(rowFiltro.FECHAHASTA))
        If Not rowFiltro.USUARIO = "(TODOS)" Then
            strSQL_WHERE.Append(" AND USUARIO = " + objConexion.XtoStr(rowFiltro.USUARIO))
        End If
        If Not rowFiltro.NOMBREPC = "(TODAS)" Then
            strSQL_WHERE.Append(" AND NOMBREPC = " + objConexion.XtoStr(rowFiltro.NOMBREPC))
        End If
        If Not rowFiltro.SUPERVISOR = "(TODOS)" Then
            strSQL_WHERE.Append(" AND SUPERVISOR = " + objConexion.XtoStr(rowFiltro.SUPERVISOR))
        End If
        If Not rowFiltro.SISTEMA = "(TODOS)" Then
            strSQL_WHERE.Append(" AND SISTEMA = " + objConexion.XtoStr(rowFiltro.SISTEMA))
        End If
        If Not rowFiltro.TABLA = "(TODAS)" Then
            strSQL_WHERE.Append(" AND TABLA = " + objConexion.XtoStr(rowFiltro.TABLA))
        End If
        If Not rowFiltro.OPERACION = "T" Then
            strSQL_WHERE.Append(" AND OPERACION = " + objConexion.XtoStr(rowFiltro.OPERACION))
        End If
        If rowFiltro.TEXTOBUSCAR.Trim.Length > 0 Then
            strSQL_WHERE.Append(" AND STRINGSQL LIKE '%" + rowFiltro.TEXTOBUSCAR.Trim + "%'")
        End If
        If objConexion.ConectadoA.Trim.ToUpper = "ORACLE" Then
            'COLOCA UN TOPE DE 100 REGISTROS PARA ORACLE
            'DEVUELVE 101 PARA AVISAR QUE LA CONSULTA SUPERO LOS 100 REGISTROS QUE ACOTE 
            'MEJOR LA BUSQUEDA
            strSQL_WHERE.Append(") WHERE ROWNUM <= " + strTopeRegistros)
        End If

        Return strSQL.Append(strSQL_WHERE.ToString()).ToString()

    End Function

    ''' <summary>
    ''' Agrega un registro de Texto a la tabla indicada. 
    ''' Si no se indica texto agrega el texto TODOS
    ''' </summary>
    ''' <param name="dtsOrigen">DataSet con la tabla</param>
    ''' <param name="NombreDtTable">Nombre de la Tabla donde se agregara el registro</param>
    ''' <returns>Dataset con la datatable con la datarow agregada</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [lunes, 02 de enero de 2009]   Creado
    ''' </history>
    Private Function sumaRegTODOS(ByVal dtsOrigen As EE.dtsAuditoriaEventos, ByVal NombreDtTable As String) As EE.dtsAuditoriaEventos
        Return sumaRegTODOS(dtsOrigen, NombreDtTable, "", "")
    End Function
    ''' <summary>
    ''' Agrega un registro de Texto a la tabla indicada
    ''' </summary>
    ''' <param name="dtsOrigen">DataSet con la tabla</param>
    ''' <param name="NombreDtTable">Nombre de la Tabla donde se agregara el registro</param>
    ''' <param name="TextoAgregar">Texto a agregar como registro en la tabla</param>
    ''' <returns>Dataset con la datatable con la datarow agregada</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [lunes, 02 de enero de 2009]   Creado
    ''' </history>
    Private Function sumaRegTODOS(ByVal dtsOrigen As EE.dtsAuditoriaEventos, ByVal NombreDtTable As String, ByVal TextoAgregar As String) As EE.dtsAuditoriaEventos
        Return sumaRegTODOS(dtsOrigen, NombreDtTable, TextoAgregar, "")
    End Function
    ''' <summary>
    ''' Agrega un registro de Texto a la tabla indicada
    ''' </summary>
    ''' <param name="dtsOrigen">DataSet con la tabla</param>
    ''' <param name="NombreDtTable">Nombre de la Tabla donde se agregara el registro</param>
    ''' <param name="TextoAgregar">Texto a agregar como registro en la tabla</param>
    ''' <param name="NombreCampoAdicional">Si la tabla tiene un campo para mostrar y un campo clave,
    ''' se le asigna el mismo valor a los dos</param>
    ''' <returns>Dataset con la datatable con la datarow agregada</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [lunes, 02 de enero de 2009]   Creado
    ''' </history>
    Private Function sumaRegTODOS(ByVal dtsOrigen As EE.dtsAuditoriaEventos, _
                                    ByVal NombreDtTable As String, ByVal TextoAgregar As String, _
                                    ByVal NombreCampoAdicional As String) As EE.dtsAuditoriaEventos
        Dim rowNuevo As System.Data.DataRow
        rowNuevo = dtsOrigen.Tables(NombreDtTable).NewRow
        If TextoAgregar.Trim.Length <= 0 Then TextoAgregar = "(TODOS)"
        rowNuevo.Item(0) = TextoAgregar
        If NombreCampoAdicional.Trim.Length > 0 Then
            rowNuevo.Item(NombreCampoAdicional) = TextoAgregar
        End If
        dtsOrigen.Tables(NombreDtTable).Rows.Add(rowNuevo)
        Return dtsOrigen
    End Function

#End Region

#End Region
End Class
