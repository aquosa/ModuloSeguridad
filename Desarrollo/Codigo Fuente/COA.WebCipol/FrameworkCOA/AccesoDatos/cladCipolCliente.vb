Imports EntidadesEmpresariales
Imports System.Configuration

''' -----------------------------------------------------------------------------
''' Project	 : AccesoDatos
''' Class	 : cladCipol
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Componente lógico de acceso a datos de CIPOL
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	30/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class cladCipol
    Inherits PadreAccesoDatos

    ''' <summary>
    ''' Audita la supervisión de la tarea primitiva
    ''' </summary>
    ''' <param name="IDUsuarioSupervisor">Identificador del usuario que supervisó</param>
    ''' <param name="IDTarea">Tarea primitiva que se supervisó</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [lunes, 18 de mayo de 2009]          GCP-Cambios ID: 8923
    ''' </history>
    Public Sub AuditarSupervision(ByVal IDUsuarioSupervisor As Integer, ByVal IDTarea As Integer)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("INSERT INTO SE_AUDITORIASUPERVISION(FechaHoraLog, IDUsuario_Supervisor, IDUsuario_Supervisado, ")
        sblSql.Append("IDTarea, Terminal) VALUES ( ")
        sblSql.Append(Me.objConexion.XtoStr(Me.objConexion.FechaServidor))
        sblSql.Append(", ")
        sblSql.Append(IDUsuarioSupervisor)
        sblSql.Append(", ")
        sblSql.Append(Me.DatosDelUsuario.IdUsuario)
        sblSql.Append(", ")
        sblSql.Append(IDTarea)
        sblSql.Append(", ")
        If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            sblSql.Append(Me.objConexion.XtoStr(CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")))
        Else
            sblSql.Append(Me.objConexion.XtoStr(CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente).OtrosDatos("terminal")))
        End If
        sblSql.Append(")")

        Me.objConexion.Ejecutar(sblSql.ToString())

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los datos del usuario
    ''' </summary>
    ''' <returns>DataSet con los datos del usuario</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarDatos() As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsRet : DataSet de retorno 
        'objSp  : Objeto Command que se utiliza para ejecutar el store procedure
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Dim dtsRet As System.Data.DataSet = Nothing
        Dim objSp As System.Data.IDbCommand
        Dim dtsRet As System.Data.DataSet = Nothing


        objSp = Me.objConexion.RecuperarComando
        objSp.CommandText = "spRecuperarDatos"
        objSp.CommandType = CommandType.StoredProcedure


        'TODO:ANGEL ver si se considera la conexion a ACCESS
        Select Case objConexion.ConectadoA
            Case "Oracle"
                objSp.Parameters.Add(objConexion.AgregarParametroComando("Valor1", DbType.String, ParameterDirection.Output, 2000))
                objSp.Parameters.Add(objConexion.AgregarParametroComando("Valor2", DbType.String, ParameterDirection.Output, 2000))
            Case "SQLServer"
                objSp.Parameters.Add(objConexion.AgregarParametroComando("@Valor1", DbType.String, ParameterDirection.Output, 2000))
                objSp.Parameters.Add(objConexion.AgregarParametroComando("@Valor2", DbType.String, ParameterDirection.Output, 2000))
        End Select


        objSp.ExecuteNonQuery()

        Dim dttTabla As New System.Data.DataTable("Datos")
        dttTabla.Columns.Add(New System.Data.DataColumn("Columna1", Type.GetType("System.String")))
        dttTabla.Columns.Add(New System.Data.DataColumn("Columna2", Type.GetType("System.String")))

        Dim dtrDatos As System.Data.DataRow = dttTabla.NewRow


        dtrDatos.Item(0) = CType(objSp.Parameters.Item(0), System.Data.IDataParameter).Value
        dtrDatos.Item(1) = CType(objSp.Parameters.Item(1), System.Data.IDataParameter).Value

        dttTabla.Rows.Add(dtrDatos)

        dtsRet = New System.Data.DataSet
        dtsRet.Tables.Add(dttTabla)

        Return dtsRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna el Identificador del usuario, si existe o -1 si no existe
    ''' </summary>
    ''' <param name="Login">Login del usuario</param>
    ''' <returns>Identificador del usuario si existe</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ExisteUsuario(ByVal Login As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsUsuario : Objeto DataSet que se utiliza para recuperar el identificador
        '             del usuario
        'shtID      : Identificador del usuario
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsUsuario As System.Data.DataSet = Nothing
        Dim shtID As Integer = -1

        Me.objConexion.Ejecutar("SELECT IDUsuario FROM SE_USUARIOS WHERE Usuario = " & Me.objConexion.XtoStr(Login), dtsUsuario, "Usuario")
        If dtsUsuario.Tables(0).Rows.Count > 0 Then
            shtID = System.Convert.ToInt32(dtsUsuario.Tables(0).Rows(0).Item(0))
        End If

        Return shtID

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera el nombre del Dominio al cual CIPOL debe conectarse
    ''' </summary>
    ''' <returns>Datos del Dominio</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarDominio() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'strRet : Valor de retorno de la función
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strRet As String

        strRet = Me.objConexion.EjecutarEscalar("SELECT Observaciones FROM SE_SIST_Habilitados WHERE NombreExec IS NULL").ToString

        Return strRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna la cantidad de terminales cargadas
    ''' </summary>
    ''' <returns>Cantidad de terminales</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarCantidadTerminales() As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtrCant    : Cantidad de terminales existentes
        'intCant    : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtrCant As System.Data.IDataReader = Nothing
        Dim intCant As Integer

        Me.objConexion.Ejecutar("SELECT COUNT(*) FROM SE_TERMINALES", dtrCant)
        dtrCant.Read()
        intCant = CType(dtrCant.Item(0), Integer)
        dtrCant.Close()

        Return intCant

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los IDUsuario de los usuarios existentes
    ''' </summary>
    ''' <returns>DataSet de usuarios</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarIDUsuarios() As DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsUs  : Objeto DataSet de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsUs As System.Data.DataSet = Nothing

        Const Tabla As String = "Usuarios"

        Me.objConexion.Ejecutar("SELECT IDUsuario, Usuario, Nombres FROM SE_USUARIOS WHERE FechaBaja IS NULL", dtsUs, Tabla)
        Return dtsUs

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los mensajes a mostrar al usuario y los mensajes de auditoria
    ''' </summary>
    ''' <returns>DataSet con los mensajes</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarMensajes() As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsRet : DataSet de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsRet As System.Data.DataSet = Nothing

        Const TablaMsjUs As String = "Usuario"
        Const TablaMsjAudit As String = "Auditoria"

        Me.objConexion.Ejecutar("SELECT CodMensaje, TextoMensaje FROM SE_Mensajes", dtsRet, TablaMsjUs)
        Me.objConexion.Ejecutar("SELECT CodAuditoria, TextoAuditoria FROM SE_CodAuditoria", dtsRet, TablaMsjAudit)

        Return dtsRet

    End Function

    Public Function RecuperarMensajeAuditoria(ByVal CodMensaje As Int16, ByVal CodAuditoria As Int16, ByRef TextoAuditoria As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'strRet : string de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objRet As Object
        Dim objAudit As Object
        'Si el Código de Auditoría esta seteado
        If CodAuditoria >= 0 Then
            objAudit = Me.objConexion.EjecutarEscalar("SELECT TextoAuditoria FROM SE_CodAuditoria WHERE CodAuditoria=" + CodAuditoria.ToString())
            If objAudit Is Nothing OrElse IsDBNull(objAudit) Then
                TextoAuditoria = ""
            Else
                TextoAuditoria = objAudit.ToString()
            End If
        Else
            TextoAuditoria = ""
        End If

        objRet = Me.objConexion.EjecutarEscalar("SELECT TextoMensaje FROM SE_Mensajes WHERE CodMensaje=" + CodMensaje.ToString())
        If objRet Is Nothing OrElse IsDBNull(objRet) Then
            Return ""
        Else
            Return objRet.ToString()
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los parámetros de Políticas de Seguridad
    ''' </summary>
    ''' <returns>String que contiene los parámetros de seguridad</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarParametros() As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtrParam   : Objeto DataReader que se utiliza para recuperar los parámetros
        '             de seguridad
        'strRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtrParam As System.Data.IDataReader = Nothing
        Dim strRet As String

        Me.objConexion.Ejecutar("SELECT Columna4 FROM SE_PARAMETROS", dtrParam)
        dtrParam.Read()
        If dtrParam.IsDBNull(0) Then
            strRet = String.Empty
        Else
            strRet = CType(dtrParam.Item(0), String)
        End If
        dtrParam.Close()

        Return strRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Audita una determinada acción de seguridad
    ''' </summary>
    '''<param name="CodMensaje">Codigo de Mensaje a auditar</param>
    '''<param name="Mensaje">Mensaje a auditar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' [LucianoP]          [miércoles, 5 de abril de 2017]    Se agrega el usuario logeado a la auditoria
    ''' </history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Auditar(ByVal CodMensaje As Short, ByVal Mensaje As String, ByVal Login As String) As Boolean
        Dim sblSql As New System.Text.StringBuilder
        Dim objComando As IDbCommand

        objComando = objConexion.RecuperarComando()

        sblSql.Append("INSERT INTO SE_Auditoria(FechaHoraLog, CodMensaje, TextoMensaje, UsuarioActuante, UsuarioAfectado, IP")

        If Not String.IsNullOrEmpty(Login) Then
            sblSql.Append(",IDUSUARIO ")
            sblSql.Append(",IDAREA ")
        End If

        sblSql.Append(" ) VALUES ( ")
        sblSql.Append(Me.objConexion.XtoStr(Me.FechaServidor))
        sblSql.Append(",")
        sblSql.Append(Me.objConexion.XtoStr(CodMensaje))
        sblSql.Append(",")
        sblSql.Append(Me.objConexion.XtoStr(Mensaje))
        sblSql.Append(",")
        sblSql.Append("NULL, NULL,")
        sblSql.Append(objConexion.XtoStr(EntidadesEmpresariales.Sesion.IP.GetIPAddress()))

        If Not String.IsNullOrEmpty(Login) Then
            sblSql.Append(", (SELECT IDUSUARIO FROM SE_USUARIOS WHERE USUARIO = @pUSUARIO)")
            sblSql.Append(", (SELECT IDAREA FROM SE_USUARIOS WHERE USUARIO = @pUSUARIO)")
            objComando.Parameters.Add(objConexion.AgregarParametroComando("@pUSUARIO", DbType.StringFixedLength, ParameterDirection.Input, 50, Login))
        End If

        sblSql.Append(")")

        objComando.CommandType = CommandType.Text
        objComando.CommandText = sblSql.ToString()

        Return Me.objConexion.Ejecutar(objComando) > 0

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera datos del usuario para el proceso de login
    ''' </summary>
    '''<param name="Login">Nombre de inicio de sesión del usuario</param>
    '''<returns>DataSet con datos del usuario que se recibe por parámetro</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' 	[gustavom]	14/06/2007	GCP-Cambios ID: 5208
    '''     [MartinV]          [miércoles, 06 de noviembre de 2013]    GCP-Cambios 14460
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarDatosUsuario(ByVal Login As String) As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Autor: gustavom
        'Fecha de Creación: 03/11/2004
        'Modificaciones:
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsRet : DataSet de Retorno
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsRet As System.Data.DataSet = Nothing, sblSql As New System.Text.StringBuilder


        With sblSql
            .Append("SELECT SE_USUARIOS.IdUsuario, Usuario, Nombres, ForzarCambio, CantIntInvUsoCta,FechaUltUsoCta,FECHADESBLOQUEO, ")
            .Append("CtaBloqueada, FechaBloqueo, SE_Historial_Usuario.Sinonimo, SE_Historial_Usuario.FechaVencimiento, SIST_KAREAS.IDArea, SIST_KAREAS.NombreArea, UltimoSistema, ALIAS_USUARIO FROM SE_USUARIOS INNER JOIN SE_Historial_Usuario ")
            .Append("ON SE_USUARIOS.IDUsuario = SE_Historial_Usuario.IDUsuario ")
            .Append("INNER JOIN SIST_KAREAS ON SIST_KAREAS.IDArea = SE_USUARIOS.IDArea ")
            .Append("WHERE SE_USUARIOS.FechaBaja IS NULL AND SE_Historial_Usuario.Orden = 0")
            .Append(" AND SE_USUARIOS.Usuario = ")
            .Append(Me.objConexion.XtoStr(Login))

            Me.objConexion.Ejecutar(.ToString, dtsRet, Login)
        End With

        Return dtsRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los datos de tareas primitivas, usuarios autorizantes, etc para un usuario
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario</param>
    '''<returns>DataSet con datos del usuario</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarTareasPrimitivas(ByVal IDUsuario As Integer) As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        'dtsRet : DataSet de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder, dtsRet As System.Data.DataSet = Nothing

        'Recupera los usuarios que pueden autorizar las tareas primitivas que
        'tiene asignado el usuario 
        With sblSql

            .Append("SELECT SE_USUARIOS.IdUsuario, Usuario, Nombres, IdTareaPrimitiva, TareaInhibida FROM SE_USUARIOS INNER JOIN SE_Tareas_Usuario ON ")
            .Append("SE_USUARIOS.IdUsuario = SE_Tareas_Usuario.IdUsuario INNER JOIN SE_Rel_Autoriz ON SE_Tareas_Usuario.IdTarea = IdTareaAutor ")
            .Append(" WHERE FechaBaja IS NULL AND FechaBloqueo IS NULL AND IdTareaPrimitiva IN( SELECT IdTarea FROM SE_Tareas_Usuario WHERE IdUsuario = ")
            .Append(IDUsuario)
            .Append(")"c)

            Me.objConexion.Ejecutar(.ToString, dtsRet, "Autorizantes")
        End With
        sblSql = Nothing
        sblSql = New System.Text.StringBuilder

        'Recupera las tareas primitivas que un usuario posee a partir de los roles
        With sblSql
            .Append("SELECT SE_Tareas.IdTarea, DescripcionTarea, TareaInhibida, IdAutorizacion, RequiereAuditoria, " & Me.objConexion.ISNULL("Momento", "9") & " As Momento,CodigoTarea,IDSistema ")
            .Append("FROM SE_Tareas INNER JOIN SE_Tareas_Usuario ON ")
            .Append("SE_Tareas.IdTarea = SE_Tareas_Usuario.IdTarea ")
            .Append("LEFT JOIN SE_AtributosTareas ON SE_Tareas.IDTarea = SE_AtributosTareas.IDTarea ")
            .Append("WHERE IdUsuario = ")
            .Append(IDUsuario)

            Me.objConexion.Ejecutar(.ToString, dtsRet, "Tareas")
        End With

        sblSql = Nothing

        Return dtsRet

    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza el sistema actual en el cual se encuentra el usuario
    ''' </summary>
    ''' <param name="IDUsuario">Identificador del usuario</param>
    ''' <param name="IDSistema">Identificador del sistema donde el usuario se encuentra</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub EstablecerSistemaActual(ByVal IDUsuario As Short, ByVal IDSistema As Short)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Me.objConexion.Ejecutar("UPDATE SE_USUARIOS SET UltimoSistema = " & IDSistema.ToString & " WHERE IDUsuario = " & IDUsuario.ToString)

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los sistemas y menues permitidos por el usuario
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario</param>
    '''<returns>DataSet con datos del usuario</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [gustavom]	30/08/2005	Created
    ''' [AndresR]          [miércoles, 28 de mayo de 2008]       GCP-Cambios ID: 5814
    ''' [LeandroF]         [lunes, 21 de octubre de 2013]        Se agregó al columna PAGINAPORDEFECTO  a la consulta
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarSistemasMenu(ByVal IDUsuario As Integer) As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        'dtsRet : DataSet de retorno
        'rowFila: Objeto DataRow que se utiliza para recuperar los identificadores
        '         de sistemas y retornar los menús de los mismos
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder, dtsRet As System.Data.DataSet = Nothing
        Dim rowFila As System.Data.DataRow

        'Recupera los sistemas que tiene permitido el usuario
        If IDUsuario = 0 Then
            sblSql.Append("SELECT IDSistema, DescSistema, NombreExec, Icono,CodSistema, PAGINAPORDEFECTO FROM SE_Sist_Habilitados WHERE IDSistema = 1")

            Me.objConexion.Ejecutar(sblSql.ToString, dtsRet, "SE_SIST_HABILITADOS")
        Else
            'Si no es el master
            With sblSql
                .Append("SELECT DISTINCT SE_Sist_Habilitados.IDSistema, CodSistema, DescSistema, NombreExec, Icono, PAGINAPORDEFECTO FROM SE_Sist_Habilitados INNER JOIN SE_Tareas ON ")
                .Append("SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema INNER JOIN SE_Tareas_Usuario ON ")
                .Append("SE_Tareas.IdTarea = SE_Tareas_Usuario.IdTarea ")
                .Append("WHERE IdUsuario = ")
                .Append(IDUsuario)
                .Append(" AND SistemaHabilitado = ")
                .Append(Me.objConexion.XtoStr("S"c))
                'AndresR GCP-Cambios ID: 5814 - Se agrega verificacion de sistemas BLOQUEADOS:
                '1.- Traer los sistemas que NO esten en la tabla SE_SIST_BLOQUEADOS
                '2.- Y ademas traer los sistemas para los que el usuario este habilitado
                .Append(" AND ( ")
                .Append("	   SE_Sist_Habilitados.IDSISTEMA not in ( SELECT DISTINCT IDSISTEMA FROM SE_SIST_BLOQUEADOS ) ")
                .Append("	   OR SE_Sist_Habilitados.IDSISTEMA in ( SELECT IDSISTEMA FROM SE_SIST_BLOQUEADOS WHERE IDUSUARIO = " & IDUsuario & " ) ")
                .Append("	  ) ")
                .Append(" ORDER BY DescSistema")

            End With

            Me.objConexion.Ejecutar(sblSql.ToString, dtsRet, "SE_SIST_HABILITADOS")

        End If

        sblSql = Nothing
        sblSql = New System.Text.StringBuilder


        With sblSql
            .Append("SELECT IDSistemas, IDItemMenu, TextoItem, Url, IDItemPadre, ")
            .Append("EsPadre, Descripcion, IDTarea, OrdenSubMenu, AccesoDirecto ")
            'AndresR agrego 1=2 para que por defecto no traiga nada
            .Append("FROM SE_MENUES WHERE 1=2 ")


            If dtsRet.Tables("SE_SIST_HABILITADOS").Rows.Count > 0 Then

                .Append(" OR IDSistemas IN( ")
                For Each rowFila In dtsRet.Tables("SE_SIST_HABILITADOS").Rows
                    .Append(CType(rowFila.Item("IDSistema"), String))
                    .Append(","c)
                Next
                .Remove(.Length - 1, 1)

                .Append(") ")

            End If

            .Append(" ORDER BY IDItemPadre ASC, OrdenSubMenu ASC")

            Me.objConexion.Ejecutar(sblSql.ToString, dtsRet, "SE_MENUES")
        End With

        Return dtsRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la fecha y hora de último uso de la cuenta e inicializa la cantidad de intentos fallidos
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario</param>
    '''<param name="InicFallidos">valor de inicialización de intentos fallidos encriptados</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    '''     [MartinV]          [martes, 12 de noviembre de 2013]       Modificado  GCP-Cambios 14460.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub ActualizarCuenta(ByVal IDUsuario As Integer, ByVal InicFallidos As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Me.objConexion.Ejecutar("UPDATE SE_USUARIOS SET FechaUltUsoCta = " & Me.objConexion.XtoStr(Me.objConexion.FechaServidor) & ", CantIntInvUsoCta = '" & InicFallidos & "',FECHADESBLOQUEO = NULL WHERE IdUsuario = " & IDUsuario.ToString)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna datos con respecto a una terminal
    ''' </summary>
    '''<param name="NombrePC">Nombre NetBios de la terminal</param>
    '''<returns>DataSet que contiene el identificador de la terminal y si la terminal puede usarse o no</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarDatosTerminal(ByVal NombrePC As String) As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsRet : DataSet de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsRet As System.Data.DataSet = Nothing

        Const Tabla As String = "Terminal"

        Me.objConexion.Ejecutar("SELECT IDTerminal, UsoHabilitado, ORIGENACTUALIZACION FROM SE_Terminales WHERE NombreNetBios = '" & NombrePC & "'", dtsRet, Tabla)

        Return dtsRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Determina si un usuario puede iniciar sesión en una determinada terminal
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario</param>
    '''<param name="IDTerminal">Identificador de terminal</param>
    '''<returns>True o False</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function PuedeIniciarSesionEnTerminal(ByVal IDUsuario As Integer, ByVal IDTerminal As Short) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'blnRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim blnRet As Boolean

        blnRet = System.Convert.ToInt16(Me.objConexion.EjecutarEscalar("SELECT COUNT(*) FROM SE_Term_Usuario WHERE IDUsuario = " & IDUsuario.ToString & " AND IdTerminal = " & IDTerminal.ToString)) = 0

        Return blnRet

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Determina si el usuario puede iniciar sesión en un horario definido
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario</param>
    '''<param name="FechaHoraActual">Fecha y hora actual</param>
    '''<returns>True o False</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function HorarioPermitido(ByVal IDUsuario As Integer, ByVal FechaHoraActual As Date) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsVerif   : Objeto DataSet que se utiliza para verificar si el usuario
        '             puede iniciar sesion en un horario determinado
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        'blnRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsVerif As System.Data.DataSet = Nothing
        Dim sblSql As New System.Text.StringBuilder
        Dim blnRet As Boolean


        With sblSql
            .Append("SELECT IdUsuario FROM SE_Horarios_Usuario WHERE IdUsuario = ")
            .Append("<x>")
            .Append(" AND IDDia = ")
            .Append(Weekday(FechaHoraActual, FirstDayOfWeek.Monday))
            .Append(" AND IDHorario = ")
            .Append(FechaHoraActual.Hour)
        End With


        'verifico si el usuario posee algún horario no permitido
        If System.Convert.ToInt32( _
                objConexion.EjecutarEscalar( _
                "select count(*) as cantidad from se_horarios_usuario where idusuario = " _
                + Me.objConexion.XtoStr(IDUsuario))) > 0 Then

            Me.objConexion.Ejecutar(sblSql.ToString.Replace("<x>", IDUsuario.ToString).ToString, dtsVerif, "Verif")


            If dtsVerif.Tables("Verif").Rows.Count > 0 Then
                blnRet = True
            End If
        Else
            'Si el usuario no tiene horario personalizado
            'obtengo el horario del master
            dtsVerif = New System.Data.DataSet
            Me.objConexion.Ejecutar(sblSql.ToString.Replace("<x>", "0").ToString, dtsVerif, "Verif")
            blnRet = dtsVerif.Tables(0).Rows.Count > 0
        End If
        Return blnRet
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Determina si el usuario posee una sesion en otra PC
    ''' </summary>
    '''<param name="Usuario">Identificador del usuario</param>
    '''<param name="Terminal">Identificador de la terminal</param>
    '''<returns>Si el usuario tiene iniciada una sesion devuelve el NombreNetBios,
    '''         En caso no tener una sesion iniciada devuelve string vacio
    ''' </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [gustavom]	30/08/2005	Created
    ''' [AndresR]          [lunes, 05 de mayo de 2008]       GCP-Cambios ID: 4255
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function PuedeIniciarSesion(ByVal Usuario As String, ByVal Terminal As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsVerif   : Objeto DataSet que se utiliza para verificar si el usuario
        '             posee una sesión iniciada en otra PC
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsVerif As System.Data.DataSet = Nothing
        Dim sblSql As New System.Text.StringBuilder

        'Elimino la sesión del usuario en la terminal 
        Me.objConexion.Ejecutar("DELETE FROM SE_SESIONESACTIVAS WHERE NombreNetBios = " & Me.objConexion.XtoStr(Terminal) & " AND StrUsuario = " & Me.objConexion.XtoStr(Usuario))
        Me.objConexion.Ejecutar("SELECT NombreNetBios FROM SE_SESIONESACTIVAS WHERE strUsuario = " & Me.objConexion.XtoStr(Usuario), dtsVerif, "Verif")
        If dtsVerif.Tables(0).Rows.Count > 0 Then
            Return dtsVerif.Tables(0).Rows(0).Item("NombreNetBios").ToString
        Else
            'Ingreso la sesión del usuario
            With sblSql
                .Append("INSERT INTO SE_SESIONESACTIVAS(strUsuario, NombreNetBios, InicioTarea) VALUES ( ")
                .Append(Me.objConexion.XtoStr(Usuario))
                .Append(", ")
                .Append(Me.objConexion.XtoStr(Terminal))
                .Append(", ")
                .Append(Me.objConexion.XtoStr(Me.objConexion.FechaServidor))
                .Append(" )")
            End With
            Me.objConexion.Ejecutar(sblSql.ToString)

            Return ""
        End If

    End Function

    ''' <summary>
    ''' Bloquea la cuenta de un usuario
    ''' </summary>
    ''' <param name="IDUsuario">Identificador del usuario</param>
    ''' <param name="ValorEnc">valor de bloqueo encriptado</param>
    ''' <param name="ResetearUsuario">Indica si se debe resetear la Fecha de último uso de cuenta</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    '''     [MartinV]          [jueves, 07 de noviembre de 2013]       Modificado  GCP-Cambios 14460
    ''' </history>
    Public Sub BloquearCta(ByVal IDUsuario As Integer, ByVal ValorEnc As String, ByVal FechaBloqueo As DateTime?)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Autor: gustavom
        'Fecha de Creación: 04/11/2004
        'Modificaciones:
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("UPDATE SE_USUARIOS SET CtaBloqueada = '" & ValorEnc & "'")
        If (FechaBloqueo.HasValue) Then
            sblSql.Append(", FechaBloqueo = " & Me.objConexion.XtoStr(FechaBloqueo.Value))
        Else
            sblSql.Append(", FechaBloqueo = NULL")
        End If
        sblSql.Append(" WHERE IdUsuario = " & IDUsuario.ToString)

        Me.objConexion.Ejecutar(sblSql.ToString())

    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Desbloquea una cuenta de usuario
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario </param>
    '''<param name="ValorEnc">Valor de desbloqueo encriptado</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub DesbloquearCta(ByVal IDUsuario As Integer, ByVal ValorEnc As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Me.objConexion.Ejecutar("UPDATE SE_USUARIOS SET CtaBloqueada = '" & ValorEnc & "', FechaBloqueo = NULL WHERE IDUsuario = " & IDUsuario.ToString)

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la cantidad de intentos fallidos de un usuario
    ''' </summary>
    '''<param name="IDUsuario">Identificador del usuario </param>
    '''<param name="Cantidad">Cantidad de intentos</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub IntentosFallidos(ByVal IDUsuario As Integer, ByVal Cantidad As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Me.objConexion.Ejecutar("UPDATE SE_USUARIOS SET CantIntInvUsoCta = '" & Cantidad & "' WHERE IdUsuario = " & IDUsuario.ToString)

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina el inicio de sesión del usuario en la tabla SE_SESIONESACTIVAS
    ''' </summary>
    '''<param name="Usuario">Nombre del usuario</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	30/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub CerrarSesion(ByVal Usuario As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'Elimino la sesión del usuario en la terminal 
        Me.objConexion.Ejecutar("DELETE FROM SE_SESIONESACTIVAS WHERE StrUsuario = " & Me.objConexion.XtoStr(Usuario))

    End Sub

    ''' <summary>
    ''' Indica si existe una sesion registrada para el usuario
    ''' </summary>
    '''<param name="Usuario">Identificador del usuario</param>
    '''<param name="Terminal">Identificador de la terminal</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [martes, 5 de julio de 2016]    Creado 
    ''' </history>
    Public Function ExisteSesionActiva(ByVal Usuario As String, _
                                       ByVal Terminal As String) As Boolean
        Dim objComando As IDbCommand
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT COUNT(*) FROM SE_SESIONESACTIVAS ")
        sblSql.Append("WHERE NombreNetBios = @pNombreNetBios AND ")
        sblSql.Append("      StrUsuario    = @pStrUsuario")

        objComando = objConexion.RecuperarComando
        objComando.CommandType = CommandType.Text
        objComando.CommandText = sblSql.ToString

        objComando.Parameters.Add(objConexion.AgregarParametroComando("@pNombreNetBios", DbType.StringFixedLength, ParameterDirection.Input, 255, Terminal))
        objComando.Parameters.Add(objConexion.AgregarParametroComando("@pStrUsuario", DbType.StringFixedLength, ParameterDirection.Input, 15, Usuario))

        Dim objRet As Object = objConexion.EjecutarEscalar(objComando)

        Return objRet IsNot Nothing AndAlso Not System.Convert.DBNull.Equals(objRet) AndAlso Convert.ToInt32(objRet) > 0

    End Function

End Class
