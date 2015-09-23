USE [GD2C2015]
GO
/****** Object:  Schema [JANADIAN_DATE]    Script Date: 09/10/2015 01:58:54 ******/
/**** creacion de esquema para trabajar en el tp ********/
CREATE SCHEMA [JANADIAN_DATE] AUTHORIZATION [gd]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************************/
/*********************************** CREACION DE FUNCIONES ******************************/
/****************************************************************************************/

/****** Funcion que chequea si un rol esta habilitado  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Rol_Habilitado](@id int)
RETURNS  bit
AS 
BEGIN
   DECLARE @retval bit
   SELECT @retval = Habilitado FROM [JANADIAN_DATE].[Rol] WHERE id=@id   
   RETURN ISNULL ( @retval , 0 )
END
GO
/****** Funcion que chequea si una aeronave esta habilitada  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Aeronave_Habilitada](@id int)
RETURNS  bit
AS 
BEGIN
   DECLARE @retval bit
   SELECT @retval = Habilitado FROM [JANADIAN_DATE].[Aeronave] WHERE id=@id
   RETURN ISNULL ( @retval , 0 )
END
GO
/****** Funcion que devuelve el tipo de servicio de una aeronave  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Get_Tipo_Servicio_Aeronave](@id int)
RETURNS  int
AS 
BEGIN
   DECLARE @retval int
   SELECT @retval = Tipo_Servicio FROM [JANADIAN_DATE].[Aeronave] WHERE id=@id
   RETURN @retval
END
GO

/****** Funcion que devuelve el tipo de servicio de una ruta  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Get_Tipo_Servicio_Ruta](@id int)
RETURNS  int
AS 
BEGIN
   DECLARE @retval int
   SELECT @retval = Tipo_Servicio FROM [JANADIAN_DATE].[Ruta] WHERE id=@id
   RETURN @retval
END
GO


 /****** Funcion que devuelve cuantos viajes tiene en una fecha una aeronave  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Viajes_Fecha_Aeronave](@id int,@fecha datetime)
RETURNS  int
AS 
BEGIN
   DECLARE @count int
   SELECT @count = COUNT(*) FROM [JANADIAN_DATE].[Viaje] WHERE Aeronave=@id AND DATEPART(yy,FechaSalida) = DATEPART(yy,@fecha) AND DATEPART(mm,FechaSalida) = DATEPART(mm,@fecha) AND DATEPART(dd,FechaSalida) = DATEPART(dd,@fecha)
   RETURN  ISNULL ( @count , 0 )
END
GO

/************************************************************************************/
/************************** CREACION DE TABLAS **************************************/
/************************************************************************************/

 /** Creacion de tabla funcionalidad  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Funcionalidad]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Funcionalidad](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar](255) NOT NULL UNIQUE

) ON [PRIMARY]

GO
/** Creacion de tabla rol ***/
IF OBJECT_ID('[JANADIAN_DATE].[Rol]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Rol](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE,
	/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1 

) ON [PRIMARY]

GO
/** Creacion de tabla rol_funcionalidad ***/
IF OBJECT_ID('[JANADIAN_DATE].[Rol_Funcionalidad]') IS NULL
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
IF OBJECT_ID('[JANADIAN_DATE].[Usuario]') IS NULL
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

 /** Creacion de tabla ciudad  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Ciudad]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Ciudad](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE

) ON [PRIMARY]

GO

 /** Creacion de tabla tipo servicio  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Tipo_Servicio]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Tipo_Servicio](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE

) ON [PRIMARY]

GO

 /** Creacion de tabla tipo fabricante  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Fabricante]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Fabricante](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE

) ON [PRIMARY]

GO
 /** Creacion de tabla ruta  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Ruta]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Ruta](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Codigo] [numeric](18,0) NOT NULL UNIQUE,
	[Precio_BaseKG] [numeric](18,2) NOT NULL,
	[Precio_BasePasaje] [numeric](18,2) NOT NULL,
	[Ciudad_Origen] [int],
	[Ciudad_Destino] [int],
	[Tipo_Servicio] [int] FOREIGN KEY REFERENCES  [JANADIAN_DATE].[Tipo_Servicio] (Id) NOT NULL,
	/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1 
	CONSTRAINT FK_Ciudad_Origen FOREIGN KEY (Ciudad_Origen) REFERENCES [JANADIAN_DATE].[Ciudad] (Id)
	ON DELETE NO ACTION
    ON UPDATE  NO ACTION,
	CONSTRAINT FK_Ciudad_Destino FOREIGN KEY (Ciudad_Destino) REFERENCES [JANADIAN_DATE].[Ciudad] (Id)
	ON DELETE NO ACTION
    ON UPDATE  NO ACTION,
	CONSTRAINT CHK_Origen CHECK ( [Ciudad_Origen]<>[Ciudad_Destino])
) ON [PRIMARY]

GO

 /** Creacion de tabla aeronave  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Aeronave]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Aeronave](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Matricula] [nvarchar](255) NOT NULL UNIQUE,
	[Modelo] [nvarchar](255) NOT NULL ,
	[KG_Disponibles] [numeric](18,0) NOT NULL,
	[Fabricante] [int] FOREIGN KEY REFERENCES  [JANADIAN_DATE].[Fabricante] (Id) NOT NULL,
	[Tipo_Servicio] [int] FOREIGN KEY REFERENCES  [JANADIAN_DATE].[Tipo_Servicio] (Id) NOT NULL,
	[Fecha_Alta] [datetime] NOT NULL DEFAULT CURRENT_TIMESTAMP,
		/**Por defecto sin bajas ***/
	[Baja_Fuera_Servicio] [bit] NOT NULL DEFAULT 0,
	[Baja_Vida_Util] [bit] NOT NULL DEFAULT 0,
	[Fecha_Fuera_Servicio] [datetime],
	[Fecha_Reinicio_Servicio] [datetime],
	[Fecha_Baja_Definitiva] [datetime],
	[Cant_Butacas_Ventanilla] [int]  NOT NULL,
	[Cant_Butacas_Pasillo] [int]  NOT NULL,
		/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1

) ON [PRIMARY]

GO

 /** Creacion de tabla butaca  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Butaca]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Butaca](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Numero] [numeric](18,0) NOT NULL,
	[Tipo] [nvarchar](255) NOT NULL,
    [Piso] [numeric](18,0) NOT NULL,
	[Aeronave] [int] NOT NULL,
	CONSTRAINT FK_Butaca_Aeronave FOREIGN KEY (Aeronave) REFERENCES [JANADIAN_DATE].[Aeronave] (Id)
	ON DELETE NO ACTION
    ON UPDATE CASCADE

) ON [PRIMARY]

GO

 /** Creacion de tabla viaje  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Viaje]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Viaje](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[FechaSalida] [datetime] NOT NULL,
	[Fecha_Llegada_Estimada] [datetime]  NOT NULL,
	[FechaLlegada] [datetime],
	[Aeronave] [int],
	CONSTRAINT FK_Viaje_Aeronave FOREIGN KEY (Aeronave) REFERENCES [JANADIAN_DATE].[Aeronave] (Id)
	ON DELETE NO ACTION
    ON UPDATE CASCADE,
	[Ruta] [int],
	CONSTRAINT FK_Ruta_Viaje FOREIGN KEY (Ruta) REFERENCES [JANADIAN_DATE].[Ruta] (Id)
	ON DELETE NO ACTION
    ON UPDATE CASCADE,
	CONSTRAINT CHK_Fechas_Futuras CHECK ( DATEDIFF(day,CURRENT_TIMESTAMP,[FechaSalida])>0 and DATEDIFF(day,CURRENT_TIMESTAMP,[Fecha_Llegada_Estimada])>0),
	CONSTRAINT CHK_Mismo_Tipo_Servicio CHECK ([JANADIAN_DATE].[Get_Tipo_Servicio_Aeronave]([Aeronave]) = [JANADIAN_DATE].[Get_Tipo_Servicio_Ruta]([Ruta]) ),
	CONSTRAINT CHK_Aeronave_Disponible CHECK ([JANADIAN_DATE].[Aeronave_Habilitada]([Aeronave])=1 AND [JANADIAN_DATE].[Viajes_Fecha_Aeronave]([Aeronave],[FechaSalida])=0)

) ON [PRIMARY]

GO

 /** Creacion de tabla Butaca viaje ***/
IF OBJECT_ID('[JANADIAN_DATE].[Butaca_Viaje]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Butaca_Viaje](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Butaca] [int] NOT NULL,
	[Viaje] [int] NOT NULL,
	CONSTRAINT FK_Butaca FOREIGN KEY (Butaca) REFERENCES [JANADIAN_DATE].[Butaca] (Id),
	CONSTRAINT FK_Viaje FOREIGN KEY (Viaje) REFERENCES [JANADIAN_DATE].[Viaje] (Id),
	CONSTRAINT AK_Butaca_Viaje UNIQUE (Butaca,Viaje)
) ON [PRIMARY]

GO

 /** Creacion de tabla Cliente ***/
IF OBJECT_ID('[JANADIAN_DATE].[Cliente]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Cliente](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Dni] [numeric](18,0) UNIQUE NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[Apellido] [nvarchar](255) NOT NULL,
	[Dir] [nvarchar](255) NOT NULL,
	[Telefono] [numeric](18,0) UNIQUE NOT NULL,
	[Mail] [nvarchar](255),
	[Fecha_Nac] [datetime] NOT NULL,
) ON [PRIMARY]

GO


 /** Creacion de tabla Compra ***/
IF OBJECT_ID('[JANADIAN_DATE].[Compra]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Compra](
	[PNR] [int] IDENTITY(1,1) PRIMARY KEY,
	[Precio] [numeric](18,2) NOT NULL,
	[Fecha_Compra] [datetime] NOT NULL,
	[Viaje] [int] NOT NULL,
	[Forma_Pago] [nvarchar](255) NOT NULL,
	CONSTRAINT FK_Compra_Viaje FOREIGN KEY (Viaje) REFERENCES [JANADIAN_DATE].[Viaje] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,

) ON [PRIMARY]

GO

  /** Creacion de tabla Pasaje ***/
IF OBJECT_ID('[JANADIAN_DATE].[Pasaje]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Pasaje](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Codigo] [numeric](18,0) NOT NULL UNIQUE,
	[Butaca] [int] NOT NULL,
	[Compra] [int] NOT NULL FOREIGN KEY (Compra) REFERENCES [JANADIAN_DATE].[Compra] (PNR),
	[Cliente] [int] NOT NULL,
	/**Por defecto activo ***/
	[Cancelado] [bit] NOT NULL DEFAULT 0 
	CONSTRAINT FK_Pasaje_Butaca FOREIGN KEY (Butaca) REFERENCES [JANADIAN_DATE].[Butaca] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	CONSTRAINT FK_Pasaje_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
) ON [PRIMARY]

GO

  /** Creacion de tabla Paquete ***/
IF OBJECT_ID('[JANADIAN_DATE].[Paquete]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Paquete](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Codigo] [numeric](18,0) NOT NULL UNIQUE,
	[KG] [numeric](18,0),
	[Compra] [int] NOT NULL FOREIGN KEY (Compra) REFERENCES [JANADIAN_DATE].[Compra] (PNR),
	[Cliente] [int] NOT NULL,
	/**Por defecto activo ***/
	[Cancelado] [bit] NOT NULL DEFAULT 0 
	CONSTRAINT FK_Paquete_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
) ON [PRIMARY]

GO

  /** Creacion de tabla Datos tarjeta ***/
IF OBJECT_ID('[JANADIAN_DATE].[Datos_Tarjeta]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Datos_Tarjeta](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Numero] [numeric](18,0) NOT NULL UNIQUE,
	[Tipo] [nvarchar](255) NOT NULL,
	[Cod_Seg]  [numeric](18,0) NOT NULL,
	[Fecha_Venc] [nvarchar](4) NOT NULL,
	[Compra] [int] NOT NULL FOREIGN KEY (Compra) REFERENCES [JANADIAN_DATE].[Compra] (PNR),
	[Cliente] [int] NOT NULL,
	CONSTRAINT FK_Tarjeta_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
) ON [PRIMARY]

GO

 /** Creacion de tabla Producto  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Producto]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Producto](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](255) NOT NULL UNIQUE,
	[Stock] [int] NOT NULL,
	[Millas_Necesarias] [int] NOT NULL

) ON [PRIMARY]

GO


 /** Creacion de tabla Canje  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Canje]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Canje](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Fecha] [datetime] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Cliente] [int] NOT NULL,
	[Motivo] [nvarchar](255) NOT NULL,
	CONSTRAINT FK_Canje_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	[Producto] [int] NOT NULL,
	CONSTRAINT FK_Canje_Producto FOREIGN KEY (Producto) REFERENCES [JANADIAN_DATE].[Producto] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,

) ON [PRIMARY]

GO

 /** Creacion de tabla Millas  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Millas]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Millas](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Fecha] [datetime] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Cliente] [int] NOT NULL,
	[Motivo] [nvarchar](255) NOT NULL,
	CONSTRAINT FK_Millas_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,

) ON [PRIMARY]

GO

 /** Creacion de tabla Cancelacion  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Cancelacion]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Cancelacion](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[PNR] [int] NOT NULL,
	[Codigo_Pasaje] [int] FOREIGN KEY (Codigo_Pasaje) REFERENCES [JANADIAN_DATE].[Pasaje] (Id),
	[Codigo_Paquete] [int] FOREIGN KEY (Codigo_Paquete) REFERENCES [JANADIAN_DATE].[Paquete] (Id),
	[Pasaje_o_Paquete] [nvarchar](255) NOT NULL,
	[Motivo] [nvarchar](255) NOT NULL,
	[FechaDevolucion] [datetime] NOT NULL,
	CONSTRAINT FK_Cancelacion_Compra FOREIGN KEY (PNR) REFERENCES [JANADIAN_DATE].[Compra] (PNR)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	CONSTRAINT CHK_Algun_tipo CHECK ( ([Codigo_Pasaje] IS NOT NULL) OR ([Codigo_Paquete] IS NOT NULL) ),
	CONSTRAINT CHK_Tipos CHECK ( [Pasaje_o_Paquete] IN ('PASAJE','PAQUETE') )
) ON [PRIMARY]

GO


/*******************************************************************************/
/******************** CREACION DE VISTAS ***************************************/
/*******************************************************************************/

 /** Creacion de vista itinerario aeronave ***/
 CREATE VIEW [JANADIAN_DATE].[Itinerario_Aeronave] AS  
 SELECT a.Matricula,v.FechaLlegada,o.Nombre as Origen,d.Nombre as Destino FROM   [JANADIAN_DATE].[Viaje] v
 INNER JOIN [JANADIAN_DATE].[Aeronave] a ON (v.Aeronave = a.Id)
 INNER JOIN [JANADIAN_DATE].[Ruta] r ON (v.Ruta = r.Id)
 INNER JOIN [JANADIAN_DATE].[Ciudad] o ON (r.Ciudad_Origen = o.Id)
 INNER JOIN [JANADIAN_DATE].[Ciudad] d ON (r.Ciudad_Destino = d.Id)

 GO

 
 /** Creacion de vista viajes disponibles ***/
 CREATE VIEW [JANADIAN_DATE].[Viaje_Disponible] AS  
 SELECT v.FechaSalida,o.Nombre as Origen,d.Nombre as Destino FROM   [JANADIAN_DATE].[Viaje] v
 INNER JOIN [JANADIAN_DATE].[Ruta] r ON (v.Ruta = r.Id)
 INNER JOIN [JANADIAN_DATE].[Ciudad] o ON (r.Ciudad_Origen = o.Id)
 INNER JOIN [JANADIAN_DATE].[Ciudad] d ON (r.Ciudad_Destino = d.Id)

 GO

 /********************************************************************************/
/******************** CREACION DE TRIGGERS ***************************************/
/*********************************************************************************/

/** Creacion de trigger para quitar rol de usuario cuando se inhabilita el rol ***/
CREATE TRIGGER [JANADIAN_DATE].[trgQuitarRolDeshabilitarDeUsuario] ON  [JANADIAN_DATE].[Rol]
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
CREATE TRIGGER [JANADIAN_DATE].[trgDeshabilitarUserFallido] ON  [JANADIAN_DATE].[Usuario]
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


/*** trigger que cancela pasajes y encomiendas al dar de baja una ruta (inhabilitar) ****/
/***        ****/
CREATE TRIGGER [JANADIAN_DATE].[trgInhabilitarRuta] ON  [JANADIAN_DATE].[Ruta]
FOR UPDATE
AS
BEGIN
	declare @habilitado  bit ;
	declare @id int;
	declare @pnr int;

	select @id=i.Id,@habilitado=i.Habilitado from inserted i;	
	
	if  @habilitado=0	
			
	/***   RECORRER TODO LO ASOCIADO A LA RUTA Y CANCEARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Pasaje] ***/
		
		SELECT @pnr=c.PNR FROM [JANADIAN_DATE].[Ruta] r
		INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Ruta = r.Id)
		INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
		WHERE r.Id=@Id		

		exec [JANADIAN_DATE].[Cancelar_Pasaje] @pnr,24,'Baja de Ruta'
	end
	
GO


 /********************************************************************************/
/******************** CREACION DE PROCEDIMIENTOS ***************************************/
/*********************************************************************************/

/***   cancelar un pasaje  necesito pnr cod pasaje y motivo , obtengo la fecha y guardo con la relacion a id de la tabla pasaje ***/
CREATE PROCEDURE [JANADIAN_DATE].[Cancelar_Pasaje] 
	@PNR_compra int,
	@cod_pasaje [numeric](18,0),
	@motivo nvarchar(255) 

AS
BEGIN TRY
	declare @butacaPasaje int;
	declare @kg_pasaje numeric(18,0);

	BEGIN TRANSACTION
	/** consultar**/
	SELECT  @butacaPasaje=Butaca FROM [JANADIAN_DATE].[Pasaje] WHERE Id=@cod_pasaje
	SELECT  @kg_pasaje=KG FROM [JANADIAN_DATE].[Paquete] WHERE Id=@cod_pasaje

	/** verificar si es pasaje o paquete**/
	IF  @kg_pasaje IS NOT NULL
		BEGIN
		 /** Liberar los KG **/
			/** Marcamos el paquete como cancelado **/
			UPDATE  [JANADIAN_DATE].[Paquete] SET Cancelado=1 WHERE Id=@cod_pasaje

			/** grabamos la cancelacion en la tabla correspondiente **/
				INSERT INTO Cancelacion 
				(PNR,Codigo_Paquete,Motivo,FechaDevolucion,Pasaje_o_Paquete) 
				VALUES
				(@PNR_compra,@cod_pasaje,ISNULL (@motivo,''),CURRENT_TIMESTAMP,'PAQUETE')	
				END
			ELSE IF  @butacaPasaje IS NOT NULL
		BEGIN
		/** Liberar la butaca **/
		 DELETE Butaca_Viaje WHERE Butaca=@butacaPasaje
		/** Marcamos el pasaje como cancelado **/
			UPDATE  [JANADIAN_DATE].[Pasaje] SET Cancelado=1 WHERE Id=@cod_pasaje
					/** grabamos la cancelacion en la tabla correspondiente **/
				INSERT INTO Cancelacion 
				(PNR,Codigo_Pasaje,Motivo,FechaDevolucion,Pasaje_o_Paquete) 
				VALUES
				(@PNR_compra,@cod_pasaje,ISNULL (@motivo,''),CURRENT_TIMESTAMP,'PAQUETE')	
		END


	/** aplicamos los cambios **/
	COMMIT
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);

END CATCH;
GO
