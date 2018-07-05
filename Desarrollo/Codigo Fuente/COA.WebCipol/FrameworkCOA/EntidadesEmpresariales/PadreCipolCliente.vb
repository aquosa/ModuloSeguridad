<Serializable()> _
Public Class PadreCipolCliente
    Implements ICIPOL
    Implements System.Security.Principal.IPrincipal

    Protected mdtsMenuSistemas As System.Data.DataSet = Nothing
    Protected mobjIdentidad As Security.Principal.GenericIdentity
    Protected mintIDUsuario As Integer = -1
    Protected mdtsTareasAutoriz As System.Data.DataSet = Nothing
    Protected mstrLogin As String = String.Empty
    Protected mstrIV As String = String.Empty, mstrKey As String = String.Empty
    Protected mobjOtrosDatos As New System.Collections.Specialized.NameValueCollection
    'Protected mstrOtrosDatos As String = String.Empty
    Protected mstrMensajeError As String = String.Empty
    Protected mstrNombreyApellido As String = String.Empty
    Protected mshtIDSistemaDefault As Short = -1
    Protected mstrDominio As String = "no definido"
    Protected mblnSeguridad_SoloDominio As Boolean
    Protected mstrOrganizacion As String = "no definido"
    Protected mshtInactividad As Short = 0
    Protected mdtmFechaServidor As DateTime = Date.Today.Date
    Protected mstrAliasUsuario As String = String.Empty


    Public Sub New()
    End Sub

    Public Sub New(ByVal pmdtsMenuSistemas As System.Data.DataSet, _
        ByVal pmobjIdentidad As Security.Principal.GenericIdentity, _
        ByVal pmintIDUsuario As Integer, _
        ByVal pmdtsTareasAutoriz As System.Data.DataSet, _
        ByVal pmstrLogin As String, _
        ByVal pmstrIV As String, ByVal pmstrKey As String, _
        ByVal pmstrOtrosDatos As System.Collections.Specialized.NameValueCollection, _
        ByVal pmstrMensajeError As String, _
        ByVal pmstrNombreyApellido As String, _
        ByVal pmshtIDSistemaDefault As Short, _
        ByVal pmstrDominio As String, _
        ByVal pmstrOrganizacion As String, _
        ByVal pmshtInactividad As Short, ByVal pmstrAliasUsuario As String, ByVal Seguridad_SoloDominio As Boolean)

        mdtsMenuSistemas = pmdtsMenuSistemas
        mobjIdentidad = pmobjIdentidad
        mintIDUsuario = pmintIDUsuario
        mdtsTareasAutoriz = pmdtsTareasAutoriz
        mstrLogin = pmstrLogin
        mstrKey = pmstrKey
        mstrIV = pmstrIV
        For intI As Integer = 0 To pmstrOtrosDatos.Keys.Count - 1
            mobjOtrosDatos.Add(pmstrOtrosDatos.Keys(intI), pmstrOtrosDatos.Item(intI))
        Next
        mstrMensajeError = pmstrMensajeError
        mstrNombreyApellido = pmstrNombreyApellido
        mshtIDSistemaDefault = pmshtIDSistemaDefault
        mstrDominio = pmstrDominio
        mstrOrganizacion = pmstrOrganizacion
        mshtInactividad = pmshtInactividad
        mstrAliasUsuario = pmstrAliasUsuario     '[gustavom]	14/06/2007	GCP-Cambios ID: 5208
        mblnSeguridad_SoloDominio = Seguridad_SoloDominio 'Gustavom  07/02/2008  GCP-Cambios ID: 6410
    End Sub

    Public Property FechaServidor() As DateTime
        Get
            Return mdtmFechaServidor
        End Get
        Set(ByVal value As DateTime)
            mdtmFechaServidor = value
        End Set
    End Property
    Public ReadOnly Property MensajeError() As String Implements ICIPOL.MensajeError
        Get
            Return Me.mstrMensajeError
        End Get
    End Property

    Public ReadOnly Property IDUsuario() As Integer Implements ICIPOL.IDUsuario
        Get
            Return Me.mintIDUsuario
        End Get
    End Property

    Public ReadOnly Property Login() As String Implements ICIPOL.Login
        Get
            Return Me.mstrLogin
        End Get
    End Property

    Public ReadOnly Property NombreYApellido() As String Implements ICIPOL.NombreYApellido
        Get
            Return Me.mstrNombreyApellido
        End Get
    End Property

    Public Property IDSistemaActual() As Short Implements ICIPOL.IDSistemaActual
        Get
            Return Me.mshtIDSistemaDefault
        End Get
        Set(ByVal Value As Short)
            'Verifico si el sistema existe entre los permitidos por el usuario
            Dim dtrFila() As System.Data.DataRow
            dtrFila = Me.mdtsMenuSistemas.Tables("SE_SIST_HABILITADOS").Select("IDSistema = " & Value.ToString)
            If dtrFila.GetUpperBound(0) >= 0 Then
                Me.mshtIDSistemaDefault = Value
            End If
        End Set
    End Property
    Public ReadOnly Property SistemaActual() As String
        Get
            Return RecuperarValorSistema("DescSistema")
        End Get
    End Property

    Public ReadOnly Property CodigoSistema() As String
        Get
            Return Me.RecuperarValorSistema("CodSistema")
        End Get
    End Property

    Public ReadOnly Property Seguridad_SoloDominio() As Boolean
        Get
            Return Me.mblnSeguridad_SoloDominio
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los datos del sistema actual
    ''' </summary>
    '''<param name="Campo">Campo de la tabla SE_SIST_HABILITADOS del cual se requiere el valor</param>
    '''<returns> Valor del campo que se busca </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Function RecuperarValorSistema(ByVal Campo As String) As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Me.mdtsMenuSistemas Is Nothing Then Return ""

        'Retorna el nombre del sistema
        Dim dtrFila() As System.Data.DataRow
        dtrFila = Me.mdtsMenuSistemas.Tables("SE_SIST_HABILITADOS").Select("IDSistema = " & Me.mshtIDSistemaDefault.ToString)
        If dtrFila.GetUpperBound(0) = -1 Then
            Return String.Empty
        Else
            Return CType(dtrFila(0).Item(Campo), String)
        End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los items de menu permitidos por un usuario
    ''' </summary>
    '''<param name="IDSistema">Identificador del sistema</param>
    '''<returns> DataSet con los items de menú </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ObtenerOpcionesMenu(ByVal IDSistema As Short) As dtsSeMenues
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsMenu    : DataSet de retorno
        'rowMenues  : Matriz que representan los items de menú de un determinado 
        '             sistema
        'rowFila    : Objeto DataRow que se utiliza para recorrer la matríz
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsMenu As New dtsSeMenues, rowMenues() As System.Data.DataRow
        Dim rowFila As System.Data.DataRow

        rowMenues = Me.mdtsMenuSistemas.Tables("SE_MENUES").Select("IDSistemas = " & IDSistema.ToString)

        If rowMenues.GetUpperBound(0) >= 0 Then
            For Each rowFila In rowMenues
                dtsMenu.Tables("se_menues").ImportRow(rowFila)
            Next
        End If

        Return dtsMenu

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna los sistema en los cuales el usuario tiene acceso
    ''' </summary>
    '''<returns> DataSet con los sistemas permitidos por el usuario </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [gustavom]	31/08/2005	Created
    ''' [LeandroF]          [lunes, 21 de octubre de 2013]       Se agregaron las columnas PAGINAPORDEFECTO  
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ObtenerSistemas() As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsRet : DataSet de retorno
        'rowPerm: Objeto DataRow que representa el sistema permitido
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsRet As New dtsSeMenues, rowFila As System.Data.DataRow
        Dim rowPerm As dtsSeMenues.SE_SIST_HABILITADOSRow

        For Each rowFila In Me.mdtsMenuSistemas.Tables("SE_SIST_HABILITADOS").Rows
            rowPerm = dtsRet.SE_SIST_HABILITADOS.NewSE_SIST_HABILITADOSRow
            With rowPerm
                .IDSISTEMA = CType(rowFila.Item("IDSistema"), Short)
                .DESCSISTEMA = rowFila.Item("DescSistema").ToString.TrimEnd
                If rowFila.Table.Columns.Contains("CodSistema") Then
                    .CodSistema = rowFila.Item("CodSistema").ToString.TrimEnd
                End If
                .ICONO = rowFila.Item("Icono").ToString.TrimEnd
                .NOMBREEXEC = rowFila.Item("NombreExec").ToString.TrimEnd
                .PAGINAPORDEFECTO = rowFila.Item("PAGINAPORDEFECTO").ToString.Trim
            End With
            dtsRet.SE_SIST_HABILITADOS.AddSE_SIST_HABILITADOSRow(rowPerm)
        Next

        Return dtsRet

    End Function

    Public Sub OtrosDatos(ByVal Clave As String, ByVal Valor As String) Implements ICIPOL.OtrosDatos
        'si la clave es nula, salimos
        If Clave Is Nothing Then Return

        'agregamos el objeto
        'Si la clave existe
        If mobjOtrosDatos.GetValues(Clave) Is Nothing Then
            mobjOtrosDatos.Add(Clave, Valor)
        Else
            mobjOtrosDatos.Remove(Clave)
            mobjOtrosDatos.Add(Clave, Valor)
        End If


    End Sub

    Public Function OtrosDatos(ByVal Clave As String) As String Implements ICIPOL.OtrosDatos
        If Clave Is Nothing Then Return String.Empty

        Return mobjOtrosDatos.Item(Clave)

    End Function

    Public Sub OtrosDatosEliminar(ByVal Clave As String) Implements ICIPOL.OtrosDatosEliminar
        'si la clave es nula, salimos
        If Clave Is Nothing Then Return

        mobjOtrosDatos.Remove(Clave)

    End Sub

    Public ReadOnly Property Identity() As System.Security.Principal.IIdentity Implements System.Security.Principal.IPrincipal.Identity
        Get
            Return mobjIdentidad
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Determina si el usuario posee una determinada tarea primitiva
    ''' </summary>
    ''' <param name="role">Identificador de la tarea primitiva</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [gustavom]	31/08/2005	Created
    ''' [IvanR] 20/05/2010 Modificado GCP-Cambios ID: 9049
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function IsInRole(ByVal role As String) As Boolean Implements System.Security.Principal.IPrincipal.IsInRole
        Dim intIDTarea As Integer
        Dim rowTarea() As System.Data.DataRow

        If role Is Nothing Then
            Throw New ArgumentException("Se ha recibido un parámetro nulo.")
        End If

        If mintIDUsuario = 0 Then Return True 'si es master GCP-Cambios ID: 9049

        Try
            intIDTarea = CType(role, Integer)
            'Verifico si el usuario tiene permitida la tarea
            rowTarea = Me.mdtsTareasAutoriz.Tables("Tareas").Select("IDTarea = " & intIDTarea.ToString)
            If rowTarea.GetUpperBound(0) = -1 Then
                Return False
            Else
                'Verifico si la tarea esta permitida
                If CType(rowTarea(0).Item("TareaInhibida"), String).TrimEnd.Equals("N") Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception("El parámetro recibido no es numérico.")
        End Try

    End Function

    ''' <summary>
    ''' Determina si el usuario posee una determinada tarea primitiva para el sistema
    ''' </summary>
    ''' <param name="CodigoTarea">Código de Tarea</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsInRoleSistema(ByVal CodigoTarea As String) As Boolean Implements ICIPOL.IsInRole
        Dim rowTarea() As System.Data.DataRow

        If CodigoTarea Is Nothing Then
            Throw New ArgumentException("Se ha recibido un parámetro nulo.")
        End If


        If mintIDUsuario = 0 Then Return True 'si es master GCP-Cambios ID: 9049

        Try
            'Verifico si el usuario tiene permitida la tarea
            rowTarea = Me.mdtsTareasAutoriz.Tables("Tareas").Select("CodigoTarea = '" & CodigoTarea & "' and IdSistema = " & IDSistemaActual)
            If rowTarea.GetUpperBound(0) = -1 Then
                Return False
            Else
                'Verifico si la tarea esta permitida
                If CType(rowTarea(0).Item("TareaInhibida"), String).TrimEnd.Equals("N") Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception("El parámetro recibido no es numérico.")
        End Try

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recupera los usuarios supervisores de una tarea primitiva
    ''' </summary>
    ''' <param name="IDTarea">Identificador de la tarea de la cual se recuperan los usuarios supervisores</param>
    ''' <returns>
    '''     DataSet con el campo Nombre de los supervisores
    ''' </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' 	[gustavom]	25/10/2006	GCP-Cambios ID: 4409
    ''' 	[gustavom]	29/04/2009	GCP-Cambios ID: 8047
    ''' 	[gustavom]	viernes, 26 de marzo de 2010 GCP-Cambios ID: 8891
    '''     [Leandrof]  01/09/2016 TFS 7693
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperarSupervisores(ByVal IDTarea As Integer) As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsRet         : DataSet de retorno que contiene los usuarios supervisores
        'dttSupervisores: DataTable de supervisión
        'dclNombre      : Columna del DataTable
        'rowSup         : Array que se utiliza para recuperar los usuarios
        '                 supervisores de las tareas
        'rowRecorrer    : Objeto DataRow que se utiliza para recorrer la colección de 
        '                 DataRows
        'rowAgregar     : Objeto DataRow que se utiliza para agregar al DataSet de 
        '                 retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim dtsRet As New System.Data.DataSet
        Dim dttSupervisores As New System.Data.DataTable("Supervisores")
        Dim dclNombre As New System.Data.DataColumn("Nombre", GetType(System.String))
        Dim dclIDUsuario As New System.Data.DataColumn("IDUsuario", GetType(System.Int32))
        Dim dclUsuario As New System.Data.DataColumn("Usuario", GetType(System.String))
        Dim rowSup() As System.Data.DataRow, rowRecorrer As System.Data.DataRow
        Dim rowAgregar As System.Data.DataRow

        dttSupervisores.Columns.Add(dclNombre)
        dttSupervisores.Columns.Add(dclIDUsuario)
        dttSupervisores.Columns.Add(dclUsuario)
        dtsRet.Tables.Add(dttSupervisores)

        rowSup = Me.mdtsTareasAutoriz.Tables("Autorizantes").Select("IdTareaPrimitiva = " & IDTarea.ToString)
        For Each rowRecorrer In rowSup
            'Si el usuario es el activo, no participa como usuario supervisante
            If mintIDUsuario <> CType(rowRecorrer.Item("IDUsuario"), Integer) Then

                rowAgregar = dttSupervisores.NewRow
                rowAgregar.Item("Nombre") = rowRecorrer.Item("Nombres").ToString.Trim & " (" & rowRecorrer.Item("Usuario").ToString.Trim & ")"
                rowAgregar.Item("IDUsuario") = CType(rowRecorrer.Item("IDUsuario"), Integer)
                rowAgregar.Item("Usuario") = rowRecorrer.Item("Usuario").ToString.Trim

                dttSupervisores.Rows.Add(rowAgregar)
            End If
        Next

        Return dtsRet

    End Function

    Public ReadOnly Property TiempoPorInactividad() As Short
        Get
            Return mshtInactividad
        End Get
    End Property


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Determina si una tarea requiere supervisión
    ''' </summary>
    ''' <param name="IDTarea">Identificador de la tarea primitiva</param>
    ''' <returns>True o False</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	07/09/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RequiereSupervision(ByVal IDTarea As Integer) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                    DESCRIPCION DE VARIABLES LOCALES
        'rowTarea   : Objeto DataRow que se utiliza para recuperar los datos de la 
        '             tarea
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowTarea() As System.Data.DataRow

        rowTarea = Me.mdtsTareasAutoriz.Tables("Autorizantes").Select("IDTareaPrimitiva = " & IDTarea.ToString)
        If rowTarea.GetUpperBound(0) = -1 Then
            Return False
        Else
            Return True
        End If

    End Function

    'Public MustOverride Function RequiereSupervision(ByVal NombrePagina As String, ByRef Evento As String, ByRef Script As String, ByRef IDTareaSupervisar As Integer) As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="NombrePagina"></param>
    ''' <param name="IDTareaSupervisar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MartinV]          [jueves, 16 de octubre de 2014]       Modificado  GCP-Cambios 15575
    ''' </history>
    Public Function RequiereSupervision(ByVal NombrePagina As String, ByRef IDTareaSupervisar As Integer) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'rowFila    : Objeto DataRow que se utiliza para recuperar el identificador
        '             de la tarea a la cual hace referencia la página
        'rowTarea   : Objeto DataRow que se utiliza para recuperar los datos de la 
        '             tarea
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowFila() As System.Data.DataRow, sblJs As New System.Text.StringBuilder
        Dim rowTarea() As System.Data.DataRow

        If NombrePagina.Trim = String.Empty Then
            Throw New Exception("El nombre de la página es un dato obligatorio.")
        End If

        rowFila = Me.mdtsMenuSistemas.Tables("SE_MENUES").Select("Url LIKE '%" & NombrePagina & "%'")
        ''Busca la tarea primitiva que autoriza la página para ver si tiene autorizante
        If rowFila.GetUpperBound(0) = -1 Then
            rowTarea = Me.mdtsTareasAutoriz.Tables("Tareas").Select("IDTarea = " & IDTareaSupervisar.ToString)
        Else
            rowTarea = Me.mdtsTareasAutoriz.Tables("Tareas").Select("IDTarea = " & CType(rowFila(0).Item("IDTarea"), String))
        End If

        If rowTarea.GetUpperBound(0) = -1 Then
            Return False
        Else
            Dim intIdAutorizacion As Integer = System.Convert.ToInt32(rowTarea(0).Item("IdAutorizacion"))
            IDTareaSupervisar = System.Convert.ToInt32(rowTarea(0).Item("IDTarea"))
            Return intIdAutorizacion > 0
        End If
    End Function

    ''' <summary> 
    ''' Verifica si la página esta asociada a una tarea primitiva que requiere supervisión 
    ''' </summary>
    Public Function RequiereSupervisionWEB(ByVal NombrePagina As String, ByRef Evento As String, _
                                           ByRef IDTareaSupervisar As Short, ByRef DatosTarea As System.Data.DataRow) As Boolean
        Dim rowFila() As System.Data.DataRow, sblJs As New System.Text.StringBuilder
        Dim intFila As Int32 = 0
        Dim rowTarea() As System.Data.DataRow
        If NombrePagina.Trim = String.Empty Then
            Throw New Exception("El nombre de la página es un dato obligatorio.")
        End If

        rowFila = Me.mdtsMenuSistemas.Tables("SE_MENUES").Select("Url LIKE '%" & NombrePagina & "%'")

        If rowFila Is Nothing OrElse rowFila.Length = 0 Then
            Return False
        End If

        rowTarea = Me.mdtsTareasAutoriz.Tables("Tareas").Select("IDTarea = " & CType(rowFila(intFila).Item("IDTarea"), String))
        If rowTarea.GetUpperBound(0) = -1 Then
            Return False
        Else
            'Si existe verifica si la tarea primitiva requiere supervisión
            If CType(rowTarea(0).Item("Momento"), Byte) = 9 Then
                Return False
            Else
                Select Case CType(rowTarea(0).Item("Momento"), Byte)
                    Case 0
                        Evento = "onload"
                    Case 1
                        Evento = "onsubmit"
                    Case 10
                        Evento = ""
                    Case Else
                        Throw New Exception("Código de momento de supervisión no soportado.")
                End Select
            End If

            DatosTarea = rowTarea(0)
            IDTareaSupervisar = CType(rowFila(intFila).Item("IDTarea"), Short)

            Return True
        End If

    End Function


    Public Property IV() As String
        Get
            Return mstrIV
        End Get
        Set(ByVal value As String)
            mstrIV = value
        End Set
    End Property

    Public Property Key() As String
        Get
            Return mstrKey
        End Get
        Set(ByVal value As String)
            mstrKey = value
        End Set
    End Property

    Public Event CambiarContrasenia(ByVal mensaje As String, ByVal pregunta As Boolean)

    Public objColeccionDeCookies As System.Net.CookieContainer
    Public objColeccionDeCookiesCipol As System.Net.CookieContainer

    'Contiene la clave pública del algoritmo RSA del servidor  - 'GCP-Cambios ID: 7844
    Public gobjRSAServ() As Byte

    Public ReadOnly Property NombreDominio() As String
        Get
            Return Me.mstrDominio
        End Get
    End Property

    'Public MustOverride Sub CerrarSesion()

    Public ReadOnly Property NombreOrganizacion() As String
        Get
            Return Me.mstrOrganizacion
        End Get
    End Property

    'Public MustOverride Function IniciarSesion(ByVal login As String, ByVal clave As String, Optional ByVal ip As String = "") As Boolean

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si el usuario actual tiene permisos para ver la página solicitada
    ''' </summary>
    '''<param name="NombrePagina">Nombre de la página</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub PermiteRequest(ByVal NombrePagina As String)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'rowFila    : Objeto DataRow que se utiliza para recuperar el identificador
        '             de la tarea a la cual hace referencia la página
        'sblJs      : String Builder que se utiliza para retornar el código javascript
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rowFila() As System.Data.DataRow, sblJs As New System.Text.StringBuilder

        If NombrePagina.Trim = String.Empty Then
            Throw New Exception("El nombre de la página es un dato obligatorio.")
        End If

        rowFila = Me.mdtsMenuSistemas.Tables("SE_MENUES").Select("Url LIKE '%" & NombrePagina & "%'")
        If rowFila.GetUpperBound(0) = -1 Then
            Throw New Exception("El usuario " & mstrLogin & " no tiene acceso a la página " & NombrePagina)
        End If

    End Sub

    Public ReadOnly Property AliasUsuario() As String Implements ICIPOL.AliasUsuario
        Get
            Return mstrAliasUsuario
        End Get
    End Property
End Class
