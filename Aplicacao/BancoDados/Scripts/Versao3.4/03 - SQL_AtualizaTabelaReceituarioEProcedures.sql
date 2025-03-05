
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Altering Table [dbo].[Receituario]...';


GO
ALTER TABLE [dbo].[Receituario] ALTER COLUMN [QuantVeic] VARCHAR (50) NULL;


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
						IdCardapio, Descricao, DtCardapio, PesoAtual, 
						PesoIdeal, IdReceita, Arquivo, NrReceita, dTpRec, 
						TipoReceita, InstrucoesReceita, Titulo, Veiculo, 
						QuantVeic, Posologia, Prescricao, DataReceita, 
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
									rec.Titulo, rec.Veiculo, rec.QuantVeic, rec.Posologia, rec.Prescricao, 
									rec.DataReceita, rec.LocalReceita, rec.Ativo, rec.IdOperador, rec.IP, 
									rec.DataCadastro
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
PRINT N'Altering Procedure [dbo].[ReceituarioListarSelecaoTotalPaginas]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <27/10/2022>
-- Description:	<Total de Páginas Receituário>
-- =============================================
ALTER PROCEDURE [dbo].[ReceituarioListarSelecaoTotalPaginas]
	@idPessoa	as Int,
	@idTutor	as Int,
	@idTpRec	as Int,
	@idAnimal	as Int,
	@dtIni		as Varchar(20),
	@dtFim		as VArchar(20)
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
						 @dtFim			VArchar(20)';

	SET @SQL = 'SELECT	Count(rec.IdReceita) Total
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

	--PRINT @SQL
	EXEC sp_Executesql	@SQL, @ParameterDef, @idPessoa = @idPessoa, 
		@idTutor = @idTutor, @idAnimal = @idAnimal, @idTpRec = @idTpRec, 
		@dtIni = @dtIni, @dtFim = @dtFim
END
GO
PRINT N'Refreshing Procedure [dbo].[ReceituarioNutrientesImpressao]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ReceituarioNutrientesImpressao]';


GO
PRINT N'Refreshing Procedure [dbo].[ReceituarioNutrientesListar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ReceituarioNutrientesListar]';


GO
PRINT N'Update complete.';


GO
