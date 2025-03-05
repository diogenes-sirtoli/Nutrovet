SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping Default Constraint [dbo].[DF_Animais_Ativo]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [DF_Animais_Ativo];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_Animais_DataCadastro]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [DF_Animais_DataCadastro];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_Animais_RecalcularNEM]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [DF_Animais_RecalcularNEM];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_AnimaisPesoHistorico_Animais]...';


GO
ALTER TABLE [dbo].[AnimaisPesoHistorico] DROP CONSTRAINT [FK_AnimaisPesoHistorico_Animais];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Animais];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [FK_Animais_Tutores];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Animais_AnimaisRacas]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [FK_Animais_AnimaisRacas];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_LogsSistema_Pessoas]...';


GO
ALTER TABLE [dbo].[LogsSistema] DROP CONSTRAINT [FK_LogsSistema_Pessoas];


GO
PRINT N'Starting rebuilding table [dbo].[Animais]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Animais] (
    [IdTutores]     INT             NOT NULL,
    [IdAnimal]      INT             IDENTITY (1, 1) NOT NULL,
    [Nome]          VARCHAR (150)   NOT NULL,
    [IdRaca]        INT             NULL,
    [Sexo]          INT             NULL,
    [DtNascim]      DATE            NULL,
    [RgPet]         VARCHAR (150)   NULL,
    [PesoAtual]     DECIMAL (10, 3) NULL,
    [PesoIdeal]     DECIMAL (10, 3) NULL,
    [RecalcularNEM] BIT             CONSTRAINT [DF_Animais_RecalcularNEM] DEFAULT ((0)) NULL,
    [Observacoes]   VARCHAR (MAX)   NULL,
    [Ativo]         BIT             CONSTRAINT [DF_Animais_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]    INT             NULL,
    [IP]            VARCHAR (20)    NULL,
    [DataCadastro]  DATETIME        CONSTRAINT [DF_Animais_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]        ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Animais1] PRIMARY KEY CLUSTERED ([IdAnimal] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Animais])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Animais] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Animais] ([IdAnimal], [IdTutores], [Nome], [IdRaca], [Sexo], [DtNascim], [PesoAtual], [PesoIdeal], [RecalcularNEM], [Observacoes], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdAnimal],
                 [IdTutores],
                 [Nome],
                 [IdRaca],
                 [Sexo],
                 [DtNascim],
                 [PesoAtual],
                 [PesoIdeal],
                 [RecalcularNEM],
                 [Observacoes],
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

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Index [dbo].[Animais].[FK_Animais_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_3]
    ON [dbo].[Animais]([IdRaca] ASC);


GO
PRINT N'Creating Index [dbo].[Animais].[FK_Animais_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_2]
    ON [dbo].[Animais]([IdTutores] ASC);


GO
PRINT N'Creating Index [dbo].[Animais].[IX_Animais]...';


GO
CREATE NONCLUSTERED INDEX [IX_Animais]
    ON [dbo].[Animais]([RgPet] ASC);


GO
PRINT N'Creating Index [dbo].[Animais].[IX_Animais_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_Animais_1]
    ON [dbo].[Animais]([IdTutores] ASC, [Nome] ASC);


GO
PRINT N'Altering Table [dbo].[LogsSistema]...';


GO
ALTER TABLE [dbo].[LogsSistema] ALTER COLUMN [IdPessoa] INT NULL;


GO
PRINT N'Creating Index [dbo].[LogsSistema].[FK_LogsSistema_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_LogsSistema_1]
    ON [dbo].[LogsSistema]([IdPessoa] ASC);


GO
PRINT N'Creating Index [dbo].[LogsSistema].[FK_LogsSistema_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_LogsSistema_2]
    ON [dbo].[LogsSistema]([IdTabela] ASC);


GO
PRINT N'Creating Index [dbo].[LogsSistema].[FK_LogsSistema_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_LogsSistema_3]
    ON [dbo].[LogsSistema]([IdAcao] ASC);


GO
PRINT N'Creating Index [dbo].[Pessoas].[IX_Pessoas_2]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_2]
    ON [dbo].[Pessoas]([DocumentosOutros] ASC);


GO
PRINT N'Creating Index [dbo].[Pessoas].[IX_Pessoas_5]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_5]
    ON [dbo].[Pessoas]([Passaporte] ASC);


GO
PRINT N'Creating Foreign Key [dbo].[FK_AnimaisPesoHistorico_Animais]...';


GO
ALTER TABLE [dbo].[AnimaisPesoHistorico] WITH NOCHECK
    ADD CONSTRAINT [FK_AnimaisPesoHistorico_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_Tutores] FOREIGN KEY ([IdTutores]) REFERENCES [dbo].[Tutores] ([IdTutores]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Animais_AnimaisRacas]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_AnimaisRacas] FOREIGN KEY ([IdRaca]) REFERENCES [dbo].[AnimaisAuxRacas] ([IdRaca]) ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_LogsSistema_Pessoas]...';


GO
ALTER TABLE [dbo].[LogsSistema] WITH NOCHECK
    ADD CONSTRAINT [FK_LogsSistema_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Altering Procedure [dbo].[AnimaisCarregarTO]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <23/07/2021>
-- Description:	<Carregar Animais>
-- =============================================

ALTER PROCEDURE [dbo].[AnimaisCarregarTO]
	@idAnimal as int,
	@idTutor as int,
	@animais as varchar(150)
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	If (@idAnimal > 0)
		Begin
			SELECT  c.IdPessoa, c.Nome AS cNome, a.IdTutores, Tutores.IdTutor, 
					t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, a.RgPet,
					(a.Nome COLLATE SQL_Latin1_General_CP1251_CI_AS) AnimalPesquisa,
					r.IdEspecie, e.Especie, a.IdRaca, r.Raca, a.Sexo AS IdSexo, 
					(CASE a.Sexo 
						WHEN 1 THEN 'Macho' 
						WHEN 2 THEN 'Fêmea' 
					END) AS Sexo, a.DtNascim, a.PesoAtual, a.PesoIdeal, 
					r.IdadeAdulta, r.CrescInicial, r.CrescFinal, a.RecalcularNEM, 
					a.Observacoes, a.Ativo, a.IdOperador, a.IP, a.DataCadastro,
					a.Versao
			FROM    Pessoas AS c INNER JOIN
					Animais AS a INNER JOIN
					Pessoas AS t INNER JOIN
					Tutores ON t.IdPessoa = Tutores.IdTutor ON a.IdTutores = 
						Tutores.IdTutores ON c.IdPessoa = 
						Tutores.IdCliente LEFT OUTER JOIN
					AnimaisAuxEspecies AS e INNER JOIN
					AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON a.IdRaca = 
						r.IdRaca
			WHERE (a.IdAnimal = @idAnimal)
		End
	ELSE IF ((@idTutor > 0) AND (@animais <> ''))
		Begin
			SELECT  c.IdPessoa, c.Nome AS cNome, a.IdTutores, Tutores.IdTutor, 
					t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, a.RgPet,
					(a.Nome COLLATE SQL_Latin1_General_CP1251_CI_AS) AnimalPesquisa,
					r.IdEspecie, e.Especie, a.IdRaca, r.Raca, a.Sexo AS IdSexo, 
					(CASE a.Sexo 
						WHEN 1 THEN 'Macho' 
						WHEN 2 THEN 'Fêmea' 
					END) AS Sexo, a.DtNascim, a.PesoAtual, a.PesoIdeal, 
					r.IdadeAdulta, r.CrescInicial, r.CrescFinal, a.RecalcularNEM, 
					a.Observacoes, a.Ativo, a.IdOperador, a.IP, a.DataCadastro,
					a.Versao
			FROM    Pessoas AS c INNER JOIN
					Animais AS a INNER JOIN
					Pessoas AS t INNER JOIN
					Tutores ON t.IdPessoa = Tutores.IdTutor ON a.IdTutores = 
						Tutores.IdTutores ON c.IdPessoa = 
						Tutores.IdCliente LEFT OUTER JOIN
					AnimaisAuxEspecies AS e INNER JOIN
					AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON a.IdRaca = 
						r.IdRaca
			WHERE (Tutores.IdTutor = @idTutor) AND (a.Nome = @animais)
		End
END
GO
PRINT N'Altering Procedure [dbo].[AnimaisListar]...';


GO
-- =============================================
-- Author:		<Rafael Britto Lobo>
-- Create date: <20/01/2021>
-- Description:	<Lista geral de Animais>
-- =============================================

ALTER PROCEDURE [dbo].[AnimaisListar]
	@idTutor as int,
	@idCliente as int,
	@tpLista as int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    If (@tpLista = 1)
		Begin
			SELECT  c.IdPessoa, c.Nome AS cNome, a.IdTutores, Tutores.IdTutor, 
					t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, a.RgPet,
					(a.Nome COLLATE SQL_Latin1_General_CP1251_CI_AS) AnimalPesquisa,
					r.IdEspecie, e.Especie, a.IdRaca, r.Raca, a.Sexo AS IdSexo, 
					(CASE a.Sexo 
						WHEN 1 THEN 'Macho' 
						WHEN 2 THEN 'Fêmea' 
						END) AS Sexo, a.DtNascim, a.PesoAtual, a.PesoIdeal, 
					r.IdadeAdulta, r.CrescInicial, r.CrescFinal, a.RecalcularNEM, 
					a.Observacoes, a.Ativo, a.IdOperador, a.IP, a.DataCadastro,
					a.Versao
			FROM    Pessoas AS c INNER JOIN
						Animais AS a INNER JOIN
						Pessoas AS t INNER JOIN
						Tutores ON t.IdPessoa = Tutores.IdTutor ON a.IdTutores = 
							Tutores.IdTutores ON c.IdPessoa = 
							Tutores.IdCliente LEFT OUTER JOIN
						AnimaisAuxEspecies AS e INNER JOIN
						AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON a.IdRaca = 
							r.IdRaca
			WHERE	(a.Ativo = 1) AND (c.IdPessoa = @idCliente)
			ORDER BY cNome, Animal
		END
	ELSE IF (@tpLista = 2)
		Begin
			SELECT  c.IdPessoa, c.Nome AS cNome, a.IdTutores, Tutores.IdTutor, 
					t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, a.RgPet, 
					(a.Nome COLLATE SQL_Latin1_General_CP1251_CI_AS) AnimalPesquisa,
					r.IdEspecie, e.Especie, a.IdRaca, r.Raca, a.Sexo AS IdSexo, 
					(CASE a.Sexo 
						WHEN 1 THEN 'Macho' 
						WHEN 2 THEN 'Fêmea' 
						END) AS Sexo, a.DtNascim, a.PesoAtual, a.PesoIdeal, 
					r.IdadeAdulta, r.CrescInicial, r.CrescFinal, a.RecalcularNEM, 
					a.Observacoes, a.Ativo, a.IdOperador, a.IP, a.DataCadastro,
					a.Versao
			FROM    Pessoas AS c INNER JOIN
						Animais AS a INNER JOIN
						Pessoas AS t INNER JOIN
						Tutores ON t.IdPessoa = Tutores.IdTutor ON a.IdTutores = 
							Tutores.IdTutores ON c.IdPessoa = 
							Tutores.IdCliente LEFT OUTER JOIN
						AnimaisAuxEspecies AS e INNER JOIN
						AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON a.IdRaca = 
							r.IdRaca
			WHERE	(a.Ativo = 1) AND (Tutores.IdTutor = @idTutor) AND 
					(Tutores.IdCliente = @idCliente)
			ORDER BY tNome, Animal
		END
END
GO
PRINT N'Altering Procedure [dbo].[AnimaisListarSelecao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <26/07/2021>
-- Description:	<Listagem de Animais para Tela de Seleção>
-- =============================================
ALTER PROCEDURE [dbo].[AnimaisListarSelecao] 
	@pesquisaAnimal as Varchar(250), 
	@idCliente as Int,
	@RowspPage as Int,   /*Linhas por página*/
	@PageNumber as Int   /*Número da Página*/
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@pesquisaAnimal	Varchar(250),
						 @idCliente			Int,
						 @RowspPage			Int,
						 @PageNumber		Int'

	SET @SQL = 'SELECT  IdPessoa, cNome, IdTutores, IdTutor, tNome, IdAnimal, Animal, RgPet,
						AnimalPesquisa, IdEspecie, Especie, IdRaca, Raca, IdSexo, Sexo, 
						DtNascim, PesoAtual, PesoIdeal, IdadeAdulta, CrescInicial, 
						CrescFinal, RecalcularNEM, Observacoes, Ativo, IdOperador, IP, 
						DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY c.Nome, a.Nome) AS NUMBER,
									c.IdPessoa, c.Nome AS cNome, a.IdTutores, Tutores.IdTutor, 
									t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, a.RgPet, 
									(a.Nome COLLATE SQL_Latin1_General_CP1251_CI_AS) AnimalPesquisa,
									r.IdEspecie, e.Especie, a.IdRaca, r.Raca, a.Sexo AS IdSexo, 
									(CASE a.Sexo 
										WHEN 1 THEN ' + '''Macho''' + ' 
										WHEN 2 THEN ' + '''Fêmea''' + ' 
									 END) AS Sexo, a.DtNascim, a.PesoAtual, a.PesoIdeal, 
									r.IdadeAdulta, r.CrescInicial, r.CrescFinal, a.RecalcularNEM, 
									a.Observacoes, a.Ativo, a.IdOperador, a.IP, a.DataCadastro,
									a.Versao
							FROM    Pessoas AS c INNER JOIN
										Animais AS a INNER JOIN
										Pessoas AS t INNER JOIN
										Tutores ON t.IdPessoa = Tutores.IdTutor ON a.IdTutores = 
											Tutores.IdTutores ON c.IdPessoa = 
											Tutores.IdCliente LEFT OUTER JOIN
										AnimaisAuxEspecies AS e INNER JOIN
										AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON a.IdRaca = 
											r.IdRaca
							WHERE   (a.Ativo = 1) AND (c.IdPessoa = @idCliente) '

	IF ((@pesquisaAnimal <> '') AND (@pesquisaAnimal IS NOT NULL))
    BEGIN
         SET @SQL = @SQL + ' AND 
									((a.Nome Like ' + '''%' + @pesquisaAnimal + '%''' + ' COLLATE Latin1_General_CI_AI) OR
									 (t.Nome Like ' + '''%' + @pesquisaAnimal + '%''' + ' COLLATE Latin1_General_CI_AI))';
    END

	SET @SQL = @SQL + ') AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    cNome, Animal';
	
	--PRINT @SQL
	EXEC sp_Executesql     @SQL, @ParameterDef, @pesquisaAnimal = @pesquisaAnimal,
		@idCliente = @idCliente, @RowspPage = @RowspPage, @PageNumber = @PageNumber

END
GO
PRINT N'Altering Procedure [dbo].[LogsListarSelecao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <08/09/2022>
-- Description:	<Listagem de Logs>
-- =============================================
ALTER PROCEDURE [dbo].[LogsListarSelecao] 
	@pesquisaTabela	as Int, 
	@pesquisaAcao	as Int, 
	@dataInicial	as varchar(20),
	@dataFinal		as varchar(20),
	@RowspPage		as Int,   /*Linhas por página*/
	@PageNumber		as Int   /*Número da Página*/
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(MAX);

	SET @ParameterDef = '@pesquisaTabela	as	Int,
						 @pesquisaAcao		as	Int,
						 @dataInicial		as	Varchar(20),
						 @dataFinal			as	Varchar(20),
						 @RowspPage			as	Int,
						 @PageNumber		as	Int';

	SET @SQL = 'SELECT	Number, IdPessoa, Nome As Assinante, IdLog, IdTabela, Tabela, IdAcao, 
						Acao, Mensagem, Justificativa, Datahora
				FROM	(
						SELECT	ROW_NUMBER() OVER(ORDER BY t.IdLog Desc) AS Number, t.IdPessoa, 
								t.Nome, t.IdLog, t.IdTabela, t.Tabela, t.IdAcao, t.Acao, 
								t.Mensagem, t.Justificativa, t.Datahora
						FROM   (
								Select	l.IdPessoa, p.Nome, l.IdLog, l.IdTabela,
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
											When 10 Then ' + '''Assinatura de Planos''' + '
										 End) Acao, l.Mensagem, l.Justificativa, l.Datahora
								FROM	LogsSistema AS l INNER JOIN
											Pessoas AS p ON l.IdPessoa = p.IdPessoa
								WHERE	(1 = 1)';

	IF ((@pesquisaTabela IS NOT NULL) AND (@pesquisaTabela > 0))
		BEGIN
			 SET @SQL = @SQL + ' AND 
										(l.IdTabela =  @pesquisaTabela)';
		END
	
	IF ((@pesquisaAcao IS NOT NULL) AND (@pesquisaAcao > 0))
		BEGIN
			 SET @SQL = @SQL + ' AND 
										(l.IdAcao = @pesquisaAcao)';
		END

	IF ((@dataInicial <> '') AND (@dataInicial IS NOT NULL))
		BEGIN
			IF ((@dataFinal <> '') AND (@dataFinal IS NOT NULL))
				BEGIN
			 		SET @SQL = @SQL + ' AND 
										(convert(date, l.Datahora, 103) BETWEEN ''' + 
											@dataInicial + ''' AND ''' + @dataFinal + ''')';
				END
			ELSE 
				BEGIN
						SET @SQL = @SQL + ' AND 
										(convert(date, l.Datahora, 103) = ''' + 
											@dataInicial + ''')';
				END
		END

	SET @SQL = @SQL + '
								) t
						) logs
				WHERE	NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)';
	
	--PRINT @SQL
	EXEC sp_Executesql @SQL, @ParameterDef, @pesquisaTabela = @pesquisaTabela,
			@pesquisaAcao = @pesquisaAcao, @dataInicial = @dataInicial, 
			@dataFinal = @dataFinal, @RowspPage = @RowspPage, 
			@PageNumber = @PageNumber
END
GO
PRINT N'Refreshing Procedure [dbo].[ModificaTutorParaCliente]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ModificaTutorParaCliente]';


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AnimaisPesoHistorico] WITH CHECK CHECK CONSTRAINT [FK_AnimaisPesoHistorico_Animais];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Animais];

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_Tutores];

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_AnimaisRacas];

ALTER TABLE [dbo].[LogsSistema] WITH CHECK CHECK CONSTRAINT [FK_LogsSistema_Pessoas];


GO
PRINT N'Update complete.';


GO
