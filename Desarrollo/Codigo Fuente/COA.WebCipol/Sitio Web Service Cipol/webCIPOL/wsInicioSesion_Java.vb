Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols


Namespace Seguridad.InicioSesion_Java

    <WebService(Namespace:="http://RGP/CIPOL/InicioSesion_Java")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class wsInicioSesion_Java
        Inherits PadreSistema

        ''' <summary>
        ''' Inicia una sesion de cipol, en base al nombre de usuario y la terminal
        ''' </summary>
        ''' <param name="pUsuario">nombre de login de usuario compuesto con el dominio</param>
        ''' <param name="pTerminal">nombre de la terminal desde la que el usuario se loguea</param>
        ''' <param name="pPassword">Clave del usuario</param>
        ''' <param name="pError">Mensaje de error</param>
        ''' <param name="pTerminal_ActualizacionLAN">Indica si la terminal se actualiza a través de un servidor de la LAN o remoto</param>
        ''' <returns>retorna un archivo xml que representa el objeto CIPOL de seguridad</returns>
        ''' <remarks>
        ''' 	[gustavom]	miércoles, 23 de enero de 2008	GCP-Cambios ID: 6426
        ''' 	[gustavom]	viernes, 08 de febrero de 2008	GCP-Cambios ID: 6410
        ''' </remarks>
        <WebMethod(enableSession:=True)> _
        Public Function IniciarSesion(ByVal pUsuario As String, ByVal pTerminal As String, ByVal pPassword As String, ByRef pError As String, ByRef pTerminal_ActualizacionLAN As Boolean) As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '           DECLARACION DE VARIABLES LOCALES
            'objXmlWriter : Objeto que se utilza para representar en xml el objeto CIPOL
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objCipol As New Fachada.CCipolCliente

            Try
                pTerminal_ActualizacionLAN = False
                If objCipol.IniciarSesion(pUsuario, pPassword, pTerminal, pTerminal_ActualizacionLAN) Then
                    'si se autenticó bien, se lo carga en el current principal.
                    If System.Configuration.ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
                        System.Web.HttpContext.Current.Session("objCipol") = objCipol
                    Else
                        System.Threading.Thread.CurrentPrincipal = objCipol
                    End If
                Else
                    pError = objCipol.MensajeError
                    'Retorno el error como retorno de función por no recibir en el cliente
                    'el cliente java el valor (string por referencia)
                    Return "@" + objCipol.MensajeError
                End If

                'Genera el archivo xml
                Dim objMemory As New System.IO.MemoryStream
                Dim objXmlWriter As New System.Xml.XmlTextWriter(objMemory, System.Text.Encoding.GetEncoding("ISO-8859-1"))

                objXmlWriter.WriteStartDocument(True)
                objXmlWriter.WriteStartElement("CIPOL")

                'IDUsuario
                objXmlWriter.WriteElementString("IDUsuario", objCipol.IDUsuario.ToString)

                'Login
                objXmlWriter.WriteElementString("Login", objCipol.Login.ToString)

                'Nombre y Apellido 
                objXmlWriter.WriteElementString("NombreyApellido", objCipol.NombreYApellido.ToString)

                'Tiempo por inactividad
                objXmlWriter.WriteElementString("TiempoPorInactividad", objCipol.TiempoPorInactividad.ToString)

                'Nombre Dominio
                objXmlWriter.WriteElementString("NombreDominio", objCipol.NombreDominio.ToString)

                'Seguridad solo integrada
                objXmlWriter.WriteElementString("SegSoloDominio", IIf(objCipol.Seguridad_SoloDominio, "S", "N").ToString)

                'Nombre Organizacion
                objXmlWriter.WriteElementString("NombreOrganizacion", objCipol.NombreOrganizacion.ToString)

                'Alias de usuario
                objXmlWriter.WriteElementString("AliasUsuario", objCipol.AliasUsuario.ToString)

                'IDSistema actual
                objXmlWriter.WriteElementString("IDSistemaActual", objCipol.IDSistemaActual.ToString)

                'Genera los nodos de sistemas, permisos de menú
                For intK As Integer = 0 To 1
                    Dim dtsTemp As System.Data.DataSet
                    If intK = 0 Then
                        dtsTemp = objCipol.ObtenerDTSMenuSistemas
                    Else
                        dtsTemp = objCipol.ObtenerDTSTareas
                    End If
                    'Genera cada una de las tablas como nodos secundarios
                    For intI As Integer = 0 To dtsTemp.Tables.Count - 1
                        objXmlWriter.WriteStartElement(dtsTemp.Tables(intI).TableName)

                        'Genera cada una de las filas como nodos secundarios
                        For intJ As Integer = 0 To dtsTemp.Tables(intI).Rows.Count - 1
                            objXmlWriter.WriteStartElement("FILA_" & intJ.ToString)

                            'Genera cada una de las columnas como nodos secundarios
                            For intH As Integer = 0 To dtsTemp.Tables(intI).Columns.Count - 1
                                objXmlWriter.WriteElementString(dtsTemp.Tables(intI).Columns(intH).ColumnName, dtsTemp.Tables(intI).Rows(intJ).Item(intH).ToString)
                            Next
                            objXmlWriter.WriteEndElement()
                        Next

                        objXmlWriter.WriteEndElement()
                    Next
                Next

                objXmlWriter.WriteEndElement()
                objXmlWriter.WriteEndDocument()
                objXmlWriter.Flush()
                objXmlWriter.Close()

                Return System.Convert.ToBase64String(objMemory.ToArray())

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            End Try

        End Function

    End Class
End Namespace