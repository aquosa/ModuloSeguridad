''' <summary>
''' Permite serializar/deserializar un DataSet
''' </summary>
Public Class SerializarPorXML

#Region "Serializacion"

    ''' <summary>
    ''' Serializa un DataSet como un array de bytes
    ''' </summary>
    ''' <param name="dtsDatos">DataSet a serializar</param>
    ''' <returns>Array de bytes que representa el XML del DataSet</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Gustavom]	        [04/07/2006]	                        Creado
    ''' </history>
    Public Function ConvertirDatasetEnXML(ByRef dtsDatos As System.Data.DataSet) As Byte()
        Try
            Return System.Text.Encoding.UTF8.GetBytes(dtsDatos.GetXml())
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Deserializa a un objeto DataSet a partir de un XML.
    ''' </summary>
    ''' <param name="SchemaXML">Esquema del objeto DataSet a instanciar</param>
    ''' <param name="DatosXML">Datos del DataSet</param>
    ''' <returns>Instancia del DataSet</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Gustavom]	        [30/08/2005]	                        Creado
    ''' </history>
    Public Function ConvertirXMLEnDataset(ByVal SchemaXML As String, ByVal DatosXML As String) As System.Data.DataSet
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'dtsTemp         : Objeto DataSet que se utiliza para instanciar el objeto
        'objMemStreamIn  : Objeto que se utiliza para almacenar datos en memoria
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If DatosXML = "" OrElse DatosXML = Nothing Then Return Nothing

        Dim dtsTemp As System.Data.DataSet
        Dim objMemStreamIn As System.IO.MemoryStream

        Try
            dtsTemp = New System.Data.DataSet

            'Recupera el schema DataSet
            objMemStreamIn = New System.IO.MemoryStream(System.Text.Encoding.Unicode.GetBytes(SchemaXML))
            dtsTemp.ReadXmlSchema(objMemStreamIn)
            objMemStreamIn = Nothing

            'convierto a un string comun
            DatosXML = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(DatosXML))

            'Recupera los DataSet
            objMemStreamIn = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(DatosXML))
            dtsTemp.ReadXml(objMemStreamIn)
            objMemStreamIn = Nothing

            Return dtsTemp

        Catch ex As Exception
            Throw
        End Try

    End Function

#End Region

End Class