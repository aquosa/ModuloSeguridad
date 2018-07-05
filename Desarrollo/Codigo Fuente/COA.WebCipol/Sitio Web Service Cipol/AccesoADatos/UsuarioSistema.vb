Imports EE = Fachada.Seguridad
Public Class UsuarioSistema
    Inherits PadreSistema
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' retorna las tareas concernientes a cipol
    ''' </summary>
    ''' <param name="dtsDataset">dataset que contendrá la tabla con la iinformación</param>
    ''' <returns>cantidad de filas retornadas por la consulta</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	08/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarUsuariosConTareasCipol(ByVal dtsDataset As DataSet) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" SELECT DISTINCT su.idusuario, su.nombres, su.Usuario  ")
        sblSql.Append(" FROM SE_USUARIOS su, se_tareas_usuario t ")
        sblSql.Append(" WHERE t.idtarea >= 1000 And t.idtarea < 2000 ")
        sblSql.Append(" AND t.idusuario = su.idusuario ")
        sblSql.Append(" ORDER BY su.nombres  ")

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, "Sist_UsuariosConTareasCipol")
    End Function

    ''' <summary>
    ''' Los usuario con tareas asignadas
    ''' </summary>
    ''' <param name="dtsDataset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [JorgeI]  [viernes, 25 de abril de 2014] Creado GCP-Cambios ID:15293
    ''' </history>
    Public Function RecuperarUsuariosConTareas(ByVal dtsDataset As DataSet, ByVal strNombreTabla As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT DISTINCT su.idusuario, su.nombres, su.Usuario")
        sblSql.Append(" FROM SE_USUARIOS su")
        sblSql.Append(" INNER JOIN se_tareas_usuario t ON")
        sblSql.Append(" t.idusuario = su.idusuario")
        sblSql.Append(" ORDER BY Usuario")

        objConexion.Ejecutar(sblSql.ToString(), dtsDataset, strNombreTabla)

        ''Insertamos el valor de usuario master.
        Dim row As System.Data.DataRow
        row = dtsDataset.Tables(strNombreTabla).NewRow()
        row(0) = 0
        row(1) = "SuperUsuario"
        row(2) = "master"

        dtsDataset.Tables(strNombreTabla).Rows.InsertAt(row, 0)
    End Function

    ''' <summary>
    ''' Setea si un usuario será forzado o no a que cambie su contrasenia.
    ''' </summary>
    ''' <param name="idUsuario">identificador del usuario</param>
    ''' <param name="ForzarCambioContrasenia">valor para el "fuerze" de la contraseña</param>
    ''' <returns>cantidad de filas afectadas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	03/11/2005	Created
    ''' </history>
    Public Function Forzar_NoForzar_CambioContrasenia(ByVal idUsuario As Int32, ByVal ForzarCambioContrasenia As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de conexion sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        sblSql.Append("UPDATE SE_USUARIOS SET ")
        sblSql.Append(" ForzarCambio = " + objConexion.XtoStr(ForzarCambioContrasenia))
        sblSql.Append(" WHERE IdUsuario = " & objConexion.XtoStr(idUsuario))

        Return objConexion.Ejecutar(sblSql.ToString())
    End Function
    Public Function AltaTerminalProhibida(ByVal idUsuario As Int32, ByVal idTerminal As Int32) As Int32
        Dim sblsql As New System.Text.StringBuilder

        sblsql.Append(" INSERT INTO SE_Term_Usuario( IdUsuario, IdTerminal ) VALUES ( ")
        sblsql.Append(objConexion.XtoStr(idUsuario) & ", " & objConexion.XtoStr(idTerminal) + ")")

        objConexion.Ejecutar(sblsql.ToString())
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' da de alta un horario
    ''' </summary>
    ''' <param name="IdUsuario">identificador del usuario</param>
    ''' <param name="idDia">identificador del dia</param>
    ''' <param name="idHora">identificador de la hora</param>
    ''' <returns>cantidad de filas afectadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	27/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function AltaHorarios(ByVal IdUsuario As Int32, ByVal idDia As Int32, ByVal idHora As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("INSERT INTO SE_Horarios_Usuario( IdUsuario, IdDia, IdHorario ) VALUES ( ")
        sblSql.Append(objConexion.XtoStr(IdUsuario))
        sblSql.Append(", " + objConexion.XtoStr(idDia))
        sblSql.Append(", " + objConexion.XtoStr(idHora))
        sblSql.Append(" ) ")

        Return objConexion.Ejecutar(sblSql.ToString())
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' dA DE ALTA UN REGISTRO EN EL HISTORIAL DE USUARIOS
    ''' </summary>
    ''' <param name="idusuario"></param>
    ''' <param name="orden"></param>
    ''' <param name="contrasenia"></param>
    ''' <param name="fechaUltAct"></param>
    ''' <param name="FechaVencimiento"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	27/10/2005	Created
    '''     [MartinV]          [jueves, 07 de noviembre de 2013]       Modificado  GCP-Cambios 14460
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function AltaHistorialUsuario(ByVal idusuario As Int32, ByVal orden As Int32, ByVal contrasenia As String, ByVal fechaUltAct As System.DateTime, ByVal FechaVencimiento As Object, ByVal ModificaUsuario As Boolean) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        sblsql.Append(" INSERT INTO SE_Historial_Usuario( ")
        sblsql.Append(" IdUsuario, Orden, Sinonimo, FechaUltAct, FechaVencimiento")
        If (ModificaUsuario) Then
            sblsql.Append(" , FECHAULTACTUSUARIO")
        End If
        sblsql.Append(" ) VALUES ( ")
        sblsql.Append(objConexion.XtoStr(idusuario))         'idusuario
        sblsql.Append("," + objConexion.XtoStr(orden))       'orden
        sblsql.Append("," + objConexion.XtoStr(contrasenia)) 'sinonimo
        sblsql.Append("," + objConexion.XtoStr(fechaUltAct)) 'fechaUltAct
        If IsDBNull(FechaVencimiento) OrElse FechaVencimiento Is Nothing Then
            sblsql.Append(", NULL") 'fechavencimiento
        Else
            sblsql.Append("," + objConexion.XtoStr(System.Convert.ToDateTime(FechaVencimiento))) 'fechavencimiento
        End If
        If ModificaUsuario Then
            sblsql.Append("," + objConexion.XtoStr(FechaServidor))
        End If
        sblsql.Append(")")

        Return objConexion.Ejecutar(sblsql.ToString())
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Incrementa en 1 los ordenes del hisotrial de un usuario
    ''' </summary>
    ''' <param name="idUsuario">identificador del usuario cuyos órdenes se incrementarán</param>
    ''' <returns>cantidad de filas afectadas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	27/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ActualizarOrdenesHistorial(ByVal idUsuario As Int32) As Int32
        Return objConexion.Ejecutar("UPDATE SE_Historial_Usuario SET Orden = Orden + 1 WHERE IdUsuario = " & objConexion.XtoStr(idUsuario))

    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina datos de la tabla historial Usuario.
    ''' </summary>
    ''' <param name="idUsuario">identificador de usuario. </param>
    ''' <param name="ComparacionORden">tipod e comparacion para el parametro 
    ''' Orden, si es Ignorar, no se tiene en cuenta el parámetro </param>
    ''' <param name="orden"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	27/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function EliminarDeHistorialUsuario(ByVal idUsuario As Int32, ByVal ComparacionORden As TipoComparacion, ByVal orden As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql        : string de consulta Sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" DELETE FROM SE_Historial_Usuario ")
        sblSql.Append(" WHERE IdUsuario = " + objConexion.XtoStr(idUsuario))

        'segun el filtro de comparación indicado, hacemos = o >=.
        Select Case ComparacionORden
            Case TipoComparacion.Igual
                sblSql.Append(" AND Orden = " + objConexion.XtoStr(orden))
            Case TipoComparacion.Mayor_o_Igual
                sblSql.Append(" AND Orden >= " + objConexion.XtoStr(orden))
            Case TipoComparacion.Ignorar
        End Select

        Return objConexion.Ejecutar(sblSql.ToString())
    End Function

    Public Enum TipoComparacion As Integer
        Igual = 1
        Mayor_o_Igual = 2
        Ignorar = 3
    End Enum

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' modifica un usuario un usuario
    ''' </summary>
    ''' <param name="pdtsDatos">datarow con la información a dar de alta.</param>
    ''' <returns>cantidad de filas afectadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' [LucianoP]          [viernes, 11 de mayo de 2007]       GCP-Cambios ID: 5026
    ''' [AndresR]           [martes, 29 de abril de 2008]       GCP-Cambios ID: 6795
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ActualizarUsuario(ByVal pdtsDatos As EE.dtsUsuariosABM) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de conexion sql.
        ' objEncriptarNet   : 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim objCipol As EntidadesEmpresariales.PadreCipolCliente

        If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            objCipol = CType(System.Web.HttpContext.Current.Session("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        Else
            objCipol = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        End If

        Dim objEncriptarNet As New COA.CifrarDatos.TresDES(objCipol.IV, objCipol.Key)

        With pdtsDatos.Sist_Usuarios(0)
            'Actualizo los datos del usuario
            '06/01/2003 - Gustavo Mazzaglia - GCP-Cambios ID: 1453
            sblSql.Append("UPDATE SE_USUARIOS SET ")
            sblSql.Append(" Usuario = " & objConexion.XtoStr(.Usuario.ToLower().Trim()))
            sblSql.Append(", Nombres = " & objConexion.XtoStr(.Nombres.Trim()))
            If .IsDomicilioNull Then
                sblSql.Append(", Domicilio = null")
            Else
                sblSql.Append(", Domicilio = " & objConexion.XtoStr(.Domicilio.Trim()))
            End If
            sblSql.Append(", IdTipoDoc = " & objConexion.XtoStr(.IdTipoDoc))
            If .IsNroDocumentoNull Then
                sblSql.Append(", NroDocumento = null")
            Else
                sblSql.Append(", NroDocumento = " & objConexion.XtoStr(.NroDocumento.Trim()))
            End If
            sblSql.Append(", IdArea = " & objConexion.XtoStr(.idArea))
            sblSql.Append(", ForzarCambio = " + objConexion.XtoStr(objEncriptarNet.Criptografia(COA.CifrarDatos.Accion.Encriptacion, .ForzarCambio)))
            sblSql.Append(", CtaBloqueada = " + objConexion.XtoStr(objEncriptarNet.Criptografia(COA.CifrarDatos.Accion.Encriptacion, .CtaBloqueada)))

            If .Table.Columns.Contains("BLNFECHADESBLOQUEO") AndAlso CType(.Item("BLNFECHADESBLOQUEO"), Boolean) Then
                sblSql.Append(", FECHADESBLOQUEO = " + objConexion.XtoStr(Me.FechaServidor()))
            End If

            If .IsCantIntInvUsoCtaNull Then
                .CantIntInvUsoCta = "0"
            End If
            sblSql.Append(", CantIntInvUsoCta = " & objConexion.XtoStr(.CantIntInvUsoCta))
            If .IsFechaBloqueoNull OrElse .FechaBloqueo = DateTime.MinValue Then
                sblSql.Append(", FechaBloqueo = null ")
            Else
                sblSql.Append(", FechaBloqueo = " + objConexion.XtoStr(.FechaBloqueo))
            End If
            If String.IsNullOrEmpty(.Comentario) Then
                sblSql.Append(", Comentario = " + objConexion.XtoStr(" ") + ", ")
            Else
                sblSql.Append(", Comentario = " & objConexion.XtoStr(.Comentario.Trim()) + ", ")
            End If
            'GCP-Cambios ID: 5026
            If .IsALIAS_USUARIONull Then
                sblSql.Append(" ALIAS_USUARIO = null")
            Else
                sblSql.Append(" ALIAS_USUARIO = " & objConexion.XtoStr(.ALIAS_USUARIO))
            End If

            'GCP-Cambios ID: 6795
            If .IsEmailNull Then
                sblSql.Append(", Email = null")
            Else
                sblSql.Append(", Email = " & objConexion.XtoStr(.Email.Trim()))
            End If

            sblSql.Append(" WHERE IdUsuario = " & objConexion.XtoStr(.IdUsuario))

        End With

        Return objConexion.Ejecutar(sblSql.ToString())

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina los horarios de un usuario
    ''' </summary>
    ''' <param name="idUsuario">identificador del usuario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function EliminarHorarios(ByVal idUsuario As Int32) As Int32
        Return objConexion.Ejecutar("DELETE FROM SE_Horarios_Usuario WHERE IdUsuario = " & objConexion.XtoStr(idUsuario))
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina las terminales prohibidas por un usuario
    ''' </summary>
    ''' <param name="idUsuario">identificador del usuario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function EliminarTerminalesProhibidas(ByVal idUsuario As Int32) As Int32
        Return objConexion.Ejecutar("DELETE FROM SE_Term_Usuario WHERE IdUsuario = " & objConexion.XtoStr(idUsuario))
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina las tareas de un usuario
    ''' </summary>
    ''' <param name="idUsuario">identificador del usuario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function EliminarTareasUsuario(ByVal idUsuario As Int32) As Int32
        Return objConexion.Ejecutar(" DELETE FROM SE_Tareas_Usuario  WHERE IdUsuario = " & objConexion.XtoStr(idUsuario))
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Da de alta un usuario
    ''' </summary>
    ''' <param name="pdtsdatos">datset con la información a dar de alta.</param>
    ''' <returns>identificador del nuevo usuario.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' [LucianoP]          [viernes, 11 de mayo de 2007]       GCP-Cambios ID: 5026
    ''' [AndresR]           [martes, 29 de abril de 2008]       GCP-Cambios ID: 6795
    ''' [LeandroF]          [lunes, 27 de abril de 2015]        TFS ID: 3632
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function AltaDeUsuario(ByVal pdtsDatos As EE.dtsUsuariosABM) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de conexion sql.
        ' objCipol          : objeto de seguridad Cipol
        ' objEncriptarNet   : api de encriptacion
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim objCipol As EntidadesEmpresariales.PadreCipolCliente

        If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
            objCipol = CType(System.Web.HttpContext.Current.Session("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        Else
            objCipol = CType(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente)
        End If

        Dim objEncriptarNet As New COA.CifrarDatos.TresDES(objCipol.IV, objCipol.Key)

        With pdtsDatos.Sist_Usuarios(0)
            '06/01/2003 - Gustavo Mazzaglia - GCP-Cambios ID: 1453
            sblSql.Append("INSERT INTO SE_USUARIOS(IdUsuario, Usuario, Nombres, Domicilio, IdTipoDoc, ")
            sblSql.Append(" NroDocumento, IdArea, ForzarCambio, CtaBloqueada, Comentario, FechaAlta, FechaBloqueo, CantIntInvUsoCta,ALIAS_USUARIO, Email  ")
            sblSql.Append(",IDUSRALTA")
            
            sblSql.Append(" ) VALUES ( ")
            sblSql.Append(objConexion.XtoStr(.IdUsuario) & ", ") 'idusuario
            sblSql.Append(objConexion.XtoStr(.Usuario.ToLower()) & ", ") 'usuario
            sblSql.Append(objConexion.XtoStr(.Nombres) & ", ") 'nombres
            If .IsDomicilioNull Then
                sblSql.Append("null, ") 'domicilio
            Else
                sblSql.Append(objConexion.XtoStr(.Domicilio) + ", ") 'domicilio
            End If
            sblSql.Append(objConexion.XtoStr(.IdTipoDoc) & ", ") 'tipodocumento
            If .IsNroDocumentoNull Then
                sblSql.Append("null, ") 'nrodocumento
            Else
                sblSql.Append(objConexion.XtoStr(.NroDocumento) & ", ") 'nrodocumento
            End If
            sblSql.Append(objConexion.XtoStr(.idArea) & ", ") 'idarea
            sblSql.Append(objConexion.XtoStr(objEncriptarNet.Criptografia(COA.CifrarDatos.Accion.Encriptacion, .ForzarCambio)) & ", ") 'forzarcambio
            sblSql.Append(objConexion.XtoStr(objEncriptarNet.Criptografia(COA.CifrarDatos.Accion.Encriptacion, .CtaBloqueada)) & ", ") 'ctabloqueada
            '''''27/01/2003 - Gustavo Mazzaglia - GCP-Cambios ID: 1520
            sblSql.Append(objConexion.XtoStr(.Comentario) & ", ") 'comentario
            sblSql.Append(objConexion.XtoStr(objConexion.FechaServidor()) & ",") 'fechaalta
            If .IsFechaBloqueoNull OrElse .FechaBloqueo = DateTime.MinValue Then
                sblSql.Append(" NULL ")
            Else
                sblSql.Append(objConexion.XtoStr(.FechaBloqueo))
            End If
            sblSql.Append(", ")
            sblSql.Append(objConexion.XtoStr(.CantIntInvUsoCta) + ", ")

            'GCP-Cambios ID: 5026
            If .IsALIAS_USUARIONull Then
                sblSql.Append("null")
            Else
                sblSql.Append(objConexion.XtoStr(.ALIAS_USUARIO))
            End If

            sblSql.Append(", ")

            'GCP-Cambios ID: 6795
            If .IsEmailNull Then
                sblSql.Append("null")
            Else
                sblSql.Append(objConexion.XtoStr(.Email))
            End If

            sblSql.Append(",").Append(objConexion.XtoStr(DatosDelUsuario.IdUsuario))
        
            sblSql.Append(" ) ")

            '''''27/01/2003 - Gustavo Mazzaglia - GCP-Cambios ID: 1520
            '''''06/01/2003 - Gustavo Mazzaglia - GCP-Cambios ID: 1453
        End With

        objConexion.Ejecutar(sblSql.ToString())
        Return pdtsDatos.Sist_Usuarios(0).IdUsuario
    End Function


    ''' <summary>
    ''' rtorna el maximo id de usuario disponible para generar un alta
    ''' </summary>
    ''' <returns>maximo id de usuario disponible</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	25/10/2005	Created
    ''' </history>
    Public Function MaximoIdUsuarioDisponible() As Int32
        Dim objRetorno As Object
        objRetorno = objConexion.EjecutarEscalar("SELECT Max(IdUsuario) As Cantidad FROM SE_USUARIOS")
        If objRetorno.Equals(DBNull.Value) Then
            Return 1
        Else
            Return System.Convert.ToInt32(objRetorno) + 1
        End If
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna las contraseñas usasdas por un usuario
    ''' </summary>
    ''' <param name="dtsDataset">dataset que contendrá la información</param>
    ''' <param name="FiltroIdUsuario">identificador del usuario para filtrar</param>
    ''' <param name="FiltroPassword"> opcional, filtro por password </param>
    ''' <returns>cantidad defilas retornadas por la consulta</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarContrasenias(ByVal dtsDataset As DataSet, ByVal FiltroIdUsuario As Int32, Optional ByVal FiltroPassWord As String = Nothing) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append("SELECT Orden, Sinonimo, FechaVencimiento,FechaUltAct FROM SE_Historial_Usuario ")
        sblSql.Append(" WHERE IdUsuario = " & objConexion.XtoStr(FiltroIdUsuario))
        'Gcp - Cambios 14460 - Validar "Duración mínima de tiempo, en días, de la contraseña actual del usuario"
        sblSql.Append(" AND Orden > 0")

        'si se pidio filtrar por el pwd
        If Not FiltroPassWord Is Nothing Then
            sblSql.Append(" and sinonimo = " + objConexion.XtoStr(FiltroPassWord))
        End If
        sblSql.Append(" ORDER BY Orden DESC")

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDataset, "SE_Historial_Usuario")
    End Function

    ''' <summary>
    ''' Recupera la fecha del ultimo cambio de contraseña del usuario
    ''' </summary>
    ''' <param name="IDUsuario">ID del usuario que se verifica</param>
    ''' <history>
    ''' [MartinV]          [jueves, 07 de noviembre de 2013]       Modificado  GCP-Cambios 14460
    ''' </history>
    Public Function RecuperarFechaUltimoCambioContrasenia(ByVal IDUsuario As Integer) As Nullable(Of DateTime)
        Dim objRet As Object

        objRet = objConexion.EjecutarEscalar("SELECT MAX(FECHAULTACTUSUARIO) FROM SE_HISTORIAL_USUARIO where IDUSUARIO = " + objConexion.XtoStr(IDUsuario))

        If objRet.Equals(DBNull.Value) OrElse objRet Is Nothing Then
            Return Nothing
        End If

        Return Convert.ToDateTime(objRet)

    End Function

    ''' <summary>
    ''' Recupera la contraseña activa del usuario
    ''' </summary>
    ''' <param name="IDUsuario">Identificador del usuario</param>
    ''' <returns>Clave del usuario</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [lunes, 11 de febrero de 2008]        GCP-Cambios ID: 6410
    ''' </history>
    Public Function RecuperarContraseniaActual(ByVal IDUsuario As Integer) As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return Me.objConexion.EjecutarEscalar("SELECT Sinonimo FROM SE_HISTORIAL_USUARIO WHERE IDUSUARIO = " & IDUsuario.ToString & " AND Orden = 0").ToString

    End Function

    ''' <summary>
    ''' Da de baja un usuario
    ''' </summary>
    ''' <param name="pidUsuario">identificador del usuario</param>
    ''' <returns>cantidad de filas actualizadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	25/10/2005	Created
    ''' </history>
    Public Function EliminarUsuarios(ByVal pidUsuario As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' strsql            : string de consulta sql
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strsql As String

        strsql = "UPDATE SE_USUARIOS SET FechaBaja = " & objConexion.XtoStr(objConexion.FechaServidor()) & _
                " WHERE IdUsuario = " & objConexion.XtoStr(pidUsuario)

        Return objConexion.Ejecutar(strsql)

    End Function

    ''' <summary>
    ''' Activa un usuario
    ''' </summary>
    ''' <param name="pidUsuario">identificador del usuario</param>
    ''' <returns>cantidad de filas actualizadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [AndresR]          [miércoles, 18 de junio de 2008]       Creado GCP-Cambios ID: 6995
    ''' </history>
    Public Function ActivarUsuarios(ByVal pidUsuario As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' strsql            : string de consulta sql
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strsql As String

        strsql = "UPDATE SE_USUARIOS SET FechaBaja = null " & _
                " WHERE IdUsuario = " & objConexion.XtoStr(pidUsuario)

        Return objConexion.Ejecutar(strsql)

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los horarios filtrados por un usuario
    ''' </summary>
    ''' <param name="dtsDatos">dataset que contendrá la informacion retornada</param>
    ''' <param name="strNombrTabla">tabla donde se guardarán los resultados de la consulta</param>
    ''' <param name="FiltroIdUsuario">filtor opcional por identificador de usuario</param>
    ''' <returns>cnatidad de filas devueltas por la consulta</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [angell]	       26/10/2005	Created
    ''' [AndresR]          [martes, 06 de noviembre de 2007]       GCP-Cambios ID: 4256
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarHorariosUsuario(ByVal dtsDatos As DataSet, ByVal strNombrTabla As String, Optional ByVal FiltroIdUsuario As Int32 = -1) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        sblSql.Append(" SELECT IdDia, IdHorario, idusuario  FROM SE_Horarios_Usuario WHERE 1=1 ")

        If Not FiltroIdUsuario.Equals(-1) Then
            sblSql.Append(" and IdUsuario = " & objConexion.XtoStr(FiltroIdUsuario))
        Else
            sblSql.Append(" and idUsuario = 0 ")
        End If

        sblSql.Append(" order by IdDia ")

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDatos, strNombrTabla)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera las terminales prohibidas de usuarios, filtrada por usuarios
    ''' </summary>
    ''' <param name="dtsDatos">dataset que contendrá la informacion retornada</param>
    ''' <param name="strNombrTabla">tabla donde se guardarán los resultados de la consulta</param>
    ''' <param name="FiltroIdUsuario">filtor opcional por identificador de usuario</param>
    ''' <returns>cnatidad de filas devueltas por la consulta</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarTerminalesProhibidasAUsuarios(ByVal dtsDatos As DataSet, ByVal strNombrTabla As String, Optional ByVal FiltroIdUsuario As Int32 = -1) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        sblSql.Append("SELECT IdTerminal FROM SE_Term_Usuario WHERE 1=1 ")

        If Not FiltroIdUsuario.Equals(-1) Then
            sblSql.Append(" AND IdUsuario = " & objConexion.XtoStr(FiltroIdUsuario))
        End If

        Return objConexion.Ejecutar(sblSql.ToString(), dtsDatos, strNombrTabla)
    End Function

    ''' <summary>
    ''' Permite eliminar una determinada terminal asociada a usuarios
    ''' </summary>
    ''' <param name="IDTerminal">Identificador de la terminal</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function EliminarTerminalProhibida(ByVal IDTerminal As Short) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return objConexion.Ejecutar("DELETE FROM SE_Term_Usuario WHERE IdTerminal = " & IDTerminal.ToString)

    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina las tareas de un usuario
    ''' </summary>
    ''' <param name="idRol">identificador del rol</param>
    ''' <param name="idTarea">identificador de la tarea</param>
    ''' <returns>cantidad de filas afectadas.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	26/10/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function EliminarUnaTareaUsuario(ByVal IdRol As Int32, Optional ByVal idtarea As Int32 = -1) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql            : string de conexion sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        sblsql.Append(" DELETE FROM SE_TAREAS_USUARIO WHERE idrol = " + objConexion.XtoStr(IdRol))
        'si se pidio el filtro por tarea, filtramos entonces
        If Not idtarea.Equals(-1) Then
            sblsql.Append(" and idtarea = " + objConexion.XtoStr(idtarea))
        End If
        Return objConexion.Ejecutar(sblsql.ToString())
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los usuarios de la tabla usuarios, con informacion del documento y el area
    ''' </summary>
    ''' <param name="dtsDatset">dataset donde se guardara informacion</param>
    ''' <param name="strNombreTabla">tabla de datset para guardar los datos</param>
    ''' <returns>cantidad de filas devueltas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	25/10/2005	Created
    ''' [LucianoP]          [viernes, 11 de mayo de 2007]       GCP-Cambios ID: 5026
    ''' [AndresR]           [martes, 06 de noviembre de 2007]   GCP-Cambios ID: 4256
    ''' [AndresR]           [viernes, 09 de noviembre de 2007]  GCP-Cambios ID: 6070 y 6071 
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RetornarUsuarios(ByVal dtsDatset As DataSet, ByVal strNombreTabla As String, _
                                     Optional ByVal pFiltroUsuario As String = Nothing, _
                                     Optional ByVal pFiltroIdUsuarioDistintoA As Int32 = -1, _
                                     Optional ByVal pblnFiltroActivos As Boolean = False, _
                                     Optional ByVal pFiltroIdUsuario As Int32 = -1, _
                                     Optional ByVal pblnFiltroSinAcceso As Boolean = False, _
                                     Optional ByVal pFiltroFechaUltUsoCta As String = "", _
                                     Optional ByVal FiltroDirecto As String = "", Optional ByVal pParaReporte As Boolean = False) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql        : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder


        If pParaReporte Then
            sblsql.Append("SELECT * FROM vRecuperarDatosParaReporteRolesXUsuarios")
        Else
            sblsql.Append("SELECT TOP 50 IdUsuario, Nombres, Usuario,su.IdArea, NombreArea, Domicilio, TipoAbreviado, NroDocumento, ")
            sblsql.Append(" ForzarCambio, ForzarCambio As ForzarCambioDes, CtaBloqueada, CtaBloqueada As CtaBloqueadaDes, ")
            sblsql.Append(" CtaBloqueada As CtaBloqueadaDesLetra, Comentario, FechaAlta, FechaBaja, FechaBloqueo,ALIAS_USUARIO ")
            sblsql.Append(" ,SIST_KAREAS.FICTICIA, su.CANTINTINVUSOCTA, su.FechaUltUsoCta, su.Email ")
            sblsql.Append("FROM SE_USUARIOS su, SIST_KDOCUMENTOS, SIST_KAREAS ")
            sblsql.Append(" WHERE SIST_KAREAS.IdArea = su.IdArea ")
            sblsql.Append(" AND SIST_KDOCUMENTOS.IdTipoDoc = su.IDTIPODOC AND ")
            sblsql.Append("SIST_KDOCUMENTOS.DocBaja ='N' ")

            If Not pFiltroUsuario Is Nothing Then
                sblsql.Append("  AND su.Usuario = " + objConexion.XtoStr(pFiltroUsuario))
            End If
            If Not pFiltroIdUsuarioDistintoA.Equals(-1) Then
                sblsql.Append(" AND su.IdUsuario <> " + objConexion.XtoStr(pFiltroIdUsuarioDistintoA))
            End If
            If Not pFiltroIdUsuario.Equals(-1) Then
                sblsql.Append(" AND su.IdUsuario = " + objConexion.XtoStr(pFiltroIdUsuario))
            End If

            'si se pidio que se filtre por usuarios activos.
            If pblnFiltroActivos Then
                sblsql.Append(" and su.fechabaja is null ")
            End If

            'si se pidio que se filtre por usuarios sin acceso
            If pblnFiltroSinAcceso Then
                sblsql.Append(" AND (FechaBaja IS NOT NULL OR FechaBloqueo IS NOT NULL) ")
            End If

            'si se pidio que se filtre por fecha última conexion
            If Not pFiltroFechaUltUsoCta.Equals(String.Empty) Then
                sblsql.Append(" and FechaUltUsoCta <= " & objConexion.XtoStr(CDate(pFiltroFechaUltUsoCta)))
            End If

            If Not String.IsNullOrEmpty(FiltroDirecto) Then
                sblsql.Append(" AND " + FiltroDirecto)
            End If
        End If


        Dim _ret As Integer = objConexion.Ejecutar(sblsql.ToString(), dtsDatset, strNombreTabla)

        dtsDatset.Tables(strNombreTabla).DefaultView.Sort = "Usuario ASC"

        Return _ret


    End Function

    ''' [MiguelP]                31/08/2016                  TFS wi : 7674 - Se creo un nuevo metodo para no modificar el original RetornoarUsuarios(). Este metodo recibe los parametros de la pantalla de usuarios
    Public Function RetornarUsuariosConParametros(ByVal dtsDatset As DataSet, ByVal strNombreTabla As String, _
                                     Optional ByVal pFiltro As String = Nothing, _
                                     Optional ByVal pblnArea As Boolean = False, _
                                     Optional ByVal pblnNombre As Boolean = False, _
                                     Optional ByVal pblnSubCadena As Boolean = False, _
                                     Optional ByVal pblnUsu As Boolean = False, _
                                     Optional ByVal pFiltroBaja As String = "") As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql        : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        sblsql.Append("SELECT TOP 1000 IdUsuario, Nombres, Usuario,su.IdArea, NombreArea, Domicilio, TipoAbreviado, NroDocumento, ")
        sblsql.Append(" ForzarCambio, ForzarCambio As ForzarCambioDes, CtaBloqueada, CtaBloqueada As CtaBloqueadaDes, ")
        sblsql.Append(" CtaBloqueada As CtaBloqueadaDesLetra, Comentario, FechaAlta, FechaBaja, FechaBloqueo,ALIAS_USUARIO ")
        sblsql.Append(" ,SIST_KAREAS.FICTICIA, su.CANTINTINVUSOCTA, su.FechaUltUsoCta, su.Email ")
        sblsql.Append("FROM SE_USUARIOS su, SIST_KDOCUMENTOS, SIST_KAREAS ")
        sblsql.Append(" WHERE SIST_KAREAS.IdArea = su.IdArea ")
        sblsql.Append(" AND SIST_KDOCUMENTOS.IdTipoDoc = su.IDTIPODOC AND ")
        sblsql.Append("SIST_KDOCUMENTOS.DocBaja ='N' ")


        If Not pFiltro Is Nothing AndAlso pFiltro.Trim.Length <> 0 Then

            If pblnUsu Then
                If pblnSubCadena Then
                    sblsql.Append("  AND su.Usuario LIKE '%" + pFiltro + "%'")
                Else
                    sblsql.Append("  AND su.Usuario LIKE '").Append(pFiltro).Append("%'")
                End If
            End If

            If pblnNombre Then
                If pblnSubCadena Then
                    sblsql.Append("  AND su.Nombres LIKE '%" + pFiltro + "%'")
                Else
                    sblsql.Append("  AND su.Nombres = " + objConexion.XtoStr(pFiltro))
                End If
            End If

            If pblnArea Then
                If pblnSubCadena Then
                    sblsql.Append("  AND SIST_KAREAS.NOMBREAREA LIKE '%" + pFiltro + "%'")
                Else
                    sblsql.Append("  AND SIST_KAREAS.NOMBREAREA = " + objConexion.XtoStr(pFiltro))
                End If
            End If
        End If
        If pFiltroBaja <> "TODOS" Then
            If pFiltroBaja = "SI" Then
                sblsql.Append("  AND su.FECHABAJA is not null ")
            ElseIf pFiltroBaja = "NO" Then
                sblsql.Append("  AND su.FECHABAJA is null ")
            End If
        End If

        Dim _ret As Integer = objConexion.Ejecutar(sblsql.ToString(), dtsDatset, strNombreTabla)

        dtsDatset.Tables(strNombreTabla).DefaultView.Sort = "Usuario ASC"

        Return _ret


    End Function
    ''' <summary>
    ''' Retorna los usuarios de la tabla usuarios, con informacion del documento y el area
    ''' </summary>
    ''' <param name="dtsDatset">dataset donde se guardara informacion</param>
    ''' <param name="strNombreTabla">tabla de dataset para guardar los datos</param>
    ''' <param name="pFiltroFechaUltUsoCta">Fecha último uso de cuenta</param>
    ''' <param name="LapsoInactividad">String que indica si traer FechaUltUsoCta menor a 30 dias o mayor</param>
    ''' <returns>cantidad de filas devueltas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]           [viernes, 09 de noviembre de 2007]  creado GCP-Cambios ID: 6071
    ''' [AndresR]           [miércoles, 30 de abril de 2008]    GCP-Cambios ID: 6617
    ''' </history>
    Public Function RetornarUsuariosFechaUltUsoCta(ByVal dtsDatset As DataSet, ByVal strNombreTabla As String, _
                                                   ByVal pFiltroFechaUltUsoCta As Date, _
                                                   ByVal LapsoInactividad As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql        : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        With sblsql
            .Append("SELECT IdUsuario, Nombres, Usuario, NombreArea, Domicilio, TipoAbreviado, NroDocumento, ")
            .Append(" ForzarCambio, ForzarCambio As ForzarCambioDes, CtaBloqueada, CtaBloqueada As CtaBloqueadaDes, ")
            .Append(" CtaBloqueada As CtaBloqueadaDesLetra, Comentario, FechaAlta, FechaBaja, FechaBloqueo,ALIAS_USUARIO ")
            .Append(" ,SIST_KAREAS.FICTICIA, su.CANTINTINVUSOCTA, su.FechaUltUsoCta, ")

            If objConexion.ConectadoA = "Oracle" Then
                .Append(" nvl(to_char(TRUNC(" & objConexion.XtoStr(pFiltroFechaUltUsoCta) & " - su.FechaUltUsoCta)),'N/A') as LapsoInactividad ")
            ElseIf objConexion.ConectadoA = "SQLServer" Then
                .Append(" isnull(CONVERT(nvarchar(12),DATEDIFF(day,su.FechaUltUsoCta," & objConexion.XtoStr(pFiltroFechaUltUsoCta) & ")),'N/A') as LapsoInactividad ")
            End If

            .Append(" FROM SE_USUARIOS su, SIST_KDOCUMENTOS, SIST_KAREAS ")
            .Append(" WHERE SIST_KAREAS.IdArea = su.IdArea ")
            .Append(" AND SIST_KDOCUMENTOS.IdTipoDoc = su.IDTIPODOC AND ")
            .Append(" SIST_KDOCUMENTOS.DocBaja ='N' ")

            'si se pidio que se filtre por lapso de Inactividad
            If Not LapsoInactividad.Equals(String.Empty) Then
                If objConexion.ConectadoA = "Oracle" Then
                    .Append(" and TRUNC(" & objConexion.XtoStr(pFiltroFechaUltUsoCta) & " - nvl(su.FechaUltUsoCta,'01/01/1800')) " & LapsoInactividad)
                ElseIf objConexion.ConectadoA = "SQLServer" Then
                    .Append(" and DATEDIFF(DAY, ISNULL(su.FechaUltUsoCta,'01/01/1800'), " & objConexion.XtoStr(pFiltroFechaUltUsoCta) & ") " & LapsoInactividad)
                End If
            Else
                If objConexion.ConectadoA = "Oracle" Then
                    .Append(" and nvl(su.FechaUltUsoCta,'01/01/1800') <= " & objConexion.XtoStr(pFiltroFechaUltUsoCta))
                ElseIf objConexion.ConectadoA = "SQLServer" Then
                    .Append(" and isnull(su.FechaUltUsoCta,'01/01/1800') <= " & objConexion.XtoStr(pFiltroFechaUltUsoCta))
                End If
            End If

            .Append("  ORDER BY Usuario ")

        End With

        Return objConexion.Ejecutar(sblsql.ToString(), dtsDatset, strNombreTabla)

    End Function


    ''' <summary>
    ''' Retorna un usuario de la tabla usuarios, con informacion del documento y el area
    ''' </summary>
    ''' <param name="dtsDatset">dataset donde se guardara informacion</param>
    ''' <param name="strNombreTabla">tabla de datset para guardar los datos</param>
    ''' <param name="pIDUsuario">identificador del usuario a traer</param>
    ''' <returns>cantidad de filas devueltas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[angell]	25/10/2005	Created
    ''' [LucianoP]          [viernes, 11 de mayo de 2007]       GCP-Cambios ID: 5026
    ''' [AndresR]           [martes, 29 de abril de 2008]       GCP-Cambios ID: 6795
    ''' </history>
    Public Function RetornarUsuarios(ByVal dtsDatset As DataSet, ByVal strNombreTabla As String, ByVal pIDUsuario As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql        : string de consulta sql.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblsql As New System.Text.StringBuilder

        sblsql.Append("SELECT IdUsuario, Nombres, Usuario, NombreArea, Domicilio, su.IDTipoDoc, su.IDArea, TipoAbreviado, NroDocumento, ")
        sblsql.Append(" ForzarCambio, ForzarCambio As ForzarCambioDes, CtaBloqueada, CtaBloqueada As CtaBloqueadaDes, ")
        sblsql.Append(" CtaBloqueada As CtaBloqueadaDesLetra, Comentario, FechaAlta, FechaBaja, FechaBloqueo, ALIAS_USUARIO")
        sblsql.Append(" ,SIST_KAREAS.FICTICIA, su.CANTINTINVUSOCTA, su.Email ")
        sblsql.Append("FROM SE_USUARIOS su, SIST_KDOCUMENTOS, SIST_KAREAS ")
        sblsql.Append(" WHERE SIST_KAREAS.IdArea = su.IdArea ")
        sblsql.Append(" AND SIST_KDOCUMENTOS.IdTipoDoc = su.IDTIPODOC AND ")
        sblsql.Append("SIST_KDOCUMENTOS.DocBaja ='N' ")
        sblsql.Append("  AND su.idUsuario = " + objConexion.XtoStr(pIDUsuario))
        sblsql.Append(" ORDER BY su.Usuario ASC")

        Return objConexion.Ejecutar(sblsql.ToString(), dtsDatset, strNombreTabla)

    End Function

    Public Function Recuperar_UsuariosXSistema(ByVal IDSistema As Int32) As EE.dtsUsuarios
        Dim dtsRetorno As New EE.dtsUsuarios
        Dim sblSql As New System.Text.StringBuilder

        With sblSql
            .Append("SELECT DISTINCT U.IDUSUARIO, U.NOMBRES, U.CTABLOQUEADA FROM SE_USUARIOS U")
            .Append(" INNER JOIN SE_TAREAS_USUARIO TU ON TU.IDUSUARIO = U.IDUSUARIO")
            .Append(" INNER JOIN SE_TAREAS T ON T.IDTAREA = TU.IDTAREA")
            .Append(" INNER JOIN SE_SIST_HABILITADOS SH ON SH.IDSISTEMA = T.IDSISTEMA")
            .Append(" WHERE SH.SISTEMAHABILITADO = " + Me.objConexion.XtoStr("S"))
            .Append(" AND SH.IDSISTEMA NOT IN(Select IDSISTEMA From SE_SIST_BLOQUEADOS Where IDSISTEMA = T.IDSISTEMA)")
            .Append(" AND SH.IDSISTEMA = " + Me.objConexion.XtoStr(IDSistema))
            .Append(" AND U.FechaBaja IS NULL")
            Me.objConexion.Ejecutar(.ToString, CType(dtsRetorno, System.Data.DataSet), dtsRetorno.UsuariosXSistema.TableName)
        End With

        Return dtsRetorno

    End Function

End Class
