Imports System.Data.EntityClient
Imports System.Reflection

Namespace Datos.Origenes

    ''' <summary>
    ''' Clase que contiene funcionalidad para interactuar con un DBMS de tipo ACCESS (OLE DB) utilizando ADO.NET
    ''' </summary>
    Public Class Access
        Inherits AccesoDB

        Private mcnConexion As System.Data.OleDb.OleDbConnection

        Public Enum ConectarA As Integer
            Access
            SQLServer
            Oracle
        End Enum

        Private mintTipoConexion As ConectarA = ConectarA.Access

        'Clave de la base de datos Access
        Private mstrClaveMDB As String = ""

        ''' <summary>
        ''' Establece a la clave de acceso al archivo MDB
        ''' </summary>
        ''' <value>Clave de acceso al archivo MDB</value>
        ''' <remarks></remarks>
        Public WriteOnly Property ClaveMDB() As String
            Set(ByVal Value As String)
                Me.mstrClaveMDB = Value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Realiza la conexión a la base de datos o retorna el objeto Connection
        ''' si la conexión se encuentra abierta
        ''' </summary>
        ''' <returns>Conexion a la base de datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Protected Overrides Function Conectar() As System.Data.IDbConnection
            Dim cmSql As System.Data.OleDb.OleDbCommand

            If mstrConexion.Equals(String.Empty) Then
                'Si se conecta a Access
                Me.mstrConexion = ArmarStringConexion()
            End If

            If mcnConexion Is Nothing Then
                mcnConexion = New System.Data.OleDb.OleDbConnection
                mcnConexion.ConnectionString = Me.mstrConexion
                mcnConexion.Open()
            Else
                'Verifico si la conexión se encuentra cerrada
                If mcnConexion.State = ConnectionState.Closed Then
                    mcnConexion.ConnectionString = Me.mstrConexion
                    mcnConexion.Open()
                End If
            End If

            cmSql = mcnConexion.CreateCommand()
            cmSql.CommandType = CommandType.Text
            Select Case mintTipoConexion
                Case ConectarA.SQLServer
                    cmSql.CommandText = "SET LANGUAGE "
                    If mblnEnTransaccion Then cmSql.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                    If mbytIdiomaDB = IdiomaDB.Ingles Then
                        cmSql.CommandText += "'us_english'"
                    Else
                        cmSql.CommandText += "'spanish'"
                    End If
                    cmSql.ExecuteNonQuery()
                Case ConectarA.Oracle
                    Select Case Me.mbytIdiomaDB
                        Case IdiomaDB.Ingles
                            If mblnEnTransaccion Then cmSql.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                            cmSql.CommandText = "ALTER SESSION SET NLS_TERRITORY = 'AMERICA'"
                            cmSql.ExecuteNonQuery()
                            cmSql.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'MM/DD/YYYY HH24:MI:SS'"
                            cmSql.ExecuteNonQuery()
                        Case IdiomaDB.Español
                            If mblnEnTransaccion Then cmSql.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                            cmSql.CommandText = "ALTER SESSION SET NLS_TERRITORY = 'SPAIN'"
                            cmSql.ExecuteNonQuery()
                            cmSql.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'"
                            cmSql.ExecuteNonQuery()
                        Case Else
                            Throw New Exception("Falta establecer la configuración de NLS_PARAMETERS")
                    End Select

            End Select

            Return mcnConexion

        End Function

        ''' <summary>
        ''' Cierra la conexión con la base de datos
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Friend Overrides Sub Desconectar()
            If Not mcnConexion Is Nothing Then
                If Not mcnConexion.State = ConnectionState.Closed Then mcnConexion.Close()
                mcnConexion = Nothing
            End If
        End Sub

        ''' <summary>
        ''' Ejecuta una sentencia Sql-SELECT y retorna un conjunto de datos
        ''' </summary>
        ''' <param name="SentSQL">Sentencia Sql a ejecutar</param>
        ''' <param name="Datos">DataSet que se debe llenar con datos</param>
        ''' <param name="NombreTabla">Nombre de tabla a crear o anexar resultado</param>
        ''' <returns>Conjunto de datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Public Overloads Overrides Function Ejecutar(ByVal SentSQL As String, ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OleDb.OleDbConnection
            Dim daEjecutar As System.Data.OleDb.OleDbDataAdapter

            Try
                cnConexion = CType(Me.Conectar(), System.Data.OleDb.OleDbConnection)
                daEjecutar = New System.Data.OleDb.OleDbDataAdapter(SentSQL, cnConexion)
                If mintTimeOutSQL >= 0 Then
                    daEjecutar.SelectCommand.CommandTimeout = mintTimeOutSQL
                End If
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                If Datos Is Nothing Then Datos = New System.Data.DataSet
                Return daEjecutar.Fill(Datos, NombreTabla)
            Catch ex As Exception
                Throw New Exception(ex.Message & vbCrLf & SentSQL, ex)
            End Try

        End Function

        ''' <summary>
        ''' Ejecuta una sentencia Sql-SELECT y retorna un conjunto de datos
        ''' </summary>
        ''' <param name="cmdSQL">Objeto Command a ejecutar</param>
        ''' <param name="Datos">DataSet que se debe llenar con datos</param>
        ''' <param name="NombreTabla">Nombre de tabla a crear o anexar resultado</param>
        ''' <returns>Conjunto de datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/10/2007]       Creado
        ''' </history>
        Public Overrides Function Ejecutar(ByVal cmdSQL As System.Data.IDbCommand, ByVal Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim daEjecutar As System.Data.OleDb.OleDbDataAdapter

            Try
                daEjecutar = New System.Data.OleDb.OleDbDataAdapter(CType(cmdSQL, System.Data.OleDb.OleDbCommand))
                If mintTimeOutSQL >= 0 Then
                    daEjecutar.SelectCommand.CommandTimeout = mintTimeOutSQL
                End If
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                If Datos Is Nothing Then Datos = New System.Data.DataSet
                Return daEjecutar.Fill(Datos, NombreTabla)
            Catch ex As Exception
                Dim strEx As String = ""

                strEx = cmdSQL.CommandText
                strEx &= vbCrLf
                strEx &= vbCrLf
                strEx &= "Paramatros:"
                strEx &= vbCrLf

                For intI As Integer = 0 To cmdSQL.Parameters.Count - 1
                    strEx &= CType(cmdSQL.Parameters(intI), System.Data.IDbDataParameter).ParameterName
                    strEx &= " = "
                    strEx &= CType(cmdSQL.Parameters(intI), System.Data.IDbDataParameter).Value.ToString
                Next
                Throw New Exception(ex.Message & vbCrLf & strEx, ex)
            End Try

        End Function

        ''' <summary>
        ''' Retorna la fecha y hora actual del servidor
        ''' </summary>
        ''' <returns>Fecha y hora actual</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Public Overrides Function FechaServidor() As Date
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            'dtrDatos   : Objeto DataSet que se utiliza para recuperar la fecha
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsDatos As System.Data.DataSet = Nothing

            Try
                'Debido a que estoy conectado con OleDB, determino cual es el 
                'proveedor
                If Me.mstrConexion.IndexOf("SQLOLEDB") >= 0 Then
                    Me.Ejecutar("SELECT GETDATE()", dtsDatos, "Fecha")
                    Return CType(dtsDatos.Tables("Fecha").Rows(0).Item(0), Date)
                ElseIf Me.mstrConexion.IndexOf("MSDAORA") >= 0 Then
                    Me.Ejecutar("SELECT SYSDATE FROM DUAL", dtsDatos, "Fecha")
                    Return CType(dtsDatos.Tables("Fecha").Rows(0).Item(0), Date)
                ElseIf Me.mstrConexion.IndexOf("Microsoft.Jet.OLEDB.4.0") >= 0 Then
                    Return Now
                End If

            Catch ex As Exception
                Throw
            End Try

        End Function

        ''' <summary>
        ''' Obtiene el simbolo de comilla
        ''' </summary>
        ''' <returns>Simbolo de comilla</returns>
        ''' <remarks></remarks>
        Protected Overrides Function SimboloComillaDia() As Char
            If Me.mstrConexion.IndexOf("Microsoft.Jet.OLEDB.4.0") >= 0 Then
                Return "#"c
            Else
                Return "'"c
            End If
        End Function

        ''' <summary>
        ''' Permite iniciar una transaccion sobre la conexion establecida
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub TransaccionIniciar()
            If Me.mblnEnTransaccion Then
                Throw New Exception("Ya existe una transacción abierta.")
            Else
                Try
                    Dim cnConexion As System.Data.OleDb.OleDbConnection
                    cnConexion = CType(Me.Conectar, System.Data.OleDb.OleDbConnection)
                    Me.mtrTransaccion = cnConexion.BeginTransaction
                    Me.mblnEnTransaccion = True
                Catch ex As Exception
                    'Se dispara la excepcion, no se grabar en archivo de error
                    'porque se graba en Visor de Sucesos
                    'Me.GrabarError(ex.Source, ex.TargetSite.Name, ex.Message, "No se pudo iniciar la transacción")
                    Throw
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Método No implementado
        ''' </summary>
        Public Overrides Function ISNULL(ByVal Campo As String, ByVal Valor As String) As String
            Throw New Exception("Método No implementado.")
        End Function

        Friend Sub New()
            MyBase.New()
            Me.mstrConectadoA = "Access"
        End Sub

        Friend Sub New(ByVal TipoConexion As ConectarA)
            MyBase.New()
            Select Case TipoConexion
                Case ConectarA.Access
                    Me.mstrConectadoA = "Access"
                Case ConectarA.SQLServer
                    Me.mstrConectadoA = "SQLServer"
                Case ConectarA.Oracle
                    Me.mstrConectadoA = "Oracle"
            End Select
            Me.mintTipoConexion = TipoConexion
        End Sub

        ''' <summary>
        ''' Arma el string de conexion para conectarse a una Base de Datos MSSQL
        ''' </summary>
        ''' <returns>String que contiene la conexion a la Base de Datos MSSQL</returns>
        ''' <remarks></remarks>
        Friend Overrides Function ArmarStringConexion() As String
            Select Case mintTipoConexion
                Case ConectarA.Access
                    Return "Jet OLEDB:Database Password=" & Me.mstrClaveMDB & ";Data Source=" & """" & Me.mstrServidor & """" & ";Password=" & Me.mstrClaveLogin & ";Provider=" & """" & "Microsoft.Jet.OLEDB.4.0" & """" & ";User ID=" & Me.mstrLogin
                Case ConectarA.SQLServer
                    Dim strCon As String
                    'Si se debe generar seguridad integrada
                    If mstrLogin.Length = 0 Then    'GCP-Cambios ID: 7750
                        strCon = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" + Me.mstrBase + ";Data Source=" + mstrServidor
                    Else
                        strCon = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" + mstrLogin + ";Initial Catalog=" + mstrBase + ";Data Source=" + mstrServidor + ";Password=" + mstrClaveLogin
                    End If
                    If Me.mintPoolMax = 0 And Me.mintPoolMin = 0 Then
                        strCon += ";OLE DB Services = -2"
                    End If

                    Return strCon
                Case ConectarA.Oracle
                    'Si se debe generar seguridad integrada
                    If mstrLogin.Length = 0 Then
                        Return "Provider=MSDAORA;Data Source=" + mstrServidor + ";Persist Security Info=False;Integrated Security=Yes;"
                    Else
                        Return "Provider=MSDAORA;Data Source=" + mstrServidor + ";User Id=" + mstrLogin + ";Password=" + mstrClaveLogin + ";"
                    End If

                Case Else
                    Throw New Exception("Origen de datos no soportado")
            End Select


        End Function

        ''' <summary>
        ''' Retorna un comando activo, para que pueda ejecutarse un SP
        ''' </summary>
        ''' <returns>Objeto Command activo</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [18/10/2004]       Creado
        ''' </history>
        Public Overrides Function RecuperarComando() As System.Data.IDbCommand
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                DESCRIPCION DE LAS VARIABLES LOCALES
            'cmSql      : Sentencia SQL a ejecutar
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cmSql As System.Data.IDbCommand

            cmSql = Me.Conectar.CreateCommand()
            If Me.mblnEnTransaccion Then cmSql.Transaction = Me.mtrTransaccion

            Return cmSql

        End Function

        ''' <summary>
        ''' Retorna un parámetro activo, para que pueda agregar a un Comando
        ''' </summary>
        ''' <param name="Nombre">Nombre del parámetro</param>
        ''' <param name="Tipo">Tipo de Dato</param>
        ''' <param name="Direccion">entrada o salida de datos</param>
        ''' <param name="Tamaño">Longitud de parámetro. </param>
        ''' <param name="Valor">Valor del parámetro</param>
        ''' <returns>Objeto Parameter activo</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [15/09/2005]       Creado
        ''' [LeandroF]          [23/03/2016]       TFS-6680
        ''' </history>
        Public Overrides Function AgregarParametroComando(ByVal Nombre As String, ByVal Tipo As System.Data.DbType, ByVal Direccion As System.Data.ParameterDirection, Optional ByVal Tamaño As Integer = -1, Optional ByVal Valor As Object = Nothing) As System.Data.IDataParameter
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' objParam : Parametro del comando SQL
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objParam As New System.Data.OleDb.OleDbParameter

            With objParam
                .ParameterName = Nombre
                .Direction = Direccion
                .DbType = Tipo
                If Tamaño > 0 Then .Size = Tamaño
                If Valor IsNot Nothing Then
                    .Value = Valor
                Else
                    .Value = DBNull.Value
                End If
            End With

            Return CType(objParam, System.Data.IDataParameter)

        End Function

        ''' <summary>
        ''' Inserta datos en la Tabla correspondiente y retorna las filas afectadas
        ''' </summary>
        ''' <param name="Datos">Dataset con las filas a Eliminar</param>
        ''' <param name="NombreTabla">Nombre de la Tabla a Eliminar</param>
        ''' <returns>Filas Afectadas</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' Juano               23/08/2007            Creado
        ''' </history>
        Public Overloads Overrides Function Insertar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de Inserción
            'DataTable  : Datatable con los datos a Insertar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OleDb.OleDbConnection
            Dim daEjecutar As System.Data.OleDb.OleDbDataAdapter
            Dim cmdBuilder As System.Data.OleDb.OleDbCommandBuilder
            Dim DataTable As DataTable

            Try
                cnConexion = CType(Me.Conectar(), System.Data.OleDb.OleDbConnection)
                daEjecutar = New System.Data.OleDb.OleDbDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.OleDb.OleDbCommandBuilder(daEjecutar)
                'Setea el comando de Inserción al DataAdapter
                daEjecutar.InsertCommand = cmdBuilder.GetInsertCommand
                'Setea los datos del Dataset de grabación a un Datatable
                DataTable = Datos.Tables(NombreTabla)
                'Actualiza los Registros y retorna las filas afectadas
                Return daEjecutar.Update(DataTable.Select(Nothing, Nothing, DataViewRowState.Added))
            Catch ex As Exception
                'Se dispara la excepcion, no se grabar en archivo de error
                'porque se graba en Visor de Sucesos
                'Me.GrabarError(ex.Source, ex.TargetSite.Name, ex.Message, SentSQL)
                Throw
            End Try

        End Function

        ''' <summary>
        ''' Actualiza datos en la Tabla correspondiente y retorna las filas afectadas
        ''' </summary>
        ''' <param name="Datos">Dataset con las filas a Actualizar</param>
        ''' <param name="NombreTabla">Nombre de la Tabla a Actualizar</param>
        ''' <returns>Filas Afectadas</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' Juano               23/08/2007            Creado
        ''' </history>
        Public Overloads Overrides Function Actualizar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de Actualización
            'DataTable  : Datatable con los datos a Actualizar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OleDb.OleDbConnection
            Dim daEjecutar As System.Data.OleDb.OleDbDataAdapter
            Dim cmdBuilder As System.Data.OleDb.OleDbCommandBuilder
            Dim DataTable As DataTable

            Try
                'Crea la Conexión
                cnConexion = CType(Me.Conectar(), System.Data.OleDb.OleDbConnection)
                daEjecutar = New System.Data.OleDb.OleDbDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.OleDb.OleDbCommandBuilder(daEjecutar)
                'Setea el comando de Actualización al DataAdapter
                daEjecutar.UpdateCommand = cmdBuilder.GetUpdateCommand
                'Setea los datos del Dataset de grabación a un Datatable
                DataTable = Datos.Tables(NombreTabla)
                'Actualiza los Registros y retorna las filas afectadas
                Return daEjecutar.Update(DataTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent))
            Catch ex As Exception
                'Se dispara la excepcion, no se grabar en archivo de error
                'porque se graba en Visor de Sucesos
                'Me.GrabarError(ex.Source, ex.TargetSite.Name, ex.Message, SentSQL)
                Throw
            End Try

        End Function

        ''' <summary>
        ''' Elimina datos en la Tabla correspondiente y retorna las filas afectadas
        ''' </summary>
        ''' <param name="Datos">Dataset con las filas a Eliminar</param>
        ''' <param name="NombreTabla">Nombre de la Tabla a Eliminar</param>
        ''' <returns>Filas Afectadas</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' Juano               23/08/2007            Creado
        ''' </history>
        Public Overloads Overrides Function Eliminar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de eliminación
            'DataTable  : Datatable con los datos a Eliminar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OleDb.OleDbConnection
            Dim daEjecutar As System.Data.OleDb.OleDbDataAdapter
            Dim cmdBuilder As System.Data.OleDb.OleDbCommandBuilder
            Dim DataTable As DataTable

            Try
                'Crea la Conexión
                cnConexion = CType(Me.Conectar(), System.Data.OleDb.OleDbConnection)
                daEjecutar = New System.Data.OleDb.OleDbDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OleDb.OleDbTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.OleDb.OleDbCommandBuilder(daEjecutar)
                'Setea el comando de eliminación al DataAdapter
                daEjecutar.DeleteCommand = cmdBuilder.GetDeleteCommand
                'Setea los datos del Dataset de grabación a un Datatable
                DataTable = Datos.Tables(NombreTabla)
                'Borra los Registros y retorna las filas afectadas
                Return daEjecutar.Update(DataTable.Select(Nothing, Nothing, DataViewRowState.Deleted))
            Catch ex As Exception
                'Se dispara la excepcion, no se grabar en archivo de error
                'porque se graba en Visor de Sucesos
                'Me.GrabarError(ex.Source, ex.TargetSite.Name, ex.Message, SentSQL)
                Throw
            End Try

        End Function

        Public Overrides Function CrearConexion_EntityFramework_SQL(ByVal asm As Assembly) As EntityConnection
            Return Nothing
        End Function

        'Cambia de string de conexion y lo persiste sobre la conexion
        Public Overrides Sub CambiarConexion(ByVal NombreDB As String)
            If String.IsNullOrEmpty(NombreDB) Then
                Throw New Exception("Conexión cerrada o incorrecta")
            End If

            mcnConexion.ChangeDatabase(NombreDB)
        End Sub

    End Class

End Namespace