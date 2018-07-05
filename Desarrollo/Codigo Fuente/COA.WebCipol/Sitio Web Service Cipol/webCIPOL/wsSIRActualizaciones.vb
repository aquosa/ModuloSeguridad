Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Namespace Seguridad.Downloader
    <WebService(Namespace:="http://RGP/SIRActualizaciones/")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class wsSIRActualizaciones
        Inherits System.Web.Services.WebService

        ''' <summary>
        ''' Me indica el significado de 100 Kilobytes en Bytes.
        ''' </summary>
        ''' <remarks>Esta duplicada del lado del cliente.</remarks>
        Private Const CANTIDAD_DE_BYTES_100K As Integer = 102400

#Region "Primer Proceso de Actualización (Se mantiene por compatibilidad hasta que se reemplace en los puestos)"
        Private mstrCarpetaVersion As String

        <WebMethod(Description:="Determina si existen actualizaciones para descargar")> _
        Public Function ExisteActualizacion(ByVal UltimaFechaActualizacion As DateTime) As Boolean
            Dim dtmLiberacionVersion As DateTime = Me.FechaLiberacionVersion

            If dtmLiberacionVersion = #1/1/1900# Then Return False

            If dtmLiberacionVersion <= UltimaFechaActualizacion Then Return False

            'Verifica si existe con la fecha-hora de liberacion de versión una carpeta creada
            mstrCarpetaVersion = String.Format("{0:####}{1:00}{2:00}", dtmLiberacionVersion.Year, dtmLiberacionVersion.Month, dtmLiberacionVersion.Day)
            mstrCarpetaVersion &= String.Format(" {0:00}{1:00}", dtmLiberacionVersion.Hour, dtmLiberacionVersion.Minute)
            mstrCarpetaVersion = Server.MapPath("~/Actualizaciones/" & mstrCarpetaVersion)

            If Not System.IO.Directory.Exists(mstrCarpetaVersion) Then Return False

            'Si la fecha-hora que posee la capa de presentación es menor a la fecha determinada en el web.config
            If UltimaFechaActualizacion < dtmLiberacionVersion Then
                Return True
            Else
                Return False
            End If

        End Function

        <WebMethod(Description:="Recupera los nombres de los archivos de la versión que se libera")> _
        Public Function RecuperarListaArchivos() As String()
            Dim intI As Integer = 0

            If Not Me.ExisteActualizacion(#1/1/1900#) Then Return Nothing

            Dim strArchivos() As String = System.IO.Directory.GetFiles(Me.mstrCarpetaVersion)

            'Elimina el path de la carpeta
            For intI = 0 To strArchivos.GetUpperBound(0)
                strArchivos(intI) = strArchivos(intI).Substring(strArchivos(intI).LastIndexOf("\"c) + 1)
            Next

            Return strArchivos

        End Function

        ''' <summary>
        ''' Recupera desde el web.config la fecha de liberación de versión
        ''' </summary>
        ''' <returns>Fecha de liberación de versión</returns>
        ''' <remarks></remarks>
        Private Function FechaLiberacionVersion() As DateTime
            Dim dtmLiberacionVersion As DateTime = Nothing
            Dim strLiberacionVersion As String = Nothing

            strLiberacionVersion = System.Configuration.ConfigurationManager.AppSettings("LiberacionDeVersion").Trim
            If strLiberacionVersion = "" Then Return #1/1/1900#
            Try
                dtmLiberacionVersion = New DateTime(Integer.Parse(strLiberacionVersion.Substring(0, 4)), _
                                                    Integer.Parse(strLiberacionVersion.Substring(4, 2)), _
                                                    Integer.Parse(strLiberacionVersion.Substring(6, 2)), _
                                                    Integer.Parse(strLiberacionVersion.Substring(9, 2)), _
                                                    Integer.Parse(strLiberacionVersion.Substring(11, 2)), _
                                                    0)

                Return dtmLiberacionVersion
            Catch ex As Exception
                Return #1/1/1900#
            End Try
        End Function
#End Region

#Region "Segundo Proceso de Actuazación"
        <WebMethod(Description:="Recupera la URL del webservice de actualización localizado en servidor LAN")> _
        Public Function RecuperarURLActualizador_ServidorLAN() As String
            Return System.Configuration.ConfigurationManager.AppSettings("URLActualizador_ServidorLAN")
        End Function

        <WebMethod(Description:="Recupera los nombres de los archivos de la versión que se libera")> _
        Public Function RecuperarArchivosADescagar(ByVal dtsCliente As System.Data.DataSet) As System.Data.DataSet
            Dim strDirectorio As String = Server.MapPath("~/ActualizacionDeProductos/")
            Dim dtsServidor As System.Data.DataSet
            Dim rowArchCli As System.Data.DataRow
            Dim intI As Integer, objArch As New COA.IO.InfoArchivos
            Dim dtsRetorno As System.Data.DataSet = objArch.CrearDataSet()
            Dim rowArchRet As System.Data.DataRow

            dtsServidor = objArch.Recuperar(strDirectorio, True)
            'Determina los archivos que deben distribuirse al cliente
            For intI = 0 To dtsServidor.Tables("Archivos").Rows.Count - 1

                'Verifica si el archivo existe en el cliente
                rowArchCli = dtsCliente.Tables("Archivos").Rows.Find(dtsServidor.Tables("Archivos").Rows(intI).Item("NombreArchivo"))

                'Si el archivo existe 
                If rowArchCli IsNot Nothing Then

                    Dim strExtensionArchivo As String = System.IO.Path.GetExtension(rowArchCli.Item("NombreArchivo"))
                    Dim blnDescargarArchivo As Boolean
                    Dim dtmUltimaModificacionArchivoCliente As DateTime
                    Dim dtmUltimaModificacionArchivoServidor As DateTime

                    ' Obtengo la fecha de ultima modificacion de ambos archivos.
                    dtmUltimaModificacionArchivoCliente = Convert.ToDateTime(rowArchCli.Item("FechaUltimaModificacion"))
                    dtmUltimaModificacionArchivoServidor = Convert.ToDateTime(dtsServidor.Tables("Archivos").Rows(intI).Item("FechaUltimaModificacion"))

                    ' Si la fecha de ultima modificacion del archivo del cliente es diferente
                    ' a la fecha de ultima modificacion del servidor entiendo que el archivo del 
                    ' del cliente esta desactualizado o sufrio una modificacion. Confio siempre en el
                    ' archivo del lado del servidor.
                    blnDescargarArchivo = (dtmUltimaModificacionArchivoCliente <> dtmUltimaModificacionArchivoServidor)

                    If strExtensionArchivo = ".exe" OrElse strExtensionArchivo = ".dll" Then
                        ' Comparo si las versiones del archivo del cliente con el del servidor son distintas
                        If Not blnDescargarArchivo Then blnDescargarArchivo = (rowArchCli.Item("Version").ToString.Trim <> dtsServidor.Tables("Archivos").Rows(intI).Item("Version").ToString.Trim)
                    End If

                    ' Discrimino los archivos por extensión
                    If blnDescargarArchivo Then
                        rowArchRet = dtsRetorno.Tables("Archivos").NewRow
                        rowArchRet.Item("Nombre") = dtsServidor.Tables("Archivos").Rows(intI).Item("Nombre")
                        rowArchRet.Item("CantidadBytes") = dtsServidor.Tables("Archivos").Rows(intI).Item("CantidadBytes")
                        rowArchRet.Item("Version") = dtsServidor.Tables("Archivos").Rows(intI).Item("Version")
                        rowArchRet.Item("NombreArchivo") = dtsServidor.Tables("Archivos").Rows(intI).Item("NombreArchivo")
                        rowArchRet.Item("FechaUltimaModificacion") = dtsServidor.Tables("Archivos").Rows(intI).Item("FechaUltimaModificacion")
                        dtsRetorno.Tables("Archivos").Rows.Add(rowArchRet)
                    End If
                Else
                    'Si el archivo no existe se envía al cliente
                    rowArchRet = dtsRetorno.Tables("Archivos").NewRow
                    rowArchRet.Item("Nombre") = dtsServidor.Tables("Archivos").Rows(intI).Item("Nombre")
                    rowArchRet.Item("CantidadBytes") = dtsServidor.Tables("Archivos").Rows(intI).Item("CantidadBytes")
                    rowArchRet.Item("Version") = dtsServidor.Tables("Archivos").Rows(intI).Item("Version")
                    rowArchRet.Item("NombreArchivo") = dtsServidor.Tables("Archivos").Rows(intI).Item("NombreArchivo")
                    rowArchRet.Item("FechaUltimaModificacion") = dtsServidor.Tables("Archivos").Rows(intI).Item("FechaUltimaModificacion")
                    dtsRetorno.Tables("Archivos").Rows.Add(rowArchRet)
                End If
            Next

            Return dtsRetorno

        End Function

        <WebMethod(Description:="Serializa el contenido de un archivo hacia el cliente")> _
        Public Function DescargarArchivo(ByVal Nombre As String) As Byte()
            Return LeerArchivo(Nombre)
        End Function

        <WebMethod(Description:="Serializa el contenido de un archivo hacia el cliente")> _
        Public Function DescargarArchivoParcial(ByVal Nombre As String, ByVal intOffset As Integer, ByRef intLeido As Integer) As Byte()
            Return LeerArchivo(Nombre, intOffset, intLeido)
        End Function

        ''' <summary>
        ''' Lee el archivo del disco y lo envía al cliente
        ''' </summary>
        ''' <param name="Nombre">Nombre del archivo que se serializa hacia al cliente</param>
        ''' <param name="intOffset">Posición de inicio de lectura del archivo</param>
        ''' <param name="intLeido">Cantidad de bytes leídos</param>
        ''' <returns>Contenido del archivo</returns>
        ''' <remarks></remarks>
        Public Function LeerArchivo(ByVal Nombre As String, Optional ByVal intOffset As Integer = -1, Optional ByRef intLeido As Integer = 0) As Byte()

            Dim objContenidoArchivo As System.IO.Stream = Nothing

            Try

                'Si el nombre del archivo no es FullName, sin Path, se trata del proceso 
                'de actualización anterior
                If Nombre.IndexOf(System.IO.Path.DirectorySeparatorChar) = -1 Then
                    ' Me fijo si el directorio existe
                    If Not Me.ExisteActualizacion(#1/1/1900#) Then Return Nothing

                    Nombre = Me.mstrCarpetaVersion & "\" & Nombre
                End If

                'Verifica si el archivo existe
                If System.IO.File.Exists(Nombre) Then
                    objContenidoArchivo = New System.IO.FileStream(Nombre, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                Else
                    Return Nothing
                End If

                Dim bytBuffer(CInt(IIf(intOffset < 0, objContenidoArchivo.Length, CANTIDAD_DE_BYTES_100K)) - 1) As Byte

                ' Si el tamaño del archivo en menor a 100K el cliente me pide con offset negativo
                ' recupero el archivo de una sola ves, en caso contrario me posiciono en offset y
                ' recupero CANTIDAD_DE_BYTES_100K bytes.
                If intOffset < 0 Then
                    objContenidoArchivo.Read(bytBuffer, 0, objContenidoArchivo.Length)
                Else
                    objContenidoArchivo.Seek(intOffset, IO.SeekOrigin.Begin)
                    intLeido = objContenidoArchivo.Read(bytBuffer, 0, CANTIDAD_DE_BYTES_100K)
                End If

                objContenidoArchivo.Close()

                Return bytBuffer

            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try

        End Function

        <WebMethod(Description:="Retorna la fecha de liberacion de version")> _
        Public Function RecuperarFechaLiberacionVersion() As DateTime
            Return Now
        End Function

        <WebMethod(Description:="Retorna el nombre del servidor")> _
        Public Function RecuperarNombreServidor() As String
            Return System.Environment.MachineName
        End Function

#End Region

    End Class
End Namespace