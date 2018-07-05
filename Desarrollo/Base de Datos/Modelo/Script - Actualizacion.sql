/*
--Autor: Luciano Pucciarelli
--Fecha: 05/04/2017
--Descripción: Codigos de mensajes 
*/

BEGIN TRY 
 
	BEGIN TRANSACTION

	DECLARE @cSql VARCHAR(2000)

	------------------
	-- SE_AUDITORIA --
	------------------
	IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_name(object_id) = 'SE_AUDITORIA' and name = 'IDUSUARIO')
	BEGIN
		ALTER TABLE SE_AUDITORIA ADD [IDUSUARIO] [numeric](5,0) NULL
		
		SET @cSql = N'Identificador único del usuario logeado en el sistema si es que existe. Puede ser NULL ya que hay operaciones que se producen en el sistema sin un usuario logeado.'
		EXEC sys.sp_addextendedproperty N'MS_Description', @cSql, N'SCHEMA',N'dbo', N'TABLE',N'SE_AUDITORIA', N'COLUMN', N'IDUSUARIO'

		ALTER TABLE [dbo].[SE_AUDITORIA]  
		WITH CHECK ADD  CONSTRAINT [FK_SE_AUDITORIA_IDUSUARIO] FOREIGN KEY([IDUSUARIO])
		REFERENCES [dbo].[SE_USUARIOS] ([IDUSUARIO])
	END

	IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_name(object_id) = 'SE_AUDITORIA' and name = 'IP')
	BEGIN
		ALTER TABLE SE_AUDITORIA ADD [IP] VARCHAR(100) NULL
		
		SET @cSql = N'Dirección IP de la terminal asociada al evento.'
		EXEC sys.sp_addextendedproperty N'MS_Description', @cSql, N'SCHEMA',N'dbo', N'TABLE',N'SE_AUDITORIA', N'COLUMN', N'IP'
	END

	IF NOT EXISTS(SELECT * FROM sys.columns WHERE object_name(object_id) = 'SE_AUDITORIA' and name = 'IDAREA')
	BEGIN
		ALTER TABLE SE_AUDITORIA ADD [IDAREA] NUMERIC(3,0) NULL
		
		SET @cSql = N'ID del área a la cual pertenece el usuario asociado al evento si es que existe. Puede ser NULL ya que hay operaciones que se producen en el sistema sin un usuario logeado.'
		EXEC sys.sp_addextendedproperty N'MS_Description', @cSql, N'SCHEMA',N'dbo', N'TABLE',N'SE_AUDITORIA', N'COLUMN', N'IDAREA'

		ALTER TABLE [dbo].[SE_AUDITORIA]  
		WITH CHECK ADD  CONSTRAINT [FK_SIST_KAREAS_IDAREA] FOREIGN KEY([IDAREA])
		REFERENCES [dbo].[SIST_KAREAS] ([IDAREA])
	END

	COMMIT

END try
BEGIN catch 
    ROLLBACK 
    PRINT ' Error!!!' 
    SELECT Error_message(), 
           Isnull(Error_procedure(), '-') 
END catch 

/*
--Autor: Luciano Pucciarelli
--Fecha: 05/04/2017 
--Descripción: Nuevos mensajes
*/

BEGIN TRY 

	BEGIN TRANSACTION

	IF EXISTS(SELECT * FROM SE_CODAUDITORIA WHERE CODAUDITORIA = 0)
	BEGIN
		UPDATE [SE_CODAUDITORIA] SET [TEXTOAUDITORIA] = 'El usuario @ realizo el login desde la terminal @' WHERE CODAUDITORIA = 0
	END

	IF NOT EXISTS(SELECT * FROM SE_CODAUDITORIA WHERE CODAUDITORIA = 1)
	BEGIN
		INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(1,'El usuario @ realizo el logout desde la terminal @');
	END

	IF NOT EXISTS(SELECT * FROM SE_CODAUDITORIA WHERE CODAUDITORIA = 2)
	BEGIN
		INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(2,'Se cerró la sesión en la terminal @ por superar el límite de tiempo permitido');
	END

	IF NOT EXISTS(SELECT * FROM SE_CODAUDITORIA WHERE CODAUDITORIA = 3)
	BEGIN
		INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(3,'El usuario @ cerró involuntariamente la sesión en la terminal @');
	END

	COMMIT

END try
BEGIN catch 
    ROLLBACK 
    PRINT ' Error!!!' 
    SELECT Error_message(), 
           Isnull(Error_procedure(), '-') 
END catch 