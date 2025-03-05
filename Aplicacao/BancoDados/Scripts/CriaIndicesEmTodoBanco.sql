
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Starting rebuilding table [dbo].[Acessos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Acessos] (
    [IdPessoa]       INT          NOT NULL,
    [IdAcFunc]       INT          NOT NULL,
    [Inserir]        BIT          CONSTRAINT [DF_Acessos_Inserir] DEFAULT ((0)) NULL,
    [Alterar]        BIT          CONSTRAINT [DF_Acessos_Alterar] DEFAULT ((0)) NULL,
    [Excluir]        BIT          CONSTRAINT [DF_Acessos_Excluir] DEFAULT ((0)) NULL,
    [Consultar]      BIT          CONSTRAINT [DF_Acessos_Consultar] DEFAULT ((0)) NULL,
    [AcoesEspeciais] BIT          NULL,
    [Relatorios]     BIT          CONSTRAINT [DF_Acessos_Relatorios] DEFAULT ((0)) NULL,
    [SuperUser]      BIT          NULL,
    [TermoUso]       BIT          CONSTRAINT [DF_Acessos_TermoUso] DEFAULT ((0)) NULL,
    [Ativo]          BIT          CONSTRAINT [DF_Acessos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]     INT          NULL,
    [IP]             VARCHAR (20) NULL,
    [DataCadastro]   DATETIME     CONSTRAINT [DF_Acessos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]         ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Acessos1] PRIMARY KEY CLUSTERED ([IdPessoa] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Acessos])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Acessos] ([IdPessoa], [IdAcFunc], [Inserir], [Alterar], [Excluir], [Consultar], [AcoesEspeciais], [Relatorios], [SuperUser], [TermoUso], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdPessoa],
                 [IdAcFunc],
                 [Inserir],
                 [Alterar],
                 [Excluir],
                 [Consultar],
                 [AcoesEspeciais],
                 [Relatorios],
                 [SuperUser],
                 [TermoUso],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Acessos]
        ORDER BY [IdPessoa] ASC;
    END

DROP TABLE [dbo].[Acessos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Acessos]', N'Acessos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Acessos1]', N'PK_Acessos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Acessos].[FK_Acessos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Acessos_1]
    ON [dbo].[Acessos]([IdAcFunc] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[AcessosAuxFuncoes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosAuxFuncoes] (
    [IdAcFunc]     INT           IDENTITY (1, 1) NOT NULL,
    [Funcao]       VARCHAR (100) NOT NULL,
    [Ativo]        BIT           CONSTRAINT [DF_AcessosAuxFuncoes_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_AcessosAuxFuncoes_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosAuxFuncoes1] PRIMARY KEY CLUSTERED ([IdAcFunc] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosAuxFuncoes1] UNIQUE NONCLUSTERED ([Funcao] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosAuxFuncoes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosAuxFuncoes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosAuxFuncoes] ([IdAcFunc], [Funcao], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdAcFunc],
                 [Funcao],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AcessosAuxFuncoes]
        ORDER BY [IdAcFunc] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosAuxFuncoes] OFF;
    END

DROP TABLE [dbo].[AcessosAuxFuncoes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AcessosAuxFuncoes]', N'AcessosAuxFuncoes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AcessosAuxFuncoes1]', N'PK_AcessosAuxFuncoes', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosAuxFuncoes1]', N'IX_AcessosAuxFuncoes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[AcessosAuxTelas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosAuxTelas] (
    [IdTela]       INT           IDENTITY (1, 1) NOT NULL,
    [CodTela]      VARCHAR (10)  NOT NULL,
    [Telas]        VARCHAR (100) NOT NULL,
    [Ativo]        BIT           CONSTRAINT [DF_AcessosAuxTelas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_AcessosAuxTelas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosAuxTelas1] PRIMARY KEY CLUSTERED ([IdTela] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosAuxTelas_11] UNIQUE NONCLUSTERED ([CodTela] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosAuxTelas1] UNIQUE NONCLUSTERED ([Telas] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosAuxTelas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosAuxTelas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosAuxTelas] ([IdTela], [CodTela], [Telas], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdTela],
                 [CodTela],
                 [Telas],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AcessosAuxTelas]
        ORDER BY [IdTela] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosAuxTelas] OFF;
    END

DROP TABLE [dbo].[AcessosAuxTelas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AcessosAuxTelas]', N'AcessosAuxTelas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AcessosAuxTelas1]', N'PK_AcessosAuxTelas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosAuxTelas_11]', N'IX_AcessosAuxTelas_1', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosAuxTelas1]', N'IX_AcessosAuxTelas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[AcessosFuncoesTelas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosFuncoesTelas] (
    [IdFuncTela]   INT          IDENTITY (1, 1) NOT NULL,
    [IdAcFunc]     INT          NOT NULL,
    [IdTela]       INT          NOT NULL,
    [Ativo]        BIT          CONSTRAINT [DF_AcessosFuncoesTelas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     CONSTRAINT [DF_AcessosFuncoesTelas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosFuncoesTelas1] PRIMARY KEY CLUSTERED ([IdFuncTela] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosFuncoesTelas1] UNIQUE NONCLUSTERED ([IdAcFunc] ASC, [IdTela] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosFuncoesTelas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosFuncoesTelas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosFuncoesTelas] ([IdFuncTela], [IdAcFunc], [IdTela], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdFuncTela],
                 [IdAcFunc],
                 [IdTela],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AcessosFuncoesTelas]
        ORDER BY [IdFuncTela] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosFuncoesTelas] OFF;
    END

DROP TABLE [dbo].[AcessosFuncoesTelas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AcessosFuncoesTelas]', N'AcessosFuncoesTelas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AcessosFuncoesTelas1]', N'PK_AcessosFuncoesTelas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosFuncoesTelas1]', N'IX_AcessosFuncoesTelas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AcessosFuncoesTelas].[FK_AcessosFuncoesTelas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosFuncoesTelas_1]
    ON [dbo].[AcessosFuncoesTelas]([IdAcFunc] ASC);


GO
PRINT N'Creating [dbo].[AcessosFuncoesTelas].[FK_AcessosFuncoesTelas_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosFuncoesTelas_2]
    ON [dbo].[AcessosFuncoesTelas]([IdTela] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[AcessosVigenciaCupomDesconto]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] (
    [IdCupom]      INT          IDENTITY (1, 1) NOT NULL,
    [NrCumpom]     VARCHAR (20) NOT NULL,
    [IdTipoDesc]   INT          NULL,
    [ValorDesc]    INT          NOT NULL,
    [DtInicial]    DATE         NOT NULL,
    [DtFinal]      DATE         NOT NULL,
    [fUsado]       BIT          CONSTRAINT [DF_AcessosVigenciaCupomDesconto_fUsado] DEFAULT ((0)) NULL,
    [Ativo]        BIT          CONSTRAINT [DF_AcessosVigenciaCupomDesconto_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     CONSTRAINT [DF_AcessosVigenciaCupomDesconto_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosVigenciaCupomDesconto1] PRIMARY KEY CLUSTERED ([IdCupom] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosVigenciaCupomDesconto1] UNIQUE NONCLUSTERED ([NrCumpom] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosVigenciaCupomDesconto])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosVigenciaCupomDesconto] ([IdCupom], [NrCumpom], [IdTipoDesc], [ValorDesc], [DtInicial], [DtFinal], [fUsado], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdCupom],
                 [NrCumpom],
                 [IdTipoDesc],
                 [ValorDesc],
                 [DtInicial],
                 [DtFinal],
                 [fUsado],
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
PRINT N'Starting rebuilding table [dbo].[AcessosVigenciaPlanos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] (
    [IdPessoa]               INT           NOT NULL,
    [IdVigencia]             INT           IDENTITY (1, 1) NOT NULL,
    [IdPlano]                INT           NOT NULL,
    [DtInicial]              DATE          NOT NULL,
    [DtFinal]                DATE          NOT NULL,
    [QtdAnim]                INT           NULL,
    [Periodo]                INT           NULL,
    [Receituario]            BIT           CONSTRAINT [DF_AcessosVigenciaPlanos_Receituario] DEFAULT ((0)) NULL,
    [Prontuario]             BIT           CONSTRAINT [DF_AcessosVigenciaPlanos_Prontuario] DEFAULT ((0)) NULL,
    [ComprovanteAnexou]      BIT           CONSTRAINT [DF_AcessosVigenciaPlanos_ComprovanteAnexou] DEFAULT ((0)) NULL,
    [ComprovantePath]        VARCHAR (350) NULL,
    [ComprovanteHomologado]  BIT           CONSTRAINT [DF_AcessosVigenciaPlanos_ComprovanteHomologado] DEFAULT ((0)) NULL,
    [ComprovanteDtHomolog]   DATE          NULL,
    [ComprovanteHomologador] INT           NULL,
    [Ativo]                  BIT           CONSTRAINT [DF_AcessosVigenciaPlanos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]             INT           NULL,
    [IP]                     VARCHAR (20)  NULL,
    [DataCadastro]           DATETIME      CONSTRAINT [DF_AcessosVigenciaPlanos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]                 ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosVigenciaPlanos1] PRIMARY KEY CLUSTERED ([IdVigencia] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosVigenciaPlanos1] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [IdPlano] ASC, [DtInicial] ASC, [DtFinal] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosVigenciaPlanos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosVigenciaPlanos] ([IdVigencia], [IdPessoa], [IdPlano], [DtInicial], [DtFinal], [QtdAnim], [Periodo], [Receituario], [Prontuario], [ComprovanteAnexou], [ComprovantePath], [ComprovanteHomologado], [ComprovanteDtHomolog], [ComprovanteHomologador], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdVigencia],
                 [IdPessoa],
                 [IdPlano],
                 [DtInicial],
                 [DtFinal],
                 [QtdAnim],
                 [Periodo],
                 [Receituario],
                 [Prontuario],
                 [ComprovanteAnexou],
                 [ComprovantePath],
                 [ComprovanteHomologado],
                 [ComprovanteDtHomolog],
                 [ComprovanteHomologador],
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
PRINT N'Starting rebuilding table [dbo].[AcessosVigenciaSituacao]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AcessosVigenciaSituacao] (
    [IdVigencia]   INT          NOT NULL,
    [IdVigSit]     INT          IDENTITY (1, 1) NOT NULL,
    [IdSituacao]   INT          NOT NULL,
    [DataSituacao] DATE         NULL,
    [Ativo]        BIT          CONSTRAINT [DF_AcessosVigenciaSituacao_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     CONSTRAINT [DF_AcessosVigenciaSituacao_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AcessosVigenciaSituacao1] PRIMARY KEY CLUSTERED ([IdVigSit] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AcessosVigenciaSituacao1] UNIQUE NONCLUSTERED ([IdVigencia] ASC, [IdSituacao] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AcessosVigenciaSituacao])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaSituacao] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AcessosVigenciaSituacao] ([IdVigSit], [IdVigencia], [IdSituacao], [DataSituacao], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdVigSit],
                 [IdVigencia],
                 [IdSituacao],
                 [DataSituacao],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AcessosVigenciaSituacao]
        ORDER BY [IdVigSit] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AcessosVigenciaSituacao] OFF;
    END

DROP TABLE [dbo].[AcessosVigenciaSituacao];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AcessosVigenciaSituacao]', N'AcessosVigenciaSituacao';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AcessosVigenciaSituacao1]', N'PK_AcessosVigenciaSituacao', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AcessosVigenciaSituacao1]', N'IX_AcessosVigenciaSituacao', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AcessosVigenciaSituacao].[FK_AcessosVigenciaSituacao_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaSituacao_1]
    ON [dbo].[AcessosVigenciaSituacao]([IdVigencia] ASC);


GO
PRINT N'Creating [dbo].[AcessosVigenciaSituacao].[FK_AcessosVigenciaSituacao_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AcessosVigenciaSituacao_2]
    ON [dbo].[AcessosVigenciaSituacao]([IdSituacao] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[AlimentoNutrientes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AlimentoNutrientes] (
    [IdAlimento]   INT             NOT NULL,
    [IdAlimNutr]   INT             IDENTITY (1, 1) NOT NULL,
    [IdNutr]       INT             NOT NULL,
    [Valor]        DECIMAL (18, 3) NOT NULL,
    [IdUnidade]    INT             NULL,
    [Ativo]        BIT             CONSTRAINT [DF_AlimentoComponentes_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        CONSTRAINT [DF_AlimentoComponentes_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AlimentoComponentes1] PRIMARY KEY CLUSTERED ([IdAlimNutr] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AlimentoNutrientes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AlimentoNutrientes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AlimentoNutrientes] ([IdAlimNutr], [IdAlimento], [IdNutr], [Valor], [IdUnidade], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdAlimNutr],
                 [IdAlimento],
                 [IdNutr],
                 [Valor],
                 [IdUnidade],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AlimentoNutrientes]
        ORDER BY [IdAlimNutr] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AlimentoNutrientes] OFF;
    END

DROP TABLE [dbo].[AlimentoNutrientes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AlimentoNutrientes]', N'AlimentoNutrientes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AlimentoComponentes1]', N'PK_AlimentoComponentes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AlimentoNutrientes].[FK_AlimentoComponentes_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentoComponentes_1]
    ON [dbo].[AlimentoNutrientes]([IdAlimento] ASC);


GO
PRINT N'Creating [dbo].[AlimentoNutrientes].[FK_AlimentoComponentes_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentoComponentes_2]
    ON [dbo].[AlimentoNutrientes]([IdNutr] ASC);


GO
PRINT N'Creating [dbo].[AlimentoNutrientes].[FK_AlimentoNutrientes_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentoNutrientes_3]
    ON [dbo].[AlimentoNutrientes]([IdUnidade] ASC);


GO
PRINT N'Creating [dbo].[AlimentoNutrientes].[IX_AlimentoNutrientes]...';


GO
CREATE NONCLUSTERED INDEX [IX_AlimentoNutrientes]
    ON [dbo].[AlimentoNutrientes]([IdAlimento] ASC, [IdNutr] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[Alimentos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Alimentos] (
    [IdPessoa]     INT           NULL,
    [IdAlimento]   INT           IDENTITY (1, 1) NOT NULL,
    [IdGrupo]      INT           NOT NULL,
    [IdFonte]      INT           NOT NULL,
    [NDB_No]       INT           NULL,
    [Alimento]     VARCHAR (150) NOT NULL,
    [Compartilhar] BIT           CONSTRAINT [DF_Alimentos_Compartilhar] DEFAULT ((0)) NULL,
    [fHomologado]  BIT           CONSTRAINT [DF_Alimentos_fHomologado] DEFAULT ((0)) NULL,
    [DataHomol]    DATE          NULL,
    [Ativo]        BIT           CONSTRAINT [DF_Alimentos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_Alimentos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Alimentos1] PRIMARY KEY CLUSTERED ([IdAlimento] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Alimentos1] UNIQUE NONCLUSTERED ([IdFonte] ASC, [Alimento] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Alimentos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Alimentos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Alimentos] ([IdAlimento], [IdPessoa], [IdGrupo], [IdFonte], [NDB_No], [Alimento], [Compartilhar], [fHomologado], [DataHomol], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdAlimento],
                 [IdPessoa],
                 [IdGrupo],
                 [IdFonte],
                 [NDB_No],
                 [Alimento],
                 [Compartilhar],
                 [fHomologado],
                 [DataHomol],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Alimentos]
        ORDER BY [IdAlimento] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Alimentos] OFF;
    END

DROP TABLE [dbo].[Alimentos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Alimentos]', N'Alimentos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Alimentos1]', N'PK_Alimentos', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Alimentos1]', N'IX_Alimentos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Alimentos].[FK_Alimentos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Alimentos_1]
    ON [dbo].[Alimentos]([IdFonte] ASC);


GO
PRINT N'Creating [dbo].[Alimentos].[FK_Alimentos_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Alimentos_2]
    ON [dbo].[Alimentos]([IdGrupo] ASC);


GO
PRINT N'Creating [dbo].[Alimentos].[IX_Alimentos_2]...';


GO
CREATE NONCLUSTERED INDEX [IX_Alimentos_2]
    ON [dbo].[Alimentos]([Alimento] ASC);


GO
PRINT N'Creating [dbo].[Alimentos].[FK_Alimentos_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_Alimentos_3]
    ON [dbo].[Alimentos]([IdPessoa] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[AlimentosAuxFontes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AlimentosAuxFontes] (
    [IdFonte]      INT           IDENTITY (1, 1) NOT NULL,
    [Fonte]        VARCHAR (150) NOT NULL,
    [Ativo]        BIT           CONSTRAINT [DF_AlimentosAuxFontes_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_AlimentosAuxFontes_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AlimentosAuxFontes1] PRIMARY KEY CLUSTERED ([IdFonte] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AlimentosAuxFontes1] UNIQUE NONCLUSTERED ([Fonte] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AlimentosAuxFontes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AlimentosAuxFontes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AlimentosAuxFontes] ([IdFonte], [Fonte], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdFonte],
                 [Fonte],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AlimentosAuxFontes]
        ORDER BY [IdFonte] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AlimentosAuxFontes] OFF;
    END

DROP TABLE [dbo].[AlimentosAuxFontes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AlimentosAuxFontes]', N'AlimentosAuxFontes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AlimentosAuxFontes1]', N'PK_AlimentosAuxFontes', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AlimentosAuxFontes1]', N'IX_AlimentosAuxFontes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[AlimentosAuxGrupos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AlimentosAuxGrupos] (
    [IdGrupo]       INT           IDENTITY (1, 1) NOT NULL,
    [GrupoAlimento] VARCHAR (150) NOT NULL,
    [Ativo]         BIT           CONSTRAINT [DF_AlimentosAuxGrupos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]    INT           NULL,
    [IP]            VARCHAR (20)  NULL,
    [DataCadastro]  DATETIME      CONSTRAINT [DF_AlimentosAuxGrupos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]        ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AlimentosAuxGrupos1] PRIMARY KEY CLUSTERED ([IdGrupo] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AlimentosAuxGrupos1] UNIQUE NONCLUSTERED ([GrupoAlimento] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AlimentosAuxGrupos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AlimentosAuxGrupos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AlimentosAuxGrupos] ([IdGrupo], [GrupoAlimento], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdGrupo],
                 [GrupoAlimento],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AlimentosAuxGrupos]
        ORDER BY [IdGrupo] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AlimentosAuxGrupos] OFF;
    END

DROP TABLE [dbo].[AlimentosAuxGrupos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AlimentosAuxGrupos]', N'AlimentosAuxGrupos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AlimentosAuxGrupos1]', N'PK_AlimentosAuxGrupos', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AlimentosAuxGrupos1]', N'IX_AlimentosAuxGrupos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Animais]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Animais] (
    [IdPessoa]     INT             NOT NULL,
    [IdAnimal]     INT             IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (150)   NOT NULL,
    [IdRaca]       INT             NULL,
    [Sexo]         INT             NULL,
    [DtNascim]     DATE            NULL,
    [PesoAtual]    DECIMAL (10, 3) NULL,
    [PesoIdeal]    DECIMAL (10, 3) NULL,
    [Ativo]        BIT             CONSTRAINT [DF_Animais_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        CONSTRAINT [DF_Animais_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Animais1] PRIMARY KEY CLUSTERED ([IdAnimal] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Animais1] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [Nome] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Animais])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Animais] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Animais] ([IdAnimal], [IdPessoa], [Nome], [IdRaca], [Sexo], [DtNascim], [PesoAtual], [PesoIdeal], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdAnimal],
                 [IdPessoa],
                 [Nome],
                 [IdRaca],
                 [Sexo],
                 [DtNascim],
                 [PesoAtual],
                 [PesoIdeal],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Animais]
        ORDER BY [IdAnimal] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Animais] OFF;
    END

DROP TABLE [dbo].[Animais];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Animais]', N'Animais';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Animais1]', N'PK_Animais', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Animais1]', N'IX_Animais', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Animais].[FK_Animais_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_1]
    ON [dbo].[Animais]([IdPessoa] ASC);


GO
PRINT N'Creating [dbo].[Animais].[FK_Animais_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_3]
    ON [dbo].[Animais]([IdRaca] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[AnimaisAuxEspecies]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AnimaisAuxEspecies] (
    [IdEspecie]    INT           IDENTITY (1, 1) NOT NULL,
    [Especie]      VARCHAR (100) NOT NULL,
    [Ativo]        BIT           CONSTRAINT [DF_AnimaisAuxEspecies_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_AnimaisAuxEspecies_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AnimaisAuxEspecies1] PRIMARY KEY CLUSTERED ([IdEspecie] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AnimaisAuxEspecies1] UNIQUE NONCLUSTERED ([Especie] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AnimaisAuxEspecies])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AnimaisAuxEspecies] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AnimaisAuxEspecies] ([IdEspecie], [Especie], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdEspecie],
                 [Especie],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AnimaisAuxEspecies]
        ORDER BY [IdEspecie] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AnimaisAuxEspecies] OFF;
    END

DROP TABLE [dbo].[AnimaisAuxEspecies];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AnimaisAuxEspecies]', N'AnimaisAuxEspecies';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AnimaisAuxEspecies1]', N'PK_AnimaisAuxEspecies', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AnimaisAuxEspecies1]', N'IX_AnimaisAuxEspecies', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[AnimaisAuxRacas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AnimaisAuxRacas] (
    [IdEspecie]    INT           NOT NULL,
    [IdRaca]       INT           IDENTITY (1, 1) NOT NULL,
    [Raca]         VARCHAR (100) NOT NULL,
    [IdadeAdulta]  INT           NULL,
    [CrescInicial] INT           NULL,
    [CrescFinal]   INT           NULL,
    [Ativo]        BIT           CONSTRAINT [DF_AnimaisRacas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_AnimaisRacas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AnimaisRacas1] PRIMARY KEY CLUSTERED ([IdRaca] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AnimaisRacas1] UNIQUE NONCLUSTERED ([IdEspecie] ASC, [Raca] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AnimaisAuxRacas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AnimaisAuxRacas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AnimaisAuxRacas] ([IdRaca], [IdEspecie], [Raca], [IdadeAdulta], [CrescInicial], [CrescFinal], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdRaca],
                 [IdEspecie],
                 [Raca],
                 [IdadeAdulta],
                 [CrescInicial],
                 [CrescFinal],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[AnimaisAuxRacas]
        ORDER BY [IdRaca] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AnimaisAuxRacas] OFF;
    END

DROP TABLE [dbo].[AnimaisAuxRacas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AnimaisAuxRacas]', N'AnimaisAuxRacas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AnimaisRacas1]', N'PK_AnimaisRacas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AnimaisRacas1]', N'IX_AnimaisRacas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Cardapio]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Cardapio] (
    [IdPessoa]        INT             NOT NULL,
    [IdAnimal]        INT             NOT NULL,
    [IdCardapio]      INT             IDENTITY (1, 1) NOT NULL,
    [DtCardapio]      DATE            NOT NULL,
    [PesoIdeal]       DECIMAL (10, 3) NULL,
    [FatorEnergia]    DECIMAL (10, 3) NULL,
    [NEM]             DECIMAL (10, 3) NULL,
    [Gestante]        BIT             NULL,
    [Lactante]        BIT             NULL,
    [LactacaoSemanas] INT             NULL,
    [NrFilhotes]      INT             NULL,
    [IdDieta]         INT             NULL,
    [EmCardapio]      DECIMAL (10, 3) NULL,
    [Observacao]      VARCHAR (MAX)   NULL,
    [Ativo]           BIT             CONSTRAINT [DF_Cardapio_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]      INT             NULL,
    [IP]              VARCHAR (20)    NULL,
    [DataCadastro]    DATETIME        CONSTRAINT [DF_Cardapio_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]          ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Cardapio1] PRIMARY KEY CLUSTERED ([IdCardapio] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Cardapio])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Cardapio] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Cardapio] ([IdCardapio], [IdPessoa], [IdAnimal], [DtCardapio], [PesoIdeal], [FatorEnergia], [NEM], [Gestante], [Lactante], [LactacaoSemanas], [NrFilhotes], [IdDieta], [EmCardapio], [Observacao], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdCardapio],
                 [IdPessoa],
                 [IdAnimal],
                 [DtCardapio],
                 [PesoIdeal],
                 [FatorEnergia],
                 [NEM],
                 [Gestante],
                 [Lactante],
                 [LactacaoSemanas],
                 [NrFilhotes],
                 [IdDieta],
                 [EmCardapio],
                 [Observacao],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Cardapio]
        ORDER BY [IdCardapio] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Cardapio] OFF;
    END

DROP TABLE [dbo].[Cardapio];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Cardapio]', N'Cardapio';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Cardapio1]', N'PK_Cardapio', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Cardapio].[FK_Cardapio_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Cardapio_1]
    ON [dbo].[Cardapio]([IdAnimal] ASC);


GO
PRINT N'Creating [dbo].[Cardapio].[FK_Cardapio_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Cardapio_2]
    ON [dbo].[Cardapio]([IdDieta] ASC);


GO
PRINT N'Creating [dbo].[Cardapio].[FK_Cardapio_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_Cardapio_3]
    ON [dbo].[Cardapio]([IdPessoa] ASC);


GO
PRINT N'Creating [dbo].[Cardapio].[IX_Cardapio_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_Cardapio_1]
    ON [dbo].[Cardapio]([DtCardapio] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[CardapiosAlimentos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_CardapiosAlimentos] (
    [IdCardapio]   INT          NOT NULL,
    [IdCardapAlim] INT          IDENTITY (1, 1) NOT NULL,
    [IdAlimento]   INT          NOT NULL,
    [Quant]        INT          NULL,
    [Ativo]        BIT          CONSTRAINT [DF_CardapiosAlimentos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     CONSTRAINT [DF_CardapiosAlimentos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_CardapiosAlimentos1] PRIMARY KEY CLUSTERED ([IdCardapAlim] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_CardapiosAlimentos1] UNIQUE NONCLUSTERED ([IdCardapio] ASC, [IdAlimento] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[CardapiosAlimentos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CardapiosAlimentos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CardapiosAlimentos] ([IdCardapAlim], [IdCardapio], [IdAlimento], [Quant], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdCardapAlim],
                 [IdCardapio],
                 [IdAlimento],
                 [Quant],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[CardapiosAlimentos]
        ORDER BY [IdCardapAlim] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CardapiosAlimentos] OFF;
    END

DROP TABLE [dbo].[CardapiosAlimentos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CardapiosAlimentos]', N'CardapiosAlimentos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_CardapiosAlimentos1]', N'PK_CardapiosAlimentos', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_CardapiosAlimentos1]', N'IX_CardapiosAlimentos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[CardapiosAlimentos].[FK_CardapiosAlimentos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_CardapiosAlimentos_1]
    ON [dbo].[CardapiosAlimentos]([IdCardapio] ASC);


GO
PRINT N'Creating [dbo].[CardapiosAlimentos].[FK_CardapiosAlimentos_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_CardapiosAlimentos_2]
    ON [dbo].[CardapiosAlimentos]([IdAlimento] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[Dietas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Dietas] (
    [IdPessoa]     INT             NOT NULL,
    [IdDieta]      INT             IDENTITY (1, 1) NOT NULL,
    [Dieta]        VARCHAR (150)   NOT NULL,
    [Carboidrato]  DECIMAL (10, 3) NULL,
    [Proteina]     DECIMAL (10, 3) NULL,
    [Gordura]      DECIMAL (10, 3) NULL,
    [Ativo]        BIT             CONSTRAINT [DF_Dietas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        CONSTRAINT [DF_Dietas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Dietas1] PRIMARY KEY CLUSTERED ([IdDieta] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Dietas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Dietas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Dietas] ([IdDieta], [IdPessoa], [Dieta], [Carboidrato], [Proteina], [Gordura], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdDieta],
                 [IdPessoa],
                 [Dieta],
                 [Carboidrato],
                 [Proteina],
                 [Gordura],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Dietas]
        ORDER BY [IdDieta] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Dietas] OFF;
    END

DROP TABLE [dbo].[Dietas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Dietas]', N'Dietas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Dietas1]', N'PK_Dietas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[DietasAlimentos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_DietasAlimentos] (
    [IdDieta]       INT          NOT NULL,
    [IdDietaAlim]   INT          IDENTITY (1, 1) NOT NULL,
    [IdAlimento]    INT          NOT NULL,
    [IdTpIndicacao] INT          NOT NULL,
    [Quant]         INT          NULL,
    [Ativo]         BIT          CONSTRAINT [DF_DietasAlimentos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]    INT          NULL,
    [IP]            VARCHAR (20) NULL,
    [DataCadastro]  DATETIME     CONSTRAINT [DF_DietasAlimentos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]        ROWVERSION   NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_DietasAlimentos1] PRIMARY KEY CLUSTERED ([IdDietaAlim] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_DietasAlimentos1] UNIQUE NONCLUSTERED ([IdDieta] ASC, [IdAlimento] ASC, [IdTpIndicacao] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[DietasAlimentos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DietasAlimentos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_DietasAlimentos] ([IdDietaAlim], [IdDieta], [IdAlimento], [IdTpIndicacao], [Quant], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdDietaAlim],
                 [IdDieta],
                 [IdAlimento],
                 [IdTpIndicacao],
                 [Quant],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[DietasAlimentos]
        ORDER BY [IdDietaAlim] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_DietasAlimentos] OFF;
    END

DROP TABLE [dbo].[DietasAlimentos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_DietasAlimentos]', N'DietasAlimentos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_DietasAlimentos1]', N'PK_DietasAlimentos', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_DietasAlimentos1]', N'IX_DietasAlimentos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[DietasAlimentos].[FK_DietasAlimentos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_DietasAlimentos_1]
    ON [dbo].[DietasAlimentos]([IdDieta] ASC);


GO
PRINT N'Creating [dbo].[DietasAlimentos].[FK_DietasAlimentos_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_DietasAlimentos_2]
    ON [dbo].[DietasAlimentos]([IdAlimento] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[ExigenciasNutrAuxIndicacoes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ExigenciasNutrAuxIndicacoes] (
    [IdIndic]      INT           IDENTITY (1, 1) NOT NULL,
    [Indicacao]    VARCHAR (150) NOT NULL,
    [Ativo]        BIT           CONSTRAINT [DF_NutrientesIndicacoes_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_NutrientesIndicacoes_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_NutrientesIndicacoes1] PRIMARY KEY CLUSTERED ([IdIndic] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_NutrientesIndicacoes1] UNIQUE NONCLUSTERED ([Indicacao] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ExigenciasNutrAuxIndicacoes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ExigenciasNutrAuxIndicacoes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ExigenciasNutrAuxIndicacoes] ([IdIndic], [Indicacao], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdIndic],
                 [Indicacao],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[ExigenciasNutrAuxIndicacoes]
        ORDER BY [IdIndic] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ExigenciasNutrAuxIndicacoes] OFF;
    END

DROP TABLE [dbo].[ExigenciasNutrAuxIndicacoes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ExigenciasNutrAuxIndicacoes]', N'ExigenciasNutrAuxIndicacoes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_NutrientesIndicacoes1]', N'PK_NutrientesIndicacoes', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_NutrientesIndicacoes1]', N'IX_NutrientesIndicacoes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[ExigenciasNutricionais]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ExigenciasNutricionais] (
    [IdExigNutr]   INT             IDENTITY (1, 1) NOT NULL,
    [IdTabNutr]    INT             NOT NULL,
    [IdEspecie]    INT             NOT NULL,
    [IdIndic]      INT             NOT NULL,
    [IdTpValor]    INT             NULL,
    [IdNutr1]      INT             NOT NULL,
    [IdUnidade1]   INT             NULL,
    [Proporcao1]   DECIMAL (10, 3) NULL,
    [Valor1]       DECIMAL (10, 3) NULL,
    [IdNutr2]      INT             NULL,
    [IdUnidade2]   INT             NULL,
    [Proporcao2]   DECIMAL (10, 3) NULL,
    [Valor2]       DECIMAL (10, 3) NULL,
    [Ativo]        BIT             CONSTRAINT [DF_ExigenciasNutricionais_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        CONSTRAINT [DF_ExigenciasNutricionais_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_ExigenciasNutricionais1] PRIMARY KEY CLUSTERED ([IdExigNutr] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_ExigenciasNutricionais1] UNIQUE NONCLUSTERED ([IdTabNutr] ASC, [IdEspecie] ASC, [IdIndic] ASC, [IdTpValor] ASC, [IdNutr1] ASC, [IdUnidade1] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ExigenciasNutricionais])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ExigenciasNutricionais] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ExigenciasNutricionais] ([IdExigNutr], [IdTabNutr], [IdEspecie], [IdIndic], [IdTpValor], [IdNutr1], [IdUnidade1], [Proporcao1], [Valor1], [IdNutr2], [IdUnidade2], [Proporcao2], [Valor2], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdExigNutr],
                 [IdTabNutr],
                 [IdEspecie],
                 [IdIndic],
                 [IdTpValor],
                 [IdNutr1],
                 [IdUnidade1],
                 [Proporcao1],
                 [Valor1],
                 [IdNutr2],
                 [IdUnidade2],
                 [Proporcao2],
                 [Valor2],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[ExigenciasNutricionais]
        ORDER BY [IdExigNutr] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ExigenciasNutricionais] OFF;
    END

DROP TABLE [dbo].[ExigenciasNutricionais];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ExigenciasNutricionais]', N'ExigenciasNutricionais';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_ExigenciasNutricionais1]', N'PK_ExigenciasNutricionais', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_ExigenciasNutricionais1]', N'IX_ExigenciasNutricionais', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais].[FK_ExigenciasNutricionais_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_ExigenciasNutricionais_1]
    ON [dbo].[ExigenciasNutricionais]([IdTabNutr] ASC);


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais].[FK_ExigenciasNutricionais_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_ExigenciasNutricionais_2]
    ON [dbo].[ExigenciasNutricionais]([IdEspecie] ASC);


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais].[FK_ExigenciasNutricionais_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_ExigenciasNutricionais_3]
    ON [dbo].[ExigenciasNutricionais]([IdIndic] ASC);


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais].[FK_ExigenciasNutricionais_4]...';


GO
CREATE NONCLUSTERED INDEX [FK_ExigenciasNutricionais_4]
    ON [dbo].[ExigenciasNutricionais]([IdNutr1] ASC);


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais].[FK_ExigenciasNutricionais_5]...';


GO
CREATE NONCLUSTERED INDEX [FK_ExigenciasNutricionais_5]
    ON [dbo].[ExigenciasNutricionais]([IdUnidade1] ASC);


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais].[FK_ExigenciasNutricionais_6]...';


GO
CREATE NONCLUSTERED INDEX [FK_ExigenciasNutricionais_6]
    ON [dbo].[ExigenciasNutricionais]([IdTpValor] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[Nutrientes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Nutrientes] (
    [IdGrupo]        INT             NOT NULL,
    [IdNutr]         INT             IDENTITY (1, 1) NOT NULL,
    [Nutriente]      VARCHAR (150)   NOT NULL,
    [IdUnidade]      INT             NOT NULL,
    [Referencia]     VARCHAR (50)    NULL,
    [ValMin]         DECIMAL (10, 3) NULL,
    [ValMax]         DECIMAL (10, 3) NULL,
    [ListarCardapio] BIT             NULL,
    [Ativo]          BIT             CONSTRAINT [DF_AlimentosAuxComponentes_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]     INT             NULL,
    [IP]             VARCHAR (20)    NULL,
    [DataCadastro]   DATETIME        CONSTRAINT [DF_AlimentosAuxComponentes_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]         ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AlimentosAuxComponentes1] PRIMARY KEY CLUSTERED ([IdNutr] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_AlimentosAuxComponentes1] UNIQUE NONCLUSTERED ([Nutriente] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Nutrientes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Nutrientes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Nutrientes] ([IdNutr], [IdGrupo], [Nutriente], [IdUnidade], [Referencia], [ValMin], [ValMax], [ListarCardapio], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdNutr],
                 [IdGrupo],
                 [Nutriente],
                 [IdUnidade],
                 [Referencia],
                 [ValMin],
                 [ValMax],
                 [ListarCardapio],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Nutrientes]
        ORDER BY [IdNutr] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Nutrientes] OFF;
    END

DROP TABLE [dbo].[Nutrientes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Nutrientes]', N'Nutrientes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AlimentosAuxComponentes1]', N'PK_AlimentosAuxComponentes', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_AlimentosAuxComponentes1]', N'IX_AlimentosAuxComponentes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[NutrientesAuxGrupos]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_NutrientesAuxGrupos] (
    [IdGrupo]      INT           IDENTITY (1, 1) NOT NULL,
    [Grupo]        VARCHAR (150) NOT NULL,
    [Ordem]        INT           NULL,
    [Ativo]        BIT           CONSTRAINT [DF_AlimentosComponentesAuxGrupos_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_AlimentosComponentesAuxGrupos_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AlimentosComponentesAuxGrupos1] PRIMARY KEY CLUSTERED ([IdGrupo] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[NutrientesAuxGrupos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_NutrientesAuxGrupos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_NutrientesAuxGrupos] ([IdGrupo], [Grupo], [Ordem], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdGrupo],
                 [Grupo],
                 [Ordem],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[NutrientesAuxGrupos]
        ORDER BY [IdGrupo] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_NutrientesAuxGrupos] OFF;
    END

DROP TABLE [dbo].[NutrientesAuxGrupos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_NutrientesAuxGrupos]', N'NutrientesAuxGrupos';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AlimentosComponentesAuxGrupos1]', N'PK_AlimentosComponentesAuxGrupos', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Pessoas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Pessoas] (
    [IdPessoa]       INT           IDENTITY (1, 1) NOT NULL,
    [IdTpPessoa]     INT           NOT NULL,
    [Nome]           VARCHAR (200) NOT NULL,
    [DataNascimento] DATE          NULL,
    [IdCliente]      INT           NULL,
    [RG]             VARCHAR (20)  NULL,
    [CPF]            VARCHAR (20)  NULL,
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
    [Logr_UF]        CHAR (2)      NULL,
    [Logra_Pais]     VARCHAR (150) NULL,
    [Latitude]       VARCHAR (50)  NULL,
    [Longitude]      VARCHAR (50)  NULL,
    [Usuario]        VARCHAR (10)  NULL,
    [Senha]          VARCHAR (50)  NULL,
    [Bloqueado]      BIT           NULL,
    [Ativo]          BIT           CONSTRAINT [DF_Pessoas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]     INT           NULL,
    [IP]             VARCHAR (20)  NULL,
    [DataCadastro]   DATETIME      CONSTRAINT [DF_Pessoas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]         ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Pessoas1] PRIMARY KEY CLUSTERED ([IdPessoa] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Pessoas1] UNIQUE NONCLUSTERED ([Nome] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Pessoas_11] UNIQUE NONCLUSTERED ([Email] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Pessoas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Pessoas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Pessoas] ([IdPessoa], [IdTpPessoa], [Nome], [DataNascimento], [IdCliente], [RG], [CPF], [CRVM], [Email], [Telefone], [Celular], [Logotipo], [Assinatura], [Logradouro], [Logr_Nr], [Logr_Compl], [Logr_Bairro], [Logr_CEP], [Logr_Cidade], [Logr_UF], [Logra_Pais], [Latitude], [Longitude], [Usuario], [Senha], [Bloqueado], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdPessoa],
                 [IdTpPessoa],
                 [Nome],
                 [DataNascimento],
                 [IdCliente],
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
                 [Logra_Pais],
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

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Pessoas1]', N'IX_Pessoas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Pessoas_11]', N'IX_Pessoas_1', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[PlanosAssinaturas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_PlanosAssinaturas] (
    [IdPlano]      INT           IDENTITY (1, 1) NOT NULL,
    [Plano]        VARCHAR (100) NOT NULL,
    [Ativo]        BIT           CONSTRAINT [DF_PlanosAssinaturas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_PlanosAssinaturas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_PlanosAssinaturas1] PRIMARY KEY CLUSTERED ([IdPlano] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_PlanosAssinaturas1] UNIQUE NONCLUSTERED ([Plano] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[PlanosAssinaturas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PlanosAssinaturas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_PlanosAssinaturas] ([IdPlano], [Plano], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdPlano],
                 [Plano],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[PlanosAssinaturas]
        ORDER BY [IdPlano] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PlanosAssinaturas] OFF;
    END

DROP TABLE [dbo].[PlanosAssinaturas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_PlanosAssinaturas]', N'PlanosAssinaturas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_PlanosAssinaturas1]', N'PK_PlanosAssinaturas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_PlanosAssinaturas1]', N'IX_PlanosAssinaturas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


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
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_PortalContato1] PRIMARY KEY CLUSTERED ([IdContato] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[PortalContato])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_PortalContato] ON;
        INSERT INTO [dbo].[tmp_ms_xx_PortalContato] ([IdContato], [NomeContato], [EmailContato], [Assunto], [Mensagem], [DataEnvio], [MsgSituacao], [DataResposta], [Observacoes])
        SELECT   [IdContato],
                 [NomeContato],
                 [EmailContato],
                 [Assunto],
                 [Mensagem],
                 [DataEnvio],
                 [MsgSituacao],
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
PRINT N'Creating [dbo].[FK_Acessos_AcessosAuxFuncoes]...';


GO
ALTER TABLE [dbo].[Acessos] WITH NOCHECK
    ADD CONSTRAINT [FK_Acessos_AcessosAuxFuncoes] FOREIGN KEY ([IdAcFunc]) REFERENCES [dbo].[AcessosAuxFuncoes] ([IdAcFunc]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Acessos_Pessoas]...';


GO
ALTER TABLE [dbo].[Acessos] WITH NOCHECK
    ADD CONSTRAINT [FK_Acessos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosFuncoesTelas_AcessosAuxFuncoes]...';


GO
ALTER TABLE [dbo].[AcessosFuncoesTelas] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosFuncoesTelas_AcessosAuxFuncoes] FOREIGN KEY ([IdAcFunc]) REFERENCES [dbo].[AcessosAuxFuncoes] ([IdAcFunc]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosFuncoesTelas_AcessosAuxTelas]...';


GO
ALTER TABLE [dbo].[AcessosFuncoesTelas] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosFuncoesTelas_AcessosAuxTelas] FOREIGN KEY ([IdTela]) REFERENCES [dbo].[AcessosAuxTelas] ([IdTela]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_Pessoas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_Pessoas1]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas1] FOREIGN KEY ([ComprovanteHomologador]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas] FOREIGN KEY ([IdPlano]) REFERENCES [dbo].[PlanosAssinaturas] ([IdPlano]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao] WITH NOCHECK
    ADD CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos] FOREIGN KEY ([IdVigencia]) REFERENCES [dbo].[AcessosVigenciaPlanos] ([IdVigencia]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AlimentoComponentes_AlimentosAuxComponentes]...';


GO
ALTER TABLE [dbo].[AlimentoNutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_AlimentoComponentes_AlimentosAuxComponentes] FOREIGN KEY ([IdNutr]) REFERENCES [dbo].[Nutrientes] ([IdNutr]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AlimentoComponentes_Alimentos]...';


GO
ALTER TABLE [dbo].[AlimentoNutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_AlimentoComponentes_Alimentos] FOREIGN KEY ([IdAlimento]) REFERENCES [dbo].[Alimentos] ([IdAlimento]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Alimentos_AlimentosAuxFontes]...';


GO
ALTER TABLE [dbo].[Alimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_Alimentos_AlimentosAuxFontes] FOREIGN KEY ([IdFonte]) REFERENCES [dbo].[AlimentosAuxFontes] ([IdFonte]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Alimentos_AlimentosAuxGrupos]...';


GO
ALTER TABLE [dbo].[Alimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_Alimentos_AlimentosAuxGrupos] FOREIGN KEY ([IdGrupo]) REFERENCES [dbo].[AlimentosAuxGrupos] ([IdGrupo]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Alimentos_Pessoas]...';


GO
ALTER TABLE [dbo].[Alimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_Alimentos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Animais_Pessoas]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Animais_AnimaisRacas]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_AnimaisRacas] FOREIGN KEY ([IdRaca]) REFERENCES [dbo].[AnimaisAuxRacas] ([IdRaca]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AnimaisAuxRacas_AnimaisAuxEspecies]...';


GO
ALTER TABLE [dbo].[AnimaisAuxRacas] WITH NOCHECK
    ADD CONSTRAINT [FK_AnimaisAuxRacas_AnimaisAuxEspecies] FOREIGN KEY ([IdEspecie]) REFERENCES [dbo].[AnimaisAuxEspecies] ([IdEspecie]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Cardapio_Dietas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Dietas] FOREIGN KEY ([IdDieta]) REFERENCES [dbo].[Dietas] ([IdDieta]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_CardapiosAlimentos_Cardapio]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_CardapiosAlimentos_Cardapio] FOREIGN KEY ([IdCardapio]) REFERENCES [dbo].[Cardapio] ([IdCardapio]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_CardapiosAlimentos_Alimentos]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_CardapiosAlimentos_Alimentos] FOREIGN KEY ([IdAlimento]) REFERENCES [dbo].[Alimentos] ([IdAlimento]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_DietasAlimentos_Dietas]...';


GO
ALTER TABLE [dbo].[DietasAlimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_DietasAlimentos_Dietas] FOREIGN KEY ([IdDieta]) REFERENCES [dbo].[Dietas] ([IdDieta]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_DietasAlimentos_Alimentos]...';


GO
ALTER TABLE [dbo].[DietasAlimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_DietasAlimentos_Alimentos] FOREIGN KEY ([IdAlimento]) REFERENCES [dbo].[Alimentos] ([IdAlimento]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_ExigenciasNutricionais_ExigenciasNutrAuxIndicacoes]...';


GO
ALTER TABLE [dbo].[ExigenciasNutricionais] WITH NOCHECK
    ADD CONSTRAINT [FK_ExigenciasNutricionais_ExigenciasNutrAuxIndicacoes] FOREIGN KEY ([IdIndic]) REFERENCES [dbo].[ExigenciasNutrAuxIndicacoes] ([IdIndic]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_ExigenciasNutricionais_Nutrientes]...';


GO
ALTER TABLE [dbo].[ExigenciasNutricionais] WITH NOCHECK
    ADD CONSTRAINT [FK_ExigenciasNutricionais_Nutrientes] FOREIGN KEY ([IdNutr1]) REFERENCES [dbo].[Nutrientes] ([IdNutr]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_ExigenciasNutricionais_AnimaisAuxEspecies]...';


GO
ALTER TABLE [dbo].[ExigenciasNutricionais] WITH NOCHECK
    ADD CONSTRAINT [FK_ExigenciasNutricionais_AnimaisAuxEspecies] FOREIGN KEY ([IdEspecie]) REFERENCES [dbo].[AnimaisAuxEspecies] ([IdEspecie]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_ExigenciasNutricionais_Nutrientes1]...';


GO
ALTER TABLE [dbo].[ExigenciasNutricionais] WITH NOCHECK
    ADD CONSTRAINT [FK_ExigenciasNutricionais_Nutrientes1] FOREIGN KEY ([IdNutr2]) REFERENCES [dbo].[Nutrientes] ([IdNutr]);


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Acessos] WITH CHECK CHECK CONSTRAINT [FK_Acessos_AcessosAuxFuncoes];

ALTER TABLE [dbo].[Acessos] WITH CHECK CHECK CONSTRAINT [FK_Acessos_Pessoas];

ALTER TABLE [dbo].[AcessosFuncoesTelas] WITH CHECK CHECK CONSTRAINT [FK_AcessosFuncoesTelas_AcessosAuxFuncoes];

ALTER TABLE [dbo].[AcessosFuncoesTelas] WITH CHECK CHECK CONSTRAINT [FK_AcessosFuncoesTelas_AcessosAuxTelas];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas1];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_PlanosAssinaturas];

ALTER TABLE [dbo].[AcessosVigenciaSituacao] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaSituacao_AcessosVigenciaPlanos];

ALTER TABLE [dbo].[AlimentoNutrientes] WITH CHECK CHECK CONSTRAINT [FK_AlimentoComponentes_AlimentosAuxComponentes];

ALTER TABLE [dbo].[AlimentoNutrientes] WITH CHECK CHECK CONSTRAINT [FK_AlimentoComponentes_Alimentos];

ALTER TABLE [dbo].[Alimentos] WITH CHECK CHECK CONSTRAINT [FK_Alimentos_AlimentosAuxFontes];

ALTER TABLE [dbo].[Alimentos] WITH CHECK CHECK CONSTRAINT [FK_Alimentos_AlimentosAuxGrupos];

ALTER TABLE [dbo].[Alimentos] WITH CHECK CHECK CONSTRAINT [FK_Alimentos_Pessoas];

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_Pessoas];

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_AnimaisRacas];

ALTER TABLE [dbo].[AnimaisAuxRacas] WITH CHECK CHECK CONSTRAINT [FK_AnimaisAuxRacas_AnimaisAuxEspecies];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Pessoas];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Animais];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Dietas];

ALTER TABLE [dbo].[CardapiosAlimentos] WITH CHECK CHECK CONSTRAINT [FK_CardapiosAlimentos_Cardapio];

ALTER TABLE [dbo].[CardapiosAlimentos] WITH CHECK CHECK CONSTRAINT [FK_CardapiosAlimentos_Alimentos];

ALTER TABLE [dbo].[DietasAlimentos] WITH CHECK CHECK CONSTRAINT [FK_DietasAlimentos_Dietas];

ALTER TABLE [dbo].[DietasAlimentos] WITH CHECK CHECK CONSTRAINT [FK_DietasAlimentos_Alimentos];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_ExigenciasNutrAuxIndicacoes];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_Nutrientes];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_AnimaisAuxEspecies];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_Nutrientes1];


GO
PRINT N'Update complete.';


GO
