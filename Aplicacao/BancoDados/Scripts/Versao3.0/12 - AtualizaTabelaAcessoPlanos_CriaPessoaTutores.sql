
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_Ativo];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_DataCadastro];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao] DROP CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_Pessoas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto];


GO
PRINT N'Starting rebuilding table [dbo].[AcessosVigenciaPlanos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] (
    [IdPessoa]              INT          NOT NULL,
    [IdVigencia]            INT          IDENTITY (1, 1) NOT NULL,
    [IdPlano]               INT          NOT NULL,
    [IdSubscriptionPagarMe] VARCHAR (50) NULL,
    [StatusPagarMe]         VARCHAR (50) NULL,
    [DtInicial]             DATE         NOT NULL,
    [DtFinal]               DATE         NOT NULL,
    [IdCartao]              INT          NULL,
    [IdCupom]               INT          NULL,
    [Ativo]                 BIT          CONSTRAINT [DF_AcessosVigenciaPlanos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]            INT          NULL,
    [IP]                    VARCHAR (20) NULL,
    [DataCadastro]          DATETIME     CONSTRAINT [DF_AcessosVigenciaPlanos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]                ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosVigenciaPlanos1] PRIMARY KEY CLUSTERED ([IdVigencia] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosVigenciaPlanos1] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [IdPlano] ASC, [DtInicial] ASC, [DtFinal] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosVigenciaPlanos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] ([IdVigencia], [IdPessoa], [IdPlano], [IdSubscriptionPagarMe], [StatusPagarMe], [DtInicial], [DtFinal], [IdCupom], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdVigencia],
                 [IdPessoa],
                 [IdPlano],
                 [IdSubscriptionPagarMe],
                 [StatusPagarMe],
                 [DtInicial],
                 [DtFinal],
                 [IdCupom],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AcessosVigenciaPlanos]
        ORDER BY [IdVigencia] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] OFF;
    END

DROP TABLE [dbo].[AcessosVigenciaPlanos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AcessosVigenciaPlanos]', N'AcessosVigenciaPlanos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AcessosVigenciaPlanos1]', N'PK_AcessosVigenciaPlanos', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosVigenciaPlanos1]', N'IX_AcessosVigenciaPlanos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AcessosVigenciaPlanos].[FK_AcessosVigenciaPlanos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaPlanos_1]
    ON [dbo].[AcessosVigenciaPlanos]([IdPessoa] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaPlanos].[FK_AcessosVigenciaPlanos_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaPlanos_2]
    ON [dbo].[AcessosVigenciaPlanos]([IdPlano] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaPlanos].[IX_AcessosVigenciaPlanos_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_AcessosVigenciaPlanos_1]
    ON [dbo].[AcessosVigenciaPlanos]([DtInicial] ASC, [DtFinal] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaPlanos].[FK_AcessosVigenciaPlanos_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaPlanos_3]
    ON [dbo].[AcessosVigenciaPlanos]([IdCupom] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaPlanos].[FK_AcessosVigenciaPlanos_4]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaPlanos_4]
    ON [dbo].[AcessosVigenciaPlanos]([IdCartao] ASC);


GO
PRINT N'Creating [dbo].[PessoaTutores]...';


GO
CREATE TABLE [dbo].[PessoaTutores] (
    [IdPessoa]     INT           NULL,
    [IdTutor]      INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (300) NOT NULL,
    [dTpEntidade]  INT           NULL,
    [CPF]          VARCHAR (100) NULL,
    [Email]        VARCHAR (200) NULL,
    [Telefone]     VARCHAR (20)  NULL,
    [Celular]      VARCHAR (20)  NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_PessoaTutores] PRIMARY KEY CLUSTERED ([IdTutor] ASC)
);


GO
PRINT N'Creating [dbo].[PessoaTutores].[IX_PessoaTutores]...';


GO
CREATE NONCLUSTERED INDEX [IX_PessoaTutores]
    ON [dbo].[PessoaTutores]([Nome] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[FK_AcessosVigenciaCupomDesconto_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaCupomDesconto_1]
    ON [dbo].[AcessosVigenciaCupomDesconto]([IdPlano] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto].[FK_AcessosVigenciaCupomDesconto_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaCupomDesconto_2]
    ON [dbo].[AcessosVigenciaCupomDesconto]([IdTipoDesc] ASC);


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
PRINT N'Creating [dbo].[DF_PessoaTutores_DataCadastro]...';


GO
ALTER TABLE [dbo].[PessoaTutores]
    ADD CONSTRAINT [DF_PessoaTutores_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_PessoaTutores_Ativo]...';


GO
ALTER TABLE [dbo].[PessoaTutores]
    ADD CONSTRAINT [DF_PessoaTutores_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos] FOREIGN KEY ([IdVigencia]) REFERENCES [dbo].[AcessosVigenciaPlanos] ([IdVigencia]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas] FOREIGN KEY ([IdPlano]) REFERENCES [dbo].[PlanosAssinaturas] ([IdPlano]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_Pessoas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto] FOREIGN KEY ([IdCupom]) REFERENCES [dbo].[AcessosVigenciaCupomDesconto] ([IdCupom]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_PessoasCartaoCredito]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_PessoasCartaoCredito] FOREIGN KEY ([IdCartao]) REFERENCES [dbo].[PessoasCartaoCredito] ([IdCartao]);


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AcessosVigenciaSituacao] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_PessoasCartaoCredito];


GO
PRINT N'Update complete.';


GO
