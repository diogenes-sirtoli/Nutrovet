
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO


PRINT N'Dropping [dbo].[Animais].[FK_Animais_2]...';


GO
DROP INDEX [FK_Animais_2]
    ON [dbo].[Animais];


GO
PRINT N'Dropping [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [FK_Animais_Tutores];


GO
PRINT N'Dropping [dbo].[IX_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas] DROP CONSTRAINT [IX_PlanosAssinaturas];


GO
PRINT N'Altering [dbo].[Animais]...';


GO
ALTER TABLE [dbo].[Animais] ALTER COLUMN [IdTutores] INT NOT NULL;


GO
PRINT N'Creating [dbo].[Animais].[FK_Animais_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_2]
    ON [dbo].[Animais]([IdTutores] ASC);


GO
PRINT N'Altering [dbo].[PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas] ALTER COLUMN [dNomePlano] INT NOT NULL;

ALTER TABLE [dbo].[PlanosAssinaturas] ALTER COLUMN [dPeriodo] INT NOT NULL;

ALTER TABLE [dbo].[PlanosAssinaturas] ALTER COLUMN [dPlanoTp] INT NOT NULL;


GO
PRINT N'Creating [dbo].[IX_PlanosAssinaturas]...';


GO
ALTER TABLE [dbo].[PlanosAssinaturas]
    ADD CONSTRAINT [IX_PlanosAssinaturas] UNIQUE NONCLUSTERED ([dNomePlano] ASC, [dPeriodo] ASC);


GO
PRINT N'Creating [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_Tutores] FOREIGN KEY ([IdTutores]) REFERENCES [dbo].[Tutores] ([IdTutores]) ON UPDATE CASCADE;


GO
PRINT N'Refreshing [dbo].[ListaGeralAnimais]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ListaGeralAnimais]';


GO
PRINT N'Refreshing [dbo].[ModificaTutorParaCliente]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ModificaTutorParaCliente]';


GO
PRINT N'Checking existing data against newly created constraints';


GO


ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_Tutores];


GO
PRINT N'Update complete.';


GO
