/****** Script para el comando SelectTopNRows de SSMS  ******/
SELECT distinct(
      [Ruta_Ciudad_Origen])
  FROM [GD2C2015].[gd_esquema].[Maestra] UNION SELECT distinct(
     [Ruta_Ciudad_Destino])
	    FROM [GD2C2015].[gd_esquema].[Maestra]