
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating [dbo].[Dietas].[FK_Dietas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Dietas_1]
    ON [dbo].[Dietas]([IdPessoa] ASC);


GO
PRINT N'Creating [dbo].[Dietas].[IX_Dietas]...';


GO
CREATE NONCLUSTERED INDEX [IX_Dietas]
    ON [dbo].[Dietas]([Dieta] ASC);


GO
PRINT N'Creating [dbo].[Nutrientes].[FK_AlimentosAuxComponentes_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentosAuxComponentes_1]
    ON [dbo].[Nutrientes]([IdGrupo] ASC);


GO
PRINT N'Creating [dbo].[Nutrientes].[FK_AlimentosAuxComponentes_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentosAuxComponentes_2]
    ON [dbo].[Nutrientes]([IdUnidade] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[FK_Pessoas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Pessoas_1]
    ON [dbo].[Pessoas]([IdTpPessoa] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[FK_Pessoas_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Pessoas_2]
    ON [dbo].[Pessoas]([IdCliente] ASC);


GO
PRINT N'Creating [dbo].[FK_Dietas_Pessoas]...';


GO
ALTER TABLE [dbo].[Dietas] WITH NOCHECK
    ADD CONSTRAINT [FK_Dietas_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_AlimentosAuxComponentes_AlimentosComponentesAuxGrupos]...';


GO
ALTER TABLE [dbo].[Nutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_AlimentosAuxComponentes_AlimentosComponentesAuxGrupos] FOREIGN KEY ([IdGrupo]) REFERENCES [dbo].[NutrientesAuxGrupos] ([IdGrupo]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Pessoas_Pessoas]...';


GO
ALTER TABLE [dbo].[Pessoas] WITH NOCHECK
    ADD CONSTRAINT [FK_Pessoas_Pessoas] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Dietas] WITH CHECK CHECK CONSTRAINT [FK_Dietas_Pessoas];

ALTER TABLE [dbo].[Nutrientes] WITH CHECK CHECK CONSTRAINT [FK_AlimentosAuxComponentes_AlimentosComponentesAuxGrupos];

ALTER TABLE [dbo].[Pessoas] WITH CHECK CHECK CONSTRAINT [FK_Pessoas_Pessoas];


GO
PRINT N'Update complete.';


GO
