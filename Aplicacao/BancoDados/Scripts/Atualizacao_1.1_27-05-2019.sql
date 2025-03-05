SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Altering [dbo].[AlimentoNutrientes]...';


GO
ALTER TABLE [dbo].[AlimentoNutrientes] ALTER COLUMN [Valor] DECIMAL (18, 3) NULL;


GO
PRINT N'Creating [dbo].[DF_AlimentoNutrientes_Valor]...';


GO
ALTER TABLE [dbo].[AlimentoNutrientes]
    ADD CONSTRAINT [DF_AlimentoNutrientes_Valor] DEFAULT ((0)) FOR [Valor];


GO
PRINT N'Update complete.';


GO
