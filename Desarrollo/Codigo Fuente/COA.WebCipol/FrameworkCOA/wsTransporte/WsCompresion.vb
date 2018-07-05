Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Services.Protocols.SoapExtension
Imports System.IO
Public Class WsCompresion
    Inherits SoapExtension

    Private mobjBufferOrig As Stream   'Representa un Soap Request Response
    Private mobjBufferActual As System.IO.MemoryStream

    'Se utiliza para inicializar datos. Se aplica cuando la configuración se realiza
    'a nivel de métodos del webservice. Se llama la primera vez que se llama al método
    'y luego por cada llamada se pasa al procedimiento Initialize el objeto que se 
    'retorna
    Public Overloads Overrides Function GetInitializer(ByVal serviceType As System.Type) As Object
        Return System.DBNull.Value
    End Function

    'Se utiliza para inicializar datos. Se aplica cuando la configuración se realiza
    'a nivel del web.config. Se llama la primera vez que se llama al webservices
    'y luego por cada llamada se pasa al procedimiento Initialize el objeto que se 
    'retorna
    Public Overloads Overrides Function GetInitializer(ByVal methodInfo As System.Web.Services.Protocols.LogicalMethodInfo, ByVal attribute As System.Web.Services.Protocols.SoapExtensionAttribute) As Object
        Return System.DBNull.Value
    End Function

    Public Overrides Sub Initialize(ByVal initializer As Object)
    End Sub

    Public Overrides Sub ProcessMessage(ByVal message As System.Web.Services.Protocols.SoapMessage)

        Dim objStreamComprimido As MemoryStream
        Dim objStreamDescomprimido As MemoryStream

        Select Case message.Stage
            Case SoapMessageStage.AfterSerialize
                objStreamComprimido = Compactar(mobjBufferActual)
                CopiarOrigen(objStreamComprimido, mobjBufferOrig)

            Case SoapMessageStage.BeforeDeserialize
                objStreamComprimido = CopiarDestino(mobjBufferOrig)
                objStreamDescomprimido = Descompactar(objStreamComprimido)
                Copiar(objStreamDescomprimido, mobjBufferActual)
                mobjBufferActual.Position = 0
            Case SoapMessageStage.BeforeSerialize
            Case SoapMessageStage.AfterDeserialize

        End Select

    End Sub

    'Se utiliza para almacenar el stream en el cual viene el mensaje soap original
    'y retornar el stream del mensaje soap modificado
    Public Overrides Function ChainStream(ByVal stream As System.IO.Stream) As System.IO.Stream
        mobjBufferOrig = stream
        mobjBufferActual = New System.IO.MemoryStream

        Return mobjBufferActual
    End Function


    ''' <summary>
    ''' Permite compactar un Stream a través de GZIPStream
    ''' </summary>
    ''' <param name="objStreamCompactar">Stream que se desea compactar</param>
    ''' <returns>Stream compactado</returns>
    ''' <remarks></remarks>
    Private Function Compactar(ByVal objStreamCompactar As MemoryStream) As MemoryStream
        Dim objBufferCompresion As New System.IO.MemoryStream
        Dim objCompactar As System.IO.Compression.GZipStream = Nothing

        objStreamCompactar.Position = 0
        objCompactar = New Compression.GZipStream(objBufferCompresion, Compression.CompressionMode.Compress, True)
        objCompactar.Write(objStreamCompactar.GetBuffer, 0, CType(objStreamCompactar.Length, Integer))
        objCompactar.Close()

        Return objBufferCompresion

    End Function

    ''' <summary>
    ''' Permite descompactar un Stream utilizando GZipStream
    ''' </summary>
    ''' <param name="objStreamCompactado">Stream que se encuentra compactado</param>
    ''' <returns>Stream descompactado</returns>
    ''' <remarks></remarks>
    Private Function Descompactar(ByVal objStreamCompactado As MemoryStream) As MemoryStream
        Dim intTotalBytes As Integer = CalcularCantidadBytesSinCompactar(objStreamCompactado)
        Dim bytBuffer(intTotalBytes) As Byte
        Dim objCompactar As System.IO.Compression.GZipStream
        Dim objRet As MemoryStream

        objStreamCompactado.Position = 0
        objCompactar = New System.IO.Compression.GZipStream(objStreamCompactado, Compression.CompressionMode.Decompress, True)

        intTotalBytes = objCompactar.Read(bytBuffer, 0, intTotalBytes)


        objRet = New MemoryStream(intTotalBytes * 2)
        objRet.Write(bytBuffer, 0, intTotalBytes)

        objCompactar.Close()

        Return objRet

    End Function

    ''' <summary>
    ''' Determina la cantidad de bytes que poseía el Stream antes de descompactarse
    ''' </summary>
    ''' <param name="objStreamCompactado">Stream compactado</param>
    ''' <returns>Cantidad de bytes del Stream original</returns>
    ''' <remarks></remarks>
    Private Function CalcularCantidadBytesSinCompactar(ByVal objStreamCompactado As MemoryStream) As Integer
        Dim intCantByte As Integer, intTotalByte As Integer
        Dim bytBuffer(100) As Byte
        Dim objCompactar As System.IO.Compression.GZipStream

        objStreamCompactado.Position = 0
        objCompactar = New System.IO.Compression.GZipStream(objStreamCompactado, Compression.CompressionMode.Decompress, True)
        Do
            intCantByte = objCompactar.Read(bytBuffer, 0, 100)
            If intCantByte = 0 Then Exit Do
            intTotalByte += intCantByte
        Loop
        objCompactar.Close()

        Return intTotalByte

    End Function

    ''' <summary>
    ''' Permite copiar el contenido de un Stream en otro 
    ''' </summary>
    ''' <param name="StreamOrigen">Stream del cual se desea copiar el contenido</param>
    ''' <param name="StreamDestino">Stream al cual se copia el contenido</param>
    ''' <remarks></remarks>
    Private Sub Copiar(ByVal StreamOrigen As Stream, ByVal StreamDestino As Stream)
        Dim reader As StreamReader = Nothing
        Dim writer As StreamWriter

        StreamOrigen.Position = 0
        reader = New StreamReader(StreamOrigen)
        writer = New StreamWriter(StreamDestino)
        writer.WriteLine(reader.ReadToEnd())

        writer.Flush()

    End Sub

    ''' <summary>
    ''' Permite copiar el contenido de un Stream en otro realizando la conversión a Base64
    ''' </summary>
    ''' <param name="StreamOrigen">Stream del cual se desea copiar el contenido</param>
    ''' <param name="StreamDestino">Stream al cual se copia el contenido</param>
    ''' <remarks></remarks>
    Private Sub CopiarOrigen(ByVal StreamOrigen As MemoryStream, ByVal StreamDestino As Stream)
        Dim reader As StreamReader = Nothing
        Dim writer As StreamWriter

        StreamOrigen.Position = 0
        'reader = New StreamReader(StreamOrigen)
        writer = New StreamWriter(StreamDestino)
        'writer.WriteLine(reader.ReadToEnd())
        writer.WriteLine(System.Convert.ToBase64String(StreamOrigen.GetBuffer, 0, CType(StreamOrigen.Length, Integer)))

        writer.Flush()

    End Sub

    ''' <summary>
    ''' Permite copiar el contenido de un Stream en otro eliminando Base64
    ''' </summary>
    ''' <param name="StreamOrigen">Stream del cual se desea copiar el contenido</param>
    ''' <returns>Stream al cual se copia el contenido</returns>
    ''' <remarks></remarks>
    Private Function CopiarDestino(ByVal StreamOrigen As Stream) As MemoryStream
        Dim reader As StreamReader
        Dim writer As StreamWriter = Nothing

        reader = New StreamReader(StreamOrigen)
        'writer = New StreamWriter(StreamDestino)
        'writer.WriteLine(reader.ReadToEnd())
        'writer.Flush()
        Return New MemoryStream(System.Convert.FromBase64String(reader.ReadToEnd))

    End Function

End Class
