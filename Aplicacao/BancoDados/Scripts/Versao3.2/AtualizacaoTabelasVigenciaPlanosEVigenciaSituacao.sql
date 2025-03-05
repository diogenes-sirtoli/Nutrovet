SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping [dbo].[AcessosVigenciaCupomDesconto].[FK_AcessosVigenciaCupomDesconto_2]...';


GO
DROP INDEX [FK_AcessosVigenciaCupomDesconto_2]
    ON [dbo].[AcessosVigenciaCupomDesconto];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaCupomDesconto_fUsado]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] DROP CONSTRAINT [DF_AcessosVigenciaCupomDesconto_fUsado];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaCupomDesconto_fAcessoLiberado]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] DROP CONSTRAINT [DF_AcessosVigenciaCupomDesconto_fAcessoLiberado];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaCupomDesconto_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] DROP CONSTRAINT [DF_AcessosVigenciaCupomDesconto_Ativo];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaCupomDesconto_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] DROP CONSTRAINT [DF_AcessosVigenciaCupomDesconto_DataCadastro];


GO
PRINT N'Dropping [dbo].[DF_PessoaTutores_DataCadastro]...';


GO
ALTER TABLE [dbo].[PessoaTutores] DROP CONSTRAINT [DF_PessoaTutores_DataCadastro];


GO
PRINT N'Dropping [dbo].[DF_PessoaTutores_Ativo]...';


GO
ALTER TABLE [dbo].[PessoaTutores] DROP CONSTRAINT [DF_PessoaTutores_Ativo];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaCupomDesconto_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] DROP CONSTRAINT [FK_AcessosVigenciaCupomDesconto_PlanosAssinaturas];


GO
PRINT N'Dropping [dbo].[IX_AcessosVigenciaSituacao]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao] DROP CONSTRAINT [IX_AcessosVigenciaSituacao];


GO
PRINT N'Dropping [dbo].[PessoaTutores]...';


GO
DROP TABLE [dbo].[PessoaTutores];


GO
PRINT N'Starting rebuilding table [dbo].[AcessosVigenciaCupomDesconto]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] (
    [IdCupom]         INT           IDENTITY (1, 1) NOT NULL,
    [NrCumpom]        VARCHAR (20)  NOT NULL,
    [dPlanoTp]        INT           NULL,
    [DtInicial]       DATE          NOT NULL,
    [DtFinal]         DATE          NOT NULL,
    [fUsado]          BIT           CONSTRAINT [DF_AcessosVigenciaCupomDesconto_fUsado] DEFAULT ((0)) NULL,
    [fAcessoLiberado] BIT           CONSTRAINT [DF_AcessosVigenciaCupomDesconto_fAcessoLiberado] DEFAULT ((0)) NULL,
    [Professor]       VARCHAR (400) NULL,
    [Ativo]           BIT           CONSTRAINT [DF_AcessosVigenciaCupomDesconto_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]      INT           NULL,
    [IP]              VARCHAR (20)  NULL,
    [DataCadastro]    DATETIME      CONSTRAINT [DF_AcessosVigenciaCupomDesconto_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]          ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosVigenciaCupomDesconto1] PRIMARY KEY CLUSTERED ([IdCupom] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosVigenciaCupomDesconto1] UNIQUE NONCLUSTERED ([NrCumpom] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosVigenciaCupomDesconto])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] ([IdCupom], [NrCumpom], [DtInicial], [DtFinal], [fUsado], [fAcessoLiberado], [Professor], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdCupom],
                 [NrCumpom],
                 [DtInicial],
                 [DtFinal],
                 [fUsado],
                 [fAcessoLiberado],
                 [Professor],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AcessosVigenciaCupomDesconto]
        ORDER BY [IdCupom] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] OFF;
    END

DROP TABLE [dbo].[AcessosVigenciaCupomDesconto];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto]', N'AcessosVigenciaCupomDesconto';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AcessosVigenciaCupomDesconto1]', N'PK_AcessosVigenciaCupomDesconto', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosVigenciaCupomDesconto1]', N'IX_AcessosVigenciaCupomDesconto', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[IX_AcessosVigenciaCupomDesconto_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_AcessosVigenciaCupomDesconto_1]
    ON [dbo].[AcessosVigenciaCupomDesconto]([Professor] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[IX_AcessosVigenciaCupomDesconto_2]...';


GO
CREATE NONCLUSTERED INDEX [IX_AcessosVigenciaCupomDesconto_2]
    ON [dbo].[AcessosVigenciaCupomDesconto]([DtInicial] ASC, [DtFinal] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[FK_AcessosVigenciaCupomDesconto_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaCupomDesconto_1]
    ON [dbo].[AcessosVigenciaCupomDesconto]([dPlanoTp] ASC);


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto] FOREIGN KEY ([IdCupom]) REFERENCES [dbo].[AcessosVigenciaCupomDesconto] ([IdCupom]) ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto];


GO
PRINT N'Update complete.';


GO
