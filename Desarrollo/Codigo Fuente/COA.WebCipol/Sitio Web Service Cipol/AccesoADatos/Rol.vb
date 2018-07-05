Option Strict On
Option Explicit On

Imports System.Convert
Public Class Rol
   Inherits PadreSistema

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Recupera los roles con su composicion (???)
   ''' </summary>
   ''' <param name="dtsDatos">dataset donde se cargaran los datos</param>
   ''' <param name="nombreTabla">tabla en la que se cargara la información devuelta por la consulta</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	24/10/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub RecuperarRolesComposicion(ByVal dtsDatos As DataSet, ByVal nombreTabla As String)
      Dim strSql As String

      strSql = "SELECT SE_Roles.IdRol, SE_Roles.DescripcionPerfil, SE_Grupo_Tarea.IdGrupo, DescGrupo, SE_Sist_Habilitados.IdSistema, DescSistema, SE_Tareas.IdTarea, DescripcionTarea " ', TareaInhibida "
      strSql = strSql & " FROM SE_Roles, SE_Comp_Roles, SE_Sist_Habilitados, SE_Grupo_Tarea, SE_Tareas WHERE "
      strSql = strSql & " SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema AND "
      strSql = strSql & " SE_Grupo_Tarea.IdGrupo = SE_Tareas.IdGrupo AND SE_Roles.IdRol = SE_Comp_Roles.IdRol AND "
      strSql = strSql & " SE_Tareas.IdTarea = SE_Comp_Roles.IdTarea "
      strSql = strSql & " ORDER BY SE_Roles.DescripcionPerfil, SE_Roles.IdRol, DescGrupo, SE_Grupo_Tarea.IdGrupo, DescSistema, SE_Sist_Habilitados.IdSistema, DescripcionTarea"
      objConexion.Ejecutar(strSql, dtsDatos, nombreTabla)

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Recupera los roles con su composicion (???)
   ''' </summary>
   ''' <param name="dtsDatos">dataset donde se cargaran los datos</param>
   ''' <param name="nombreTabla">tabla en la que se cargara la información devuelta por la consulta</param>
   ''' <param name="FiltroUsuario">filtro opcional por identificador de usuario</param>
   ''' <param name="FiltroRol"> filtro para el rol </param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	24/10/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub RecuperarRolesComposicion(ByVal dtsDatos As DataSet, ByVal nombreTabla As String, ByVal FiltroUsuario As Int32, Optional ByVal FiltroRol As Int32 = -1)
      Dim strSql As String

      strSql = "SELECT " + IIf(FiltroUsuario.Equals(-1), " Distinct ", "").ToString() _
                      + " SE_Roles.IdRol, SE_Roles.DescripcionPerfil, SE_Grupo_Tarea.IdGrupo, DescGrupo, SE_Sist_Habilitados.IdSistema, DescSistema, SE_Tareas.IdTarea, DescripcionTarea " ', TareaInhibida "
      strSql = strSql & ", " + IIf(FiltroUsuario.Equals(-1), " 's' as tareainhibida ", " stu.tareainhibida ").ToString() _
                              + " FROM SE_Roles, SE_Comp_Roles, SE_Sist_Habilitados, SE_Grupo_Tarea, SE_Tareas, se_tareas_usuario stu WHERE "
      strSql = strSql & "SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema AND "
      strSql = strSql & "SE_Grupo_Tarea.IdGrupo = SE_Tareas.IdGrupo AND SE_Roles.IdRol = SE_Comp_Roles.IdRol AND "
      strSql = strSql & "SE_Tareas.IdTarea = SE_Comp_Roles.IdTarea "

      'verificamos si se quiere un filtro por usuario, si es así, filtramos
      If Not FiltroUsuario.Equals(-1) Then
         strSql = strSql & " AND stu.idtarea = se_tareas.idtarea  "
         strSql = strSql & " and stu.idrol = se_comp_roles.idrol "
         strSql = strSql & " AND stu.IdUsuario = " & objConexion.XtoStr(FiltroUsuario)
      End If
      If Not FiltroRol.Equals(-1) Then
         strSql = strSql & " And se_roles.idrol = " + objConexion.XtoStr(FiltroRol)
      End If

      strSql = strSql & " ORDER BY SE_Roles.DescripcionPerfil, SE_Roles.IdRol, DescGrupo, SE_Grupo_Tarea.IdGrupo, DescSistema, SE_Sist_Habilitados.IdSistema, DescripcionTarea"
      objConexion.Ejecutar(strSql, dtsDatos, nombreTabla)

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Elimina un rol de la tabla
   ''' </summary>
   ''' <param name="pidRol">identificador del rol a eliminar.</param>
   ''' <returns>Retorna la cantidad de líneas afectadas por la consulta.</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	02/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function EliminarRol(ByVal pidRol As Int32) As Int32
      Return objConexion.Ejecutar("DELETE FROM SE_Roles WHERE IdRol = " + objConexion.XtoStr(pidRol))
   End Function

   ''' <summary>
   ''' ???????????????????????
   ''' </summary>
   ''' <param name="pidTarea">identificador de la tarea</param>
   ''' <param name="TareaInhibida">indica si la tarea esta inhibida o no</param>
   ''' <param name="pidrol">identificador del rol</param>
   ''' <remarks></remarks>
   ''' <history>
   '''    08/03/2006           [AngelL]       Creado
   ''' </history>
   Public Sub AltaTareaUsuarioPorRol(ByVal pidTarea As Int32, ByVal TareaInhibida As String, ByVal pidrol As Int32)
      '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '           DECRIPCION DE LAS VARIABLES LOCALES
      ' strSql            : string de consulta sql.
      '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim strSql As String

        strSql = "INSERT INTO SE_Tareas_Usuario(IdUsuario, IdRol, IdTarea, TareaInhibida, IDUsuarioUltModif, FechaUltModif) " + _
           "SELECT DISTINCT IdUsuario, IdRol, " + _
              objConexion.XtoStr(pidTarea) + _
              ", " + objConexion.XtoStr(TareaInhibida) + ", " + Me.DatosDelUsuario.IdUsuario.ToString() + ", " + objConexion.XtoStr(objConexion.FechaServidor()) + _
              " FROM SE_Tareas_Usuario WHERE IdRol = " + objConexion.XtoStr(pidrol)

      objConexion.Ejecutar(strSql)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Da de alta una composicion de roles
   ''' </summary>
   ''' <param name="pIdRol">identificador del rol</param>
   ''' <param name="pIdTarea">identificador de la tarea</param>
   ''' <returns>cantidad de filas afectadas</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	01/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function AltaComposicionRol(ByVal pIdRol As Int32, ByVal pIdTarea As Int32) As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                DESCRIPCION DE LAS VARIABLES LOCALES
      ' sblSql            : string de consulta sql.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim sblSql As New System.Text.StringBuilder

      sblSql.Append(" INSERT INTO SE_Comp_Roles(IdRol, IdTarea) Values (")
      sblSql.Append(objConexion.XtoStr(pIdRol))
      sblSql.Append(", " + objConexion.XtoStr(pIdTarea))
      sblSql.Append(" ) ")

      Return objConexion.Ejecutar(sblSql.ToString())

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' elimina una tarea de una composicion de rol.
   ''' </summary>
   ''' <param name="pidRol">identificador del rol</param>
   ''' <param name="pidtarea">identificador de la tarea.</param>
   ''' <returns>cantidad de filas afectadas.</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	01/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function EliminarComposicionRol(ByVal pidRol As Int32, Optional ByVal pidtarea As Int32 = -1) As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                DESCRIPCION DE LAS VARIABLES LOCALES
      ' sblSql            : string de consulta sql.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim sblSql As New System.Text.StringBuilder

      sblSql.Append(" DELETE FROM SE_Comp_Roles WHERE ")
      sblSql.Append("     idrol = " + objConexion.XtoStr(pidRol))

      'si se pide filtrar por tarea, filtramos por tarea.
      If Not pidtarea.Equals(-1) Then
         sblSql.Append("   and idTarea = " + objConexion.XtoStr(pidtarea))
      End If

      Return objConexion.Ejecutar(sblSql.ToString())
   End Function

   Public Function UsuariosPorRol(ByVal idrol As Int32, ByRef dtsdataset As DataSet, ByVal strnombreTabla As String) As Int32
      Dim strsql As String
      strsql = "SELECT distinct IdUsuario FROM SE_Tareas_Usuario WHERE IdRol = " & objConexion.XtoStr(idrol)
      Return objConexion.Ejecutar(strsql, dtsdataset, strnombreTabla)
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Actualiza una fila en la tabla de roles
   ''' </summary>
   ''' <param name="pIdRol">identificador de roles</param>
   ''' <param name="pDescripcionPErfil">descripcion del perfil</param>
   ''' <returns>Cantidad de filas afectadas.</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	01/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function ModificacionDeRol(ByVal pIdRol As Int32, ByVal pDescripcionPErfil As String) As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                DESCRIPCION DE LAS VARIABLES LOCALES
      ' sblSql            : string de consulta sql.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim sblSql As New System.Text.StringBuilder

      sblSql.Append("UPDATE SE_Roles SET ")
      sblSql.Append(" DescripcionPerfil = " & objConexion.XtoStr(pDescripcionPErfil))
      sblSql.Append(" WHERE IdRol = " & objConexion.XtoStr(pIdRol))

      Return objConexion.Ejecutar(sblSql.ToString())
   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Da de alta un rol
   ''' </summary>
   ''' <param name="pIdRol">identificador del rol a dar de alta</param>
   ''' <param name="pDescripcionPErfil">descripcion del rol</param>
   ''' <returns>identificador del rol dado de alta.</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	01/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function AltaDeRol(ByVal pIdRol As Int32, ByVal pDescripcionPErfil As String) As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                DESCRIPCION DE LAS VARIABLES LOCALES
      ' sblSql            : string de consulta sql.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim sblSql As New System.Text.StringBuilder

      sblSql.Append("INSERT INTO SE_Roles(IdRol, DescripcionPerfil)")
      sblSql.Append(" VALUES ( ")
      sblSql.Append(objConexion.XtoStr(pIdRol))
      sblSql.Append("," + objConexion.XtoStr(pDescripcionPErfil))
      sblSql.Append(" ) ")

      objConexion.Ejecutar(sblSql.ToString)
      Return pIdRol

   End Function

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Retorna le último nro usado identificador de la tabla Se_Roles
   ''' </summary>
   ''' <returns>último nro usado identificador de la tabla Se_Roles</returns>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	01/11/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Function UltimoIdRoles() As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                DESCRIPCION DE LAS VARIABLES LOCALES
      ' objRetorno            : valor que será retornado.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim objRetorno As Object
      objRetorno = objConexion.EjecutarEscalar("SELECT Max(IdRol) As Cantidad FROM SE_Roles")
      Return ToInt32(IIf(objRetorno Is DBNull, 0, objRetorno))
   End Function
   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Retorna la composición de un rol.
   ''' </summary>
   ''' <param name="idrol">identificadord el rol</param>
   ''' <param name="dtsDatos">dataset para los datos</param>
   ''' <param name="nombretabla">tabla del dataset donde se cargaran los datos.</param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[angell]	24/10/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub RecuperarComposicionDeUnRol(ByVal idrol As Int32, ByVal dtsDatos As DataSet, ByVal nombretabla As String, ByVal SoloIdTarea As Boolean)
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                DESCRIPCION DE LAS VARIABLES LOCALES
      ' strSql            : String de consulta sql
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim strsql As String

      strsql = "SELECT SE_Tareas.IdTarea, SE_Grupo_Tarea.IdGrupo, SE_Sist_Habilitados.IdSistema "
      If Not SoloIdTarea Then _
          strsql = strsql & ", DescGrupo, DescSistema ,  DescripcionTarea "

      strsql = strsql & "FROM SE_Sist_Habilitados, SE_Grupo_Tarea, SE_Tareas, SE_Comp_Roles WHERE "
      strsql = strsql & "SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema AND "
      strsql = strsql & "SE_Grupo_Tarea.IdGrupo = SE_Tareas.IdGrupo AND SE_Tareas.IdTarea = SE_Comp_Roles.IdTarea AND "
      strsql = strsql & "SE_Comp_Roles.IdRol = " & objConexion.XtoStr(idrol) & " ORDER BY SE_Grupo_Tarea.IdGrupo, SE_Sist_Habilitados.IdSistema"

      objConexion.Ejecutar(strsql, dtsDatos, nombretabla)
   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Recupera los roles existentes
   ''' </summary>
   ''' <param name="dtsDatos"></param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[gustavom]	29/09/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub RecuperarRoles(ByVal dtsDatos As DataSet, ByVal strNombreTabla As String)
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                    DESCRIPCION DE VARIABLES LOCALES
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        objConexion.Ejecutar("SELECT *  FROM vRecuperarRoles ORDER BY descripcionperfil", CType(dtsDatos, System.Data.DataSet), strNombreTabla)

   End Sub

   ''' -----------------------------------------------------------------------------
   ''' <summary>
   ''' Recupera los datos necesarios para armar el árbol de grupos existentes
   ''' </summary>
   ''' <param name="dtsDatos"></param>
   ''' <remarks>
   ''' </remarks>
   ''' <history>
   ''' 	[gustavom]	29/09/2005	Created
   ''' </history>
   ''' -----------------------------------------------------------------------------
   Public Sub RecuperarDatosParaArbolGrupo(ByVal dtsDatos As DataSet, ByVal strNombreTabla As String)
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '                    DESCRIPCION DE VARIABLES LOCALES
      'sblSql : String Builder que se utiliza para armar la sentencia SQL
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim sblSql As New System.Text.StringBuilder
      With sblSql
         .Append("SELECT SE_Grupo_Tarea.IdGrupo, DescGrupo, SE_Sist_Habilitados.IdSistema, DescSistema, IdTarea, DescripcionTarea ")
         .Append("FROM SE_Sist_Habilitados, SE_Grupo_Tarea, SE_Tareas WHERE ")
         .Append("SE_Sist_Habilitados.IdSistema = SE_Tareas.IdSistema AND ")
         .Append("SE_Grupo_Tarea.IdGrupo = SE_Tareas.IdGrupo ORDER BY DescGrupo , SE_Grupo_Tarea.IdGrupo, DescSistema, SE_Sist_Habilitados.IDSistema, DescripcionTarea, IdTarea")
      End With

      objConexion.Ejecutar(sblSql.ToString, CType(dtsDatos, System.Data.DataSet), strNombreTabla)

   End Sub

   Public Function VerificarRolAsignadoAUsuarios(ByVal pidRol As Int32) As Integer

      Return CType(objConexion.EjecutarEscalar("SELECT COUNT(*) AS Asignados FROM SE_TAREAS_USUARIO WHERE IDROL = " + objConexion.XtoStr(pidRol)), Integer)

   End Function

End Class
