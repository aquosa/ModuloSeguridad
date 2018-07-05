Option Strict On
Option Explicit On

Imports System.Convert
Public Class Areas
	Inherits PadreSistema

    ''' <history>
    ''' [AndresR]          [lunes, 30 de junio de 2008]       Modificado Se corrige consulta SQL de nvl a isnull para SQLServer
    ''' </history>
    Public Function Insertar(ByVal pDataset As System.Data.DataSet, ByVal nroLinea As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql			: script de consulta sql
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim sblSql As New System.Text.StringBuilder()

        Dim intIDArea As Integer = 0

        If objConexion.ConectadoA = "Oracle" Then
            intIDArea = ToInt32(objConexion.EjecutarEscalar("select nvl(max(idarea),0)+1 from sist_kareas"))
        ElseIf objConexion.ConectadoA = "SQLServer" Then
            intIDArea = ToInt32(objConexion.EjecutarEscalar("select isnull(max(idarea),0)+1 from sist_kareas"))
        End If

        With pDataset.Tables("SIST_KAREAS").Rows(nroLinea)
            sblSql.Append(" Insert into SIST_KAREAS( ")
            sblSql.Append("IDAREA, NOMBREAREA, RESPONSABLE, CARGORESPONSABLE, COMENTARIOS, BAJA, FICTICIA ) Values (")

            sblSql.Append(intIDArea & " ,")

            sblSql.Append(objConexion.XtoStr(.Item("NOMBREAREA").ToString()) + ",")
            If .IsNull("RESPONSABLE") Then
                sblSql.Append(" null, ")
            Else
                sblSql.Append(objConexion.XtoStr(.Item("RESPONSABLE").ToString()) + ",")
            End If
            If .IsNull("CARGORESPONSABLE") Then
                sblSql.Append(" null, ")
            Else
                sblSql.Append(objConexion.XtoStr(.Item("CARGORESPONSABLE").ToString()) + ",")
            End If
            If .IsNull("COMENTARIOS") Then
                sblSql.Append(" null, ")
            Else
                sblSql.Append(objConexion.XtoStr(.Item("COMENTARIOS").ToString()) + ",")
            End If
            sblSql.Append(objConexion.XtoStr(.Item("BAJA").ToString()) + ",")
            If .IsNull("FICTICIA") Then
                sblSql.Append(" null) ")
            Else
                sblSql.Append(objConexion.XtoStr(.Item("FICTICIA").ToString()) + ")")
            End If

        End With
        objConexion.Ejecutar(sblSql.ToString())

        Return intIDArea

    End Function

	Public Function Actualizar(ByVal pDataset As System.Data.DataSet, ByVal nroLinea As Int32) As Int32
		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		'                DESCRIPCION DE LAS VARIABLES LOCALES
		' sblSql			: script de consulta sql
		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		Dim sblSql As New System.Text.StringBuilder()
		With pDataset.tables("SIST_KAREAS").rows(nroLinea)
			sblSql.Append(" UPDATE SIST_KAREAS SET ")
			If .IsNull("NOMBREAREA") Then
				sblSql.Append(" NOMBREAREA = NULL,")
			Else
				sblSql.Append(" NOMBREAREA = " + objConexion.XtoStr(.Item("NOMBREAREA").ToString()) + ",  ")
			End If
			If .isnull("RESPONSABLE") Then
				sblSql.Append(" RESPONSABLE = NULL,")
			Else
				sblSql.Append(" RESPONSABLE = " + objConexion.xtostr(.item("RESPONSABLE").ToString()) + ",  ")
			End If
			If .isnull("CARGORESPONSABLE") Then
				sblSql.Append(" CARGORESPONSABLE = NULL,")
			Else
				sblSql.Append(" CARGORESPONSABLE = " + objConexion.xtostr(.item("CARGORESPONSABLE").ToString()) + ",  ")
			End If
			If .isnull("COMENTARIOS") Then
				sblSql.Append(" COMENTARIOS = NULL,")
			Else
				sblSql.Append(" COMENTARIOS = " + objConexion.xtostr(.item("COMENTARIOS").ToString()) + ",  ")
			End If
			If .isnull("BAJA") Then
				sblSql.Append(" BAJA = NULL,")
			Else
				sblSql.Append(" BAJA = " + objConexion.xtostr(.item("BAJA").ToString()) + ",  ")
			End If
			If .isnull("FICTICIA") Then
				sblSql.Append(" FICTICIA = NULL ")
			Else
				sblSql.Append(" FICTICIA = " + objConexion.xtostr(.item("FICTICIA").ToString()))
			End If

			sblsql.append(" WHERE ")
			sblSql.Append("  IDAREA = " + objConexion.XtoStr(ToDecimal(.Item("IDAREA"))))
		End With
		Return objconexion.Ejecutar(sblSql.ToString())
	End Function

	Public Function Recuperar(ByRef pdtsDataset As System.Data.DataSet, ByVal pstrNombreTabla As String) As Int32
		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		'                DESCRIPCION DE LAS VARIABLES LOCALES
		' sblSql			: script de consulta sql
		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		Dim sblSql As New System.Text.StringBuilder()
        sblSql.Append(" SELECT * FROM  SIST_KAREAS ")
		Return objConexion.Ejecutar(sblSql.ToString(), pdtsDataset, pstrNombreTabla)
	End Function

	Public Function Eliminar(ByVal pidArea As Int32) As Int32
		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		'                DESCRIPCION DE LAS VARIABLES LOCALES
		' sblSql			: script de consulta sql
		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		Dim sblSql As New System.Text.StringBuilder()
		sblSql.Append(" UPDATE SIST_KAREAS SET ")
		sblSql.Append(" BAJA = 'S'")
		sblSql.Append(" WHERE ")
		sblSql.Append("  IDAREA = " + objConexion.XtoStr(pidArea))

		Return objConexion.Ejecutar(sblSql.ToString())
    End Function

    Public Function Agregar(ByVal pidArea As Int32) As Int32
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                DESCRIPCION DE LAS VARIABLES LOCALES
        ' sblSql			: script de consulta sql
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '[IvanR]         [Viernes, 11 de junio del 2010] Creado GCP-Cambio ID:9098
        Dim sblSql As New System.Text.StringBuilder()
        sblSql.Append(" UPDATE SIST_KAREAS SET ")
        sblSql.Append(" BAJA = 'N'")
        sblSql.Append(" WHERE ")
        sblSql.Append("  IDAREA = " + objConexion.XtoStr(pidArea))

        Return objConexion.Ejecutar(sblSql.ToString())
    End Function

End Class
