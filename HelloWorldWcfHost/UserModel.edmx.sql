
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/31/2017 19:33:43
-- Generated from EDMX file: D:\Tri Le`\Documents\Visual Studio 2015\Projects\WCFHello-master\HelloWorld\HelloWorldWcfHost\UserModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Account];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfoTableEntities'
CREATE TABLE [dbo].[UserInfoTableEntities] (
    [ID] bigint  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Email] nvarchar(100)  NULL,
    [Password] nvarchar(50)  NULL,
    [First_Name] char(50)  NULL,
    [Last_Name] char(50)  NULL,
    [Sex] char(10)  NULL,
    [StreetAddress] nvarchar(100)  NULL,
    [State] char(50)  NULL,
    [ZipCode] nvarchar(50)  NULL,
    [Country] nvarchar(50)  NULL,
    [LastLogin] datetime  NULL,
    [LastActive] binary(8)  NULL,
    [AccountDateCreation] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'UserInfoTableEntities'
ALTER TABLE [dbo].[UserInfoTableEntities]
ADD CONSTRAINT [PK_UserInfoTableEntities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------