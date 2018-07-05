Imports System.Web
Imports System.Web.Caching

Namespace Datos

    ''' <summary>
    ''' Datos de la conexion al origen de Datos
    ''' </summary>
    Public Class ConexionAlOrigen

        Private mobjMotor As COA.Datos.Origenes.AccesoDB

        'Alias de conexión 
        Private Shared mstrAliasConexion As String = String.Empty
        Private Shared mstrConexion As String = String.Empty
        Private Shared mstrIDConexion As String = "(ninguno)"
        Private Shared mbytIdiomaDB As Byte
        Private Shared mbytTipoBase As Byte

        ''' <summary>
        ''' Obtiene el componente lógico que interactua con el motor de Base de Datos
        ''' </summary>
        ''' <returns>Componente lógico que interactua con el motor de Base de Datos</returns>
        Public ReadOnly Property Motor() As COA.Datos.Origenes.AccesoDB
            Get
                Return mobjMotor
            End Get
        End Property

        ''' <summary>
        ''' Obtiene o establece el alias de la conexión
        ''' </summary>
        ''' <value>Alias de la conexión</value>
        ''' <returns>Alias de la conexión</returns>
        Public Property AliasConexion() As String
            Get
                Return mstrAliasConexion
            End Get
            Set(ByVal Value As String)
                mstrAliasConexion = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Establece la conexión mediante un ID
        ''' </summary>
        ''' <param name="IDConexion">ID de la conexión</param>
        ''' <value>ID de la conexión</value>
        Public WriteOnly Property ConectarConArchivoXml(ByVal IDConexion As String) As String
            Set(ByVal Value As String)
                ConectarConXML(IDConexion, Value)
            End Set
        End Property

        ''' <summary>
        ''' Establece la conexión mediante un ID
        ''' </summary>
        Public WriteOnly Property ConectarConArchivoXml() As String
            Set(ByVal Value As String)
                ConectarConXML("", Value)
            End Set
        End Property

        Public Property TerminalUsuario() As String
            Get
                Return mobjMotor.TerminalUsuario
            End Get
            Set(ByVal value As String)
                mobjMotor.TerminalUsuario = value
            End Set
        End Property

        ''' <summary>
        ''' Permite crear la conexión a la base de datos mediante un ID
        ''' </summary>
        ''' <param name="IDConexion">ID de la conexion</param>
        ''' <param name="Value">Path del archivo XML con los datos de la conexión</param>
        ''' <remarks></remarks>
        Private Sub ConectarConXML(ByVal IDConexion As String, ByVal Value As String)
            
            If HttpRuntime.Cache.Item("ArchivoConexion") Is Nothing Then
                mstrConexion = String.Empty
                Try
                    Me.LeerArchivoXml(Value, IDConexion)
                Catch ex As Exception
                    Throw
                End Try
            Else
                'Si el identificador del la conexión es distinto
                If Not IDConexion.Equals(mstrIDConexion) Then
                    mstrConexion = String.Empty
                    Try
                        Me.LeerArchivoXml(Value, IDConexion)
                    Catch ex As Exception
                        Throw
                    End Try
                Else
                    InstanciarOrigen(CType(mbytTipoBase, AdmConexion.TipoBase))
                    Me.mobjMotor.StrConexionActiva = mstrConexion
                    Me.mobjMotor.IdiomaInicioSesion = CType(mbytIdiomaDB, COA.Datos.Origenes.AccesoDB.IdiomaDB)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Permite realizar la desconexión de la Base de Datos
        ''' </summary>
        Public Sub Desconectar()
            If Not Me.mobjMotor Is Nothing Then
                Me.mobjMotor.Desconectar()
                Me.mobjMotor = Nothing
            End If
        End Sub

        ''' <summary>
        ''' Parsea el Archivo XML con los datos de la conexión
        ''' </summary>
        ''' <param name="PathArchivo">Ruta donde se encuentra el Archivo XML</param>
        ''' <param name="IDConexion">ID de la conexión en el archivo XML</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function LeerArchivoXml(ByVal PathArchivo As String, Optional ByVal IDConexion As String = "") As Boolean

            'Si no se establece un directorio
            If PathArchivo.LastIndexOf("\"c) = -1 Then
                Dim strDirectorioActual As String

                strDirectorioActual = System.Reflection.Assembly.GetCallingAssembly.Location
                strDirectorioActual = strDirectorioActual.Substring(0, strDirectorioActual.LastIndexOf("\"c) + 1)

                PathArchivo = strDirectorioActual & PathArchivo
            End If

            'Verifico si el archivo existe
            If System.IO.File.Exists(PathArchivo) Then
                Dim objArchivoConexion As New AdmConexion(PathArchivo)
                Dim strConexiones() As String

                If IDConexion.Equals(String.Empty) Then
                    'Obtengo la conexión por defecto (primera conexión)
                    strConexiones = objArchivoConexion.ObtenerIDConexiones
                    objArchivoConexion.Buscar(strConexiones(0))
                Else
                    'Recupera la conexión x identificador
                    If Not objArchivoConexion.Buscar(IDConexion) Then
                        Throw New Exception("El identificador de conexión " & IDConexion & " no existe en el archivo " & PathArchivo)
                    End If
                End If

                'Instancia el motor de base de datos
                With objArchivoConexion
                    InstanciarOrigen(.DBMS)

                    mbytIdiomaDB = .Idioma

                    Me.mobjMotor.BaseDatosSetear = .BasedeDatos
                    Me.mobjMotor.ServidorSetear = .Servidor
                    Me.mobjMotor.LoginSetear = .Login
                    Me.mobjMotor.ClaveLogin = .LoginClave
                    If System.Configuration.ConfigurationManager.AppSettings("PoolMin") Is Nothing OrElse _
                       System.Configuration.ConfigurationManager.AppSettings("PoolMax") Is Nothing Then
                        Throw New Exception("Falta especificar el tamaño mínimo (clave PoolMin) y máximo (clave PoolMax) del Pool de Conexiones en el archivo de configuración.")
                    End If
                    Me.mobjMotor.PoolMin = Integer.Parse(System.Configuration.ConfigurationManager.AppSettings("PoolMin"))
                    Me.mobjMotor.PoolMax = Integer.Parse(System.Configuration.ConfigurationManager.AppSettings("PoolMax"))

                    'Si se trata de una base de datos Access
                    If .DBMS = AdmConexion.TipoBase.Access Then CType(Me.mobjMotor, COA.Datos.Origenes.Access).ClaveMDB = .ClaveMDB

                    Me.mobjMotor.IdiomaInicioSesion = CType(mbytIdiomaDB, COA.Datos.Origenes.AccesoDB.IdiomaDB)

                    mstrAliasConexion = .AliasConexion

                    'Guarda el string de conexión y el identificador de la conexión
                    mstrIDConexion = IDConexion
                    mstrConexion = Me.mobjMotor.ArmarStringConexion

                End With
                GrabarEnCache(PathArchivo)
            Else
                Throw New Exception("El Archivo " & PathArchivo & " no existe.")
            End If

        End Function

        ''' <summary>
        ''' Permite recuperar un administrador de un Origen de Datos especifico
        ''' </summary>
        ''' <param name="Motor">Tipo de Base de Datos</param>
        ''' <remarks></remarks>
        Private Sub InstanciarOrigen(ByVal Motor As AdmConexion.TipoBase)

            Select Case Motor
                Case AdmConexion.TipoBase.Access
                    Me.mobjMotor = New COA.Datos.Origenes.Access
                Case AdmConexion.TipoBase.Oracle
                    Me.mobjMotor = New COA.Datos.Origenes.Oracle
                Case AdmConexion.TipoBase.SQLServer
                    Me.mobjMotor = New COA.Datos.Origenes.SQLServer
                Case AdmConexion.TipoBase.SQLServerOLEDB
                    Me.mobjMotor = New COA.Datos.Origenes.Access(Origenes.Access.ConectarA.SQLServer)
                Case AdmConexion.TipoBase.OracleOleDb
                    Me.mobjMotor = New COA.Datos.Origenes.Access(Origenes.Access.ConectarA.Oracle)
                Case Else
                    Throw New Exception("Origen de datos no soportado.")
            End Select

            mbytTipoBase = Motor

        End Sub

        ''' <summary>
        ''' Permite grabar en Cache los datos del archivo de conexión a la Base de Datos
        ''' </summary>
        ''' <param name="PathArchivo">Ruta del archivo XML</param>
        ''' <remarks></remarks>
        Private Sub GrabarEnCache(ByVal PathArchivo As String)
            'Graba el string de conexión en cache para generar una dependencia sobre
            'el archivo Conexion.xml, de tal manera que si el mismo cambia, se genere
            'nuevamente el string de conexión
            Dim objCacheDep As New System.Web.Caching.CacheDependency(PathArchivo)
            HttpRuntime.Cache.Insert("ArchivoConexion", PathArchivo, objCacheDep)

        End Sub

        ''' <summary>
        ''' Recupera el nombre de la base de datos de acuerdo al ID de conexion recibido como parametro
        ''' </summary>
        ''' <param name="IDConexion">ID de la conexion que se desea recuperar el nombre de la base de datos</param>
        ''' <param name="PathArchivo">Path del archivo conexion.xml</param>
        ''' <returns>Nombre de la base de datos</returns>
        '''<history> 
        ''' LucianoP          [viernes, 25 de septiembre de 2015 17:45:18]       Creado
        ''' </history>
        Public Function RecuperarNombreDBxIDConexion(ByVal IDConexion As String, ByVal PathArchivo As String) As String
            Dim objArchivoConexion As New AdmConexion(PathArchivo)
            objArchivoConexion.Buscar(IDConexion)
            Return objArchivoConexion.BasedeDatos
        End Function

        ''' <summary>
        ''' Recupera el nombre de la base de datos de acuerdo al ID de conexion recibido como parametro
        ''' </summary>
        ''' <param name="IDConexion">ID de la conexion que se desea recuperar el nombre de la base de datos</param>
        ''' <param name="PathArchivo">Path del archivo conexion.xml</param>
        ''' <returns>Nombre de la base de datos</returns>
        '''<history> 
        ''' LucianoP          [viernes, 25 de septiembre de 2015 17:45:18]       Creado
        ''' </history>
        Public Function RecuperarServidorxIDConexion(ByVal IDConexion As String, ByVal PathArchivo As String) As String
            Dim objArchivoConexion As New AdmConexion(PathArchivo)
            objArchivoConexion.Buscar(IDConexion)
            Return objArchivoConexion.Servidor
        End Function

        ''' <summary>
        ''' Recupera el nombre de la base de datos de acuerdo al ID de conexion recibido como parametro
        ''' </summary>
        ''' <param name="PathArchivo">Path del archivo conexion.xml</param>
        ''' <returns>Nombre de la base de datos</returns>
        '''<history> 
        ''' LucianoP          [viernes, 25 de septiembre de 2015 17:45:18]       Creado
        ''' </history>
        Public Function RecuperarNombreDBDefault(ByVal PathArchivo As String) As String
            Dim objArchivoConexion As New AdmConexion(PathArchivo)
            Dim strConexiones() As String

            strConexiones = objArchivoConexion.ObtenerIDConexiones
            objArchivoConexion.Buscar(strConexiones(0))

            Return objArchivoConexion.BasedeDatos
        End Function

    End Class

End Namespace