USE [GD2C2015]
GO
/****** Object:  Schema [JANADIAN_DATE]    Script Date: 09/10/2015 01:58:54 ******/
/**** creacion de esquema para trabajar en el tp ********/
CREATE SCHEMA [JANADIAN_DATE] AUTHORIZATION [gd]
GO


/** Creacion de tablas ***/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Funcion que chequea si un rol esta habilitado  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Rol_Habilitado](@id int)
RETURNS  bit
AS 
BEGIN
   DECLARE @retval bit
   SELECT @retval = Habilitado FROM [JANADIAN_DATE].[Rol] WHERE id=@id
   RETURN @retval
END
GO

/** Creacion de tabla funcionalidad 
IF OBJECT_ID( '[JANADIAN_DATE].[Funcionalidad]') IS NOT NULL
DROP TABLE  [JANADIAN_DATE].[Funcionalidad];
GO
 ***/
 /** Creacion de tabla funcionalidad  ***/
CREATE TABLE [JANADIAN_DATE].[Funcionalidad](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar](255) NOT NULL UNIQUE

) ON [PRIMARY]

GO
/** Creacion de tabla rol ***/
CREATE TABLE [JANADIAN_DATE].[Rol](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE,
	/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1 

) ON [PRIMARY]

GO
/** Creacion de tabla rol_funcionalidad ***/
CREATE TABLE [JANADIAN_DATE].[Rol_Funcionalidad](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Rol] [int] NOT NULL,
	[Funcionalidad] [int] NOT NULL,
	CONSTRAINT FK_Rol FOREIGN KEY (Rol) REFERENCES [JANADIAN_DATE].[Rol] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	CONSTRAINT FK_Funcionalidad FOREIGN KEY (Funcionalidad) REFERENCES [JANADIAN_DATE].[Funcionalidad] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	CONSTRAINT AK_Rol_Func UNIQUE (Rol,Funcionalidad)
) ON [PRIMARY]

GO

/** Creacion de tabla usuario ***/
CREATE TABLE [JANADIAN_DATE].[Usuario](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE,
	[Password] [nvarchar](255) NOT NULL,
	[Intentos] [int] NOT NULL DEFAULT 0,
	[Rol] [int],
		/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1 
	CONSTRAINT FK_Usuario_Rol FOREIGN KEY (Rol) REFERENCES [JANADIAN_DATE].[Rol] (Id)
	ON DELETE NO ACTION
    ON UPDATE CASCADE,
	CONSTRAINT CHK_Rol_Habilitado CHECK ( [JANADIAN_DATE].[Rol_Habilitado]([Rol])=1)
) ON [PRIMARY]

GO
/** Creacion de trigger para quitar rol de usuario cuando se inhabilita el rol ***/
CREATE TRIGGER trgQuitarRolDeshabilitarDeUsuario ON  [JANADIAN_DATE].[Rol]
FOR UPDATE
AS
BEGIN
	declare @habilitado  bit ;
	declare @id int;

	select @id=i.Id,@habilitado=i.Habilitado from inserted i;	
	
	if  @habilitado=0
		UPDATE [JANADIAN_DATE].[Usuario]  set  Rol=NULL
		where Rol=@id
	end
GO
/** Creacion de trigger para deshabilitar usuario cuando tuvo 3 intentos ***/
CREATE TRIGGER trgDeshabilitarUserFallido ON  [JANADIAN_DATE].[Usuario]
FOR UPDATE
AS
BEGIN
	declare @intentos int  ;
	declare @id int;

	select @id=i.Id,@intentos=i.Intentos from inserted i;	
	
	if @intentos>=3
		UPDATE [JANADIAN_DATE].[Usuario]  set  Habilitado=0
		where id=@id
	end
GO