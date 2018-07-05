Namespace Controles
    Public Class DateTimePickerCoa
        Inherits System.Windows.Forms.DateTimePicker

        Public Sub New()
            Me.Format = Windows.Forms.DateTimePickerFormat.Custom
            Me.CustomFormat = "dd/MM/yyyy"
        End Sub
    End Class
End Namespace
