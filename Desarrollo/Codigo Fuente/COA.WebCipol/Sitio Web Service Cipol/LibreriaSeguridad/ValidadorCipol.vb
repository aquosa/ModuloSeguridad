Namespace Validaciones
    Public Class ValidadorCipol
        '' ''Inherits Microsoft.Web.Services3.Security.Tokens.UsernameTokenManager

        '' ''Private WithEvents mobjCipol As Fachada.CCipolCliente
        '' ''Protected Overrides Function AuthenticateToken(ByVal token As Microsoft.Web.Services3.Security.Tokens.UsernameToken) As String

        '' ''    With System.Web.HttpContext.Current.Request.Url
        '' ''        'si no se llama al modulo de seguridad
        '' ''        If Not .Segments(.Segments.Length - 1).Trim().ToUpper().Equals("WSINICIOSESION.ASMX") Then
        '' ''            Return token.Password
        '' ''        End If
        '' ''    End With

        '' ''    'si se llama al módulo de seguridad.
        '' ''    If token.Username.Trim.Split("\"c)(1).ToUpper().Equals("MASTER") AndAlso token.Password.Trim.ToUpper.Equals("SUPERVISOR") Then
        '' ''        Return token.Password
        '' ''    Else
        '' ''        Return MyBase.AuthenticateToken(token)
        '' ''    End If

        '' ''    ''Return MyBase.AuthenticateToken(token)
        '' ''    ''Return "lostroglo"
        '' ''    'Dim objBuscador As DirectoryServices.DirectorySearcher
        '' ''    'Dim objEntrada As New DirectoryServices.DirectoryEntry("LDAP://dc=COA,dc=LOCAL")

        '' ''    'objEntrada.Username = token.Username
        '' ''    'objEntrada.Password = token.Password

        '' ''    'objBuscador = New DirectoryServices.DirectorySearcher(objEntrada)
        '' ''    'objBuscador.Filter = "(&(objectClass=user))"
        '' ''    'If objBuscador.FindOne() Is Nothing Then
        '' ''    '    Throw New Microsoft.Web.Services3.Security.SecurityFormatException("Usuario no Valido")
        '' ''    'Else
        '' ''    '    Return token.Password
        '' ''    'End If
        '' ''End Function
    End Class
End Namespace