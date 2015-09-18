/*****Inserts funcionalidades a roles ****/
BEGIN TRANSACTION [Asignar_Funcionalidades_a_Rol]

BEGIN TRY

/*****Inserts funcionalidades admin ****/
DECLARE @Id int
DECLARE @Admin int 
DECLARE db_cursor CURSOR FOR  
SELECT Id
FROM JANADIAN_DATE.Funcionalidad


SET @Admin = (select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%')

OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @Id

WHILE @@FETCH_STATUS = 0   
BEGIN   
       INSERT INTO JANADIAN_DATE.Rol_Funcionalidad (Rol,Funcionalidad) VALUES (@Admin,@Id)

       FETCH NEXT FROM db_cursor INTO @Id  
END   

CLOSE db_cursor   
DEALLOCATE db_cursor

COMMIT TRANSACTION [Asignar_Funcionalidades_a_Rol]

END TRY
BEGIN CATCH
  ROLLBACK TRANSACTION [Asignar_Funcionalidades_a_Rol]
END CATCH  

GO

/*****Inserts funcionalidades user ****/
INSERT INTO JANADIAN_DATE.Rol_Funcionalidad (Rol,Funcionalidad) VALUES ((select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Cliente%'),(select top 1 id from JANADIAN_DATE.Funcionalidad WHERE Descripcion LIKE '%COMPRA_PASAJE%'))
INSERT INTO JANADIAN_DATE.Rol_Funcionalidad (Rol,Funcionalidad) VALUES ((select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Cliente%'),(select top 1 id from JANADIAN_DATE.Funcionalidad WHERE Descripcion LIKE '%CONSULTA_MILLAS%'))

GO
