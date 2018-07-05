-- Generado por Oracle SQL Developer Data Modeler 4.1.0.881
--   en:        2015-10-15 12:58:34 ART
--   sitio:      SQL Server 2008
--   tipo:      SQL Server 2008




CREATE
  TABLE SE_ATRIBUTOSTAREAS
  (
    IDTAREA   NUMERIC (5) NOT NULL ,
    MOMENTO   NUMERIC (2) NOT NULL ,
    TIPOMONTO NUMERIC (2) ,
    CONSTRAINT PK_SE_ATRIBUTOSTAREAS PRIMARY KEY CLUSTERED (IDTAREA)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Atributos de las tareas' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ATRIBUTOSTAREAS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de la tarea CIPOL' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ATRIBUTOSTAREAS' ,
'COLUMN' ,
'IDTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Momento de la autorización:
0 - al carga la pagina
1 - en el submit
10- en cualquier otro momento
'
,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ATRIBUTOSTAREAS' ,
'COLUMN' ,
'MOMENTO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Importe de la transacción' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ATRIBUTOSTAREAS' ,
'COLUMN' ,
'TIPOMONTO'
GO

CREATE
  TABLE SE_AUDITORIA
  (
    FECHAHORALOG    DATETIME NOT NULL ,
    CODMENSAJE      NUMERIC (5) ,
    TEXTOMENSAJE    VARCHAR (MAX) ,
    USUARIOACTUANTE VARCHAR (50) ,
    USUARIOAFECTADO VARCHAR (50)
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registro de auditorías interrnas de sucesos de seguridad' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha hora del evento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIA' ,
'COLUMN' ,
'FECHAHORALOG'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Código del mensaje' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIA' ,
'COLUMN' ,
'CODMENSAJE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Texto del mensaje' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIA' ,
'COLUMN' ,
'TEXTOMENSAJE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Usuario que efectuo la operación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIA' ,
'COLUMN' ,
'USUARIOACTUANTE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Usuario afectado' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIA' ,
'COLUMN' ,
'USUARIOAFECTADO'
GO

CREATE
  TABLE SE_AUDITORIASUPERVISION
  (
    FECHAHORALOG          DATETIME NOT NULL ,
    IDUSUARIO_SUPERVISOR  NUMERIC (5) NOT NULL ,
    IDUSUARIO_SUPERVISADO NUMERIC (5) NOT NULL ,
    IDTAREA               NUMERIC (5) NOT NULL ,
    TERMINAL              VARCHAR (100)
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registros de Supervisión' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIASUPERVISION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha que se realizo la auditoría.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIASUPERVISION' ,
'COLUMN' ,
'FECHAHORALOG'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID del usuario que superviso la tarea.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIASUPERVISION' ,
'COLUMN' ,
'IDUSUARIO_SUPERVISOR'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID del usuario que requiere supervisión para realizar una accion especifica.'
,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIASUPERVISION' ,
'COLUMN' ,
'IDUSUARIO_SUPERVISADO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de la tarea que se supervisa.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIASUPERVISION' ,
'COLUMN' ,
'IDTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Terminal donde se realiza la supervisión.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUDITORIASUPERVISION' ,
'COLUMN' ,
'TERMINAL'
GO

CREATE
  TABLE SE_AUTORIZACION
  (
    IDAUTORIZACION NUMERIC (5) NOT NULL ,
    CODAUTORIZ     VARCHAR (3) NOT NULL ,
    DESCAUTORIZ    VARCHAR (100) NOT NULL ,
    CONSTRAINT PK_SE_AUTORIZACION PRIMARY KEY CLUSTERED (IDAUTORIZACION)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tipos de autorización de tareas' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUTORIZACION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de la autorización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUTORIZACION' ,
'COLUMN' ,
'IDAUTORIZACION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Coódigo de la autorización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUTORIZACION' ,
'COLUMN' ,
'CODAUTORIZ'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripción del tipo de autorización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_AUTORIZACION' ,
'COLUMN' ,
'DESCAUTORIZ'
GO

CREATE
  TABLE SE_CODAUDITORIA
  (
    CODAUDITORIA   NUMERIC (5) NOT NULL ,
    TEXTOAUDITORIA VARCHAR (MAX) NOT NULL ,
    CONSTRAINT PK__SE_CODAUDITORIA__11007AA7 PRIMARY KEY CLUSTERED (
    CODAUDITORIA)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Patrones de mensajes de auditorías' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_CODAUDITORIA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Código de auditoria' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_CODAUDITORIA' ,
'COLUMN' ,
'CODAUDITORIA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Texto del evento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_CODAUDITORIA' ,
'COLUMN' ,
'TEXTOAUDITORIA'
GO

CREATE
  TABLE SE_COMP_ROLES
  (
    IDTAREA NUMERIC (5) NOT NULL ,
    IDROL   NUMERIC (5) NOT NULL ,
    CONSTRAINT PK_SE_COMP_ROLES PRIMARY KEY CLUSTERED (IDTAREA, IDROL)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Relaciones entre roles y sus tareas componentes' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_COMP_ROLES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Tarea' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_COMP_ROLES' ,
'COLUMN' ,
'IDTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Rol al que pertenece' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_COMP_ROLES' ,
'COLUMN' ,
'IDROL'
GO

CREATE
  TABLE SE_GRUPO_EXCLUSION
  (
    IDGRUPOACTUAL    NUMERIC (5) NOT NULL ,
    IDGRUPEXCLUYENTE NUMERIC (5) NOT NULL ,
    CONSTRAINT PK_SE_GRUPO_EXCLUSION PRIMARY KEY CLUSTERED (IDGRUPOACTUAL,
    IDGRUPEXCLUYENTE)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Relaciones de exclusión entre grupo de tareas' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_GRUPO_EXCLUSION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Grupo' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_GRUPO_EXCLUSION' ,
'COLUMN' ,
'IDGRUPOACTUAL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de grupo excluidos de pertenecer al mismo rol' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_GRUPO_EXCLUSION' ,
'COLUMN' ,
'IDGRUPEXCLUYENTE'
GO

CREATE
  TABLE SE_GRUPO_TAREA
  (
    IDGRUPO   NUMERIC (5) NOT NULL ,
    DESCGRUPO VARCHAR (100) NOT NULL ,
    CONSTRAINT PK_SE_GRUPO_TAREA PRIMARY KEY CLUSTERED (IDGRUPO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registros de identificación de grupos de tareas' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_GRUPO_TAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id grupo' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_GRUPO_TAREA' ,
'COLUMN' ,
'IDGRUPO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripción del grupo' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_GRUPO_TAREA' ,
'COLUMN' ,
'DESCGRUPO'
GO

CREATE
  TABLE SE_HISTORIAL_USUARIO
  (
    IDUSUARIO          NUMERIC (5) NOT NULL ,
    ORDEN              NUMERIC (2) NOT NULL ,
    SINONIMO           VARCHAR (MAX) NOT NULL ,
    FECHAULTACT        DATETIME ,
    FECHAVENCIMIENTO   DATETIME ,
    FECHAULTACTUSUARIO DATETIME ,
    CONSTRAINT PK_SE_HISTORIAL_USUARIO PRIMARY KEY CLUSTERED (IDUSUARIO, ORDEN)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registro de cambios de contraseñas' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HISTORIAL_USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Usuario.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HISTORIAL_USUARIO' ,
'COLUMN' ,
'IDUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Cambios de clave sucesivos' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HISTORIAL_USUARIO' ,
'COLUMN' ,
'ORDEN'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Clave del usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HISTORIAL_USUARIO' ,
'COLUMN' ,
'SINONIMO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de ultima actualización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HISTORIAL_USUARIO' ,
'COLUMN' ,
'FECHAULTACT'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de vencimiento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HISTORIAL_USUARIO' ,
'COLUMN' ,
'FECHAVENCIMIENTO'
GO

CREATE
  TABLE SE_HORARIOS_USUARIO
  (
    IDDIA     NUMERIC (1) NOT NULL ,
    IDUSUARIO NUMERIC (5) NOT NULL ,
    IDHORARIO NUMERIC (2) NOT NULL ,
    CONSTRAINT PK_SE_HORARIOS_USUARIO PRIMARY KEY CLUSTERED (IDDIA, IDUSUARIO,
    IDHORARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registro de restricciones horarias de permisos de uso de los sistemas' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HORARIOS_USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del dia de la semana' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HORARIOS_USUARIO' ,
'COLUMN' ,
'IDDIA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HORARIOS_USUARIO' ,
'COLUMN' ,
'IDUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Horario donde puede iniciar sesion' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_HORARIOS_USUARIO' ,
'COLUMN' ,
'IDHORARIO'
GO

CREATE
  TABLE SE_INSTANCIAUSUARIO
  (
    IDUSUARIO NUMERIC (5) NOT NULL ,
    OBJETO    VARCHAR (MAX) NOT NULL ,
    CONSTRAINT PK_SE_INSTANCIAUSUARIO PRIMARY KEY CLUSTERED (IDUSUARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registros con Serialización del objeto de seguridad CIPOL del usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_INSTANCIAUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_INSTANCIAUSUARIO' ,
'COLUMN' ,
'IDUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Serialización del objeto de seguridad CIPOL' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_INSTANCIAUSUARIO' ,
'COLUMN' ,
'OBJETO'
GO

CREATE
  TABLE SE_MENSAJES
  (
    CODMENSAJE   NUMERIC (5) NOT NULL ,
    TEXTOMENSAJE VARCHAR (200) NOT NULL ,
    CONSTRAINT PK_SE_MENSAJES PRIMARY KEY CLUSTERED (CODMENSAJE)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de mensajes para auditorías' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENSAJES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Codigo de mensaje' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENSAJES' ,
'COLUMN' ,
'CODMENSAJE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Texto del Mensaje' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENSAJES' ,
'COLUMN' ,
'TEXTOMENSAJE'
GO

CREATE
  TABLE SE_MENUES
  (
    IDSISTEMAS    NUMERIC (5) NOT NULL ,
    IDITEMMENU    NUMERIC (5) NOT NULL ,
    TEXTOITEM     VARCHAR (30) NOT NULL ,
    URL           VARCHAR (200) ,
    IDITEMPADRE   NUMERIC (5) NOT NULL ,
    ESPADRE       CHAR (1) NOT NULL ,
    DESCRIPCION   VARCHAR (250) ,
    IDTAREA       NUMERIC (5) NOT NULL ,
    ORDENSUBMENU  NUMERIC (5) ,
    ACCESODIRECTO CHAR (1) ,
    CONSTRAINT PK_SE_MENUES PRIMARY KEY CLUSTERED (IDITEMMENU, IDSISTEMAS)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default" ,
  CONSTRAINT IX_SE_MENUES UNIQUE NONCLUSTERED (IDITEMMENU) ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registros con Menúes de las aplicaciones' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del sistema al que pertenece' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'IDSISTEMAS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del item de menu' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'IDITEMMENU'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Texto que se mostrará por pantalla' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'TEXTOITEM'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Url de la opcion' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'URL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del item de menu del que depende' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'IDITEMPADRE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'"S" o "N" Si es un item "padre" o no' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'ESPADRE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripción adicional' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'DESCRIPCION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de tarea de CIPOL' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'IDTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Orden de la opción en el submenu que le corresponda' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'ORDENSUBMENU'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'S = Aparece en los accesos directos del sistema.
N = No aparece.
' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_MENUES' ,
'COLUMN' ,
'ACCESODIRECTO'
GO

CREATE
  TABLE SE_PARAMETROS
  (
    COLUMNA1 VARCHAR (500) ,
    COLUMNA2 VARCHAR (500) ,
    COLUMNA3 VARCHAR (500) ,
    COLUMNA4 VARCHAR (500) ,
    COLUMNA5 VARCHAR (500)
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Parámetros encriptados de uso internos de CIPOL' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_PARAMETROS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Parámetros Encriptados' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_PARAMETROS' ,
'COLUMN' ,
'COLUMNA1'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Parámetros Encriptados' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_PARAMETROS' ,
'COLUMN' ,
'COLUMNA2'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Parámetros Encriptados' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_PARAMETROS' ,
'COLUMN' ,
'COLUMNA3'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Parámetros Encriptados' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_PARAMETROS' ,
'COLUMN' ,
'COLUMNA4'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Parámetros Encriptados' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_PARAMETROS' ,
'COLUMN' ,
'COLUMNA5'
GO

CREATE
  TABLE SE_REL_AUTORIZ
  (
    IDTAREAPRIMITIVA NUMERIC (5) NOT NULL ,
    IDTAREAAUTOR     NUMERIC (5) NOT NULL ,
    CONSTRAINT PK_SE_REL_AUTORIZ PRIMARY KEY CLUSTERED (IDTAREAPRIMITIVA,
    IDTAREAAUTOR)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Relaciones entre tareas (autorizante y autorizada)' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_REL_AUTORIZ'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tarea a autorizar' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_REL_AUTORIZ' ,
'COLUMN' ,
'IDTAREAPRIMITIVA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tarea que la autoriza' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_REL_AUTORIZ' ,
'COLUMN' ,
'IDTAREAAUTOR'
GO

CREATE
  TABLE SE_ROLES
  (
    IDROL             NUMERIC (5) NOT NULL ,
    DESCRIPCIONPERFIL VARCHAR (150) NOT NULL ,
    FECHAULTMODIF     DATETIME ,
    IDUSUARIOULTMODIF NUMERIC (5) ,
    CONSTRAINT PK_SE_ROLES PRIMARY KEY CLUSTERED (IDROL)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de Roles de usuarios' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ROLES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de rol' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ROLES' ,
'COLUMN' ,
'IDROL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripcion del Rol' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ROLES' ,
'COLUMN' ,
'DESCRIPCIONPERFIL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de última modificación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ROLES' ,
'COLUMN' ,
'FECHAULTMODIF'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de Usuario de última modificación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_ROLES' ,
'COLUMN' ,
'IDUSUARIOULTMODIF'
GO

CREATE
  TABLE SE_SESIONESACTIVAS
  (
    STRUSUARIO    CHAR (15) NOT NULL ,
    INICIOTAREA   DATETIME NOT NULL ,
    NOMBRENETBIOS VARCHAR (255) NOT NULL ,
    CODIGO        VARCHAR (10) ,
    CONSTRAINT PK_SE_SESIONESACTIVAS PRIMARY KEY CLUSTERED (STRUSUARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registros de control de sesiones activas de usuarios' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SESIONESACTIVAS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SESIONESACTIVAS' ,
'COLUMN' ,
'STRUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de Login' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SESIONESACTIVAS' ,
'COLUMN' ,
'INICIOTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre netbios de la PC donde inició sesión.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SESIONESACTIVAS' ,
'COLUMN' ,
'NOMBRENETBIOS'
GO

CREATE
  TABLE SE_SIST_BLOQUEADOS
  (
    IDSISTEMA         NUMERIC (5) NOT NULL ,
    IDUSUARIO         NUMERIC (5) NOT NULL ,
    FECHAULTMODIF     DATETIME ,
    IDUSUARIOULTMODIF NUMERIC (5) NOT NULL ,
    CONSTRAINT PK_SE_INHABILITADOS PRIMARY KEY CLUSTERED (IDSISTEMA, IDUSUARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Registros de Sistemas Bloqueados' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_BLOQUEADOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de Sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_BLOQUEADOS' ,
'COLUMN' ,
'IDSISTEMA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_BLOQUEADOS' ,
'COLUMN' ,
'IDUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de última modificación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_BLOQUEADOS' ,
'COLUMN' ,
'FECHAULTMODIF'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID Usuario de Modificación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_BLOQUEADOS' ,
'COLUMN' ,
'IDUSUARIOULTMODIF'
GO

CREATE
  TABLE SE_SIST_EVENTOS
  (
    FECHAHORALOG DATETIME NOT NULL ,
    OPERACION    CHAR (1) ,
    TABLA        VARCHAR (50) ,
    STRINGSQL    VARCHAR (MAX) NOT NULL ,
    USUARIO      VARCHAR (25) NOT NULL ,
    SUPERVISOR   VARCHAR (25) ,
    NOMBREPC     VARCHAR (40) ,
    SISTEMA      VARCHAR (50)
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tabla de auditoría de sentencias SQL ejecutados contra la Base de Datos' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha hora del evento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'FECHAHORALOG'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'A = Alta
B = Baja
M = Modificación
' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'OPERACION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tabla Afectada' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'TABLA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Sentencia sql' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'STRINGSQL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Usuario ejecutante' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'n/a' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'SUPERVISOR'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre de la terminal' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'NOMBREPC'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_EVENTOS' ,
'COLUMN' ,
'SISTEMA'
GO

CREATE
  TABLE SE_SIST_HABILITADOS
  (
    IDSISTEMA         NUMERIC (5) NOT NULL ,
    CODSISTEMA        VARCHAR (10) NOT NULL ,
    DESCSISTEMA       VARCHAR (50) NOT NULL ,
    FECHAHABILITACION DATETIME ,
    NOMBREEXEC        VARCHAR (30) ,
    SISTEMAHABILITADO CHAR (1) ,
    ICONO             VARCHAR (30) ,
    OBSERVACIONES     VARCHAR (255) ,
    PAGINAPORDEFECTO  VARCHAR (100) ,
    DESCRIPCIONCORTA  VARCHAR (50) ,
    PATHREPORTES      VARCHAR (255) ,
    IMPACTACAJA       VARCHAR (1) ,
    SOAPACTION        VARCHAR (100) ,
    CONSTRAINT PK_SE_SIST_HABILITADOS PRIMARY KEY CLUSTERED (IDSISTEMA)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de sistemas habilitados para operar desde la sesión de CIPOL' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador del Sistema
' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'IDSISTEMA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Código de Sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'CODSISTEMA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre del Sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'DESCSISTEMA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de Habilitación del Sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'FECHAHABILITACION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre del Programa Ejecutable' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'NOMBREEXEC'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Sistema Habilitado o No

S = Si
N = No' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'SISTEMAHABILITADO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Icono del Sisitema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'ICONO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Modo de segurided de cipol y descripcion de la organización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'OBSERVACIONES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre la página por defecto' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'PAGINAPORDEFECTO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Decripción corta del nombre del sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'DESCRIPCIONCORTA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Path de reportes para el sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'PATHREPORTES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Si impacta en caja: "S" / "N"' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'IMPACTACAJA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'SOAP' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_SIST_HABILITADOS' ,
'COLUMN' ,
'SOAPACTION'
GO

CREATE
  TABLE SE_TAREAS
  (
    IDTAREA           NUMERIC (5) NOT NULL ,
    CODIGOTAREA       VARCHAR (10) NOT NULL ,
    DESCRIPCIONTAREA  VARCHAR (100) NOT NULL ,
    REQUIEREAUDITORIA CHAR (255) NOT NULL ,
    IDAUTORIZACION    NUMERIC (5) ,
    IDSISTEMA         NUMERIC (5) NOT NULL ,
    IDGRUPO           NUMERIC (5) ,
    CONSTRAINT PK_SE_TAREAS PRIMARY KEY CLUSTERED (IDTAREA)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de tareas de los sistemas administrados por CIPOL' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Tarea' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'IDTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Código de la tarea' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'CODIGOTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripción de la tarea' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'DESCRIPCIONTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Indica si la tarea va a auditarse' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'REQUIEREAUDITORIA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tipo de autorizacion' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'IDAUTORIZACION'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id del sistema' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'IDSISTEMA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id grupo' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS' ,
'COLUMN' ,
'IDGRUPO'
GO

CREATE
  TABLE SE_TAREAS_USUARIO
  (
    IDTAREA            NUMERIC (5) NOT NULL ,
    IDROL              NUMERIC (5) NOT NULL ,
    IDUSUARIO          NUMERIC (5) NOT NULL ,
    CANTIDADAUTORIZADA NUMERIC (14) ,
    TAREAINHIBIDA      CHAR (255) NOT NULL ,
    FECHAULTMODIF      DATETIME NOT NULL ,
    IDUSUARIOULTMODIF  NUMERIC (5) NOT NULL ,
    CONSTRAINT PK_SE_TAREAS_USUARIO PRIMARY KEY CLUSTERED (IDTAREA, IDROL,
    IDUSUARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tareas permitidas por usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Tarea' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'IDTAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Rol al que pertenece' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'IDROL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'IDUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'No se utiliza actualmente' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'CANTIDADAUTORIZADA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Indica si la tarea esta inhibida' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'TAREAINHIBIDA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de última modificación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'FECHAULTMODIF'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de Usuario de última modificación' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TAREAS_USUARIO' ,
'COLUMN' ,
'IDUSUARIOULTMODIF'
GO
CREATE NONCLUSTERED INDEX
IDX_SE_TAREAS_USUARIO ON SE_TAREAS_USUARIO
(
  IDUSUARIO
)
ON "default"
GO

CREATE
  TABLE SE_TERMINALES
  (
    IDTERMINAL          NUMERIC (5) NOT NULL ,
    CODTERMINAL         VARCHAR (20) NOT NULL ,
    NOMBRENETBIOS       VARCHAR (255) NOT NULL ,
    USOHABILITADO       VARCHAR (255) NOT NULL ,
    MODELOPROCESADOR    VARCHAR (50) ,
    CANTMEMORIARAM      NUMERIC (5) ,
    TAMANIODISCO        NUMERIC (5) ,
    MODELOMONITOR       VARCHAR (50) ,
    MODELOACELVIDEO     VARCHAR (50) ,
    DESCADICIONAL       VARCHAR (250) ,
    IDAREA              NUMERIC (3) NOT NULL ,
    ORIGENACTUALIZACION VARCHAR (1) NOT NULL DEFAULT 'L' ,
    CONSTRAINT PK_SE_TERMINALES PRIMARY KEY CLUSTERED (IDTERMINAL)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
ALTER TABLE SE_TERMINALES
ADD CONSTRAINT DF__SE_TERMIN__ORIGE__723D9313
CHECK ( ORIGENACTUALIZACION IN ('L','R') )
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de terminales habilitadas para usar los aplicativos' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id terminal' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'IDTERMINAL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Código de la terminal' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'CODTERMINAL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre netbios de la terminal' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'NOMBRENETBIOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Indica si la terminal esté habilitada o no.' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'USOHABILITADO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Información hardware' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'MODELOPROCESADOR'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Información hardware' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'CANTMEMORIARAM'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Información hardware' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'TAMANIODISCO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Información hardware' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'MODELOMONITOR'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Información hardware' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'MODELOACELVIDEO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Información hardware' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'DESCADICIONAL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'ID de Area' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'IDAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Origen de Actualización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERMINALES' ,
'COLUMN' ,
'ORIGENACTUALIZACION'
GO

CREATE
  TABLE SE_TERM_USUARIO
  (
    IDTERMINAL NUMERIC (5) NOT NULL ,
    IDUSUARIO  NUMERIC (5) NOT NULL ,
    CONSTRAINT PK_SE_TERM_USUARIO PRIMARY KEY CLUSTERED (IDTERMINAL, IDUSUARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Permisos/restricciones de uso de los sistemas por terminal por usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERM_USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id terminal' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERM_USUARIO' ,
'COLUMN' ,
'IDTERMINAL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_TERM_USUARIO' ,
'COLUMN' ,
'IDUSUARIO'
GO

CREATE
  TABLE SE_USUARIOS
  (
    IDUSUARIO        NUMERIC (5) NOT NULL ,
    USUARIO          VARCHAR (50) ,
    NOMBRES          VARCHAR (50) ,
    DOMICILIO        VARCHAR (50) ,
    FECHAULTUSOCTA   DATETIME ,
    NRODOCUMENTO     VARCHAR (10) ,
    CANTINTINVUSOCTA CHAR (255) ,
    FORZARCAMBIO     CHAR (255) ,
    CTABLOQUEADA     CHAR (255) ,
    COMENTARIO       VARCHAR (255) ,
    FECHABLOQUEO     DATETIME ,
    FECHAALTA        DATETIME ,
    FECHABAJA        DATETIME ,
    IDAREA           NUMERIC (3) NOT NULL ,
    IDTIPODOC        NUMERIC (3) NOT NULL ,
    ULTIMOSISTEMA    NUMERIC (5) ,
    ALIAS_USUARIO    VARCHAR (16) ,
    IDUSRALTA        NUMERIC (5) DEFAULT 0 ,
    IDUSRBAJA        NUMERIC (5) ,
    EMAIL            VARCHAR (50) ,
    FECHADESBLOQUEO  DATETIME ,
    CONSTRAINT PK_SE_USUARIOS PRIMARY KEY CLUSTERED (IDUSUARIO)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de usuarios' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Id de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'IDUSUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Cuenta de login de Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Apellido y Nombre del Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'NOMBRES'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Domicilio' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'DOMICILIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha del último login' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'FECHAULTUSOCTA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Número de Documento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'NRODOCUMENTO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Cantidad de intentos inválidos autorizados a su cuenta' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'CANTINTINVUSOCTA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Marca que indica si en el próximo login debe modificar su contraseña' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'FORZARCAMBIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Marca que indica si la cuenta está bloqueada' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'CTABLOQUEADA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Texto libre para comentarios' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'COMENTARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de bloqueo de la cuenta' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'FECHABLOQUEO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de alta del usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'FECHAALTA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de baja del usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'FECHABAJA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador exclusivo del área. Se trata del área primaria a la que pertenece el usuario.'
,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'IDAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador del tipo de documento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'IDTIPODOC'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador del último sistema usado' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'ULTIMOSISTEMA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Alias del Usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'ALIAS_USUARIO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador del usuario que operó el alta de esta cuenta de usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'IDUSRALTA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador del usuario que operó la baja de esta cuenta de usuario' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'IDUSRBAJA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Cuenta de correo electrónico' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'EMAIL'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Fecha de desbloqueo' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SE_USUARIOS' ,
'COLUMN' ,
'FECHADESBLOQUEO'
GO
CREATE NONCLUSTERED INDEX
SE_USUARIOS_IDX_USUARIO ON SE_USUARIOS
(
  USUARIO
)
ON "default"
GO
CREATE NONCLUSTERED INDEX
SE_USUARIOS_IDX_NOMBRES ON SE_USUARIOS
(
  NOMBRES
)
ON "default"
GO

CREATE
  TABLE SIST_KAREAS
  (
    IDAREA           NUMERIC (3) NOT NULL ,
    NOMBREAREA       VARCHAR (30) NOT NULL ,
    RESPONSABLE      VARCHAR (30) ,
    CARGORESPONSABLE VARCHAR (30) ,
    COMENTARIOS      VARCHAR (255) ,
    BAJA             CHAR (1) NOT NULL ,
    FICTICIA         CHAR (1) NOT NULL ,
    CONSTRAINT PK_SIST_KAREAS PRIMARY KEY CLUSTERED (IDAREA)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
ALTER TABLE SIST_KAREAS
ADD CONSTRAINT CF_SIST_KAreas_BAJA
CHECK ( BAJA IN ('S','N') )
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Maestro de áreas funcionales de la organización' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador exclusivo del área' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'IDAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre del área' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'NOMBREAREA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Nombre y apellido del responsable del área' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'RESPONSABLE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Cargo del responsable del área' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'CARGORESPONSABLE'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Texto libre para comentarios' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'COMENTARIOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Marca que indica la baja del registro' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'BAJA'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Marca que indica si el área es o no ficticia' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KAREAS' ,
'COLUMN' ,
'FICTICIA'
GO

CREATE
  TABLE SIST_KDOCUMENTOS
  (
    IDTIPODOC     NUMERIC (3) NOT NULL ,
    TIPODOCUMENTO CHAR (30) NOT NULL ,
    TIPOABREVIADO CHAR (6) NOT NULL ,
    DOCBAJA       CHAR (1) ,
    CONSTRAINT PK_KDOCUMENTOS PRIMARY KEY CLUSTERED (IDTIPODOC)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
  )
  ON "default"
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Tipos de documentos' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KDOCUMENTOS'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Identificador del tipo de documento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KDOCUMENTOS' ,
'COLUMN' ,
'IDTIPODOC'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripción del tipo de documento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KDOCUMENTOS' ,
'COLUMN' ,
'TIPODOCUMENTO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Descripción abreviada del tipo de documento' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KDOCUMENTOS' ,
'COLUMN' ,
'TIPOABREVIADO'
GO
EXEC sp_addextendedproperty 'MS_Description' ,
'Marca que indica si el registro está o no vigente' ,
'USER' ,
'dbo' ,
'TABLE' ,
'SIST_KDOCUMENTOS' ,
'COLUMN' ,
'DOCBAJA'
GO

ALTER TABLE SE_USUARIOS
ADD CONSTRAINT FK_IDITPODOC_SIST_KDOCUMENT FOREIGN KEY
(
IDTIPODOC
)
REFERENCES SIST_KDOCUMENTOS
(
IDTIPODOC
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_REL_AUTORIZ
ADD CONSTRAINT FK_REL_AUTORIZ_IDTAREA2 FOREIGN KEY
(
IDTAREAAUTOR
)
REFERENCES SE_TAREAS
(
IDTAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_ATRIBUTOSTAREAS
ADD CONSTRAINT FK_SE_ATRIBU_IDTAREA FOREIGN KEY
(
IDTAREA
)
REFERENCES SE_TAREAS
(
IDTAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_AUDITORIA
ADD CONSTRAINT FK_SE_CODAUDITORIA FOREIGN KEY
(
CODMENSAJE
)
REFERENCES SE_CODAUDITORIA
(
CODAUDITORIA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_COMP_ROLES
ADD CONSTRAINT FK_SE_COMP_ROLES_SE_ROLES FOREIGN KEY
(
IDROL
)
REFERENCES SE_ROLES
(
IDROL
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_COMP_ROLES
ADD CONSTRAINT FK_SE_COMP_ROLES_SE_TAREAS FOREIGN KEY
(
IDTAREA
)
REFERENCES SE_TAREAS
(
IDTAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_GRUPO_EXCLUSION
ADD CONSTRAINT FK_SE_GRUPO_EXCLUSION_GRUPO FOREIGN KEY
(
IDGRUPOACTUAL
)
REFERENCES SE_GRUPO_TAREA
(
IDGRUPO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_GRUPO_EXCLUSION
ADD CONSTRAINT FK_SE_GRUPO_EXCLUSION_IDGRUPEX FOREIGN KEY
(
IDGRUPEXCLUYENTE
)
REFERENCES SE_GRUPO_TAREA
(
IDGRUPO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_HORARIOS_USUARIO
ADD CONSTRAINT FK_SE_HORARIO_IDUSUARIO FOREIGN KEY
(
IDUSUARIO
)
REFERENCES SE_USUARIOS
(
IDUSUARIO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_INSTANCIAUSUARIO
ADD CONSTRAINT FK_SE_INSTUSUARIO_IDUSUARIO FOREIGN KEY
(
IDUSUARIO
)
REFERENCES SE_USUARIOS
(
IDUSUARIO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_MENUES
ADD CONSTRAINT FK_SE_MENUES_SE_SIST_HABI FOREIGN KEY
(
IDSISTEMAS
)
REFERENCES SE_SIST_HABILITADOS
(
IDSISTEMA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_MENUES
ADD CONSTRAINT FK_SE_MENUES_SE_TAREAS FOREIGN KEY
(
IDTAREA
)
REFERENCES SE_TAREAS
(
IDTAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_REL_AUTORIZ
ADD CONSTRAINT FK_SE_REL_AUTORIZ_IDTAREA1 FOREIGN KEY
(
IDTAREAPRIMITIVA
)
REFERENCES SE_TAREAS
(
IDTAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_SIST_BLOQUEADOS
ADD CONSTRAINT FK_SE_SIST_BLOQUEAD_IDSISTEMA FOREIGN KEY
(
IDSISTEMA
)
REFERENCES SE_SIST_HABILITADOS
(
IDSISTEMA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_SIST_BLOQUEADOS
ADD CONSTRAINT FK_SE_SIST_BLOQUEAD_IDUSUARIO FOREIGN KEY
(
IDUSUARIO
)
REFERENCES SE_USUARIOS
(
IDUSUARIO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TAREAS
ADD CONSTRAINT FK_SE_TAREAS_IDAUTORIZACION FOREIGN KEY
(
IDAUTORIZACION
)
REFERENCES SE_AUTORIZACION
(
IDAUTORIZACION
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TAREAS
ADD CONSTRAINT FK_SE_TAREAS_IDGRUPO FOREIGN KEY
(
IDGRUPO
)
REFERENCES SE_GRUPO_TAREA
(
IDGRUPO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TAREAS
ADD CONSTRAINT FK_SE_TAREAS_SE_SIST_HAB FOREIGN KEY
(
IDSISTEMA
)
REFERENCES SE_SIST_HABILITADOS
(
IDSISTEMA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TAREAS_USUARIO
ADD CONSTRAINT FK_SE_TAREAS_USUARIO_IDUSU FOREIGN KEY
(
IDUSUARIO
)
REFERENCES SE_USUARIOS
(
IDUSUARIO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TAREAS_USUARIO
ADD CONSTRAINT FK_SE_TAREAS_USUARIO_ROLES FOREIGN KEY
(
IDTAREA,
IDROL
)
REFERENCES SE_COMP_ROLES
(
IDTAREA ,
IDROL
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TERMINALES
ADD CONSTRAINT FK_SE_TERMINALES_SIST_KAREAS FOREIGN KEY
(
IDAREA
)
REFERENCES SIST_KAREAS
(
IDAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TERM_USUARIO
ADD CONSTRAINT FK_SE_TERM_IDTERMINAL FOREIGN KEY
(
IDTERMINAL
)
REFERENCES SE_TERMINALES
(
IDTERMINAL
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_TERM_USUARIO
ADD CONSTRAINT FK_SE_TERM_IDUSUARIO FOREIGN KEY
(
IDUSUARIO
)
REFERENCES SE_USUARIOS
(
IDUSUARIO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_USUARIOS
ADD CONSTRAINT FK_SE_USUARIOS_IDAREA FOREIGN KEY
(
IDAREA
)
REFERENCES SIST_KAREAS
(
IDAREA
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE SE_HISTORIAL_USUARIO
ADD CONSTRAINT FK__SE_HISTOR__IDUSU__353DDB1D FOREIGN KEY
(
IDUSUARIO
)
REFERENCES SE_USUARIOS
(
IDUSUARIO
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO



CREATE VIEW vRecuperarDatosParaReporteRolesXUsuarios  AS 
SELECT DISTINCT
  su.IDUSUARIO,
  su.NOMBRES,
  su.USUARIO,
  SIST_KAREAS.NOMBREAREA,
  su.DOMICILIO,
  SIST_KDOCUMENTOS.TIPOABREVIADO,
  su.NRODOCUMENTO,
  su.FORZARCAMBIO,
  su.FORZARCAMBIO AS ForzarCambioDes,
  su.CTABLOQUEADA,
  su.CTABLOQUEADA AS CtaBloqueadaDes,
  su.CTABLOQUEADA AS CtaBloqueadaDesLetra,
  su.COMENTARIO,
  su.FECHAALTA,
  su.FECHABAJA,
  su.FECHABLOQUEO,
  su.ALIAS_USUARIO,
  SIST_KAREAS.FICTICIA,
  su.CANTINTINVUSOCTA,
  su.FECHAULTUSOCTA,
  su.EMAIL
FROM
  SE_USUARIOS su
INNER JOIN SIST_KDOCUMENTOS
ON
  SIST_KDOCUMENTOS.IDTIPODOC = su.IDTIPODOC
AND SIST_KDOCUMENTOS.DOCBAJA = 'N'
AND su.FECHABAJA            IS NULL
INNER JOIN SIST_KAREAS
ON
  SIST_KAREAS.IDAREA = su.IDAREA
INNER JOIN SE_TAREAS_USUARIO stu
ON
  stu.IDUSUARIO = su.IDUSUARIO
AND stu.IDROL  <> -99 
GO




CREATE VIEW vRecuperarRoles  AS 
SELECT
  SE_ROLES.IDROL,
  rtrim(SE_ROLES.DESCRIPCIONPERFIL) AS DescripcionPerfil
FROM
  SE_ROLES
WHERE
  SE_ROLES.IDROL <> -99 
GO




CREATE VIEW vRetornarRolesXUsuarios  AS 
SELECT
  COUNT(stu.IDUSUARIO) AS CanTareasUsuarioRol,
  stu.IDUSUARIO,
  stu.IDROL,
  su.NOMBRES,
  su.FECHAALTA,
  su.FECHABAJA,
  su.NRODOCUMENTO,
  su.USUARIO,
  scr.DESCRIPCIONPERFIL AS descripcionperfil,
  kd.TIPOABREVIADO      AS idtipodocdesc,
  kd.TIPOABREVIADO,
  MAX(stu.FECHAULTMODIF) AS fechaAModificacion,
  0                      AS cantTareasRol,
  '        '             AS usuariomodif,
  '          '           AS COMPLETO,
  su.IDAREA,
  ka.NOMBREAREA
FROM
  SE_ROLES scr
INNER JOIN SE_TAREAS_USUARIO stu
ON
  scr.IDROL    = stu.IDROL
AND scr.IDROL <> -99
INNER JOIN SE_USUARIOS su
ON
  su.IDUSUARIO = stu.IDUSUARIO
INNER JOIN SIST_KDOCUMENTOS kd
ON
  su.IDTIPODOC = kd.IDTIPODOC
INNER JOIN SIST_KAREAS ka
ON
  su.IDAREA = ka.IDAREA
GROUP BY
  stu.IDUSUARIO,
  stu.IDROL,
  su.NOMBRES,
  su.FECHAALTA,
  su.FECHABAJA,
  su.NRODOCUMENTO,
  su.USUARIO,
  scr.DESCRIPCIONPERFIL,
  kd.TIPOABREVIADO,
  su.IDAREA,
  ka.NOMBREAREA 
GO





-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            27
-- CREATE INDEX                             3
-- ALTER TABLE                             27
-- CREATE VIEW                              3
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
