
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping [dbo].[IX_Pessoas_1]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [IX_Pessoas_1];


GO
PRINT N'Altering [dbo].[Pessoas]...';


GO
ALTER TABLE [dbo].[Pessoas] ALTER COLUMN [Email] VARCHAR (200) NULL;


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_1]
    ON [dbo].[Pessoas]([Email] ASC);


GO
PRINT N'Altering [dbo].[AnimaisListar]...';


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
					t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, 
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
					t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, 
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
PRINT N'Refreshing [dbo].[AnimaisCarregarTO]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[AnimaisCarregarTO]';


GO
PRINT N'Refreshing [dbo].[ListaGeralPessoas]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ListaGeralPessoas]';


GO
PRINT N'Refreshing [dbo].[ModificaTutorParaCliente]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ModificaTutorParaCliente]';


GO
PRINT N'Update complete.';


GO
