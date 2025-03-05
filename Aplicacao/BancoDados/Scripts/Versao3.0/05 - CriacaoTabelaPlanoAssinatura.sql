SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping [dbo].[DF_PlanosAssinaturas_Ativo]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas] DROP CONSTRAINT [DF_PlanosAssinaturas_Ativo];


GO
PRINT N'Dropping [dbo].[DF_PlanosAssinaturas_DataCadastro]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas] DROP CONSTRAINT [DF_PlanosAssinaturas_DataCadastro];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];


GO
PRINT N'Starting rebuilding table [dbo].[PlanosAssinaturas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_PlanosAssinaturas] (
    [IdPlano]        INT          IDENTITY (1, 1) NOT NULL,
    [IdPlanoPagarMe] INT          NULL,
    [dNomePlano]     INT          NULL,
    [ValorPlano]     MONEY        NULL,
    [dPlanoTp]       INT          NULL,
    [dPeriodo]       INT          NULL,
    [QtdAnimais]     INT          NULL,
    [Ativo]          BIT          CONSTRAINT [DF_PlanosAssinaturas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]     INT          NULL,
    [IP]             VARCHAR (20) NULL,
    [DataCadastro]   DATETIME     CONSTRAINT [DF_PlanosAssinaturas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]         ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_PlanosAssinaturas1] PRIMARY KEY CLUSTERED ([IdPlano] ASC)
	--,
 --   CONSTRAINT [tmp_ms_xx_constraint_IX_PlanosAssinaturas1] UNIQUE NONCLUSTERED ([dNomePlano] ASC, [dPeriodo] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[PlanosAssinaturas])
    BEGIN
        --SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PlanosAssinaturas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_PlanosAssinaturas] (/*[IdPlano], */[QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   /*[IdPlano],*/
                 [QtdAnimais],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[PlanosAssinaturas]
        ORDER BY [IdPlano] ASC;
        --SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PlanosAssinaturas] OFF;
    END

DROP TABLE [dbo].[PlanosAssinaturas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_PlanosAssinaturas]', N'PlanosAssinaturas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_PlanosAssinaturas1]', N'PK_PlanosAssinaturas', N'OBJECT';

--EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_PlanosAssinaturas1]', N'IX_PlanosAssinaturas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[PlanosAssinaturas].[IX_PlanosAssinaturas_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_PlanosAssinaturas_1]
    ON [dbo].[PlanosAssinaturas]([IdPlanoPagarMe] ASC);


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas] FOREIGN KEY ([IdPlano]) REFERENCES [dbo].[PlanosAssinaturas] ([IdPlano]) ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];


GO
PRINT N'Update complete.';


GO
