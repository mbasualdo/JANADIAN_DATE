INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('admin',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('sucursal1',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('admin2',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))
INSERT INTO JANADIAN_DATE.Usuario (Nombre,Password,Rol) VALUES ('sucursal2',HASHBYTES('SHA2_256','w23e'),(select top 1 id from JANADIAN_DATE.Rol WHERE Nombre LIKE '%Admin%'))

GO 

--UPDATE JANADIAN_DATE.Rol SET Habilitado=0 WHERE Id=2
--UPDATE JANADIAN_DATE.Usuario SET Intentos=Intentos+1 where id=1