SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

GO

PRINT N'Update na Tabela PlanosAssinaturas com IdPlano = 1';

GO

UPDATE PlanosAssinaturas SET
	IdPlanoPagarMe = 564080,
	dNomePlano = 1,
	ValorPlano = 20.00,
	dPlanoTp = 1,
	dPeriodo = 1
WHERE IdPlano = 1

GO

PRINT N'Update na Tabela PlanosAssinaturas com IdPlano = 2';

GO

UPDATE PlanosAssinaturas SET
	IdPlanoPagarMe = 564082,
	dNomePlano = 2,
	ValorPlano = 40.00,
	dPlanoTp = 1,
	dPeriodo = 1
WHERE IdPlano = 2

GO

PRINT N'Update na Tabela PlanosAssinaturas com IdPlano = 3';

GO

UPDATE PlanosAssinaturas SET
	IdPlanoPagarMe = 564084,
	dNomePlano = 3,
	ValorPlano = 60.00,
	dPlanoTp = 1,
	dPeriodo = 1
WHERE IdPlano = 3

GO

PRINT N'Inserts na Tabela PlanosAssinaturas';

GO

INSERT	INTO	PlanosAssinaturas (IdPlanoPagarMe, dNomePlano, ValorPlano, dPlanoTp, dPeriodo, QtdAnimais, Ativo)
Values	(564081, 1, 200.00, 1, 2, 10, 1),
		(564083, 2, 400.00, 1, 2, 20, 1),
		(564085, 3, 600.00, 1, 2, 7000000, 1),
		(null, 4, 10.00, 2, 1, 0, 1),
		(null, 4, 100.00, 2, 2, 0, 1),
		(null, 5, 15.00, 2, 1, 0, 1),
		(null, 5, 150.00, 2, 2, 0, 1)

GO


PRINT N'Update complete.';

GO







