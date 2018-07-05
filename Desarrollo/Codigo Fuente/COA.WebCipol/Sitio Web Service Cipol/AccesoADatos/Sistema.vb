Option Strict On
Option Explicit On

Imports System.Convert

Public Class Sistema
    Inherits PadreSistema

#Region "Auditoria"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna el log de auditoria entre un par de fechas indicado filtrado por
    ''' los correspondientes usuarios
    ''' </summary>
    ''' <param name="fechadesde">extremo inicial del intervalo de fechas</param>
    ''' <param name="fechahasta">extremo final del intervalo de fechas</param>
    ''' <param name="UsuarioActuante">filtro por usuario actuante.</param>
    ''' <param name="usuarioafectado">filtro por usuario afectado</param>
    ''' <param name="CodigoEvento">filtro por codigo de evento</param>
    ''' <param name="dtsDataset">dataset que contendrá la tabla con el resultado de la consulta</param>
    ''' <param name="strNombreTabla">tabla que contendrá los datos resultantes de la consulta.</param>
    ''' <returns>cantida de filas devueltas por la consulta.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	08/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetornarLogAuditoria(ByVal fechadesde As DateTime, ByVal fechahasta As DateTime, ByVal UsuarioActuante As String, ByVal usuarioafectado As String, ByVal CodigoEvento As String, ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder


        'DONE:TODO:ANGEL --- Mira lo que hice!!! :p
        If objConexion.ConectadoA = "Oracle" Then
            sblSql.Append("Select sa.*, to_char(fechahoralog,'yyyymmdd') as fechacanonica ")
        ElseIf objConexion.ConectadoA = "SQLServer" Then
            sblSql.Append("Select sa.*, convert(char,fechahoralog,112)  as fechacanonica ")
        End If

        sblSql.Append(" from se_AUDITORIA sa WHERE 1=1 ")

        'fecha desde
        sblSql.Append(" AND fechahoralog  >= " & objConexion.XtoStr(fechadesde))
        'fecha hasta
        sblSql.Append(" AND fechahoralog <= " & objConexion.XtoStr(fechahasta.AddDays(1)))

        'usuario administrador
        ' chkUsuarioAdm.Value = 1 Then
        If (Not UsuarioActuante Is Nothing) AndAlso _
            (Not UsuarioActuante.Trim.Equals("(Todos)")) AndAlso _
            (Not UsuarioActuante.Trim.Equals("")) _
            AndAlso (Not UsuarioActuante.Trim.Equals("-99")) _
                Then _
                 sblSql.Append(" AND upper(USUARIOACTUANTE) = " & objConexion.XtoStr(UsuarioActuante.Trim().ToUpper()))

        'usuario afectado
        '    If chkUsuarioAfectado.Value = 1 Then
        If (Not usuarioafectado Is Nothing) AndAlso _
            (Not usuarioafectado.Trim.Equals("(Todos)")) AndAlso _
            (Not usuarioafectado.Trim.Equals("")) _
            AndAlso (Not usuarioafectado.Trim.Equals("-99")) _
                Then _
                sblSql.Append(" AND upper(USUARIOAFECTADO) = " & objConexion.XtoStr(usuarioafectado.ToUpper().Trim()))

        'codigo de mensaje
        If (Not CodigoEvento Is Nothing) AndAlso _
            (Not CodigoEvento.Trim.Equals("(Todos)")) AndAlso _
            (Not CodigoEvento.Trim.Equals("")) Then _
            sblSql.Append(" AND CODMENSAJE = " & CodigoEvento)

        sblSql.Append(" ORDER BY sa.FECHAHORALOG desc ")

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)

    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los códigos de auditoría
    ''' </summary>
    ''' <returns>DataSet que contiene los códigos de auditoría</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	23/09/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarCodigosAuditoria(ByVal dtsRet As DataSet) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'dtsRet : DataSet de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Return objConexion.Ejecutar("SELECT CodAuditoria, TextoAuditoria FROM SE_CodAuditoria", dtsRet, "SE_CodAuditoria")

    End Function

    ''' <summary>
    ''' Audita cambios realizados en la administración de la seguridad
    ''' </summary>
    ''' <param name="CodMensaje">Código de mensaje de auditoría</param>
    ''' <param name="Mensaje">Descripción de la auditoría</param>
    ''' <param name="Usuario">Usuario al cual se le aplica el cambio</param>
    ''' <param name="UsuarioActuante">Usuario que realiza el cambio</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
    ''' <remarks></remarks>
    Public Function Auditar(ByVal CodMensaje As Short, ByVal Mensaje As String, ByVal Usuario As String, ByVal UsuarioActuante As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("INSERT INTO SE_Auditoria(FechaHoraLog, CodMensaje, TextoMensaje, UsuarioActuante, UsuarioAfectado ) VALUES ( ")
            .Append(objConexion.XtoStr(objConexion.FechaServidor)) : .Append(","c) : .Append(CodMensaje) : .Append(","c)
            .Append(objConexion.XtoStr(Mensaje)) : .Append(","c)
            .Append(objConexion.XtoStr(UsuarioActuante)) : .Append(","c)
            .Append(objConexion.XtoStr(Usuario)) : .Append(")"c)
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function

    ''' <summary>
    ''' Recupera los mensajes de auditoría
    ''' </summary>
    ''' <param name="dtsDatos">DataSet de retorno de datos</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 10/03/2006 </history>
    Public Function RecuperarMensajesDeAuditoria(ByVal dtsDatos As System.Data.DataSet) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'intRet : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim intRet As Integer

        'intRet = RecuperarCodigosAuditoria(dtsDatos) 'objConexion.Ejecutar("SELECT CodAuditoria, TextoAuditoria FROM SE_CodAuditoria", dtsDatos, "SE_CodAuditoria")
        intRet += objConexion.Ejecutar("SELECT CodMensaje, TextoMensaje FROM SE_Mensajes ", dtsDatos, "grecMensajes")

        Return intRet

    End Function

#End Region

#Region "Parámetros"
    ''' <summary>
    ''' retorna el parametro de la columna cuatro
    ''' </summary>
    ''' <param name="dtsDataset">datsaet dodne se cargaran los datso</param>
    ''' <param name="strNombreTabla">nombre de la tabla en la que se almacenaran</param>
    ''' <returns>cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''  ????????     [????????]        Creado
    ''' </history>
    Public Function RetornarParametrosColumna4(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        Return objConexion.Ejecutar("SELECT Columna4 FROM SE_PARAMETROS", dtsDataset, strNombreTabla)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera las políticas generales
    ''' </summary>
    ''' <returns>DataSet con las políticas de seguridad</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	22/09/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarPoliticasGenerales() As System.Data.DataSet
        Dim dtsRet As New System.Data.DataSet

        objConexion.Ejecutar("SELECT Columna4, Columna5 FROM SE_PARAMETROS", dtsRet, "Sist_Parametros")

        Return dtsRet
    End Function

    ''' <summary>
    ''' Actualiza las políticas generales de CIPOL
    ''' </summary>
    ''' <param name="Parametro1">Valores de las plíticas generales</param>
    ''' <param name="Parametro2">Valores de las políticas generales</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history> Gustavo Mazzaglia - 28/02/2006 </history>
    Public Function ActualizarPoliticasGenerales(ByVal Parametro1 As String, ByVal Parametro2 As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("UPDATE SE_PARAMETROS SET Columna4 = ")
            .Append(objConexion.XtoStr(Parametro1))
            .Append(" , columna5 = ")
            .Append(objConexion.XtoStr(Parametro2))
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function


    ''' <summary>
    ''' Ingresa las políticas generales de CIPOL
    ''' </summary>
    ''' <param name="Parametro1">Valores de las políticas generales</param>
    ''' <param name="Parametro2">Valores de las políticas generales</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history> Gustavo Mazzaglia - 28/02/2006 </history>
    Public Function IngresarPoliticasGenerales(ByVal Parametro1 As String, ByVal Parametro2 As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("INSERT INTO SE_PARAMETROS(Columna4, Columna5) VALUES ( ")
            .Append(objConexion.XtoStr(Parametro1))
            .Append(","c)
            .Append(objConexion.XtoStr(Parametro2))
            .Append(")"c)

        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function
#End Region

#Region "Areas"
    ''' <summary>
    ''' Recupera las áreas que utilizan el sistema
    ''' </summary>
    ''' <param name="dtsDatos">DataSet en el cual se retornan las áreas</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
    Public Sub RecuperarAreas(ByRef dtsDatos As System.Data.DataSet)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("SELECT IdArea, NombreArea FROM SIST_KAREAS WHERE Baja = ")
            .Append(objConexion.XtoStr("N"c))
        End With

        objConexion.Ejecutar(sblSql.ToString, dtsDatos, "KAREAS")

    End Sub

    ''' <summary>
    ''' Retorna las areas de la tabla SIST_KAREAS
    ''' </summary>
    ''' <param name="dtsDataset">dataset donde se guardaran los datos</param>
    ''' <param name="strNombretabla">tabla en l que se almacenaran los datos</param>
    ''' <returns>cantidad de filas devueltas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	25/10/2005	Created
    '''     [MiguelP]         28/10/2014      GCP - Cambios 15598
    '''      Se agrego la clausula para que solo traiga las areas que no estan dadas de baja
    ''' </history>
    Public Function RecuperarAreasNOFicticias(ByVal dtsDataset As DataSet, ByVal strNombretabla As String) As Int32
        Return objConexion.Ejecutar("SELECT IdArea, NombreArea, baja FROM SIST_KAREAS " + _
                                    " where upper(rtrim(Ficticia))='N' AND upper(rtrim(baja))='N' ORDER BY NombreArea", dtsDataset, strNombretabla)
        'AND BAJA = 'N'
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los tipos de documentos
    ''' </summary>
    ''' <param name="dtsDataset">dataset donde se guardaran los datos</param>
    ''' <param name="strNombretabla">tabla en l que se almacenaran los datos</param>
    ''' <returns>cantidad de filas devueltas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	25/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarTiposDocumentos(ByVal dtsdataset As DataSet, ByVal strNombreTabla As String) As Int32
        Return objConexion.Ejecutar("SELECT IdTipoDoc, TipoAbreviado FROM SIST_KDOCUMENTOS WHERE DocBaja = 'N' ORDER BY TipoAbreviado", dtsdataset, strNombreTabla)
    End Function

#End Region

#Region "Sistemas"

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
    Public Sub ActualizarTipoSeguridad(ByVal TipoSeguridad As String)

        objConexion.Ejecutar("UPDATE SE_SIST_HABILITADOS SET Observaciones = " & objConexion.XtoStr(TipoSeguridad) & " WHERE IDSistema = 1")

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los datos de los sistemas habilitados en cipol
    ''' </summary>
    ''' <param name="dtsDataset">dataset que contendrá la información</param>
    ''' <param name="strNombreTabla">tabla donde se guardarán los datos.</param>
    ''' <returns>cantidad de filas que devolvió la consulta.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	03/11/2005	Created
    ''' [LucianoP]          [jueves, 24 de enero de 2008]       GCP-Cambios ID: 6409
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetornarSistemasHabilitados(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT IDSISTEMA, CODSISTEMA, DESCSISTEMA, FECHAHABILITACION, NOMBREEXEC,")
        sblSql.Append("       SISTEMAHABILITADO, ICONO, OBSERVACIONES, PAGINAPORDEFECTO, DESCRIPCIONCORTA, IMPACTACAJA")
        sblSql.Append(" FROM SE_SIST_HABILITADOS")

        '[MiguelP]          22/10/2014      Cambio GCP - Se modifico el ordenamiento de la consulta
        'sblSql.Append(" ORDER BY DESCSISTEMA")
        sblSql.Append(" ORDER BY IDSISTEMA")

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)

    End Function

    ''' <summary>
    ''' Inserta un sistema habilitado
    ''' </summary>
    ''' <param name="rowDatos">DataRow con los datos de nuevo sistema</param>
    ''' <returns>Numero de filas afectadas en la operacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [viernes, 25 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' [AndresR]           [lunes, 30 de junio de 2008]         Modificado
    ''' </history>
    Public Function InsertarSistemaHabilitado(ByVal rowDatos As System.Data.DataRow) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql : StringBuilder que contiene la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append(" INSERT INTO SE_SIST_HABILITADOS (IDSISTEMA, CODSISTEMA, ")
            .Append(" DESCSISTEMA, FECHAHABILITACION, NOMBREEXEC, SISTEMAHABILITADO, ICONO, ")
            .Append(" PAGINAPORDEFECTO)")
            .Append(" VALUES (")
            .Append(rowDatos.Item("IDSISTEMA").ToString & ", ")
            .Append(objConexion.XtoStr(rowDatos.Item("CODSISTEMA").ToString) & ", ")
            .Append(objConexion.XtoStr(rowDatos.Item("DESCSISTEMA").ToString) & ", ")
            If Not rowDatos.IsNull("FECHAHABILITACION") Then
                .Append(objConexion.XtoStr(CDate(rowDatos.Item("FECHAHABILITACION").ToString)) & ", ")
            Else
                .Append("NULL, ")
            End If
            If Not rowDatos.IsNull("NOMBREEXEC") Then
                If rowDatos.Item("NOMBREEXEC").ToString.Trim() = "" Then
                    .Append("' ', ")
                Else
                    .Append(objConexion.XtoStr(rowDatos.Item("NOMBREEXEC").ToString) & ", ")
                End If
            Else
                .Append("' ', ")
            End If
            If Not rowDatos.IsNull("SISTEMAHABILITADO") Then
                .Append(objConexion.XtoStr(rowDatos.Item("SISTEMAHABILITADO").ToString) & ", ")
            Else
                .Append("NULL, ")
            End If
            If Not rowDatos.IsNull("ICONO") Then
                .Append(objConexion.XtoStr(rowDatos.Item("ICONO").ToString) & ", ")
            Else
                .Append("NULL, ")
            End If
            'No se manipula por medio del sistema
            'If Not rowDatos.IsNull("OBSERVACIONES") Then
            '    .Append(objConexion.XtoStr(rowDatos.Item("OBSERVACIONES").ToString) & ", ")
            'Else
            '    .Append("NULL, ")
            'End If
            If Not rowDatos.IsNull("PAGINAPORDEFECTO") Then
                .Append(objConexion.XtoStr(rowDatos.Item("PAGINAPORDEFECTO").ToString))
            Else
                .Append("NULL ")
            End If
            'If Not rowDatos.IsNull("DESCRIPCIONCORTA") Then
            '    .Append(objConexion.XtoStr(rowDatos.Item("DESCRIPCIONCORTA").ToString) & " ")
            'Else
            '    .Append("NULL ")
            'End If
            'No se manipula por medio del sistema
            'If Not rowDatos.IsNull("IMPACTACAJA") Then
            '    .Append(objConexion.XtoStr(rowDatos.Item("IMPACTACAJA").ToString) & ", ")
            'Else
            '    .Append("NULL, ")
            'End If
            .Append(")")
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function

    ''' <summary>
    ''' Actualiza los datos del sistema habilitado
    ''' </summary>
    ''' <param name="rowDatos">DataRow con los datos actualizados del sistema</param>
    ''' <returns>Numero de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [viernes, 25 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' </history>
    Public Function ActualizarSistemaHabilitado(ByVal rowDatos As System.Data.DataRow) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql : StringBuilder que contiene la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("UPDATE SE_SIST_HABILITADOS SET ")
            .Append(" CODSISTEMA = " & objConexion.XtoStr(rowDatos.Item("CODSISTEMA").ToString) & ", ")
            .Append(" DESCSISTEMA = " & objConexion.XtoStr(rowDatos.Item("DESCSISTEMA").ToString) & ", ")
            If Not rowDatos.IsNull("FECHAHABILITACION") Then
                .Append(" FECHAHABILITACION = " & objConexion.XtoStr(CDate(rowDatos.Item("FECHAHABILITACION").ToString)) & ", ")
            Else
                .Append(" FECHAHABILITACION = NULL, ")
            End If
            If Not rowDatos.IsNull("NOMBREEXEC") Then
                .Append(" NOMBREEXEC = " & objConexion.XtoStr(rowDatos.Item("NOMBREEXEC").ToString) & ", ")
            Else
                .Append(" NOMBREEXEC = NULL,")
            End If
            If Not rowDatos.IsNull("SISTEMAHABILITADO") Then
                .Append(" SISTEMAHABILITADO = " & objConexion.XtoStr(rowDatos.Item("SISTEMAHABILITADO").ToString) & ", ")
            Else
                .Append(" SISTEMAHABILITADO = NULL,")
            End If
            If Not rowDatos.IsNull("ICONO") Then
                .Append(" ICONO = " & objConexion.XtoStr(rowDatos.Item("ICONO").ToString) & ", ")
            Else
                .Append(" ICONO = NULL,")
            End If
            'No se manipula por medio del sistema
            'If Not rowDatos.IsNull("OBSERVACIONES") Then
            '    .Append(" OBSERVACIONES = " & objConexion.XtoStr(rowDatos.Item("OBSERVACIONES").ToString) & ", ")
            'Else
            '    .Append(" OBSERVACIONES = NULL, ")
            'End If
            If Not rowDatos.IsNull("PAGINAPORDEFECTO") Then
                .Append(" PAGINAPORDEFECTO = " & objConexion.XtoStr(rowDatos.Item("PAGINAPORDEFECTO").ToString))
            Else
                .Append(" PAGINAPORDEFECTO = NULL ")
            End If
            'If Not rowDatos.IsNull("DESCRIPCIONCORTA") Then
            '    .Append(" DESCRIPCIONCORTA = " & objConexion.XtoStr(rowDatos.Item("DESCRIPCIONCORTA").ToString) & ", ")
            'Else
            '    .Append(" DESCRIPCIONCORTA = NULL, ")
            'End If
            'No se manipula por medio del sistema
            'If Not rowDatos.IsNull("IMPACTACAJA") Then
            '    .Append(" IMPACTACAJA = " & objConexion.XtoStr(rowDatos.Item("IMPACTACAJA").ToString))
            'Else
            '    .Append(" IMPACTACAJA = NULL")
            'End If
            .Append(" WHERE IDSISTEMA = " & rowDatos.Item("IDSISTEMA").ToString)
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function

    ''' <summary>
    ''' Elimina un sistema habilitado
    ''' </summary>
    ''' <returns>Numero de filas afectadas en la operacin</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [viernes, 25 de enero de 2008]       Creado GCP-Cambios ID: 6409
    ''' </history>
    Public Function EliminarSistemaHabilitado(ByVal IDSistema As Integer) As Integer
        Return objConexion.Ejecutar("DELETE SE_SIST_HABILITADOS WHERE IDSISTEMA = " & IDSistema.ToString())
    End Function

    ''' <summary>
    ''' Verifica la existencia de una Tarea Primitiva
    ''' </summary>
    ''' <param name="IDSistema">Identificador del Sistema</param>
    ''' <returns>True si existe, False caso contrario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [lunes, 07 de julio de 2008]       Creado
    ''' </history>
    Public Function VerificarExistenciaTareaPrimitiva(ByVal IDSistema As Integer) As Boolean
        Return CType(objConexion.EjecutarEscalar("SELECT COUNT(IDSISTEMA) AS Tareas FROM SE_TAREAS WHERE IDSISTEMA = " & IDSistema.ToString()), Integer) > 0
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los datos de los sistemas habilitados en cipol
    ''' </summary>
    ''' <param name="pidUsuario">identificador del usuario a quien se 
    ''' le retornarnan los sistemas </param>
    ''' <param name="dtsDataset">dataset que contendrá la información</param>
    ''' <param name="strNombreTabla">tabla donde se guardarán los datos.</param>
    ''' <returns>cantidad de filas que devolvió la consulta.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	03/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetornarSistemasHabilitados(ByVal pidUsuario As Int32, ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT DISTINCT DescSistema, NombreExec, Icono, SE_Sist_Habilitados.idsistema ")
        sblSql.Append(" FROM SE_Sist_Habilitados, SE_Tareas, SE_Tareas_Usuario ")
        sblSql.Append(" WHERE SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema ")
        sblSql.Append("         AND SE_Tareas.IdTarea = SE_Tareas_Usuario.IdTarea ")
        sblSql.Append("         AND IdUsuario = " + objConexion.XtoStr(pidUsuario))
        sblSql.Append("         AND SistemaHabilitado = 'S' ORDER BY DescSistema")


        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los datos del sistema Cipol Administrador.
    ''' </summary>
    ''' <param name="dtsDataset">dataset que contendrá la información</param>
    ''' <param name="strNombreTabla">tabla donde se guardarán los datos.</param>
    ''' <returns>cantidad de filas que devolvió la consulta.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	03/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetornarSistemaAdministrador(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT DescSistema, NombreExec, Icono, SE_Sist_Habilitados.idsistema ")
        sblSql.Append(" FROM SE_Sist_Habilitados WHERE IDSistema = 1 ")

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)
    End Function

    ''' <summary>
    ''' Recupera el id. y el nombre de los sistemas habilitados
    ''' </summary>
    ''' <param name="dtsDatos">DataSet de retorno</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>Gustavo Mazzaglia - 01/03/2005 
    ''' [LucianoP]          [martes, 29 de enero de 2008]       GCP-Cambios ID: 6409 - Se agrego CODSISTEMA
    ''' </history>
    Public Sub RecuperarSistemasHabilitados(ByRef dtsDatos As System.Data.DataSet)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT IdSistema, DescSistema,CODSISTEMA FROM SE_Sist_Habilitados WHERE SistemaHabilitado = 'S' ORDER BY DescSistema", dtsDatos, "SE_SIST_HABILITADOS")

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' retorna una lista de las sesiones activas
    ''' </summary>
    ''' <param name="dtsDataset">dataset donde se guardará la tabla resultante de la consulta</param>
    ''' <param name="strNombreTabla">tabla que contendrá los datos resultantes de la consulta.</param>
    ''' <returns>cantidad de filas de la tabla.</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [angell]	11/11/2005	Created
    ''' [AndresR]   [jueves, 01 de noviembre de 2007]       GCP-Cambios ID: 5998
    ''' </history>
    Public Function RetornarSesionesActivas(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql

            .Append("SELECT SeA.strUsuario as Usuario, SeA.NombreNetBios as Terminal, ")
            .Append(" Usu.IDUSUARIO, Usu.NOMBRES, Usu.IDAREA ,KAr.NOMBREAREA, SeA.INICIOTAREA ")
            .Append(" FROM SE_SESIONESACTIVAS SeA ")

            If Me.objConexion.ConectadoA.ToLower() = "oracle" Then
                .Append(" INNER JOIN SE_USUARIOS Usu on UPPER(LTRIM(RTRIM(SeA.strUsuario))) = UPPER(LTRIM(RTRIM(Usu.USUARIO))) ")
            Else
                .Append(" INNER JOIN SE_USUARIOS Usu on SeA.strUsuario = Usu.USUARIO ")
            End If
            .Append(" INNER JOIN SIST_KAREAS KAr on Usu.IDAREA = KAr.IDAREA ")
            .Append(" ORDER BY strUsuario ")

        End With

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' elimina un registrod e la tabla de sesiones activas
    ''' </summary>
    ''' <param name="pUsuario">usuario a identificar</param>
    ''' <param name="pTerminal">terminal a identificar.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	11/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function EliminarSesionActiva(ByVal pUsuario As String, ByVal pTerminal As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de sentencia sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" DELETE FROM SE_SESIONESACTIVAS ")
        sblSql.Append(" WHERE  SE_SESIONESACTIVAS.STRUSUARIO = " + objConexion.XtoStr(pUsuario))
        sblSql.Append("     AND SE_SESIONESACTIVAS.NOMBRENETBIOS = " + objConexion.XtoStr(pTerminal))

        Return objConexion.Ejecutar(sblSql.ToString())
    End Function
#End Region

#Region "Reportes"

    'RECUPERA las tareas relacionadas con usuarios
    '
    'PARAMETROS DE ENTRADA:
    '   precRecordset           : recordset donde se cargaran los datos
    '   FiltroUsuario           : filtro para el usuario
    '   FiltroRol               : filtro por roles
    '   FiltroTarea             : filtro por identificador de taeas
    '
    'PARAMETROS DE SALIDA:
    '   Retorna la cantidad de tareas NO INHIBIDAS retornadas por la consulta
    '''<remarks> Listo! </remarks>
    Public Function RetornarTareasXUsuarios(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String, Optional ByVal FiltroUsuario As Integer = -1, Optional ByVal FiltroRol As Integer = -1, Optional ByVal FiltroTarea As Integer = -1) As Int32
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'AUTOR: ANGEL FERNANDO LUBENOV
        'FECHA: 30/08/2005
        'MODIFICACIONES:
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        'objCipol       : objeto de seguridad cipol
        'strSql         : String de consulta sql
        'intRetorno     : valor que retornará la función.
        'intI           : contador para el for
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strSql As String
        Dim intRetorno As Int32
        Dim intI As Int32
        Dim objCipl As EntidadesEmpresariales.PadreCipolCliente = CType(Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)

        strSql = " SELECT *"
        strSql = strSql & " FROM se_tareas_usuario"
        strSql = strSql & " WHERE 1=1"
        'se filtra por rol si fuese necesario
        If FiltroRol <> -1 Then strSql = strSql & "      AND idRol = " & objConexion.XtoStr(FiltroRol)
        'se filtra por usuario si fuere neesario
        If FiltroUsuario <> -1 Then strSql = strSql & "      AND idusuario = " & objConexion.XtoStr(FiltroUsuario)
        'se filtra por tarea si fuere necesario
        If FiltroTarea <> -1 Then strSql = strSql & "      AND idtarea = " & objConexion.XtoStr(FiltroTarea)
        'ejecutamos la consulta
        objConexion.Ejecutar(strSql, dtsDataset, strNombreTabla)

        'DESENCRIPTAMOS el indicador sobre la tarea inhibida/desinibida
        For intI = 0 To dtsDataset.Tables(strNombreTabla).Rows.Count - 1
            'si no fue desencriptada todavia.
            If Not (dtsDataset.Tables(strNombreTabla).Rows(intI)("TareaInhibida").ToString().Equals("S") OrElse dtsDataset.Tables(strNombreTabla).Rows(intI)("TareaInhibida").ToString().Equals("N")) Then
                With dtsDataset.Tables(strNombreTabla)
                    .Rows(intI)("TareaInhibida") = (New COA.CifrarDatos.TresDES(objCipl.IV, objCipl.Key)).Criptografia(COA.CifrarDatos.Accion.Desencriptacion, .Rows(intI)("TareaInhibida").ToString())
                End With
            End If
        Next intI


        'si no hay tareas, retonamos cero
        If dtsDataset.Tables(strNombreTabla).Rows.Count.Equals(0) Then
            Return 0
        End If
        'si hay tareas, filtramos por TareaInhibbida = "N"
        'y retornamo la cantidad de registros al respecto
        dtsDataset.Tables(strNombreTabla).DefaultView.RowFilter = "TareaInhibida = 'N' "
        intRetorno = dtsDataset.Tables(strNombreTabla).DefaultView.Count
        dtsDataset.Tables(strNombreTabla).DefaultView.RowFilter = ""

        Return intRetorno
    End Function
    'RETORNA LOS ROLES RELACIONADOS CON USUARIOS:
    '
    'PARAMETROS DE ENTRADA:
    '   dtsDataset              : dataset donde se cargara la informacion
    '   strNombreTabla          : nombre de la tabla donde se guardaran los datos de la consulta.
    '   FiltroRol               : filtro por roles (opcional)
    '   FiltroUsuario           : filtro por usuarios (opcional)
    '
    'PARAMETROS DE SALIDA:
    '   (N/A)
    ''' <history> 
    ''' [AndresR]          [jueves, 01 de noviembre de 2007]       Modificado GCP-Cambios ID: 5997
    ''' </history>
    Public Sub RetornarRolesXUsuarios(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String, Optional ByVal FiltroRol As Integer = -1, Optional ByVal FiltroUsuario As Integer = -1)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'AUTOR: ANGEL FERNANDO LUBENOV
        'FECHA: 30/08/2005
        'MODIFICACIONES:
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        '
        'strSql         : String de consulta sql
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" SELECT * FROM vRetornarRolesXUsuarios ")
        sblSql.Append(" WHERE 1 = 1 ")

        'si fuese necesario, filtramos por ROL"
        If FiltroRol <> -1 Then sblSql.Append("          AND idrol = " & objConexion.XtoStr(FiltroRol))
        'si fuese necesario, filtramos por USUARIO
        If FiltroUsuario <> -1 Then sblSql.Append("          AND idusuario = " & objConexion.XtoStr(FiltroUsuario))

        sblSql.Append("  ORDER BY nombres,descripcionperfil	 ")

        'ejecutamos la consulta
        objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)


        Dim intI As Int32
        For intI = 0 To dtsDataset.Tables(strNombreTabla).Rows.Count - 1
            With dtsDataset.Tables(strNombreTabla).Rows(intI)
                'generamos la especificacion del documento
                If Not .IsNull("nrodocumento") AndAlso Not .IsNull("idtipodocdesc") Then
                    .Item("nrodocumento") = .Item("idtipodocdesc").ToString.Trim() + " " + .Item("nrodocumento").ToString().Trim()
                End If

                'agregamos los datos para la columna CantTareasRol
                .Item("CantTareasRol") = objConexion.EjecutarEscalar("SELECT COUNT(*) FROM SE_COMP_ROLES escr WHERE escr.idrol= " + .Item("IdRol").ToString())
                .Item("Completo") = IIf(ToInt32(.Item("CantTareasRol")) = ToInt32(.Item("CanTareasUsuarioRol")), "S", "N")
                Dim strSql As String = " Select usuario FROM SE_TAREAS_USUARIO tstu, SE_USUARIOS su " + _
                                                                    " WHERE tstu.idusuario = " + .Item("IdUsuario").ToString() + _
                                                                    " and tstu.idusuarioultmodif = su.idusuario " + _
                                                                    " AND tstu.idrol = " + .Item("IdRol").ToString() + _
                                                                    " ORDER BY tstu.fechaultmodif DESC, tstu.idusuarioultmodif DESC "
                'agregamos los datos de la columna UsuarioModif
                .Item("usuariomodif") = objConexion.EjecutarEscalar(strSql)
            End With
        Next
    End Sub

    ''' <summary>
    ''' retorna el detalle de Usuarios por Rol
    ''' </summary>
    ''' <param name="IDRol">ID de Rol a filtrar</param>
    ''' <param name="DataSetRta">DataSet de respuesta</param>
    ''' <returns>DataSet: dtsRolesXUsuario Tabla: UsuariosXRolDetalle</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [miércoles, 14 de noviembre de 2012] Creado GCP-Cambios ID:12852
    ''' </history>
    Public Function RetornarUsuariosXRolDetalle(ByVal IDRol As Int32, ByRef DataSetRta As Data.DataSet) As Int32
        Dim sblSQL As New System.Text.StringBuilder
        Dim objCipl As EntidadesEmpresariales.PadreCipolCliente = CType(Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        Dim strTareaInhibida As String
        strTareaInhibida = (New COA.CifrarDatos.TresDES(objCipl.IV, objCipl.Key)).Criptografia(COA.CifrarDatos.Accion.Encriptacion, "N")

        sblSQL.Append("SELECT DISTINCT U.IDUSUARIO, U.USUARIO, U.NOMBRES, U.NRODOCUMENTO")
        sblSQL.Append(",(Select SU.USUARIO From SE_USUARIOS SU Where SU.IDUSUARIO = TAR.IDUSUARIOULTMODIF) AS AsignadoPor")
        sblSQL.Append(",TAR.FECHAULTMODIF ,U.FECHABAJA ,KA.NOMBREAREA ,ROLES.IDTAREA ,ST.DESCRIPCIONTAREA")
        sblSQL.Append(",CASE WHEN (Select Count(*) From SE_TAREAS_USUARIO T Where T.IDUSUARIO = U.IDUSUARIO")
        sblSQL.Append("            And T.IDROL = TAR.IDROL And T.IDTAREA = ROLES.IDTAREA) > 0")
        sblSQL.Append(" THEN 'SI' ELSE 'NO' END AS TieneAsignada")
        sblSQL.Append(",CASE WHEN ((Select Count(*) From SE_TAREAS_USUARIO T Where T.IDUSUARIO = u.IDUSUARIO")
        sblSQL.Append("              And (T.TAREAINHIBIDA=").Append(objConexion.XtoStr(strTareaInhibida)).Append(")")
        sblSQL.Append("              And T.IDROL = TAR.IDROL) = (Select Count(*) From SE_COMP_ROLES R Where R.IDROL = TAR.IDROL)) ")
        sblSQL.Append("  THEN 'Completo' ELSE 'Incompleto' END AS Uso")
        'sblSQL.Append(",CASE WHEN (TAR.TAREAINHIBIDA=").Append(objConexion.XtoStr(strTareaInhibida)).Append(")")
        'sblSQL.Append(" THEN 'NO' ELSE 'SI' END AS TareaInhibida")
        sblSQL.Append(",(Select Case When (TAR.TAREAINHIBIDA=").Append(objConexion.XtoStr(strTareaInhibida)).Append(")")
        sblSQL.Append("  Then 'NO'  Else 'SI'")
        sblSQL.Append("  End As TAREAINHIBIDA")
        sblSQL.Append("  From SE_TAREAS_USUARIO TAR Where TAR.IDTAREA = ROLES.IDTAREA And TAR.IDUSUARIO = U.IDUSUARIO) AS TareaInhibida")
        sblSQL.Append(" FROM SE_USUARIOS U")
        sblSQL.Append(" LEFT JOIN SE_TAREAS_USUARIO TAR ON TAR.IDUSUARIO = U.IDUSUARIO")
        sblSQL.Append(" INNER JOIN SE_COMP_ROLES ROLES ON ROLES.IDROL = TAR.IDROL")
        sblSQL.Append(" INNER JOIN SE_TAREAS ST ON ST.IDTAREA = ROLES.IDTAREA")
        sblSQL.Append(" INNER JOIN SIST_KAREAS KA ON KA.IDAREA=U.IDAREA")
        sblSQL.Append(" WHERE U.IDUSUARIO IN (Select Distinct IDUSUARIO From SE_TAREAS_USUARIO Where IDROL = ").Append(objConexion.XtoStr(IDRol)).Append(")")
        sblSQL.Append(" AND TAR.IDROL = ").Append(objConexion.XtoStr(IDRol))
        sblSQL.Append(" ORDER BY NOMBRES,DESCRIPCIONTAREA")

        Return objConexion.Ejecutar(sblSQL.ToString(), DataSetRta, "UsuariosXRolDetalle")
    End Function

    ''' <summary>
    ''' Retorna los datos del Usuario para el reporte de Roles x Usuario
    ''' </summary>
    ''' <param name="IDUsuario">ID de Usuario a filtrar</param>
    ''' <param name="DataSetRta">Dataset con los datos recuperados</param>
    ''' <returns>Cantidad de registros recuperados</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [jueves, 15 de noviembre de 2012] Creado GCP-Cambios ID:12853
    ''' </history>
    Public Function RetornarRolesXUsuarioDatosUsr(ByVal IDUsuario As Int32, ByRef DataSetRta As DataSet) As Int32
        Dim sblSQL As New System.Text.StringBuilder
        sblSQL.Append("SELECT SU.NOMBRES, '' AS DESCRIPCIONPERFIL, 0 AS CANTTAREASROL, 0 AS CANTAREASUSUARIOROL")
        sblSQL.Append(",SU.IDUSUARIO ,0 AS IDROL, ' ' AS COMPLETO, SU.FECHAALTA, SU.FECHABAJA")
        sblSQL.Append(",KD.TIPOABREVIADO AS IDTIPODOCDESC ,SU.NRODOCUMENTO ,SU.USUARIO, TAR.FECHAULTMODIF AS FECHAAMODIFICACION")
        sblSQL.Append(",' ' AS USUARIOMODIF, SU.IDAREA, KA.NOMBREAREA")
        sblSQL.Append(" FROM SE_USUARIOS SU INNER JOIN SE_TAREAS_USUARIO TAR ON SU.IDUSUARIO = TAR.IDUSUARIO")
        sblSQL.Append(" INNER JOIN SIST_KDOCUMENTOS KD ON KD.IDTIPODOC = SU.IDTIPODOC ")
        sblSQL.Append(" INNER JOIN SIST_KAREAS KA ON KA.IDAREA =SU.IDAREA ")
        sblSQL.Append(" WHERE SU.IDUSUARIO = ").Append(objConexion.XtoStr(IDUsuario))
        Return objConexion.Ejecutar(sblSQL.ToString(), DataSetRta, "RolesXUsuarios")
    End Function
    ''' <summary>
    ''' Retorna los datos del Rol para el reporte de Usuarios x Rol
    ''' </summary>
    ''' <param name="IDRol">ID de Rol a filtrar</param>
    ''' <param name="DataSetRta">Dataset con los datos recuperados</param>
    ''' <returns>Cantidad de registros recuperados</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [jueves, 15 de noviembre de 2012] Creado GCP-Cambios ID:12852
    ''' </history>
    Public Function RetornarUsuariosXRolDatosRol(ByVal IDRol As Int32, ByRef DataSetRta As DataSet) As Int32
        Dim sblSQL As New System.Text.StringBuilder
        sblSQL.Append("SELECT ' ' AS NOMBRES ,DESCRIPCIONPERFIL ,0 AS CANTTAREASROL ,0 AS CANTAREASUSUARIOROL")
        sblSQL.Append(",IDUSUARIOULTMODIF AS IDUSUARIO ,IDROL ,' ' AS COMPLETO ,FECHAULTMODIF AS FECHAALTA ,NULL AS FECHABAJA")
        sblSQL.Append(",' ' AS IDTIPODOCDESC ,' ' AS NRODOCUMENTO ,' ' AS USUARIO ,FECHAULTMODIF AS FECHAAMODIFICACION")
        sblSQL.Append(",' ' AS USUARIOMODIF ,0 AS IDAREA ,' ' AS NOMBREAREA")
        sblSQL.Append(" FROM SE_ROLES")
        sblSQL.Append(" WHERE IDROL = ").Append(objConexion.XtoStr(IDRol))
        Return objConexion.Ejecutar(sblSQL.ToString(), DataSetRta, "RolesXUsuarios")
    End Function

    ''' <summary>
    ''' Retorna el detalle de Roles por Usuario
    ''' </summary>
    ''' <param name="IDUsuario">ID Usuario a filtrar</param>
    ''' <param name="DataSetRta">DataSet de respuesta</param>
    ''' <returns>DataSet: dtsRolesXUsuario Tabla: RolesXUsuarioDetalle</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [miércoles, 14 de noviembre de 2012] Creado GCP-Cambios ID:12853
    ''' </history>
    Public Function RetornarRolesXUsuarioDetalle(ByVal IDUsuario As Int32, ByRef DataSetRta As DataSet) As Int32
        Dim sblSQL As New System.Text.StringBuilder
        Dim objCipl As EntidadesEmpresariales.PadreCipolCliente = CType(Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        Dim strTareaInhibida As String
        strTareaInhibida = (New COA.CifrarDatos.TresDES(objCipl.IV, objCipl.Key)).Criptografia(COA.CifrarDatos.Accion.Encriptacion, "N")
        sblSQL.Append("SELECT CR.IDTAREA, ST.DESCRIPCIONTAREA, CR.IDROL, ROL.DESCRIPCIONPERFIL,")
        sblSQL.Append(objConexion.XtoStr(IDUsuario)).Append(" AS IDUSUARIO")
        sblSQL.Append(",CASE WHEN (Select Count(*) From SE_TAREAS_USUARIO T Where T.IDUSUARIO = ").Append(objConexion.XtoStr(IDUsuario))
        sblSQL.Append("             And T.IDROL = CR.IDROL And T.IDTAREA = CR.IDTAREA) > 0")
        sblSQL.Append(" THEN 'SI'")
        sblSQL.Append(" ELSE 'NO'")
        sblSQL.Append(" END AS TieneAsignada")
        sblSQL.Append(",(Select Case When (TAR.TAREAINHIBIDA=").Append(objConexion.XtoStr(strTareaInhibida)).Append(")")
        sblSQL.Append("  Then 'NO'")
        sblSQL.Append("  Else 'SI' End As TAREAINHIBIDA")
        sblSQL.Append("  From SE_TAREAS_USUARIO TAR")
        sblSQL.Append("  Where TAR.IDTAREA = CR.IDTAREA And TAR.IDUSUARIO = ").Append(objConexion.XtoStr(IDUsuario))
        sblSQL.Append(" ) AS TareaInhibida")
        sblSQL.Append(",CASE WHEN ((Select Count(*) From SE_TAREAS_USUARIO T Where T.IDUSUARIO = ").Append(objConexion.XtoStr(IDUsuario))
        sblSQL.Append("             And (T.TAREAINHIBIDA=").Append(objConexion.XtoStr(strTareaInhibida)).Append(")")
        sblSQL.Append("             And T.IDROL = CR.IDROL) = (Select Count(*) From SE_COMP_ROLES R Where R.IDROL = CR.IDROL))")
        sblSQL.Append(" THEN 'Completo' ELSE 'Incompleto' END AS Uso")
        sblSQL.Append(" FROM SE_COMP_ROLES CR")
        sblSQL.Append(" INNER JOIN SE_ROLES ROL ON ROL.IDROL = CR.IDROL")
        sblSQL.Append(" INNER JOIN SE_TAREAS ST ON ST.IDTAREA = CR.IDTAREA")
        sblSQL.Append(" WHERE CR.IDROL IN (SELECT DISTINCT IDROL FROM SE_TAREAS_USUARIO WHERE IDUSUARIO = ").Append(objConexion.XtoStr(IDUsuario)).Append(")")
        sblSQL.Append(" ORDER BY DESCRIPCIONPERFIL, DESCRIPCIONTAREA")

        Return objConexion.Ejecutar(sblSQL.ToString(), DataSetRta, "RolesXUsuarioDetalle")
    End Function


    ''' <summary>
    ''' Retorna los Usuarios por Área
    ''' </summary>
    ''' <param name="dtsDataset">Dataset el cual se llena con los datos recuperados</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [viernes, 02 de noviembre de 2007]       Creado GCP-Cambios ID: 6001
    ''' </history>
    Public Sub RetornarUsuariosXAreas(ByVal IdArea As Int32, ByVal dtsDataset As DataSet)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        'strSql         : String de consulta sql
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql

            .Append("SELECT ")
            .Append("   su.idusuario, ")
            .Append("   su.Usuario , ")
            .Append("   su.nombres, ")
            .Append("   su.fechaalta,  ")
            .Append("   su.fechabaja, ")
            .Append("   su.nrodocumento, ")
            .Append("   kd.tipoabreviado, ")
            .Append("   su.idarea, ")
            .Append("   ka.nombrearea, ")
            .Append("   ka.baja ")
            .Append(" FROM ")
            .Append("   SE_USUARIOS su, ")
            .Append("   SIST_KDOCUMENTOS kd, ")
            .Append("   SIST_KAREAS ka ")
            .Append(" WHERE ")
            .Append("   su.idarea = ka.idarea ")
            .Append("   AND su.idtipodoc = kd.idtipodoc ")
            If IdArea <> -99 Then .Append("   AND ka.idarea = " & IdArea.ToString)
            .Append(" ORDER BY su.nombres ")

        End With

        objConexion.Ejecutar(sblSql.ToString(), dtsDataset, "UsuariosXAreas")

    End Sub

    ''' <summary>
    ''' Recupera los sistemas y usuarios inhabilitados
    ''' </summary>
    ''' <param name="dtsDatos">DataSet de retorno</param>
    ''' <param name="IDSistema">Identificador del Sistema</param>
    ''' <param name="IDUsuario">Identificador del Usuario</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [martes, 06 de mayo de 2008]       GCP-Cambios ID: 5814
    ''' </history>
    Public Sub RecuperarSistBloqueados(ByRef dtsDatos As System.Data.DataSet, Optional ByVal IDSistema As Short = -1, Optional ByVal IDUsuario As Short = -1)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        With sblsql
            .Append("SELECT ")
            .Append("    BLO.IDSistema, HAB.DESCSISTEMA, BLO.IDUsuario, USU.NOMBRES ")
            .Append("FROM ")
            .Append("    SE_SIST_BLOQUEADOS BLO ")
            .Append("    INNER JOIN SE_USUARIOS USU ON USU.IDUSUARIO = BLO.IDUSUARIO ")
            .Append("    INNER JOIN SE_SIST_HABILITADOS HAB ON HAB.IDSISTEMA = BLO.IDSISTEMA ")
            .Append("WHERE 1=1 ")
            If IDSistema >= 0 Then .Append("    AND IDSISTEMA = " & IDSistema.ToString)
            If IDUsuario >= 0 Then .Append("    AND IDUSUARIO = " & IDUsuario.ToString)
            .Append(" ORDER BY HAB.DESCSISTEMA")
        End With

        objConexion.Ejecutar(sblsql.ToString, CType(dtsDatos, System.Data.DataSet), "SE_SIST_BLOQUEADOS")

    End Sub

    ''' <summary>
    ''' Inserta un sistema bloqueado
    ''' </summary>
    ''' <param name="rowDatos">DataRow con los datos de nuevo sistema</param>
    ''' <returns>Numero de filas afectadas en la operacion</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [viernes, 09 de mayo de 2008]       Creado GCP-Cambios ID: 5814
    ''' </history>
    Public Function InsertarSistBloqueado(ByVal rowDatos As System.Data.DataRow) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'sblSql : StringBuilder que contiene la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim IDUsuario As Integer = 0

        Dim objUsuarioCipol As EntidadesEmpresariales.PadreCipolCliente

        If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            objUsuarioCipol = CType(System.Web.HttpContext.Current.Session("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        Else
            objUsuarioCipol = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        End If

        With sblSql

            .Append("INSERT INTO SE_SIST_BLOQUEADOS ( ")
            .Append("    IDSISTEMA, IDUSUARIO, ")
            .Append("    FECHAULTMODIF, IDUSUARIOULTMODIF) ")
            .Append("VALUES ( ")
            .Append(rowDatos.Item("IDSISTEMA").ToString & ", ")
            .Append(rowDatos.Item("IDUSUARIO").ToString & ", ")
            .Append(objConexion.XtoStr(objConexion.FechaServidor) & ", ")
            .Append(objConexion.XtoStr(IDUsuario))
            .Append(") ")
        End With

        Return objConexion.Ejecutar(sblSql.ToString)

    End Function

    ''' <summary>
    ''' Elimina un sistema bloqueado
    ''' </summary>
    ''' <returns>Numero de filas afectadas en la operacin</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [viernes, 09 de mayo de 2008]       GCP-Cambios ID: 5814
    ''' </history>
    Public Function EliminarSistBloqueado(ByVal IDSistema As Integer, ByVal IDUsuario As Integer) As Integer

        Dim sblsql As New System.Text.StringBuilder

        With sblsql

            .Append("DELETE FROM SE_SIST_BLOQUEADOS WHERE 1=1 ")

            If IDSistema >= 0 Then .Append(" AND IDSISTEMA = " & IDSistema.ToString)
            If IDUsuario >= 0 Then .Append(" AND IDUSUARIO = " & IDUsuario.ToString)

        End With

        Return objConexion.Ejecutar(sblsql.ToString())

    End Function


#End Region
End Class
