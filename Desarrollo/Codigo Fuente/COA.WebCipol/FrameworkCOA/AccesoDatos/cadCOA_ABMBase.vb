Public Class cadCOA_ABMBase
    Inherits PadreAccesoDatos
    ''' <summary>
    ''' Recupera los datos según la sentencia pasada como parámetro
    ''' </summary>
    ''' <param name="SentenciaSql">Sentencia SQL a Ejecutar</param>
    ''' <param name="dtsDataset">datset donde se cargarán los datos</param>
    ''' <param name="NombreTabla">Nombre de la Tabla del Dataset en donde se recuperan los datos</param>
    ''' <returns>Cantidad de filas recuperadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          22/08/2007        Creado
    ''' </history>
    Public Function Recuperar(ByVal SentenciaSql As String, _
                              ByRef dtsDataset As DataSet, _
                              ByVal NombreTabla As String) As Int32
        Try
            Return objConexion.Ejecutar(SentenciaSql.Trim, _
                                        dtsDataset, _
                                        NombreTabla)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Inserta datos de la tabla pasada por parámetro
    ''' </summary>
    ''' <param name="dtsDataset">Dataset con los datos a insertar</param>
    ''' <param name="NombreTabla">Nombre de la Tabla</param>
    ''' <returns>Cantidad de filas recuperadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          22/08/2007        Creado
    ''' </history>
    Public Function Insertar(ByRef dtsDataset As DataSet, _
                             ByVal NombreTabla As String) As Int32
        Try
            Return objConexion.Insertar(dtsDataset, _
                                        NombreTabla)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Actualiza datos de la tabla pasada por parametro
    ''' </summary>
    ''' <param name="dtsDataset">Dataset con los datos a actualizar</param>
    ''' <param name="NombreTabla">Nombre de la Tabla</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          22/08/2007        Creado
    ''' </history>
    Public Function Actualizar(ByRef dtsDataset As DataSet, _
                               ByVal NombreTabla As String) As Int32
        Try
            Return objConexion.Actualizar(dtsDataset, _
                                          NombreTabla)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Elimina datos de la tabla pasada por parametro
    ''' </summary>
    ''' <param name="dtsDataset">Dataset con los datos a eliminar</param>
    ''' <param name="NombreTabla">Nombre de la Tabla</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          23/08/2007        Creado
    ''' </history>
    Public Function Eliminar(ByRef dtsDataset As DataSet, _
                             ByVal NombreTabla As String) As Int32
        Try
            Return objConexion.Eliminar(dtsDataset, _
                                        NombreTabla)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Recupera el próximo valor para el campo clave de una tabla
    ''' </summary>
    ''' <param name="CampoClave">Campo Clave</param>
    ''' z<param name="NombreTabla">Nombre de la Tabla</param>
    ''' <returns>Valor Clave</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Juano]          23/08/2007        Creado
    ''' </history>
    Public Function RecuperarProximoValorClave(ByVal CampoClave As String, _
                                               ByVal NombreTabla As String) As Integer
        Dim strSentenciaSQL As New System.Text.StringBuilder
        Try
            With strSentenciaSQL
                .Append(" SELECT MAX(" & CampoClave & ")")
                strSentenciaSQL.Append(" FROM " & NombreTabla)
            End With
            Return CInt(objConexion.EjecutarEscalar(strSentenciaSQL.ToString)) + 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class