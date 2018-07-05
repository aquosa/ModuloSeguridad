Imports Microsoft.VisualBasic

Imports EntidadesEmpresariales

Public Class PadreSistema
    Inherits System.Web.Services.WebService

    Protected FachadaCOA As New Fachada.PadreFachada()

    Public CookieContainer As New System.Net.CookieContainer
    Public Timeout As Integer
    Public Url As String

    Protected Shared _sessioncipol_standalone As Fachada.CCipolCliente


    Public Sub New()

#If Not StandAlone Then

        'Si se llama al módulo de seguridad para iniciar sesión
        If Me.Context.Request.Headers("SOAPAction") = """http://RGP/CIPOL/InicioSesion/IniciarSesion""" Then
            Return
        End If
        'JorgeI - [viernes, 22 de mayo de 2009] Modificaciones GCP-Cambios ID: 7961
        If Me.Context.Request.Headers("SOAPAction") = """http://RGP/CIPOL/InicioSesion/RecuperarNombreDominio""" Then
            Return
        End If
        'si no existe en sesion el objeto cipol se avisa con una excepcion
        '        If sesion.getinstance.Item("objCipol") Is Nothing Then
        'Throw New System.Web.HttpException("Seguridad no establecida")
        'Else
        System.Threading.Thread.CurrentPrincipal = CType(Sesion.getInstance.Item("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        'End If

#Else
        'If TryCast(System.Threading.Thread.CurrentPrincipal, EntidadesEmpresariales.PadreCipolCliente) Is Nothing AndAlso Sesion.getInstance("objCipol") IsNot Nothing Then
        'System.Threading.Thread.CurrentPrincipal = CType(Sesion.getInstance("objCipol"), EntidadesEmpresariales.PadreCipolCliente)
        'End If
#End If

    End Sub

    ''' <summary>
    ''' Publica la excepción en el visor de sucesos y guarda el mensaje de error y
    ''' el StackTrace en una variable de sesión
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [miércoles, 16 de agosto de 2006]        Creado
    ''' [Gustavom]            [miércoles, 13 de febrero de 2008]       GCP-Cambios ID: 6474
    ''' </history>
    Protected Sub PublicarExcepcion(ByVal ex As Exception)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE LAS VARIABLES LOCALES
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strError As String = ""

        strError &= "Mensaje:"
        strError &= ex.Message
        strError &= vbCrLf
        strError &= vbCrLf
        strError &= "StackTrace:"
        strError &= vbCrLf
        strError &= ex.StackTrace

        Sesion.getInstance("UltimoError") = strError
        Me.FachadaCOA.PublicarExcepcion(ex)

    End Sub
End Class
