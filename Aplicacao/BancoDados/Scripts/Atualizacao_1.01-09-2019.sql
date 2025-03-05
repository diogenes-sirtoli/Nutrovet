
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating [dbo].[Nutraceuticos]...';


GO
CREATE TABLE [dbo].[Nutraceuticos] (
    [IdNutrac]     INT             IDENTITY (1, 1) NOT NULL,
    [IdEspecie]    INT             NOT NULL,
    [IdNutr]       INT             NOT NULL,
    [DoseMin]      DECIMAL (10, 3) NULL,
    [IdUnidMin]    INT             NULL,
    [DoseMax]      DECIMAL (10, 3) NULL,
    [IdUnidMax]    INT             NULL,
    [IdPrescr1]    INT             NULL,
    [IdPrescr2]    INT             NULL,
    [Obs]          VARCHAR (MAX)   NULL,
    [Ativo]        BIT             NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [PK_Nutraceuticos] PRIMARY KEY CLUSTERED ([IdNutrac] ASC),
    CONSTRAINT [IX_Nutraceuticos] UNIQUE NONCLUSTERED ([IdEspecie] ASC, [IdNutr] ASC)
);


GO
PRINT N'Creating [dbo].[Nutraceuticos].[FK_Nutraceuticos_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Nutraceuticos_1]
    ON [dbo].[Nutraceuticos]([IdEspecie] ASC);


GO
PRINT N'Creating [dbo].[Nutraceuticos].[FK_Nutraceuticos_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Nutraceuticos_2]
    ON [dbo].[Nutraceuticos]([IdNutr] ASC);


GO
PRINT N'Creating [dbo].[PrescricaoAuxTipos]...';


GO
CREATE TABLE [dbo].[PrescricaoAuxTipos] (
    [IdPrescr]     INT           IDENTITY (1, 1) NOT NULL,
    [Prescricao]   VARCHAR (100) NOT NULL,
    [Ativo]        BIT           NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [PK_PrescricaoAuxTipos] PRIMARY KEY CLUSTERED ([IdPrescr] ASC)
);


GO
PRINT N'Creating [dbo].[PrescricaoAuxTipos].[IX_PrescricaoAuxTipos]...';


GO
CREATE NONCLUSTERED INDEX [IX_PrescricaoAuxTipos]
    ON [dbo].[PrescricaoAuxTipos]([Prescricao] ASC);


GO
PRINT N'Creating [dbo].[DF_Nutraceuticos_Ativo]...';


GO
ALTER TABLE [dbo].[Nutraceuticos]
    ADD CONSTRAINT [DF_Nutraceuticos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[DF_Nutraceuticos_DataCadastro]...';


GO
ALTER TABLE [dbo].[Nutraceuticos]
    ADD CONSTRAINT [DF_Nutraceuticos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_PrescricaoAuxTipos_DataCadastro]...';


GO
ALTER TABLE [dbo].[PrescricaoAuxTipos]
    ADD CONSTRAINT [DF_PrescricaoAuxTipos_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating [dbo].[DF_PrescricaoAuxTipos_Ativo]...';


GO
ALTER TABLE [dbo].[PrescricaoAuxTipos]
    ADD CONSTRAINT [DF_PrescricaoAuxTipos_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating [dbo].[FK_Nutraceuticos_Nutrientes]...';


GO
ALTER TABLE [dbo].[Nutraceuticos] WITH NOCHECK
    ADD CONSTRAINT [FK_Nutraceuticos_Nutrientes] FOREIGN KEY ([IdNutr]) REFERENCES [dbo].[Nutrientes] ([IdNutr]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Nutraceuticos_AnimaisAuxEspecies]...';


GO
ALTER TABLE [dbo].[Nutraceuticos] WITH NOCHECK
    ADD CONSTRAINT [FK_Nutraceuticos_AnimaisAuxEspecies] FOREIGN KEY ([IdEspecie]) REFERENCES [dbo].[AnimaisAuxEspecies] ([IdEspecie]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Nutraceuticos_PrescricaoAuxTipos]...';


GO
ALTER TABLE [dbo].[Nutraceuticos] WITH NOCHECK
    ADD CONSTRAINT [FK_Nutraceuticos_PrescricaoAuxTipos] FOREIGN KEY ([IdPrescr1]) REFERENCES [dbo].[PrescricaoAuxTipos] ([IdPrescr]);


GO
PRINT N'Creating [dbo].[FK_Nutraceuticos_PrescricaoAuxTipos1]...';


GO
ALTER TABLE [dbo].[Nutraceuticos] WITH NOCHECK
    ADD CONSTRAINT [FK_Nutraceuticos_PrescricaoAuxTipos1] FOREIGN KEY ([IdPrescr2]) REFERENCES [dbo].[PrescricaoAuxTipos] ([IdPrescr]);


GO
PRINT N'Creating [dbo].[IdadeEmAnosMesesDias]...';


GO

CREATE PROCEDURE IdadeEmAnosMesesDias
	@dataAniver as Date
AS
BEGIN
	SET DATEFORMAT dmy                
    SET QUERY_GOVERNOR_COST_LIMIT 0
                
    Declare @now date, @dob date 
    Declare @now_i int, @dob_i int, @days_in_birth_month int
    Declare @years int, @months int, @days int
    Set @now = GetDate() 
    Set @dob = @dataAniver

    Set @now_i = convert(varchar(8), @now, 112)
    Set @dob_i = convert(varchar(8), @dob, 112)

    Set @years = ( @now_i - @dob_i)/10000
    set @months = (1200 + (month(@now) - month(@dob)) * 100 + 
	    day(@now) - day(@dob)) / 100 % 12

    set @days_in_birth_month = day(dateadd(d, -1, left(
	    convert(varchar(8), dateadd(m, 1, @dob), 112), 6) + '01'))
    set @days = (sign(day(@now) - day(@dob)) + 1) / 2 * (day(@now) - day(@dob))
                + (sign(day(@dob) - day(@now)) + 1) / 2 * (@days_in_birth_month 
		        - day(@dob) + day(@now))

	Select @years Anos, @months Meses, @days Dias
END
GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[Nutraceuticos] WITH CHECK CHECK CONSTRAINT [FK_Nutraceuticos_Nutrientes];

ALTER TABLE [dbo].[Nutraceuticos] WITH CHECK CHECK CONSTRAINT [FK_Nutraceuticos_AnimaisAuxEspecies];

ALTER TABLE [dbo].[Nutraceuticos] WITH CHECK CHECK CONSTRAINT [FK_Nutraceuticos_PrescricaoAuxTipos];

ALTER TABLE [dbo].[Nutraceuticos] WITH CHECK CHECK CONSTRAINT [FK_Nutraceuticos_PrescricaoAuxTipos1];


GO
PRINT N'Update complete.';


GO
