Imports AccesoDatos
Public Class rnCOA_ABMBase
    Inherits PadreReglasNegocio
    Public Enum TipoProceso As Integer
        Insertar = 1
        Eliminar = 2
        Actualizar = 3
    End Enum
    ''' <summary>
    ''' Recupera todos los datos de la tabla pasada por parametro
    ''' </summary>
    ''' <param name="dtsDataset">datset donde se cargarán los datos</param>
    ''' <param name="NombreTabla">Nombre de la Tabla del Dataset en donde se recuperan los datos</param>
    ''' <param name="Filtro">Filtro de la Sentencia Sql a ejecutar</param>
    ''' <returns>Cantidad de filas recuperadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          22/08/2007        Creado
    ''' </history>
    Public Function Recuperar(ByRef dtsDataset As DataSet, _
                              ByVal NombreTabla As String, _
                              Optional ByVal Filtro As String = "") As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'cadCOA_ABMBase  : Componente utilizado para acceder a Acceso a Datos.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim cadCOA_ABMBase As New AccesoDatos.cadCOA_ABMBase
        Dim sblSentenciaSQL As New System.Text.StringBuilder
        Dim strConexion As String
        Try
            strConexion = Conexion.ObtenerIDConexion
            If strConexion.Trim <> "" Then
                cadCOA_ABMBase.CrearConexion(Conexion.ObtenerIDConexion)
            Else
                cadCOA_ABMBase.CrearConexion()
            End If
            'Arma la Sentencia a Ejecutar
            With sblSentenciaSQL
                .Append("SELECT * FROM ") : .Append(NombreTabla)
                If Filtro <> "" Then .Append(" WHERE ") : .Append(Filtro)
            End With
            'Recupera los Datos de la Tabla
            Return cadCOA_ABMBase.Recuperar(sblSentenciaSQL.ToString, dtsDataset, NombreTabla)
        Catch ex As Exception
            Throw ex
        Finally
            cadCOA_ABMBase.Desconectar()
        End Try
    End Function
    ''' <summary>
    ''' Recupera el próximo valor para el campo clave de una tabla
    ''' </summary>
    ''' <param name="Dataset">Dataset con la Estructura del cual saca el Campo Clave</param>
    ''' <param name="NombreTabla">Nombre de la Tabla</param>
    ''' <returns>Dataset con los datos</returns>
    ''' [Juano]          23/08/2007        Creado
    ''' <remarks></remarks>
    Public Function RecuperarProximoValorClave(ByRef Dataset As DataSet, _
                                               ByVal NombreTabla As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'rnCOA_ABMBase  : Componente utilizado para acceder a reglas de negocio.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim cadCOA_ABMBase As New AccesoDatos.cadCOA_ABMBase
        Dim strSentenciaSQL As New System.Text.StringBuilder
        Dim intRetorno As Integer
        Dim strConexion As String
        Try
            intRetorno = -1
            With Dataset.Tables(NombreTabla)
                If .PrimaryKey.Length = 1 Then
                    strConexion = Conexion.ObtenerIDConexion
                    If strConexion.Trim <> "" Then
                        cadCOA_ABMBase.CrearConexion(strConexion)
                    Else
                        cadCOA_ABMBase.CrearConexion()
                    End If
                    intRetorno = cadCOA_ABMBase.RecuperarProximoValorClave(.PrimaryKey(0).ColumnName.Trim, _
                                                                            NombreTabla)
                    cadCOA_ABMBase.Desconectar()
                End If
            End With
            'Retorna el próximo valor para el campo clave de una tabla
            Return intRetorno
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Inserta, Actualiza o Elimina datos de la tabla pasada por parametro
    ''' </summary>
    ''' <param name="Dataset">Dataset utilizado para la Grabación</param>
    ''' <param name="NombreTabla">Nombre de la Tabla de Grabación</param>
    ''' <param name="TipoProceso">Tipo de Proceso a Grabar (Inserción, Eliminación o Actualización)</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          22/08/2007        Creado
    ''' [Juano]          16/07/2009        Soporta campos AutoIncrement
    ''' </history>
    Public Function Grabar(ByRef Dataset As DataSet, _
                           ByVal NombreTabla As String, _
                           ByVal TipoProceso As TipoProceso) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'cadCOA_ABMBase   : Componente utilizado para acceder a Acceso a Datos.
        'intFilasAfectadas: Cantidad de Filas afectadas (Retorno de la función)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim cadCOA_ABMBase As New AccesoDatos.cadCOA_ABMBase
        Dim intFilasAfectadas As Int32 = 0
        Dim strConexion As String
        Try
            strConexion = Conexion.ObtenerIDConexion
            If strConexion.Trim <> "" Then
                'Inicia la Transacción
                cadCOA_ABMBase.IniciarTransaccion(strConexion)
            Else
                'Inicia la Transacción
                cadCOA_ABMBase.IniciarTransaccion()
            End If
            'Según el tipo de proceso Inserta, Elimina o Actualiza
            Select Case TipoProceso
                Case rnCOA_ABMBase.TipoProceso.Insertar
                    With Dataset.Tables(NombreTabla)
                        'Si la clave principal de la tabla es -1000
                        'significa que debe buscar el próximo 
                        'valor disponible para dicha clave.
                        If .PrimaryKey.Length = 1 Then
                            Select Case .Columns(.PrimaryKey()(0).ColumnName.Trim).DataType.Name
                                Case "DateTime", "TimeSpan", "Char", "String"
                                    'No setea ningún valor si la columna no es numérica
                                Case Else
                                    If CInt(.Select(Nothing, Nothing, DataViewRowState.Added)(0).Item(.PrimaryKey()(0).ColumnName.Trim)) = -1000 Then
                                        If Not .Columns(.PrimaryKey()(0).ColumnName.Trim).AutoIncrement Then
                                            .Select(Nothing, Nothing, DataViewRowState.Added)(0).Item(.PrimaryKey()(0).ColumnName.Trim) = RecuperarProximoValorClave(Dataset, NombreTabla)
                                        End If
                                    End If
                            End Select
                            If Not .Columns(.PrimaryKey()(0).ColumnName.Trim).AutoIncrement Then
                                'Controla Antes de Insertar que no exista el registro 
                                Dim strCaracterAdicional As String = RecuperarCaracterAdicionalDataType(.PrimaryKey()(0).DataType.Name)
                                If Me.Recuperar(Dataset, NombreTabla, _
                                                .PrimaryKey()(0).ColumnName & " = " & strCaracterAdicional & .Select(Nothing, Nothing, DataViewRowState.Added)(0).Item(.PrimaryKey()(0).ColumnName.Trim).ToString & strCaracterAdicional) > 0 Then
                                    Exit Select
                                End If
                            End If
                        End If
                    End With
                    'Genera la Inserción
                    intFilasAfectadas = cadCOA_ABMBase.Insertar(Dataset, NombreTabla)
                Case rnCOA_ABMBase.TipoProceso.Eliminar
                    'Genera la Eliminación
                    intFilasAfectadas = cadCOA_ABMBase.Eliminar(Dataset, NombreTabla)
                Case rnCOA_ABMBase.TipoProceso.Actualizar
                    'Genera la Actualización
                    intFilasAfectadas = cadCOA_ABMBase.Actualizar(Dataset, NombreTabla)
            End Select
            'Finaliza la Transacción 
            cadCOA_ABMBase.FinalizarTransaccion(True)
            Return intFilasAfectadas
        Catch ex As Exception
            cadCOA_ABMBase.FinalizarTransaccion(False)
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Recupera el caracter adicional a sumar en un filtro según
    ''' el tipo de dato 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarCaracterAdicionalDataType(ByVal TipoDato As String) As String
        Dim strCaracterAdicional As String = ""
        Try
            Select Case TipoDato
                Case "DateTime", "TimeSpan"
                    strCaracterAdicional = "#"
                Case "Char", "String"
                    strCaracterAdicional = "'"
            End Select
            Return strCaracterAdicional
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class