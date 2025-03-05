
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating [dbo].[Acessos]...';


GO
CREATE TABLE [dbo].[Acessos] (
    [IdPessoa]       INT          NOT NULL,
    [IdAcFunc]       INT          NOT NULL,
    [Inserir]        BIT          NULL,
    [Alterar]        BIT          NULL,
    [Excluir]        BIT          NULL,
    [Consultar]      BIT          NULL,
    [AcoesEspeciais] BIT          NULL,
    [Relatorios]     BIT          NULL,
    [SuperUser]      BIT          NULL,
    [TermoUso]       BIT          NULL,
    [Ativo]          BIT          NULL,
    [IdOperador]     INT          NULL,
    [IP]             VARCHAR (20) NULL,
    [DataCadastro]   DATETIME     NULL,
    [Versao]         ROWVERSION   NULL,
    CONSTRAINT [PK_Acessos] PRIMARY KEY CLUSTERED ([IdPessoa] ASC)
);


GO
PRINT N'Creating [dbo].[Acessos].[FK_Acessos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Acessos_1]
    ON [dbo].[Acessos]([IdAcFunc] ASC);


GO
PRINT N'Creating [dbo].[AcessosAuxFuncoes]...';


GO
CREATE TABLE [dbo].[AcessosAuxFuncoes] (
    [IdAcFunc]     INT           IDENTITY (1, 1) NOT NULL,
    [Funcao]       VARCHAR (100) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AcessosAuxFuncoes] PRIMARY KEY CLUSTERED ([IdAcFunc] ASC),
    CONSTRAINT [IX_AcessosAuxFuncoes] UNIQUE NONCLUSTERED ([Funcao] ASC)
);


GO
PRINT N'Creating [dbo].[AcessosAuxTelas]...';


GO
CREATE TABLE [dbo].[AcessosAuxTelas] (
    [IdTela]       INT           IDENTITY (1, 1) NOT NULL,
    [CodTela]      VARCHAR (10)  NOT NULL,
    [Telas]        VARCHAR (100) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AcessosAuxTelas] PRIMARY KEY CLUSTERED ([IdTela] ASC),
    CONSTRAINT [IX_AcessosAuxTelas] UNIQUE NONCLUSTERED ([Telas] ASC),
    CONSTRAINT [IX_AcessosAuxTelas_1] UNIQUE NONCLUSTERED ([CodTela] ASC)
);


GO
PRINT N'Creating [dbo].[AcessosFuncoesTelas]...';


GO
CREATE TABLE [dbo].[AcessosFuncoesTelas] (
    [IdFuncTela]   INT          IDENTITY (1, 1) NOT NULL,
    [IdAcFunc]     INT          NOT NULL,
    [IdTela]       INT          NOT NULL,
    [Ativo]        BIT          NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [PK_AcessosFuncoesTelas] PRIMARY KEY CLUSTERED ([IdFuncTela] ASC),
    CONSTRAINT [IX_AcessosFuncoesTelas] UNIQUE NONCLUSTERED ([IdAcFunc] ASC, [IdTela] ASC)
);


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
PRINT N'Creating [dbo].[AcessosVigenciaCupomDesconto]...';


GO
CREATE TABLE [dbo].[AcessosVigenciaCupomDesconto] (
    [IdCupom]      INT          IDENTITY (1, 1) NOT NULL,
    [NrCumpom]     VARCHAR (20) NOT NULL,
    [IdTipoDesc]   INT          NULL,
    [ValorDesc]    INT          NOT NULL,
    [DtInicial]    DATE         NOT NULL,
    [DtFinal]      DATE         NOT NULL,
    [fUsado]       BIT          NULL,
    [Ativo]        BIT          NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [PK_AcessosVigenciaCupomDesconto] PRIMARY KEY CLUSTERED ([IdCupom] ASC),
    CONSTRAINT [IX_AcessosVigenciaCupomDesconto] UNIQUE NONCLUSTERED ([NrCumpom] ASC)
);


GO
PRINT N'Creating [dbo].[AcessosVigenciaPlanos]...';


GO
CREATE TABLE [dbo].[AcessosVigenciaPlanos] (
    [IdPessoa]               INT           NOT NULL,
    [IdVigencia]             INT           IDENTITY (1, 1) NOT NULL,
    [IdPlano]                INT           NOT NULL,
    [DtInicial]              DATE          NOT NULL,
    [DtFinal]                DATE          NOT NULL,
    [QtdAnim]                INT           NULL,
    [Periodo]                INT           NULL,
    [Receituario]            BIT           NULL,
    [Prontuario]             BIT           NULL,
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
    CONSTRAINT [PK_AcessosVigenciaPlanos] PRIMARY KEY CLUSTERED ([IdVigencia] ASC),
    CONSTRAINT [IX_AcessosVigenciaPlanos] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [IdPlano] ASC, [DtInicial] ASC, [DtFinal] ASC)
);


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
PRINT N'Creating [dbo].[AcessosVigenciaSituacao]...';


GO
CREATE TABLE [dbo].[AcessosVigenciaSituacao] (
    [IdVigencia]   INT          NOT NULL,
    [IdVigSit]     INT          IDENTITY (1, 1) NOT NULL,
    [IdSituacao]   INT          NOT NULL,
    [DataSituacao] DATE         NULL,
    [Ativo]        BIT          NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [PK_AcessosVigenciaSituacao] PRIMARY KEY CLUSTERED ([IdVigSit] ASC),
    CONSTRAINT [IX_AcessosVigenciaSituacao] UNIQUE NONCLUSTERED ([IdVigencia] ASC, [IdSituacao] ASC)
);


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
PRINT N'Creating [dbo].[AlimentoNutrientes]...';


GO
CREATE TABLE [dbo].[AlimentoNutrientes] (
    [IdAlimento]   INT             NOT NULL,
    [IdAlimNutr]   INT             IDENTITY (1, 1) NOT NULL,
    [IdNutr]       INT             NOT NULL,
    [Valor]        DECIMAL (18, 3) NOT NULL,
    [IdUnidade]    INT             NULL,
    [Ativo]        BIT             NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [PK_AlimentoComponentes] PRIMARY KEY CLUSTERED ([IdAlimNutr] ASC)
);


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
PRINT N'Creating [dbo].[Alimentos]...';


GO
CREATE TABLE [dbo].[Alimentos] (
    [IdPessoa]     INT           NULL,
    [IdAlimento]   INT           IDENTITY (1, 1) NOT NULL,
    [IdGrupo]      INT           NOT NULL,
    [IdFonte]      INT           NOT NULL,
    [NDB_No]       INT           NULL,
    [Alimento]     VARCHAR (150) NOT NULL,
    [Compartilhar] BIT           NULL,
    [fHomologado]  BIT           NULL,
    [DataHomol]    DATE          NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_Alimentos] PRIMARY KEY CLUSTERED ([IdAlimento] ASC),
    CONSTRAINT [IX_Alimentos] UNIQUE NONCLUSTERED ([IdFonte] ASC, [Alimento] ASC)
);


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
PRINT N'Creating [dbo].[AlimentosAuxFontes]...';


GO
CREATE TABLE [dbo].[AlimentosAuxFontes] (
    [IdFonte]      INT           IDENTITY (1, 1) NOT NULL,
    [Fonte]        VARCHAR (150) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AlimentosAuxFontes] PRIMARY KEY CLUSTERED ([IdFonte] ASC),
    CONSTRAINT [IX_AlimentosAuxFontes] UNIQUE NONCLUSTERED ([Fonte] ASC)
);


GO
PRINT N'Creating [dbo].[AlimentosAuxGrupos]...';


GO
CREATE TABLE [dbo].[AlimentosAuxGrupos] (
    [IdGrupo]       INT           IDENTITY (1, 1) NOT NULL,
    [GrupoAlimento] VARCHAR (150) NOT NULL,
    [Ativo]         BIT           NULL,
    [IdOperador]    INT           NULL,
    [IP]            VARCHAR (20)  NULL,
    [DataCadastro]  DATETIME      NULL,
    [Versao]        ROWVERSION    NULL,
    CONSTRAINT [PK_AlimentosAuxGrupos] PRIMARY KEY CLUSTERED ([IdGrupo] ASC),
    CONSTRAINT [IX_AlimentosAuxGrupos] UNIQUE NONCLUSTERED ([GrupoAlimento] ASC)
);


GO
PRINT N'Creating [dbo].[Animais]...';


GO
CREATE TABLE [dbo].[Animais] (
    [IdPessoa]     INT             NOT NULL,
    [IdAnimal]     INT             IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (150)   NOT NULL,
    [IdRaca]       INT             NULL,
    [Sexo]         INT             NULL,
    [DtNascim]     DATE            NULL,
    [PesoAtual]    DECIMAL (10, 3) NULL,
    [PesoIdeal]    DECIMAL (10, 3) NULL,
    [Ativo]        BIT             NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [PK_Animais] PRIMARY KEY CLUSTERED ([IdAnimal] ASC),
    CONSTRAINT [IX_Animais] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [Nome] ASC)
);


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
PRINT N'Creating [dbo].[AnimaisAuxEspecies]...';


GO
CREATE TABLE [dbo].[AnimaisAuxEspecies] (
    [IdEspecie]    INT           IDENTITY (1, 1) NOT NULL,
    [Especie]      VARCHAR (100) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AnimaisAuxEspecies] PRIMARY KEY CLUSTERED ([IdEspecie] ASC),
    CONSTRAINT [IX_AnimaisAuxEspecies] UNIQUE NONCLUSTERED ([Especie] ASC)
);


GO
PRINT N'Creating [dbo].[AnimaisAuxRacas]...';


GO
CREATE TABLE [dbo].[AnimaisAuxRacas] (
    [IdEspecie]    INT           NOT NULL,
    [IdRaca]       INT           IDENTITY (1, 1) NOT NULL,
    [Raca]         VARCHAR (100) NOT NULL,
    [IdadeAdulta]  INT           NULL,
    [CrescInicial] INT           NULL,
    [CrescFinal]   INT           NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AnimaisRacas] PRIMARY KEY CLUSTERED ([IdRaca] ASC),
    CONSTRAINT [IX_AnimaisRacas] UNIQUE NONCLUSTERED ([IdEspecie] ASC, [Raca] ASC)
);


GO
PRINT N'Creating [dbo].[Cardapio]...';


GO
CREATE TABLE [dbo].[Cardapio] (
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
    [Ativo]           BIT             NULL,
    [IdOperador]      INT             NULL,
    [IP]              VARCHAR (20)    NULL,
    [DataCadastro]    DATETIME        NULL,
    [Versao]          ROWVERSION      NULL,
    CONSTRAINT [PK_Cardapio] PRIMARY KEY CLUSTERED ([IdCardapio] ASC)
);


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
PRINT N'Creating [dbo].[CardapiosAlimentos]...';


GO
CREATE TABLE [dbo].[CardapiosAlimentos] (
    [IdCardapio]   INT          NOT NULL,
    [IdCardapAlim] INT          IDENTITY (1, 1) NOT NULL,
    [IdAlimento]   INT          NOT NULL,
    [Quant]        INT          NULL,
    [Ativo]        BIT          NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [PK_CardapiosAlimentos] PRIMARY KEY CLUSTERED ([IdCardapAlim] ASC),
    CONSTRAINT [IX_CardapiosAlimentos] UNIQUE NONCLUSTERED ([IdCardapio] ASC, [IdAlimento] ASC)
);


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
PRINT N'Creating [dbo].[Dietas]...';


GO
CREATE TABLE [dbo].[Dietas] (
    [IdPessoa]     INT             NOT NULL,
    [IdDieta]      INT             IDENTITY (1, 1) NOT NULL,
    [Dieta]        VARCHAR (150)   NOT NULL,
    [Carboidrato]  DECIMAL (10, 3) NULL,
    [Proteina]     DECIMAL (10, 3) NULL,
    [Gordura]      DECIMAL (10, 3) NULL,
    [Ativo]        BIT             NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [PK_Dietas] PRIMARY KEY CLUSTERED ([IdDieta] ASC)
);


GO
PRINT N'Creating [dbo].[Dietas].[IX_Dietas]...';


GO
CREATE NONCLUSTERED INDEX [IX_Dietas]
    ON [dbo].[Dietas]([Dieta] ASC);


GO
PRINT N'Creating [dbo].[Dietas].[FK_Dietas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Dietas_1]
    ON [dbo].[Dietas]([IdPessoa] ASC);


GO
PRINT N'Creating [dbo].[DietasAlimentos]...';


GO
CREATE TABLE [dbo].[DietasAlimentos] (
    [IdDieta]       INT          NOT NULL,
    [IdDietaAlim]   INT          IDENTITY (1, 1) NOT NULL,
    [IdAlimento]    INT          NOT NULL,
    [IdTpIndicacao] INT          NOT NULL,
    [Quant]         INT          NULL,
    [Ativo]         BIT          NULL,
    [IdOperador]    INT          NULL,
    [IP]            VARCHAR (20) NULL,
    [DataCadastro]  DATETIME     NULL,
    [Versao]        ROWVERSION   NULL,
    CONSTRAINT [PK_DietasAlimentos] PRIMARY KEY CLUSTERED ([IdDietaAlim] ASC),
    CONSTRAINT [IX_DietasAlimentos] UNIQUE NONCLUSTERED ([IdDieta] ASC, [IdAlimento] ASC, [IdTpIndicacao] ASC)
);


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
PRINT N'Creating [dbo].[ExigenciasNutrAuxIndicacoes]...';


GO
CREATE TABLE [dbo].[ExigenciasNutrAuxIndicacoes] (
    [IdIndic]      INT           IDENTITY (1, 1) NOT NULL,
    [Indicacao]    VARCHAR (150) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_NutrientesIndicacoes] PRIMARY KEY CLUSTERED ([IdIndic] ASC),
    CONSTRAINT [IX_NutrientesIndicacoes] UNIQUE NONCLUSTERED ([Indicacao] ASC)
);


GO
PRINT N'Creating [dbo].[ExigenciasNutricionais]...';


GO
CREATE TABLE [dbo].[ExigenciasNutricionais] (
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
    [Ativo]        BIT             NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [PK_ExigenciasNutricionais] PRIMARY KEY CLUSTERED ([IdExigNutr] ASC),
    CONSTRAINT [IX_ExigenciasNutricionais] UNIQUE NONCLUSTERED ([IdTabNutr] ASC, [IdEspecie] ASC, [IdIndic] ASC, [IdTpValor] ASC, [IdNutr1] ASC, [IdUnidade1] ASC)
);


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
PRINT N'Creating [dbo].[Nutrientes]...';


GO
CREATE TABLE [dbo].[Nutrientes] (
    [IdGrupo]        INT             NOT NULL,
    [IdNutr]         INT             IDENTITY (1, 1) NOT NULL,
    [Nutriente]      VARCHAR (150)   NOT NULL,
    [IdUnidade]      INT             NOT NULL,
    [Referencia]     VARCHAR (50)    NULL,
    [ValMin]         DECIMAL (10, 3) NULL,
    [ValMax]         DECIMAL (10, 3) NULL,
    [ListarCardapio] BIT             NULL,
    [Ativo]          BIT             NULL,
    [IdOperador]     INT             NULL,
    [IP]             VARCHAR (20)    NULL,
    [DataCadastro]   DATETIME        NULL,
    [Versao]         ROWVERSION      NULL,
    CONSTRAINT [PK_AlimentosAuxComponentes] PRIMARY KEY CLUSTERED ([IdNutr] ASC),
    CONSTRAINT [IX_AlimentosAuxComponentes] UNIQUE NONCLUSTERED ([Nutriente] ASC)
);


GO
PRINT N'Creating [dbo].[Nutrientes].[FK_AlimentosAuxComponentes_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentosAuxComponentes_1]
    ON [dbo].[Nutrientes]([IdGrupo] ASC);


GO
PRINT N'Creating [dbo].[Nutrientes].[FK_AlimentosAuxComponentes_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentosAuxComponentes_2]
    ON [dbo].[Nutrientes]([IdUnidade] ASC);


GO
PRINT N'Creating [dbo].[NutrientesAuxGrupos]...';


GO
CREATE TABLE [dbo].[NutrientesAuxGrupos] (
    [IdGrupo]      INT           IDENTITY (1, 1) NOT NULL,
    [Grupo]        VARCHAR (150) NOT NULL,
    [Ordem]        INT           NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AlimentosComponentesAuxGrupos] PRIMARY KEY CLUSTERED ([IdGrupo] ASC)
);


GO
PRINT N'Creating [dbo].[Pessoas]...';


GO
CREATE TABLE [dbo].[Pessoas] (
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
    [Ativo]          BIT           NULL,
    [IdOperador]     INT           NULL,
    [IP]             VARCHAR (20)  NULL,
    [DataCadastro]   DATETIME      NULL,
    [Versao]         ROWVERSION    NULL,
    CONSTRAINT [PK_Pessoas] PRIMARY KEY CLUSTERED ([IdPessoa] ASC),
    CONSTRAINT [IX_Pessoas] UNIQUE NONCLUSTERED ([Nome] ASC),
    CONSTRAINT [IX_Pessoas_1] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Creating [dbo].[Pessoas].[FK_Pessoas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Pessoas_1]
    ON [dbo].[Pessoas]([IdTpPessoa] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[FK_Pessoas_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Pessoas_2]
    ON [dbo].[Pessoas]([IdCliente] ASC);


GO
PRINT N'Creating [dbo].[PlanosAssinaturas]...';


GO
CREATE TABLE [dbo].[PlanosAssinaturas] (
    [IdPlano]      INT           IDENTITY (1, 1) NOT NULL,
    [Plano]        VARCHAR (100) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_PlanosAssinaturas] PRIMARY KEY CLUSTERED ([IdPlano] ASC),
    CONSTRAINT [IX_PlanosAssinaturas] UNIQUE NONCLUSTERED ([Plano] ASC)
);


GO
PRINT N'Creating [dbo].[PortalContato]...';


GO
CREATE TABLE [dbo].[PortalContato] (
    [IdContato]    INT           IDENTITY (1, 1) NOT NULL,
    [NomeContato]  VARCHAR (250) NULL,
    [EmailContato] VARCHAR (250) NULL,
    [Assunto]      VARCHAR (250) NULL,
    [Mensagem]     VARCHAR (MAX) NULL,
    [DataEnvio]    DATE          NULL,
    [MsgSituacao]  INT           NULL,
    [DataResposta] DATE          NULL,
    [Observacoes]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_PortalContato] PRIMARY KEY CLUSTERED ([IdContato] ASC)
);


GO
PRINT N'Creating [dbo].[PortalContato].[IX_PortalContato]...';


GO
CREATE NONCLUSTERED INDEX [IX_PortalContato]
    ON [dbo].[PortalContato]([DataEnvio] ASC);


GO
PRINT N'Creating [dbo].[DF_Acessos_Inserir]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_Inserir] DEFAULT ((0)) FOR [Inserir];


GO
PRINT N'Creating [dbo].[DF_Acessos_Alterar]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_Alterar] DEFAULT ((0)) FOR [Alterar];


GO
PRINT N'Creating [dbo].[DF_Acessos_Excluir]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_Excluir] DEFAULT ((0)) FOR [Excluir];


GO
PRINT N'Creating [dbo].[DF_Acessos_Consultar]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_Consultar] DEFAULT ((0)) FOR [Consultar];


GO
PRINT N'Creating [dbo].[DF_Acessos_Relatorios]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_Relatorios] DEFAULT ((0)) FOR [Relatorios];


GO
PRINT N'Creating [dbo].[DF_Acessos_TermoUso]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_TermoUso] DEFAULT ((0)) FOR [TermoUso];


GO
PRINT N'Creating [dbo].[DF_Acessos_Ativo]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Acessos_DataCadastro]...';


GO
ALTER TABLE [dbo].[Acessos]
    ADD CONSTRAINT [DF_Acessos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AcessosAuxFuncoes_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosAuxFuncoes]
    ADD CONSTRAINT [DF_AcessosAuxFuncoes_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AcessosAuxFuncoes_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosAuxFuncoes]
    ADD CONSTRAINT [DF_AcessosAuxFuncoes_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AcessosAuxTelas_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosAuxTelas]
    ADD CONSTRAINT [DF_AcessosAuxTelas_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AcessosAuxTelas_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosAuxTelas]
    ADD CONSTRAINT [DF_AcessosAuxTelas_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AcessosFuncoesTelas_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosFuncoesTelas]
    ADD CONSTRAINT [DF_AcessosFuncoesTelas_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AcessosFuncoesTelas_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosFuncoesTelas]
    ADD CONSTRAINT [DF_AcessosFuncoesTelas_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


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
PRINT N'Creating [dbo].[DF_AcessosVigenciaCupomDesconto_fUsado]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaCupomDesconto]
    ADD CONSTRAINT [DF_AcessosVigenciaCupomDesconto_fUsado] DEFAULT ((0)) FOR [fUsado];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaPlanos_Receituario]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos]
    ADD CONSTRAINT [DF_AcessosVigenciaPlanos_Receituario] DEFAULT ((0)) FOR [Receituario];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaPlanos_Prontuario]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos]
    ADD CONSTRAINT [DF_AcessosVigenciaPlanos_Prontuario] DEFAULT ((0)) FOR [Prontuario];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaPlanos_ComprovanteAnexou]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos]
    ADD CONSTRAINT [DF_AcessosVigenciaPlanos_ComprovanteAnexou] DEFAULT ((0)) FOR [ComprovanteAnexou];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaPlanos_ComprovanteHomologado]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos]
    ADD CONSTRAINT [DF_AcessosVigenciaPlanos_ComprovanteHomologado] DEFAULT ((0)) FOR [ComprovanteHomologado];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaPlanos_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos]
    ADD CONSTRAINT [DF_AcessosVigenciaPlanos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaPlanos_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos]
    ADD CONSTRAINT [DF_AcessosVigenciaPlanos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaSituacao_Ativo]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao]
    ADD CONSTRAINT [DF_AcessosVigenciaSituacao_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AcessosVigenciaSituacao_DataCadastro]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaSituacao]
    ADD CONSTRAINT [DF_AcessosVigenciaSituacao_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AlimentoComponentes_Ativo]...';


GO
ALTER TABLE [dbo].[AlimentoNutrientes]
    ADD CONSTRAINT [DF_AlimentoComponentes_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AlimentoComponentes_DataCadastro]...';


GO
ALTER TABLE [dbo].[AlimentoNutrientes]
    ADD CONSTRAINT [DF_AlimentoComponentes_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_Alimentos_fHomologado]...';


GO
ALTER TABLE [dbo].[Alimentos]
    ADD CONSTRAINT [DF_Alimentos_fHomologado] DEFAULT ((0)) FOR [fHomologado];


GO
PRINT N'Creating [dbo].[DF_Alimentos_Compartilhar]...';


GO
ALTER TABLE [dbo].[Alimentos]
    ADD CONSTRAINT [DF_Alimentos_Compartilhar] DEFAULT ((0)) FOR [Compartilhar];


GO
PRINT N'Creating [dbo].[DF_Alimentos_Ativo]...';


GO
ALTER TABLE [dbo].[Alimentos]
    ADD CONSTRAINT [DF_Alimentos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Alimentos_DataCadastro]...';


GO
ALTER TABLE [dbo].[Alimentos]
    ADD CONSTRAINT [DF_Alimentos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AlimentosAuxFontes_Ativo]...';


GO
ALTER TABLE [dbo].[AlimentosAuxFontes]
    ADD CONSTRAINT [DF_AlimentosAuxFontes_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AlimentosAuxFontes_DataCadastro]...';


GO
ALTER TABLE [dbo].[AlimentosAuxFontes]
    ADD CONSTRAINT [DF_AlimentosAuxFontes_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AlimentosAuxGrupos_Ativo]...';


GO
ALTER TABLE [dbo].[AlimentosAuxGrupos]
    ADD CONSTRAINT [DF_AlimentosAuxGrupos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AlimentosAuxGrupos_DataCadastro]...';


GO
ALTER TABLE [dbo].[AlimentosAuxGrupos]
    ADD CONSTRAINT [DF_AlimentosAuxGrupos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_Animais_Ativo]...';


GO
ALTER TABLE [dbo].[Animais]
    ADD CONSTRAINT [DF_Animais_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Animais_DataCadastro]...';


GO
ALTER TABLE [dbo].[Animais]
    ADD CONSTRAINT [DF_Animais_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AnimaisAuxEspecies_Ativo]...';


GO
ALTER TABLE [dbo].[AnimaisAuxEspecies]
    ADD CONSTRAINT [DF_AnimaisAuxEspecies_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AnimaisAuxEspecies_DataCadastro]...';


GO
ALTER TABLE [dbo].[AnimaisAuxEspecies]
    ADD CONSTRAINT [DF_AnimaisAuxEspecies_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AnimaisRacas_Ativo]...';


GO
ALTER TABLE [dbo].[AnimaisAuxRacas]
    ADD CONSTRAINT [DF_AnimaisRacas_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AnimaisRacas_DataCadastro]...';


GO
ALTER TABLE [dbo].[AnimaisAuxRacas]
    ADD CONSTRAINT [DF_AnimaisRacas_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_Cardapio_Ativo]...';


GO
ALTER TABLE [dbo].[Cardapio]
    ADD CONSTRAINT [DF_Cardapio_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Cardapio_DataCadastro]...';


GO
ALTER TABLE [dbo].[Cardapio]
    ADD CONSTRAINT [DF_Cardapio_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_CardapiosAlimentos_Ativo]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos]
    ADD CONSTRAINT [DF_CardapiosAlimentos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_CardapiosAlimentos_DataCadastro]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos]
    ADD CONSTRAINT [DF_CardapiosAlimentos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_Dietas_Ativo]...';


GO
ALTER TABLE [dbo].[Dietas]
    ADD CONSTRAINT [DF_Dietas_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Dietas_DataCadastro]...';


GO
ALTER TABLE [dbo].[Dietas]
    ADD CONSTRAINT [DF_Dietas_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_DietasAlimentos_Ativo]...';


GO
ALTER TABLE [dbo].[DietasAlimentos]
    ADD CONSTRAINT [DF_DietasAlimentos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_DietasAlimentos_DataCadastro]...';


GO
ALTER TABLE [dbo].[DietasAlimentos]
    ADD CONSTRAINT [DF_DietasAlimentos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_NutrientesIndicacoes_Ativo]...';


GO
ALTER TABLE [dbo].[ExigenciasNutrAuxIndicacoes]
    ADD CONSTRAINT [DF_NutrientesIndicacoes_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_NutrientesIndicacoes_DataCadastro]...';


GO
ALTER TABLE [dbo].[ExigenciasNutrAuxIndicacoes]
    ADD CONSTRAINT [DF_NutrientesIndicacoes_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_ExigenciasNutricionais_Ativo]...';


GO
ALTER TABLE [dbo].[ExigenciasNutricionais]
    ADD CONSTRAINT [DF_ExigenciasNutricionais_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_ExigenciasNutricionais_DataCadastro]...';


GO
ALTER TABLE [dbo].[ExigenciasNutricionais]
    ADD CONSTRAINT [DF_ExigenciasNutricionais_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AlimentosAuxComponentes_Ativo]...';


GO
ALTER TABLE [dbo].[Nutrientes]
    ADD CONSTRAINT [DF_AlimentosAuxComponentes_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AlimentosAuxComponentes_DataCadastro]...';


GO
ALTER TABLE [dbo].[Nutrientes]
    ADD CONSTRAINT [DF_AlimentosAuxComponentes_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_AlimentosComponentesAuxGrupos_Ativo]...';


GO
ALTER TABLE [dbo].[NutrientesAuxGrupos]
    ADD CONSTRAINT [DF_AlimentosComponentesAuxGrupos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_AlimentosComponentesAuxGrupos_DataCadastro]...';


GO
ALTER TABLE [dbo].[NutrientesAuxGrupos]
    ADD CONSTRAINT [DF_AlimentosComponentesAuxGrupos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_Pessoas_Ativo]...';


GO
ALTER TABLE [dbo].[Pessoas]
    ADD CONSTRAINT [DF_Pessoas_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Pessoas_DataCadastro]...';


GO
ALTER TABLE [dbo].[Pessoas]
    ADD CONSTRAINT [DF_Pessoas_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_PlanosAssinaturas_Ativo]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas]
    ADD CONSTRAINT [DF_PlanosAssinaturas_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_PlanosAssinaturas_DataCadastro]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas]
    ADD CONSTRAINT [DF_PlanosAssinaturas_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


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
PRINT N'Creating [dbo].[FK_Dietas_Pessoas]...';


GO
ALTER TABLE [dbo].[Dietas] WITH NOCHECK
    ADD CONSTRAINT [FK_Dietas_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


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
PRINT N'Creating [dbo].[FK_AlimentosAuxComponentes_AlimentosComponentesAuxGrupos]...';


GO
ALTER TABLE [dbo].[Nutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_AlimentosAuxComponentes_AlimentosComponentesAuxGrupos] FOREIGN KEY ([IdGrupo]) REFERENCES [dbo].[NutrientesAuxGrupos] ([IdGrupo]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Pessoas_Pessoas]...';


GO
ALTER TABLE [dbo].[Pessoas] WITH NOCHECK
    ADD CONSTRAINT [FK_Pessoas_Pessoas] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


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

ALTER TABLE [dbo].[Dietas] WITH CHECK CHECK CONSTRAINT [FK_Dietas_Pessoas];

ALTER TABLE [dbo].[DietasAlimentos] WITH CHECK CHECK CONSTRAINT [FK_DietasAlimentos_Dietas];

ALTER TABLE [dbo].[DietasAlimentos] WITH CHECK CHECK CONSTRAINT [FK_DietasAlimentos_Alimentos];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_ExigenciasNutrAuxIndicacoes];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_Nutrientes];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_AnimaisAuxEspecies];

ALTER TABLE [dbo].[ExigenciasNutricionais] WITH CHECK CHECK CONSTRAINT [FK_ExigenciasNutricionais_Nutrientes1];

ALTER TABLE [dbo].[Nutrientes] WITH CHECK CHECK CONSTRAINT [FK_AlimentosAuxComponentes_AlimentosComponentesAuxGrupos];

ALTER TABLE [dbo].[Pessoas] WITH CHECK CHECK CONSTRAINT [FK_Pessoas_Pessoas];


GO
PRINT N'Update complete.';


GO
