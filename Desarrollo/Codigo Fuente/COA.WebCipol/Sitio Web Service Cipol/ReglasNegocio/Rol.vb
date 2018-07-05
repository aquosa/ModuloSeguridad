Option Strict On
Option Explicit On

Imports EE = Fachada.Seguridad
Imports System.Convert
Public Class Rol
    Inherits PadreSistema

    ''' <summary>
    ''' Genera las acciones necesarias para realizar un alta de  un rol.
    ''' </summary>
    ''' <param name="pdtsDatos">dataset con los parametros para dar el alta</param>
    ''' <returns>identificador del nuevo rol dado de alta</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''    07/03/2006        [AngelL]    Creado
    ''' </history>
    Public Function AltaDeRol(ByRef pdtsDatos As EE.dtsRoles) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE VARIABLES LOCALES
        'objADRoles         : Objeto para el manejo de datos referente a roles.
        'intId              : identificador del nuevo rol
        'intI               : contador para el for
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADRoles As New AccesoDatos.Rol
        Dim objAdSistema As New AccesoDatos.Sistema
        Dim intId, inti As Int32
        Try
            objADRoles.IniciarTransaccion()

            intId = objADRoles.UltimoIdRoles() + 1

            objADRoles.AltaDeRol(intId, pdtsDatos.SE_ROLES(0).DESCRIPCIONPERFIL)

            'se da de alta las tareas asignadas al rol.
            For inti = 0 To pdtsDatos.TareasAsignadas.Rows.Count - 1
                objADRoles.AltaComposicionRol(intId, pdtsDatos.TareasAsignadas(inti).idTarea)
            Next

            pdtsDatos.SE_ROLES(0).IDROL = intId

            objAdSistema.ConexionActiva = objADRoles.ConexionActiva
            For inti = 0 To pdtsDatos.Tables("ParametrosDeABM").Rows.Count - 1
                If Not pdtsDatos.Tables("ParametrosDeABM").Rows(inti)("MensajesAuditoria").ToString().Trim().Equals("") Then _
                    AuditarCambios(objAdSistema, pdtsDatos.Tables("ParametrosDeABM").Rows(inti)("MensajesAuditoria").ToString())
            Next

            objADRoles.FinalizarTransaccion(True)
        Catch ex As Exception
            objADRoles.FinalizarTransaccion(False)
            intId = 0
        End Try


        Return intId
    End Function
    ''' <summary>
    ''' Modifica un rol dado
    ''' </summary>
    ''' <param name="pdtsDatos">dataset con los datos para la modificaciond el rol</param>
    ''' <returns>Identificador del rol modificado</returns>
    ''' <remarks></remarks>
    ''' <hystory>
    '''    07/03/2006       [AngelL]     Creado
    ''' [Gustavom]          [viernes, 10 de agosto de 2007]   GCP-Cambios ID: 5489
    ''' </hystory>
    Public Function ModificarRol(ByVal pdtsDatos As EE.dtsRoles, ByVal strElementoseliminados As String) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE VARIABLES LOCALES
        'objADRoles         : Objeto para el manejo de datos referente a roles.
        'intId              : identificador del nuevo rol
        'strEliminarTareas  : array que contiene las tareas que deben eliminarse
        '0bjADUsuario       : objeto para el manejo de datos referente a usuarios
        'intI               : contador para el FOR.
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADRoles As New AccesoDatos.Rol
        Dim objADUsuario As New AccesoDatos.UsuarioSistema
        Dim intId As Integer = 0
        Dim intI As Int32
        Dim strEliminarTareas() As String
        Dim objADSistema As New AccesoDatos.Sistema

        intId = pdtsDatos.SE_ROLES(0).IDROL
        Try
            objADRoles.IniciarTransaccion()
            objADUsuario.ConexionActiva = objADRoles.ConexionActiva
            objADRoles.ModificacionDeRol(intId, pdtsDatos.SE_ROLES(0).DESCRIPCIONPERFIL)
            'JorgeI - [viernes, 24 de septiembre de 2010] Modificaciones GCP-Cambios ID: ????
            'vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
            If pdtsDatos.Tables("tblUsuariosXRoles") Is Nothing Then
                objADRoles.EliminarComposicionRol(pdtsDatos.SE_ROLES(0).IDROL)
                'Recorro la colección para obtener las tareas asignadas al rol
                'y eliminarlas
                For intI = 0 To pdtsDatos.TareasAsignadas.Rows.Count - 1
                    objADRoles.AltaComposicionRol(intId, pdtsDatos.TareasAsignadas(intI).idTarea) '.Roles_Composicion(intI).idTarea)
                Next intI

            Else '^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                'si el rol se encuentra asignado a usuarios
                If pdtsDatos.Tables("tblUsuariosXRoles").Rows.Count > 0 Then
                    'Verifico si existen tareas que han sido eliminadas del árbol de roles
                    If Not strElementoseliminados.Equals("") Then
                        'Elimino las tareas de aquellos usuarios que posean el rol
                        'y luego como componente del rol
                        strEliminarTareas = strElementoseliminados.Split(",".ToCharArray()(0))
                        For intI = 0 To strEliminarTareas.Length - 1 ' .GetUpperBound(1)
                            If strEliminarTareas(intI) <> "" Then
                                objADUsuario.EliminarUnaTareaUsuario(intId, ToInt32(Mid(strEliminarTareas(intI), InStr(1, strEliminarTareas(intI), "¤", CompareMethod.Text) + 1)))
                                objADRoles.EliminarComposicionRol(intId, ToInt32(Mid(strEliminarTareas(intI), InStr(1, strEliminarTareas(intI), "¤", CompareMethod.Text) + 1)))
                            End If
                        Next intI
                    End If
                    'damos de alta las tareas asignadas al rol
                    For intI = 0 To pdtsDatos.TareasAsignadas.Rows.Count - 1
                        objADUsuario.EliminarUnaTareaUsuario(intId, pdtsDatos.TareasAsignadas(intI).idTarea)
                        objADRoles.EliminarComposicionRol(intId, pdtsDatos.TareasAsignadas(intI).idTarea)

                        objADRoles.AltaComposicionRol(intId, pdtsDatos.TareasAsignadas(intI).idTarea)
                        objADRoles.AltaTareaUsuarioPorRol(pdtsDatos.TareasAsignadas(intI).idTarea, pdtsDatos.TareasAsignadas(intI).TareaInhibida, intId)
                    Next
                    'eliminamos las tareas desasignadas al rol
                    For intI = 0 To pdtsDatos.TareasNoAsignadas.Rows.Count - 1
                        objADUsuario.EliminarUnaTareaUsuario(intId, pdtsDatos.TareasNoAsignadas(intI).idTarea)
                        objADRoles.EliminarComposicionRol(intId, pdtsDatos.TareasNoAsignadas(intI).idTarea)
                    Next
                Else 'si el rol no se encuentra asignado a usuarios
                    objADRoles.EliminarComposicionRol(pdtsDatos.SE_ROLES(0).IDROL)
                    'Recorro la colección para obtener las tareas asignadas al rol
                    'y eliminarlas
                    For intI = 0 To pdtsDatos.TareasAsignadas.Rows.Count - 1
                        objADRoles.AltaComposicionRol(intId, pdtsDatos.TareasAsignadas(intI).idTarea) '.Roles_Composicion(intI).idTarea)
                    Next intI
                End If
            End If

            objADSistema.ConexionActiva = objADRoles.ConexionActiva
            For intI = 0 To pdtsDatos.Tables("ParametrosDeABM").Rows.Count - 1
                If Not pdtsDatos.Tables("ParametrosDeABM").Rows(intI)("MensajesAuditoria").ToString().Trim.Equals("") Then _
                    AuditarCambios(objADSistema, pdtsDatos.Tables("ParametrosDeABM").Rows(intI)("MensajesAuditoria").ToString())
            Next


            objADRoles.FinalizarTransaccion(True)
            Return intId
        Catch ex As Exception
            'si ocurrió un error, se vuelve hacia atras la transacción
            objADRoles.FinalizarTransaccion(False)
            Throw
        End Try

    End Function
    ''' <summary>
    ''' decide si se realiza un alta o baja de roles
    ''' </summary>
    ''' <param name="pdtsDatos">dataset que contendra los datos para la decision</param>
    ''' <returns>identificador del elemento dado de alta o modificado</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''    007/03/2006      [AngelL]    Creado
    ''' </history>
    Public Function AdministrarRoles(ByRef pdtsDatos As EE.dtsRoles, ByVal pstrElementosEliminados As String) As Int32
        If pdtsDatos.SE_ROLES(0).IDROL = -1 Then
            Return AltaDeRol(pdtsDatos)
        Else
            Return ModificarRol(pdtsDatos, pstrElementosEliminados)
        End If
    End Function

    ''' <summary>
    ''' Elimina un rol
    ''' </summary>
    ''' <param name="pidRol">identificador del rol</param>
    ''' <returns>el identificadr del rol eliminado o 0 en caso de error</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''    08/03/2006      [AngelL]    Creado
    ''' </history>
    Public Function EliminarRol(ByVal pidRol As Int32) As Int32
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '           DESCRIPCION DE VARIABLES LOCALES
        'objADRol           : objeto para el manejo de datos referente a roles
        'objADUsuario       : objeto para el manejo de datos referente a usuarios
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADRol As New AccesoDatos.Rol
        Dim objADUsuario As New AccesoDatos.UsuarioSistema

        Try
            objADRol.IniciarTransaccion()
            objADUsuario.ConexionActiva = objADRol.ConexionActiva
            objADUsuario.EliminarUnaTareaUsuario(pidRol)
            objADRol.EliminarComposicionRol(pidRol)
            objADRol.EliminarRol(pidRol)
            objADRol.FinalizarTransaccion(True)
            Return pidRol
        Catch ex As Exception
            objADRol.FinalizarTransaccion(False)
            Return 0
        End Try
    End Function
End Class
