Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Namespace Seguridad
    Partial Public Class wsSeguridad

        ''' <summary>
        ''' Retorna los datos de las terminales activas y las áreas en las cuales
        ''' se encuentran instaladas
        ''' </summary>
        ''' <returns>DataSet con retorno de datos de terminales</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function RecuperarDatosParaABMTerminales() As dtsTerminales
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objADTerm  : Componente lógico de acceso a datos que se utiliza para 
            '             recuperar las terminales activas
            'objADSist  : Componente lógico de acceso a datos que se utiliza para 
            '             recuperar las áreas 
            'dtsRet     : DataSet de retorno 
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objADTerm As AccesoDatos.Terminal = Nothing
            Dim objADSist As AccesoDatos.Sistema = Nothing
            Dim dtsRet As New dtsTerminales

            Try
                objADTerm = New AccesoDatos.Terminal
                objADSist = New AccesoDatos.Sistema
                objADTerm.CrearConexion()
                objADTerm.RecuperarTerminales(dtsRet, False) 'GCP-Cambio ID:9287
                objADSist.ConexionActiva = objADTerm.ConexionActiva
                objADSist.RecuperarAreas(dtsRet)

                Return dtsRet
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return Nothing
            Finally
                objADTerm.Desconectar()
            End Try

        End Function

        ''' <summary>
        ''' Permite el ingreso o actualización de una terminal
        ''' </summary>
        ''' <param name="dtsTerminal">DataSet que contiene los datos de la terminal</param>
        ''' <returns> 0 en caso de que no se haya ingresado o actualizado. Un valor positivo en la inserción representa el nuevo ID de la terminal</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function AdministrarTerminales(ByVal dtsTerminal As dtsTerminales) As Short
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objTerm    : Componente de regla de negocio que se utiliza para ingresa o
            '             actualizar la terminal
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objTerm As ReglasNegocio.Terminal

            If dtsTerminal Is Nothing Then Throw New ArgumentException("El dataset esta vacío")
            If dtsTerminal.SE_TERMINALES.Rows.Count = 0 Then Throw New ArgumentException("El dataset esta vacío")

            Try
                objTerm = New ReglasNegocio.Terminal
                Return objTerm.AdministrarTerminales(dtsTerminal)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function



        ''' <summary>
        ''' Permite eliminar una terminal
        ''' </summary>
        ''' <param name="IDTerminal">Identificador de la terminal</param>
        ''' <returns>True o False</returns>
        ''' <remarks></remarks>
        ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
        <WebMethod(enableSession:=True)> _
        Public Function EliminarTerminal(ByVal IDTerminal As Short) As Boolean
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                   DESCRIPCION DE VARIABLES LOCALES
            'objTerminal   : Componente que se utiliza para eliminar la terminal
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objTerminal As ReglasNegocio.Terminal = Nothing

            Try
                objTerminal = New ReglasNegocio.Terminal
                Return objTerminal.EliminarTerminal(IDTerminal)
            Catch ex As Exception
                Me.PublicarExcepcion(ex)
                Return False
            End Try

        End Function

    End Class
End Namespace