CREATE DATABASE [CakeShop]

USE [CakeShop]

GO

CREATE TABLE [dbo].[Products](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[NameProduct] [varchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Dimensions] [varchar](255) NOT NULL,
	[AcquisitionDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[ImageUrl] [varchar](255) NOT NULL,
	[Description] [varchar](1000) NULL,
	);

	GO

CREATE TABLE [dbo].[PushesOrder](
	[IdOrder] [int] IDENTITY(1,1) NOT NULL,
	[IdProduct] [int] NULL,
	[Names] [varchar](255) NOT NULL,
	[LastNames] [varchar](255) NOT NULL,
	[DUI] [varchar](10) NOT NULL,
	[Adress] [varchar](255) NOT NULL,
	[Phone] [varchar](10) NOT NULL,
	[Amount] [int] NOT NULL,
	[Dimension] [varchar](255) NULL,
	[ReservationDate] [datetime] NOT NULL,
	[DeliverDate] [datetime] NOT NULL,
	[Dedication] [varchar](100) NULL,
	[Details] [varchar](255) NULL,
	[State] [tinyint] NOT NULL,
	[Correo] [varchar](100) NULL,
	);

	GO

CREATE TABLE [dbo].[Rol](
	[RolId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Estado] [tinyint] NOT NULL,
	);

	GO

CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[RolId] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Contraseña] [varchar](255) NOT NULL,
	[Estado] [tinyint] NOT NULL,
	);