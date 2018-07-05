Namespace Datos

    ''' <summary>
    ''' Contiene las propiedades necesarias para administrar la Conexi�n a la Base de Datos.
    ''' Las propiedades son obtenidas por la lectura de un archivo xml.
    ''' Tambien soporta la escritura del archivo de configuraci�n xml con los datos de la conexi�n.
    ''' </summary>
    Public Class AdmConexion

        Private mstrNombreArchivo As String = ""
        Private mdtsArchivo As System.Data.DataSet
        Private mstrIDConexionActiva As String = ""
        Private mbytTipoBase As TipoBase = TipoBase.Ninguno
        Private mstrClaveMDB As String = ""
        Private mstrBaseDatos As String = ""
        Private mstrLogin As String = ""
        Private mstrContrase�a As String = ""
        Private mstrServidor As String = ""
        Private mstrAlias As String = ""
        Private mstrIdioma As Byte

        ''' <summary>
        ''' Representa el tipo de Base de Datos
        ''' </summary>
        Public Enum TipoBase As Byte
            Access = 0
            Oracle = 1
            SQLServer = 2
            SQLServerOLEDB = 3
            OracleOleDb = 4
            Ninguno = 10
        End Enum

        ReadOnly Property Cantidad() As Byte
            Get
                Return CType(mdtsArchivo.Tables("Conexion").Rows.Count, Byte)
            End Get
        End Property

        ''' <summary>
        ''' Obtiene los ID de las conexiones
        ''' </summary>
        ''' <value>ID de las conexiones</value>
        ''' <returns>ID de las conexiones</returns>
        ReadOnly Property ObtenerIDConexiones() As String()
            Get
                Dim intI As Integer
                Dim strConexiones() As String

                If Me.mdtsArchivo.Tables("Conexion").Rows.Count > 0 Then
                    ReDim strConexiones(Me.mdtsArchivo.Tables("Conexion").Rows.Count - 1)
                    For intI = 0 To Me.mdtsArchivo.Tables("Conexion").Rows.Count - 1
                        strConexiones(intI) = CType(Me.mdtsArchivo.Tables("Conexion").Rows(intI).Item("IDConexion"), String)
                    Next
                Else
                    ReDim strConexiones(0)
                End If
                Return strConexiones
            End Get
        End Property

        ''' <summary>
        ''' Obtiene o establece el tipo de Base de Datos
        ''' </summary>
        ''' <value>Tipo de Base de Datos</value>
        ''' <returns>Tipo de Base de Datos</returns>
        Property DBMS() As TipoBase
            Get
                Return Me.mbytTipoBase
            End Get
            Set(ByVal Value As TipoBase)
                If Value = TipoBase.Ninguno Then
                    Throw New Exception("Valor no permitido en una conexi�n.")
                Else
                    Me.mbytTipoBase = Value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece la clave del archivo MDB
        ''' </summary>
        ''' <value>Clave del archivo MDB</value>
        ''' <returns>Clave del archivo MDB</returns>
        Property ClaveMDB() As String
            Get
                Return Me.mstrClaveMDB
            End Get
            Set(ByVal Value As String)
                Me.mstrClaveMDB = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece la Base de Datos
        ''' </summary>
        ''' <value>Base de Datos</value>
        ''' <returns>Base de Datos</returns>
        Property BasedeDatos() As String
            Get
                Return Me.mstrBaseDatos
            End Get
            Set(ByVal Value As String)
                Me.mstrBaseDatos = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el Login de la Base de Datos
        ''' </summary>
        ''' <value>Login de la Base de Datos</value>
        ''' <returns>Login de la Base de Datos</returns>
        Property Login() As String
            Get
                Return Me.mstrLogin
            End Get
            Set(ByVal Value As String)
                Me.mstrLogin = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece la clave del login de la Base de Datos
        ''' </summary>
        ''' <value>Clave del login de la Base de Datos</value>
        ''' <returns>Clave del login de la Base de Datos</returns>
        Property LoginClave() As String
            Get
                Return Me.mstrContrase�a
            End Get
            Set(ByVal Value As String)
                Me.mstrContrase�a = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el servidor donde se encuentra la Base de Datos
        ''' </summary>
        ''' <value>Servidor donde se encuentra la Base de Datos</value>
        ''' <returns>Servidor donde se encuentra la Base de Datos</returns>
        Property Servidor() As String
            Get
                Return Me.mstrServidor
            End Get
            Set(ByVal Value As String)
                Me.mstrServidor = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Ontiene o establece el Idioma de la Base de Datos
        ''' </summary>
        ''' <value>Idioma de la Base de Datos</value>
        ''' <returns>Idioma de la Base de Datos</returns>
        Property Idioma() As Byte
            Get
                Return Me.mstrIdioma
            End Get
            Set(ByVal Value As Byte)
                Me.mstrIdioma = Value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el alias de la Conexi�n
        ''' </summary>
        ''' <value>Alias de la Conexi�n</value>
        ''' <returns>Alias de la Conexi�n</returns>
        Property AliasConexion() As String
            Get
                Return Me.mstrAlias
            End Get
            Set(ByVal Value As String)
                Me.mstrAlias = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Inicia el proceso de creaci�n de una nueva conexi�n
        ''' </summary>
        ''' <param name="IDConexion">Id de la conexi�n a agregar</param>
        ''' <remarks>True o False</remarks>
        ''' <history>
        ''' [GustavoM]          [19/05/2004]       Creado
        ''' </history>
        Public Sub Agregar(ByVal IDConexion As String)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'rowConexion: Objeto DataRow que se utiliza para recuperar los datos
            '             de la conexi�n actual
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'Verifico si la conexi�n existe
            Dim rowConexion As System.Data.DataRow

            If IDConexion.Equals("") Then
                Throw New Exception("El Identificador de conexi�n no puede ser una cadena vac�a")
            Else
                rowConexion = mdtsArchivo.Tables("Conexion").Rows.Find("IDConexion")
                If Not (rowConexion Is Nothing) Then
                    Throw New Exception("El Identificador de Conexi�n: " & IDConexion & " ya existe.")
                End If
            End If
            'Limpio los valores de las variables privadas
            mstrIDConexionActiva = "&N&" & IDConexion
            mbytTipoBase = TipoBase.Ninguno
            mstrClaveMDB = ""
            mstrBaseDatos = ""
            mstrLogin = ""
            mstrContrase�a = ""
            mstrServidor = ""
            mstrAlias = ""
            Me.mstrIdioma = 0

        End Sub

        ''' <summary>
        ''' Elimina la conexi�n actual
        ''' </summary>
        ''' <param name="IDConexion">ID de la conexi�n</param>
        ''' <returns>True o False</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [19/05/2004]       Creado
        ''' </history>
        Public Function Buscar(ByVal IDConexion As String) As Boolean
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            ' rowConexion: Objeto DataRow que se utiliza para recuperar los datos
            '              de la conexi�n actual
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim rowConexion As System.Data.DataRow

            'Busca una determinada conexi�n y retorna true si ha sido encontrado

            If IDConexion.Equals("") Then
                Throw New Exception("El Identificador de conexi�n no puede ser una cadena vac�a")
            Else
                rowConexion = Me.mdtsArchivo.Tables("Conexion").Rows.Find(IDConexion)
                If rowConexion Is Nothing Then
                    Return False
                Else
                    With rowConexion
                        mstrIDConexionActiva = CType(.Item("IDConexion"), String)
                        mbytTipoBase = CType(.Item("TipoBase"), TipoBase)
                        mstrClaveMDB = Criptografia(CType(.Item("ClaveMDB"), String), False)
                        mstrBaseDatos = Criptografia(CType(.Item("BaseDeDatos"), String), False)
                        mstrLogin = Criptografia(CType(.Item("Usuario"), String), False)
                        mstrContrase�a = Criptografia(CType(.Item("Clave"), String), False)
                        mstrServidor = Criptografia(CType(.Item("Servidor"), String), False)
                        mstrAlias = Criptografia(CType(.Item("Alias"), String), False)
                        mstrIdioma = CType(Criptografia(CType(.Item("Idioma"), String), False), Byte)
                        Return True
                    End With
                End If
            End If
        End Function

        ''' <summary>
        ''' Elimina la conexi�n actual
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [19/05/2004]       Creado
        ''' </history>
        Public Sub Eliminar()

            'Si no existe una conexi�n activa o existe activo un proceso de nueva conexi�n 
            If Me.mstrIDConexionActiva.Equals("") OrElse Me.mstrIDConexionActiva.Substring(0, 3).Equals("&N&") Then
                Throw New Exception("No existe una Conexi�n seleccionada o existe un proceso de Nueva Conexi�n activo.")
            End If
            'Elimino la conexi�n actual
            Me.mdtsArchivo.Tables("Conexion").Rows.Remove(Me.mdtsArchivo.Tables("Conexion").Rows.Find(Me.mstrIDConexionActiva))

        End Sub

        ''' <summary>
        ''' Guarda una nueva conexi�n o actualiza una existente
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [19/05/2004]       Creado
        ''' </history>
        Public Sub Guardar()
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                       DESCRIPCION DE VARIABLES LOCALES
            'rowConexion    : Objeto DataRow que representa la conexi�n a crear o 
            '                 actualizar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim rowConexion As System.Data.DataRow

            If Me.mstrIDConexionActiva.Equals("") Then
                Throw New Exception("No existe una Conexi�n seleccionada o un Proceso de Creaci�n activo.")
            Else
                'Si se trata de una nueva conexi�n
                If Me.mstrIDConexionActiva.Substring(0, 3) = "&N&" Then
                    rowConexion = Me.mdtsArchivo.Tables("Conexion").NewRow
                    rowConexion.Item("IDConexion") = Me.mstrIDConexionActiva.Substring(3)
                Else
                    rowConexion = Me.mdtsArchivo.Tables("Conexion").Rows.Find(Me.mstrIDConexionActiva)
                    rowConexion.BeginEdit()
                End If
                With rowConexion
                    .Item("TipoBase") = Me.mbytTipoBase
                    .Item("ClaveMdb") = Criptografia(Me.mstrClaveMDB, True)
                    .Item("BaseDeDatos") = Criptografia(Me.mstrBaseDatos, True)
                    .Item("Usuario") = Criptografia(Me.mstrLogin, True)
                    .Item("Clave") = Criptografia(Me.mstrContrase�a, True)
                    .Item("Servidor") = Criptografia(Me.mstrServidor, True)
                    .Item("Alias") = Criptografia(Me.mstrAlias, True)
                    .Item("Idioma") = Criptografia(CType(Me.mstrIdioma, String), True)
                End With
                If Me.mstrIDConexionActiva.Substring(0, 3) = "&N&" Then
                    Me.mdtsArchivo.Tables("Conexion").Rows.Add(rowConexion)
                Else
                    rowConexion.EndEdit()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Graba las conexiones en el archivo
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [19/05/2004]       Creado
        ''' </history>
        Public Sub GrabarEnArchivo()
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                      DESCRIPCION DE VARIABLES LOCALES
            ' objXmlWrite : Escritor de archivo XML
            ' rowConexion : DataRow que contiene datos de la conexion
            ' colFila     : DataColumn de la tabla con datos de la conexion
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objXmlWrite As System.Xml.XmlTextWriter
            Dim rowConexion As System.Data.DataRow
            Dim colFila As System.Data.DataColumn

            objXmlWrite = New Xml.XmlTextWriter(Me.mstrNombreArchivo, Nothing)
            objXmlWrite.WriteStartDocument(True)
            objXmlWrite.WriteStartElement("Conexiones")
            For Each rowConexion In mdtsArchivo.Tables("Conexion").Rows
                objXmlWrite.WriteStartElement("Conexion")
                For Each colFila In mdtsArchivo.Tables("Conexion").Columns
                    If colFila.ColumnName = "IDConexion" Then
                        objXmlWrite.WriteAttributeString("IDConexion", CType(rowConexion.Item(colFila), String))
                    Else
                        objXmlWrite.WriteElementString(colFila.ColumnName, CType(rowConexion.Item(colFila), String))
                    End If
                Next
                objXmlWrite.WriteEndElement()
            Next
            objXmlWrite.WriteEndElement()
            objXmlWrite.WriteEndDocument()

            objXmlWrite.Flush()
            objXmlWrite.Close()

            'Verifico si el archivo existe, de lo contrario lo creo
            'Me.mdtsArchivo.WriteXml(Me.mstrNombreArchivo)

        End Sub

        ''' <summary>
        ''' Encripta o desencripta un valor a trav�s de DPAPI
        ''' </summary>
        ''' <param name="Valor">Cadena que se desea encriptar, desencriptar</param>
        ''' <param name="Encriptar">True encripta, False desencripta</param>
        ''' <returns>Cadena encriptada o desencriptada</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [20/05/2004]       Creado
        ''' [GustavoM]          [17/06/2010]       Determina si se debe encriptar por DPAPI (sin conocer frase clave) o 3DES (conociendo frase clave)
        ''' </history>
        Private Function Criptografia(ByVal Valor As String, ByVal Encriptar As Boolean) As String
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            'strRet : Valor de retorno
            'objEnc : Objeto DPAPI de encriptacion
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strRet As String, objEnc As New CifrarDatos.DPAPI(CifrarDatos.DPAPI.EStore.USE_MACHINE_STORE)

            Dim strOp As String = System.Configuration.ConfigurationManager.AppSettings("Crypto")
            If strOp <> Nothing AndAlso strOp.Length > 0 Then
                strOp = strOp.Trim().ToLower()
                If strOp = "dpapi" Then
                    strRet = objEnc.Generar(CType(IIf(Encriptar, CifrarDatos.DPAPI.Tipo.Encriptacion, CifrarDatos.DPAPI.Tipo.Desencriptacion), CifrarDatos.DPAPI.Tipo), Valor, ".AdmConexion.")
                ElseIf strOp = "3des" Then
                    Dim objEncsim As New COA.CifrarDatos.TresDES("WwW0q9ckviI=", "bXMIMMQkW/EFXnlgJa4xjEAc8Rp7pfTd")
                    If Encriptar Then
                        strRet = objEncsim.Criptografia(CifrarDatos.Accion.Encriptacion, Valor)
                    Else
                        strRet = objEncsim.Criptografia(CifrarDatos.Accion.Desencriptacion, Valor)
                    End If
                Else
                    Throw New Exception("Solo se soportan como algoritmo de encriptacion: DPAPI y 3DES")
                End If
            Else
                strRet = objEnc.Generar(CType(IIf(Encriptar, CifrarDatos.DPAPI.Tipo.Encriptacion, CifrarDatos.DPAPI.Tipo.Desencriptacion), CifrarDatos.DPAPI.Tipo), Valor, ".AdmConexion.")
            End If

            Return strRet

        End Function

        ''' <summary>
        ''' Crea el DataTable que contiene la estructura del archivo Xml
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [18/05/2004]       Creado
        ''' </history>
        Private Sub New()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'colColumna : Objeto DataColumn
            'dtConexion : Objeto DataTable
            'bytI       : Contador del For
            'objPK      : Primary Key de la tabla
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim colColumna As System.Data.DataColumn, dtConexion As System.Data.DataTable
            Dim bytI As Byte, objPk(0) As DataColumn

            mdtsArchivo = New System.Data.DataSet("CIPOLConexionDB")
            dtConexion = mdtsArchivo.Tables.Add("Conexion")

            For bytI = 0 To 8
                colColumna = New System.Data.DataColumn
                'IDConexion
                With colColumna
                    Select Case bytI
                        Case 0  'IDConexion
                            .ColumnName = "IDConexion"
                            .DataType = System.Type.GetType("System.String")
                            objPk(0) = colColumna
                        Case 1  'Tipo de base
                            .ColumnName = "TipoBase"
                            .DataType = System.Type.GetType("System.Byte")
                        Case 2  'Clave MDB
                            .ColumnName = "ClaveMdb"
                            .DataType = System.Type.GetType("System.String")
                        Case 3  'Base de datos
                            .ColumnName = "BaseDeDatos"
                            .DataType = System.Type.GetType("System.String")
                        Case 4  'Login
                            .ColumnName = "Usuario"
                            .DataType = System.Type.GetType("System.String")
                        Case 5  'Contrase�a
                            .ColumnName = "Clave"
                            .DataType = System.Type.GetType("System.String")
                        Case 6  'Servidor
                            .ColumnName = "Servidor"
                            .DataType = System.Type.GetType("System.String")
                        Case 7  'Alias
                            .ColumnName = "Alias"
                            .DataType = System.Type.GetType("System.String")
                        Case 8  'Idioma
                            .ColumnName = "Idioma"
                            .DataType = System.Type.GetType("System.String")
                    End Select
                    .AllowDBNull = False
                End With
                'Agrego la columna al DataTable
                dtConexion.Columns.Add(colColumna)
                colColumna = Nothing
            Next bytI
            dtConexion.PrimaryKey = objPk   'Establezco IDConexion como PK

        End Sub

        ''' <summary>
        ''' Verifica si el Archivo existe 
        ''' </summary>
        ''' <param name="Archivo">Se�al que indica si se debe generar un archivo 
        ''' con m�ltiples conexiones o solo una conexi�n Path f�sico del archivo</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [18/05/2004]       Creado
        ''' </history>
        Public Sub New(ByVal Archivo As String)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'strCarpeta : Nombre de las carpetas donde debe almacenarse el archivo
            'blnPriCon  : Se�al que indica que se recuper� la primera conexi�n
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Me.New()

            Dim strCarpeta As String, objXml As System.Xml.XmlTextReader
            Dim rowConexion As System.Data.DataRow = Nothing
            Dim strTexto As String = String.Empty

            'Obtengo las conexiones desde el archivo
            If System.IO.File.Exists(Archivo) Then
                objXml = New System.Xml.XmlTextReader(Archivo)
                Do While objXml.Read
                    Select Case objXml.NodeType
                        Case Xml.XmlNodeType.Element
                            strTexto = objXml.Name
                            If strTexto = "Conexion" Then
                                rowConexion = mdtsArchivo.Tables("Conexion").NewRow
                                rowConexion.Item("IDConexion") = objXml.GetAttribute(0)
                            End If
                        Case Xml.XmlNodeType.Text
                            rowConexion.Item(strTexto) = objXml.Value
                        Case Xml.XmlNodeType.EndElement
                            If objXml.Name = "Conexion" Then
                                mdtsArchivo.Tables("Conexion").Rows.Add(rowConexion)
                            End If
                    End Select
                Loop
                objXml.Close()
            Else
                'Si existe un directorio
                If Archivo.LastIndexOf("\"c) >= 0 Then
                    strCarpeta = Archivo.Substring(0, Archivo.LastIndexOf("\"c))
                    If Not System.IO.Directory.Exists(strCarpeta) Then
                        Throw New Exception("El Directorio " & strCarpeta & " no existe. Verifique.")
                    End If
                Else
                    Throw New Exception("El Archivo " & Archivo & " no existe en el directorio " & System.Reflection.Assembly.GetExecutingAssembly.Location & ". Verifique.")
                End If
                'mstrNombreArchivo = Archivo
            End If
            ' Modificacion para que permita modificar
            mstrNombreArchivo = Archivo
        End Sub

    End Class

End Namespace