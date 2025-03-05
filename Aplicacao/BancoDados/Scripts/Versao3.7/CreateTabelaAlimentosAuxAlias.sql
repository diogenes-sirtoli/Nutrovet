SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating Table [dbo].[AlimentosAuxAlias]...';


GO
CREATE TABLE [dbo].[AlimentosAuxAlias] (
    [IdPessoa]     INT           NOT NULL,
    [IdAlias]      INT           IDENTITY (1, 1) NOT NULL,
    [IdAlimento]   INT           NOT NULL,
    [Alias]        VARCHAR (200) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_AlimentosAuxAlias] PRIMARY KEY CLUSTERED ([IdAlias] ASC),
    CONSTRAINT [IX_AlimentosAuxAlias] UNIQUE NONCLUSTERED ([IdPessoa] ASC, [IdAlimento] ASC)
);


GO
PRINT N'Creating Index [dbo].[AlimentosAuxAlias].[FK_AlimentosAuxAlias_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentosAuxAlias_1]
    ON [dbo].[AlimentosAuxAlias]([IdPessoa] ASC);


GO
PRINT N'Creating Index [dbo].[AlimentosAuxAlias].[FK_AlimentosAuxAlias_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_AlimentosAuxAlias_2]
    ON [dbo].[AlimentosAuxAlias]([IdAlimento] ASC);


GO
PRINT N'Creating Index [dbo].[AlimentosAuxAlias].[IX_AlimentosAuxAlias_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_AlimentosAuxAlias_1]
    ON [dbo].[AlimentosAuxAlias]([IdPessoa] ASC, [Alias] ASC);


GO
PRINT N'Creating Default Constraint [dbo].[DF_AlimentosAuxAlias_Ativo]...';


GO
ALTER TABLE [dbo].[AlimentosAuxAlias]
    ADD CONSTRAINT [DF_AlimentosAuxAlias_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating Default Constraint [dbo].[DF_AlimentosAuxAlias_DataCadastro]...';


GO
ALTER TABLE [dbo].[AlimentosAuxAlias]
    ADD CONSTRAINT [DF_AlimentosAuxAlias_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating Foreign Key [dbo].[FK_AlimentosAuxAlias_Alimentos]...';


GO
ALTER TABLE [dbo].[AlimentosAuxAlias] WITH NOCHECK
    ADD CONSTRAINT [FK_AlimentosAuxAlias_Alimentos] FOREIGN KEY ([IdAlimento]) REFERENCES [dbo].[Alimentos] ([IdAlimento]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_AlimentosAuxAlias_Pessoas]...';


GO
ALTER TABLE [dbo].[AlimentosAuxAlias] WITH NOCHECK
    ADD CONSTRAINT [FK_AlimentosAuxAlias_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[AlimentosAuxAlias] WITH CHECK CHECK CONSTRAINT [FK_AlimentosAuxAlias_Alimentos];

ALTER TABLE [dbo].[AlimentosAuxAlias] WITH CHECK CHECK CONSTRAINT [FK_AlimentosAuxAlias_Pessoas];


GO
PRINT N'Update complete.';


GO
