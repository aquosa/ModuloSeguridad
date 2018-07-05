Imports EE = Fachada.Seguridad
Public Class Terminal
    Inherits PadreSistema

    ''' <summary>
    ''' Permite el ingreso o actualización de una terminal
    ''' </summary>
    ''' <param name="dtsTerminal">DataSet que contiene los datos de la terminal</param>
    ''' <returns>0 en caso de que no se haya ingresado o actualizado. Un valor positivo en la inserción representa el nuevo ID de la terminal</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
    Public Function AdministrarTerminales(ByVal dtsTerminal As EE.dtsTerminales) As Short
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'objADTerm  : Componente lógico de acceso a datos que se utiliza para 
        '             ingresar o actualizar la terminal
        'shtRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADTerm As AccesoDatos.Terminal = Nothing
        Dim shtRet As Short = 0

        Try
            objADTerm = New AccesoDatos.Terminal
            objADTerm.CrearConexion()
            If dtsTerminal.SE_TERMINALES(0).RowState = DataRowState.Added Then
                'Recupera el próximo ID de la terminal
                shtRet = objADTerm.IngresarTerminal(dtsTerminal)
            Else
                shtRet = objADTerm.ActualizarTerminal(dtsTerminal)
            End If
        Finally
            objADTerm.Desconectar()
        End Try

        Return shtRet

    End Function


    ''' <summary>
    ''' Permite eliminar una terminal
    ''' </summary>
    ''' <param name="IDTerminal">Identificador de la terminal</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Public Function EliminarTerminal(ByVal IDTerminal As Short) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'objADTerm  : Componente lógico de acceso a datos que se utiliza para 
        '             eliminar la terminal
        'objADUs    : Componente lógico de acceso a datos que se utiliza para
        '             eliminar las terminales prohibidas por los usuarios
        'blnRet     : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADTerm As AccesoDatos.Terminal = Nothing
        Dim objADUs As AccesoDatos.UsuarioSistema = Nothing, blnRet As Boolean = False


        Try
            objADTerm = New AccesoDatos.Terminal
            objADUs = New AccesoDatos.UsuarioSistema
            objADTerm.IniciarTransaccion()
            objADUs.ConexionActiva = objADTerm.ConexionActiva
            objADUs.EliminarTerminalProhibida(IDTerminal)
            blnRet = objADTerm.EliminarTerminal(IDTerminal) > 0
            objADTerm.FinalizarTransaccion(True)

            Return blnRet
        Catch ex As Exception
            objADTerm.FinalizarTransaccion(False)
            Throw
        End Try

    End Function




End Class
