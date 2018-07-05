Imports System.Data.Metadata.Edm
Imports System.Reflection
Imports System.Data.EntityClient

Namespace Datos.Origenes

    ''' <summary>
    ''' Clase que contiene funcionalidad para interactuar con un DBMS de tipo MSSQL utilizando ADO.NET
    ''' </summary>
    Public Class SQLServer
        Inherits AccesoDB

        Private mcnConexion As System.Data.SqlClient.SqlConnection

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
            Dim cnConexion As System.Data.SqlClient.SqlConnection
            Dim daEjecutar As System.Data.SqlClient.SqlDataAdapter

            Try
                cnConexion = CType(Me.Conectar(), System.Data.SqlClient.SqlConnection)
                daEjecutar = New System.Data.SqlClient.SqlDataAdapter(SentSQL, cnConexion)
                If mintTimeOutSQL >= 0 Then
                    daEjecutar.SelectCommand.CommandTimeout = mintTimeOutSQL
                End If
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.SqlClient.SqlTransaction)
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
            Dim daEjecutar As System.Data.SqlClient.SqlDataAdapter

            Try
                daEjecutar = New System.Data.SqlClient.SqlDataAdapter(CType(cmdSQL, System.Data.SqlClient.SqlCommand))
                If mintTimeOutSQL >= 0 Then
                    daEjecutar.SelectCommand.CommandTimeout = mintTimeOutSQL
                End If
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.SqlClient.SqlTransaction)
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
        ''' Obtiene la fecha y hora actual del servidor
        ''' </summary>
        ''' <returns>Fecha del servidor de la Base de Datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Public Overrides Function FechaServidor() As Date
            Return CType(Me.EjecutarEscalar("SELECT GETDATE()"), Date)
        End Function

        ''' <summary>
        ''' Realiza la conexión a la base de datos o retorna el objeto Connection
        ''' si la conexión se encuentra abierta
        ''' </summary>
        ''' <returns>Conexion a la Base de Datos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [17/05/2004]       Creado
        ''' </history>
        Protected Overrides Function Conectar() As System.Data.IDbConnection
            Dim cmSql As System.Data.IDbCommand

            'Si se conecta a SQL Server
            If mstrConexion.Equals(String.Empty) Then
                Me.mstrConexion = Me.ArmarStringConexion
            End If

            If mcnConexion Is Nothing Then
                Me.mblnEnTransaccion = False
                mcnConexion = New System.Data.SqlClient.SqlConnection
                mcnConexion.ConnectionString = Me.mstrConexion
                mcnConexion.Open()
            Else
                'Verifico si la conexión se encuentra cerrada
                If mcnConexion.State = ConnectionState.Closed Then
                    Me.mblnEnTransaccion = False
                    mcnConexion.ConnectionString = Me.mstrConexion
                    mcnConexion.Open()
                End If
            End If

            cmSql = mcnConexion.CreateCommand()
            cmSql.CommandText = "SET LANGUAGE "
            If mblnEnTransaccion Then cmSql.Transaction = CType(Me.mtrTransaccion, System.Data.SqlClient.SqlTransaction)
            If mbytIdiomaDB = IdiomaDB.Ingles Then
                cmSql.CommandText += "'us_english'"
            Else
                cmSql.CommandText += "'spanish'"
            End If
            cmSql.ExecuteNonQuery()

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
        ''' Obtiene el simbolo de comilla
        ''' </summary>
        ''' <returns>Simbolo de comilla</returns>
        ''' <remarks></remarks>
        Protected Overrides Function SimboloComillaDia() As Char
            Return "'"c
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
                    Dim cnConexion As System.Data.SqlClient.SqlConnection
                    cnConexion = CType(Me.Conectar, System.Data.SqlClient.SqlConnection)
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
        ''' Obtiene el string que representa en MSSQL la funcion ISNULL(Campo, Valor)
        ''' </summary>
        ''' <param name="Campo">Es la expresión que se va a comprobar si es NULL. Puede ser de cualquier tipo.</param>
        ''' <param name="Valor">Es la expresión que se devuelve si Campo es NULL. 
        ''' Valor debe ser de un tipo que se pueda convertir implícitamente al tipo de Campo.</param>
        ''' <returns>Reemplaza NULL con el valor de reemplazo especificado.</returns>
        ''' <remarks></remarks>
        Public Overrides Function ISNULL(ByVal Campo As String, ByVal Valor As String) As String
            Return "ISNULL(" & Campo & "," & Valor & ")"
        End Function

        Friend Sub New()
            MyBase.New()
            Me.mstrConectadoA = "SQLServer"
        End Sub

        ''' <summary>
        ''' Arma el string de conexion para conectarse a una Base de Datos MSSQL
        ''' </summary>
        ''' <returns>String que contiene la conexion a la Base de Datos MSSQL</returns>
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
            strConexion += ";Initial Catalog=" + mstrBase
            strConexion += ";workstation id=" & System.Environment.MachineName
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
        ''' Obtiene un comando activo, para que pueda ejecutarse un SP
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
        ''' <param name="Tamaño">Longitud de parámetro</param>
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
            ' objParam : Parametro que se agrega al comando
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objParam As New System.Data.SqlClient.SqlParameter

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
            '
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de Inserción
            'DataTable  : Datatable con los datos a Insertar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.SqlClient.SqlConnection
            Dim daEjecutar As System.Data.SqlClient.SqlDataAdapter
            Dim cmdBuilder As System.Data.SqlClient.SqlCommandBuilder
            Dim DataTable As DataTable

            Try
                cnConexion = CType(Me.Conectar(), System.Data.SqlClient.SqlConnection)
                daEjecutar = New System.Data.SqlClient.SqlDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.SqlClient.SqlTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.SqlClient.SqlCommandBuilder(daEjecutar)
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
            '
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de Actualización
            'DataTable  : Datatable con los datos a Actualizar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.SqlClient.SqlConnection
            Dim daEjecutar As System.Data.SqlClient.SqlDataAdapter
            Dim cmdBuilder As System.Data.SqlClient.SqlCommandBuilder
            Dim DataTable As DataTable

            Try
                cnConexion = CType(Me.Conectar(), System.Data.SqlClient.SqlConnection)
                daEjecutar = New System.Data.SqlClient.SqlDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.SqlClient.SqlTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.SqlClient.SqlCommandBuilder(daEjecutar)
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
            '
            'cnConexion : Conexión a la base de datos
            'daEjecutar : DataAdapter que se utiliza para recuperar el conjunto de datos
            'cmdBuilder : Comando de eliminación
            'DataTable  : Datatable con los datos a Eliminar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim cnConexion As System.Data.SqlClient.SqlConnection
            Dim daEjecutar As System.Data.SqlClient.SqlDataAdapter
            Dim cmdBuilder As System.Data.SqlClient.SqlCommandBuilder
            Dim DataTable As DataTable

            Try
                cnConexion = CType(Me.Conectar(), System.Data.SqlClient.SqlConnection)
                daEjecutar = New System.Data.SqlClient.SqlDataAdapter(" SELECT * FROM " & NombreTabla, cnConexion)
                If Me.mblnEnTransaccion Then daEjecutar.SelectCommand.Transaction = CType(Me.mtrTransaccion, System.Data.SqlClient.SqlTransaction)
                'Instancia el comando con el DataAdapter
                cmdBuilder = New System.Data.SqlClient.SqlCommandBuilder(daEjecutar)
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

        'Cambia de string de conexion y lo persiste sobre la conexion
        Public Overrides Sub CambiarConexion(ByVal NombreDB As String)
            If String.IsNullOrEmpty(NombreDB) Then
                Throw New Exception("Conexión cerrada o incorrecta")
            End If

            mcnConexion.ChangeDatabase(NombreDB)
        End Sub


        ''' <summary>
        ''' Crea la conexion del contexto Entity Framework 
        ''' </summary>
        ''' <returns>Conexion para un contexto de tipo Entity Framework</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [miércoles, 16 de abril de 2014]       Creado
        ''' </history>
        Public Overrides Function CrearConexion_EntityFramework_SQL(ByVal asm As Assembly) As EntityConnection
            Dim objWorkspace As MetadataWorkspace
            Dim objEFConexion As EntityConnection
            Dim cnConexion As System.Data.SqlClient.SqlConnection = Nothing

            Try
                Dim strConexion As String = ""

                If Not String.IsNullOrEmpty(mstrConexion) Then
                    strConexion = mstrConexion
                Else
                    strConexion = Me.ArmarStringConexion()
                End If

                Me.mblnEnTransaccion = False

                cnConexion = New System.Data.SqlClient.SqlConnection
                cnConexion.ConnectionString = strConexion

                objWorkspace = New MetadataWorkspace(New String() {"res://*/"}, New Assembly() {asm})

                objEFConexion = New EntityConnection(objWorkspace, cnConexion)

                Return objEFConexion

            Catch ex As Exception
                Throw
            End Try
        End Function

    End Class

End Namespace