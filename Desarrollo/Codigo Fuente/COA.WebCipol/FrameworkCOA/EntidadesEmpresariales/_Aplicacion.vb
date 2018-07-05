Option Strict On
Option Explicit On 

Imports System.Web
Imports EntidadesEmpresariales
Imports System.Configuration

''' -----------------------------------------------------------------------------
''' Project	 : EntidadesEmpresariales
''' Class	 : _Aplicacion
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase de la cual heredan las clases bases de cada una de las capas. Contiene
''' información necesaria para la comunicación de las mismas, tales como datos
''' del usuario.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	30/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> _
Public Class Aplicacion

    Public Shared STRING_IDCONEXION As String = "IDConexion"
    Public Structure udtParametrosUsuario
        Public IdUsuario As Integer
        Public Usuario As String
        Public NombreYApellido As String
        Public IDSistema As Short
        Public AliasUsuario As String
    End Structure

    Public ReadOnly Property DatosDelUsuario() As udtParametrosUsuario
        Get
            Dim muUsuario As udtParametrosUsuario
            With muUsuario
                Dim objCipol As System.Security.Principal.IPrincipal = Nothing
                Dim objSesion As Sesion = Sesion.getInstance
                'Define de donde tomar el objeto cipol.
                If ConfigurationManager.AppSettings("TomarCipolCurrentPrincipal").ToString().Trim() = "N" Then
                    objCipol = CType(objSesion("objCipol"), System.Security.Principal.IPrincipal)
                Else
                    objCipol = System.Threading.Thread.CurrentPrincipal
                End If

                'Si todavia no ha sido asignado la clase CipolCliente
                If objCipol.Identity.Name <> "" AndAlso objCipol.Identity.Name.IndexOf("\") = -1 Then

                    .Usuario = CType(objCipol, EntidadesEmpresariales.ICIPOL).Login
                    .IdUsuario = CType(objCipol, EntidadesEmpresariales.ICIPOL).IDUsuario
                    .NombreYApellido = CType(objCipol, EntidadesEmpresariales.ICIPOL).NombreYApellido
                    .IDSistema = CType(objCipol, EntidadesEmpresariales.ICIPOL).IDSistemaActual
                    .AliasUsuario = CType(objCipol, EntidadesEmpresariales.ICIPOL).AliasUsuario

                Else
                    .IdUsuario = 0
                    .NombreYApellido = String.Empty
                    .IDSistema = 0
                    .Usuario = String.Empty
                    .AliasUsuario = String.Empty
                End If
            End With

            Return muUsuario
        End Get

    End Property

End Class


