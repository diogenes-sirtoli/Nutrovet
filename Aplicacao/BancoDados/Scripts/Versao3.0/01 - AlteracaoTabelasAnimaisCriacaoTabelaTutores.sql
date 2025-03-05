
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

GO

PRINT N'Dropping [dbo].[Animais].[FK_Animais_1]...';

GO
DROP INDEX [FK_Animais_1]
    ON [dbo].[Animais];


GO
PRINT N'Dropping [dbo].[DF_Animais_RecalcularNEM]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [DF_Animais_RecalcularNEM];


GO
PRINT N'Dropping [dbo].[DF_Animais_Ativo]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [DF_Animais_Ativo];


GO
PRINT N'Dropping [dbo].[DF_Animais_DataCadastro]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [DF_Animais_DataCadastro];


GO
PRINT N'Dropping [dbo].[FK_Animais_Pessoas]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [FK_Animais_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Animais_AnimaisRacas]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [FK_Animais_AnimaisRacas];


GO
PRINT N'Dropping [dbo].[FK_AnimaisPesoHistorico_Animais]...';


GO
ALTER TABLE [dbo].[AnimaisPesoHistorico] DROP CONSTRAINT [FK_AnimaisPesoHistorico_Animais];


GO
PRINT N'Dropping [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Animais];


GO
PRINT N'Dropping [dbo].[IX_Animais]...';


GO
ALTER TABLE [dbo].[Animais] DROP CONSTRAINT [IX_Animais];


GO
PRINT N'Starting rebuilding table [dbo].[Animais]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Animais] (
    [IdPessoa]		INT             NULL,
	[IdTutores]     INT             NULL,
    [IdAnimal]      INT             IDENTITY (1, 1) NOT NULL,
    [Nome]          VARCHAR (150)   NOT NULL,
    [IdRaca]        INT             NULL,
    [Sexo]          INT             NULL,
    [DtNascim]      DATE            NULL,
    [PesoAtual]     DECIMAL (10, 3) NULL,
    [PesoIdeal]     DECIMAL (10, 3) NULL,
    [RecalcularNEM] BIT             CONSTRAINT [DF_Animais_RecalcularNEM] DEFAULT ((0)) NULL,
    [Observacoes]   VARCHAR (MAX)   NULL,
    [Ativo]         BIT             CONSTRAINT [DF_Animais_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]    INT             NULL,
    [IP]            VARCHAR (20)    NULL,
    [DataCadastro]  DATETIME        CONSTRAINT [DF_Animais_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]        ROWVERSION      NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Animais1] PRIMARY KEY CLUSTERED ([IdAnimal] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Animais])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Animais] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Animais] ([IdPessoa], [IdAnimal], [Nome], [IdRaca], [Sexo], [DtNascim], [PesoAtual], [PesoIdeal], [RecalcularNEM], [Observacoes], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   
				 [IdPessoa],
				 [IdAnimal],
                 [Nome],
                 [IdRaca],
                 [Sexo],
                 [DtNascim],
                 [PesoAtual],
                 [PesoIdeal],
                 [RecalcularNEM],
                 [Observacoes],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[Animais]
        ORDER BY [IdAnimal] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Animais] OFF;
    END

DROP TABLE [dbo].[Animais];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Animais]', N'Animais';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Animais1]', N'PK_Animais', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Animais].[FK_Animais_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_3]
    ON [dbo].[Animais]([IdRaca] ASC);


GO
PRINT N'Creating [dbo].[Animais].[FK_Animais_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Animais_2]
    ON [dbo].[Animais]([IdTutores] ASC);


GO
PRINT N'Creating [dbo].[Tutores]...';


GO
CREATE TABLE [dbo].[Tutores] (
    [IdCliente]    INT          NOT NULL,
    [IdTutores]    INT          IDENTITY (1, 1) NOT NULL,
    [IdTutor]      INT          NOT NULL,
    [Ativo]        BIT          NULL,
    [IdOperador]   INT          NULL,
    [IP]           VARCHAR (20) NULL,
    [DataCadastro] DATETIME     NULL,
    [Versao]       ROWVERSION   NULL,
    CONSTRAINT [PK_Tutores] PRIMARY KEY CLUSTERED ([IdTutores] ASC),
    CONSTRAINT [IX_Tutores] UNIQUE NONCLUSTERED ([IdCliente] ASC, [IdTutor] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Animais_AnimaisRacas]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_AnimaisRacas] FOREIGN KEY ([IdRaca]) REFERENCES [dbo].[AnimaisAuxRacas] ([IdRaca]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AnimaisPesoHistorico_Animais]...';


GO
ALTER TABLE [dbo].[AnimaisPesoHistorico] WITH NOCHECK
    ADD CONSTRAINT [FK_AnimaisPesoHistorico_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Cardapio_Animais]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
    ADD CONSTRAINT [FK_Cardapio_Animais] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animais] ([IdAnimal]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Animais_Tutores]...';


GO
ALTER TABLE [dbo].[Animais] WITH NOCHECK
    ADD CONSTRAINT [FK_Animais_Tutores] FOREIGN KEY ([IdTutores]) REFERENCES [dbo].[Tutores] ([IdTutores]) ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_AnimaisRacas];

ALTER TABLE [dbo].[AnimaisPesoHistorico] WITH CHECK CHECK CONSTRAINT [FK_AnimaisPesoHistorico_Animais];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Animais];

ALTER TABLE [dbo].[Animais] WITH CHECK CHECK CONSTRAINT [FK_Animais_Tutores];


GO
PRINT N'Update complete.';


GO
