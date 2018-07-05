Public Class Sesion


    Private Function Recuperar(ByVal nombre As String) As Object
#If Not StandAlone Then
        Try
            Return System.Web.HttpContext.Current.Session(nombre)
        Catch ex As Exception
            Return Nothing
        End Try
#Else
        Try
            Return _collection(nombre)
        Catch ex As Exception
            Return Nothing
        End Try
#End If

    End Function

    Private Sub Guardar(ByVal nombre As String, ByVal valor As Object)
#If Not StandAlone Then
        System.Web.HttpContext.Current.Session(nombre) = valor
#Else
        If _collection.ContainsKey(nombre) Then
            _collection(nombre) = valor
        Else
            _collection.Add(nombre, valor)
        End If

#End If
    End Sub

    Default Public Property Item(ByVal nombre As String) As Object
        Get
            Return Recuperar(nombre)
        End Get
        Set(ByVal value As Object)
            Guardar(nombre, value)
        End Set
    End Property

    Public Sub Remove(ByVal nombre As String)
#If Not StandAlone Then
        System.Web.HttpContext.Current.Session.Remove(nombre)
#Else
        _collection.Remove(nombre)
#End If
    End Sub

    Public Sub Abandon()
#If Not StandAlone Then
        System.Web.HttpContext.Current.Session.Abandon()
#Else
        _collection.Clear()
#End If
    End Sub


    Private Shared _instance As Sesion

    Private _collection As Generic.Dictionary(Of String, Object)

    Public Shared ReadOnly Property getInstance() As Sesion
        Get
            If _instance Is Nothing Then
                _instance = New Sesion()

            End If

            Return _instance
        End Get
    End Property

    Protected Sub New()
#If StandAlone Then
        'standalone -> crea la collection que emula al objeto Session
        _collection = New Generic.Dictionary(Of String, Object)
#End If
    End Sub

    Public Class IP
        ''' <summary>
        ''' Obtiene el IP del cliente conectado
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [LucianoP]          [miércoles, 5 de abril de 2017]    Creado 
        ''' </history>
        Public Shared Function GetIPAddress() As String
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current()
            Dim sIPAddress As String

            sIPAddress = context.Request.ServerVariables("HTTP_X_CLUSTER_CLIENT_IP")
            If String.IsNullOrEmpty(sIPAddress) Then
                sIPAddress = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                If String.IsNullOrEmpty(sIPAddress) Then
                    sIPAddress = context.Request.ServerVariables("REMOTE_ADDR")
                Else
                    Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                    sIPAddress = ipArray(0)
                End If
            End If
            Return sIPAddress
        End Function
    End Class

End Class
