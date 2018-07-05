Imports System.Runtime.InteropServices
Imports System.Text
Namespace CifrarDatos

    ''' <summary>
    ''' Permite el acceso a la API de protección de datos (DPAPI), la misma permite cifrar 
    ''' datos utilizando información de la cuenta o equipo del usuario actual.
    ''' </summary>
    Public Class DPAPI
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
        Public Structure DATA_BLOB
            Public cbData As Integer
            Public pbData As IntPtr
        End Structure

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
        Friend Structure CRYPTPROTECT_PROMPSTRUCT
            Public cbSize As Integer
            Public dwPromptFlags As Integer
            Public hwndApp As IntPtr
            Public szPrompt As String
        End Structure

        Private Shared NullPtr As IntPtr = IntPtr.Zero
        Private Const CRYPTPROTECT_UI_FORBIDEN As Integer = &H1
        Private Const CRYPTPROTECT_LOCAL_MACHINE As Integer = &H4

        <DllImport("Crypt32.dll", SetLastError:=True)> _
        Private Shared Function CryptProtectData(ByRef pDataIn As DATA_BLOB, _
                                                 ByVal szDataDescr As String, _
                                                 ByRef pOptionalEntropy As DATA_BLOB, _
                                                 ByVal pvReserved As IntPtr, _
                                                 ByRef pPromptStruct As CRYPTPROTECT_PROMPSTRUCT, _
                                                 ByVal dwFlags As Integer, _
                                                 ByRef pDataOut As DATA_BLOB) As Boolean
        End Function

        <DllImport("Crypt32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Private Shared Function CryptUnprotectData(ByRef pDataIn As DATA_BLOB, _
                                                 ByVal szDataDescr As String, _
                                                 ByRef pOptionalEntropy As DATA_BLOB, _
                                                 ByVal pvReserved As IntPtr, _
                                                 ByRef pPromptStruct As CRYPTPROTECT_PROMPSTRUCT, _
                                                 ByVal dwFlags As Integer, _
                                                 ByRef pDataOut As DATA_BLOB) As Integer

        End Function

        <DllImport("Kernel32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function FormatMessage(ByVal dwFlags As Integer, _
                                    ByRef lpSource As IntPtr, _
                                    ByVal dwMessageId As Integer, _
                                    ByVal dwLanguageId As Integer, _
                                    ByRef lpBuffer As String, _
                                    ByVal nSize As Integer, _
                                    ByVal Arguments As IntPtr) As Integer
        End Function

        <DllImport("kernel32")> _
        Public Shared Function LocalFree(ByVal hMem As IntPtr) As IntPtr
        End Function

        Public Enum EStore As Byte
            USE_MACHINE_STORE = 1
            USE_USER_STORE
        End Enum

        Private store As EStore

        Public Sub New(ByVal TempStore As EStore)
            store = TempStore
        End Sub

        Public Sub New()
            store = EStore.USE_MACHINE_STORE
        End Sub

        ''' <summary>
        ''' Tipo de accion a ejecutar
        ''' </summary>
        Public Enum Tipo As Byte
            Encriptacion = 0
            Desencriptacion
        End Enum

        ''' <summary>
        ''' Encripta o desencripta un texto utilizadon DPAPI
        ''' </summary>
        ''' <param name="Realizar">Indica si se realiza encriptación o desencriptación</param>
        ''' <param name="Texto">Texto a encriptar o desencriptar</param>
        ''' <param name="OpcionalEntropia">Entropía utilizada para hacer más 
        ''' seguro cuando la encriptación es por Maquina</param>
        ''' <returns>Texto encriptado o desencriptado</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [21/10/2004]       Creado
        ''' </history>
        Public Function Generar(ByVal Realizar As Tipo, ByVal Texto As String, ByVal OpcionalEntropia As String) As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                DESCRIPCION DE LAS VARIABLES LOCALES
            ' retValor : Valor de retorno del metodo
            ' stcEstructura_1, stcEstructura_1, stcEntropiaBlob : Estructuras de datos BLOB
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Aclaración: este código fue traducido del lenguaje C# a VB.NET
            'a partir de los artículos publicados por Microsoft en el 
            'Centro de Arquitectura, página: Crear Aplicaciones ASP.NET Seguras:
            'http://www.microsoft.com/spanish/msdn/arquitectura/aplic_sec.asp

            Dim retValor As Boolean = False
            Dim stcEstructura_1 As DATA_BLOB = New DATA_BLOB
            Dim stcEstructura_2 As DATA_BLOB = New DATA_BLOB
            Dim stcEntropiaBlob As DATA_BLOB = New DATA_BLOB
            Dim Prompt As CRYPTPROTECT_PROMPSTRUCT = New CRYPTPROTECT_PROMPSTRUCT
            Dim dwFlags As Integer
            Dim bytTexto() As Byte
            Dim bytEntropia() As Byte = Nothing

            If Realizar = Tipo.Encriptacion Then
                bytTexto = System.Text.Encoding.UTF8.GetBytes(Texto)
            Else
                bytTexto = System.Convert.FromBase64String(Texto)
            End If

            If Not OpcionalEntropia Is Nothing Then bytEntropia = System.Text.UTF8Encoding.UTF8.GetBytes(OpcionalEntropia)

            InitPromptStruct(Prompt)

            Try
                Try
                    Dim bytesSize As Integer = bytTexto.Length

                    stcEstructura_1.cbData = bytesSize
                    stcEstructura_1.pbData = Marshal.AllocHGlobal(bytesSize)
                    If IntPtr.Zero.Equals(stcEstructura_1.pbData) Then
                        Throw New Exception("Unable to allocate " & Realizar.ToString & " buffer.")
                    End If
                    Marshal.Copy(bytTexto, 0, stcEstructura_1.pbData, bytesSize)

                Catch ex As Exception
                    Throw New Exception("Exception Marshalling data. " & ex.Message)
                End Try
                'Si se usa encriptación por máquina debería proveerse 
                'el parámetro de entropía
                If store = EStore.USE_MACHINE_STORE Then
                    dwFlags = CRYPTPROTECT_LOCAL_MACHINE Or CRYPTPROTECT_UI_FORBIDEN
                    'Verifica si no se recibio el valor de entropia
                    If bytEntropia Is Nothing Then bytEntropia = New Byte(0) {}
                    Try
                        Dim bytesSize As Integer = bytEntropia.Length

                        stcEntropiaBlob.cbData = bytesSize
                        stcEntropiaBlob.pbData = Marshal.AllocHGlobal(bytesSize)
                        If IntPtr.Zero.Equals(stcEntropiaBlob.pbData) Then
                            Throw New Exception("Unable to allocate entropy data buffer.")
                        End If
                        Marshal.Copy(bytEntropia, 0, stcEntropiaBlob.pbData, bytesSize)

                    Catch ex As Exception
                        Throw New Exception("Exception Entropy Marshalling data. " & ex.Message)
                    End Try
                Else
                    'Si se usa el almacén por usuario
                    dwFlags = CRYPTPROTECT_UI_FORBIDEN
                End If
                If Realizar = Tipo.Encriptacion Then
                    retValor = CryptProtectData(stcEstructura_1, "", stcEntropiaBlob, IntPtr.Zero, Prompt, dwFlags, stcEstructura_2)
                Else
                    retValor = CryptUnprotectData(stcEstructura_1, Nothing, stcEntropiaBlob, IntPtr.Zero, Prompt, dwFlags, stcEstructura_2) = 1
                End If

                If retValor.Equals(False) Then
                    Throw New Exception(Realizar.ToString & " failed " & GetErrorMessage(Marshal.GetLastWin32Error))
                End If
                'Libera el Blob y la entropía
                If Not stcEstructura_1.pbData.Equals(IntPtr.Zero) Then
                    Marshal.FreeHGlobal(stcEstructura_1.pbData)
                End If
                If Not stcEntropiaBlob.pbData.Equals(IntPtr.Zero) Then
                    Marshal.FreeHGlobal(stcEntropiaBlob.pbData)
                End If

            Catch ex As Exception
                Throw New Exception("Exception " & Realizar.ToString & ". " & ex.Message)
            End Try

            Dim Resultado(stcEstructura_2.cbData - 1) As Byte
            Dim Retornar As String

            Marshal.Copy(stcEstructura_2.pbData, Resultado, 0, stcEstructura_2.cbData)
            LocalFree(stcEstructura_2.pbData)

            If Realizar = Tipo.Encriptacion Then
                Retornar = System.Convert.ToBase64String(Resultado)
            Else
                Retornar = System.Text.Encoding.UTF8.GetString(Resultado)
            End If

            Return Retornar

        End Function

        ''' <summary>
        ''' Obtiene un mensaje formateado
        ''' </summary>
        ''' <param name="ErrorCode">Codigo del mensaje</param>
        ''' <returns>Mensaje formateado</returns>
        ''' <remarks></remarks>
        Private Function GetErrorMessage(ByVal ErrorCode As Integer) As String
            Const FORMAT_MESSAGE_ALLOCATE_BUFFER As Integer = &H100
            Const FORMAT_MESSAGE_IGNORE_INSERTS As Integer = &H200
            Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
            Dim MessageSize As Integer = 255
            Dim lpMsgBuf As String = ""

            Dim dwFlags As Integer = FORMAT_MESSAGE_ALLOCATE_BUFFER Or _
                                    FORMAT_MESSAGE_FROM_SYSTEM Or _
                                    FORMAT_MESSAGE_IGNORE_INSERTS
            Dim ptrlpSource As IntPtr = New IntPtr
            Dim ptrArguments As IntPtr = New IntPtr

            'http://msdn.microsoft.com/en-us/library/ms679351.aspx
            Dim retValor As Integer = FormatMessage(dwFlags, ptrlpSource, ErrorCode, 0, lpMsgBuf, MessageSize, ptrArguments)

            If retValor = 0 Then
                Throw New Exception("Failed to format message for error code " & ErrorCode.ToString & ".")
            End If

            Return lpMsgBuf

        End Function

        ''' <summary>
        ''' Inicializa la estructura CRYPTPROTECT_PROMPSTRUCT
        ''' </summary>
        ''' <param name="ps">Estructura de tipo CRYPTPROTECT_PROMPSTRUCT</param>
        ''' <remarks></remarks>
        Private Sub InitPromptStruct(ByRef ps As CRYPTPROTECT_PROMPSTRUCT)
            ps.cbSize = Marshal.SizeOf(GetType(CRYPTPROTECT_PROMPSTRUCT))
            ps.dwPromptFlags = 0
            ps.hwndApp = NullPtr
            ps.szPrompt = Nothing
        End Sub

    End Class

End Namespace