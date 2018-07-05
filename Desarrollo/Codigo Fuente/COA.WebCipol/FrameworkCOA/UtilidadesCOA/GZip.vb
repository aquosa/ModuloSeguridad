Imports System.IO

Namespace Compresion

    ''' <summary>
    ''' Permite Compatar/Descompactar archivos utilizando GZip
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GZip

        ''' <summary>
        ''' Permite compactar un Stream a través de GZIPStream
        ''' </summary>
        ''' <param name="bytDatos">Array de bytes que representa los datos a compactar</param>
        ''' <returns>Stream compactado</returns>
        ''' <remarks></remarks>
        Public Function Compactar(ByVal bytDatos() As Byte) As MemoryStream
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' objBufferCompresion : Buffer con los datos compactados
            ' objCompactar        : Clase GZip utilizada para compactar los datos
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objBufferCompresion As New System.IO.MemoryStream
            Dim objCompactar As System.IO.Compression.GZipStream = Nothing

            objCompactar = New Compression.GZipStream(objBufferCompresion, Compression.CompressionMode.Compress, True)
            objCompactar.Write(bytDatos, 0, bytDatos.Length)
            objCompactar.Close()

            Return objBufferCompresion

        End Function

        ''' <summary>
        ''' Determina la cantidad de bytes que poseía el Stream antes de descompactarse
        ''' </summary>
        ''' <param name="objStreamCompactado">Stream compactado</param>
        ''' <returns>Cantidad de bytes del Stream original</returns>
        ''' <remarks></remarks>
        Private Function CalcularCantidadBytesSinCompactar(ByVal objStreamCompactado As MemoryStream) As Integer
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' intCantByte  : Cantidad de bytes leidos 
            ' intTotalByte : Cantidad de bytel total del archivo compactado 
            ' bytBuffer    : Buffer con los datos leidos
            ' objCompactar : GZip utilizado para leer el stream
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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
        ''' Permite descompactar un Stream utilizando GZipStream
        ''' </summary>
        ''' <param name="objStreamCompactado">Stream que se encuentra compactado</param>
        ''' <returns>Stream descompactado</returns>
        ''' <remarks></remarks>
        Public Function Descompactar(ByVal objStreamCompactado As MemoryStream) As MemoryStream
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' intTotalBytes : Cantidad total de bytes del archivo antes de compactar
            ' bytBuffer     : Buffer con los datos a leer o escribir
            ' objCompactar  : GZip utilizado para compactar
            ' objRet        : Flujo de memoria
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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

    End Class

End Namespace