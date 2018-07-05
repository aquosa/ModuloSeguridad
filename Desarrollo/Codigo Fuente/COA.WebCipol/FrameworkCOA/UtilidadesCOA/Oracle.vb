Imports System.Reflection
Imports System.Data.EntityClient

Namespace Datos.Origenes

    ''' <summary>
    ''' Clase que contiene funcionalidad para interactuar con un DBMS de tipo ORACLE utilizando ADO.NET
    ''' </summary>
    Public Class Oracle
        Inherits AccesoDB

        Private mcnConexion As System.Data.OracleClient.OracleConnection

        ''' <summary>
        ''' Realiza la conexión a la base de datos o retorna el objeto Connection
        ''' si la conexión se encuentra abierta
        ''' </summary>
        ''' <returns>Conexion a la Base de Datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [29/03/2005]       Creado
        ''' </history>
        Protected Overrides Function Conectar() As System.Data.IDbConnection

            'Si se conecta a Oracle
            If mstrConexion.Equals(String.Empty) Then
                Me.mstrConexion = ArmarStringConexion()
            End If
            If mcnConexion Is Nothing Then
                Me.mblnEnTransaccion = False
                mcnConexion = New System.Data.OracleClient.OracleConnection
                mcnConexion.ConnectionString = Me.mstrConexion
                mcnConexion.Open()

                Select Case Me.mbytIdiomaDB
                    Case IdiomaDB.Ingles
                        With mcnConexion.CreateCommand
                            .CommandType = CommandType.Text
                            .CommandText = "ALTER SESSION SET NLS_TERRITORY = 'AMERICA'"
                            .ExecuteNonQuery()
                            .CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'MM/DD/YYYY HH24:MI:SS'"
                            .ExecuteNonQuery()
                        End With
                    Case IdiomaDB.Español
                        With mcnConexion.CreateCommand
                            .CommandType = CommandType.Text
                            .CommandText = "ALTER SESSION SET NLS_TERRITORY = 'SPAIN'"
                            .ExecuteNonQuery()
                            .CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'"
                            .ExecuteNonQuery()
                        End With
                    Case Else
                        Throw New Exception("Falta establecer la configuración de NLS_PARAMETERS")
                End Select
            Else
                'Verifico si la conexión se encuentra cerrada
                If mcnConexion.State = ConnectionState.Closed Then
                    Me.mblnEnTransaccion = False
                    mcnConexion.ConnectionString = Me.mstrConexion
                    mcnConexion.Open()
                End If
            End If

            Return mcnConexion

        End Function

        ''' <summary>
        ''' Cierra la conexión con la base de datos
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [29/03/2005]       Creado
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
        ''' [GustavoM]          [29/03/2005]       Creado
        ''' </history>
        Public Overloads Overrides Function Ejecutar(ByVal SentSQL As String, ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OracleClient.OracleConnection
            Dim daEjecutar As System.Data.OracleClient.OracleDataAdapter

            Try
                cnConexion = CType(Me.Conectar(), System.Data.OracleClient.OracleConnection)
                daEjecutar = New System.Data.OracleClient.OracleDataAdapter(SentSQL, cnConexion)
                If mintTimeOutSQL >= 0 Then
                    daEjecutar.SelectCommand.CommandTimeout = mintTimeOutSQL
                End If
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OracleClient.OracleTransaction)
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
            ' daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            ' strEx      : Contiene informacion de la excepcion que se produjo
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim daEjecutar As System.Data.OracleClient.OracleDataAdapter

            Try

                daEjecutar = New System.Data.OracleClient.OracleDataAdapter(CType(cmdSQL, System.Data.OracleClient.OracleCommand))
                If mintTimeOutSQL >= 0 Then
                    daEjecutar.SelectCommand.CommandTimeout = mintTimeOutSQL
                End If
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OracleClient.OracleTransaction)
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
        ''' <returns>Fecha y hora actual del servidor</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [29/03/2005]       Creado
        ''' </history>
        Public Overrides Function FechaServidor() As Date
            Return CType(Me.EjecutarEscalar("SELECT SYSDATE FROM DUAL"), Date)
        End Function

        ''' <summary>
        ''' Obtiene el simbolo de comilla
        ''' </summary>
        ''' <returns>Simbolo de comilla</returns>
        ''' <remarks></remarks>
        Protected Overrides Function SimboloComillaDia() As Char
            Return "'"c
        End Function

        ''' <summary>
        ''' Inicia transacción con la base de datos
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [29/03/2005]       Creado
        ''' </history>
        Public Overrides Sub TransaccionIniciar()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            ' cnConexion : Conexion a la Base de Datos
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If Me.mblnEnTransaccion Then
                Throw New Exception("Ya existe una transacción abierta.")
            Else
                Try
                    Dim cnConexion As System.Data.OracleClient.OracleConnection
                    cnConexion = CType(Me.Conectar, System.Data.OracleClient.OracleConnection)
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
        ''' Retorna la expresion para ser utilizada en ORACLE para formatear una fecha
        ''' </summary>
        ''' <param name="Valor">Fecha utilizada para obtener la expresion de ORACLE</param>
        ''' <returns>Expresion para ser utilizada en ORACLE para formatear una fecha</returns>
        ''' <remarks></remarks>
        Public Overloads Overrides Function XtoStr(ByVal Valor As Date) As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' strComilla       : Simbolo de comilla
            ' strFormatoToDate : Valor que contiene la mascar a aplicar de acuerdo 
            '                    al idioma
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim strComilla As Char = Me.SimboloComillaDia
            Dim strFormatoToDate As String

            If Me.mbytIdiomaDB = AccesoDB.IdiomaDB.Español Then
                strFormatoToDate = "'DD/MM/YYYY HH24:MI:SS'"
            Else
                strFormatoToDate = "'MM/DD/YYYY HH24:MI:SS'"
            End If

            Return "TO_DATE(" & strComilla & Format(Valor, Me.FormatoFecha) & strComilla & ", " & strFormatoToDate & ")"

        End Function

        ''' <summary>
        ''' Obtiene el string que representa en ORACLE la funcion NVL(Campo, Valor)
        ''' </summary>
        ''' <param name="Campo">Es la expresión que se va a comprobar si es NULL. Puede ser de cualquier tipo.</param>
        ''' <param name="Valor">Es la expresión que se devuelve si Campo es NULL. 
        ''' Valor debe ser de un tipo que se pueda convertir implícitamente al tipo de Campo.</param>
        ''' <returns>Reemplaza NULL con el valor de reemplazo especificado.</returns>
        ''' <remarks></remarks>
        Public Overrides Function ISNULL(ByVal Campo As String, ByVal Valor As String) As String
            Return "NVL(" & Campo & "," & Valor & ")"
        End Function

        Friend Sub New()
            MyBase.New()
            Me.mstrConectadoA = "Oracle"
        End Sub

        ''' <summary>
        ''' Arma el string de conexion para conectarse a una Base de Datos ORACLE
        ''' </summary>
        ''' <returns>String que contiene la conexion a la Base de Datos ORACLE</returns>
        ''' <remarks></remarks>
        Friend Overrides Function ArmarStringConexion() As String
            Dim strConexion As String


            If Me.mstrLogin.Length = 0 Then 'GCP-Cambios ID: 7750
                strConexion = ";Integrated Security=True"
            Else
                strConexion = "Integrated Security=False" + ";user id=" & Me.mstrLogin & _
                                                ";password=" & Me.mstrClaveLogin
            End If

            strConexion += ";Data Source=" + Me.mstrServidor
            strConexion += ";persist security info=False"
            If Me.mintPoolMax = 0 And Me.mintPoolMin = 0 Then
                strConexion += ";Pooling=False"
            Else
                strConexion += ";Max Pool Size=" & Me.mintPoolMax.ToString
                strConexion += ";Min Pool Size=" & Me.mintPoolMin.ToString
                strConexion += ";Pooling=True"
            End If

            Return strConexion

        End Function

        ''' <summary>
        ''' Permite recuperar un comando activo, para que pueda ejecutarse un SP
        ''' </summary>
        ''' <returns>Objeto Command activo</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [18/10/2004]       Creado
        ''' </history>
        Public Overrides Function RecuperarComando() As System.Data.IDbCommand
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                DESCRIPCION DE LAS VARIABLES LOCALES
            'cmSql : Sentencia SQL a ejecutar
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
        ''' <param name="Tamaño">Longitud de parámetro.</param>
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
            ' objParam : Parametro de un SP de ORACLE
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objParam As New System.Data.OracleClient.OracleParameter

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
        ''' [Juano]               [23/08/2007]            Creado
        ''' </history>
        Public Overloads Overrides Function Insertar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de Inserción
            'DataTable  : Datatable con los datos a Insertar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OracleClient.OracleConnection
            Dim daEjecutar As System.Data.OracleClient.OracleDataAdapter
            Dim cmdBuilder As System.Data.OracleClient.OracleCommandBuilder
            Dim DataTable As DataTable
            Try
                cnConexion = CType(Me.Conectar(), System.Data.OracleClient.OracleConnection)
                daEjecutar = New System.Data.OracleClient.OracleDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OracleClient.OracleTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.OracleClient.OracleCommandBuilder(daEjecutar)
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
        ''' [Juano]               [23/08/2007]            Creado
        ''' </history>
        Public Overloads Overrides Function Actualizar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de Actualización
            'DataTable  : Datatable con los datos a Actualizar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OracleClient.OracleConnection
            Dim daEjecutar As System.Data.OracleClient.OracleDataAdapter
            Dim cmdBuilder As System.Data.OracleClient.OracleCommandBuilder
            Dim DataTable As DataTable
            Try
                'Crea la Conexión
                cnConexion = CType(Me.Conectar(), System.Data.OracleClient.OracleConnection)
                daEjecutar = New System.Data.OracleClient.OracleDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OracleClient.OracleTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.OracleClient.OracleCommandBuilder(daEjecutar)
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
        ''' [Juano]               [23/08/2007]            Creado
        ''' </history>
        Public Overloads Overrides Function Eliminar(ByRef Datos As System.Data.DataSet, ByVal NombreTabla As String) As Integer
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                        DESCRIPCION DE VARIABLES LOCALES
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de eliminación
            'DataTable  : Datatable con los datos a Eliminar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.OracleClient.OracleConnection
            Dim daEjecutar As System.Data.OracleClient.OracleDataAdapter
            Dim cmdBuilder As System.Data.OracleClient.OracleCommandBuilder
            Dim DataTable As DataTable
            Try
                'Crea la Conexión
                cnConexion = CType(Me.Conectar(), System.Data.OracleClient.OracleConnection)
                daEjecutar = New System.Data.OracleClient.OracleDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.OracleClient.OracleTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.OracleClient.OracleCommandBuilder(daEjecutar)
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