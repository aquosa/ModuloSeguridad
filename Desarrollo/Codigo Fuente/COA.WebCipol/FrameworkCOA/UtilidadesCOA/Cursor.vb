''' <summary>
''' Permite administrar el Cursor mostrado al cliente en la aplicación
''' </summary>
Public Class Cursor
    Private Shared curActual As System.Windows.Forms.Cursor = Windows.Forms.Cursors.Default

    ''' <summary>
    ''' Muestra el tipo de cursor establecido
    ''' </summary>
    ''' <param name="Tipo">Tipo de cursor a mostrar en la aplicación</param>
    ''' <remarks></remarks>
    Public Shared Sub Mostrar(ByVal Tipo As System.Windows.Forms.Cursor)
        curActual = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = Tipo
    End Sub

    ''' <summary>
    ''' Restaura el tipo del Cursor al valor que tenia antes de ser mostrado
    ''' mediante el método Mostrar
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Restaurar()
        System.Windows.Forms.Cursor.Current = curActual
    End Sub

End Class
