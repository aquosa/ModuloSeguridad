Option Strict On
Option Explicit On

Imports EE = Fachada.Seguridad
Imports System.Convert

Public Class Tarea
   Inherits PadreSistema
   ''' <summary>
   ''' Verifica si la tarea autorizante está en uso en un ROL
   ''' </summary>
   ''' <param name="pidTarea">ID de tarea a verificar</param>
   ''' <returns></returns>
   ''' <remarks></remarks>
   ''' <history>
   ''' [JorgeI]  [miércoles, 22 de septiembre de 2010] Creado GCP-Cambios ID:9374
   ''' </history>
   Public Function VerificaTareasAutorizantesEnUso(ByVal pidTarea As Int32) As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '               DESCRIPCION DE LAS VARIABLES LOCALES
      ' objADTareas               : acceso a datos paa el manejo de tareas
      ' intI                      : valor que se retornará
      ' dtsTemporal               : dataset de uso temporario.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim objADTareas As New AccesoDatos.Tarea
      Dim intI As Int32

      Try
         objADTareas.CrearConexion()

         intI = objADTareas.VerificaTareasAutorizantesEnUso(pidTarea)

      Catch ex As Exception
         Throw
      Finally
         objADTareas.Desconectar()
      End Try

      Return intI

   End Function


   ''' <summary>
   ''' Elimina una relacion de tareas autorizantes
   ''' </summary>
   ''' <param name="pidTarea">identificador de la tarea a eliminar</param>
   ''' <returns>cantidad de registros eliminados, 0 en caso de error</returns>
   ''' <remarks></remarks>
   ''' <history>
   '''    13/03/2006            [AngelL]        Creado
   ''' </history>
   Public Function EliminarTareasAutorizantes(ByVal pidTarea As Int32) As Int32
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      '               DESCRIPCION DE LAS VARIABLES LOCALES
      ' objADTareas               : acceso a datos paa el manejo de tareas
      ' intI                      : valor que se retornará
      ' dtsTemporal               : dataset de uso temporario.
      ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
      Dim objADTareas As New AccesoDatos.Tarea
      Dim intI As Int32
      Dim dtsTemporal As New DataSet

      Try
         objADTareas.IniciarTransaccion()

         objADTareas.RetornarRelacionAutorizacion(dtsTemporal, "se_rel_autoriz_temp", pidTarea) '
         For intI = 0 To dtsTemporal.Tables("se_rel_autoriz_temp").Rows.Count - 1 '
            objADTareas.ModificarIdAutorizacionDeTarea(0, ToInt32(dtsTemporal.Tables("se_rel_autoriz_temp").Rows(intI)("IdTareaPrimitiva"))) '
         Next intI '

         objADTareas.BajaRelacionAutorizacion(, pidTarea) '
         objADTareas.BajaTarea(pidTarea) '
         objADTareas.FinalizarTransaccion(True) '
      Catch ex As Exception
         objADTareas.FinalizarTransaccion(False)
         Throw
      End Try

      Return intI

   End Function

   ''' <summary>
   ''' Permite ingresar o actualizar una tarea autorizante
   ''' </summary>
   ''' <param name="dtsDatos">DataSet que contiene los datos para ingresar o actualizar la tarea</param>
   ''' <returns>Retorna cero si no se ha podido ingresar o actualizar la tarea. En el caso de alta 
   ''' el valor de retorno representa el identificador de la tarea
   ''' </returns>
   ''' <remarks></remarks>
   ''' <history>Gustavo Mazzaglia - 06/03/2006 </history>
    Public Function AdministrarTareasSupervisantes(ByVal dtsDatos As EE.dtsTareas) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'objADTareas    : Componente lógico de acceso a datos que se utiliza para
        '                 ingresar o actualizar la tarea autorizante
        'objADSistema   : Componente lógico de acceso a datos que se utiliza para
        '                 ingresar la auditoría
        'intID          : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADTareas As AccesoDatos.Tarea = Nothing
        Dim objADSistema As AccesoDatos.Sistema = Nothing
        Dim intID As Integer = 0


        Try
            objADTareas = New AccesoDatos.Tarea
            objADSistema = New AccesoDatos.Sistema
            objADTareas.IniciarTransaccion()
            objADSistema.ConexionActiva = objADTareas.ConexionActiva
            'Si se trata de un alta
            If dtsDatos.TAREAS(0).IDTAREA = -1 Then
                intID = objADTareas.IngresarTareaAutorizante(dtsDatos)
                With dtsDatos.TAREAS(0)
                    .BeginEdit()
                    .IDTAREA = intID
                    .EndEdit()
                End With
            Else
                objADTareas.EliminarRelacionConTareaAutorizante(dtsDatos.TAREAS(0).IDTAREA)
                intID = objADTareas.ActualizarTareaAutorizante(dtsDatos)
            End If
            Me.AuditarCambios(objADSistema, dtsDatos.TAREAS(0).AUDITORIA)
            objADTareas.IngresarRelacionConTareaAutorizante(dtsDatos.TAREAS(0).IDTAREA, CType(dtsDatos.Tables("SE_TAREASAUTORIZADAS").Rows(0)("IDTAREA"), Integer))
            'TODO:FALTABA ESTO!!! :@
            objADTareas.ModificarIdAutorizacionDeTarea(CType(dtsDatos.Tables("SE_TAREASAUTORIZADAS").Rows(0)("IDAUTORIZACION"), Integer), CType(dtsDatos.Tables("SE_TAREASAUTORIZADAS").Rows(0)("IDTAREA"), Integer))


            objADTareas.FinalizarTransaccion(True)

        Catch ex As Exception
            objADTareas.FinalizarTransaccion(False)
            intID = 0
        End Try

        Return intID

    End Function

End Class
