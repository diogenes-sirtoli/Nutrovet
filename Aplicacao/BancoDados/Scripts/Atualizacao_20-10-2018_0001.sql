
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto]...';


GO
CREATE TABLE [dbo].[AcessosVigenciaCupomDesconto] (
    [IdVigencia]      INT          NOT NULL,
    [IdCupom]         INT          IDENTITY (1, 1) NOT NULL,
    [NrCumpom]        VARCHAR (20) NOT NULL,
    [PercentDesconto] INT          NOT NULL,
    [DtInicial]       DATE         NOT NULL,
    [DtFinal]         DATE         NOT NULL,
    [Ativo]           BIT          NULL,
    [IdOperador]      INT          NULL,
    [IP]              VARCHAR (20) NULL,
    [DataCadastro]    DATETIME     NULL,
    [Versao]          ROWVERSION   NULL,
    CONSTRAINT [PK_AcessosVigenciaCupomDesconto] PRIMARY KEY CLUSTERED ([IdCupom] ASC),
    CONSTRAINT [IX_AcessosVigenciaCupomDesconto] UNIQUE NONCLUSTERED ([IdVigencia] ASC, [NrCumpom] ASC)
);


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[FK_AcessosVigenciaCupomDesconto_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaCupomDesconto_1]
    ON [dbo].[AcessosVigenciaCupomDesconto]([IdVigencia] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[IX_AcessosVigenciaCupomDesconto_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_AcessosVigenciaCupomDesconto_1]
    ON [dbo].[AcessosVigenciaCupomDesconto]([IdVigencia] ASC, [DtInicial] ASC, [DtFinal] ASC);


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaCupomDesconto_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto]
    ADD CONSTRAINT [DF_AcessosVigenciaCupomDesconto_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaCupomDesconto_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto]
    ADD CONSTRAINT [DF_AcessosVigenciaCupomDesconto_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaCupomDesconto_AcessosVigenciaPlanos]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaCupomDesconto_AcessosVigenciaPlanos] FOREIGN KEY ([IdVigencia]) REFERENCES [dbo].[AcessosVigenciaPlanos] ([IdVigencia]) ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaCupomDesconto_AcessosVigenciaPlanos];


GO
PRINT N'Update complete.';


GO
