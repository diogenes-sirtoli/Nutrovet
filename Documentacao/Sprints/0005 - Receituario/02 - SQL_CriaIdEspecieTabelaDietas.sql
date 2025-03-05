SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

GO

/* Cria IdEspecie na Tabela Dietas e faz a Referência com a Tabela AnimaisAuxEspecies   */

PRINT N'Dropping Default Constraint [dbo].[DF_Dietas_Ativo]...';


GO
ALTER TABLE [dbo].[Dietas] DROP CONSTRAINT [DF_Dietas_Ativo];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_Dietas_DataCadastro]...';


GO
ALTER TABLE [dbo].[Dietas] DROP CONSTRAINT [DF_Dietas_DataCadastro];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Cardapio_Dietas]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Dietas];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_DietasAlimentos_Dietas]...';


GO
ALTER TABLE [dbo].[DietasAlimentos] DROP CONSTRAINT [FK_DietasAlimentos_Dietas];


GO
PRINT N'Starting rebuilding table [dbo].[Dietas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Dietas] (
    [IdEspecie]    INT             NULL,
    [IdDieta]      INT             IDENTITY (1, 1) NOT NULL,
    [Dieta]        VARCHAR (150)   NOT NULL,
    [Carboidrato]  DECIMAL (10, 3) NULL,
    [Proteina]     DECIMAL (10, 3) NULL,
    [Gordura]      DECIMAL (10, 3) NULL,
    [Ativo]        BIT             CONSTRAINT [DF_Dietas_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        CONSTRAINT [DF_Dietas_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Dietas1] PRIMARY KEY CLUSTERED ([IdDieta] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Dietas])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Dietas] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Dietas] ([IdDieta], [Dieta], [Carboidrato], [Proteina], [Gordura], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdDieta],
                 [Dieta],
                 [Carboidrato],
                 [Proteina],
                 [Gordura],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Dietas]
        ORDER BY [IdDieta] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Dietas] OFF;
    END

DROP TABLE [dbo].[Dietas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Dietas]', N'Dietas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Dietas1]', N'PK_Dietas', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Index [dbo].[Dietas].[IX_Dietas]...';


GO
CREATE NONCLUSTERED INDEX [IX_Dietas]
    ON [dbo].[Dietas]([Dieta] ASC);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Cardapio_Dietas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Dietas] FOREIGN KEY ([IdDieta]) REFERENCES [dbo].[Dietas] ([IdDieta]) ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_DietasAlimentos_Dietas]...';


GO
ALTER TABLE [dbo].[DietasAlimentos] WITH NOCHECK
    ADD CONSTRAINT [FK_DietasAlimentos_Dietas] FOREIGN KEY ([IdDieta]) REFERENCES [dbo].[Dietas] ([IdDieta]) ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Dietas_AnimaisAuxEspecies]...';


GO
ALTER TABLE [dbo].[Dietas] WITH NOCHECK
    ADD CONSTRAINT [FK_Dietas_AnimaisAuxEspecies] FOREIGN KEY ([IdEspecie]) REFERENCES [dbo].[AnimaisAuxEspecies] ([IdEspecie]);


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Dietas];

ALTER TABLE [dbo].[DietasAlimentos] WITH CHECK CHECK CONSTRAINT [FK_DietasAlimentos_Dietas];

ALTER TABLE [dbo].[Dietas] WITH CHECK CHECK CONSTRAINT [FK_Dietas_AnimaisAuxEspecies];


GO
PRINT N'Update complete.';

GO

/* Atualiza dados para IdEspecie na Tabela Dietas  */


SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[Dietas] DROP CONSTRAINT [FK_Dietas_AnimaisAuxEspecies]
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=1
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=4
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=5
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=6
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=7
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=8
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=9
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=10
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=11
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=12
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=13
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=14
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=15
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=16
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=17
UPDATE [dbo].[Dietas] SET [IdEspecie]=2 WHERE [IdDieta]=18
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=19
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=20
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=21
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=22
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=23
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=24
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=25
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=26
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=27
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=28
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=29
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=30
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=31
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=32
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=33
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=34
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=35
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=36
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=37
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=38
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=39
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=40
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=41
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=42
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=43
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=45
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=46
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=47
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=48
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=49
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=50
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=51
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=52
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=53
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=54
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=55
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=56
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=57
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=58
UPDATE [dbo].[Dietas] SET [IdEspecie]=1 WHERE [IdDieta]=59
ALTER TABLE [dbo].[Dietas]
    ADD CONSTRAINT [FK_Dietas_AnimaisAuxEspecies] FOREIGN KEY ([IdEspecie]) REFERENCES [dbo].[AnimaisAuxEspecies] ([IdEspecie])
COMMIT TRANSACTION

Go

/* Excluí Os dois registros inativos */

Delete From DietasAlimentos
Where IdDieta = 3

Go

Delete From Dietas
Where IdDieta = 3

Go

Delete From DietasAlimentos
Where IdDieta = 2

Go

Update	Cardapio Set
	IdDieta = 23
Where	IdCardapio in (
		SELECT	c.IdCardapio
		FROM	Cardapio AS c INNER JOIN
					Animais AS a ON c.IdAnimal = a.IdAnimal INNER JOIN
					AnimaisAuxRacas AS r ON a.IdRaca = r.IdRaca INNER JOIN
					AnimaisAuxEspecies AS e ON r.IdEspecie = e.IdEspecie
		WHERE	(c.IdDieta = 2) AND (e.IdEspecie = 1))

Go

Update	Cardapio Set
	IdDieta = 5
Where	IdCardapio in (
		SELECT	c.IdCardapio
		FROM	Cardapio AS c INNER JOIN
					Animais AS a ON c.IdAnimal = a.IdAnimal INNER JOIN
					AnimaisAuxRacas AS r ON a.IdRaca = r.IdRaca INNER JOIN
					AnimaisAuxEspecies AS e ON r.IdEspecie = e.IdEspecie
		WHERE	(c.IdDieta = 2) AND (e.IdEspecie = 2))

GO

Delete From Dietas
Where IdDieta = 2

GO

UPDATE	c Set
	c.Dieta = Replace(d.Dieta, 'Felinos - ', '')
FROM	Dietas AS d INNER JOIN
			Dietas AS c ON d.IdDieta = c.IdDieta
WHERE	(c.IdEspecie = 2)

Go

UPDATE	c Set
	c.Dieta = Replace(d.Dieta, 'Cães - ', '')
FROM	Dietas AS d INNER JOIN
			Dietas AS c ON d.IdDieta = c.IdDieta
WHERE	(c.IdEspecie = 1)

Go

PRINT N'Dropping Index [dbo].[Dietas].[IX_Dietas]...';


GO
DROP INDEX [IX_Dietas]
    ON [dbo].[Dietas];


GO
PRINT N'Creating Unique Constraint [dbo].[IX_Dietas]...';


GO
ALTER TABLE [dbo].[Dietas]
    ADD CONSTRAINT [IX_Dietas] UNIQUE NONCLUSTERED ([IdEspecie] ASC, [Dieta] ASC);


GO

Update Cardapio Set
	IdDieta = 35
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 1))

GO

/* Manutenção Canina */
Update Cardapio Set
	IdDieta = 23
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta in (4, 5)))

GO

Update Cardapio Set
	IdDieta = 24
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 6))

GO

Update Cardapio Set
	IdDieta = 25
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 7))

GO

Update Cardapio Set
	IdDieta = 27
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 9))

GO

Update Cardapio Set
	IdDieta = 36
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 10))

GO

Update Cardapio Set
	IdDieta = 37
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 11))

GO

Update Cardapio Set
	IdDieta = 38
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 12))

GO

Update Cardapio Set
	IdDieta = 39
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 13))

GO

Update Cardapio Set
	IdDieta = 40
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 14))

GO

Update Cardapio Set
	IdDieta = 49
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 16))

GO

Update Cardapio Set
	IdDieta = 52
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 1) AND (Dietas.IdEspecie = 2) AND
			(Cardapio.IdDieta = 17))

GO

Update Cardapio Set
	IdDieta = 14
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 19))

GO

/*Manutenção Felina*/

Update Cardapio Set
	IdDieta = 5
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta in (20, 22, 23, 28, 29, 31, 33, 57, 58)))

GO

Update Cardapio Set
	IdDieta = 6
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 24))

GO

Update Cardapio Set
	IdDieta = 8
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 26))

GO

Update Cardapio Set
	IdDieta = 9
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 27))

GO

Update Cardapio Set
	IdDieta = 1
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 35))

GO

Update Cardapio Set
	IdDieta = 10
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 36))

GO

Update Cardapio Set
	IdDieta = 11
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 37))

GO

Update Cardapio Set
	IdDieta = 12
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 38))

GO

Update Cardapio Set
	IdDieta = 13
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 39))

GO

Update Cardapio Set
	IdDieta = 16
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 49))

GO

Update Cardapio Set
	IdDieta = 17
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 52))

GO

Update Cardapio Set
	IdDieta = 18
Where IdCardapio in 
	(SELECT	Cardapio.IdCardapio
	FROM	Cardapio INNER JOIN
				Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
				Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
				AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
				AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
	WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) AND
			(Cardapio.IdDieta = 53))

GO


PRINT N'Update complete.';


GO
