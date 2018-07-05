Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Serializar
    ''' <summary>
    ''' Permite serializar un objeto a través de BinaryFormatter
    ''' </summary>
    ''' <param name="objInstancia">Objeto que deseo serializar</param>
    ''' <history>
    ''' [LucianoP]          [viernes, 09 de enero de 2009]       Creado
    ''' </history>
    Public Function SerializarDatos(ByVal objInstancia As Object) As Byte()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'objFlujoDeMemoria : Flujo de memoria donde se guarda el objeto serializado
        'objFormateador    : Objeto utilizado para serializar el flujo de memoria
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objFlujoDeMemoria As MemoryStream = New MemoryStream()
        Dim objFormateador As BinaryFormatter = New BinaryFormatter()

        If objInstancia Is Nothing Then
            Return Nothing
        Else
            objFormateador.Serialize(objFlujoDeMemoria, objInstancia)

            Return objFlujoDeMemoria.ToArray()
        End If


    End Function

    ''' <summary>
    ''' Permite deserializar un objeto a través de BinaryFormatter
    ''' </summary>
    ''' <param name="objInstancia">Objeto serializado en un array de bytes que deseo
    ''' deserializar</param>
    ''' <history>
    ''' [LucianoP]          [viernes, 09 de enero de 2009]       Creado
    ''' </history>
    Public Function DeserializarDatos(ByVal objInstancia As Byte()) As Object
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        'objFlujoDeMemoria : Flujo de memoria donde se guarda el objeto serializado
        'objFormateador    : Objeto utilizado para serializar el flujo de memoria
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objFlujoDeMemoria As MemoryStream
        Dim objFormateador As BinaryFormatter = New BinaryFormatter()

        If objInstancia Is Nothing Then
            Return Nothing
        Else
            objFlujoDeMemoria = New MemoryStream(objInstancia)
            Return objFormateador.Deserialize(objFlujoDeMemoria)
        End If

    End Function
End Class
