
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping Default Constraint [dbo].[DF_Cardapio_Ativo]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [DF_Cardapio_Ativo];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_Cardapio_DataCadastro]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [DF_Cardapio_DataCadastro];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_ConfigReceituario_Ativo]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] DROP CONSTRAINT [DF_ConfigReceituario_Ativo];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_ConfigReceituario_DataCadastro]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] DROP CONSTRAINT [DF_ConfigReceituario_DataCadastro];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_Receituario_DataCadastro]...';


GO
ALTER TABLE [dbo].[Receituario] DROP CONSTRAINT [DF_Receituario_DataCadastro];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_Receituario_Ativo]...';


GO
ALTER TABLE [dbo].[Receituario] DROP CONSTRAINT [DF_Receituario_Ativo];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Pessoas];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Animais];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Cardapio_Dietas]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Dietas];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Receituario_Cardapio]...';


GO
ALTER TABLE [dbo].[Receituario] DROP CONSTRAINT [FK_Receituario_Cardapio];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_CardapiosAlimentos_Cardapio]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos] DROP CONSTRAINT [FK_CardapiosAlimentos_Cardapio];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_ConfigReceituario_Pessoas]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] DROP CONSTRAINT [FK_ConfigReceituario_Pessoas];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_ReceituarioNutrientes_Receituario]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes] DROP CONSTRAINT [FK_ReceituarioNutrientes_Receituario];


GO
PRINT N'Starting rebuilding table [dbo].[Cardapio]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Cardapio] (
    [IdPessoa]        INT             NOT NULL,
    [IdAnimal]        INT             NULL,
    [IdCardapio]      INT             IDENTITY (1, 1) NOT NULL,
    [Descricao]       VARCHAR (200)   NULL,
    [DtCardapio]      DATE            NOT NULL,
    [FatorEnergia]    DECIMAL (10, 3) NULL,
    [NEM]             DECIMAL (10, 3) NULL,
    [Gestante]        BIT             NULL,
    [Lactante]        BIT             NULL,
    [LactacaoSemanas] INT             NULL,
    [NrFilhotes]      INT             NULL,
    [IdDieta]         INT             NULL,
    [EmCardapio]      DECIMAL (10, 3) NULL,
    [NrCardapio]      INT             CONSTRAINT [DF_Cardapio_NrDieta] DEFAULT ((0)) NULL,
    [Arquivo]         VARCHAR (500)   NULL,
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
        INSERT INTO [dbo].[tmp_ms_xx_Cardapio] ([IdCardapio], [IdPessoa], [IdAnimal], [Descricao], [DtCardapio], [FatorEnergia], [NEM], [Gestante], [Lactante], [LactacaoSemanas], [NrFilhotes], [IdDieta], [EmCardapio], [Observacao], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdCardapio],
                 [IdPessoa],
                 [IdAnimal],
                 [Descricao],
                 [DtCardapio],
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
PRINT N'Creating Index [dbo].[Cardapio].[FK_Cardapio_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Cardapio_1]
    ON [dbo].[Cardapio]([IdAnimal] ASC);


GO
PRINT N'Creating Index [dbo].[Cardapio].[FK_Cardapio_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Cardapio_2]
    ON [dbo].[Cardapio]([IdDieta] ASC);


GO
PRINT N'Creating Index [dbo].[Cardapio].[FK_Cardapio_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_Cardapio_3]
    ON [dbo].[Cardapio]([IdPessoa] ASC);


GO
PRINT N'Creating Index [dbo].[Cardapio].[IX_Cardapio_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_Cardapio_1]
    ON [dbo].[Cardapio]([DtCardapio] ASC);


GO
PRINT N'Creating Index [dbo].[Cardapio].[IX_Cardapio]...';


GO
CREATE NONCLUSTERED INDEX [IX_Cardapio]
    ON [dbo].[Cardapio]([Descricao] ASC);


GO
PRINT N'Creating Index [dbo].[Cardapio].[IX_Cardapio_2]...';


GO
CREATE NONCLUSTERED INDEX [IX_Cardapio_2]
    ON [dbo].[Cardapio]([NrCardapio] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[ConfigReceituario]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ConfigReceituario] (
    [IdPessoa]     INT           NOT NULL,
    [DtIniUso]     DATE          NULL,
    [NomeClinica]  VARCHAR (300) NULL,
    [Slogan]       VARCHAR (MAX) NULL,
    [Site]         VARCHAR (300) NULL,
    [Facebook]     VARCHAR (300) NULL,
    [Twitter]      VARCHAR (300) NULL,
    [Instagram]    VARCHAR (300) NULL,
    [Logotipo]     VARCHAR (250) NULL,
    [Assinatura]   VARCHAR (250) NULL,
    [CRMV]         VARCHAR (50)  NULL,
    [CrmvUf]       VARCHAR (10)  NULL,
    [Email]        VARCHAR (200) NULL,
    [Telefone]     VARCHAR (20)  NULL,
    [Celular]      VARCHAR (20)  NULL,
    [Logradouro]   VARCHAR (300) NULL,
    [Logr_Nr]      VARCHAR (10)  NULL,
    [Logr_Compl]   VARCHAR (100) NULL,
    [Logr_Bairro]  VARCHAR (200) NULL,
    [Logr_CEP]     VARCHAR (10)  NULL,
    [Logr_Cidade]  VARCHAR (200) NULL,
    [Logr_UF]      VARCHAR (10)  NULL,
    [Logr_Pais]    VARCHAR (150) NULL,
    [Ativo]        BIT           CONSTRAINT [DF_ConfigReceituario_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_ConfigReceituario_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_ConfigReceituario1] PRIMARY KEY CLUSTERED ([IdPessoa] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ConfigReceituario])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_ConfigReceituario] ([IdPessoa], [NomeClinica], [Slogan], [Site], [Facebook], [Twitter], [Instagram], [Logotipo], [Assinatura], [CRMV], [CrmvUf], [Email], [Telefone], [Celular], [Logradouro], [Logr_Nr], [Logr_Compl], [Logr_Bairro], [Logr_CEP], [Logr_Cidade], [Logr_UF], [Logr_Pais], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdPessoa],
                 [NomeClinica],
                 [Slogan],
                 [Site],
                 [Facebook],
                 [Twitter],
                 [Instagram],
                 [Logotipo],
                 [Assinatura],
                 [CRMV],
                 [CrmvUf],
                 [Email],
                 [Telefone],
                 [Celular],
                 [Logradouro],
                 [Logr_Nr],
                 [Logr_Compl],
                 [Logr_Bairro],
                 [Logr_CEP],
                 [Logr_Cidade],
                 [Logr_UF],
                 [Logr_Pais],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[ConfigReceituario]
        ORDER BY [IdPessoa] ASC;
    END

DROP TABLE [dbo].[ConfigReceituario];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ConfigReceituario]', N'ConfigReceituario';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_ConfigReceituario1]', N'PK_ConfigReceituario', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Receituario]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Receituario] (
    [IdCardapio]        INT           NOT NULL,
    [IdReceita]         INT           IDENTITY (1, 1) NOT NULL,
    [dTpRec]            INT           NOT NULL,
    [Titulo]            VARCHAR (200) NULL,
    [Veiculo]           VARCHAR (100) NULL,
    [QuantVeic]         VARCHAR (50)  NULL,
    [Posologia]         VARCHAR (MAX) NULL,
    [VeiculoSid]        VARCHAR (100) NULL,
    [QuantVeicSid]      VARCHAR (50)  NULL,
    [PosologiaSid]      VARCHAR (MAX) NULL,
    [VeiculoTid]        VARCHAR (100) NULL,
    [QuantVeicTid]      VARCHAR (50)  NULL,
    [PosologiaTid]      VARCHAR (MAX) NULL,
    [Prescricao]        VARCHAR (MAX) NULL,
    [InstrucoesReceita] VARCHAR (500) NULL,
    [DataReceita]       DATE          NULL,
    [LocalReceita]      VARCHAR (300) NULL,
    [Arquivo]           VARCHAR (500) NULL,
    [NrReceita]         VARCHAR (20)  NULL,
    [Ativo]             BIT           CONSTRAINT [DF_Receituario_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]        INT           NULL,
    [IP]                VARCHAR (20)  NULL,
    [DataCadastro]      DATETIME      CONSTRAINT [DF_Receituario_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]            ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_RECEITUARIO1] PRIMARY KEY CLUSTERED ([IdReceita] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Receituario])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Receituario] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Receituario] ([IdReceita], [IdCardapio], [dTpRec], [Titulo], [Veiculo], [QuantVeic], [Posologia], [Prescricao], [InstrucoesReceita], [DataReceita], [LocalReceita], [Arquivo], [NrReceita], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdReceita],
                 [IdCardapio],
                 [dTpRec],
                 [Titulo],
                 [Veiculo],
                 [QuantVeic],
                 [Posologia],
                 [Prescricao],
                 [InstrucoesReceita],
                 [DataReceita],
                 [LocalReceita],
                 [Arquivo],
                 [NrReceita],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Receituario]
        ORDER BY [IdReceita] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Receituario] OFF;
    END

DROP TABLE [dbo].[Receituario];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Receituario]', N'Receituario';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_RECEITUARIO1]', N'PK_RECEITUARIO', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Index [dbo].[Receituario].[FK_Receituario_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Receituario_1]
    ON [dbo].[Receituario]([IdCardapio] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[FK_Receituario_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Receituario_2]
    ON [dbo].[Receituario]([dTpRec] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[IX_Receituario]...';


GO
CREATE NONCLUSTERED INDEX [IX_Receituario]
    ON [dbo].[Receituario]([NrReceita] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[IX_Receituario_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_Receituario_1]
    ON [dbo].[Receituario]([Arquivo] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[IX_Receituario_2]...';


GO
CREATE NONCLUSTERED INDEX [IX_Receituario_2]
    ON [dbo].[Receituario]([DataReceita] ASC);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cardapio_Dietas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Dietas] FOREIGN KEY ([IdDieta]) REFERENCES [dbo].[Dietas] ([IdDieta]) ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Receituario_Cardapio]...';


GO
ALTER TABLE [dbo].[Receituario] WITH NOCHECK
    ADD CONSTRAINT [FK_Receituario_Cardapio] FOREIGN KEY ([IdCardapio]) REFERENCES [dbo].[Cardapio] ([IdCardapio]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_CardapiosAlimentos_Cardapio]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_CardapiosAlimentos_Cardapio] FOREIGN KEY ([IdCardapio]) REFERENCES [dbo].[Cardapio] ([IdCardapio]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_ConfigReceituario_Pessoas]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] WITH NOCHECK
    ADD CONSTRAINT [FK_ConfigReceituario_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_ReceituarioNutrientes_Receituario]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_ReceituarioNutrientes_Receituario] FOREIGN KEY ([IdReceita]) REFERENCES [dbo].[Receituario] ([IdReceita]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Altering Procedure [dbo].[ReceituarioNutrientesImpressao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <16/11/2022>
-- Description:	<Listagem Geral de Nutrietnes da Receita>
-- =============================================
ALTER PROCEDURE [dbo].[ReceituarioNutrientesImpressao]
	@IdReceita as int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    SELECT	rn.IdReceita, r.dTpRec, 
			(Case r.dTpRec
				When 1 Then 'Suplementação' 
				When 2 Then 'Nutracêuticos'
				When 3 Then 'Receita_em_branco'
			 End) TipoReceita, r.Arquivo, r.NrReceita,
			r.Titulo, r.DataReceita, rn.IdNutrRec, rn.EmReceita,
			rn.IdNutr, n.Nutriente, rn.Consta, rn.Falta, 
			rn.DoseMin, rn.IdUnidMin,
			(CASE rn.IdUnidMin 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS UnidadeMin,
			rn.IdPrescrMin, pMin.Prescricao AS PrescricaoMin,
			rn.DoseMax, rn.IdUnidMax,
			(CASE rn.IdUnidMax 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS UnidadeMax,
			rn.IdPrescrMax, 
			PrescricaoAuxTipos.Prescricao AS PrescricaoMax,
			rn.Dose, rn.IdUnid,
			(CASE rn.IdUnid 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS Unidade,
			rn.IdPrescr, p.Prescricao, p.Intervalo,
			rn.PesoAtual, rn.Quantidade,
			rn.Ativo, rn.IdOperador, 
			rn.[IP], rn.DataCadastro
	FROM	Receituario AS r INNER JOIN
				ReceituarioNutrientes AS rn ON r.IdReceita = 
					rn.IdReceita INNER JOIN
				Nutrientes AS n ON rn.IdNutr = n.IdNutr LEFT OUTER JOIN
				PrescricaoAuxTipos ON rn.IdPrescrMax = 
					PrescricaoAuxTipos.IdPrescr LEFT OUTER JOIN
				PrescricaoAuxTipos AS pMin ON rn.IdPrescrMin = 
					pMin.IdPrescr LEFT OUTER JOIN
				PrescricaoAuxTipos AS p ON rn.IdPrescr = p.IdPrescr
	WHERE	(rn.IdReceita = @IdReceita) AND (rn.EmReceita = 1)
	ORDER BY n.Nutriente
END
GO
PRINT N'Altering Procedure [dbo].[ReceituarioNutrientesListar]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <29/10/2022>
-- Description:	<Listagem Geral de Nutrietnes da Receita>
-- =============================================
ALTER PROCEDURE [dbo].[ReceituarioNutrientesListar]
	@IdReceita as int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    SELECT	rn.IdReceita, r.dTpRec, 
			(Case r.dTpRec
				When 1 Then 'Suplementação' 
				When 2 Then 'Nutracêuticos'
				When 3 Then 'Receita_em_branco'
			 End) TipoReceita, r.Arquivo, r.NrReceita,
			r.Titulo, r.DataReceita, rn.IdNutrRec, rn.EmReceita,
			rn.IdNutr, n.Nutriente, rn.Consta, rn.Falta, 
			rn.DoseMin, rn.IdUnidMin,
			(CASE rn.IdUnidMin 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS UnidadeMin,
			rn.IdPrescrMin, pMin.Prescricao AS PrescricaoMin,
			rn.DoseMax, rn.IdUnidMax,
			(CASE rn.IdUnidMax 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS UnidadeMax,
			rn.IdPrescrMax, 
			PrescricaoAuxTipos.Prescricao AS PrescricaoMax,
			rn.Dose, rn.IdUnid,
			(CASE rn.IdUnid 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS Unidade,
			rn.IdPrescr, p.Prescricao, p.Intervalo,
			rn.PesoAtual, rn.Quantidade,
			rn.Ativo, rn.IdOperador, 
			rn.[IP], rn.DataCadastro
	FROM	Receituario AS r INNER JOIN
				ReceituarioNutrientes AS rn ON r.IdReceita = 
					rn.IdReceita INNER JOIN
				Nutrientes AS n ON rn.IdNutr = n.IdNutr LEFT OUTER JOIN
				PrescricaoAuxTipos ON rn.IdPrescrMax = 
					PrescricaoAuxTipos.IdPrescr LEFT OUTER JOIN
				PrescricaoAuxTipos AS pMin ON rn.IdPrescrMin = 
					pMin.IdPrescr LEFT OUTER JOIN
				PrescricaoAuxTipos AS p ON rn.IdPrescr = p.IdPrescr
	WHERE	(rn.IdReceita = @IdReceita)
	ORDER BY n.Nutriente

END
GO
PRINT N'Altering Procedure [dbo].[AlimentosNutrientesListar]...';


GO
-- =============================================
-- Author:		Lobo, Rafael Britto
-- Create date: 22/03/2021
-- Description:	Listagem geral de alimentos e nutrientes
-- =============================================
ALTER PROCEDURE [dbo].[AlimentosNutrientesListar] 
	@pesqAlimento	as NVarchar(250), 
	@idNutr			as Int,
	@idPessoa		as Int,
	@RowspPage		as Int,   /*Linhas por página*/
	@PageNumber		as Int,   /*Número da Página*/
	@gerencia		as bit
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@pesqAlimento	NVarchar(250), 
						 @idNutr		Int,
						 @idPessoa		Int,
						 @RowspPage		Int,  
						 @PageNumber	Int, 
						 @gerencia		bit';

	SET	@SQL = '';

	IF (@idNutr > 0)
		BEGIN
			SET @SQL += '
				SELECT  IdPessoa, Pessoa, IdFonte, Fonte, FontePesquisa, IdGrupo, GrupoAlimento, 
						GrupoAlimentoPesquisa, IdCateg, Categoria, CategoriaPesquisa, IdAlimento, 
						Alimento, AlimentoFonte, AlimentoTexto, AlimentoPesquisa, IdNutr, Nutriente,
						Valor, Unidade, NDB_No, Compartilhar, fHomologado, Ativo, IdOperador, 
						IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY an.Valor DESC) AS NUMBER,
									a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, f.Fonte,
									(f.Fonte COLLATE SQL_Latin1_General_CP1251_CI_AS) FontePesquisa,
									a.IdGrupo, g.GrupoAlimento, 
									(g.GrupoAlimento COLLATE SQL_Latin1_General_CP1251_CI_AS) GrupoAlimentoPesquisa,
									a.IdCateg, c.Categoria,
									(c.Categoria COLLATE SQL_Latin1_General_CP1251_CI_AS) CategoriaPesquisa,
									a.IdAlimento, a.Alimento, 
									(a.Alimento + ' + ''' - ''' + ' + f.Fonte) AlimentoFonte,
									((REPLACE(REPLACE(Alimento, ' + ''',''' + ', ' + '''''' + '), ' + '''-''' + ', ' + ''' ''' + '))) AS AlimentoTexto,
									(a.Alimento COLLATE SQL_Latin1_General_CP1251_CI_AS) AlimentoPesquisa,
									n.IdNutr, n.Nutriente, an.Valor,
									(Case an.IdUnidade
										WHEN 1 THEN ' + '''µg''' + '
										WHEN 2 THEN ' + '''g''' + '
										WHEN 3 THEN ' + '''mg''' + '
										WHEN 4 THEN ' + '''Kcal''' + '
										WHEN 5 THEN ' + '''UI''' + '
										WHEN 6 THEN ' + '''Proporção''' + '
										WHEN 7 THEN ' + '''µg/Kg''' + '
										WHEN 8 THEN ' + '''mg/animal''' + '
										WHEN 9 THEN ' + '''mg/Kg''' + '
										WHEN 10 THEN ' + '''UI/Kg''' + '
									End) Unidade, a.NDB_No, a.Compartilhar, a.fHomologado, 
									a.Ativo, a.IdOperador, a.IP, a.DataCadastro
							FROM	Nutrientes AS n INNER JOIN
									AlimentoNutrientes AS an ON n.IdNutr = 
										an.IdNutr RIGHT OUTER JOIN
									Alimentos AS a ON an.IdAlimento = 
										a.IdAlimento LEFT OUTER JOIN
									AlimentosAuxFontes AS f ON a.IdFonte = 
										f.IdFonte LEFT OUTER JOIN
									AlimentosAuxGrupos AS g ON a.IdGrupo = 
										g.IdGrupo LEFT OUTER JOIN
									AlimentosAuxCategorias AS c ON a.IdCateg = 
										c.IdCateg LEFT OUTER JOIN
									Pessoas ON a.IdPessoa = Pessoas.IdPessoa ';
		END
	ELSE
		BEGIN
			SET @SQL += '
				SELECT  IdPessoa, Pessoa, IdFonte, Fonte, FontePesquisa, IdGrupo, GrupoAlimento, 
						GrupoAlimentoPesquisa, IdCateg, Categoria, CategoriaPesquisa, IdAlimento, 
						Alimento, AlimentoFonte, AlimentoTexto, AlimentoPesquisa, NDB_No, 
						Compartilhar, fHomologado, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY a.Alimento) AS NUMBER,
									a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, 
									f.Fonte, 
									(f.Fonte COLLATE SQL_Latin1_General_CP1251_CI_AS) FontePesquisa, 
									a.IdGrupo, g.GrupoAlimento, 
									(g.GrupoAlimento COLLATE SQL_Latin1_General_CP1251_CI_AS) GrupoAlimentoPesquisa,
									a.IdCateg, c.Categoria,
									(c.Categoria COLLATE SQL_Latin1_General_CP1251_CI_AS) CategoriaPesquisa, 
									a.IdAlimento, a.Alimento, 
									(a.Alimento + ' + ''' - ''' + ' + f.Fonte) AlimentoFonte,
									((REPLACE(REPLACE(Alimento, ' + ''',''' + ', ' + '''''' + '), ' + '''-''' + ', ' + ''' ''' + '))) AS AlimentoTexto,
									(a.Alimento COLLATE SQL_Latin1_General_CP1251_CI_AS) AlimentoPesquisa,
									a.NDB_No, a.Compartilhar, 
									a.fHomologado, a.Ativo, a.IdOperador, a.IP, 
									a.DataCadastro 
							FROM	Alimentos AS a LEFT OUTER JOIN
									AlimentosAuxFontes AS f ON a.IdFonte = 
										f.IdFonte LEFT OUTER JOIN
									AlimentosAuxGrupos AS g ON a.IdGrupo = 
										g.IdGrupo LEFT OUTER JOIN
									AlimentosAuxCategorias AS c ON a.IdCateg = 
										c.IdCateg LEFT OUTER JOIN
									Pessoas ON a.IdPessoa = Pessoas.IdPessoa';
		END

	SET @SQL +=  '
							WHERE	';

	IF (@gerencia = 1)
		BEGIN
			IF ((@pesqAlimento <> '') AND (@pesqAlimento IS NOT NULL))
				BEGIN
					IF (@idNutr > 0)
						BEGIN
							SET @SQL += ' 
									((REPLACE(REPLACE(Alimento, ' + ''',''' + ', ' + '''''' + '), ' + '''-''' + ', ' + ''' ''' + ') Like ' + 
										'''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR 
									 (g.GrupoAlimento Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI)) AND
									 (n.IdNutr = @idNutr) AND (an.Valor > 0) AND ';
						END
					ELSE
						BEGIN
							SET @SQL += ' 
									((REPLACE(REPLACE(Alimento, ' + ''',''' + ', ' + '''''' + '), ' + '''-''' + ', ' + ''' ''' + ') Like ' + 
										'''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR 
									 (g.GrupoAlimento Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI)) AND ';
						END
				END
				ELSE IF (@idNutr > 0)
					BEGIN
						SET @SQL += '
									(n.IdNutr = @idNutr) AND (an.Valor > 0) AND ';
					END

				SET @SQL += '
									(a.fHomologado = 0)
						) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)';
		END
	ELSE
		BEGIN
			SET @SQL += '(a.Ativo = 1) ';

			IF (@idPessoa > 0)
				BEGIN
					SET @SQL += ' AND
									(((a.Compartilhar = 1) And (a.fHomologado = 1)) OR 
									 ((a.fHomologado = 0) AND (a.IdPessoa = @idPessoa))) ';
				END
			else
				BEGIN
					SET @SQL += ' AND (a.Compartilhar = 1) AND (a.fHomologado = 1) ';
				END

			IF ((@pesqAlimento <> '') AND (@pesqAlimento IS NOT NULL))
				BEGIN
					IF (@idNutr > 0)
						BEGIN
							SET @SQL += 'AND 
									((REPLACE(REPLACE(Alimento, ' + ''',''' + ', ' + '''''' + '), ' + '''-''' + ', ' + ''' ''' + ') Like ' + 
										'''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR 
									 (g.GrupoAlimento Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI)) AND
									 (n.IdNutr = @idNutr) AND (an.Valor > 0) ';
						END
					ELSE
						BEGIN
							SET @SQL += 'AND 
									((REPLACE(REPLACE(Alimento, ' + ''',''' + ', ' + '''''' + '), ' + '''-''' + ', ' + ''' ''' + ') Like ' + 
										'''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR 
									 (g.GrupoAlimento Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte Like ' + '''%' + @pesqAlimento + '%''' + ' COLLATE Latin1_General_CI_AI)) ';
						END
				END
			ELSE IF (@idNutr > 0)
				BEGIN
					SET @SQL += 'AND
									(n.IdNutr = @idNutr) AND (an.Valor > 0)';
				END

			SET @SQL += '  ) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)';
		END


	--PRINT @SQL
	EXEC sp_Executesql	@SQL, @ParameterDef, @pesqAlimento	= @pesqAlimento, @idNutr = @idNutr,
						@idPessoa = @idPessoa, @RowspPage = @RowspPage, @PageNumber = @PageNumber, 
						@gerencia = @gerencia
END
GO
PRINT N'Altering Procedure [dbo].[ListaGeralNutrientes]...';


GO
-- =============================================
-- Author:		Lobo, Rafael Britto
-- Create date: 22/03/2021
-- Description:	Listagem geral de alimentos
-- =============================================
ALTER PROCEDURE [dbo].[ListaGeralNutrientes] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, n.Referencia, 
			n.ValMin, n.ValMax, n.IdUnidade, 
			(Case n.IdUnidade
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI'
				WHEN 6 THEN 'Proporção'
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal'
				WHEN 9 THEN 'mg/Kg'
				WHEN 10 THEN 'UI/Kg'
			End) Unidade,
			n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
			n.IdOperador, n.IP, n.DataCadastro
	FROM	Nutrientes AS n INNER JOIN
				NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
END
GO
PRINT N'Altering Procedure [dbo].[ListarCardapioExigNutr]...';


GO

ALTER PROCEDURE [dbo].[ListarCardapioExigNutr]
	@energia as decimal(18,3), 
	@idTabNutr as int,
	@idEspecie as int,
	@idIndic as int,
	@idGrupo as int
AS
BEGIN
	SET NOCOUNT ON;
    SET QUERY_GOVERNOR_COST_LIMIT 0;

    SELECT	p.IdGrupo, p.Grupo,
			p.IdNutr, (p.Nutriente + ' (' + p.Unidade + ')') Nutriente, 
            p.IdUnid, p.Unidade,
			p.[Mínimo] AS Minimo, p.[Máximo] As Maximo, p.Adequado, 
            p.Recomendado
    From	(
			    SELECT	n.IdGrupo, NutrientesAuxGrupos.Grupo, 
						en.IdNutr1 As IdNutr, n.Nutriente, 
					    (CASE en.IdTpValor 
						    WHEN 1 THEN 'Mínimo' 
						    WHEN 2 THEN 'Máximo' 
						    WHEN 3 THEN 'Adequado' 
						    WHEN 4 THEN 'Recomendado' 
					        END) AS TpValor, 
					        ((@energia * en.Valor1) / 1000) AS ValCalc,
						n.IdUnidade As IdUnid,
		                (Case n.IdUnidade
			                WHEN 1 THEN 'µg' 
			                WHEN 2 THEN 'g' 
			                WHEN 3 THEN 'mg' 
			                WHEN 4 THEN 'Kcal' 
			                WHEN 5 THEN 'UI'
			                WHEN 6 THEN 'Proporção'
			                WHEN 7 THEN 'µg/Kg' 
			                WHEN 8 THEN 'mg/animal'
			                WHEN 9 THEN 'mg/Kg'
			                WHEN 10 THEN 'UI/Kg'
		                    End) Unidade
			    FROM		ExigenciasNutricionais AS en INNER JOIN
							Nutrientes AS n ON en.IdNutr1 = n.IdNutr INNER JOIN
							ExigenciasNutrAuxIndicacoes AS indic ON 
								en.IdIndic = indic.IdIndic LEFT OUTER JOIN
							NutrientesAuxGrupos ON n.IdGrupo = 
								NutrientesAuxGrupos.IdGrupo
			    WHERE	(en.Ativo = 1) AND (n.ListarEmAlim = 1) AND
					    (en.IdTabNutr = @idTabNutr) AND 
					    (en.IdEspecie = @idEspecie) AND
					    (en.IdIndic = @idIndic) AND
					    (en.IdUnidade1 <> 6) AND
                        (n.IdGrupo = @idGrupo) AND 
						(n.ListarEmAlim = 1) AND 
						(n.Ativo = 1)
		    ) As b
    PIVOT (
		    Sum(b.ValCalc) For b.TpValor in ([Mínimo], [Máximo], 
			    Adequado, Recomendado)
    ) AS P
    Order By P.Nutriente;
END
GO
PRINT N'Altering Procedure [dbo].[NutraceuticosDietasListar]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <03/02/2023>
-- Description:	<Listagem de Nutracêuticos x Dietas>
-- =============================================
ALTER PROCEDURE [dbo].[NutraceuticosDietasListar]
	@idEspecie	as Int,
	@idDieta	as Int,
	@pesoAtual	as Decimal(10, 3)
AS
BEGIN
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;
	SET NOCOUNT ON;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '
		@idEspecie		Int,
		@idDieta		Int,
		@pesoAtual		Decimal(10, 3)';

	SET @SQL = '    
		SELECT	Coalesce(
					Cast(
							(Select	1
							 From	NutraceuticosDietas nd
							 Where	(nd.IdNutrac = nutra.IdNutrac) And
									(nd.IdDieta = @idDieta)
						) as bit), 0
				) EmReceita,
			nutra.IdEspecie, e.Especie, nutra.IdNutr, n.Nutriente, 
			nutra.IdNutrac, nutra.DoseMin, nutra.IdUnidMin, 
			(CASE	nutra.IdUnidMin 
				WHEN 1 THEN ' + '''µg''' + '
				WHEN 2 THEN ' + '''g''' + '
				WHEN 3 THEN ' + '''mg''' + '
				WHEN 4 THEN ' + '''Kcal''' + '
				WHEN 5 THEN ' + '''UI''' + '
				WHEN 6 THEN ' + '''Proporção''' + '
				WHEN 7 THEN ' + '''µg/Kg''' + '
				WHEN 8 THEN ' + '''mg/animal''' + '
				WHEN 9 THEN ' + '''mg/Kg''' + '
				WHEN 10 THEN ' + '''UI/Kg''' + ' END
			) AS UnidadeMin, nutra.DoseMax, nutra.IdUnidMax, 
			(CASE	nutra.IdUnidMax 
				WHEN 1 THEN ' + '''µg''' + '
				WHEN 2 THEN ' + '''g''' + '
				WHEN 3 THEN ' + '''mg''' + '
				WHEN 4 THEN ' + '''Kcal''' + '
				WHEN 5 THEN ' + '''UI''' + '
				WHEN 6 THEN ' + '''Proporção''' + '
				WHEN 7 THEN ' + '''µg/Kg''' + '
				WHEN 8 THEN ' + '''mg/animal''' + '
				WHEN 9 THEN ' + '''mg/Kg''' + '
				WHEN 10 THEN ' + '''UI/Kg''' + ' END
			) AS UnidadeMax, nutra.IdPrescr1, p1.Prescricao AS Prescricao1, 
			nutra.IdPrescr2, p2.Prescricao AS Prescricao2, @pesoAtual As PesoAtual
		FROM Nutrientes AS n INNER JOIN
				Nutraceuticos AS nutra ON n.IdNutr = nutra.IdNutr INNER JOIN
				AnimaisAuxEspecies AS e ON nutra.IdEspecie = e.IdEspecie LEFT OUTER JOIN
				PrescricaoAuxTipos AS p2 ON nutra.IdPrescr2 = p2.IdPrescr LEFT OUTER JOIN
				PrescricaoAuxTipos AS p1 ON nutra.IdPrescr1 = p1.IdPrescr
		WHERE	(nutra.Ativo = 1)';

	If ((@idEspecie is not null) AND (@idEspecie > 0))
	Begin
		SET @SQL += ' AND 
				(nutra.IdEspecie = @idEspecie) ';
	End

	SET @SQL = @SQL + '
		ORDER BY	n.Nutriente';

	--PRINT @SQL
	EXEC sp_Executesql	@SQL, @ParameterDef, @idEspecie = @idEspecie, 
		@idDieta =@idDieta, @pesoAtual = @pesoAtual
END
GO
PRINT N'Altering Procedure [dbo].[NutraceuticosListar]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <01/11/2021>
-- Description:	<Listagem de Nutracêuticos>
-- =============================================
ALTER PROCEDURE [dbo].[NutraceuticosListar] 
	AS
BEGIN
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;
	SET NOCOUNT ON;

    SELECT	nutra.IdNutrac, nutra.IdEspecie, e.Especie, n.IdGrupo, 
			ng.Grupo, nutra.IdNutr, n.Nutriente, nutra.DoseMin, 
			nutra.IdUnidMin, 
			(CASE nutra.IdUnidMin 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' END) AS UnidadeMin, 
			nutra.DoseMax, nutra.IdUnidMax, 
			(CASE nutra.IdUnidMax 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'µg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' END) AS UnidadeMax, 
			nutra.IdPrescr1, p1.Prescricao AS TpPrescr1, 
			nutra.IdPrescr2, p2.Prescricao AS TpPrescr2, nutra.Obs, 
			nutra.Ativo, nutra.IdOperador, nutra.IP, 
			nutra.DataCadastro
	FROM    Nutraceuticos AS nutra INNER JOIN
				AnimaisAuxEspecies AS e ON nutra.IdEspecie = 
					e.IdEspecie INNER JOIN
				Nutrientes AS n ON nutra.IdNutr = 
					n.IdNutr INNER JOIN
				NutrientesAuxGrupos AS ng ON n.IdGrupo = 
					ng.IdGrupo LEFT OUTER JOIN
				PrescricaoAuxTipos AS p1 ON nutra.IdPrescr1 = 
					p1.IdPrescr LEFT OUTER JOIN
				PrescricaoAuxTipos AS p2 ON nutra.IdPrescr2 = 
					p2.IdPrescr
	WHERE	(nutra.Ativo = 1)
	ORDER BY e.Especie, n.Nutriente
END
GO
PRINT N'Altering Procedure [dbo].[ReceituarioListarSelecao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <24/10/2022>
-- Description:	<Listagem de Receitas por Tutor>
-- =============================================
ALTER PROCEDURE [dbo].[ReceituarioListarSelecao] 
	@idPessoa	as Int,
	@idTutor	as Int,
	@idTpRec	as Int,
	@idAnimal	as Int,
	@dtIni		as Varchar(20),
	@dtFim		as VArchar(20),
	@RowspPage	as Int,   /*Linhas por página*/
	@PageNumber	as Int   /*Número da Página*/
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@idPessoa		Int,
						 @idTutor		Int,
						 @idAnimal		Int,
						 @idTpRec		Int,
						 @dtIni			Varchar(20),
						 @dtFim			VArchar(20),
						 @RowspPage		Int,
						 @PageNumber	Int';

	SET @SQL = '
				SELECT  IdPessoa, IdTutor, Tutor, IdAnimal, Animal, Especie, Raca, 
						IdCardapio, Descricao, DtCardapio, PesoAtual, PesoIdeal, 
						IdReceita, Arquivo, NrReceita, dTpRec, TipoReceita, 
						InstrucoesReceita, Titulo, Veiculo, QuantVeic, Posologia, 
						VeiculoSid, QuantVeicSid, PosologiaSid, VeiculoTid, 
						QuantVeicTid, PosologiaTid, Prescricao, DataReceita, 
						LocalReceita, Ativo, IdOperador, [IP], DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY rec.DataReceita Desc) AS NUMBER,
									c.IdPessoa, t.IdTutor, p.Nome AS Tutor, c.IdAnimal, a.Nome AS Animal, 
									e.Especie, r.Raca, c.IdCardapio, c.Descricao, c.DtCardapio, a.PesoAtual, 
									a.PesoIdeal, rec.IdReceita, rec.Arquivo, rec.NrReceita, rec.dTpRec, 
									(Case rec.dTpRec
										When 1 Then ' + '''Suplementação''' + ' 
										When 2 Then ' + '''Nutracêuticos''' + '
										When 3 Then ' + '''Receita em branco''' + '
									 End) TipoReceita, rec.InstrucoesReceita,
									rec.Titulo, rec.Veiculo, rec.QuantVeic, rec.Posologia, rec.VeiculoSid, 
									rec.QuantVeicSid, rec.PosologiaSid, rec.VeiculoTid, rec.QuantVeicTid, 
									rec.PosologiaTid, rec.Prescricao, rec.DataReceita, rec.LocalReceita, 
									rec.Ativo, rec.IdOperador, rec.IP, rec.DataCadastro
							FROM	Tutores AS t INNER JOIN
										Animais AS a ON t.IdTutores = a.IdTutores INNER JOIN
										Pessoas AS p ON t.IdTutor = p.IdPessoa INNER JOIN
										Cardapio AS c ON a.IdAnimal = c.IdAnimal INNER JOIN
										Receituario AS rec ON c.IdCardapio = rec.IdCardapio LEFT OUTER JOIN
										AnimaisAuxEspecies AS e INNER JOIN
										AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON 
											a.IdRaca = r.IdRaca
							WHERE	(rec.Ativo = 1)';

	If ((@idPessoa is not null) AND (@idPessoa > 0))
	Begin
		SET @SQL += ' AND 
									(c.IdPessoa = @idPessoa) ';
	End

	If ((@idTutor is not null) AND (@idTutor > 0))
	Begin
		SET @SQL += ' AND 
									(t.IdTutor = @idTutor) ';
	End

	If ((@idAnimal is not null) AND (@idAnimal > 0))
	Begin
		SET @SQL += ' AND 
									(c.IdAnimal = @idAnimal) ';
	End

	If ((@idTpRec is not null) AND (@idTpRec > 0))
	Begin
		SET @SQL += ' AND 
									(rec.dTpRec = @idTpRec) ';
	End

	If ((@dtIni is not null) AND (@dtIni <> '')) 
		Begin
			If ((@dtFim is not null) AND (@dtFim <> ''))
				Begin
					SET @SQL += ' AND 
									(convert(date, rec.DataReceita, 103) BETWEEN ''' + 
									 @dtIni + ''' AND ''' + @dtFim + ''') ';
				End
			Else
				Begin
					SET @SQL += ' AND 
									(convert(date, rec.DataReceita, 103) = ''' + 
									 @dtIni + ''') ';
				End
		End

	SET @SQL = @SQL + '
						) AS TBL
			WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
					(@PageNumber * @RowspPage)';
	
	--PRINT @SQL
	EXEC sp_Executesql	@SQL, @ParameterDef, @idPessoa = @idPessoa, 
		@idTutor = @idTutor, @idAnimal = @idAnimal, @idTpRec = @idTpRec, 
		@dtIni = @dtIni, @dtFim = @dtFim, @RowspPage = @RowspPage, 
		@PageNumber = @PageNumber
END
GO
PRINT N'Creating Procedure [dbo].[TotalIntervalos]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <2-/03/2023>
-- Description:	<Totalização de Intervalos>
-- =============================================
CREATE PROCEDURE TotalIntervalos
	@idReceita as Int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    SELECT		p.IdPrescr, COUNT(rn.IdPrescr) AS Total
	FROM		ReceituarioNutrientes AS rn INNER JOIN
					PrescricaoAuxTipos AS p ON rn.IdPrescr = p.IdPrescr
	WHERE		(rn.IdReceita = @idReceita) AND (rn.EmReceita = 1)
	GROUP BY	p.IdPrescr, rn.IdPrescr 
END
GO
PRINT N'Refreshing Procedure [dbo].[ModificaTutorParaCliente]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ModificaTutorParaCliente]';


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Pessoas];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Animais];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Dietas];

ALTER TABLE [dbo].[Receituario] WITH CHECK CHECK CONSTRAINT [FK_Receituario_Cardapio];

ALTER TABLE [dbo].[CardapiosAlimentos] WITH CHECK CHECK CONSTRAINT [FK_CardapiosAlimentos_Cardapio];

ALTER TABLE [dbo].[ConfigReceituario] WITH CHECK CHECK CONSTRAINT [FK_ConfigReceituario_Pessoas];

ALTER TABLE [dbo].[ReceituarioNutrientes] WITH CHECK CHECK CONSTRAINT [FK_ReceituarioNutrientes_Receituario];


GO
PRINT N'Update complete.';


GO
