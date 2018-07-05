Namespace Controles
    Public Class DataGridViewCoa
        Inherits System.Windows.Forms.DataGridView

        Public Sub New()
            Me.ReadOnly = True
            Me.AllowUserToAddRows = False
            Me.AllowUserToDeleteRows = False
        End Sub
    End Class
End Namespace