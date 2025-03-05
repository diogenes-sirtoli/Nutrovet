
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

GO

PRINT N'Dropping [dbo].[Dietas].[FK_Dietas_1]...';

GO
DROP INDEX [FK_Dietas_1]
    ON [dbo].[Dietas];

GO
PRINT N'Dropping [dbo].[Pessoas].[FK_Pessoas_2]...';

GO
DROP INDEX [FK_Pessoas_2]
    ON [dbo].[Pessoas];

GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_DataCadastro];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_Receituario]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_Receituario];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_Prontuario]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_Prontuario];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_ComprovanteAnexou]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_ComprovanteAnexou];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_ComprovanteHomologado]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_ComprovanteHomologado];


GO
PRINT N'Dropping [dbo].[DF_AcessosVigenciaPlanos_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [DF_AcessosVigenciaPlanos_Ativo];


GO
PRINT N'Dropping [dbo].[DF_Pessoas_Ativo]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [DF_Pessoas_Ativo];


GO
PRINT N'Dropping [dbo].[DF_Pessoas_DataCadastro]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [DF_Pessoas_DataCadastro];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao] DROP CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_Pessoas1]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas1];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_Pessoas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Acessos_Pessoas]...';


GO
ALTER TABLE [dbo].[Acessos] DROP CONSTRAINT [FK_Acessos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Alimentos_Pessoas]...';


GO
ALTER TABLE [dbo].[Alimentos] DROP CONSTRAINT [FK_Alimentos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Dietas_Pessoas]...';


GO
ALTER TABLE [dbo].[Dietas] DROP CONSTRAINT [FK_Dietas_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Pessoas_Pessoas]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [FK_Pessoas_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Pessoas];


GO
PRINT N'Dropping [dbo].[IX_Alimentos]...';


GO
ALTER TABLE [dbo].[Alimentos] DROP CONSTRAINT [IX_Alimentos];


GO
PRINT N'Dropping [dbo].[IX_Pessoas]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [IX_Pessoas];


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
        INSERT INTO [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] ([IdVigencia], [IdPessoa], [IdPlano], [DtInicial], [DtFinal], [IdCupom], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdVigencia],
                 [IdPessoa],
                 [IdPlano],
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
PRINT N'Altering [dbo].[Animais]...';


GO
ALTER TABLE [dbo].[Animais] DROP COLUMN [IdPessoa];


GO
PRINT N'Altering [dbo].[Dietas]...';


GO
ALTER TABLE [dbo].[Dietas] DROP COLUMN [IdPessoa];


GO
PRINT N'Starting rebuilding table [dbo].[Pessoas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Pessoas] (
    [IdPessoa]       INT           IDENTITY (1, 1) NOT NULL,
    [IdTpPessoa]     INT           NOT NULL,
    [Nome]           VARCHAR (300) NOT NULL,
    [dTpEntidade]    INT           NULL,
    [dNacionalidade] INT           NULL,
    [DataNascimento] DATE          NULL,
    [RG]             VARCHAR (20)  NULL,
    [CPF]            VARCHAR (20)  NULL,
    [CNPJ]           VARCHAR (30)  NULL,
    [CRVM]           INT           NULL,
    [Email]          VARCHAR (200) NOT NULL,
    [Telefone]       VARCHAR (20)  NULL,
    [Celular]        VARCHAR (20)  NULL,
    [Logotipo]       VARCHAR (250) NULL,
    [Assinatura]     VARCHAR (250) NULL,
    [Logradouro]     VARCHAR (300) NULL,
    [Logr_Nr]        VARCHAR (10)  NULL,
    [Logr_Compl]     VARCHAR (100) NULL,
    [Logr_Bairro]    VARCHAR (200) NULL,
    [Logr_CEP]       VARCHAR (10)  NULL,
    [Logr_Cidade]    VARCHAR (200) NULL,
    [Logr_UF]        VARCHAR (10)  NULL,
    [Logr_Pais]      VARCHAR (150) NULL,
    [Latitude]       VARCHAR (50)  NULL,
    [Longitude]      VARCHAR (50)  NULL,
    [Usuario]        VARCHAR (200) NULL,
    [Senha]          VARCHAR (50)  NULL,
    [Bloqueado]      BIT           NULL,
    [Ativo]          BIT           CONSTRAINT [DF_Pessoas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]     INT           NULL,
    [IP]             VARCHAR (20)  NULL,
    [DataCadastro]   DATETIME      CONSTRAINT [DF_Pessoas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]         ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Pessoas1] PRIMARY KEY CLUSTERED ([IdPessoa] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Pessoas_11] UNIQUE NONCLUSTERED ([Email] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Pessoas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Pessoas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Pessoas] ([IdPessoa], [IdTpPessoa], [Nome], [DataNascimento], [RG], [CPF], [CRVM], [Email], [Telefone], [Celular], [Logotipo], [Assinatura], [Logradouro], [Logr_Nr], [Logr_Compl], [Logr_Bairro], [Logr_CEP], [Logr_Cidade], [Logr_UF], [Latitude], [Longitude], [Usuario], [Senha], [Bloqueado], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdPessoa],
                 [IdTpPessoa],
                 [Nome],
                 [DataNascimento],
                 [RG],
                 [CPF],
                 [CRVM],
                 [Email],
                 [Telefone],
                 [Celular],
                 [Logotipo],
                 [Assinatura],
                 [Logradouro],
                 [Logr_Nr],
                 [Logr_Compl],
                 [Logr_Bairro],
                 [Logr_CEP],
                 [Logr_Cidade],
                 [Logr_UF],
                 [Latitude],
                 [Longitude],
                 [Usuario],
                 [Senha],
                 [Bloqueado],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Pessoas]
        ORDER BY [IdPessoa] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Pessoas] OFF;
    END

DROP TABLE [dbo].[Pessoas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Pessoas]', N'Pessoas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Pessoas1]', N'PK_Pessoas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Pessoas_11]', N'IX_Pessoas_1', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas]
    ON [dbo].[Pessoas]([Nome] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[FK_Pessoas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Pessoas_1]
    ON [dbo].[Pessoas]([IdTpPessoa] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas_3]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_3]
    ON [dbo].[Pessoas]([CNPJ] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas_4]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_4]
    ON [dbo].[Pessoas]([CPF] ASC);


GO
PRINT N'Creating [dbo].[PessoasCartaoCredito]...';


GO
CREATE TABLE [dbo].[PessoasCartaoCredito] (
    [IdPessoa]     INT           NOT NULL,
    [IdCartao]     INT           IDENTITY (1, 1) NOT NULL,
    [NrCartao]     VARCHAR (150) NOT NULL,
    [CodSeg]       VARCHAR (50)  NOT NULL,
    [dBandeira]    INT           NOT NULL,
    [VencimCartao] VARCHAR (100) NOT NULL,
    [NomeCartao]   VARCHAR (250) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_PessoasCartaoCredito] PRIMARY KEY CLUSTERED ([IdCartao] ASC),
    CONSTRAINT [IX_PessoasCartaoCredito] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [NrCartao] ASC)
);


GO
PRINT N'Creating [dbo].[PessoasCartaoCredito].[FK_PessoasCartaoCredito_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_PessoasCartaoCredito_1]
    ON [dbo].[PessoasCartaoCredito]([IdPessoa] ASC);


GO
PRINT N'Creating [dbo].[PessoasCartaoCredito].[FK_PessoasCartaoCredito_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_PessoasCartaoCredito_2]
    ON [dbo].[PessoasCartaoCredito]([dBandeira] ASC);


GO
PRINT N'Creating [dbo].[PessoasDocumentos]...';


GO
CREATE TABLE [dbo].[PessoasDocumentos] (
    [IdPessoa]               INT           NOT NULL,
    [ComprovanteAnexou]      BIT           NULL,
    [ComprovantePath]        VARCHAR (350) NULL,
    [ComprovanteHomologado]  BIT           NULL,
    [ComprovanteDtHomolog]   DATE          NULL,
    [ComprovanteHomologador] INT           NULL,
    [Ativo]                  BIT           NULL,
    [IdOperador]             INT           NULL,
    [IP]                     VARCHAR (20)  NULL,
    [DataCadastro]           DATETIME      NULL,
    [Versao]                 ROWVERSION    NULL,
    CONSTRAINT [PK_PessoasDocumentos] PRIMARY KEY CLUSTERED ([IdPessoa] ASC)
);


GO
PRINT N'Creating [dbo].[PessoasDocumentos].[FK_PessoasDocumentos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_PessoasDocumentos_1]
    ON [dbo].[PessoasDocumentos]([ComprovanteHomologador] ASC);


GO
PRINT N'Creating [dbo].[Alimentos].[IX_Alimentos]...';


GO
CREATE NONCLUSTERED INDEX [IX_Alimentos]
    ON [dbo].[Alimentos]([IdFonte] ASC, [Alimento] ASC);


GO
PRINT N'Creating [dbo].[Tutores].[FK_Tutores_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Tutores_1]
    ON [dbo].[Tutores]([IdCliente] ASC);


GO
PRINT N'Creating [dbo].[Tutores].[FK_Tutores_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Tutores_2]
    ON [dbo].[Tutores]([IdTutor] ASC);


GO
PRINT N'Creating [dbo].[DF_PessoasCartaoCredito_Ativo]...';


GO
ALTER TABLE [dbo].[PessoasCartaoCredito]
    ADD CONSTRAINT [DF_PessoasCartaoCredito_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_PessoasCartaoCredito_DataCadastro]...';


GO
ALTER TABLE [dbo].[PessoasCartaoCredito]
    ADD CONSTRAINT [DF_PessoasCartaoCredito_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_PessoasDocumentos_ComprovanteHomologado]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos]
    ADD CONSTRAINT [DF_PessoasDocumentos_ComprovanteHomologado] DEFAULT ((0)) FOR [ComprovanteHomologado];


GO
PRINT N'Creating [dbo].[DF_PessoasDocumentos_ComprovanteAnexou]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos]
    ADD CONSTRAINT [DF_PessoasDocumentos_ComprovanteAnexou] DEFAULT ((0)) FOR [ComprovanteAnexou];


GO
PRINT N'Creating [dbo].[DF_PessoasDocumentos_Ativo]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos]
    ADD CONSTRAINT [DF_PessoasDocumentos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_PessoasDocumentos_DataCadastro]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos]
    ADD CONSTRAINT [DF_PessoasDocumentos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_Tutores_Ativo]...';


GO
ALTER TABLE [dbo].[Tutores]
    ADD CONSTRAINT [DF_Tutores_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Tutores_DataCadastro]...';


GO
ALTER TABLE [dbo].[Tutores]
    ADD CONSTRAINT [DF_Tutores_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos] FOREIGN KEY ([IdVigencia]) REFERENCES [dbo].[AcessosVigenciaPlanos] ([IdVigencia]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto] FOREIGN KEY ([IdCupom]) REFERENCES [dbo].[AcessosVigenciaCupomDesconto] ([IdCupom]) ON UPDATE CASCADE;


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
PRINT N'Creating [dbo].[FK_Acessos_Pessoas]...';


GO
ALTER TABLE [dbo].[Acessos] WITH NOCHECK
    ADD CONSTRAINT [FK_Acessos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Alimentos_Pessoas]...';


GO
ALTER TABLE [dbo].[Alimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_Alimentos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_PessoasCartaoCredito_Pessoas]...';


GO
ALTER TABLE [dbo].[PessoasCartaoCredito] WITH NOCHECK
    ADD CONSTRAINT [FK_PessoasCartaoCredito_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_PessoasDocumentos_Pessoas]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos] WITH NOCHECK
    ADD CONSTRAINT [FK_PessoasDocumentos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_PessoasDocumentos_Pessoas1]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos] WITH NOCHECK
    ADD CONSTRAINT [FK_PessoasDocumentos_Pessoas1] FOREIGN KEY ([ComprovanteHomologador]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Tutores_Pessoas]...';


GO
ALTER TABLE [dbo].[Tutores] WITH NOCHECK
    ADD CONSTRAINT [FK_Tutores_Pessoas] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Tutores_Pessoas1]...';


GO
ALTER TABLE [dbo].[Tutores] WITH NOCHECK
    ADD CONSTRAINT [FK_Tutores_Pessoas1] FOREIGN KEY ([IdTutor]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AcessosVigenciaSituacao] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_AcessosVigenciaCupomDesconto];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];

ALTER TABLE [dbo].[Acessos] WITH CHECK CHECK CONSTRAINT [FK_Acessos_Pessoas];

ALTER TABLE [dbo].[Alimentos] WITH CHECK CHECK CONSTRAINT [FK_Alimentos_Pessoas];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Pessoas];

ALTER TABLE [dbo].[PessoasCartaoCredito] WITH CHECK CHECK CONSTRAINT [FK_PessoasCartaoCredito_Pessoas];

ALTER TABLE [dbo].[PessoasDocumentos] WITH CHECK CHECK CONSTRAINT [FK_PessoasDocumentos_Pessoas];

ALTER TABLE [dbo].[PessoasDocumentos] WITH CHECK CHECK CONSTRAINT [FK_PessoasDocumentos_Pessoas1];

ALTER TABLE [dbo].[Tutores] WITH CHECK CHECK CONSTRAINT [FK_Tutores_Pessoas];

ALTER TABLE [dbo].[Tutores] WITH CHECK CHECK CONSTRAINT [FK_Tutores_Pessoas1];


GO
PRINT N'Update complete.';


GO
