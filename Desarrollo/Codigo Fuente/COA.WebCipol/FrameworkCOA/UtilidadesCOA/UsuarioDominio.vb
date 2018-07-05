Imports System.DirectoryServices

Namespace SO_Windows
    ''' <summary>
    ''' Clase que permite interactuar con el Active Directory
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gustavom]            [02/02/2009]        GCP-Cambios ID: 7750
    ''' </history>
    Public Class ActiveDirectoryUser
        Private mstrNombre As String
        Private mobjItems As New System.Collections.Generic.List(Of String)
        Private mstrNombreCompleto As String
        Private mstrID As String
        Private mintTipo As TipoObjeto
        Private mintBuscar As TipoAtributo
        Private mstrDescripcion As String = ""

        Private mstrLdap As String

        Public Enum TipoObjeto As Integer
            Usuario
            Grupo
        End Enum

        Public Enum TipoAtributo As Integer
            CommonName
            SamAccount
        End Enum

        Public Sub New(ByVal Nombre As String, ByVal Dominio As String, ByVal Tipo As TipoObjeto, ByVal BuscarPor As TipoAtributo)
            Dim strDom As String = GetDominioLdap(Dominio)

            mstrLdap = "LDAP://" + strDom
            mstrNombre = Nombre
            mintTipo = Tipo
            mintBuscar = BuscarPor

        End Sub

        Public Sub New(ByVal Nombre As String, ByVal Dominio As String, ByVal Tipo As TipoObjeto, ByVal BuscarPor As TipoAtributo, ByVal DirectoryServer As String)
            Dim strDom As String = GetDominioLdap(Dominio)

            mstrLdap = "LDAP://" + DirectoryServer + "/" + strDom
            mstrNombre = Nombre
            mintTipo = Tipo
            mintBuscar = BuscarPor

        End Sub

        Public Sub New(ByVal Nombre As String, ByVal Dominio As String, ByVal Tipo As TipoObjeto, ByVal BuscarPor As TipoAtributo, ByVal DirectoryServer As String, ByVal Puerto As Integer)
            Dim strDom As String = GetDominioLdap(Dominio)

            mstrLdap = "LDAP://" + DirectoryServer + ":" + Puerto.ToString() + "/" + strDom
            mstrNombre = Nombre
            mintTipo = Tipo
            mintBuscar = BuscarPor

        End Sub

        ''' <summary>
        ''' Formatea los datos del dominio de acuerdo a LDAP
        ''' </summary>
        ''' <param name="Dominio">Nombre de dominio</param>
        ''' <returns>Formato para realizar búsquedas sobre LDAP</returns>
        ''' <remarks></remarks>
        Private Function GetDominioLdap(ByVal Dominio As String) As String
            Dim strDN() As String = Dominio.Split("."c)
            Dim strADPath As String = ""

            For intI As Integer = 0 To strDN.Length - 1
                strADPath += "DC="
                strADPath += strDN(intI)
                strADPath += ","
            Next
            strADPath = strADPath.Substring(0, strADPath.Length - 1)

            Return strADPath

        End Function

        Public Function Buscar() As Boolean
            Dim objAD As New DirectoryEntry(mstrLdap)
            Dim objADSearch As New DirectorySearcher(objAD)
            Dim objResult As SearchResult
            Dim strItem() As String
            Dim strAtributo As String
            Dim strBusqueda As String

            Select Case mintBuscar
                Case TipoAtributo.CommonName
                    strBusqueda = "cn"

                Case TipoAtributo.SamAccount
                    strBusqueda = "samaccountname"

                Case Else
                    Throw New Exception("Error de programación: elemento de enumeración no soportado.")
            End Select

            Select Case mintTipo
                Case TipoObjeto.Grupo
                    objADSearch.Filter = "(&(objectClass=group)(" + strBusqueda + "=" + Me.mstrNombre.Trim() + "))"
                    strAtributo = "member"

                Case TipoObjeto.Usuario
                    objADSearch.Filter = "(&(objectClass=user)(" + strBusqueda + "=" + Me.mstrNombre.Trim() + "))"
                    strAtributo = "memberof"

                Case Else
                    Throw New Exception("Error de programación: elemento de enumeración no soportado.")
            End Select


            '#If DEBUG Then
            '            'Este código debe habilitarse cuando se trabaja en notebook de desarrollo 
            '            'y la misma no se encuentra en el dominio COA.local
            '            Select Case mintTipo
            '                Case TipoObjeto.Grupo
            '                    mstrNombre = "Gpo Bco Chubut Pruebas"
            '                    mstrNombreCompleto = "Gpo Bco Chubut Pruebas"
            '                    mstrID = "S-1-5-21-3370056077-3368664693-1737765451-2887"

            '                    mobjItems.Add("Vanina Gabetta")
            '                    mobjItems.Add("Juan José Orusa")
            '                    mobjItems.Add("Iván Bergonzi")
            '                    mobjItems.Add("Gustavo Mazzaglia")

            '                Case TipoObjeto.Usuario
            '                    If mintBuscar = TipoAtributo.CommonName Then
            '                        Select Case Me.mstrNombre.Trim()
            '                            Case "Vanina Gabetta"
            '                                mstrNombre = "VGabetta"
            '                                mstrNombreCompleto = "Vanina Gabetta"
            '                                mstrID = "S-1-5-21-3370056077-3368664693-1737765451-1252"

            '                            Case "Juan José Orusa"
            '                                mstrNombre = "JOrusa"
            '                                mstrNombreCompleto = "Juan José Orusa"
            '                                mstrID = "S-1-5-21-3370056077-3368664693-1737765451-1250"

            '                            Case "Iván Bergonzi"
            '                                mstrNombre = "IBergonzi"
            '                                mstrNombreCompleto = "Iván Bergonzi"
            '                                mstrID = "S-1-5-21-3370056077-3368664693-1737765451-1254"

            '                            Case "Gustavo Mazzaglia"
            '                                mstrNombre = "Gustavom"
            '                                mstrNombreCompleto = "Gustavo Mazzaglia"
            '                                mstrID = "S-1-5-21-3370056077-3368664693-1737765451-1248"
            '                        End Select
            '                    Else
            '                        mstrNombre = "Gustavom"
            '                        mstrNombreCompleto = "Gustavo Mazzaglia"
            '                        mstrID = "S-1-5-21-3370056077-3368664693-1737765451-1248"
            '                    End If

            '                    mobjItems.Add("P Bco Chubut 2002 D - Sist. Integral - Circuito de Cheques L")
            '                    mobjItems.Add("P Bco Chubut 2002 D - Sist. Integral - Circuito de Cheques E")
            '                    mobjItems.Add("P Bco Hipotecario 2001 D - Modif. Sist. de Roles y Funciones E")

            '            End Select

            '            Return True
            '#End If

            objADSearch.PropertiesToLoad.Add("cn")
            objADSearch.PropertiesToLoad.Add(strAtributo)
            objADSearch.PropertiesToLoad.Add("objectsid")
            objADSearch.PropertiesToLoad.Add("samaccountname")
            If mintTipo = TipoObjeto.Usuario Then
                objADSearch.PropertiesToLoad.Add("description") 'GCP-Cambios ID: 8047
            End If
            objResult = objADSearch.FindOne()
            If objResult Is Nothing Then
                Return False
            End If

            mstrNombre = objResult.Properties("samaccountname")(0).ToString()
            mstrNombreCompleto = objResult.Properties("cn")(0).ToString()
            mstrID = (New System.Security.Principal.SecurityIdentifier(CType(objResult.Properties("objectsid")(0), System.Byte()), 0)).Value
            If mintTipo = TipoObjeto.Usuario Then
                'Puede suceder que la descripción este vacía y active directory no la retorna
                Try
                    mstrDescripcion = objResult.Properties("description")(0).ToString()
                Catch ex As Exception
                    mstrDescripcion = ""
                End Try
            End If

            For intI As Integer = 0 To objResult.Properties(strAtributo).Count - 1
                strItem = objResult.Properties(strAtributo)(intI).ToString().Split(","c)
                For IntJ As Integer = 0 To strItem.Length - 1
                    If strItem(IntJ).ToUpper().StartsWith("CN=") Then
                        mobjItems.Add(strItem(IntJ).Substring(3))
                        Exit For
                    End If
                Next
            Next

            Return True

        End Function


        Public ReadOnly Property PerteneceA() As System.Collections.Generic.List(Of String)
            Get
                Return mobjItems
            End Get
        End Property

        Public ReadOnly Property Nombre() As String
            Get
                Return mstrNombre
            End Get
        End Property

        Public ReadOnly Property NombreCompleto() As String
            Get
                Return mstrNombreCompleto
            End Get
        End Property

        Public ReadOnly Property ID() As String
            Get
                Return mstrID
            End Get
        End Property

        Public ReadOnly Property Descripcion() As String
            Get
                Return mstrDescripcion
            End Get
        End Property

    End Class

    <Serializable()> _
    Public Class UsuarioDominio
        Private Declare Auto Function LogonUser Lib "advapi32.dll" ( _
          ByVal lpszUsername As String, _
          ByVal lpszDomain As String, _
          ByVal lpszPassword As String, _
          ByVal dwLogonType As Integer, _
          ByVal dwLogonProvider As Integer, _
          ByRef phToken As IntPtr) As Boolean

        ' Declare the logon types as constants 
        Private Const LOGON32_LOGON_INTERACTIVE As Long = 2
        Private Const LOGON32_LOGON_NETWORK As Long = 3
        Private Const LOGON32_LOGON_BATCH As Long = 4

        ' Declare the logon providers as constants 
        Private Const LOGON32_PROVIDER_DEFAULT As Long = 0
        Private Const LOGON32_PROVIDER_WINNT50 As Long = 3
        Private Const LOGON32_PROVIDER_WINNT40 As Long = 2
        Private Const LOGON32_PROVIDER_WINNT35 As Long = 1

        Private mobjToken As System.IntPtr = IntPtr.Zero
        Private mobjContexto As System.Security.Principal.WindowsImpersonationContext

        Public Enum TipoAutenticacion
            RecursosLocales
            RecursosRemotos
        End Enum

        Public ReadOnly Property Token_Usuario() As IntPtr
            Get
                Return mobjToken
            End Get
        End Property

        Public Function Autenticar(ByVal Usuario As String, ByVal Clave As String, ByVal Dominio As String, ByVal AccesoRecursos As TipoAutenticacion) As Boolean
            If AccesoRecursos = TipoAutenticacion.RecursosLocales Then
                Return LogonUser(Usuario, Dominio, Clave, LOGON32_LOGON_NETWORK, _
                    LOGON32_PROVIDER_DEFAULT, mobjToken)
            Else
                Return LogonUser(Usuario, Dominio, Clave, LOGON32_LOGON_INTERACTIVE, _
                    LOGON32_PROVIDER_DEFAULT, mobjToken)
            End If
        End Function

        Public Sub Impersonate()
            Dim objUsuario As System.Security.Principal.WindowsIdentity
            If mobjToken <> IntPtr.Zero Then
                objUsuario = New Security.Principal.WindowsIdentity(mobjToken)
                mobjContexto = objUsuario.Impersonate()
            End If
        End Sub

        Public Sub UndoImpersonate()
            If mobjToken <> IntPtr.Zero Then
                mobjContexto.Undo()
            End If
        End Sub

    End Class


End Namespace