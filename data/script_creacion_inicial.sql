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
SELECT @count = COUNT(*) FROM [JANADIAN_DATE].[Viaje] WHERE Aeronave=@id AND DATEDIFF(SECOND,@fecha,[FechaSalida]) >=0 AND DATEDIFF(SECOND,@fecha,ISNULL([FechaLlegada],[Fecha_Llegada_Estimada])) <= 0
   RETURN  ISNULL ( @count , 0 )
END
GO

/****** Funcion que chequea si un rol esta habilitado  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Aeronave_Habilitada_Por_Matricula](@matricula nvarchar(255))
RETURNS  bit
AS 
BEGIN
   DECLARE @retval bit
   SELECT @retval = Habilitado FROM [JANADIAN_DATE].[Aeronave] WHERE Matricula=@matricula   
   RETURN ISNULL ( @retval , 0 )
END
GO

 /****** Funcion que devuelve cuantos viajes tiene en una fecha una aeronave  ********/
CREATE FUNCTION  [JANADIAN_DATE].[Millas_Disponibles](@id int)
RETURNS  int
AS 
BEGIN
   DECLARE @count int
	SELECT @count = SUM (Cant) FROM 
	((SELECT - SUM ([Cantidad]) as Cant FROM [GD2C2015].[JANADIAN_DATE].[Canje] WHERE Cliente=@id AND DATEDIFF(YEAR,Fecha,CURRENT_TIMESTAMP) =0)
	union
	(SELECT SUM ([Cantidad]) as Cant FROM [GD2C2015].[JANADIAN_DATE].[Millas] WHERE Cliente=@id AND DATEDIFF(YEAR,Fecha,CURRENT_TIMESTAMP) =0)
	)AS launion

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
	[Intentos] [int] NOT NULL DEFAULT 0 CHECK([Intentos]>=0),
	[Rol] [int],
		/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1 
	CONSTRAINT FK_Usuario_Rol FOREIGN KEY (Rol) REFERENCES [JANADIAN_DATE].[Rol] (Id)
	ON DELETE NO ACTION
    ON UPDATE CASCADE,
	CONSTRAINT CHK_Rol_Habilitado CHECK ([Rol] IS NULL OR [JANADIAN_DATE].[Rol_Habilitado]([Rol])=1)
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
	[Codigo] [numeric](18,0) NOT NULL,
	[Precio_BaseKG] [numeric](18,2) NOT NULL ,
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
	[KG_Disponibles] [numeric](18,0) NOT NULL CHECK([KG_Disponibles]>=0),
	[Fabricante] [int] FOREIGN KEY REFERENCES  [JANADIAN_DATE].[Fabricante] (Id) NOT NULL,
	[Tipo_Servicio] [int] FOREIGN KEY REFERENCES  [JANADIAN_DATE].[Tipo_Servicio] (Id) NOT NULL,
	[Fecha_Alta] [datetime] NOT NULL DEFAULT CURRENT_TIMESTAMP,
		/**Por defecto sin bajas ***/
	[Baja_Fuera_Servicio] [bit] NOT NULL DEFAULT 0,
	[Baja_Vida_Util] [bit] NOT NULL DEFAULT 0,
	[Fecha_Baja_Definitiva] [datetime],
	[Cant_Butacas_Ventanilla] [int]  NOT NULL  CHECK([Cant_Butacas_Ventanilla]>=0),
	[Cant_Butacas_Pasillo] [int]  NOT NULL  CHECK([Cant_Butacas_Pasillo]>=0),
		/**Por defecto habilitado ***/
	[Habilitado] [bit] NOT NULL DEFAULT 1

) ON [PRIMARY]

GO

/*****************   creacion tabla fuera servicio   *********/
IF OBJECT_ID('[JANADIAN_DATE].[Fuera_Servicio]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Fuera_Servicio](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Fecha_Baja] [datetime] NOT NULL,
	[Fecha_Reinicio] [datetime] NOT NULL,
	[Aeronave] [int] FOREIGN KEY REFERENCES [JANADIAN_DATE].[Aeronave] (Id) NOT NULL,
	CONSTRAINT CHK_Reinicion_despues_Baja CHECK ( DATEDIFF(day,[Fecha_Baja],[Fecha_Reinicio])>0)
) ON [PRIMARY]

GO

 /** Creacion de tabla butaca  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Butaca]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Butaca](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Numero] [numeric](18,0) NOT NULL,
	[Tipo] [nvarchar](255) NOT NULL,
    [Piso] [numeric](18,0) NOT NULL CHECK([Piso]>=0),
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
	--CONSTRAINT CHK_Aeronave_Disponible CHECK ([JANADIAN_DATE].[Aeronave_Habilitada]([Aeronave])=1 AND [JANADIAN_DATE].[Viajes_Fecha_Aeronave]([Aeronave],[FechaSalida])=0)
	CONSTRAINT CHK_Aeronave_Habilitada CHECK ([JANADIAN_DATE].[Aeronave_Habilitada]([Aeronave])=1),
	CONSTRAINT CHK_Aeronave_Disponible CHECK ([JANADIAN_DATE].[Viajes_Fecha_Aeronave]([Aeronave],[FechaSalida])<=1),
	[Ruta] [int],
	CONSTRAINT FK_Ruta_Viaje FOREIGN KEY (Ruta) REFERENCES [JANADIAN_DATE].[Ruta] (Id)
	ON DELETE NO ACTION
    ON UPDATE CASCADE,
	CONSTRAINT CHK_Fechas_Futuras CHECK ( DATEDIFF(day,CURRENT_TIMESTAMP,[FechaSalida])>0 and DATEDIFF(day,CURRENT_TIMESTAMP,[Fecha_Llegada_Estimada])>0),
	CONSTRAINT CHK_Mismo_Tipo_Servicio CHECK ([JANADIAN_DATE].[Get_Tipo_Servicio_Aeronave]([Aeronave]) = [JANADIAN_DATE].[Get_Tipo_Servicio_Ruta]([Ruta]) )

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
	[Dni] [numeric](18,0) NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[Apellido] [nvarchar](255) NOT NULL,
	[Dir] [nvarchar](255) NOT NULL,
	[Telefono] [numeric](18,0) NOT NULL,
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
	[Forma_Pago] [nvarchar](255) NOT NULL CHECK (Forma_Pago IN ('TC','EFECTIVO')),
	[Cliente] [int] FOREIGN KEY (Cliente) REFERENCES  [JANADIAN_DATE].[Cliente] (Id),
	CONSTRAINT FK_Compra_Viaje FOREIGN KEY (Viaje) REFERENCES [JANADIAN_DATE].[Viaje] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	[Codigo] [numeric](18,0) UNIQUE,
) ON [PRIMARY]

GO

 /** Creacion de tabla Cancelacion  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Cancelacion]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Cancelacion](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Motivo] [nvarchar](255) NOT NULL,
	[FechaDevolucion] [datetime] NOT NULL
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
	[Precio] [numeric](18,2) NOT NULL CHECK ([Precio] > 0),
	/**Por defecto activo ***/
	[Cancelado] [bit] NOT NULL DEFAULT 0,
	[Cancelacion] [int] FOREIGN KEY (Cancelacion) REFERENCES [JANADIAN_DATE].[Cancelacion] (Id),	 
	CONSTRAINT FK_Pasaje_Butaca FOREIGN KEY (Butaca) REFERENCES [JANADIAN_DATE].[Butaca] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,
	CONSTRAINT FK_Pasaje_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
) ON [PRIMARY]

GO

  /** Creacion de tabla Paquete ***/
IF OBJECT_ID('[JANADIAN_DATE].[Paquete]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Paquete](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Codigo] [numeric](18,0) NOT NULL UNIQUE,
	[KG] [numeric](18,0) CHECK ([KG]>=0),
	[Compra] [int] NOT NULL FOREIGN KEY (Compra) REFERENCES [JANADIAN_DATE].[Compra] (PNR),
	[Cliente] [int] NOT NULL,
	[Precio] [numeric](18,2) NOT NULL CHECK ([Precio] > 0),
	[Cancelacion] [int] FOREIGN KEY (Cancelacion) REFERENCES [JANADIAN_DATE].[Cancelacion] (Id),	 
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
	[Stock] [int] NOT NULL CHECK ([Stock]>=0),
	[Millas_Necesarias] [int] NOT NULL CHECK ([Millas_Necesarias]>0)

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
	[Fecha] [datetime] NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[Cantidad] [int] NOT NULL,
	[Cliente] [int] NOT NULL,
	[Motivo] [nvarchar](255) NOT NULL,
	CONSTRAINT FK_Millas_Cliente FOREIGN KEY (Cliente) REFERENCES [JANADIAN_DATE].[Cliente] (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE,

) ON [PRIMARY]

GO

/*******************************************************************************/
/******************** CREACION DE VISTAS ***************************************/
/*******************************************************************************/

 /** Creacion de vista itinerario aeronave ***/
 CREATE VIEW [JANADIAN_DATE].[Itinerario_Aeronave] AS  
 SELECT a.Matricula,v.FechaSalida,v.Fecha_Llegada_Estimada,o.Nombre as Origen,d.Nombre as Destino FROM   [JANADIAN_DATE].[Viaje] v
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

  
 /** Creacion de vista pasajes vendidos a un destino ***/
 CREATE VIEW [JANADIAN_DATE].[Pasajes_Vendidos_Destino] AS  
SELECT ci.Nombre as Destino,c.Fecha_Compra FROM JANADIAN_DATE.Pasaje p 
INNER JOIN JANADIAN_DATE.Compra c ON (c.PNR=p.Compra)
INNER JOIN JANADIAN_DATE.Viaje v ON (v.Id=c.Viaje)
INNER JOIN JANADIAN_DATE.Ruta r ON (r.Id=v.Ruta)
INNER JOIN JANADIAN_DATE.Ciudad ci ON (ci.Id=r.Ciudad_Destino)

 GO

  /** Creacion de vista clientes millas ***/
 CREATE VIEW [JANADIAN_DATE].[Clientes_Millas] AS  
SELECT DISTINCT(c.Id), (c.Nombre +' '+ c.Apellido) as Nombre,c.Dni,C.Mail,m.Fecha FROM JANADIAN_DATE.Cliente c INNER JOIN JANADIAN_DATE.Millas m ON (m.Cliente=c.Id)
 GO

  /** Creacion de vista pasajes cancelados a un destino ***/
 CREATE VIEW [JANADIAN_DATE].[Pasajes_Cancelados_Destino] AS  
SELECT ci.Nombre as Destino,c.Fecha_Compra FROM JANADIAN_DATE.Pasaje p 
INNER JOIN JANADIAN_DATE.Compra c ON (c.PNR=p.Compra)
INNER JOIN JANADIAN_DATE.Viaje v ON (v.Id=c.Viaje)
INNER JOIN JANADIAN_DATE.Ruta r ON (r.Id=v.Ruta)
INNER JOIN JANADIAN_DATE.Ciudad ci ON (ci.Id=r.Ciudad_Destino)
WHERE p.Cancelado=1
 GO

   /** Creacion de vista pasajes cancelados a un destino ***/
 CREATE VIEW [JANADIAN_DATE].[Aeronaves_Fuera_Servicio] AS  
SELECT a.Matricula,f.Fecha_Baja,f.Fecha_Reinicio FROM JANADIAN_DATE.Aeronave a 
INNER JOIN JANADIAN_DATE.Fuera_Servicio f ON (f.Aeronave=a.Id)
 GO
 
 /********************************************************************************/
/******************** CREACION DE PROCEDIMIENTOS ***************************************/
/*********************************************************************************/

/***   cancelar un pasaje  necesito pnr cod pasaje y motivo , obtengo la fecha y guardo con la relacion a id de la tabla pasaje ***/
CREATE PROCEDURE [JANADIAN_DATE].[Cancelar_Pasaje] 
	@PNR_compra int,
	@cod_pasaje [numeric](18,0),
	@motivo nvarchar(255),
	@Viaje int 

AS
BEGIN TRY
	declare @butacaPasaje int;

	BEGIN TRANSACTION
	/** consultar**/
	SELECT  @butacaPasaje=Butaca FROM [JANADIAN_DATE].[Pasaje] WHERE Id=@cod_pasaje

	/** verificar si es pasaje o paquete**/
	 IF  @butacaPasaje IS NOT NULL
		BEGIN
		/** Liberar la butaca **/
		 DELETE JANADIAN_DATE.Butaca_Viaje WHERE Butaca=@butacaPasaje AND Viaje=@Viaje
		
		/** grabamos la cancelacion en la tabla correspondiente **/
		INSERT INTO JANADIAN_DATE.Cancelacion 
		(Motivo,FechaDevolucion) 
		VALUES	
		(ISNULL (@motivo,''),CURRENT_TIMESTAMP)	

		/** Marcamos el pasaje como cancelado **/

			UPDATE  [JANADIAN_DATE].[Pasaje] SET Cancelado=1,Cancelacion=(SELECT SCOPE_IDENTITY()) WHERE Id=@cod_pasaje
		END


	/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO
/***   cancelar un pasaje  necesito pnr cod paquete y motivo , obtengo la fecha y guardo con la relacion a id de la tabla paquete ***/
CREATE PROCEDURE [JANADIAN_DATE].[Cancelar_Paquete] 
	@PNR_compra int,
	@cod_paquete [numeric](18,0),
	@motivo nvarchar(255) 

AS
BEGIN TRY
	declare @kg_paquete numeric(18,0);

	BEGIN TRANSACTION
	/** consultar**/
	SELECT  @kg_paquete=KG FROM [JANADIAN_DATE].[Paquete] WHERE Id=@cod_paquete

	/** verificar si es pasaje o paquete**/
	IF  @kg_paquete IS NOT NULL
		BEGIN
		 /** Liberar los KG **/
		
		/** grabamos la cancelacion en la tabla correspondiente **/
		INSERT INTO JANADIAN_DATE.Cancelacion 
		(Motivo,FechaDevolucion) 
		VALUES	
		(ISNULL (@motivo,''),CURRENT_TIMESTAMP)	

		/** Marcamos el paquete como cancelado **/

			UPDATE  [JANADIAN_DATE].[Paquete] SET Cancelado=1,Cancelacion=(SELECT SCOPE_IDENTITY()) WHERE Id=@cod_paquete

		END


	/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO

/*** procedimiento que cancela pasajes al dar de baja una aeronave (inhabilitar) - si tiene fecha maxima es baja fuera de servicio sino baja por fin de vida util ****/
/***        ****/
CREATE PROCEDURE [JANADIAN_DATE].[inhabilitarPasajesAeronave]
@Id_Aeronave int,
@Fecha_maxima datetime
AS
BEGIN TRY
			
	/***   RECORRER TODO LO ASOCIADO A LA AERONAVE Y CANCELARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Pasaje] ***/
		declare @pnr int;
		declare @codigo [numeric](18,0);
		declare @viaje int;

		BEGIN TRANSACTION
			IF (@Fecha_maxima IS NULL)
			BEGIN
				DECLARE ViajesEnAeronave CURSOR FOR 

					SELECT c.PNR, p.Id AS codigo,v.Id as Viaje FROM [JANADIAN_DATE].[Aeronave] a
					INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Aeronave = a.Id)
					INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
					INNER JOIN [JANADIAN_DATE].[Pasaje] p ON (p.Compra = c.PNR)
					WHERE a.Id=@Id_Aeronave and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0	

			END
			ELSE
			BEGIN

			DECLARE ViajesEnAeronave CURSOR FOR 
			SELECT c.PNR, p.Id AS codigo,v.Id as Viaje FROM [JANADIAN_DATE].[Aeronave] a
					INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Aeronave = a.Id)
					INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
					INNER JOIN [JANADIAN_DATE].[Pasaje] p ON (p.Compra = c.PNR)
					WHERE a.Id=@Id_Aeronave and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0	AND DATEDIFF(day,@Fecha_maxima,v.FechaSalida)<0

			END

		OPEN ViajesEnAeronave 
		FETCH NEXT FROM ViajesEnAeronave INTO @pnr,@codigo,@viaje
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Pasaje] @pnr,@codigo,'Baja de Aeronave',@viaje

			FETCH NEXT FROM ViajesEnAeronave INTO @pnr,@codigo,@viaje
		END
		CLOSE ViajesEnAeronave
		DEALLOCATE ViajesEnAeronave
		/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO

/*** procedure que cancela paquetes al dar de baja una aeronave (inhabilitar) - si tiene fecha maxima es baja fuera de servicio sino baja por fin de vida util****/
/***        ****/
CREATE PROCEDURE [JANADIAN_DATE].[inhabilitarPaquetesAeronave]
@Id_Aeronave int,
@Fecha_maxima datetime
AS
BEGIN TRY
			
	/***   RECORRER TODO LO ASOCIADO A LA AERONAVE Y CANCELARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Paquete] ***/
		declare @pnr int;
		declare @codigo [numeric](18,0);

		BEGIN TRANSACTION
			IF (@Fecha_maxima IS NULL)
			BEGIN
				DECLARE ViajesEnAeronave CURSOR FOR 


			SELECT c.PNR, p.Id AS codigo FROM [JANADIAN_DATE].[Aeronave] a
					INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Aeronave = a.Id)
					INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
					INNER JOIN [JANADIAN_DATE].[Paquete] p ON (p.Compra = c.PNR)
					WHERE a.Id=@Id_Aeronave and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0	

			END
			ELSE
			BEGIN

			DECLARE ViajesEnAeronave CURSOR FOR 
			SELECT c.PNR, p.Id AS codigo FROM [JANADIAN_DATE].[Aeronave] a
					INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Aeronave = a.Id)
					INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
					INNER JOIN [JANADIAN_DATE].[Paquete] p ON (p.Compra = c.PNR)
					WHERE a.Id=@Id_Aeronave and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0	AND DATEDIFF(day,@Fecha_maxima,v.FechaSalida)<0

			END

		OPEN ViajesEnAeronave 
		FETCH NEXT FROM ViajesEnAeronave INTO @pnr,@codigo
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Paquete] @pnr,@codigo,'Baja de Aeronave'
			FETCH NEXT FROM ViajesEnAeronave INTO @pnr,@codigo
		END
		CLOSE ViajesEnAeronave
		DEALLOCATE ViajesEnAeronave
		/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO

/*** procedimiento que cancela pasajes al dar cancelar una compra ****/
/***        ****/
CREATE PROCEDURE [JANADIAN_DATE].[inhabilitarPasajesCompra]
@Id_Compra int,
@motivo nvarchar(255)
AS
BEGIN TRY
			
	/***   RECORRER TODO LO ASOCIADO A LA AERONAVE Y CANCELARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Pasaje] ***/
		declare @pnr int;
		declare @codigo [numeric](18,0);
		declare @viaje int;

		BEGIN TRANSACTION
				DECLARE ViajesEnCompra CURSOR FOR 

				SELECT c.PNR, p.Id AS codigo,v.Id  FROM [JANADIAN_DATE].[Compra] c
					INNER JOIN [JANADIAN_DATE].[Pasaje] p ON (p.Compra = c.PNR)
					INNER JOIN [JANADIAN_DATE].[Viaje] v ON (c.Viaje = v.Id)
					WHERE c.PNR=@Id_Compra and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0	


		OPEN ViajesEnCompra 
		FETCH NEXT FROM ViajesEnCompra INTO @pnr,@codigo,@viaje
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Pasaje] @pnr,@codigo,@motivo,@viaje

			FETCH NEXT FROM ViajesEnCompra INTO @pnr,@codigo,@viaje
		END
		CLOSE ViajesEnCompra
		DEALLOCATE ViajesEnCompra
		/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO

/*** procedimiento que cancela paquetes al dar cancelar una compra ****/
/***        ****/
CREATE PROCEDURE [JANADIAN_DATE].[inhabilitarPaquetesCompra]
@Id_Compra int,
@motivo nvarchar(255)
AS
BEGIN TRY
			
	/***   RECORRER TODO LO ASOCIADO A LA AERONAVE Y CANCELARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Paquete] ***/
		declare @pnr int;
		declare @codigo [numeric](18,0);

		BEGIN TRANSACTION
				DECLARE ViajesEnCompra CURSOR FOR 

				SELECT c.PNR, p.Id AS codigo FROM [JANADIAN_DATE].[Compra] c
					INNER JOIN [JANADIAN_DATE].[Paquete] p ON (p.Compra = c.PNR)
					INNER JOIN [JANADIAN_DATE].[Viaje] v ON (c.Viaje = v.Id)
					WHERE c.PNR=@Id_Compra and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0	


		OPEN ViajesEnCompra 
		FETCH NEXT FROM ViajesEnCompra INTO @pnr,@codigo
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Paquete] @pnr,@codigo,@motivo
			FETCH NEXT FROM ViajesEnCompra INTO @pnr,@codigo
		END
		CLOSE ViajesEnCompra
		DEALLOCATE ViajesEnCompra
		/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO


/***  liberar las butacas_viajes con butacas viejas y 
	crear las butacas_viajes con las butacas nuevas, 
	cambiar los pasajes de esa compra para ese viaje a la butaca nueva 
	reemplaza el id aeronave en viaje ****/
/***        ****/
CREATE PROCEDURE [JANADIAN_DATE].[reemplazarAeronaveViaje]
@Id_Aeronave int,
@Id_Viaje int,
@Id_Aeronave_Reemplazo int
AS
BEGIN TRY
			
	/***   RECORRER TODAS LAS BUTACAS PARA EL VIAJE***/
		declare @butaca_anterior int;
		declare @butaca_viaje int;
		declare @num numeric(18,0);
		declare @tipo nvarchar(255);
		declare @piso numeric(18,0);
		declare @butaca_nueva int;

		BEGIN TRANSACTION
			BEGIN
				DECLARE ButacasViaje CURSOR FOR 
			SELECT  b.Id as butaca,bv.Id as butaca_viaje,b.Numero,b.Tipo,b.Piso
				FROM [GD2C2015].[JANADIAN_DATE].[Butaca_Viaje] bv 
				INNER JOIN  [GD2C2015].[JANADIAN_DATE].[Butaca] b 
				ON (b.Id = bv.Butaca AND b.Aeronave=@Id_Aeronave) WHERE Viaje=@Id_Viaje

			END			

		OPEN ButacasViaje 
		FETCH NEXT FROM ButacasViaje INTO @butaca_anterior,@butaca_viaje,@num,@tipo,@piso
		WHILE @@FETCH_STATUS=0
		BEGIN

		--crear las butacas_viajes con las butacas nuevas, 
		SELECT @butaca_nueva=Id from JANADIAN_DATE.Butaca WHERE Numero=@num AND Tipo =@tipo and Piso=@piso and Aeronave=@Id_Aeronave_Reemplazo
		INSERT INTO JANADIAN_DATE.Butaca_Viaje(Butaca,Viaje) VALUES (@butaca_nueva,@Id_Viaje) 
		
		-- liberar las butacas_viajes con butacas viejas 
		DELETE FROM JANADIAN_DATE.Butaca_Viaje WHERE Id=@butaca_viaje

	     --cambiar los pasajes de esa compra para ese viaje a la butaca nueva 
		  UPDATE JANADIAN_DATE.Pasaje SET Butaca=@butaca_nueva WHERE Butaca=@butaca_anterior AND Compra IN (  select PNR from JANADIAN_DATE.Compra WHERE Viaje=@Id_Viaje)


			FETCH NEXT FROM ButacasViaje INTO @butaca_anterior,@butaca_viaje,@num,@tipo,@piso
		END
		CLOSE ButacasViaje
		DEALLOCATE ButacasViaje

		-- Actualizar el ID de aeronave en el viaje
		UPDATE JANADIAN_DATE.Viaje SET Aeronave=@Id_Aeronave_Reemplazo WHERE Id=@Id_Viaje;
		/** aplicamos los cambios **/
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO

/***  //crear nueva aeronave
		    // copia butacas
                    // llamar a reemplazar viaje ****/
/***        ****/
CREATE PROCEDURE [JANADIAN_DATE].[crearNuevaAeronave]
@Id_Aeronave int,
@naveMat nvarchar(255),
@Id_Viaje int
AS
BEGIN TRY

		BEGIN TRANSACTION
		declare @Aeronave int;
		IF ( [JANADIAN_DATE].[Aeronave_Habilitada_Por_Matricula](@naveMat + '_2')=0)
		BEGIN
			INSERT INTO JANADIAN_DATE.Aeronave(Matricula,Fabricante,Modelo,Tipo_Servicio,KG_Disponibles,Cant_Butacas_Pasillo,Cant_Butacas_Ventanilla) 
			SELECT @naveMat + '_2' ,Fabricante,Modelo , Tipo_Servicio,KG_Disponibles,Cant_Butacas_Pasillo,Cant_Butacas_Ventanilla FROM Aeronave WHERE id=@Id_Aeronave
			SELECT @Aeronave=Id FROM [JANADIAN_DATE].[Aeronave] where Matricula=(@naveMat + '_2')
		END
		ELSE
		BEGIN
			INSERT INTO JANADIAN_DATE.Aeronave(Matricula,Fabricante,Modelo,Tipo_Servicio,KG_Disponibles,Cant_Butacas_Pasillo,Cant_Butacas_Ventanilla) 
			SELECT @naveMat + '_3' ,Fabricante,Modelo , Tipo_Servicio,KG_Disponibles,Cant_Butacas_Pasillo,Cant_Butacas_Ventanilla FROM Aeronave WHERE id=@Id_Aeronave
			SELECT @Aeronave=Id FROM [JANADIAN_DATE].[Aeronave] where Matricula=(@naveMat + '_3')

		END

			INSERT INTO JANADIAN_DATE.Butaca (Numero,Tipo,Piso,Aeronave)
			select Numero,Tipo,Piso,@Aeronave from JANADIAN_DATE.Butaca WHERE AERONAVE=@Id_Aeronave
		--copiar butacas


	/** aplicamos los cambios **/
	COMMIT TRANSACTION
	
	--reemplazamos aeronave con la nueva
	EXEC [JANADIAN_DATE].[reemplazarAeronaveViaje] @Id_Aeronave,@Id_Viaje,@Aeronave



END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO
/***   Insert de funcionalidades ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Funcionalidades] 
AS
BEGIN TRY
	BEGIN TRANSACTION
	print ' '
	print 'Insertar funcionalidades'
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('ABM_ROL')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('ABM_RUTA_AEREA')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('ABM_AERONAVE')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('GENERAR_VIAJE')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('REGISTRO_LLEGADA_DESTINO')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('COMPRA_PASAJE_ENCOMIENDA')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('CANCELACION_DEVOLUCION')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('CONSULTA_MILLAS')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('CANJE_MILLAS')
	INSERT INTO JANADIAN_DATE.Funcionalidad (Descripcion) VALUES ('ESTADISTICAS')

	/** aplicamos los cambios **/
	COMMIT TRANSACTION
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

/***   Insert de Roles ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Roles] 
AS
BEGIN TRY
	BEGIN TRANSACTION
			print ' '
			print 'Insertar Roles'
			INSERT INTO JANADIAN_DATE.Rol (Nombre) VALUES ('Administrador General')
			INSERT INTO JANADIAN_DATE.Rol (Nombre) VALUES ('Cliente')

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


/***   Insert de usuarios ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Usuarios] 
AS
BEGIN TRY
	BEGIN TRANSACTION
	print ' '
	print 'Insertar Usuario'
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('admin',LOWER(CONVERT(NVARCHAR(64),HashBytes('SHA2_256', 'w23e'),2)),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('sucursal1',LOWER(CONVERT(NVARCHAR(64),HashBytes('SHA2_256', 'w23e'),2)),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('admin2',LOWER(CONVERT(NVARCHAR(64),HashBytes('SHA2_256', 'w23e'),2)),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('sucursal2',LOWER(CONVERT(NVARCHAR(64),HashBytes('SHA2_256', 'w23e'),2)),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('invitado',LOWER(CONVERT(NVARCHAR(64),HashBytes('SHA2_256', 'w23e'),2)),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre NOT LIKE '%Admin%'))

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

/*****Inserts funcionalidades a roles ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Rol_Funcionalidades] 
AS
BEGIN TRANSACTION

	print ' '
	print 'Insertar Rol_Funcionalidad'
BEGIN TRY

/*****Inserts funcionalidades admin ****/
DECLARE @Id int
DECLARE @Admin int 
DECLARE @User int 
DECLARE @Descripcion nvarchar(255)

DECLARE db_cursor_rol_func CURSOR FOR  
SELECT Id,Descripcion
FROM JANADIAN_DATE.Funcionalidad

/* rol admin*/
SET @Admin = (select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%')

/* rol user*/
SET @User = (select top 1 id from JANADIAN_DATE.Rol WHERE Nombre NOT LIKE '%Admin%')

OPEN db_cursor_rol_func   
FETCH NEXT FROM db_cursor_rol_func INTO @Id,@Descripcion

WHILE @@FETCH_STATUS = 0   
BEGIN   
       INSERT INTO JANADIAN_DATE.Rol_Funcionalidad (Rol,Funcionalidad) VALUES (@Admin,@Id)
	   
	   IF @Descripcion  LIKE '%COMPRA_PASAJE%' OR  @Descripcion LIKE '%CONSULTA_MILLAS%'
	   BEGIN
		       INSERT INTO JANADIAN_DATE.Rol_Funcionalidad (Rol,Funcionalidad) VALUES (@User,@Id)
	   END

       FETCH NEXT FROM db_cursor_rol_func INTO @Id,@Descripcion 
END   

CLOSE db_cursor_rol_func   
DEALLOCATE db_cursor_rol_func

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

/*****Inserts CIUDADES ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Ciudades] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Ciudades'
BEGIN TRY
 
/******   ******/
INSERT INTO JANADIAN_DATE.Ciudad  (Nombre) 
SELECT distinct(
      [Ruta_Ciudad_Origen]) AS ciudad
  FROM [GD2C2015].[gd_esquema].[Maestra] UNION SELECT distinct(
     [Ruta_Ciudad_Destino])
	    FROM [GD2C2015].[gd_esquema].[Maestra]

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO


/*****Inserts Tipo_Servicio ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Tipo_Servicio] 
AS
BEGIN TRANSACTION
print ' '
print 'Insertar Tipo Servicio'
BEGIN TRY

/******   ******/
 INSERT INTO JANADIAN_DATE.Tipo_Servicio(Nombre)
SELECT 
      DISTINCT [Tipo_Servicio]
  FROM [GD2C2015].[gd_esquema].[Maestra]

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

/*****Inserts Fabricante ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Fabricantes] 
AS
BEGIN TRANSACTION
print ' '
print 'Insertar Fabricantes'
BEGIN TRY

/****** S  ******/
INSERT INTO JANADIAN_DATE.Fabricante(Nombre)
SELECT DISTINCT [Aeronave_Fabricante]
  FROM [GD2C2015].[gd_esquema].[Maestra]


COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

/***   Insert de productos con su stock para canjear ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Productos] 
AS
BEGIN TRY
	BEGIN TRANSACTION
	print ' '
	print 'Insertar Productos'
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Aire Acondicionado Split',10,12)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('TV LED 32 HD',4,11)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Maquina de cafe NESPRESSO',4,10)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Metegol estadio',9,8)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Cava Wine Collection',2,14)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Gift Card despegar.com',50,4)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Grupo electrogeno GAMMA',5,40)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Maquina cortar cesped',15,13)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Descuento viajes',10,3)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Microondas Lux',5,8)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Netbook EX',4,33)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Voucher Fallabela',150,1)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Entrada Hoyts',2000,10)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('Entradas 2x1 Temaiken',1400,21)
			INSERT INTO JANADIAN_DATE.Producto(Nombre,Stock,Millas_Necesarias) VALUES ('LG DVD',15,64)

	/** aplicamos los cambios **/
	COMMIT TRANSACTION
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

/*****Inserts Cliente ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Clientes] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Clientes'
BEGIN TRY

/****** S  ******/
INSERT INTO JANADIAN_DATE.Cliente(Nombre,Apellido,Dni,Dir,Telefono,Mail,Fecha_Nac)
SELECT DISTINCT [Cli_Nombre]
      ,[Cli_Apellido]
      ,[Cli_Dni]
      ,[Cli_Dir]
      ,[Cli_Telefono]
      ,[Cli_Mail]
      ,[Cli_Fecha_Nac]
  FROM [GD2C2015].[gd_esquema].[Maestra]

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Rutas ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Rutas] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Rutas'
BEGIN TRY

DECLARE @Codigo numeric(18,0)
DECLARE @Precio_BaseKG numeric(18,2)
DECLARE @Precio_BasePasaje numeric(18,2)
DECLARE @Ciudad_Origen int
DECLARE @Ciudad_Destino int
DECLARE @Tipo_Servicio int

DECLARE db_cursor_rutas CURSOR FOR  
/****** S ******/
  SELECT distinct
      t1.[Ruta_Codigo] as Codigo
      ,t4.id as Ciudad_Origen
      ,t5.id as Ciudad_Destino
	  ,t3.id as Servicio,
	   t1.[Ruta_Precio_BaseKG],
		 t1.[Ruta_Precio_BasePasaje]
  FROM [GD2C2015].[gd_esquema].[Maestra] t1 
    inner join [JANADIAN_DATE].[Tipo_Servicio] t3 on t3.Nombre=t1.Tipo_Servicio  
  inner join [JANADIAN_DATE].[Ciudad] t4 on t4.Nombre=t1.Ruta_Ciudad_Origen
  inner join [JANADIAN_DATE].[Ciudad] t5 on t5.Nombre=t1.Ruta_Ciudad_Destino
OPEN db_cursor_rutas   
FETCH NEXT FROM db_cursor_rutas INTO @Codigo ,@Ciudad_Origen ,@Ciudad_Destino , @Tipo_Servicio,@Precio_BaseKG,@Precio_BasePasaje 

WHILE @@FETCH_STATUS = 0   
BEGIN   

   IF NOT EXISTS (SELECT * FROM JANADIAN_DATE.Ruta 
                   WHERE Codigo=@Codigo AND Ciudad_Origen =@Ciudad_Origen AND Ciudad_Destino=@Ciudad_Destino AND Tipo_Servicio =@Tipo_Servicio)
   BEGIN
     INSERT INTO JANADIAN_DATE.Ruta(Codigo,Ciudad_Origen,Ciudad_Destino,Tipo_Servicio,Precio_BaseKG,Precio_BasePasaje) VALUES ( @Codigo ,@Ciudad_Origen ,@Ciudad_Destino , @Tipo_Servicio,@Precio_BaseKG,@Precio_BasePasaje )
   END
   ELSE
   BEGIN
	UPDATE JANADIAN_DATE.Ruta 
	SET
	  Precio_BaseKG = CASE WHEN @Precio_BaseKG <> 0.00 THEN @Precio_BaseKG ELSE Precio_BaseKG END,
	  Precio_BasePasaje = CASE WHEN @Precio_BasePasaje <> 0.00 THEN @Precio_BasePasaje ELSE Precio_BasePasaje END
    WHERE Codigo=@Codigo AND Ciudad_Origen =@Ciudad_Origen AND Ciudad_Destino=@Ciudad_Destino AND Tipo_Servicio =@Tipo_Servicio
   END
    FETCH NEXT FROM db_cursor_rutas INTO  @Codigo ,@Ciudad_Origen ,@Ciudad_Destino , @Tipo_Servicio,@Precio_BaseKG,@Precio_BasePasaje 
END   

CLOSE db_cursor_rutas   
DEALLOCATE db_cursor_rutas

COMMIT TRANSACTION
ALTER TABLE  [JANADIAN_DATE].[Ruta] ADD CONSTRAINT CK_Precios_Positivos
CHECK (Precio_BaseKG>0 AND Precio_BasePasaje >0)
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Aeronaves ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Aeronaves] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Aeronaves'
BEGIN TRY

DECLARE @Matricula nvarchar(255)
DECLARE @Modelo nvarchar(255)
DECLARE @KG_Disponibles numeric(18,0)
DECLARE @Fabricante int
DECLARE @Tipo_Servicio int
DECLARE @Butacas_Pasillo int
DECLARE @Butacas_Ventanilla int

DECLARE db_cursor_naves CURSOR FOR  
/****** S ******/
    select distinct a.Aeronave_Matricula,t3.Id as fabricante,a.Aeronave_Modelo,t2.Id as tipo_servicio,a.Aeronave_KG_Disponibles from GD2C2015.gd_esquema.Maestra a 
  inner join GD2C2015.gd_esquema.Maestra b on a.Aeronave_Matricula=b.Aeronave_Matricula
  inner join [JANADIAN_DATE].[Tipo_Servicio] t2 on t2.Nombre=a.Tipo_Servicio  
  inner join [JANADIAN_DATE].[Fabricante] t3 on t3.Nombre=a.Aeronave_Fabricante  
OPEN db_cursor_naves   
FETCH NEXT FROM db_cursor_naves INTO @Matricula ,@Fabricante,@Modelo , @Tipo_Servicio,@KG_Disponibles

WHILE @@FETCH_STATUS = 0   
BEGIN   

	SELECT @Butacas_Ventanilla = MAX(c) from (SELECT  COUNT(*) as c  FROM GD2C2015.gd_esquema.Maestra  where Aeronave_Matricula=@Matricula AND   Butaca_Tipo='Ventanilla'  group by FechaSalida) as s
	SELECT @Butacas_Pasillo = MAX(c2) from (SELECT  COUNT(*) as c2  FROM GD2C2015.gd_esquema.Maestra  where Aeronave_Matricula=@Matricula AND   Butaca_Tipo='Pasillo'  group by FechaSalida) as s2

    INSERT INTO JANADIAN_DATE.Aeronave(Matricula,Fabricante,Modelo,Tipo_Servicio,KG_Disponibles,Cant_Butacas_Pasillo,Cant_Butacas_Ventanilla) VALUES (   @Matricula ,@Fabricante,@Modelo , @Tipo_Servicio,@KG_Disponibles,@Butacas_Pasillo,@Butacas_Ventanilla )

    FETCH NEXT FROM db_cursor_naves INTO  @Matricula ,@Fabricante,@Modelo , @Tipo_Servicio,@KG_Disponibles
END   

CLOSE db_cursor_naves   
DEALLOCATE db_cursor_naves

COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Viajes ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Viajes] 
AS
BEGIN TRY
	print ' '
	print 'Insertar Viajes'
BEGIN TRANSACTION

/****** S ******/
INSERT INTO JANADIAN_DATE.Viaje(FechaSalida,FechaLlegada,Fecha_Llegada_Estimada,Aeronave,Ruta) 
 SELECT distinct
       t1.[FechaSalida]	   
	  ,t1.[FechaLLegada] 
	  ,t1.[Fecha_LLegada_Estimada] 
	  ,t2.Id as aeronave
	 , t3.id as ruta
  FROM [GD2C2015].[gd_esquema].[Maestra] t1
  inner join [GD2C2015].[JANADIAN_DATE].[Aeronave] t2 on (t1.Aeronave_Matricula=t2.Matricula)
    inner join [GD2C2015].[JANADIAN_DATE].[Ruta] t3 on (t3.Codigo=t1.Ruta_Codigo and Ciudad_Origen in (select c.id from JANADIAN_DATE.ciudad c where t1.Ruta_Ciudad_Origen=c.nombre) and Ciudad_Destino in (select c.id from JANADIAN_DATE.ciudad c where t1.Ruta_Ciudad_Destino=c.nombre))



COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Butacas ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Butacas] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Butacas'
BEGIN TRY

/****** S ******/
INSERT INTO JANADIAN_DATE.Butaca(Numero,Tipo,Piso,Aeronave)
SELECT distinct
      [Butaca_Nro]
      ,[Butaca_Tipo]
      ,[Butaca_Piso]
      ,t2.id as Aeronave
  FROM [GD2C2015].[gd_esquema].[Maestra] t
  inner join [GD2C2015].[JANADIAN_DATE].[Aeronave] t2 ON (t.Aeronave_Matricula=t2.Matricula)
  where Butaca_Piso<>0

COMMIT TRANSACTION
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO
	  /*****Inserts Butaca Viajes ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Butaca_Viajes] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Butaca_Viajes'
BEGIN TRY

/****** S  ******/
INSERT INTO JANADIAN_DATE.Butaca_Viaje(Butaca,Viaje)
SELECT distinct b.Id,v.Id
  FROM [GD2C2015].[gd_esquema].[Maestra] m
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Aeronave] a ON (a.Matricula=m.Aeronave_Matricula)
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Butaca] b ON (b.Numero=m.Butaca_Nro and b.Aeronave=a.id)
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Viaje] v ON (v.Aeronave=a.Id)

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Compras ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Compras] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Compras'
BEGIN TRY

/* CADA REGISTRO DE MAESTRA LO TOMAMOS COMO UNA COMPRA INDEPENDIENTE pagada en efectivo y realizada por autoservicio*/
INSERT INTO JANADIAN_DATE.Compra(Cliente,Precio,Fecha_Compra,Viaje,Forma_Pago,Codigo) 
SELECT c.Id as Cliente, IIF(m.Pasaje_Precio=0, m.Paquete_Precio,m.Pasaje_Precio) AS Precio,  IIF(m.Pasaje_Precio=0, m.Paquete_FechaCompra,m.Pasaje_FechaCompra) as Fecha_Compra,v.iD as Viaje,'EFECTIVO',IIF(m.Pasaje_Precio=0, m.Paquete_Codigo,m.Pasaje_Codigo) as Codigo
	 FROM [GD2C2015].[gd_esquema].[Maestra] m
	INNER JOIN [GD2C2015].[JANADIAN_DATE].[Aeronave] a ON (a.Matricula=m.Aeronave_Matricula)
	INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] c1 ON (c1.Nombre=m.Ruta_Ciudad_Origen)
	INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ciudad] c2 ON (c2.Nombre=m.Ruta_Ciudad_Destino)
	INNER JOIN [GD2C2015].[JANADIAN_DATE].[Tipo_Servicio] t ON (t.Nombre=m.Tipo_Servicio)
	INNER JOIN [GD2C2015].[JANADIAN_DATE].[Ruta] r ON (r.Codigo=m.Ruta_Codigo and r.Ciudad_Origen=c1.Id and r.Ciudad_Destino=c2.Id)
    INNER JOIN [GD2C2015].[JANADIAN_DATE].[Viaje] v ON (v.Ruta=r.Id and v.Aeronave=a.Id and v.FechaSalida=m.FechaSalida)
    INNER JOIN [GD2C2015].[JANADIAN_DATE].[Cliente] c ON (c.Nombre=m.Cli_Nombre and c.Dni=m.Cli_Dni and c.Apellido=m.Cli_Apellido)

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Paquetes ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Paquetes] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Paquetes'
BEGIN TRY

/* S*/
INSERT INTO JANADIAN_DATE.Paquete (Codigo,Precio,KG,Compra,Cliente)
	  select m.[Paquete_Codigo]
      ,m.[Paquete_Precio]
      ,m.[Paquete_KG]
      ,c.PNR as Compra
	  ,cli.Id as Cliente
  FROM [GD2C2015].[gd_esquema].[Maestra] m
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Cliente] cli ON (cli.Nombre=m.Cli_Nombre AND cli.Apellido=m.Cli_Apellido and cli.Dni=m.Cli_Dni)
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Compra] c ON (c.Codigo=m.Paquete_Codigo)

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Pasajes ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Pasajes] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Pasajes'
BEGIN TRY

/* S*/
INSERT INTO JANADIAN_DATE.Pasaje(Codigo,Precio,Butaca,Compra,Cliente)
  	  select m.[Pasaje_Codigo]
      ,m.[Pasaje_Precio]
      ,b.Id as Butaca
      ,c.PNR as Compra
	  ,cli.Id as Cliente
  FROM [GD2C2015].[gd_esquema].[Maestra] m
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Cliente] cli ON (cli.Nombre=m.Cli_Nombre AND cli.Apellido=m.Cli_Apellido and cli.Dni=m.Cli_Dni)
    INNER JOIN [GD2C2015].[JANADIAN_DATE].[Compra] c ON (c.Codigo=m.Pasaje_Codigo)
	 INNER JOIN [GD2C2015].[JANADIAN_DATE].[Aeronave] a ON (a.Matricula=m.Aeronave_Matricula)
  INNER JOIN [GD2C2015].[JANADIAN_DATE].[Viaje] v  ON (c.Viaje=v.Id AND a.Id=v.Aeronave)
  INNER JOIN  [GD2C2015].[JANADIAN_DATE].Butaca b ON ( a.Id=b.Aeronave and b.Numero=m.Butaca_Nro and b.Tipo=m.Butaca_Tipo )

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

	  /*****Inserts Millas ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Millas] 
AS
BEGIN TRANSACTION
	print ' '
	print 'Insertar Millas'
BEGIN TRY

/* S*/
INSERT INTO JANADIAN_DATE.Millas(Cantidad,Cliente,Motivo)
(SELECT 
	   FLOOR( [Precio]/10 ) AS Millas
	   ,[Cliente]
	   ,'Acumulacion millas paquete nro.' + CAST([Codigo] as nvarchar(255)) as Motivo
  FROM [GD2C2015].[JANADIAN_DATE].[Paquete]

  UNION

  SELECT 
	   FLOOR( [Precio]/10 ) AS Millas
	   ,[Cliente]
	   ,'Acumulacion millas pasaje nro.' + CAST([Codigo] as nvarchar(255)) as Motivo
  FROM [GD2C2015].[JANADIAN_DATE].[Pasaje])

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

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


/*** trigger que cancela pasajes al dar de baja una ruta (inhabilitar) ****/
/***        ****/
CREATE TRIGGER [JANADIAN_DATE].[trgInhabilitarPasajesRuta] ON  [JANADIAN_DATE].[Ruta]
FOR UPDATE
AS
BEGIN
	declare @habilitado  bit ;
	declare @id int;

	select @id=i.Id,@habilitado=i.Habilitado from inserted i;	
	
	if  @habilitado=0	
			
	/***   RECORRER TODO LO ASOCIADO A LA RUTA Y CANCELARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Pasaje] ***/
		declare @pnr int;
		declare @codigo [numeric](18,0);
		declare @viaje int

		DECLARE ViajesEnRuta CURSOR FOR 
		SELECT c.PNR, p.Id AS codigo,v.Id as viaje FROM [JANADIAN_DATE].[Ruta] r
		INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Ruta = r.Id)
		INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
		INNER JOIN [JANADIAN_DATE].[Pasaje] p ON (p.Compra = c.PNR)
		WHERE r.Id=@Id and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0		

		OPEN ViajesEnRuta 
		FETCH NEXT FROM ViajesEnRuta INTO @pnr,@codigo,@viaje
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Pasaje] @pnr,@codigo,'Baja de Ruta',@viaje
			FETCH NEXT FROM ViajesEnRuta INTO @pnr,@codigo,@viaje
		END
		CLOSE ViajesEnRuta
		DEALLOCATE ViajesEnRuta
	end
	
GO
/*** trigger que cancela encomiendas al dar de baja una ruta (inhabilitar) ****/
/***        ****/
CREATE TRIGGER [JANADIAN_DATE].[trgInhabilitarPaquetesRuta] ON  [JANADIAN_DATE].[Ruta]
FOR UPDATE
AS
BEGIN
	declare @habilitado  bit ;
	declare @id int;

	select @id=i.Id,@habilitado=i.Habilitado from inserted i;	
	
	if  @habilitado=0	
			
	/***   RECORRER TODO LO ASOCIADO A LA RUTA Y CANCELARLO LLAMANDO A [JANADIAN_DATE].[Cancelar_Paquete] ***/
		declare @pnr int;
		declare @codigo [numeric](18,0);

		DECLARE ViajesEnRuta CURSOR FOR 
		SELECT c.PNR, x.Id AS codigo FROM [JANADIAN_DATE].[Ruta] r
		INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Ruta = r.Id)
		INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
		INNER JOIN [JANADIAN_DATE].[Paquete] x ON (x.Compra = c.PNR)
		WHERE r.Id=@Id and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0		

		OPEN ViajesEnRuta 
		FETCH NEXT FROM ViajesEnRuta INTO @pnr,@codigo
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Paquete] @pnr,@codigo,'Baja de Ruta'
			FETCH NEXT FROM ViajesEnRuta INTO @pnr,@codigo
		END
		CLOSE ViajesEnRuta
		DEALLOCATE ViajesEnRuta
	end
	
GO

/*** trigger que computa las millas de los clientes al registrar la llegada ****/
/***        ****/
CREATE TRIGGER [JANADIAN_DATE].[trgComputarMillasAlLegar] ON  [JANADIAN_DATE].[Viaje]
FOR UPDATE
AS
BEGIN
	declare @Llegada  datetime ;
	declare @id int;

	select @id=i.Id,@Llegada=i.FechaLlegada from inserted i;	
	
	if  @Llegada IS NOT NULL
			
		/***   RECORRER todas las compras del viaje y calcular millas ***/
		INSERT INTO [JANADIAN_DATE].[Millas] (Cantidad,Cliente,Motivo)
		(
		select FLOOR( x.[Precio]/10 ) AS Millas,x.Cliente,'Acumulacion millas paquete nro.' + CAST(x.[Codigo] as nvarchar(255)) as Motivo  from JANADIAN_DATE.Paquete x INNER JOIN JANADIAN_DATE.Compra c ON (x.Compra=c.PNR) INNER JOIN JANADIAN_DATE.Viaje v ON (C.Viaje=v.Id) WHERE x.Cancelado = 0  and v.Id=@id
		UNION
		select FLOOR( p.[Precio]/10 ) AS Millas,p.Cliente,'Acumulacion millas pasaje nro.' + CAST(p.[Codigo] as nvarchar(255)) as Motivo  from JANADIAN_DATE.Pasaje p INNER JOIN JANADIAN_DATE.Compra c ON (p.Compra=c.PNR) INNER JOIN JANADIAN_DATE.Viaje v ON (C.Viaje=v.Id) WHERE p.Cancelado = 0 and v.Id=@id
		)

	end
	
GO

EXEC [JANADIAN_DATE].[Insertar_Funcionalidades] 
GO
EXEC [JANADIAN_DATE].[Insertar_Roles] 
GO
EXEC [JANADIAN_DATE].[Insertar_Rol_Funcionalidades] 
GO
EXEC [JANADIAN_DATE].[Insertar_Usuarios] 
GO
EXEC [JANADIAN_DATE].[Insertar_Ciudades] 
GO
EXEC [JANADIAN_DATE].[Insertar_Tipo_Servicio] 
GO
EXEC [JANADIAN_DATE].[Insertar_Fabricantes] 
GO
EXEC [JANADIAN_DATE].[Insertar_Productos] 
GO
EXEC [JANADIAN_DATE].[Insertar_Clientes] 
GO
EXEC [JANADIAN_DATE].[Insertar_Rutas] 
GO
EXEC [JANADIAN_DATE].[Insertar_Aeronaves] 
GO
EXEC [JANADIAN_DATE].[Insertar_Viajes] 
GO
EXEC [JANADIAN_DATE].[Insertar_Butacas] 
GO
EXEC [JANADIAN_DATE].[Insertar_Butaca_Viajes] 
GO
EXEC [JANADIAN_DATE].[Insertar_Compras] 
GO
EXEC [JANADIAN_DATE].[Insertar_Paquetes] 
GO
EXEC [JANADIAN_DATE].[Insertar_Pasajes] 
GO
EXEC [JANADIAN_DATE].[Insertar_Millas] 
GO
