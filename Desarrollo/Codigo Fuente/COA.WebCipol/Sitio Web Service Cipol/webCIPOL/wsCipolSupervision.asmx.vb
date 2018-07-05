Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

''' <summary>
''' </summary>
''' <history>
''' [MartinV]          [lunes, 06 de octubre de 2014]       Modificado  GCP-Cambios 15588
''' </history>
' Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://RGP/CIPOL/wsCipolSupervision")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class wsCipolSupervision
    Inherits System.Web.Services.WebService

    <WebMethod(EnableSession:=True)> _
    Public Function ValidarSupervisor(ByVal Usuario As String, ByVal Clave As String) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Autor: gustavom
        'Fecha de Creación: 07/02/2005
        'Modificaciones:
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'objCipol   : Objeto de seguridad que se utiliza para verificar la clave
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objCipol As New Fachada.CCipolCliente
        Return objCipol.ClaveCorrecta(Usuario, Clave) > 0
    End Function


    <WebMethod(EnableSession:=True)> _
    Public Function ValidarSupervisorConAuditoria(ByVal Usuario As String, ByVal Clave As String, ByVal IDUsuarioSupervisor As Integer, ByVal IDUsuario As Integer, ByVal IDTareaSupervisar As Integer, ByVal Terminal As String) As Boolean
        Dim objCipol As New Fachada.CCipolCliente
        Dim blnRet As Boolean

        Try
            'Verifica si la clave ingresada es correcta.
            blnRet = objCipol.ClaveCorrecta(Usuario, Clave) > 0

            If blnRet Then
                'Registra auditoría de la supervisión.
                objCipol.AuditarSupervision(IDUsuarioSupervisor, IDUsuario) '', IDTareaSupervisar, Terminal)
            End If

            Return blnRet

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class