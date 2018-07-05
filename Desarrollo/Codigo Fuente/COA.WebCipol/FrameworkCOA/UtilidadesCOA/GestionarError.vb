''' <summary>
''' Clase que se invoca cuando ocurre un error, se cargan los datos del mismo 
''' en una estructura y se registran.
''' </summary>
''' <remarks></remarks>
Public Class GestionarError

    ''' <summary>
    ''' Estructura con informacion del error
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure gtypError
        Public FechaHora As DateTime
        Public Clase As String
        Public NumeroError As Integer
        Public Mensaje As String
        Public Sistema As String
        Public SQL As String
    End Structure

    ''' <summary>
    ''' Graba en un destino Xml tomado de web.config.
    ''' </summary>
    ''' <param name="PathArchivo">Path del archivo a grabar</param>
    ''' <param name="udtError">Estructura con informacion del error</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [DanielD]          [03/06/2004]       Creado
    ''' </history>
    Public Shared Function GrabarXml(ByVal PathArchivo As String, ByVal udtError As gtypError) As Integer
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        'strPathLog           : Path del archivo
        'objXmlWrite          : Objeto escritor del archivo XML
        'xmlLogInterrupciones : Objeto de documento XML
        'xndSucceso           : Nodo que representa el suceso
        'xatFechaHora         : Fecha y hora del suceso
        'catSistema           : Atributo con el sistema Sistema
        'xatClase             : Clase
        'xndNumeroError       : Nodo del numero de error
        'xndMensaje           : Nodo del mensaje Mensaje
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strPathLog As String
        Dim objXmlWrite As Xml.XmlTextWriter = Nothing

        strPathLog = PathArchivo

        ' Creo documento xml
        Dim xmlLogInterrupciones As System.Xml.XmlDocument = New System.Xml.XmlDocument

        ' El archivo debe existir, esta es su forma inicial:
        ' <?xml version="1.0"?>
        '<LogInterrupciones>
        '</LogInterrupciones>
        If Not System.IO.File.Exists(strPathLog) Then
            Dim xmlCrearArchivo As New System.Xml.XmlTextWriter(strPathLog, Nothing)

            With xmlCrearArchivo
                .WriteStartDocument()
                .WriteStartElement("LogErrores")
                .WriteEndElement()
                .WriteEndDocument()
                .Close()
            End With
        End If

        xmlLogInterrupciones.Load(strPathLog)

        ' Creo nodo Succeso
        Dim xndSucceso As System.Xml.XmlNode

        xndSucceso = xmlLogInterrupciones.CreateNode(System.Xml.XmlNodeType.Element, "Succeso", "")

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Agrego atributos al nodo Succeso
        Dim xatFechaHora As System.Xml.XmlAttribute = xmlLogInterrupciones.CreateAttribute("FechaHora")

        xatFechaHora.Value = Format(udtError.FechaHora, "dd/MM/yyyy hh:mm:ss")
        xndSucceso.Attributes.Append(xatFechaHora)

        Dim catSistema As System.Xml.XmlAttribute = xmlLogInterrupciones.CreateAttribute("Sistema")

        catSistema.Value = udtError.Sistema
        xndSucceso.Attributes.Append(catSistema)

        Dim xatClase As System.Xml.XmlAttribute = xmlLogInterrupciones.CreateAttribute("Clase")

        xatClase.Value = udtError.Clase
        xndSucceso.Attributes.Append(xatClase)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ' Agrega al documento xml xmlLogInterrupciones el nodo Succeso
        xmlLogInterrupciones.DocumentElement.AppendChild(xndSucceso)

        ' Crea el nodo NumeroError
        Dim xndNumeroError As System.Xml.XmlNode

        xndNumeroError = xmlLogInterrupciones.CreateNode(System.Xml.XmlNodeType.Element, "NumeroError", "")

        xndNumeroError.InnerText = CType(udtError.NumeroError, String)
        xndSucceso.AppendChild(xndNumeroError) ' Agrega al nodo Succeso el hijo xndNumeroError

        ' Crea el nodo Mensaje
        Dim xndMensaje As System.Xml.XmlNode

        xndMensaje = xmlLogInterrupciones.CreateNode(System.Xml.XmlNodeType.Element, "Mensaje", "")

        xndMensaje.InnerText = udtError.Mensaje
        xndSucceso.AppendChild(xndMensaje) ' Agrega al nodo Succeso el hijo xndMensaje

        If Not udtError.SQL.Equals(Nothing) Then
            Dim xndSql As System.Xml.XmlNode

            xndSql = xmlLogInterrupciones.CreateNode(System.Xml.XmlNodeType.Element, "SentSql", "")

            xndSql.InnerText = udtError.SQL
            xndSucceso.AppendChild(xndSql) ' Agrega al nodo Succeso el hijo xndSql
        End If

        xmlLogInterrupciones.Save(strPathLog)

    End Function

    ''' <summary>
    ''' Permite grabar un mensaje en el log del SO
    ''' </summary>
    ''' <param name="Registro">Registro del error</param>
    ''' <param name="udtError">Estructura con informacion del error</param>
    Public Shared Function GrabarLogSO(ByVal Registro As String, ByVal udtError As gtypError) As Integer
        ' Create an EventLog instance and assign its source.
        'Dim myLog As New EventLog("myNewLog", ".", "MySource")
        ' Create the source, if it does not already exist.
        Dim myLog As New EventLog, strMensaje As String

        strMensaje = "Sistema: " & udtError.Sistema & vbCrLf & "Clase: " & udtError.Clase & vbCrLf & "Error: " & udtError.Mensaje

        ' Write an entry to the log.        
        EventLog.WriteEntry(Registro, strMensaje)

    End Function

End Class