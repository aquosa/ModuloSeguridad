Imports System.Reflection
Imports System.Data.EntityClient

Namespace Datos.Origenes
    Public MustInherit Class AccesoDB
#Region "Variables Privadas de Conexi�n al Origen de Datos"
        'Nombre del servidor/Path Access / Instancia
        Protected mstrServidor As String = ""

        'Motor al que se encuentra conectado
        Protected mstrConectadoA As String = ""

        'Nombre de la base de datos
        Protected mstrBase As String = ""

        'Nombre de usuario de inicio de sesi�n
        Protected mstrLogin As String = ""

        'Idioma del usuario de sesi�n a la base. Se utiliza para formatear los datos en las sentencias SQL
        Protected mbytIdiomaDB As IdiomaDB = IdiomaDB.Ingles

        'Clave de inicio de sesi�n
        Protected mstrClaveLogin As String = ""

        'String de Conexion al origen de datos
        Protected mstrConexion As String = String.Empty

        'M�nimo Pool de conexiones
        Protected mintPoolMin As Integer = 1

        'M�ximo Pool de conexiones
        Protected mintPoolMax As Integer = 100

        Protected mstrTerminal As String = System.Environment.MachineName

        Protected mintTimeOutSQL As Integer = -1

#End Region

#Region "Variables Privadas de Auditor�a"
        'Tablas que no se deben auditar cuando se habilita Auditor�a
        Protected mstrTablasNOAuditar As String()

        'Nombre del usuario que inici� el sistema. Utilizado por Auditor�a
        Protected mstrUsuarioActivo As String = ""

        'Nombre del usuario que realiza la supervisi�n de un proceso y se desea auditarlo
        Protected mstrUsuarioSupervisor As String = ""

        'Path donde se guarda la auditor�a de errores
        Protected mstrDirectorioErrorSQL As String = ""

        'Nombre del sistema 
        Protected mstrSistema As String = ""

#End Region

#Region "Variables Privadas de Estado"
        'Objeto que mantiene una transacci�n activa
        Protected mtrTransaccion As System.Data.IDbTransaction

        'Se�al que indica si existe una transacci�n activa
        Protected mblnEnTransaccion As Boolean
#End Region


        Public Enum IdiomaDB As Byte
            Espa�ol = 0
            Ingles

        End Enum

#Region "Propiedades que comparten las clases del Assembly"

        ''' <summary>
        ''' Establece la conexion Activa
        ''' </summary>
        ''' <value>Conexion activa</value>
        Friend WriteOnly Property StrConexionActiva() As String
            Set(ByVal Value As String)
                Me.mstrConexion = Value
            End Set
        End Property

        ''' <summary>
        ''' Establece el servidor
        ''' </summary>
        ''' <value>Servidor</value>
        Friend WriteOnly Property ServidorSetear() As String
            Set(ByVal Value As String)
                Me.mstrServidor = Value
            End Set
        End Property

        Friend Property TimeOutSQL() As Integer
            Get
                Return mintTimeOutSQL
            End Get
            Set(ByVal value As Integer)
                mintTimeOutSQL = value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el idioma de inicio de sesion
        ''' </summary>
        ''' <value>Idioma de inicio de sesion</value>
        ''' <returns>Idioma de inicio de sesion</returns>
        Friend Property IdiomaInicioSesion() As IdiomaDB
            Get
                Return Me.mbytIdiomaDB
            End Get
            Set(ByVal Value As IdiomaDB)
                Me.mbytIdiomaDB = Value
            End Set
        End Property

        ''' <summary>
        ''' Establece la Base de Datos
        ''' </summary>
        ''' <value>Base de Datos</value>
        Friend WriteOnly Property BaseDatosSetear() As String
            Set(ByVal Value As String)
                Me.mstrBase = Value
            End Set
        End Property

        ''' <summary>
        ''' Establece el LogIn
        ''' </summary>
        ''' <value>LogIn</value>
        Friend WriteOnly Property LoginSetear() As String
            Set(ByVal Value As String)
                Me.mstrLogin = Value
            End Set
        End Property

        ''' <summary>
        ''' Establece la clave de login
        ''' </summary>
        ''' <value>Clave de login</value>
        Friend WriteOnly Property ClaveLogin() As String
            Set(ByVal Value As String)
                Me.mstrClaveLogin = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Establece el numero minimo de conexiones del Pool de conexiones
        ''' </summary>
        ''' <value>Numero minimo de conexiones del Pool de conexiones</value>
        Friend WriteOnly Property PoolMin() As Integer
            Set(ByVal value As Integer)
                Me.mintPoolMin = value
            End Set
        End Property

        ''' <summary>
        ''' Establece el numero maximo de conexiones del Pool de conexiones
        ''' </summary>
        ''' <value>Numero maximo de conexiones del Pool de conexiones</value>
        Friend WriteOnly Property PoolMax() As Integer
            Set(ByVal value As Integer)
                Me.mintPoolMax = value
            End Set
        End Property

#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Obtiene el Servidor
        ''' </summary>
        ''' <value>Servidor</value>
        ''' <returns>Servidor</returns>
        Public ReadOnly Property Servidor() As String
            Get
                Return Me.mstrServidor
            End Get
        End Property

        ''' <summary>
        ''' Obtiene el valor que indica si la conexion se encuentra en Transaccion
        ''' </summary>
        ''' <returns>Valor que indica si la conexion se encuentra en Transaccion</returns>
        Public ReadOnly Property EnTransaccion() As Boolean
            Get
                Return Me.mblnEnTransaccion
            End Get
        End Property

        ''' <summary>
        ''' Establece el Path de auditoria de error
        ''' </summary>
        ''' <value>Path de auditoria de error</value>
        Public WriteOnly Property PathAuditoriaError() As String
            Set(ByVal Value As String)
                'Verifico si el directorio ingresado existe
                If System.IO.Directory.Exists(Value) Then
                    If Not Value.Substring(Value.Length, 1).Equals("\") Then Value &= "\"
                    mstrDirectorioErrorSQL = Value & "ErrorSQL.xml"
                Else
                    Throw New Exception("El directorio " & """" & Value.Trim & """" & " no existe.")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Obtiene la Base de Datos
        ''' </summary>
        ''' <returns>Base de Datos</returns>
        Public ReadOnly Property BaseDatos() As String
            Get
                Return Me.mstrBase
            End Get
        End Property

        ''' <summary>
        ''' Obtiene el login de la Base de Datos
        ''' </summary>
        ''' <returns>Login de la Base de Datos</returns>
        Public ReadOnly Property Login() As String
            Get
                Return Me.mstrLogin
            End Get
        End Property

        ''' <summary>
        ''' Obtiene la Base de Datos que se realizo la conexion
        ''' </summary>
        ''' <returns>Base de Datos que se realizo la conexion</returns>
        Public ReadOnly Property ConectadoA() As String
            Get
                Return mstrConectadoA
            End Get
        End Property

        ''' <summary>
        ''' Establece el usuario Supervisor de SQL
        ''' </summary>
        ''' <value>Usuario Supervisor de SQL</value>
        Public WriteOnly Property AuditarSqlConSupervision() As String
            Set(ByVal Value As String)
                Me.mstrUsuarioSupervisor = Value.Trim
            End Set
        End Property

        Public Property TerminalUsuario() As String
            Get
                Return mstrTerminal
            End Get
            Set(ByVal value As String)
                mstrTerminal = value
            End Set
        End Property

        Public Property Sistema() As String
            Get
                Return mstrSistema
            End Get
            Set(ByVal value As String)
                mstrSistema = value
            End Set
        End Property

#End Region

#Region "Metodos de Auditor�a"

        ''' <summary>
        ''' Audita un error de sentencia SQL 
        ''' </summary>
        ''' <param name="ArchivoOrigen">Assembly desde el cual se produce el error</param>
        ''' <param name="Metodo">M�todo en el cual se produce el error</param>
        ''' <param name="Mensaje">Mensaje de error</param>
        ''' <param name="SentSQL">Sentencia SQL a auditar</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [14/05/2004]       Creado
        ''' </history>
        Protected Sub GrabarError(ByVal ArchivoOrigen As String, ByVal Metodo As String, ByVal Mensaje As String, ByVal SentSQL As String)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            'udtAuditar : Estructura que se utiliza para grabar en el archivo Xml
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim udtAuditar As GestionarError.gtypError

            With udtAuditar
                .FechaHora = Now
                .Sistema = Me.mstrSistema
                .Clase = ArchivoOrigen & "-" & Metodo
                .Mensaje = Mensaje
                .SQL = SentSQL
            End With

            GestionarError.GrabarXml(Me.mstrDirectorioErrorSQL, udtAuditar)

        End Sub

        ''' <summary>
        ''' Establece si se debe auditar la sentencia sql que se ejecuta
        ''' </summary>
        ''' <param name="TablasNoAudit">Tablas que se no auditan</param>
        ''' <param name="UsuarioLogin">Usuario que inicia la sesi�n</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [12/05/2004]       Creado
        ''' </history>
        Public Sub HabilitarAuditoria(ByVal TablasNoAudit As String, ByVal UsuarioLogin As String)
            Me.mstrTablasNOAuditar = TablasNoAudit.Trim.ToUpper.Split(";"c)
            Me.mstrUsuarioActivo = UsuarioLogin
        End Sub

        ''' <summary>
        ''' Permite auditar una sentencia SQL en la tabla de Eventos
        ''' </summary>
        ''' <param name="cmSQL">Comando SQL</param>
        ''' <param name="SentSql">Sentencia SQL a auditar</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [08/07/2004]       Creado
        ''' </history>
        Private Sub Auditar(ByRef cmSQL As System.Data.IDbCommand, ByVal SentSql As String)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'intI           : Contador del For
            'strSql         : Va a contener las sentencias Sql a ejecutar
            'strTabla       : Tabla afectada por la sentencia SQL
            'strOperAudit   : Acci�n realizada sobre la tabla
            'strSqlAudit    : Sentencia Sql de auditor�a
            'strComandoSQL  : Cadena que se utiliza para verificar si se trata
            '                 de un comando SQL
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim intI As Integer, strSql As String
            Dim strTabla As String = "", strOperAudit As String = String.Empty
            Dim strSqlAudit As String, strComandoSQL As String

            If Me.mstrUsuarioActivo.Equals("") Then Exit Sub
            SentSql = SentSql.ToUpper.Trim()
            cmSQL.CommandType = CommandType.Text
            'Debido a que en una cadena encriptada se encontr� un VbCrLf
            'no puedo realizar un Split
            intI = 0
            Do While intI >= 0
                intI = SentSql.IndexOf(vbCrLf, intI)
                If intI >= 0 Then    'Si existen varias sentencias separadas por VBCrLf
                    strComandoSQL = SentSql.Substring(intI + 2, 7).ToUpper.Trim()
                    If strComandoSQL.IndexOf("INSERT ") >= 0 OrElse _
                       strComandoSQL.IndexOf("UPDATE ") >= 0 OrElse _
                       strComandoSQL.IndexOf("DELETE ") >= 0 OrElse _
                       strComandoSQL.IndexOf("CREATE ") >= 0 OrElse _
                       strComandoSQL.IndexOf("TRUNCAT") >= 0 OrElse _
                       strComandoSQL.IndexOf("DROP TA") >= 0 Then

                        strSql = SentSql.Substring(0, intI)
                        SentSql = SentSql.Substring(intI + 2)
                        intI = 0 'Comienzo la b�squeda desde el principio
                    Else
                        strSql = ""
                        intI = intI + 2 'Sumo dos para que el IndexOf no tome nuevamente el vbCrLf encontrado
                    End If
                Else
                    strSql = SentSql 'Si se trata de una sentencia o la �ltima de varias
                End If
                strSql = strSql.Trim()
                If Not strSql.Equals("") Then
                    If strSql.Substring(0, 7).ToUpper.Equals("INSERT ") Then
                        ' Sentencia INSERT
                        strOperAudit = "A"
                        strTabla = strSql.Substring(strSql.IndexOf("INTO") + 4)
                        ' Si se trata de un INSERT INTO..SELECT
                        If strTabla.IndexOf("SELECT") >= 0 Then
                            strTabla = strTabla.Substring(0, strTabla.IndexOf("SELECT"))
                            ' Si existen campos
                            If strTabla.IndexOf("(") >= 0 Then
                                strTabla = strTabla.Substring(0, strTabla.IndexOf("("))
                            End If
                        Else
                            strTabla = strTabla.Substring(0, strTabla.IndexOf("("))
                        End If
                    ElseIf strSql.Substring(0, 7).ToUpper.Equals("UPDATE ") Then
                        ' Sentencia UPDATE
                        strOperAudit = "M"
                        strTabla = strSql.Substring(7)
                        strTabla = strTabla.Substring(0, strTabla.IndexOf("SET"))
                    ElseIf strSql.Substring(0, 7).ToUpper.Equals("DELETE ") Then
                        ' Sentencia DELETE
                        strOperAudit = "B"
                        strTabla = strSql.ToUpper
                        If strTabla.IndexOf("DELETE FROM") >= 0 Then
                            strTabla = strTabla.Substring(strTabla.IndexOf("FROM") + 4)
                        Else
                            'Solo DELETE
                            strTabla = strTabla.Substring(6)
                        End If
                        'Si existe un DELETE FROM(Sql) de Oracle
                        strTabla = strTabla.Trim
                        If strTabla.Substring(0, 1).Equals("(") Then
                            strTabla = strTabla.Substring(strTabla.IndexOf("FROM") + 4)
                        End If
                        'Si existe un DELETE Tabla FROM de SQL Server
                        If strTabla.IndexOf("FROM") >= 0 Then
                            strTabla = strTabla.Substring(0, strTabla.IndexOf("FROM"))
                        End If
                        ' Si el DELETE posee un WHERE
                        If strTabla.IndexOf("WHERE") >= 0 Then
                            strTabla = strTabla.Substring(0, strTabla.IndexOf("WHERE"))
                        End If

                    End If
                    If Not strTabla.Equals("") Then
                        strTabla = strTabla.Replace("[", "").Replace("]", "").Trim
                        'Si la tabla se debe auditar
                        Dim a As Decimal
                        a = Decimal.Round(2)

                        If Array.IndexOf(Me.mstrTablasNOAuditar, strTabla) = -1 Then
                            ' Audito la sentencia sql
                            strSqlAudit = "INSERT INTO SE_SIST_EVENTOS(FechaHoraLog, StringSql, Usuario, Supervisor, Operacion, Tabla, Sistema, NombrePC ) VALUES ( "
                            strSqlAudit &= XtoStr(FechaServidor) + ", " & XtoStr(strSql)
                            strSqlAudit &= ", " & XtoStr(Me.mstrUsuarioActivo) & ", " & XtoStr(Me.mstrUsuarioSupervisor) & ", " & XtoStr(strOperAudit) & ", " & XtoStr(strTabla)
                            strSqlAudit &= ", " & XtoStr(mstrSistema) & ", " & XtoStr(mstrTerminal) & " )"

                            cmSQL.CommandText = strSqlAudit
                            cmSQL.ExecuteNonQuery()
                        End If
                    End If
                End If
            Loop

        End Sub
#End Region

#Region "XtoStr"

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Byte para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Byte</param>
        ''' <returns>String que representa el valor de tipo Byte para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As Byte) As String
            Return Valor.ToString
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Short para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Short</param>
        ''' <returns>String que representa el valor de tipo Short para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As Short) As String
            Return Valor.ToString
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Long para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Long</param>
        ''' <returns>String que representa el valor de tipo Long para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As Long) As String
            Return Valor.ToString
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Double para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Double</param>
        ''' <returns>String que representa el valor de tipo Double para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As Double) As String
            'Reeemplazo de separador decimal de la configuraci�n regional
            Return Valor.ToString.Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, SeparadorDecimal)
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Decimal para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Decimal</param>
        ''' <returns>String que representa el valor de tipo Decimal para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As Decimal) As String
            'Reeemplazo de separador decimal de la configuraci�n regional
            Return Valor.ToString.Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, SeparadorDecimal)
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo String para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo String</param>
        ''' <returns>String que representa el valor de tipo String para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As String) As String
            'Reemplazo por ej. 80� 10' 
            Return "'" & Valor.Replace("'", "''") & "'"
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Date para la Base de datos segun el idioma
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Date</param>
        ''' <returns>String que representa el valor de tipo Date para la Base de datos segun el idioma</returns>
        Public Overridable Function XtoStr(ByVal Valor As Date) As String
            Dim strComilla As Char = Me.SimboloComillaDia

            Return strComilla & Format(Valor, FormatoFecha) & strComilla
        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Char para la Base de datos segun el idioma
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Char</param>
        ''' <returns>String que representa el valor de tipo Char para la Base de datos segun el idioma</returns>
        Public Function XtoStr(ByVal Valor As Char) As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' strComilla : Valor que contiene la comilla
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strComilla As Char = Me.SimboloComillaDia

            Return strComilla & Valor & strComilla

        End Function

        ''' <summary>
        ''' Obtiene el string que representa el valor de tipo Boolean para la Base de datos
        ''' </summary>
        ''' <param name="Valor">Valor de tipo Boolean</param>
        ''' <returns>String que representa el valor de tipo Boolean para la Base de datos</returns>
        Public Function XtoStr(ByVal Valor As Boolean) As String
            Return CType(IIf(Valor, "1", "0"), String)
        End Function

#End Region

#Region "Utilidades"

        ''' <summary>
        ''' Retorna el separador decimal de acuerdo al idioma utilizado para conectarse a 
        ''' la base de datos
        ''' </summary>
        ''' <returns>Separador decimal de acuerdo al idioma utilizado para conectarse a 
        ''' la base de datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Protected Function SeparadorDecimal() As String

            Select Case Me.mbytIdiomaDB
                Case IdiomaDB.Espa�ol
                    Return ","
                Case IdiomaDB.Ingles
                    Return "."
                Case Else
                    Return "."
            End Select

        End Function

        ''' <summary>
        ''' Retorna el formato a aplicar a una variable de tipo fecha de acuerdo 
        ''' al idioma utilizado para conectarse a la base de datos
        ''' </summary>
        ''' <returns>Formato a aplicar a una variable de tipo fecha de acuerdo 
        ''' al idioma utilizado para conectarse a la base de datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Protected Function FormatoFecha() As String

            Select Case Me.mbytIdiomaDB
                Case IdiomaDB.Espa�ol
                    Return "dd/MM/yyyy HH:mm:ss"
                Case IdiomaDB.Ingles
                    Return "MM/dd/yyyy HH:mm:ss"
                Case Else
                    Return "MM/dd/yyyy HH:mm:ss"
            End Select

        End Function

#End Region

#Region "Interacci�n con el motor"

        ''' <summary>
        ''' Permite finalizar una transaccion abierta haciendo Commit o RollBack
        ''' </summary>
        ''' <param name="Value">True para realizar Commit, False para RollBack.</param>
        ''' <remarks></remarks>
        Public Sub TransaccionFinalizar(ByVal Value As Boolean)
            Try
                If mblnEnTransaccion Then
                    If Value Then
                        mtrTransaccion.Commit()
                    Else
                        mtrTransaccion.Rollback()
                    End If
                    mblnEnTransaccion = False
                Else
                    Throw New Exception("No existe Transacci�n abierta.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Ejecuta una sentencia Sql y retorna la cantidad de filas afectadas
        ''' </summary>
        ''' <param name="SentSQL">Sentencia Sql a ejecutar</param>
        ''' <returns>Cantidad de registros afectados en la operacion</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Public Function Ejecutar(ByVal SentSQL As String) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexion a la base de datos
            'cmSql      : Sentencia SQL a ejecutar
            'intRet     : Cantidad de filas afectadas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.IDbConnection
            Dim cmSql As System.Data.IDbCommand
            Dim intRet As Integer

            Try
                cnConexion = Me.Conectar()

                cmSql = cnConexion.CreateCommand()
                With cmSql
                    .CommandText = SentSQL
                    .CommandType = CommandType.Text
                    If mintTimeOutSQL >= 0 Then
                        .CommandTimeout = mintTimeOutSQL
                    End If
                    If Me.mblnEnTransaccion Then .Transaction = Me.mtrTransaccion
                    intRet += .ExecuteNonQuery()
                    Auditar(cmSql, SentSQL)
                End With

                Return intRet

            Catch ex As Exception
                Throw New Exception(ex.Message & vbCrLf & SentSQL, ex)
            End Try

        End Function

        ''' <summary>
        ''' Ejecuta una sentencia Sql y retorna la cantidad de filas afectadas
        ''' </summary>
        ''' <param name="cmSql">Objeto Command que contiene la Sentencia Sql a ejecutar</param>
        ''' <returns>Cantidad de registros afectados</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [08/11/2007]       Creado
        ''' </history>
        Public Function Ejecutar(ByVal cmSql As System.Data.IDbCommand) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'intRet     : Cantidad de filas afectadas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim intRet As Integer

            Try
                intRet += cmSql.ExecuteNonQuery()
                cmSql.Parameters.Clear()
                Auditar(cmSql, cmSql.CommandText)

                Return intRet
            Catch ex As Exception
                Dim strEx As String = ""

                strEx = cmSql.CommandText
                strEx &= vbCrLf
                strEx &= vbCrLf
                strEx &= "Paramatros:"
                strEx &= vbCrLf

                For intI As Integer = 0 To cmSql.Parameters.Count - 1
                    strEx &= CType(cmSql.Parameters(intI), System.Data.IDbDataParameter).ParameterName
                    strEx &= " = "
                    If CType(cmSql.Parameters(intI), System.Data.IDbDataParameter).Value Is Nothing Then
                        strEx &= "null"
                    ElseIf CType(cmSql.Parameters(intI), System.Data.IDbDataParameter).Value.Equals(System.DBNull.Value) Then
                        strEx &= "System.DBNull.Value"
                    Else
                        strEx &= CType(cmSql.Parameters(intI), System.Data.IDbDataParameter).Value.ToString()
                    End If
                Next

                Throw New Exception(ex.Message & vbCrLf & strEx, ex)

            End Try

        End Function


        ''' <summary>
        ''' Ejecuta una sentencia Sql y retorna la cantidad de filas afectadas
        ''' </summary>
        ''' <param name="cmSql">Objeto Command que contiene la Sentencia Sql a ejecutar</param>
        ''' <returns>DataReader</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [01/09/2009]       Creado
        ''' </history>
        Public Function EjecutarCommand_Reader(ByVal cmSql As System.Data.IDbCommand) As System.Data.IDataReader
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'dtrRet     : DataReader de retorno
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtrRet As System.Data.IDataReader

            Try
                dtrRet = cmSql.ExecuteReader()

                Return dtrRet
            Catch ex As Exception
                Dim strEx As String = ""

                strEx = cmSql.CommandText
                strEx &= vbCrLf
                strEx &= vbCrLf
                strEx &= "Paramatros:"
                strEx &= vbCrLf

                For intI As Integer = 0 To cmSql.Parameters.Count - 1
                    strEx &= CType(cmSql.Parameters(intI), System.Data.IDbDataParameter).ParameterName
                    strEx &= " = "
                    strEx &= CType(cmSql.Parameters(intI), System.Data.IDbDataParameter).Value.ToString
                Next

                Throw New Exception(ex.Message & vbCrLf & strEx, ex)

            End Try

        End Function




        ''' <summary>
        ''' Ejecuta una sentencia Sql y retorna la primera columna de la primer fila
        ''' </summary>
        ''' <param name="SentSQL">Sentencia Sql a ejecutar</param>
        ''' <returns>Valor de retorno de la sentencia SQL</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [23/03/2005]       Creado
        ''' [GustavoM]          [05/01/2010]       Auditor�a de INSERT INTO
        ''' </history>
        Public Function EjecutarEscalar(ByVal SentSQL As String) As Object
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexion a la base de datos
            'cmSql      : Sentencia SQL a ejecutar
            'objRet     : Cantidad de filas afectadas
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.IDbConnection
            Dim cmSql As System.Data.IDbCommand
            Dim objRet As Object

            'Verifico si se envia una sentencia SQL
            SentSQL = SentSQL.Trim
            If SentSQL.ToUpper.IndexOf("SELECT") >= 0 Then
                Try
                    cnConexion = Me.Conectar()
                    cmSql = cnConexion.CreateCommand()
                    With cmSql
                        .CommandText = SentSQL
                        .CommandType = CommandType.Text
                        If mintTimeOutSQL >= 0 Then
                            .CommandTimeout = mintTimeOutSQL
                        End If
                        If Me.mblnEnTransaccion Then .Transaction = Me.mtrTransaccion
                        objRet = .ExecuteScalar()

                        'Por ejemplo, si se trata de un INSERT INTO..SELECT @@SCOPE_IDENTITY()
                        If SentSQL.Trim().Substring(0, 11).ToUpper().Equals("INSERT INTO") Then
                            Auditar(cmSql, SentSQL.Split(";"c)(0))
                        End If

                        Return objRet

                    End With

                Catch ex As Exception
                    'Se dispara la excepcion, no se grabar en archivo de error
                    'porque se graba en Visor de Sucesos
                    'Me.GrabarError(ex.Source, ex.TargetSite.Name, ex.Message, SentSQL)
                    Throw New Exception(ex.Message & vbCrLf & SentSQL, ex)
                End Try
            Else
                Throw New Exception("La sentencia SQL debe poseer una sentencia SQL-SELECT")

            End If
        End Function

        ''' <summary>
        ''' Ejecuta una sentencia Sql a trav�s de un objeto Command
        ''' </summary>
        ''' <param name="objCommand">Objeto Command a ejecutar</param>
        ''' <returns>Valor de retoro</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [03/08/2009]       Creado
        ''' [GustavoM]          [05/01/2010]       Auditor�a de INSERT INTO
        ''' [LucianoP]          [mi�rcoles, 26 de abril de 2017]    Se quita el ejecutar escalar de sobra
        ''' </history>
        Public Function EjecutarEscalar(ByVal objCommand As System.Data.IDbCommand) As Object
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Dim objRet As Object

            If objCommand.CommandText.ToUpper().Trim().IndexOf("SELECT") >= 0 Then
                Try
                    If objCommand.Connection Is Nothing Then
                        objCommand.Connection = Me.Conectar()
                        If mintTimeOutSQL >= 0 Then
                            objCommand.CommandTimeout = mintTimeOutSQL
                        End If
                        If Me.mblnEnTransaccion Then objCommand.Transaction = Me.mtrTransaccion
                    End If

                    'objRet = objCommand.ExecuteScalar()
                    'Por ejemplo, si se trata de un INSERT INTO..SELECT @@SCOPE_IDENTITY()
                    If objCommand.CommandText.Trim().Substring(0, 11).ToUpper().Equals("INSERT INTO") Then
                        Auditar(objCommand, objCommand.CommandText.Split(";"c)(0))
                    End If

                    Return objCommand.ExecuteScalar()

                Catch ex As Exception
                    'Se dispara la excepcion, no se grabar en archivo de error
                    'porque se graba en Visor de Sucesos
                    'Me.GrabarError(ex.Source, ex.TargetSite.Name, ex.Message, SentSQL)
                    Throw New Exception(ex.Message & vbCrLf & objCommand.CommandText, ex)
                End Try
            Else
                Throw New Exception("La sentencia SQL debe poseer una sentencia SQL-SELECT")
            End If
        End Function

        ''' <summary>
        ''' Ejecuta una sentencia SQL-SELECT y retorna un objeto DataReader
        ''' Este m�todo no tiene en cuenta la propiedad ConexionPermanente, con lo cual,
        ''' no cierra la conexi�n si �sta propiedad se encuentra en True
        ''' </summary>
        ''' <param name="SentSQL">Sentencia SQL-SELECT a ejecutar</param>
        ''' <param name="dtrDatos">Objeto DataReader</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Public Sub Ejecutar(ByVal SentSQL As String, ByRef dtrDatos As System.Data.IDataReader)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                      DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexi�n a la base de datos
            'cmEjecutar : Objeto DataCommand que se utiliza para retornar la sentencia
            '             SQL
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.IDbConnection
            Dim cmEjecutar As System.Data.IDbCommand

            If Not SentSQL.Substring(0, 6).ToUpper.Equals("SELECT") Then
                Throw New Exception("Este m�todo solo ejecuta sentencias SQL-SELECT")
            Else
                Try
                    cnConexion = Me.Conectar()
                    cmEjecutar = cnConexion.CreateCommand()
                    With cmEjecutar
                        .CommandText = SentSQL
                        .CommandType = CommandType.Text
                        If mintTimeOutSQL >= 0 Then
                            .CommandTimeout = mintTimeOutSQL
                        End If
                        If Me.mblnEnTransaccion Then .Transaction = Me.mtrTransaccion
                        dtrDatos = .ExecuteReader()
                    End With
                Catch ex As Exception
                    Throw New Exception(ex.Message & vbCrLf & SentSQL, ex)
                End Try
            End If

        End Sub

#End Region

#Region "M�todos que deben sobreescribir las clases derivadas"
        'Ejecuta una sentencia SQL y retorna un cursor desconectado
        Public MustOverride Function Ejecutar(ByVal SentSQL As String, ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer

        Public MustOverride Function Ejecutar(ByVal cmdSQL As System.Data.IDbCommand, ByVal Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer

        'Realiza la conexi�n a la base de datos o retorna la conexi�n activa
        Protected MustOverride Function Conectar() As System.Data.IDbConnection

        'Cierra la conexi�n con la base de datos
        Friend MustOverride Sub Desconectar()

        'Retorna el s�mbolo que representa un tipo de dato fecha en el motor de base de datos
        Protected MustOverride Function SimboloComillaDia() As Char

        'Retorna la fecha y hora actual de acuerdo al motor de base de datos
        Public MustOverride Function FechaServidor() As Date

        'Retorna la funci�n del motor que convierte un valor Null a otro valor
        Public MustOverride Function ISNULL(ByVal Campo As String, ByVal Valor As String) As String

        'Activa la transacci�n
        Public MustOverride Sub TransaccionIniciar()

        'Arma el string de conexi�n 
        Friend MustOverride Function ArmarStringConexion() As String

        'Recupera un objeto Command
        Public MustOverride Function RecuperarComando() As System.Data.IDbCommand

        'Retorna un par�metro inicializado
        Public MustOverride Function AgregarParametroComando(ByVal Nombre As String, ByVal Tipo As System.Data.DbType, ByVal Direccion As System.Data.ParameterDirection, Optional ByVal Tama�o As Integer = -1, Optional ByVal Valor As Object = Nothing) As System.Data.IDataParameter

        'Inserta datos en un tabla 
        Public MustOverride Function Insertar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer

        'Actualiza datos en un tabla 
        Public MustOverride Function Actualizar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer

        'Eliminar datos en un tabla 
        Public MustOverride Function Eliminar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer

        'Cambia la conexion a la base de datos recibida como parametro
        Public MustOverride Sub CambiarConexion(ByVal NombreDB As String)

        Public MustOverride Function CrearConexion_EntityFramework_SQL(ByVal asm As Assembly) As EntityConnection

#End Region

        Friend Sub New()
            'Defino por defecto, el directorio donde se encuentra el Assembly que inicia el proceso
            Me.mstrDirectorioErrorSQL = System.Reflection.Assembly.GetCallingAssembly.Location
            Me.mstrDirectorioErrorSQL = Me.mstrDirectorioErrorSQL.Substring(0, Me.mstrDirectorioErrorSQL.LastIndexOf("\"c) + 1)
            mstrDirectorioErrorSQL &= "ErrorSQL.xml"
        End Sub

    End Class

End Namespace