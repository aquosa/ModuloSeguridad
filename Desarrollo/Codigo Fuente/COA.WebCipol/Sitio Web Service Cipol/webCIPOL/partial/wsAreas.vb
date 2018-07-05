Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Namespace Seguridad
    Partial Public Class wsSeguridad

        ''' <summary>
        ''' recupera las areas del sistema
        ''' </summary>
        ''' <returns>datset con datos, nulo si hubo error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AngelL]   viernes, 06 de octubre de 2006   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarAreas() As dtsKArea
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '			DESCRIPCION DE LAS VARIABLES LOCALES
            ' objAD				: acceso a datos de areas
            ' dtsRetorno		: dataset con los datos a recuperar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim objAD As New AccesoDatos.Areas
            Dim dtsRetorno As New dtsKArea

            Try
                objAD.CrearConexion()
                objAD.Recuperar(dtsRetorno, dtsRetorno.SIST_KAREAS.TableName)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                dtsRetorno = Nothing
            Finally
                objAD.Desconectar()
            End Try

            Return dtsRetorno
        End Function


        ''' <summary>
        ''' recupera las areas del sistema
        ''' </summary>
        ''' <returns>datset con datos, nulo si hubo error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AngelL]   viernes, 06 de octubre de 2006   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function AltaDeAreas(ByVal dtsDatos As dtsKArea) As Int32
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '			DESCRIPCION DE LAS VARIABLES LOCALES
            ' objAD				: acceso a datos de areas
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objAD As New AccesoDatos.Areas

            Try
                objAD.CrearConexion()
                Return objAD.Insertar(dtsDatos, 0)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return -1
            Finally
                objAD.Desconectar()
            End Try
        End Function
        ''' <summary>
        ''' recupera las areas del sistema
        ''' </summary>
        ''' <returns>datset con datos, nulo si hubo error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AngelL]   viernes, 06 de octubre de 2006   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function ActualizarArea(ByVal dtsDatos As dtsKArea) As Int32
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '			DESCRIPCION DE LAS VARIABLES LOCALES
            ' objAD				: acceso a datos de areas
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objAD As New AccesoDatos.Areas

            Try
                objAD.CrearConexion()
                Return objAD.Actualizar(dtsDatos, 0)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return -1
            Finally
                objAD.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' recupera las areas del sistema
        ''' </summary>
        ''' <returns>datset con datos, nulo si hubo error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [AngelL]   viernes, 06 de octubre de 2006   Creado
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function EliminarArea(ByVal idArea As Int32) As Int32
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '			DESCRIPCION DE LAS VARIABLES LOCALES
            ' objAD				: acceso a datos de areas
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objAD As New AccesoDatos.Areas

            Try
                objAD.CrearConexion()
                Return objAD.Eliminar(idArea)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return -1
            Finally
                objAD.Desconectar()
            End Try
        End Function

        ''' <summary>
        ''' recupera las areas del sistema
        ''' </summary>
        ''' <returns>datset con datos, nulo si hubo error</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [IvanR]   viernes, 11 de junio de 2010   Creado GCP-Cambio ID:9098
        ''' </history>
        <WebMethod(enableSession:=True)> _
        Public Function AgregarArea(ByVal idArea As Int32) As Int32
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '			DESCRIPCION DE LAS VARIABLES LOCALES
            ' objAD				: acceso a datos de areas
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objAD As New AccesoDatos.Areas

            Try
                objAD.CrearConexion()
                Return objAD.Agregar(idArea)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return -1
            Finally
                objAD.Desconectar()
            End Try
        End Function

    End Class
End Namespace