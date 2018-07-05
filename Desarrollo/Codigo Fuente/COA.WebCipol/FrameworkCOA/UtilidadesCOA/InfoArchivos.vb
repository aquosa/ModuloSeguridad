Namespace IO

    ''' <summary>
    ''' Clase que permite visualizar archivos de un directorio y listarlos en un DataSet
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InfoArchivos

        ''' <summary>
        ''' Recupera la información de los archivos que se encuentran a partir de un 
        ''' determinado directorio
        ''' </summary>
        ''' <param name="Directorio">Path del directorio origen</param>
        ''' <param name="Recursivo">Determina si se deben recuperar los archivos de los subdirectorios</param>
        ''' <returns>DataSet con la información de los archivos. Si el directorio no existe retorna Nothing</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Gustavo]	[domingo, 03 de septiembre de 2006]	Created
        ''' </history>
        Public Function Recuperar(ByVal Directorio As String, ByVal Recursivo As Boolean) As System.Data.DataSet
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                DESCRIPCION DE LAS VARIABLES LOCALES
            'dtsRetorno : DataSet de retorno 
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsRetorno As System.Data.DataSet = Me.CrearDataSet()

            'Verifica si el directorio existe 
            If System.IO.Directory.Exists(Directorio) Then
                ObtenerInformacion(Directorio, Recursivo, dtsRetorno)
                Return dtsRetorno
            Else
                Return Nothing
            End If

        End Function

        ''' <summary>
        ''' Obtiene la información de cada uno de los archivos que se encuentran a partir del 
        ''' directorio origen 
        ''' </summary>
        ''' <param name="Directorio">Directorio del cual recuperar información</param>
        ''' <param name="Recursivo">Determina si se deben recuperar los archivos de los subdirectorios</param>
        ''' <param name="dtsInformacion">DataSet con la información de los archivos</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Gustavo]	[domingo, 03 de septiembre de 2006]	Created
        ''' </history>
        Private Sub ObtenerInformacion(ByVal Directorio As String, ByVal Recursivo As Boolean, ByVal dtsInformacion As System.Data.DataSet)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objDir     : Objeto que se utiliza para recuperar información del
            '             directorio que se recibe como parámetro
            'colDir()   : Colección de subdirectorios que el directorio actual
            '             posee
            'objSubDir  : Subdirectorio actual
            'colArch    : Archivos del directorio actual
            'objArch    : Archivo del cual se obtiene el tamaño
            'intI       : Contador del For
            'rowArch    : Datos del archivo 
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objDir As New System.IO.DirectoryInfo(Directorio)
            Dim colDir() As System.IO.DirectoryInfo
            Dim objSubDir As System.IO.DirectoryInfo
            Dim colArch() As System.IO.FileInfo, intI As Integer
            Dim rowArch As System.Data.DataRow

            With objDir

                'Obtiene los datos de los archivos
                colArch = .GetFiles()
                For intI = 0 To colArch.Length - 1
                    rowArch = dtsInformacion.Tables("Archivos").NewRow
                    rowArch.Item("Nombre") = colArch(intI).FullName
                    rowArch.Item("CantidadBytes") = colArch(intI).Length
                    rowArch.Item("Version") = FileVersionInfo.GetVersionInfo(objDir.FullName & "\" & colArch(intI).Name).FileVersion
                    rowArch.Item("NombreArchivo") = colArch(intI).Name
                    rowArch.Item("FechaUltimaModificacion") = System.IO.File.GetLastWriteTime(objDir.FullName & "\" & colArch(intI).Name)
                    dtsInformacion.Tables("Archivos").Rows.Add(rowArch)
                Next

                If Recursivo Then
                    'Verifica si existen subdirectorios
                    colDir = objDir.GetDirectories
                    If colDir.Length > 0 Then
                        For Each objSubDir In colDir
                            ObtenerInformacion(objSubDir.FullName, Recursivo, dtsInformacion)
                        Next
                    End If
                End If
            End With

        End Sub

        ''' <summary>
        ''' Crea el DataSet utilizado para guardar informacion de archivos
        ''' </summary>
        ''' <returns>DataSet con la estructura para guardar informacion de archivos</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Gustavo]	[domingo, 03 de septiembre de 2006]	Created
        ''' </history>
        Public Function CrearDataSet() As System.Data.DataSet
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' dttArchivos : DataTable con los datos de los archivos
            ' objPk       : Clave primaria del DataTable
            ' dtsRetorno  : DataSet con los datos de retorno
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dttArchivos As New System.Data.DataTable("Archivos")
            Dim objPk(0) As DataColumn
            Dim dtsRetorno As New System.Data.DataSet

            dttArchivos.Columns.Add("Nombre", System.Type.GetType("System.String"), "")
            dttArchivos.Columns.Add("CantidadBytes", System.Type.GetType("System.Int64"), "")
            dttArchivos.Columns.Add("Version", System.Type.GetType("System.String"), "")
            dttArchivos.Columns.Add("NombreArchivo", System.Type.GetType("System.String"), "")
            dttArchivos.Columns.Add("FechaUltimaModificacion", System.Type.GetType("System.DateTime"))
            objPk(0) = dttArchivos.Columns("NombreArchivo")
            dttArchivos.PrimaryKey = objPk

            dtsRetorno.Tables.Add(dttArchivos)

            Return dtsRetorno

        End Function

    End Class

End Namespace