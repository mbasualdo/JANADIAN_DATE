USE [GD2C2015]
GO

DROP VIEW JANADIAN_DATE.Itinerario_Aeronave
DROP VIEW JANADIAN_DATE.Viaje_Disponible

DROP PROCEDURE JANADIAN_DATE.Cancelar_Pasaje
DROP PROCEDURE JANADIAN_DATE.Insertar_Funcionalidades
DROP PROCEDURE JANADIAN_DATE.Insertar_Roles
DROP PROCEDURE JANADIAN_DATE.Insertar_Rol_Funcionalidades
DROP PROCEDURE JANADIAN_DATE.Insertar_Ciudades
DROP PROCEDURE JANADIAN_DATE.Insertar_Tipo_Servicio
DROP PROCEDURE JANADIAN_DATE.Insertar_Usuarios
DROP PROCEDURE JANADIAN_DATE.Insertar_Fabricantes
DROP PROCEDURE JANADIAN_DATE.Insertar_Productos
DROP PROCEDURE JANADIAN_DATE.Insertar_Clientes
DROP PROCEDURE JANADIAN_DATE.Insertar_Rutas
DROP PROCEDURE JANADIAN_DATE.Insertar_Aeronaves
DROP PROCEDURE JANADIAN_DATE.Insertar_Butacas
DROP PROCEDURE JANADIAN_DATE.Insertar_Viajes
DROP PROCEDURE JANADIAN_DATE.Insertar_Butaca_Viajes
DROP PROCEDURE JANADIAN_DATE.Insertar_Compras
DROP PROCEDURE JANADIAN_DATE.Insertar_Paquetes
DROP PROCEDURE JANADIAN_DATE.Insertar_Pasajes
DROP PROCEDURE JANADIAN_DATE.Insertar_Millas

DROP TABLE JANADIAN_DATE.Rol_Funcionalidad
DROP TABLE JANADIAN_DATE.Funcionalidad
DROP TABLE JANADIAN_DATE.Butaca_Viaje
DROP TABLE JANADIAN_DATE.Datos_Tarjeta
DROP TABLE JANADIAN_DATE.Millas_Canjeadas
DROP TABLE JANADIAN_DATE.Canje
DROP TABLE JANADIAN_DATE.Millas
DROP TABLE JANADIAN_DATE.Producto
DROP TABLE JANADIAN_DATE.Cancelacion
DROP TABLE JANADIAN_DATE.Pasaje
DROP TABLE JANADIAN_DATE.Paquete
DROP TABLE JANADIAN_DATE.Cliente
DROP TABLE JANADIAN_DATE.Compra
DROP TABLE JANADIAN_DATE.Usuario
DROP TABLE JANADIAN_DATE.Rol
DROP TABLE JANADIAN_DATE.Viaje


DROP TABLE JANADIAN_DATE.Ruta
DROP TABLE JANADIAN_DATE.Ciudad
DROP TABLE JANADIAN_DATE.Butaca
DROP TABLE JANADIAN_DATE.Fuera_Servicio
DROP TABLE JANADIAN_DATE.Aeronave
DROP TABLE JANADIAN_DATE.Fabricante
DROP TABLE JANADIAN_DATE.Tipo_Servicio

DROP FUNCTION JANADIAN_DATE.Rol_Habilitado
DROP FUNCTION JANADIAN_DATE.Aeronave_Habilitada
DROP FUNCTION JANADIAN_DATE.Viajes_Fecha_Aeronave
DROP FUNCTION JANADIAN_DATE.Get_Tipo_Servicio_Aeronave
DROP FUNCTION JANADIAN_DATE.Get_Tipo_Servicio_Ruta
DROP FUNCTION JANADIAN_DATE.Aeronave_Habilitada_Por_Matricula

DROP SCHEMA JANADIAN_DATE