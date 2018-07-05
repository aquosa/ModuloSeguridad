Public Class Sesion
   Inherits System.Web.Services.WebService

   Public Function ObtenerObjetoSesion(ByVal claveObjetoSesion As String) As Object
      Return Session(claveObjetoSesion)
   End Function

End Class