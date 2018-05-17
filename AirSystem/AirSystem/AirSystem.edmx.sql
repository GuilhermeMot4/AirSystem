
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/07/2018 19:10:35
-- Generated from EDMX file: E:\SENAI\Modulo2\C#\AirSystem\AirSystem\AirSystem.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [model];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Fabricante]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fabricante];
GO
IF OBJECT_ID(N'[dbo].[Aeronave]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Aeronave];
GO
IF OBJECT_ID(N'[dbo].[Modelo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modelo];
GO
IF OBJECT_ID(N'[dbo].[Companhia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companhia];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Fabricante'
CREATE TABLE [dbo].[Fabricante] (
    [id_fabricante] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [sede] nvarchar(max)  NOT NULL,
    [pais_origem] nvarchar(max)  NOT NULL,
    [telefone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Aeronave'
CREATE TABLE [dbo].[Aeronave] (
    [id_aeronave] int IDENTITY(1,1) NOT NULL,
    [id_modelo] int  NOT NULL,
    [id_fabricante] int  NOT NULL,
    [id_companhia] nvarchar(max)  NOT NULL,
    [cor] nvarchar(max)  NOT NULL,
    [descricao] nvarchar(max)  NOT NULL,
    [som] varbinary(max)  NOT NULL,
    [foto] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Modelo'
CREATE TABLE [dbo].[Modelo] (
    [id_modelo] int IDENTITY(1,1) NOT NULL,
    [modelo] nvarchar(max)  NOT NULL,
    [qtd_assento] int  NOT NULL,
    [origem] nvarchar(max)  NOT NULL,
    [fabricacao] nvarchar(max)  NOT NULL,
    [turbinas] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Companhia'
CREATE TABLE [dbo].[Companhia] (
    [id_companhia] int IDENTITY(1,1) NOT NULL,
    [companhia] nvarchar(max)  NOT NULL,
    [cnpj] nvarchar(max)  NOT NULL,
    [cidade] nvarchar(max)  NOT NULL,
    [estado] nvarchar(max)  NOT NULL,
    [telefone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [foto] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [id_usuario] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [sobrenome] nvarchar(max)  NOT NULL,
    [endereco] nvarchar(max)  NOT NULL,
    [nascimento] nvarchar(max)  NOT NULL,
    [numero] nvarchar(max)  NOT NULL,
    [usuario] nvarchar(max)  NOT NULL,
    [senha] nvarchar(max)  NOT NULL,
    [foto] varbinary(max)  NOT NULL,
    [tipo_usuario] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_fabricante] in table 'Fabricante'
ALTER TABLE [dbo].[Fabricante]
ADD CONSTRAINT [PK_Fabricante]
    PRIMARY KEY CLUSTERED ([id_fabricante] ASC);
GO

-- Creating primary key on [id_aeronave] in table 'Aeronave'
ALTER TABLE [dbo].[Aeronave]
ADD CONSTRAINT [PK_Aeronave]
    PRIMARY KEY CLUSTERED ([id_aeronave] ASC);
GO

-- Creating primary key on [id_modelo] in table 'Modelo'
ALTER TABLE [dbo].[Modelo]
ADD CONSTRAINT [PK_Modelo]
    PRIMARY KEY CLUSTERED ([id_modelo] ASC);
GO

-- Creating primary key on [id_companhia] in table 'Companhia'
ALTER TABLE [dbo].[Companhia]
ADD CONSTRAINT [PK_Companhia]
    PRIMARY KEY CLUSTERED ([id_companhia] ASC);
GO

-- Creating primary key on [id_usuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([id_usuario] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------