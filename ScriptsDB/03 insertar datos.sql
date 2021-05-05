USE [RenoExpress]
GO

INSERT INTO [dbo].[Rol]
           ([Rol_Descripcion]
           ,[Rol_Usuario_Creacion]
           ,[Rol_Fecha_Creacion]
           ,[Rol_Usuario_Modificacion]
           ,[Rol_Fecha_Modificacion]
           ,[Rol_Estado]
           ,[Rol_Eliminado])
     VALUES
           ('General'
           ,'user@correo.com'
           ,GETDATE()
           ,NULL
           ,NULL
           ,1
           ,0);


INSERT INTO [dbo].[Usuario]
           ([Usuario_Usuario]
           ,[Usuario_Contrasenia]
           ,[Usuario_Nombres]
           ,[Usuario_Apellidos]
           ,[Usuario_Correo_Electronico]
           ,[Usuario_Rol_Id]
           ,[Usuario_Fecha_Creacion]
           ,[Usuario_Usuario_Creacion]
           ,[Usuario_Fecha_Modificacion]
           ,[Usuario_Usuario_Modificacion]
           ,[Usuario_Estado]
           ,[Usuario_Eliminado])
     VALUES
           ('user@correo.com'
           ,'r/sZ03A/vjjCcjz9jBDpXw=='
           ,'user'
           ,'user'
           ,'user@correo.com'
           ,1
           ,GETDATE()
           ,'admin@correo.com'
           ,NULL
           ,NULL
           ,1
           ,0);





	INSERT INTO [dbo].[Categoria]
           ([Categoria_Descripcion]
           ,[Categoria_Usuario_Creacion]
           ,[Categoria_Fecha_Creacion]
           ,[Categoria_Usuario_Modificacion]
           ,[Categoria_Fecha_Modificacion]
           ,[Categoria_Estado]
           ,[Categoria_Eliminado])
     VALUES
           ('Snacks'
           ,'user@correo.com'
           ,GETDATE()
           ,NULL
           ,NULL
           ,1
           ,0);



	INSERT INTO [dbo].[Producto]
           ([Producto_Codigo]
           ,[Producto_Descripcion]
           ,[Producto_Marca]
           ,[Producto_Precio_Unitario]
           ,[Producto_Cantidad]
           ,[Producto_Categoria_Id]
           ,[Producto_Fecha_Ultimo_Abastecimiento]
           ,[Producto_Cantidad_Ultimo_Abastecimiento]
           ,[Producto_Fecha_Creacion]
           ,[Producto_Usuario_Creacion]
           ,[Producto_Fecha_Modificacion]
           ,[Producto_Usuario_Modificacion]
           ,[Producto_Estado]
           ,[Producto_Eliminado])
     VALUES
           (001
           ,'Galletas'
           ,'ROSE'
           ,10.00
           ,100
           ,1
           ,GETDATE()
           ,100
           ,GETDATE()
           ,'user@correo.com'
           ,NULL
           ,NULL
           ,1
           ,0),

		   (002
           ,'Nachos'
           ,'NACHO'
           ,3.00
           ,100
           ,1
           ,GETDATE()
           ,100
           ,GETDATE()
           ,'user@correo.com'
           ,NULL
           ,NULL
           ,1
           ,0),

		   (003
           ,'Chocolate'
           ,'ROSE'
           ,6.00
           ,100
           ,1
           ,GETDATE()
           ,100
           ,GETDATE()
           ,'user@correo.com'
           ,NULL
           ,NULL
           ,1
           ,0)

GO


