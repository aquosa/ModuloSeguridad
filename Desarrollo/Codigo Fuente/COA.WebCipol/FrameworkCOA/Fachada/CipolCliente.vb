Imports EntidadesEmpresariales
Imports System.Security.Principal
Imports System.Configuration

''' -----------------------------------------------------------------------------
''' Project	 : Fachada
''' Class	 : CipolCliente
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Servicios brindados a la capa de presentación por CIPOL
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	31/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> _
Public Class CCipolCliente
    Inherits EntidadesEmpresariales.PadreCipolCliente
    Implements IPrincipal
    Implements ICIPOL


#Region "Variables que se publican"



#End Region

#Region "Variables Privadas"
    Private FachadaCOA As New Fachada.PadreFachada

    Private mstrPC As String = ""

    Private Const SepDatos As Char = "æ"c ' Alt + 145
    Private Const SepOtrosDatos As Char = "Æ"c ' Alt + 146
    Private Const SepClaveValor As Char = "ô"c ' Alt + 147

    Private Enum PosicionSerializar As Byte
        IDUsuario = 0
        Login
        NombreyApellido
        IDSistemaDefault
        Dominio
        Organizacion
        TiempoInactividad
        SchemaMenuSistemas
        XmlMenuSistemas
        SchemaTareasAutoriz
        XmlTareasAutoriz
        PC
    End Enum


#End Region

#Region "Propiedades"


    'Public Overrides ReadOnly Property NombreDominio() As String
    '    Get
    '        If Me.mstrDominio.Equals("no definido") Then
    '            Dim objCipol As New ReglasNegocio.CIPOL
    '            Me.mstrDominio = objCipol.RecuperarDominio
    '            If Me.mstrDominio Is Nothing Then Return Nothing
    '            If Not Me.mstrDominio.Equals(String.Empty) Then
    '                'Setea además el nombre de la organización
    '                Me.mstrOrganizacion = objCipol.NombreOrganizacion
    '            End If
    '        End If

    '        Return Me.mstrDominio
    '    End Get
    'End Property

    'Public Overrides ReadOnly Property NombreOrganizacion() As String
    '    Get
    '        If Me.mstrOrganizacion.Equals("no definido") Then
    '            Dim objCipol As New ReglasNegocio.CIPOL
    '            objCipol.RecuperarDominio()
    '            Me.mstrOrganizacion = objCipol.NombreOrganizacion
    '        End If

    '        Return Me.mstrOrganizacion
    '    End Get
    'End Property


#End Region
    Public Shadows Event CambiarContrasenia(ByVal mensaje As String, ByVal pregunta As Boolean)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Function NuevoPadreCipolCliente() As CCipolCliente
        Dim obj As New CCipolCliente
        obj.mdtsMenuSistemas = mdtsMenuSistemas
        obj.mobjIdentidad = mobjIdentidad
        obj.mintIDUsuario = mintIDUsuario
        obj.mdtsTareasAutoriz = mdtsTareasAutoriz
        obj.mstrLogin = mstrLogin
        obj.mstrIV = mstrIV
        obj.mstrKey = mstrKey
        obj.mobjOtrosDatos = mobjOtrosDatos
        obj.mstrMensajeError = mstrMensajeError
        obj.mstrNombreyApellido = mstrNombreyApellido
        obj.mshtIDSistemaDefault = mshtIDSistemaDefault
        obj.mstrDominio = mstrDominio
        obj.mstrOrganizacion = mstrOrganizacion
        obj.mshtInactividad = mshtInactividad
        obj.mstrAliasUsuario = mstrAliasUsuario
        obj.mblnSeguridad_SoloDominio = mblnSeguridad_SoloDominio
        Return obj
    End Function

    Public Function NuevoPadreCipol() As EntidadesEmpresariales.PadreCipolCliente
        Return New EntidadesEmpresariales.PadreCipolCliente(mdtsMenuSistemas, mobjIdentidad, mintIDUsuario, mdtsTareasAutoriz, mstrLogin, mstrIV, mstrKey, mobjOtrosDatos, mstrMensajeError, mstrNombreyApellido, mshtIDSistemaDefault, mstrDominio, mstrOrganizacion, mshtInactividad, Me.mstrAliasUsuario, mblnSeguridad_SoloDominio)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Inicia la sesion del usuario y configura los datos del mismo
    ''' </summary>
    '''<param name="Login">Nombre de inicio de sesion</param>
    '''<param name="Clave">Contraseña del usuario</param>
    '''<param name="Terminal">Nombre NetBios de la PC donde se inicia sesion, de lo contrario recupera el nombre de NETBIOS </param>
    ''' <param name="Terminal_ActualizacionLAN">Indica si la terminal se actualiza a través de un servidor de la LAN o remoto</param>
    '''<returns> True si se inició sesion, False si existe mensaje de error </returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' 	[gustavom]	martes, 23 de octubre de 2007	GCP-Cambios ID: 5992
    '''     [gustavom]	07/02/2008	GCP-Cambios ID: 
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function IniciarSesion(ByVal Login As String, ByVal Clave As String, Optional ByVal Terminal As String = "", Optional ByRef Terminal_ActualizacionLAN As Boolean = False) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'strClave   : Clave del Usuario
        'shtID      : Identificador del usuario
        'objEnc     : Objeto que se utiliza para generar el Hash de la clave
        'strClave   : Hash que representa la clave del usuario
        'objCipol   : Objeto Regla de Negocio de CIPOL
        'blnRet     : Valor de retorno de la clase CIPOL
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strClave As String
        Dim shtID As Integer = -1, objCipol As ReglasNegocio.CIPOL
        Dim blnRet As Boolean

        If (Login Is Nothing) OrElse Login.Equals(String.Empty) Then
            Throw New ArgumentException("El usuario es un parámetro obligatorio.")
            'Throw New Exception("El usuario es un parámetro obligatorio.")
        End If
        If (Clave Is Nothing) OrElse Clave.Equals(String.Empty) Then
            Throw New ArgumentException("La clave es un parámetro obligatorio.")
            'Throw New Exception("La clave es un parámetro obligatorio.")
        End If
        If Terminal.Equals(String.Empty) Then
            mstrPC = System.Environment.MachineName
        Else
            mstrPC = Terminal
        End If

        Login = Login.Trim.ToLower
        strClave = Clave
        Try
            objCipol = New ReglasNegocio.CIPOL
            shtID = objCipol.ExisteUsuario(Login)
            'Si el usuario existe
            If shtID >= 0 Then
                blnRet = objCipol.IniciarSesion(shtID, strClave, mstrPC, Terminal_ActualizacionLAN)
                If blnRet Then
                    'Si el inicio de sesión es satisfactorio
                    SetearPropiedades(objCipol)
                Else
                    Me.mstrMensajeError = objCipol.MensajeError
                End If
                objCipol = Nothing
                Return blnRet
            Else
                Me.mstrMensajeError = objCipol.MensajeError
                Return False
            End If

        Catch ex As Exception
            Me.FachadaCOA.PublicarExcepcion(ex)
            Throw
        End Try

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Carga los valores en los atributos de la clase
    ''' </summary>
    '''<param name="Cipol">Instancia de CIPOL</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' 	[gustavom]	14/06/2007	GCP-Cambios ID: 5208
    ''' 	[gustavom]	viernes, 21 de mayo de 2010	    GCP-Cambios ID: 9068
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub SetearPropiedades(ByVal Cipol As ReglasNegocio.CIPOL)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        '    (agregar nombre de variables y su descripción) 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As New AccesoDatos.cladCipol
        Dim objEncriptacion As New COA.CifrarDatos.TresDES(Cipol.IV, Cipol.Key)

        objADCipol.CrearConexion()


        'Atencion !!! Nuevas variables que se agregen se deben 
        'establecer en los métodos GuardarInstancia y RecuperarInstancia
        Me.mintIDUsuario = Cipol.IDUsuario
        Me.mstrLogin = Cipol.Login
        Me.mstrNombreyApellido = Cipol.NombreApellido
        Me.mdtsMenuSistemas = Cipol.DatosDelUsuario
        Me.mdtsTareasAutoriz = Cipol.DatosTareasAutorizantes
        Me.mshtIDSistemaDefault = Cipol.SistemaActual
        Me.mstrAliasUsuario = Cipol.AliasUsuario

        Me.mstrDominio = Cipol.NombreDominio
        Me.mblnSeguridad_SoloDominio = Cipol.Seguridad_SoloDominio
        Me.mstrOrganizacion = Cipol.NombreOrganizacion


        '''''Me.mstrOrganizacion = New String(Me.NombreOrganizacion.ToCharArray()) ' Cipol.NombreOrganizacion
        Me.mshtInactividad = Cipol.TiempoInactividad
        Me.mstrIV = Cipol.IV
        Me.mstrKey = Cipol.Key


        If Cipol.UsuarioDelDominio Then '     
            OtrosDatos("usuariodeldominio", "1")
        Else
            OtrosDatos("usuariodeldominio", "0")
        End If

        If System.Configuration.ConfigurationManager.AppSettings("PublicarListaUsuarios") = "S" Then
            Dim _usuarios As String = ""
            Dim dtsInit As System.Data.DataSet

            dtsInit = objADCipol.RecuperarIDUsuarios()
            For intI As Integer = 0 To dtsInit.Tables("Usuarios").Rows.Count - 1

                If CType(dtsInit.Tables("Usuarios").Rows(intI)("idusuario"), Integer) = 0 Then
                    Continue For
                End If

                _usuarios += dtsInit.Tables("Usuarios").Rows(intI)("idusuario").ToString() + "æ" + dtsInit.Tables("Usuarios").Rows(intI)("nombres").ToString().Trim()
                If intI < dtsInit.Tables("Usuarios").Rows.Count - 1 Then
                    _usuarios += "|"
                End If
            Next

            OtrosDatos("listausuarios", _usuarios)
        End If
        objADCipol.Desconectar()

        'Genero la Identidad del usuario
        Me.mobjIdentidad = New GenericIdentity(Me.mstrLogin)


        'Si el usuario debe cambiar la contraseña
        'martinv gcp 14460
        If Cipol.Contrasenia.Cambiar Then
            OtrosDatos("ForzarCambioClave", "1")
            If Cipol.Contrasenia.SeDebePreguntar Then
                OtrosDatos("ForzarCambioClave.Mensaje", Cipol.Contrasenia.Mensaje)
                OtrosDatos("ForzarCambioClave.SeDebePreguntar", "1")
            End If
        Else
            OtrosDatos("ForzarCambioClave", "0")
        End If

    End Sub

    Public Function ObtenerDTSMenuSistemas() As System.Data.DataSet
        Return Me.mdtsMenuSistemas
    End Function

    Public Function ObtenerDTSTareas() As System.Data.DataSet
        Return Me.mdtsTareasAutoriz
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Cierra la sesion del usuario
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub CerrarSesion()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'cladCipol  : Componente lógico de acceso a datos que se utiliza para 
        '             eliminar la sesión de SE_Tareas
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim cladCipol As New AccesoDatos.cladCipol
        Dim dtsMensajes As System.Data.DataSet
        Dim objSesion As Sesion
        Dim strLogin As String
        Dim strTerminal As String
        Dim objCipol As Fachada.CCipolCliente

        Try
            objSesion = EntidadesEmpresariales.Sesion.getInstance

            cladCipol.CrearConexion()

            If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
                objCipol = CType(objSesion("objCipol"), Fachada.CCipolCliente)
            Else
                objCipol = CType(System.Threading.Thread.CurrentPrincipal, Fachada.CCipolCliente)
            End If

            strLogin = objCipol.Login
            strTerminal = objCipol.mstrPC

            cladCipol.CerrarSesion(strLogin)

            dtsMensajes = cladCipol.RecuperarMensajes()
            cladCipol.Auditar(1, CCipolCliente.MensajeAuditoria(dtsMensajes, strTerminal, 1, strLogin), strLogin)

            cladCipol.Desconectar()
        Catch ex As Exception
            Throw
        End Try

    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si la página esta asociada a una tarea primitiva que requiere supervisión
    ''' </summary>
    '''<param name="NombrePagina">Nombre de la página de la cual se desea verificar si requiere supervisión</param>
    '''<param name="Evento">Evento sobre el cual se llamar a la función JavaScript</param>
    '''<param name="Script">Función JavaScript que se retorna, la cual redirecciona a la página de supervisión</param>
    '''<returns>True o False</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Function RequiereSupervision(ByVal NombrePagina As String, ByRef Evento As String, ByRef Script As String, ByRef IDTareaSupervisar As Integer) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'rowFila    : Objeto DataRow que se utiliza para recuperar el identificador
        '             de la tarea a la cual hace referencia la página
        'sblJs      : String Builder que se utiliza para retornar el código javascript
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
            Dim strDescripcionTarea As String = ""
            Evento = "onload"
            IDTareaSupervisar = System.Convert.ToInt32(rowTarea(0).Item("IDTarea"))
            strDescripcionTarea = CType(rowTarea(0).Item("DescripcionTarea"), String).TrimEnd()
            If rowTarea(0).Table.Columns.Contains("Momento") Then
                'Si existe verifico si la tarea primitiva requiere supervisión
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
            End If


            With sblJs
                .Append("<script language=") : .Append("""")
                .Append("javascript") : .Append("""")
                .Append(" type=") : .Append("""")
                .Append("text/javascript") : .Append("""") : .Append(">")
                .Append(vbCrLf)
                .Append("<!--")
                .Append(vbCrLf)
                .Append("function Supervisar(){")
                .Append(vbCrLf)
                .Append("var ret;")
                .Append(vbCrLf)
                .Append("ret = ""ninguno"";")
                .Append(vbCrLf)
                .Append("ret = window.showModalDialog('")
                .Append("../CIPOL/CipolSupervision.aspx?")
                .Append("Nombre=") : .Append(strDescripcionTarea)
                .Append("', '', ")
                .Append("'dialogHeight:175px;dialogWidth:300px;titlebar:no;help:no;toolbars:no;scrollbars:no;status:no;resizable:no;');")
                .Append(vbCrLf)
                .Append("if(ret == ""ninguno""){")
                .Append(vbCrLf)
                'Si la supervisión se realiza al iniciar la página y se cancela
                'redirecciona a la página de inicio, de lo contrario retorna false
                'para que no se ejecute el evento Submit
                If Evento = "onload" Then
                    .Append("   window.location.href=") : .Append("""") : .Append(System.Configuration.ConfigurationManager.AppSettings("PaginaInicio")) : .Append("""") : .Append(";")
                Else
                    .Append("   return false;")
                End If
                .Append(vbCrLf)
                .Append("}")
                .Append(vbCrLf)
                .Append("else{")
                .Append(vbCrLf)
                .Append("   window.document.frm1.supervisor.value = ret;")
                .Append(vbCrLf)
                .Append("}")
                .Append(vbCrLf)
                .Append("}")
                .Append(vbCrLf)
                .Append("// -->")
                .Append(vbCrLf)
                .Append("</script>")
            End With
            Script = sblJs.ToString


            Return True
        End If


    End Function
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
    Public Overloads Function RequiereSupervision(ByVal NombrePagina As String, ByRef IDTareaSupervisar As Integer) As Boolean
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



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si la clave del usuario es correcta
    ''' </summary>
    '''<param name="Usuario">Nombre del usuario</param>
    '''<param name="Clave">Clave del usuario</param>
    '''<returns>Identificador del usuario. -99 si no se pudo validar</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[gustavom]	31/08/2005	Created
    ''' 	[gustavom]	18/05/2008	GCP-Cambios ID: 8107
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ClaveCorrecta(ByVal Usuario As String, ByVal Clave As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objCIPOL   : Reglas de Negocio de CIPOL
        'intID      : Identificador del usuario
        'blnRet     : Retorno de la llamada a la función
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objCIPOL As ReglasNegocio.CIPOL
        Dim intID As Integer, blnRet As Boolean = False

        Try
            objCIPOL = New ReglasNegocio.CIPOL
            intID = objCIPOL.ExisteUsuario(Usuario)
            If intID >= 0 Then
                blnRet = objCIPOL.ClaveCorrecta(Clave)
                If Not blnRet Then Me.mstrMensajeError = objCIPOL.MensajeError
            End If
        Catch ex As Exception
            Throw
        End Try

        If blnRet Then
            Return intID
        Else
            Return -99
        End If

    End Function

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
        'objADCipol : Componente lógico de acceso a datos que se utiliza para
        '             auditar la supervisión
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADCipol As AccesoDatos.cladCipol = Nothing

        Try
            objADCipol = New AccesoDatos.cladCipol
            objADCipol.CrearConexion()
            objADCipol.AuditarSupervision(IDUsuarioSupervisor, IDTarea)
        Catch ex As Exception
            Throw
        Finally
            If objADCipol IsNot Nothing Then objADCipol.Desconectar()
        End Try

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna el mensaje a auditar 
    ''' </summary>
    ''' <param name="CodMensaje">Codigo del mensaje</param>
    ''' <param name="Usuario">Usuario del cual se realiza la auditoria</param>
    ''' <param name="UsuarioAdm">Usuario que actua como administrador del cambio que se audita</param>
    ''' <param name="NuevoValor">Cambio realizado</param>
    ''' <returns>Mensaje a auditar</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[juano]	25/10/2007	Created
    ''' [LucianoP]          [lunes, 26 de diciembre de 2011]       Adaptado para ser utilizado en Fachada
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function MensajeAuditoria(ByVal DataSetMensajes As System.Data.DataSet, ByVal mstrTerminal As String, _
                                            ByVal CodMensaje As Short, Optional ByVal Usuario As String = "", _
                                            Optional ByVal UsuarioAdm As String = "", Optional ByVal NuevoValor As String = "") As String
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Autor: Gustavo Mazzaglia
        'Fecha de creación: 21/02/2002
        'Modificaciones:
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                  DESCRIPCION DE VARIABLES LOCALES
        'strMensaje : Mensaje que se obtiene cuando se reemplazan las valores
        '             dinámicos (@)
        'dtrMsj     : Objeto DataRow que representa el mensaje a auditar
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strMensaje As String
        Dim dtrMsj() As System.Data.DataRow

        dtrMsj = DataSetMensajes.Tables("Auditoria").Select("CodAuditoria = " & CodMensaje.ToString)
        If dtrMsj.GetUpperBound(0).Equals(-1) Then
            strMensaje = "No existe el mensaje de auditoría para el código: " & CodMensaje.ToString
        Else
            strMensaje = CType(dtrMsj(0).Item("TextoAuditoria"), String)
            Select Case CodMensaje
                Case 0, 1, 3 'Login/LogOut Existoso. Desconexión involuntaria
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                Case 2 'Cierre de sesión por timeout
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                Case 100 'Proceso de Login
                    strMensaje = strMensaje.Replace("@", mstrTerminal)
                Case 110, 230, 250 'Proceso de Login
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                Case 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 240 'Proceso de Login
                    strMensaje = strMensaje.Replace("@", Usuario)
                Case 260, 270 'Supervision
                    strMensaje = Replace(strMensaje, "@", mstrTerminal, , 1, CompareMethod.Text)
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, CompareMethod.Text)
                Case 400, 410, 420, 430, 440, 450, 460, 470, 480, 490, 500, 510 'Políticas de Seguridad
                    strMensaje = Replace(strMensaje, "@", UsuarioAdm, , 1, vbTextCompare)
                    strMensaje = Replace(strMensaje, "@", NuevoValor, , 1, vbTextCompare)
                Case 600, 610, 620, 750, 760, 770 'Administracion de Usuarios
                    strMensaje = Replace(strMensaje, "@", UsuarioAdm, , 1, vbTextCompare)
                    strMensaje = Replace(strMensaje, "@", Usuario, , 1, vbTextCompare)
                Case 700, 710, 720, 730, 740 'ABM de Seguridad
                    strMensaje = strMensaje.Replace("@", UsuarioAdm)
            End Select
        End If

        Return strMensaje

    End Function

End Class