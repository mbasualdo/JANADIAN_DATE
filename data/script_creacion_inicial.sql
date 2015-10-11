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
	[Intentos] [int] NOT NULL DEFAULT 0 CHECK([Intentos]>=0),
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
	[Precio_BaseKG] [numeric](18,2) NOT NULL  CHECK([Precio_BaseKG]>0) ,
	[Precio_BasePasaje] [numeric](18,2) NOT NULL CHECK([Precio_BasePasaje]>0) ,
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
	[Fecha_Fuera_Servicio] [datetime],
	[Fecha_Reinicio_Servicio] [datetime],
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
	[Forma_Pago] [nvarchar](255) NOT NULL,
	[Usuario] [int] FOREIGN KEY (Usuario) REFERENCES  [JANADIAN_DATE].[Usuario] (Id),
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
	[KG] [numeric](18,0) CHECK ([KG]>=0),
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

/** creacion tabla millas canjeadas**/
IF OBJECT_ID('[JANADIAN_DATE].[Millas_Canjeadas]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Millas_Canjeadas](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Milla] [int] NOT NULL,
	[Canje] [int] NOT NULL,
	CONSTRAINT FK_Milla FOREIGN KEY (Milla) REFERENCES [JANADIAN_DATE].[Millas] (Id),
	CONSTRAINT FK_Canje FOREIGN KEY (Canje) REFERENCES [JANADIAN_DATE].[Canje] (Id),
	CONSTRAINT AK_Milla_Canje UNIQUE (Milla,Canje)
) ON [PRIMARY]

GO


 /** Creacion de tabla log  ***/
IF OBJECT_ID('[JANADIAN_DATE].[Log]') IS NULL
CREATE TABLE [JANADIAN_DATE].[Log](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Step] [nvarchar](255),
	[Date] [nvarchar](255)  DEFAULT CURRENT_TIMESTAMP,
	[Status] int,
	[Message] [nvarchar](4000)
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
  INSERT INTO Log (Step,Status,Message) VALUES ('CANCELAR PASAJE',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage,@ErrorSeverity , 1);


END CATCH;
GO

/***   Insert de funcionalidades ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Funcionalidades] 
AS
BEGIN TRY
	BEGIN TRANSACTION
	
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
	COMMIT
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR FUNCIONALIDADES',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);

END CATCH;
GO

/***   Insert de Roles ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Roles] 
AS
BEGIN TRY
	BEGIN TRANSACTION
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
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR ROLES',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);

END CATCH;
GO


/***   Insert de usuarios ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Usuarios] 
AS
BEGIN TRY
	BEGIN TRANSACTION
	

INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('admin',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('sucursal1',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('admin2',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('sucursal2',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('invitado',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre NOT LIKE '%Admin%'))

	/** aplicamos los cambios **/
	COMMIT
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR USUARIOS',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);

END CATCH;
GO

/*****Inserts funcionalidades a roles ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Rol_Funcionalidades] 
AS
BEGIN TRANSACTION

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
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR ROL FUNC',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

/*****Inserts CIUDADES ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Ciudades] 
AS
BEGIN TRANSACTION

BEGIN TRY

DECLARE @Nombre_ciudad nvarchar(255)

DECLARE db_cursor_ciudades CURSOR FOR  
/******   ******/
SELECT distinct(
      [Ruta_Ciudad_Origen]) AS ciudad
  FROM [GD2C2015].[gd_esquema].[Maestra] UNION SELECT distinct(
     [Ruta_Ciudad_Destino])
	    FROM [GD2C2015].[gd_esquema].[Maestra]

OPEN db_cursor_ciudades   
FETCH NEXT FROM db_cursor_ciudades INTO @Nombre_ciudad

WHILE @@FETCH_STATUS = 0   
BEGIN   
       INSERT INTO JANADIAN_DATE.Ciudad  (Nombre) VALUES (@Nombre_ciudad)

       FETCH NEXT FROM db_cursor_ciudades INTO @Nombre_ciudad
END   

CLOSE db_cursor_ciudades   
DEALLOCATE db_cursor_ciudades

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR CIUDADES',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO


/*****Inserts Tipo_Servicio ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Tipo_Servicio] 
AS
BEGIN TRANSACTION

BEGIN TRY

DECLARE @Tipo_servicio nvarchar(255)

DECLARE db_cursor_tipo_servicio CURSOR FOR  
/******   ******/
SELECT 
      DISTINCT [Tipo_Servicio]
  FROM [GD2C2015].[gd_esquema].[Maestra]

OPEN db_cursor_tipo_servicio   
FETCH NEXT FROM db_cursor_tipo_servicio INTO @Tipo_servicio

WHILE @@FETCH_STATUS = 0   
BEGIN   
       INSERT INTO JANADIAN_DATE.Tipo_Servicio(Nombre) VALUES (@Tipo_servicio)

       FETCH NEXT FROM db_cursor_tipo_servicio INTO @Tipo_servicio
END   

CLOSE db_cursor_tipo_servicio   
DEALLOCATE db_cursor_tipo_servicio

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR TIPO SERVICIO',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

/*****Inserts Fabricante ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Fabricantes] 
AS
BEGIN TRANSACTION

BEGIN TRY

DECLARE @Nombre_fabricante nvarchar(255)

DECLARE db_cursor_fabricante CURSOR FOR  
/****** S  ******/
SELECT DISTINCT [Aeronave_Fabricante]
  FROM [GD2C2015].[gd_esquema].[Maestra]

OPEN db_cursor_fabricante   
FETCH NEXT FROM db_cursor_fabricante INTO @Nombre_fabricante

WHILE @@FETCH_STATUS = 0   
BEGIN   
       INSERT INTO JANADIAN_DATE.Fabricante(Nombre) VALUES (@Nombre_fabricante)

       FETCH NEXT FROM db_cursor_fabricante INTO @Nombre_fabricante
END   

CLOSE db_cursor_fabricante   
DEALLOCATE db_cursor_fabricante

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR FABRICANTES',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH  

GO

/***   Insert de productos con su stock para canjear ***/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Productos] 
AS
BEGIN TRY
	BEGIN TRANSACTION
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
	COMMIT
END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR PRODUCTOS',@ErrorSeverity,@ErrorMessage);
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);

END CATCH;
GO

/*****Inserts Cliente ****/
CREATE PROCEDURE [JANADIAN_DATE].[Insertar_Clientes] 
AS
BEGIN TRANSACTION

BEGIN TRY

DECLARE @Nombre_cliente nvarchar(255)
DECLARE @Apellido_cliente nvarchar(255)
DECLARE @Dni_cliente nvarchar(255)
DECLARE @Dir_cliente nvarchar(255)
DECLARE @Telefono_cliente nvarchar(255)
DECLARE @Mail_cliente nvarchar(255)
DECLARE @Fecha_Nac_cliente nvarchar(255)

DECLARE db_cursor_cliente CURSOR FOR  
/****** S  ******/
SELECT DISTINCT [Cli_Nombre]
      ,[Cli_Apellido]
      ,[Cli_Dni]
      ,[Cli_Dir]
      ,[Cli_Telefono]
      ,[Cli_Mail]
      ,[Cli_Fecha_Nac]
  FROM [GD2C2015].[gd_esquema].[Maestra]

OPEN db_cursor_cliente   
FETCH NEXT FROM db_cursor_cliente INTO @Nombre_cliente,@Apellido_cliente, @Dni_cliente, @Dir_cliente,@Telefono_cliente,@Mail_cliente,@Fecha_Nac_cliente

WHILE @@FETCH_STATUS = 0   
BEGIN   
    INSERT INTO JANADIAN_DATE.Cliente(Nombre,Apellido,Dni,Dir,Telefono,Mail,Fecha_Nac) VALUES ( @Nombre_cliente,@Apellido_cliente, @Dni_cliente, @Dir_cliente,@Telefono_cliente,@Mail_cliente,@Fecha_Nac_cliente)
    FETCH NEXT FROM db_cursor_cliente INTO @Nombre_cliente,@Apellido_cliente, @Dni_cliente, @Dir_cliente,@Telefono_cliente,@Mail_cliente,@Fecha_Nac_cliente
END   

CLOSE db_cursor_cliente   
DEALLOCATE db_cursor_cliente

COMMIT TRANSACTION

END TRY
BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK

  -- INFO DE ERROR.
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();
  INSERT INTO Log (Step,Status,Message) VALUES ('INSERTAR CLIENTES',@ErrorSeverity,@ErrorMessage);
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


/*** trigger que cancela pasajes y encomiendas al dar de baja una ruta (inhabilitar) ****/
/***        ****/
CREATE TRIGGER [JANADIAN_DATE].[trgInhabilitarRuta] ON  [JANADIAN_DATE].[Ruta]
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

		DECLARE ViajesEnRuta CURSOR FOR 
		SELECT c.PNR, ISNULL(p.Codigo,x.Codigo) AS codigo FROM [JANADIAN_DATE].[Ruta] r
		INNER JOIN [JANADIAN_DATE].[Viaje] v ON (v.Ruta = r.Id)
		INNER JOIN [JANADIAN_DATE].[Compra] c ON (c.Viaje = v.Id)
		INNER JOIN [JANADIAN_DATE].[Pasaje] p ON (p.Compra = c.PNR)
		INNER JOIN [JANADIAN_DATE].[Paquete] x ON (x.Compra = c.PNR)
		WHERE r.Id=@Id and DATEDIFF(day,CURRENT_TIMESTAMP,v.FechaSalida)>0		

		OPEN ViajesEnRuta 
		FETCH NEXT FROM ViajesEnRuta INTO @pnr,@codigo
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC [JANADIAN_DATE].[Cancelar_Pasaje] @pnr,@codigo,'Baja de Ruta'
			FETCH NEXT FROM ViajesEnRuta
		END
		CLOSE ViajesEnRuta
		DEALLOCATE ViajesEnRuta
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