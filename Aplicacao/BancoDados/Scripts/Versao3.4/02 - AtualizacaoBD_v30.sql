
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, 
	QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping Foreign Key [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [FK_Animais_Tutores];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Animais];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_CardapiosAlimentos_Cardapio]...';


GO
ALTER TABLE [dbo].[CardapiosAlimentos] DROP CONSTRAINT [FK_CardapiosAlimentos_Cardapio];


GO
PRINT N'Creating Table [dbo].[ConfigReceituario]...';


GO
CREATE TABLE [dbo].[ConfigReceituario] (
    [IdPessoa]     INT           NOT NULL,
    [NomeClinica]  VARCHAR (300) NULL,
    [Slogan]       VARCHAR (MAX) NULL,
    [Site]         VARCHAR (300) NULL,
    [Facebook]     VARCHAR (300) NULL,
    [Twitter]      VARCHAR (300) NULL,
    [Instagram]    VARCHAR (300) NULL,
    [Logotipo]     VARCHAR (250) NULL,
    [Assinatura]   VARCHAR (250) NULL,
    [CRMV]         VARCHAR (50)  NULL,
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
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_ConfigReceituario] PRIMARY KEY CLUSTERED ([IdPessoa] ASC)
);


GO
PRINT N'Creating Table [dbo].[LogsSistema]...';


GO
CREATE TABLE [dbo].[LogsSistema] (
    [IdPessoa]      INT           NOT NULL,
    [IdLog]         INT           IDENTITY (1, 1) NOT NULL,
    [IdTabela]      INT           NULL,
    [IdAcao]        INT           NULL,
    [Mensagem]      VARCHAR (MAX) NOT NULL,
    [Justificativa] VARCHAR (MAX) NULL,
    [Datahora]      DATETIME      NULL,
    [Versao]        ROWVERSION    NULL,
    CONSTRAINT [PK_LogsSistema] PRIMARY KEY CLUSTERED ([IdLog] ASC)
);


GO
PRINT N'Creating Default Constraint [dbo].[DF_ConfigReceituario_Ativo]...';


GO
ALTER TABLE [dbo].[ConfigReceituario]
    ADD CONSTRAINT [DF_ConfigReceituario_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating Default Constraint [dbo].[DF_ConfigReceituario_DataCadastro]...';


GO
ALTER TABLE [dbo].[ConfigReceituario]
    ADD CONSTRAINT [DF_ConfigReceituario_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating Default Constraint [dbo].[DF_LogsSistema_Datahora]...';


GO
ALTER TABLE [dbo].[LogsSistema]
    ADD CONSTRAINT [DF_LogsSistema_Datahora] DEFAULT (getdate()) FOR [Datahora];


GO
PRINT N'Creating Foreign Key [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_Tutores] FOREIGN KEY ([IdTutores]) REFERENCES [dbo].[Tutores] ([IdTutores]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON DELETE CASCADE ON UPDATE CASCADE;


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
PRINT N'Creating Foreign Key [dbo].[FK_LogsSistema_Pessoas]...';


GO
ALTER TABLE [dbo].[LogsSistema] WITH NOCHECK
    ADD CONSTRAINT [FK_LogsSistema_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Procedure [dbo].[LogsListarSelecao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <08/09/2022>
-- Description:	<Listagem de Logs>
-- =============================================
CREATE PROCEDURE [dbo].[LogsListarSelecao] 
	@pesquisaTabelaAcao as Varchar(250), 
	@dataInicial as varchar(20),
	@dataFinal as varchar(20),
	@RowspPage as Int,   /*Linhas por página*/
	@PageNumber as Int   /*Número da Página*/
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@pesquisaTabelaAcao	as	Varchar(250),
						 @dataInicial			as varchar(20),
						 @dataFinal				as varchar(20),
						 @RowspPage				Int,
						 @PageNumber			Int';

	SET @SQL = 'SELECT	Number, IdPessoa, IdLog, IdTabela, Tabela, IdAcao, Acao, Mensagem, 
						Justificativa, Datahora
				FROM	(
						SELECT	ROW_NUMBER() OVER(ORDER BY t.IdLog Desc) AS Number, t.IdPessoa, 
								t.IdLog, t.IdTabela, t.Tabela, t.IdAcao, t.Acao, t.Mensagem, 
								t.Justificativa, t.Datahora
						FROM   (
								Select	l.IdPessoa, l.IdLog, l.IdTabela,
										(Case l.IdTabela
											When 1 Then ' + '''Acessos''' + '			
											When 2 Then ' + '''AcessosAuxFuncoes''' + '
											When 3 Then ' + '''AcessosAuxTelas''' + '
											When 4 Then ' + '''AcessosFuncoesTelas''' + '
											When 5 Then ' + '''AcessosVigenciaCupomDesconto''' + '
											When 6 Then ' + '''AcessosVigenciaPlanos''' + '
											When 7 Then ' + '''AcessosVigenciaSituacao''' + '
											When 8 Then ' + '''AlimentoNutrientes''' + '
											When 9 Then ' + '''Alimentos''' + '
											When 10 Then ' + '''AlimentosAuxCategorias''' + '
											When 11 Then ' + '''AlimentosAuxFontes''' + '
											When 12 Then ' + '''AlimentosAuxGrupos''' + '
											When 13 Then ' + '''Animais''' + '
											When 14 Then ' + '''AnimaisAuxEspecies''' + '
											When 15 Then ' + '''AnimaisAuxRacas''' + '
											When 16 Then ' + '''AnimaisPesoHistorico''' + '
											When 17 Then ' + '''Biblioteca''' + '
											When 18 Then ' + '''BibliotecaAuxSecoes''' + '
											When 19 Then ' + '''Cardapio''' + '
											When 20 Then ' + '''CardapiosAlimentos''' + '
											When 21 Then ' + '''ConfigReceituario''' + '
											When 22 Then ' + '''Dietas''' + '
											When 23 Then ' + '''DietasAlimentos''' + '
											When 24 Then ' + '''ExigenciasNutrAuxIndicacoes''' + '
											When 25 Then ' + '''ExigenciasNutricionais''' + '
											When 26 Then ' + '''Nutraceuticos''' + '
											When 27 Then ' + '''Nutrientes''' + '
											When 28 Then ' + '''NutrientesAuxGrupos''' + '
											When 29 Then ' + '''Pessoas''' + '
											When 30 Then ' + '''PessoasCartaoCredito''' + '
											When 31 Then ' + '''PessoasDocumentos''' + '
											When 32 Then ' + '''PlanosAssinaturas''' + '
											When 33 Then ' + '''PortalContato''' + '
											When 34 Then ' + '''PrescricaoAuxTipos''' + '
											When 35 Then ' + '''Tutores''' + '
										 End) Tabela, l.IdAcao,
										 (Case l.IdAcao
											When 1 Then ' + '''Inserir''' + '			
											When 2 Then ' + '''Alterar''' + '
											When 3 Then ' + '''Excluir''' + '
											When 4 Then ' + '''Carregar''' + '
											When 5 Then ' + '''Consultar''' + '
											When 6 Then ' + '''Listar''' + '
											When 7 Then ' + '''Gerar''' + '
											When 8 Then ' + '''Gerar Relatorio''' + '
											When 9 Then ' + '''Efetuar Logon''' + '
										 End) Acao, l.Mensagem, l.Justificativa, l.Datahora
								From	LogsSistema l
							  ) t
						) logs
				WHERE	NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)';

	IF ((@pesquisaTabelaAcao <> '') AND (@pesquisaTabelaAcao IS NOT NULL))
		BEGIN
			 SET @SQL = @SQL + ' AND 
						((logs.Tabela Like ' + '''%' + @pesquisaTabelaAcao + '%''' + ' COLLATE Latin1_General_CI_AI) OR
						 (logs.Acao Like ' + '''%' + @pesquisaTabelaAcao + '%''' + ' COLLATE Latin1_General_CI_AI))';
		END
	
	IF ((@dataInicial <> '') AND (@dataInicial IS NOT NULL))
		BEGIN
			 IF ((@dataFinal <> '') AND (@dataFinal IS NOT NULL))
				BEGIN
			 		SET @SQL = @SQL + ' AND 
						(logs.Datahora BETWEEN ''' + @dataInicial + ''' AND ''' + @dataFinal + ''')';
				END
			ELSE 
				BEGIN
					 SET @SQL = @SQL + ' AND 
						(logs.Datahora = ''' + @dataInicial + ''')';
				END
		END
	
	--PRINT @SQL
	EXEC sp_Executesql @SQL, @ParameterDef, @pesquisaTabelaAcao = @pesquisaTabelaAcao,
			@dataInicial = @dataInicial, @dataFinal = @dataFinal, @RowspPage = @RowspPage, 
			@PageNumber = @PageNumber
END
GO
PRINT N'Creating Procedure [dbo].[LogsListarSelecaoTotalPaginas]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <05/09/2022>
-- Description:	<Total de Páginas de Logs>
-- =============================================
CREATE PROCEDURE [dbo].[LogsListarSelecaoTotalPaginas]
	@pesquisaTabelaAcao as Varchar(250), 
	@dataInicial as varchar(20),
	@dataFinal as varchar(20)
AS
BEGIN
SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@pesquisaTabelaAcao	as	Varchar(250),
						 @dataInicial			as varchar(20),
						 @dataFinal				as varchar(20)';

	SET @SQL = 'SELECT	Count(IdLog) Total
				FROM	(
						SELECT	t.IdPessoa, t.IdLog, t.IdTabela, t.Tabela, t.IdAcao, t.Acao, 
								t.Mensagem, t.Datahora
						FROM   (
								Select	l.IdPessoa, l.IdLog, l.IdTabela,
										(Case l.IdTabela
											When 1 Then ' + '''Acessos''' + '			
											When 2 Then ' + '''AcessosAuxFuncoes''' + '
											When 3 Then ' + '''AcessosAuxTelas''' + '
											When 4 Then ' + '''AcessosFuncoesTelas''' + '
											When 5 Then ' + '''AcessosVigenciaCupomDesconto''' + '
											When 6 Then ' + '''AcessosVigenciaPlanos''' + '
											When 7 Then ' + '''AcessosVigenciaSituacao''' + '
											When 8 Then ' + '''AlimentoNutrientes''' + '
											When 9 Then ' + '''Alimentos''' + '
											When 10 Then ' + '''AlimentosAuxCategorias''' + '
											When 11 Then ' + '''AlimentosAuxFontes''' + '
											When 12 Then ' + '''AlimentosAuxGrupos''' + '
											When 13 Then ' + '''Animais''' + '
											When 14 Then ' + '''AnimaisAuxEspecies''' + '
											When 15 Then ' + '''AnimaisAuxRacas''' + '
											When 16 Then ' + '''AnimaisPesoHistorico''' + '
											When 17 Then ' + '''Biblioteca''' + '
											When 18 Then ' + '''BibliotecaAuxSecoes''' + '
											When 19 Then ' + '''Cardapio''' + '
											When 20 Then ' + '''CardapiosAlimentos''' + '
											When 21 Then ' + '''ConfigReceituario''' + '
											When 22 Then ' + '''Dietas''' + '
											When 23 Then ' + '''DietasAlimentos''' + '
											When 24 Then ' + '''ExigenciasNutrAuxIndicacoes''' + '
											When 25 Then ' + '''ExigenciasNutricionais''' + '
											When 26 Then ' + '''Nutraceuticos''' + '
											When 27 Then ' + '''Nutrientes''' + '
											When 28 Then ' + '''NutrientesAuxGrupos''' + '
											When 29 Then ' + '''Pessoas''' + '
											When 30 Then ' + '''PessoasCartaoCredito''' + '
											When 31 Then ' + '''PessoasDocumentos''' + '
											When 32 Then ' + '''PlanosAssinaturas''' + '
											When 33 Then ' + '''PortalContato''' + '
											When 34 Then ' + '''PrescricaoAuxTipos''' + '
											When 35 Then ' + '''Tutores''' + '
										 End) Tabela, l.IdAcao,
										 (Case l.IdAcao
											When 1 Then ' + '''Inserir''' + '			
											When 2 Then ' + '''Alterar''' + '
											When 3 Then ' + '''Excluir''' + '
											When 4 Then ' + '''Carregar''' + '
											When 5 Then ' + '''Consultar''' + '
											When 6 Then ' + '''Listar''' + '
											When 7 Then ' + '''Gerar''' + '
											When 8 Then ' + '''Gerar Relatorio''' + '
											When 9 Then ' + '''Efetuar Logon''' + '
										 End) Acao, l.Mensagem, l.Datahora
								From	LogsSistema l
							  ) t
						) logs
				WHERE	(logs.IdLog > 0)';

	IF ((@pesquisaTabelaAcao <> '') AND (@pesquisaTabelaAcao IS NOT NULL))
		BEGIN
			 SET @SQL = @SQL + ' AND 
						((logs.Tabela Like ' + '''%' + @pesquisaTabelaAcao + '%''' + ' COLLATE Latin1_General_CI_AI) OR
						 (logs.Acao Like ' + '''%' + @pesquisaTabelaAcao + '%''' + ' COLLATE Latin1_General_CI_AI))';
		END
	
	IF ((@dataInicial <> '') AND (@dataInicial IS NOT NULL))
		BEGIN
			 IF ((@dataFinal <> '') AND (@dataFinal IS NOT NULL))
				BEGIN
			 		SET @SQL = @SQL + ' AND 
						(logs.Datahora BETWEEN ''' + @dataInicial + ''' AND ''' + @dataFinal + ''')';
				END
			ELSE 
				BEGIN
					 SET @SQL = @SQL + ' AND 
						(logs.Datahora = ''' + @dataInicial + ''')';
				END
		END
	
	--PRINT @SQL
	EXEC sp_Executesql @SQL, @ParameterDef, @pesquisaTabelaAcao = @pesquisaTabelaAcao,
			@dataInicial = @dataInicial, @dataFinal = @dataFinal
END
GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_Tutores];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Animais];

ALTER TABLE [dbo].[CardapiosAlimentos] WITH CHECK CHECK CONSTRAINT [FK_CardapiosAlimentos_Cardapio];

ALTER TABLE [dbo].[ConfigReceituario] WITH CHECK CHECK CONSTRAINT [FK_ConfigReceituario_Pessoas];

ALTER TABLE [dbo].[LogsSistema] WITH CHECK CHECK CONSTRAINT [FK_LogsSistema_Pessoas];


GO
PRINT N'Update complete.';


GO
