Public Class Sistema
    Inherits PadreSistema

    ''' <summary>
    ''' Permite el ingreso o actualizaci�n de las pol�ticas generales
    ''' </summary>
    ''' <param name="dtsDatos">Dataset que contiene las pol�ticas generales a ingresar o actualizar</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 28/02/2006 </history>
    Public Function AdministrarPoliticasGenerales(ByVal dtsDatos As System.Data.DataSet) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'objADSistema : Componente l�gico de acceso a datos que se
        '               utiliza para el ingreso o actualizaci�n de 
        '               las pol�ticas generales
        'intRet       : Cantidad de filas afectadas
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADSistema As AccesoDatos.Sistema = Nothing
        Dim intRet As Integer

        Try
            objADSistema = New AccesoDatos.Sistema
            objADSistema.IniciarTransaccion()
            'Si se trata de un alta
            If dtsDatos.Tables(0).Rows(0).RowState = DataRowState.Added Then
                intRet = objADSistema.IngresarPoliticasGenerales(dtsDatos.Tables(0).Rows(0).Item("Columna4").ToString, dtsDatos.Tables(0).Rows(0).Item("Columna5").ToString)
            Else
                'Si se trata de una modificaci�n
                intRet = objADSistema.ActualizarPoliticasGenerales(dtsDatos.Tables(0).Rows(0).Item("Columna4").ToString, dtsDatos.Tables(0).Rows(0).Item("Columna5").ToString)
                'Audita los cambios realizados
                Me.AuditarCambios(objADSistema, dtsDatos.Tables(0).Rows(1).Item("Columna4").ToString)
            End If
            objADSistema.FinalizarTransaccion(True)
        Catch ex As Exception
            objADSistema.FinalizarTransaccion(False)
            Throw
        End Try

        Return intRet > 0

    End Function

    ''' <summary>
    ''' Registra el mensaje de auditor�a de seguridad que se recibe
    ''' </summary>
    ''' <param name="MensajeAuditoria">Mensaje de auditor�a a grabar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegistrarAuditoriaSeguridad(ByVal MensajeAuditoria As String) As Boolean
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DECLARACION DE VARIABLES LOCALES
        'objADSistema   : Componente l�gico de acceso a datos que se utiliza para 
        '                 registrar la auditor�a
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objADSistema As AccesoDatos.Sistema = Nothing

        Try
            objADSistema = New AccesoDatos.Sistema
            objADSistema.CrearConexion()
            Return Me.AuditarCambios(objADSistema, MensajeAuditoria) > 0
        Finally
            objADSistema.Desconectar()
        End Try

    End Function


End Class
