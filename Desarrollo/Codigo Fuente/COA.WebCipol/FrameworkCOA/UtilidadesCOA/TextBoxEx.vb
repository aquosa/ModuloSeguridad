Option Strict On
Option Explicit On

Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Control TextBox extendido
''' </summary>
Public Class TextBoxEx

    Private mblnEnabled As Boolean = True
    Private mBackColorDisabled As Color = SystemColors.ButtonFace
    Private mForeColorDisabled As Color = Color.Black

    ''' <summary>
    ''' Obtiene o establece el color de fondo que tendra el control deshabilitado
    ''' </summary>
    ''' <value>Color de fondo que tendra el control deshabilitado</value>
    ''' <returns>Color de fondo que tendra el control deshabilitado</returns>
    ''' <history>
    ''' [Marinol]            [jueves, 8 de noviembre de 2006]        Creado
    ''' </history>
    Public Property BackColorDisabled() As System.Drawing.Color
        Get
            Return mBackColorDisabled
        End Get
        Set(ByVal Value As System.Drawing.Color)
            mBackColorDisabled = Value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el color de la letra que tendra el control deshabilitado
    ''' </summary>
    ''' <value>Color de la letra que tendra el control deshabilitado</value>
    ''' <returns>Color de la letra que tendra el control deshabilitado</returns>
    ''' <history>
    ''' [Marinol]            [jueves, 8 de noviembre de 2006]        Creado
    ''' </history>
    Public Property ForeColorDisabled() As System.Drawing.Color
        Get
            Return mForeColorDisabled
        End Get
        Set(ByVal Value As System.Drawing.Color)
            mForeColorDisabled = Value
        End Set
    End Property

    ''' <summary>
    ''' Evento que se produce cuando el control cambia su propiedad Enabled
    ''' </summary>
    ''' <param name="e">Informacion del evento</param>
    Protected Overrides Sub OnEnabledChanged(ByVal e As System.EventArgs)
        MyBase.OnEnabledChanged(e)
        If Not Me.Enabled Then
            Me.SetStyle(ControlStyles.UserPaint, True)
        Else
            Me.SetStyle(ControlStyles.UserPaint, False)
        End If

        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' Evento Paint del control
    ''' </summary>
    ''' <param name="e">Informacion del evento</param>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '               DESCRIPCION DE VARIABLES LOCALES
        ' TextBrush : Brush utilizado para dibujar el rectangulo
        ' sf        : Se utiliza para formatear el string
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        MyBase.OnPaint(e)
        Dim TextBrush As SolidBrush
        Dim sf As New StringFormat

        sf.LineAlignment = StringAlignment.Center
        sf.FormatFlags = StringFormatFlags.NoClip
        Select Case Me.TextAlign
            Case HorizontalAlignment.Center
                sf.Alignment = StringAlignment.Center
            Case HorizontalAlignment.Left
                sf.Alignment = StringAlignment.Near
            Case HorizontalAlignment.Right
                sf.Alignment = StringAlignment.Far
        End Select

        Dim rDraw As RectangleF = RectangleF.op_Implicit(Me.ClientRectangle)
        rDraw.Inflate(0, 0)

        If Me.Enabled Then
            TextBrush = New SolidBrush(Me.ForeColor)
        Else
            TextBrush = New SolidBrush(Me.ForeColorDisabled)
            Dim BackBrush As New SolidBrush(Me.BackColorDisabled)
            e.Graphics.FillRectangle(BackBrush, 0.0F, 0.0F, Me.Width, Me.Height)
        End If

        e.Graphics.DrawString(Me.Text, Me.Font, TextBrush, rDraw, sf)

        sf.Dispose()
        TextBrush.Dispose()
    End Sub

End Class