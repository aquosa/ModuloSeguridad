''' -----------------------------------------------------------------------------
''' Project	 : EntidadesEmpresariales
''' Interface	 : ICIPOL
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Interfaz de CIPOL utilizada para la comunicación entre las capas
''' Esta intefaz es implementada por la clase CIPOLCliente de la capa Fachada.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[gustavom]	30/08/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Interface ICIPOL
    ReadOnly Property MensajeError() As String
    ReadOnly Property IDUsuario() As Integer
    ReadOnly Property Login() As String
    ReadOnly Property NombreYApellido() As String
    Property IDSistemaActual() As Short
    'Function ObtenerOpcionesMenu(ByVal IDSistema As Short) As dtsSeMenues
    '    Function ObtenerSistemas() As dtsSeMenues
    Sub OtrosDatos(ByVal Clave As String, ByVal Valor As String)
    Function OtrosDatos(ByVal Clave As String) As String
    Sub OtrosDatosEliminar(ByVal Clave As String)
    ReadOnly Property AliasUsuario() As String  '[gustavom]	14/06/2007	GCP-Cambios ID: 5208
    Function IsInRole(ByVal CodigoTarea As String) As Boolean
End Interface
