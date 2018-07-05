Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

Namespace Seguridad.ABM
    Public Enum TipoProceso As Integer
        Insertar = 1
        Eliminar = 2
        Actualizar = 3
    End Enum

    <WebService(Namespace:="http://WSCOA_ABMBASE")> _
        <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
        <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
        Public Class wsCOA_ABMBase
        Inherits PadreSistema

        ''' <summary>
        ''' Permite establecer el Id de conexion a la Base de Datos
        ''' </summary>
        ''' <returns>True si el cambio se realizo con exito, False en caso contrario</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Juano]          [Jueves, 23 de octubre de 2008]       Creado 
        ''' </history>
        <WebMethod(EnableSession:=True)> _
        Public Function EstablecerConexionActiva(ByVal IDConexion As String) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'objRNConexion : Componente logico de Reglas de Negocio
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objRNConexion As ReglasNegocio.Conexion

            Try

                objRNConexion = New ReglasNegocio.Conexion

                objRNConexion.EstablecerIDConexion(IDConexion)

                Return True

            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function


        ''' <summary>
        ''' Recupera todos los datos de la tabla pasada por parametro
        ''' </summary>
        ''' <param name="NombreTabla">Nombre de la Tabla del Dataset en donde se recuperan los datos</param>
        ''' <returns>Dataset con los datos</returns>
        ''' [Juano]          22/08/2007        Creado
        ''' <remarks></remarks>
        <WebMethod(enableSession:=True)> _
        Public Function Recuperar(ByVal NombreTabla As String) As DataSet
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'rnCOA_ABMBase  : Componente utilizado para acceder a reglas de negocio.
            'dtsRetorno     : Dataset que se va a retornar.
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim dtsRetorno As New DataSet
            Dim rnCOA_ABMBase As New ReglasNegocio.rnCOA_ABMBase
            Try
                'Recupera los datos de la Tabla Actual
                rnCOA_ABMBase.Recuperar(dtsRetorno, NombreTabla)
                'Retorna el dataset con los datos
                Return dtsRetorno
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            End Try

        End Function

        ''' <summary>
        ''' Inserta los datos de la tabla pasada por parametro
        ''' </summary>
        ''' <param name="Dataset">Dataset utilizado para la Grabación</param>
        ''' <param name="NombreTabla">Nombre de la Tabla de Grabación</param>
        ''' <param name="TipoProceso">Tipo de Proceso a Grabar (Inserción, Eliminación o Actualización)</param>
        ''' <returns>Cantidad de filas afectadas</returns>
        ''' [Juano]          22/08/2007        Creado
        ''' <remarks></remarks>
        <WebMethod(enableSession:=True)> _
        Public Function Grabar(ByVal Dataset As DataSet, _
                               ByVal NombreTabla As String, _
                               ByVal TipoProceso As TipoProceso) As Int32
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'rnCOA_ABMBase  : Componente utilizado para acceder a reglas de negocio.
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim rnCOA_ABMBase As New ReglasNegocio.rnCOA_ABMBase
            Try
                'Inserta los datos de la Tabla Actual
                Return rnCOA_ABMBase.Grabar(Dataset, NombreTabla, TipoProceso)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Retorna la Fecha y la Hora del servidor
        ''' </summary>
        ''' <returns>Retorna la Fecha y la Hora del servidor</returns>
        ''' [Juano]          22/08/2007        Creado
        ''' <remarks></remarks>
        <WebMethod(enableSession:=True)> _
        Public Function FechaServidor() As Date
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            'PadreAccesoDatos  : Acceso a datos padre
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim PadreAccesoDatos As New AccesoDatos.PadreAccesoDatos
            Try
                'Inicia la Transacción
                PadreAccesoDatos.CrearConexion()
                Return PadreAccesoDatos.FechaServidor()
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            End Try

        End Function
    End Class
End Namespace