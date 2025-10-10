IF DB_ID(N'Apuestas') IS NULL
BEGIN
    CREATE DATABASE [Apuestas];
END
GO

USE [Apuestas];
GO

IF OBJECT_ID(N'[dbo].[bitacora]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[bitacora]
    (
        [id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [fecha] DATETIME NOT NULL,
        [liga] VARCHAR(100) NULL,
        [partido] VARCHAR(100) NULL,
        [importe] DECIMAL(18, 2) NULL,
        [ganancia] DECIMAL(18, 2) NULL,
        [tipo] VARCHAR(1000) NULL,
        [resultado] VARCHAR(2) NULL,
        [cuota] DECIMAL(18, 2) NULL,
        [nota] VARCHAR(1000) NULL,
        [antesDurante] VARCHAR(1) NULL,
        [tipster] VARCHAR(50) NULL
    );
END
GO
