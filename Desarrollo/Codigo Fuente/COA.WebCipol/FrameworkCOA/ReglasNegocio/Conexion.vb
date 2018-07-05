Option Strict On
Option Explicit On

Public Class Conexion
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Establece el valor del ID de la conexion
    ''' </summary>
    ''' <param name="IDConexion">ID de la conexion que se desea utilizar</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [lunes, 04 de agosto de 2008]       Creado GCP-Cambios ID: 7216
    ''' </history>
    Public Sub EstablecerIDConexion(ByVal IDConexion As String)
        'Establece a nivel de sesion el Id de conexion
        If Session.Item(EntidadesEmpresariales.Aplicacion.STRING_IDCONEXION) Is Nothing Then
            Session.Add(EntidadesEmpresariales.Aplicacion.STRING_IDCONEXION, IDConexion)
        Else
            Session(EntidadesEmpresariales.Aplicacion.STRING_IDCONEXION) = IDConexion
        End If
    End Sub

    ''' <summary>
    ''' Recupera el ID de conexion actual
    ''' </summary>
    ''' <returns>ID de la conexion actual</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [lunes, 04 de agosto de 2008]       Creado GCP-Cambios ID: 7216
    ''' </history>
    Private Function RecuperarIDConexion() As String

        'Verifica que el ID de conexion este establecido
        If Session(EntidadesEmpresariales.Aplicacion.STRING_IDCONEXION) Is Nothing Then
            Return ""
        Else
            Return Session(EntidadesEmpresariales.Aplicacion.STRING_IDCONEXION).ToString
        End If
    End Function

    ''' <summary>
    ''' Obtiene el Id de conexion activa a la Base de Datods
    ''' </summary>
    ''' <returns>Id de la conexion a la DB</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [LucianoP]          [lunes, 04 de agosto de 2008]       Creado GCP-Cambios ID: 7216
    ''' </history>
    Public Shared Function ObtenerIDConexion() As String
        Dim objRNConexion As New Conexion
        Return objRNConexion.RecuperarIDConexion()
    End Function

End Class
