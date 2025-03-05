
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping [dbo].[DF_PortalContato_MsgRespondida]...';


GO
ALTER TABLE [dbo].[PortalContato] DROP CONSTRAINT [DF_PortalContato_MsgRespondida];


GO
PRINT N'Dropping [dbo].[DF_PortalContato_MsgLida]...';


GO
ALTER TABLE [dbo].[PortalContato] DROP CONSTRAINT [DF_PortalContato_MsgLida];


GO
PRINT N'Starting rebuilding table [dbo].[PortalContato]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_PortalContato] (
    [IdContato]    INT           IDENTITY (1, 1) NOT NULL,
    [NomeContato]  VARCHAR (250) NULL,
    [EmailContato] VARCHAR (250) NULL,
    [Assunto]      VARCHAR (250) NULL,
    [Mensagem]     VARCHAR (MAX) NULL,
    [DataEnvio]    DATE          NULL,
    [MsgSituacao]  INT           NULL,
    [DataResposta] DATE          NULL,
    [Observacoes]  VARCHAR (MAX) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_PortalContato1] PRIMARY KEY CLUSTERED ([IdContato] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[PortalContato])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PortalContato] ON;
        INSERT INTO [dbo].[tmp_ms_xx_PortalContato] ([IdContato], [NomeContato], [EmailContato], [Assunto], [Mensagem], [DataEnvio], [DataResposta], [Observacoes])
        SELECT   [IdContato],
                 [NomeContato],
                 [EmailContato],
                 [Assunto],
                 [Mensagem],
                 [DataEnvio],
                 [DataResposta],
                 [Observacoes]
        FROM     [dbo].[PortalContato]
        ORDER BY [IdContato] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PortalContato] OFF;
    END

DROP TABLE [dbo].[PortalContato];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_PortalContato]', N'PortalContato';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_PortalContato1]', N'PK_PortalContato', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[PortalContato].[IX_PortalContato]...';


GO
CREATE NONCLUSTERED INDEX [IX_PortalContato]
    ON [dbo].[PortalContato]([DataEnvio] ASC);


GO
PRINT N'Update complete.';


GO
