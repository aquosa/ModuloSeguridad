Public Class PadreSistema
    Inherits ReglasNegocio.PadreReglasNegocio

    ''' <summary>
    ''' Desglosa los datos de auditoría y los envia a la capa de accceso a datos
    ''' para su insercion
    ''' </summary>
    ''' <param name="objADSistema">Componente lógico de acceso a datos que se utiliza para auditar</param>
    ''' <param name="strAuditorias">String que contiene los datos de auditoría</param>
    ''' <returns>Cantidad de filas afectadas</returns>
    ''' <remarks></remarks>
    ''' <history>Gustavo Mazzaglia - 02/03/2006 </history>
    Protected Function AuditarCambios(ByVal objADSistema As AccesoDatos.Sistema, ByVal strAuditorias As String) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                   DESCRIPCION DE VARIABLES LOCALES
        'intI         : Contador del For
        'strAuditar   : Array que contiene los cambios realizados por el
        '               usuario
        'strDatos     : Array que contiene los valores de una auditoría
        'intRet       : Valor de retorno
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strAuditar() As String = Nothing
        Dim strDatos() As String = Nothing
        Dim intI As Integer = 0, intRet As Integer = 0

        strAuditar = strAuditorias.Split("æ"c)    'Alt + 145
        For intI = 0 To strAuditar.GetUpperBound(0)
            If strAuditar(intI).Equals(String.Empty) Then
            Else
                strDatos = strAuditar(intI).Split("Æ"c) 'Alt + 146
                intRet += objADSistema.Auditar(Short.Parse(strDatos(0)), strDatos(1), strDatos(2), strDatos(3))
            End If
        Next

        Return intRet

    End Function
End Class
