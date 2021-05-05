

USE RenoExpress
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rol](
	[Rol_Id] [int] IDENTITY(1,1) NOT NULL,
	[Rol_Descripcion] [varchar](100) NOT NULL,
	[Rol_Usuario_Creacion] [varchar](100) NOT NULL,
	[Rol_Fecha_Creacion] [datetime] NOT NULL,
	[Rol_Usuario_Modificacion] [varchar](100) NULL,
	[Rol_Fecha_Modificacion] [datetime] NULL,
	[Rol_Estado] [bit] NULL,
	[Rol_Eliminado] [bit] NULL,
 CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED 
(
	[Rol_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario](
	[Usuario_Usuario] [varchar](100) NOT NULL,
	[Usuario_Contrasenia] [varchar](250) NOT NULL,
	[Usuario_Nombres] [varchar](100) NOT NULL,
	[Usuario_Apellidos] [varchar](100) NOT NULL,
	[Usuario_Correo_Electronico] [varchar](100) NOT NULL,
	[Usuario_Rol_Id] [int] NOT NULL,
	[Usuario_Fecha_Creacion] [datetime] NOT NULL,
	[Usuario_Usuario_Creacion] [varchar](100) NOT NULL,
	[Usuario_Fecha_Modificacion] [datetime] NULL,
	[Usuario_Usuario_Modificacion] [varchar](100) NULL,
	[Usuario_Estado] [bit] NULL,
	[Usuario_Eliminado] [bit] NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[Usuario_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([Usuario_Rol_Id])
REFERENCES [dbo].[Rol] ([Rol_Id])
GO

ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_usuario_rol]
GO







SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categoria](
	[Categoria_Id] [int] IDENTITY(1,1) NOT NULL,
	[Categoria_Descripcion] [varchar](100) NOT NULL,
	[Categoria_Usuario_Creacion] [varchar](100) NOT NULL,
	[Categoria_Fecha_Creacion] [datetime] NOT NULL,
	[Categoria_Usuario_Modificacion] [varchar](100) NULL,
	[Categoria_Fecha_Modificacion] [datetime] NULL,
	[Categoria_Estado] [bit] NULL,
	[Categoria_Eliminado] [bit] NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Categoria_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Producto](
	[Producto_Codigo] [int] NOT NULL,
	[Producto_Descripcion] [varchar](250) NOT NULL,
	[Producto_Marca] [varchar](100) NOT NULL,
	[Producto_Precio_Unitario] [decimal](12,2) NOT NULL,
	[Producto_Cantidad] [int] NOT NULL,
	[Producto_Categoria_Id] [int] NOT NULL,
	[Producto_Fecha_Ultimo_Abastecimiento] [datetime] NULL,
	[Producto_Cantidad_Ultimo_Abastecimiento] [int] NULL,
	[Producto_Fecha_Creacion] [datetime] NOT NULL,
	[Producto_Usuario_Creacion] [varchar](100) NOT NULL,
	[Producto_Fecha_Modificacion] [datetime] NULL,
	[Producto_Usuario_Modificacion] [varchar](100) NULL,
	[Producto_Estado] [bit] NULL,
	[Producto_Eliminado] [bit] NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[Producto_Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Producto] WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([Producto_Categoria_Id])
REFERENCES [dbo].[Categoria] ([Categoria_Id])
GO

ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO






SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_InicioSesion]  
	@usuario [varchar](100)  
WITH EXECUTE AS CALLER  
AS  
BEGIN  
  
 SELECT   
  Usuario_Usuario as usuario,  
  Usuario_Contrasenia as contrasenia,  
  Usuario_Nombres as nombres,  
  Usuario_Apellidos as apellidos,  
  Usuario_Correo_Electronico as correoelectronico,  
  Rol_Id AS idrol,  
  Rol_Descripcion AS rol 
 FROM  
  dbo.Usuario INNER JOIN dbo.Rol ON Usuario_Rol_Id = Rol_Id  
 WHERE  
  Usuario_Usuario = @usuario AND Usuario_Estado = 1 AND Usuario_Eliminado = 0 
  
END  






SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProducto]  
	@ProductoID INT = 0

WITH EXECUTE AS CALLER  
AS  
BEGIN  
  
 SELECT [Producto_Codigo]
      ,[Producto_Descripcion]
      ,[Producto_Marca]
      ,[Producto_Precio_Unitario]
      ,[Producto_Cantidad]
      ,Categoria_Id
	  ,Categoria_Descripcion
      ,[Producto_Fecha_Ultimo_Abastecimiento]
      ,[Producto_Cantidad_Ultimo_Abastecimiento]
  FROM [dbo].[Producto]
  INNER JOIN dbo.Categoria ON [Producto_Categoria_Id] = Categoria_Id
  WHERE [Producto_Estado] = 1 AND [Producto_Eliminado] = 0
	AND [Producto_Codigo] = (CASE 
								WHEN @ProductoID != 0
									THEN @ProductoID
								ELSE
									[Producto_Codigo]

							END);

  
END  






SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_PostProducto]  
	@Codigo INT,
	@Descripcion VARCHAR(250),
	@Marca VARCHAR(100),
	@PrecioU [decimal](12,2),
	@Cantidad [int],
	@Categoria int,
	@Usuario [varchar](100)

WITH EXECUTE AS CALLER  
AS  
BEGIN  
  
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
           (@Codigo
           ,@Descripcion
           ,@Marca
           ,@PrecioU
           ,@Cantidad
           ,@Categoria
           ,GETDATE()
           ,@Cantidad
           ,GETDATE()
           ,@Usuario
           ,NULL
           ,NULL
           ,1
           ,0);

	IF (@@ROWCOUNT > 0)
    BEGIN
        SELECT 1 Result, 'Registro agregado exitosamente' AS RespMessage;
    END;
    ELSE
    BEGIN
        SELECT 0 Result, 'No ha sido posible agregar el registro' AS RespMessage;
    END;
  
END  









SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_PutProducto]  
	@Codigo INT,
	@Opcion INT, -- 1 = Compra,  2 = Venta
	@Cantidad [int],
	@Usuario [varchar](100)

WITH EXECUTE AS CALLER  
AS  
BEGIN  
  
UPDATE [dbo].[Producto]
   SET [Producto_Fecha_Ultimo_Abastecimiento] = (CASE WHEN [Producto_Fecha_Modificacion] IS NOT NULL THEN [Producto_Fecha_Modificacion]
													ELSE [Producto_Fecha_Creacion]
												END)
      ,[Producto_Cantidad_Ultimo_Abastecimiento] = [Producto_Cantidad]
      ,[Producto_Cantidad] = (CASE WHEN @Opcion = 1 THEN [Producto_Cantidad] + @Cantidad
									WHEN @Opcion = 2 THEN [Producto_Cantidad] - @Cantidad
							END) 
      ,[Producto_Fecha_Modificacion] = GETDATE()
      ,[Producto_Usuario_Modificacion] = @Usuario
 WHERE [Producto_Codigo] = @Codigo AND [Producto_Estado] = 1 AND [Producto_Eliminado] = 0

	IF (@@ROWCOUNT > 0)
    BEGIN
        SELECT 1 Result, 'Registro actualizado exitosamente' AS RespMessage;
    END;
    ELSE
    BEGIN
        SELECT 0 Result, 'No ha sido posible actualizar el registro' AS RespMessage;
    END;
  
END  






