
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating [dbo].[NutraceuticosListar]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <01/11/2021>
-- Description:	<Listagem de Nutracêuticos>
-- =============================================
CREATE PROCEDURE NutraceuticosListar 
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
				WHEN 7 THEN 'mcg/Kg' 
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
				WHEN 7 THEN 'mcg/Kg' 
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
PRINT N'Update complete.';


GO
