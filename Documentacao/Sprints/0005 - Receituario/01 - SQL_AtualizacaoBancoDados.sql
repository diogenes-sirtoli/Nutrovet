
GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping Default Constraint [dbo].[DF_ConfigReceituario_Ativo]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] DROP CONSTRAINT [DF_ConfigReceituario_Ativo];


GO
PRINT N'Dropping Default Constraint [dbo].[DF_ConfigReceituario_DataCadastro]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] DROP CONSTRAINT [DF_ConfigReceituario_DataCadastro];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_ConfigReceituario_Pessoas]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] DROP CONSTRAINT [FK_ConfigReceituario_Pessoas];


GO
PRINT N'Starting rebuilding table [dbo].[ConfigReceituario]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ConfigReceituario] (
    [IdPessoa]     INT           NOT NULL,
    [NomeClinica]  VARCHAR (300) NULL,
    [Slogan]       VARCHAR (MAX) NULL,
    [Site]         VARCHAR (300) NULL,
    [Facebook]     VARCHAR (300) NULL,
    [Twitter]      VARCHAR (300) NULL,
    [Instagram]    VARCHAR (300) NULL,
    [Logotipo]     VARCHAR (250) NULL,
    [Assinatura]   VARCHAR (250) NULL,
    [CRMV]         VARCHAR (50)  NULL,
    [CrmvUf]       VARCHAR (10)  NULL,
    [Email]        VARCHAR (200) NULL,
    [Telefone]     VARCHAR (20)  NULL,
    [Celular]      VARCHAR (20)  NULL,
    [Logradouro]   VARCHAR (300) NULL,
    [Logr_Nr]      VARCHAR (10)  NULL,
    [Logr_Compl]   VARCHAR (100) NULL,
    [Logr_Bairro]  VARCHAR (200) NULL,
    [Logr_CEP]     VARCHAR (10)  NULL,
    [Logr_Cidade]  VARCHAR (200) NULL,
    [Logr_UF]      VARCHAR (10)  NULL,
    [Logr_Pais]    VARCHAR (150) NULL,
    [Ativo]        BIT           CONSTRAINT [DF_ConfigReceituario_Ativo] DEFAULT ((1)) NULL,
    [IdOperador]   INT           NULL,
    [IP]           VARCHAR (20)  NULL,
    [DataCadastro] DATETIME      CONSTRAINT [DF_ConfigReceituario_DataCadastro] DEFAULT (getdate()) NULL,
    [Versao]       ROWVERSION    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_ConfigReceituario1] PRIMARY KEY CLUSTERED ([IdPessoa] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ConfigReceituario])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_ConfigReceituario] ([IdPessoa], [NomeClinica], [Slogan], [Site], [Facebook], [Twitter], [Instagram], [Logotipo], [Assinatura], [CRMV], [Email], [Telefone], [Celular], [Logradouro], [Logr_Nr], [Logr_Compl], [Logr_Bairro], [Logr_CEP], [Logr_Cidade], [Logr_UF], [Logr_Pais], [Ativo], [IdOperador], [IP], [DataCadastro])
        SELECT   [IdPessoa],
                 [NomeClinica],
                 [Slogan],
                 [Site],
                 [Facebook],
                 [Twitter],
                 [Instagram],
                 [Logotipo],
                 [Assinatura],
                 [CRMV],
                 [Email],
                 [Telefone],
                 [Celular],
                 [Logradouro],
                 [Logr_Nr],
                 [Logr_Compl],
                 [Logr_Bairro],
                 [Logr_CEP],
                 [Logr_Cidade],
                 [Logr_UF],
                 [Logr_Pais],
                 [Ativo],
                 [IdOperador],
                 [IP],
                 [DataCadastro]
        FROM     [dbo].[ConfigReceituario]
        ORDER BY [IdPessoa] ASC;
    END

DROP TABLE [dbo].[ConfigReceituario];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ConfigReceituario]', N'ConfigReceituario';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_ConfigReceituario1]', N'PK_ConfigReceituario', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Table [dbo].[Receituario]...';


GO
CREATE TABLE [dbo].[Receituario] (
    [IdCardapio]        INT             NOT NULL,
    [IdReceita]         INT             IDENTITY (1, 1) NOT NULL,
    [dTpRec]            INT             NOT NULL,
    [Titulo]            VARCHAR (200)   NOT NULL,
    [Veiculo]           VARCHAR (100)   NULL,
    [QuantVeic]         DECIMAL (10, 3) NULL,
    [Posologia]         VARCHAR (MAX)   NULL,
    [Prescricao]        VARCHAR (MAX)   NULL,
    [InstrucoesReceita] VARCHAR (500)   NULL,
    [DataReceita]       DATE            NULL,
    [LocalReceita]      VARCHAR (300)   NULL,
    [Arquivo]           VARCHAR (500)   NULL,
    [NrReceita]         VARCHAR (20)    NULL,
    [Ativo]             BIT             NULL,
    [IdOperador]        INT             NULL,
    [IP]                VARCHAR (20)    NULL,
    [DataCadastro]      DATETIME        NULL,
    [Versao]            ROWVERSION      NULL,
    CONSTRAINT [PK_RECEITUARIO] PRIMARY KEY CLUSTERED ([IdReceita] ASC)
);


GO
PRINT N'Creating Index [dbo].[Receituario].[FK_Receituario_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Receituario_1]
    ON [dbo].[Receituario]([IdCardapio] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[FK_Receituario_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_Receituario_2]
    ON [dbo].[Receituario]([dTpRec] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[IX_Receituario]...';


GO
CREATE NONCLUSTERED INDEX [IX_Receituario]
    ON [dbo].[Receituario]([NrReceita] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[IX_Receituario_1]...';


GO
CREATE NONCLUSTERED INDEX [IX_Receituario_1]
    ON [dbo].[Receituario]([Arquivo] ASC);


GO
PRINT N'Creating Index [dbo].[Receituario].[IX_Receituario_2]...';


GO
CREATE NONCLUSTERED INDEX [IX_Receituario_2]
    ON [dbo].[Receituario]([DataReceita] ASC);


GO
PRINT N'Creating Table [dbo].[ReceituarioNutrientes]...';


GO
CREATE TABLE [dbo].[ReceituarioNutrientes] (
    [IdReceita]    INT             NOT NULL,
    [IdNutrRec]    INT             IDENTITY (1, 1) NOT NULL,
    [EmReceita]    BIT             NULL,
    [IdNutr]       INT             NOT NULL,
    [Consta]       DECIMAL (10, 3) NULL,
    [Falta]        DECIMAL (10, 3) NULL,
    [Dose]         DECIMAL (10, 3) NULL,
    [IdUnid]       INT             NULL,
    [IdPrescr]     INT             NULL,
    [Ativo]        BIT             NULL,
    [IdOperador]   INT             NULL,
    [IP]           VARCHAR (20)    NULL,
    [DataCadastro] DATETIME        NULL,
    [Versao]       ROWVERSION      NULL,
    CONSTRAINT [PK_ReceituarioNutrientes] PRIMARY KEY CLUSTERED ([IdNutrRec] ASC),
    CONSTRAINT [IX_ReceituarioNutrientes] UNIQUE NONCLUSTERED ([IdReceita] ASC, [IdNutr] ASC)
);


GO
PRINT N'Creating Index [dbo].[ReceituarioNutrientes].[FK_ReceituarioNutrientes_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_ReceituarioNutrientes_1]
    ON [dbo].[ReceituarioNutrientes]([IdReceita] ASC);


GO
PRINT N'Creating Index [dbo].[ReceituarioNutrientes].[FK_ReceituarioNutrientes_2]...';


GO
CREATE NONCLUSTERED INDEX [FK_ReceituarioNutrientes_2]
    ON [dbo].[ReceituarioNutrientes]([IdNutr] ASC);


GO
PRINT N'Creating Index [dbo].[ReceituarioNutrientes].[FK_ReceituarioNutrientes_3]...';


GO
CREATE NONCLUSTERED INDEX [FK_ReceituarioNutrientes_3]
    ON [dbo].[ReceituarioNutrientes]([IdUnid] ASC);


GO
PRINT N'Creating Index [dbo].[ReceituarioNutrientes].[FK_ReceituarioNutrientes_4]...';


GO
CREATE NONCLUSTERED INDEX [FK_ReceituarioNutrientes_4]
    ON [dbo].[ReceituarioNutrientes]([IdPrescr] ASC);


GO
PRINT N'Creating Default Constraint [dbo].[DF_Receituario_Ativo]...';


GO
ALTER TABLE [dbo].[Receituario]
    ADD CONSTRAINT [DF_Receituario_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating Default Constraint [dbo].[DF_Receituario_DataCadastro]...';


GO
ALTER TABLE [dbo].[Receituario]
    ADD CONSTRAINT [DF_Receituario_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating Default Constraint [dbo].[DF_ReceituarioNutrientes_Ativo]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes]
    ADD CONSTRAINT [DF_ReceituarioNutrientes_Ativo] DEFAULT ((1)) FOR [Ativo];


GO
PRINT N'Creating Default Constraint [dbo].[DF_ReceituarioNutrientes_DataCadastro]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes]
    ADD CONSTRAINT [DF_ReceituarioNutrientes_DataCadastro] DEFAULT (getdate()) FOR [DataCadastro];


GO
PRINT N'Creating Foreign Key [dbo].[FK_ConfigReceituario_Pessoas]...';


GO
ALTER TABLE [dbo].[ConfigReceituario] WITH NOCHECK
    ADD CONSTRAINT [FK_ConfigReceituario_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Receituario_Cardapio]...';


GO
ALTER TABLE [dbo].[Receituario] WITH NOCHECK
    ADD CONSTRAINT [FK_Receituario_Cardapio] FOREIGN KEY ([IdCardapio]) REFERENCES [dbo].[Cardapio] ([IdCardapio]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_ReceituarioNutrientes_Receituario]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_ReceituarioNutrientes_Receituario] FOREIGN KEY ([IdReceita]) REFERENCES [dbo].[Receituario] ([IdReceita]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_ReceituarioNutrientes_PrescricaoAuxTipos]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_ReceituarioNutrientes_PrescricaoAuxTipos] FOREIGN KEY ([IdPrescr]) REFERENCES [dbo].[PrescricaoAuxTipos] ([IdPrescr]) ON UPDATE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_ReceituarioNutrientes_Nutrientes]...';


GO
ALTER TABLE [dbo].[ReceituarioNutrientes] WITH NOCHECK
    ADD CONSTRAINT [FK_ReceituarioNutrientes_Nutrientes] FOREIGN KEY ([IdNutr]) REFERENCES [dbo].[Nutrientes] ([IdNutr]) ON UPDATE CASCADE;


GO
PRINT N'Altering Procedure [dbo].[ListarCardapioExigNutr]...';


GO

ALTER PROCEDURE [dbo].[ListarCardapioExigNutr]
	@energia as decimal(18,3), 
	@idTabNutr as int,
	@idEspecie as int,
	@idIndic as int,
	@idGrupo as int
AS
BEGIN
	SET NOCOUNT ON;
    SET QUERY_GOVERNOR_COST_LIMIT 0;

    SELECT	p.IdGrupo, p.Grupo,
			p.IdNutr, (p.Nutriente + ' (' + p.Unidade + ')') Nutriente, 
            p.IdUnid, p.Unidade,
			p.[Mínimo] AS Minimo, p.[Máximo] As Maximo, p.Adequado, 
            p.Recomendado
    From	(
			    SELECT	n.IdGrupo, NutrientesAuxGrupos.Grupo, 
						en.IdNutr1 As IdNutr, n.Nutriente, 
					    (CASE en.IdTpValor 
						    WHEN 1 THEN 'Mínimo' 
						    WHEN 2 THEN 'Máximo' 
						    WHEN 3 THEN 'Adequado' 
						    WHEN 4 THEN 'Recomendado' 
					        END) AS TpValor, 
					        ((@energia * en.Valor1) / 1000) AS ValCalc,
						n.IdUnidade As IdUnid,
		                (Case n.IdUnidade
			                WHEN 1 THEN 'µg' 
			                WHEN 2 THEN 'g' 
			                WHEN 3 THEN 'mg' 
			                WHEN 4 THEN 'Kcal' 
			                WHEN 5 THEN 'UI'
			                WHEN 6 THEN 'Proporção'
			                WHEN 7 THEN 'mcg/Kg' 
			                WHEN 8 THEN 'mg/animal'
			                WHEN 9 THEN 'mg/Kg'
			                WHEN 10 THEN 'UI/Kg'
		                    End) Unidade
			    FROM		ExigenciasNutricionais AS en INNER JOIN
							Nutrientes AS n ON en.IdNutr1 = n.IdNutr INNER JOIN
							ExigenciasNutrAuxIndicacoes AS indic ON 
								en.IdIndic = indic.IdIndic LEFT OUTER JOIN
							NutrientesAuxGrupos ON n.IdGrupo = 
								NutrientesAuxGrupos.IdGrupo
			    WHERE	(en.Ativo = 1) AND (n.ListarEmAlim = 1) AND
					    (en.IdTabNutr = @idTabNutr) AND 
					    (en.IdEspecie = @idEspecie) AND
					    (en.IdIndic = @idIndic) AND
					    (en.IdUnidade1 <> 6) AND
                        (n.IdGrupo = @idGrupo) AND 
						(n.ListarEmAlim = 1) AND 
						(n.Ativo = 1)
		    ) As b
    PIVOT (
		    Sum(b.ValCalc) For b.TpValor in ([Mínimo], [Máximo], 
			    Adequado, Recomendado)
    ) AS P
    Order By P.Nutriente;
END
GO
PRINT N'Creating Procedure [dbo].[ReceituarioListarSelecao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <24/10/2022>
-- Description:	<Listagem de Receitas por Tutor>
-- =============================================
CREATE PROCEDURE [dbo].[ReceituarioListarSelecao] 
	@idTutor as Int,
	@RowspPage as Int,   /*Linhas por página*/
	@PageNumber as Int   /*Número da Página*/
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@idTutor			Int,
						 @RowspPage			Int,
						 @PageNumber		Int';

	SET @SQL = 'SELECT  IdTutor, Tutor, IdAnimal, Animal, Especie, Raca, 
						IdCardapio, Descricao, DtCardapio, PesoAtual, 
						PesoIdeal, IdReceita, Arquivo, NrReceita, dTpRec, 
						TipoReceita, InstrucoesReceita, Titulo, Veiculo, 
						QuantVeic, Posologia, Prescricao, DataReceita, 
						LocalReceita, Ativo, IdOperador, [IP], DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY rec.DataReceita Desc) AS NUMBER,
									c.IdPessoa AS IdTutor, p.Nome AS Tutor, c.IdAnimal, a.Nome AS Animal, 
									e.Especie, r.Raca, c.IdCardapio, c.Descricao, c.DtCardapio, a.PesoAtual, 
									a.PesoIdeal, rec.IdReceita, rec.Arquivo, rec.NrReceita, rec.dTpRec, 
									(Case rec.dTpRec
										When 1 Then ' + '''Suplementação''' + ' 
										When 2 Then ' + '''Nutracêuticos''' + '
										When 3 Then ' + '''Receita_em_branco''' + '
									 End) TipoReceita, rec.InstrucoesReceita,
									rec.Titulo, rec.Veiculo, rec.QuantVeic, rec.Posologia, rec.Prescricao, 
									rec.DataReceita, rec.LocalReceita, rec.Ativo, rec.IdOperador, rec.IP, 
									rec.DataCadastro
							FROM	Tutores INNER JOIN
										Animais AS a ON Tutores.IdTutores = a.IdTutores INNER JOIN
										Pessoas AS p ON Tutores.IdTutor = p.IdPessoa INNER JOIN
										Cardapio AS c ON a.IdAnimal = c.IdAnimal INNER JOIN
										Receituario AS rec ON c.IdCardapio = rec.IdCardapio LEFT OUTER JOIN
										AnimaisAuxEspecies AS e INNER JOIN
										AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON 
											a.IdRaca = r.IdRaca
							WHERE	(c.IdPessoa = @idTutor) AND (rec.Ativo = 1)';

	SET @SQL = @SQL + '	) AS TBL
			WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
					(@PageNumber * @RowspPage)';
	
	--PRINT @SQL
	EXEC sp_Executesql	@SQL, @ParameterDef, @idTutor = @idTutor, 
		@RowspPage = @RowspPage, @PageNumber = @PageNumber
END
GO
PRINT N'Creating Procedure [dbo].[ReceituarioListarSelecaoTotalPaginas]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <27/10/2022>
-- Description:	<Total de Páginas Receituário>
-- =============================================
CREATE PROCEDURE ReceituarioListarSelecaoTotalPaginas
	@idTutor as Int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @ParameterDef NVARCHAR(500);

	SET @ParameterDef = '@idTutor	as Int';

	SET @SQL = 'SELECT	Count(rec.IdReceita) Total
				FROM	Tutores INNER JOIN
						Animais AS a ON Tutores.IdTutores = a.IdTutores INNER JOIN
						Pessoas AS p ON Tutores.IdTutor = p.IdPessoa INNER JOIN
						Cardapio AS c ON a.IdAnimal = c.IdAnimal INNER JOIN
						Receituario AS rec ON c.IdCardapio = rec.IdCardapio LEFT OUTER JOIN
						AnimaisAuxEspecies AS e INNER JOIN
						AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON 
							a.IdRaca = r.IdRaca
				WHERE	(c.IdPessoa = @idTutor) AND (rec.Ativo = 1)';

	--PRINT @SQL
	EXEC sp_Executesql     @SQL, @ParameterDef, @idTutor = @idTutor
END
GO
PRINT N'Creating Procedure [dbo].[ReceituarioNutrientesImpressao]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <16/11/2022>
-- Description:	<Listagem Geral de Nutrietnes da Receita>
-- =============================================
CREATE PROCEDURE [dbo].[ReceituarioNutrientesImpressao]
	@IdReceita as int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    SELECT	rn.IdReceita, r.dTpRec, 
			(Case r.dTpRec
				When 1 Then 'Suplementação' 
				When 2 Then 'Nutracêuticos'
				When 3 Then 'Receita_em_branco'
			 End) TipoReceita, r.Arquivo, r.NrReceita,
			r.Titulo, r.DataReceita, rn.IdNutrRec, rn.EmReceita,
			rn.IdNutr, n.Nutriente, rn.Consta, rn.Falta, rn.Dose, rn.IdUnid,
			(CASE rn.IdUnid 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'mcg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS Unidade,
			rn.IdPrescr, p.Prescricao, rn.Ativo, rn.IdOperador, 
			rn.[IP], rn.DataCadastro
	FROM	Receituario AS r INNER JOIN
				ReceituarioNutrientes AS rn ON r.IdReceita = rn.IdReceita INNER JOIN
				Nutrientes AS n ON rn.IdNutr = n.IdNutr LEFT OUTER JOIN
				PrescricaoAuxTipos AS p ON rn.IdPrescr = p.IdPrescr
	WHERE	(rn.IdReceita = @IdReceita) AND (rn.EmReceita = 1)
	ORDER BY n.Nutriente
END
GO
PRINT N'Creating Procedure [dbo].[ReceituarioNutrientesListar]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael Britto>
-- Create date: <29/10/2022>
-- Description:	<Listagem Geral de Nutrietnes da Receita>
-- =============================================
CREATE PROCEDURE [dbo].[ReceituarioNutrientesListar]
	@IdReceita as int
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

    SELECT	rn.IdReceita, r.dTpRec, 
			(Case r.dTpRec
				When 1 Then 'Suplementação' 
				When 2 Then 'Nutracêuticos'
				When 3 Then 'Receita_em_branco'
			 End) TipoReceita, r.Arquivo, r.NrReceita,
			r.Titulo, r.DataReceita, rn.IdNutrRec, rn.EmReceita,
			rn.IdNutr, n.Nutriente, rn.Consta, rn.Falta, rn.Dose, rn.IdUnid,
			(CASE rn.IdUnid 
				WHEN 1 THEN 'µg' 
				WHEN 2 THEN 'g' 
				WHEN 3 THEN 'mg' 
				WHEN 4 THEN 'Kcal' 
				WHEN 5 THEN 'UI' 
				WHEN 6 THEN 'Proporção' 
				WHEN 7 THEN 'mcg/Kg' 
				WHEN 8 THEN 'mg/animal' 
				WHEN 9 THEN 'mg/Kg' 
				WHEN 10 THEN 'UI/Kg' 
			 END) AS Unidade,
			rn.IdPrescr, p.Prescricao, rn.Ativo, rn.IdOperador, 
			rn.[IP], rn.DataCadastro
	FROM	Receituario AS r INNER JOIN
				ReceituarioNutrientes AS rn ON r.IdReceita = rn.IdReceita INNER JOIN
				Nutrientes AS n ON rn.IdNutr = n.IdNutr LEFT OUTER JOIN
				PrescricaoAuxTipos AS p ON rn.IdPrescr = p.IdPrescr
	WHERE	(rn.IdReceita = @IdReceita)
	ORDER BY n.Nutriente

END
GO
PRINT N'Checking existing data against newly created constraints';


GO

ALTER TABLE [dbo].[ConfigReceituario] WITH CHECK CHECK CONSTRAINT [FK_ConfigReceituario_Pessoas];

ALTER TABLE [dbo].[Receituario] WITH CHECK CHECK CONSTRAINT [FK_Receituario_Cardapio];

ALTER TABLE [dbo].[ReceituarioNutrientes] WITH CHECK CHECK CONSTRAINT [FK_ReceituarioNutrientes_Receituario];

ALTER TABLE [dbo].[ReceituarioNutrientes] WITH CHECK CHECK CONSTRAINT [FK_ReceituarioNutrientes_PrescricaoAuxTipos];

ALTER TABLE [dbo].[ReceituarioNutrientes] WITH CHECK CHECK CONSTRAINT [FK_ReceituarioNutrientes_Nutrientes];


GO
PRINT N'Update complete.';


GO
