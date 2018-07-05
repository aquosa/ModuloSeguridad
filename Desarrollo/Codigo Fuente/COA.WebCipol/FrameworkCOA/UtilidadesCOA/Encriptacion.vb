Imports System.Security.Cryptography
Imports System.IO

Namespace CifrarDatos

    ''' <summary>
    ''' Tipo de algoritmo de encriptacion
    ''' </summary>
    Public Enum TipoHash As Byte
        SHA1 = 0
        MD5
    End Enum

    ''' <summary>
    ''' Crea el hash de acuerdo al algoritmo utilizado
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Hash

        ''' <summary>
        ''' Crea el Hash utilizando el algoritmo establecido
        ''' </summary>
        ''' <param name="Tipo">Tipo de algoritmo utilizado para encriptar</param>
        ''' <param name="Datos">Datos a encriptar</param>
        ''' <param name="Salto">Salta que se concatena a los datos a encriptar</param>
        ''' <returns>Clave hash que representa el string de entrada</returns>
        ''' <remarks></remarks>
        Public Shared Function CrearHash(ByVal Tipo As TipoHash, ByVal Datos As String, Optional ByVal Salto As String = "") As String
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '               DESCRIPCION DE VARIABLES LOCALES
            ' bytValor : Bytes que representan los datos a encriptar
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim bytValor() As Byte

            bytValor = System.Text.Encoding.UTF8.GetBytes(System.String.Concat(Datos, Salto))
            If Tipo = TipoHash.SHA1 Then
                Dim objSHA As New SHA1CryptoServiceProvider

                bytValor = objSHA.ComputeHash(bytValor)
                objSHA.Clear()
            Else
                Dim objMD5 As New MD5CryptoServiceProvider

                bytValor = objMD5.ComputeHash(bytValor)
                objMD5.Clear()
            End If

            Return System.Convert.ToBase64String(bytValor)

        End Function
    End Class

    ''' <summary>
    ''' Determina la accion de encriptación o desencriptación
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Accion
        Encriptacion
        Desencriptacion
    End Enum

    ''' <summary>
    ''' Permite administrar la encriptacion mediante 3DES
    ''' </summary>
    Public Class TresDES
        Public Sub New()

        End Sub

        Public Sub New(ByVal pstrIV As String, ByVal pstrKey As String)
            Me.Key = pstrKey
            Me.IV = pstrIV
        End Sub

        Private mstrIV() As Byte
        Private mstrKey() As Byte

        ''' <summary>
        ''' Obtiene o establece la Llave principal. Información utilizada para encriptar y desencriptar
        ''' </summary>
        ''' <value>Llave principal. Información utilizada para encriptar y desencriptar</value>
        ''' <returns>Llave principal. Información utilizada para encriptar y desencriptar</returns>
        ''' <remarks></remarks>
        Public Property Key() As String
            Get
                Return System.Convert.ToBase64String(mstrKey)
            End Get
            Set(ByVal Value As String)
                mstrKey = System.Convert.FromBase64String(Value)
            End Set
        End Property

        ''' <summary>
        ''' Obtiene o establece el Vector de inicialización: se utiliza para empezar cada proceso de encriptación.
        ''' </summary>
        ''' <value>Vector de inicialización: se utiliza para empezar cada proceso de encriptación.</value>
        ''' <returns>Vector de inicialización: se utiliza para empezar cada proceso de encriptación.</returns>
        ''' <remarks></remarks>
        Public Property IV() As String
            Get
                Return System.Convert.ToBase64String(mstrIV)
            End Get
            Set(ByVal Value As String)
                mstrIV = System.Convert.FromBase64String(Value)
            End Set
        End Property

        ''' <summary>
        ''' Encripta una valor a través de 3DES
        ''' </summary>
        ''' <param name="Realizar">Accion a realizar: encriptar o desencriptar </param>
        ''' <param name="Valor">Cadena que se desea encriptar, desencriptar</param>
        ''' <returns>Cadena encriptada o desencriptada</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [GustavoM]          [20/05/2004]       Creado
        ''' </history>
        Public Function Criptografia(ByVal Realizar As Accion, ByVal Valor As String) As String
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                  DESCRIPCION DE VARIABLES LOCALES
            'secTresDes : Objeto de algoritmo 3 DES Provider
            'iEncript   : Interfase de encriptación
            'objMemoria : Objeto Memory Stream
            'objCrypto  : Objeto CryptoStream
            'bytValor   : Valor a encriptar o desencriptar
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim secTresDes As System.Security.Cryptography.TripleDESCryptoServiceProvider
            Dim iEncript As System.Security.Cryptography.ICryptoTransform
            Dim objMemoria As New System.IO.MemoryStream, objCrypto As System.Security.Cryptography.CryptoStream
            Dim bytValor() As Byte

            secTresDes = New System.Security.Cryptography.TripleDESCryptoServiceProvider
            If mstrIV Is Nothing Then
                'Si se trata de una desencriptacion
                If Realizar = Accion.Desencriptacion Then
                    Throw New Exception("Se debe especificar un Vector de Inicialización")
                Else
                    mstrIV = secTresDes.IV
                End If
            Else
                secTresDes.IV = mstrIV
            End If

            If mstrKey Is Nothing Then
                'Si se trata de una desencriptacion
                If Realizar = Accion.Desencriptacion Then
                    Throw New Exception("Se debe especificar un Vector de Inicialización")
                Else
                    mstrKey = secTresDes.Key
                End If
            Else
                If mstrKey.Equals(String.Empty) Then
                    Throw New Exception("Se debe establecer una clave.")
                Else
                    If Not (mstrKey.Length.Equals(16) OrElse mstrKey.Length.Equals(24)) Then
                        Throw New Exception("La longitud de la clave debe ser de 128 o 192 bits")
                    End If
                End If
                secTresDes.Key = mstrKey
            End If
            If Realizar = Accion.Encriptacion Then
                bytValor = System.Text.Encoding.UTF8.GetBytes(Valor)
                iEncript = secTresDes.CreateEncryptor
            Else
                Valor = Valor.Replace("#", "+")
                bytValor = System.Convert.FromBase64String(Valor)
                iEncript = secTresDes.CreateDecryptor
            End If

            objCrypto = New System.Security.Cryptography.CryptoStream(objMemoria, iEncript, Security.Cryptography.CryptoStreamMode.Write)
            objCrypto.Write(bytValor, 0, bytValor.Length)
            objCrypto.FlushFinalBlock()
            objCrypto.Close()

            If Realizar = Accion.Encriptacion Then
                Return System.Convert.ToBase64String(objMemoria.ToArray()).Replace("+", "#")    'Por problemas con QueryString en Paginas Web
            Else
                Return System.Text.Encoding.UTF8.GetString(objMemoria.ToArray())
            End If

        End Function
    End Class

End Namespace