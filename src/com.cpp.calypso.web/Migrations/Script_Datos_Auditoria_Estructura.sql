USE [com.cpp.calypso]
GO

/****** Objeto: Table [SCH_USUARIOS].[AuditoriaEntidad] Fecha del script: 15/10/2018 20:24:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [SCH_USUARIOS].[AuditoriaEntidad] (
    [AuditEntryID]   INT            IDENTITY (1, 1) NOT NULL,
    [EntitySetName]  NVARCHAR (255) NULL,
    [EntityTypeName] NVARCHAR (255) NULL,
    [State]          INT            NOT NULL,
    [StateName]      NVARCHAR (255) NULL,
    [CreatedBy]      NVARCHAR (255) NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [Id]             INT            NULL,
    [ObjectType]     NVARCHAR (128) NULL
);



CREATE TABLE [SCH_USUARIOS].[AuditoriaPropiedad] (
    [AuditEntryPropertyID] INT            IDENTITY (1, 1) NOT NULL,
    [AuditEntryID]         INT            NOT NULL,
    [RelationName]         NVARCHAR (255) NULL,
    [PropertyName]         NVARCHAR (255) NULL,
    [OldValue]             NVARCHAR (MAX) NULL,
    [NewValue]             NVARCHAR (MAX) NULL,
    [Id]                   INT            NULL,
    [ObjectType]           NVARCHAR (128) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AuditEntryID]
    ON [SCH_USUARIOS].[AuditoriaPropiedad]([AuditEntryID] ASC);


GO
ALTER TABLE [SCH_USUARIOS].[AuditoriaPropiedad]
    ADD CONSTRAINT [PK_SCH_USUARIOS.AuditoriaPropiedad] PRIMARY KEY CLUSTERED ([AuditEntryPropertyID] ASC);


GO
ALTER TABLE [SCH_USUARIOS].[AuditoriaPropiedad]
    ADD CONSTRAINT [FK_SCH_USUARIOS.AuditoriaPropiedad_SCH_USUARIOS.AuditoriaEntidad_AuditEntryID] FOREIGN KEY ([AuditEntryID]) REFERENCES [SCH_USUARIOS].[AuditoriaEntidad] ([AuditEntryID]) ON DELETE CASCADE;



