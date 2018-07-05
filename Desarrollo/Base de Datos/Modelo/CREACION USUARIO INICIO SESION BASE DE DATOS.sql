USE [master]
GO

CREATE LOGIN [usrcipol] WITH PASSWORD=N'usrcipol', 
DEFAULT_DATABASE=[webcipol], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=Off
GO



USE [WEBCIPOL]
GO

CREATE USER [usrcipol] FOR LOGIN [usrcipol] WITH DEFAULT_SCHEMA=[dbo]
GO

EXEC sp_addrolemember N'db_owner', N'usrcipol'
GO