SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


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
PRINT N'Altering Procedure [dbo].[LogsListarSelecaoTotalPaginas]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <05/09/2022>
-- Description:	<Total de Páginas de Logs>
-- =============================================
ALTER PROCEDURE [dbo].[LogsListarSelecaoTotalPaginas]
	@pesquisaTabela	as Int, 
	@pesquisaAcao	as Int,
	@dataInicial as varchar(20),
	@dataFinal as varchar(20)
AS
BEGIN
SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@pesquisaTabela	as Int, 
						 @pesquisaAcao		as Int,
						 @dataInicial		as varchar(20),
						 @dataFinal			as varchar(20)';

	SET @SQL = 'SELECT	Count(IdLog) Total
				FROM	(
						SELECT	t.IdPessoa, t.Nome, t.IdLog, t.IdTabela, t.Tabela, 
								t.IdAcao, t.Acao, t.Mensagem, t.Datahora
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
										 End) Acao, l.Mensagem, l.Datahora
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
						) logs';
	
	--PRINT @SQL
	EXEC sp_Executesql @SQL, @ParameterDef, @pesquisaTabela = @pesquisaTabela,
			@pesquisaAcao = @pesquisaAcao, @dataInicial = @dataInicial, 
			@dataFinal = @dataFinal
END
GO
PRINT N'Update complete.';


GO
