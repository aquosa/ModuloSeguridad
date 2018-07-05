Imports EE = Fachada.Seguridad
Public Class Terminal
    Inherits PadreSistema

    ''' <summary>
    ''' Recupera las terminales activas
    ''' </summary>
    ''' <param name="dtsTerminales">DataSet en el cual se retornan las terminales</param>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 
    ''' [IvanR]         [Jueves, 5 de Agosto de 2010]           Modificado     GCP-Cambio ID:9287
    ''' </history>
    Public Sub RecuperarTerminales(ByVal dtsTerminales As DataSet, ByVal DadaDeBaja As Boolean)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        If (DadaDeBaja) Then
            With sblSql
                .Append("SELECT IdTerminal, CodTerminal, NombreNetBios As NombreComputadora, ")
                .Append("NombreArea, SE_Terminales.IdArea, UsoHabilitado, ")
                .Append("ModeloProcesador, CantMemoriaRam, TamanioDisco, ModeloMonitor, ModeloAcelVideo, ")
                .Append("DescAdicional, ORIGENACTUALIZACION FROM SE_Terminales, SIST_KAREAS WHERE SIST_KAREAS.IdArea = SE_Terminales.IdArea AND SIST_KAREAS.Baja = ")
                .Append(objConexion.XtoStr("N"c))
                '[MiguelP]          21/10/2014      Cambio  GCP-
                '.Append(" ORDER BY NombreNetBios ASC")
                .Append(" ORDER BY CodTerminal ASC")
            End With
        Else 'GCP-Cambio ID:9287
            With sblSql
                .Append("SELECT IdTerminal, CodTerminal, NombreNetBios As NombreComputadora, ")
                .Append("CASE A.BAJA WHEN 'S' THEN '' ELSE A.NOMBREAREA END AS NombreArea")
                .Append(", T.IdArea, UsoHabilitado")
                .Append(", ModeloProcesador, CantMemoriaRam, TamanioDisco, ModeloMonitor, ModeloAcelVideo, DescAdicional, ORIGENACTUALIZACION")
                .Append(" FROM SE_Terminales T")
                .Append(" LEFT JOIN SIST_KAREAS A")
                .Append(" ON A.IdArea = T.IdArea")
                '[MiguelP]          21/10/2014      Cambio  GCP-
                '.Append(" ORDER BY NombreNetBios ASC")
                .Append(" ORDER BY CodTerminal ASC")
            End With
        End If
        objConexion.Ejecutar(sblSql.ToString, dtsTerminales, "SE_TERMINALES")

    End Sub

    ''' <summary>
    ''' Recupera las terminales permitidas para un usuario
    ''' </summary>
    ''' <param name="IdUsuario">Identificador de Usuario</param>
    ''' <param name="dtsRetorno">Dataset con los datos recuperados</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [AndresR]          [miércoles, 07 de noviembre de 2007]       Creado GCP-Cambios ID: 4256
    ''' </history>
    Public Function RecuperarTerminalesPermitidasUsuario(ByVal dtsRetorno As System.Data.DataSet, ByVal IdUsuario As Integer) As Integer
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder

        With sblSql

            .Append("SELECT Ter.idterminal, " & IdUsuario & " as IDUSUARIO, Ter.CODTERMINAL, Ter.NOMBRENETBIOS, Ter.IDAREA, Kar.NombreArea ")
            .Append(" FROM SE_TERMINALES Ter ")
            .Append(" INNER JOIN SIST_KAREAS KAr on Ter.IDAREA = Kar.IDAREA ")
            .Append(" WHERE ")
            .Append(" Ter.IDTERMINAL NOT IN (SELECT IDTERMINAL FROM SE_Term_Usuario where IDUSUARIO = " & IdUsuario & ")")

        End With

        Return Me.objConexion.Ejecutar(sblSql.ToString, dtsRetorno, "SE_Term_Usuario")

    End Function




    ''' <summary>
    ''' Recupera las terminales activas por area y por habilitadas
    ''' </summary>
    ''' <param name="dtsTerminales">DataSet en el cual se retornan las terminales</param>
    ''' <param name="Habilitada">Me indica si deseo las terminales habilitadas</param>
    ''' <param name="IDArea">ID del area que deseo obtener las terminales</param>
    ''' <param name="TodasLasAreas">Si deseo todas las areas</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [viernes, 27 de octubre de 2006]       Creado
    ''' </history>
    Public Sub RecuperarTerminalesPorAreaYHabilitadas(ByVal dtsTerminales As DataSet, ByVal IDArea As Integer, _
                    ByVal Habilitada As String, ByVal TodasLasAreas As Boolean)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql : String Builder que se utiliza para armar la sentencia SQL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim strWhere As String = ""


        With sblSql
            .Append("SELECT IdTerminal, CodTerminal, NombreNetBios As NombreComputadora, ")
            .Append("NombreArea, SE_Terminales.IdArea, UsoHabilitado, ")
            .Append("ModeloProcesador, CantMemoriaRam, TamanioDisco, ModeloMonitor, ModeloAcelVideo, ")
            .Append("DescAdicional, ORIGENACTUALIZACION FROM SE_Terminales, SIST_KAREAS ")

            strWhere &= " SIST_KAREAS.IdArea = SE_Terminales.IdArea And SIST_KAREAS.Baja = " & objConexion.XtoStr("N"c)

            If Not TodasLasAreas Then
                strWhere &= " AND SE_Terminales.IDArea = " & IDArea.ToString
            End If

            If Habilitada <> "" Then
                strWhere &= " AND SE_Terminales.UsoHabilitado = '" & Habilitada & "'"
            End If

            If strWhere <> "" Then
                .Append(" WHERE " & strWhere)
            End If

            .Append(" ORDER BY NombreNetBios ASC")

        End With

        objConexion.Ejecutar(sblSql.ToString, dtsTerminales, "SE_TERMINALES")

    End Sub

    ''' <summary>
    ''' Permite el ingreso de una nueva terminal
    ''' </summary>
    ''' <param name="dtsDatos">DataSet con los datos de la terminal a ingresar</param>
    ''' <returns>Identificador de la nueva terminal</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
    Public Function IngresarTerminal(ByVal dtsDatos As EE.dtsTerminales) As Short
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        'objID      : Identificador de la terminal
        'shtID      : Identificador de la terminal
        'rowTerminal: Objeto DataRow que contiene los datos a ingresar
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim objID As System.Object, shtID As Short = 0
        Dim rowTerminal As EE.dtsTerminales.SE_TERMINALESRow = dtsDatos.SE_TERMINALES(0)

        objID = objConexion.EjecutarEscalar("SELECT MAX(IDTerminal) FROM SE_TERMINALES")
        If System.Convert.IsDBNull(objID) Then
            shtID = 1
        Else
            shtID = System.Convert.ToInt16(objID) + 1S
        End If

        With sblSql
            .Append("INSERT INTO SE_Terminales(IdTerminal, CodTerminal, NombreNetBios, IdArea, UsoHabilitado, ")
            .Append("ModeloProcesador, CantMemoriaRam, TamanioDisco, ModeloMonitor, ModeloAcelVideo, ")
            .Append("DescAdicional, ORIGENACTUALIZACION ) VALUES ( ")
            .Append(shtID) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.CODTERMINAL)) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.NOMBRECOMPUTADORA)) : .Append(","c)
            .Append(rowTerminal.IDAREA) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.USOHABILITADO)) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.MODELOPROCESADOR)) : .Append(","c)
            .Append(rowTerminal.CANTMEMORIARAM) : .Append(","c)
            .Append(rowTerminal.TAMANIODISCO) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.MODELOMONITOR))
            .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.MODELOACELVIDEO)) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.DESCADICIONAL)) : .Append(","c)
            .Append(objConexion.XtoStr(rowTerminal.ORIGENACTUALIZACION)) : .Append(")"c)

        End With

        If objConexion.Ejecutar(sblSql.ToString) > 0 Then
            Return shtID
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' Actualiza los datos de una determinada terminal
    ''' </summary>
    ''' <param name="dtsDatos">DataSet que contiene los datos de la terminal a actualizar</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
    Public Function ActualizarTerminal(ByVal dtsDatos As EE.dtsTerminales) As Short
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'sblSql     : String Builder que se utiliza para armar la sentencia SQL
        'rowTerminal: Objeto DataRow que contiene los datos de la terminal a
        '             actualizar
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder
        Dim rowTerminal As EE.dtsTerminales.SE_TERMINALESRow = dtsDatos.SE_TERMINALES(0)

        With sblSql
            .Append("UPDATE SE_Terminales SET CodTerminal = ") : .Append(objConexion.XtoStr(rowTerminal.CODTERMINAL.Trim))
            .Append(", NombreNetBios = ") : .Append(objConexion.XtoStr(rowTerminal.NOMBRECOMPUTADORA.Trim))
            .Append(", IdArea = ") : .Append(rowTerminal.IDAREA)
            .Append(", UsoHabilitado = ") : .Append(objConexion.XtoStr(rowTerminal.USOHABILITADO))
            .Append(", ModeloProcesador = ") : .Append(objConexion.XtoStr(rowTerminal.MODELOPROCESADOR.Trim))
            .Append(", CantMemoriaRam = ") : .Append(rowTerminal.CANTMEMORIARAM)
            .Append(", TamanioDisco = ") : .Append(rowTerminal.TAMANIODISCO)
            If Not rowTerminal.IsMODELOMONITORNull Then _
                .Append(", ModeloMonitor = ") : .Append(objConexion.XtoStr(rowTerminal.MODELOMONITOR.Trim))

            If Not rowTerminal.IsMODELOACELVIDEONull Then _
                .Append(", ModeloAcelVideo = ") : .Append(objConexion.XtoStr(rowTerminal.MODELOACELVIDEO.Trim))

            If Not rowTerminal.IsDESCADICIONALNull Then _
                .Append(", DescAdicional = ") : .Append(objConexion.XtoStr(rowTerminal.DESCADICIONAL.Trim))

            .Append(", ORIGENACTUALIZACION = ") : .Append(Me.objConexion.XtoStr(rowTerminal.ORIGENACTUALIZACION))

            .Append(" WHERE IdTerminal = ") : .Append(rowTerminal.IDTERMINAL)
        End With

        Return CType(objConexion.Ejecutar(sblSql.ToString), Short)

    End Function

    ''' <summary>
    ''' Permite eliminar una terminal
    ''' </summary>
    ''' <param name="IDTerminal">Identificador de la terminal</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function EliminarTerminal(ByVal IDTerminal As Short) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Return objConexion.Ejecutar("DELETE FROM SE_Terminales WHERE IdTerminal = " & IDTerminal.ToString)

    End Function
End Class
