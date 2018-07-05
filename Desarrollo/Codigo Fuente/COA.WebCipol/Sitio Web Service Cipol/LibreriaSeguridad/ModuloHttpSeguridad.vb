Option Strict On
Option Explicit On

Namespace Validaciones
    Public Class ModuloHttpSeguridad
        Implements System.Web.IHttpModule


        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

        End Sub

        Dim WithEvents mobjContexto As System.Web.HttpApplication

        Public Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            mobjContexto = context
        End Sub

        ''' <summary>
        ''' Valida si el objeto cipol esta en sesion o no.
        ''' </summary>
        ''' <param name="sender">objeto que genera el evento</param>
        ''' <param name="e">Datos adicionales sobre el objeto generador</param>
        ''' <remarks>
        ''' [AngelL]     28/02/2006 - Creado
        ''' </remarks>
        Private Sub contexto_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs) Handles mobjContexto.PreRequestHandlerExecute
            Dim objContexto As System.Web.HttpApplication = CType(sender, System.Web.HttpApplication)

            'With objContexto.Context.Request.Url
            '    'si se llama al modulo de seguridad, salimos
            '    If .Segments(.Segments.Length - 1).Trim().ToUpper().Equals("WSINICIOSESION.ASMX") Then
            '        Return
            '    End If
            'End With

            If objContexto.Context.Request.Headers("SOAPAction") Is Nothing Then Return

            'Si se llama al módulo de seguridad para iniciar sesión
            If objContexto.Context.Request.Headers("SOAPAction") = """http://RGP/CIPOL/InicioSesion/IniciarSesion""" Then
                Return
            End If



            'si no existe en sesion el objeto cipol se avisa con una excepcion
            If objContexto.Context.Session.Item("objCipol") Is Nothing Then
                Throw New System.Web.HttpException("Seguridad no establecida")
                Return
            End If


            'si el objeto cipol esta en sesion, lo cargamos en el current principal
            System.Threading.Thread.CurrentPrincipal = CType(objContexto.Context.Session.Item("objCipol"), Fachada.CCipolCliente)

        End Sub

 
    End Class
End Namespace