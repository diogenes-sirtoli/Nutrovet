USE [Nutrovet]
GO
/****** Object:  StoredProcedure [dbo].[AlimentosNutrientesListar]    Script Date: 25/09/2023 11:01:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


	/*PRINT @SQL*/
	EXEC sp_Executesql	@SQL, @ParameterDef, @pesqAlimento	= @pesqAlimento, @idNutr = @idNutr,
						@idPessoa = @idPessoa, @RowspPage = @RowspPage, @PageNumber = @PageNumber, 
						@gerencia = @gerencia
END
