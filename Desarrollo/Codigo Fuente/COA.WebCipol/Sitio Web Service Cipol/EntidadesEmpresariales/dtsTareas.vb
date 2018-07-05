

Partial Public Class dtsTareas
    Partial Class SE_SIST_HABILITADOSDataTable

        Private Sub SE_SIST_HABILITADOSDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.IDCODSISTEMAColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class TAREASDataTable

        Private Sub TAREASDataTable_TAREASRowChanging(ByVal sender As System.Object, ByVal e As TAREASRowChangeEvent) Handles Me.TAREASRowChanging

        End Sub

    End Class

End Class
