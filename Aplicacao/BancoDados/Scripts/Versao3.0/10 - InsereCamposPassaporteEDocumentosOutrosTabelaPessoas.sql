
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Dropping [dbo].[DF_Pessoas_Ativo]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [DF_Pessoas_Ativo];


GO
PRINT N'Dropping [dbo].[DF_Pessoas_DataCadastro]...';


GO
ALTER TABLE [dbo].[Pessoas] DROP CONSTRAINT [DF_Pessoas_DataCadastro];


GO
PRINT N'Dropping [dbo].[FK_PessoasCartaoCredito_Pessoas]...';


GO
ALTER TABLE [dbo].[PessoasCartaoCredito] DROP CONSTRAINT [FK_PessoasCartaoCredito_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_PessoasDocumentos_Pessoas]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos] DROP CONSTRAINT [FK_PessoasDocumentos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_PessoasDocumentos_Pessoas1]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos] DROP CONSTRAINT [FK_PessoasDocumentos_Pessoas1];


GO
PRINT N'Dropping [dbo].[FK_Acessos_Pessoas]...';


GO
ALTER TABLE [dbo].[Acessos] DROP CONSTRAINT [FK_Acessos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] DROP CONSTRAINT [FK_Cardapio_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Alimentos_Pessoas]...';


GO
ALTER TABLE [dbo].[Alimentos] DROP CONSTRAINT [FK_Alimentos_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Tutores_Pessoas]...';


GO
ALTER TABLE [dbo].[Tutores] DROP CONSTRAINT [FK_Tutores_Pessoas];


GO
PRINT N'Dropping [dbo].[FK_Tutores_Pessoas1]...';


GO
ALTER TABLE [dbo].[Tutores] DROP CONSTRAINT [FK_Tutores_Pessoas1];


GO
PRINT N'Dropping [dbo].[FK_AcessosVigenciaPlanos_Pessoas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] DROP CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];


GO
PRINT N'Starting rebuilding table [dbo].[Pessoas]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Pessoas] (
	[IdPessoa]         INT           IDENTITY (1, 1) NOT NULL,
	[IdTpPessoa]       INT           NOT NULL,
	[Nome]             VARCHAR (300) NOT NULL,
	[dTpEntidade]      INT           NULL,
	[dNacionalidade]   INT           NULL,
	[DataNascimento]   DATE          NULL,
	[RG]               VARCHAR (100) NULL,
	[CPF]              VARCHAR (100) NULL,
	[CNPJ]             VARCHAR (100) NULL,
	[Passaporte]       VARCHAR (200) NULL,
	[DocumentosOutros] VARCHAR (200) NULL,
	[CRVM]             INT           NULL,
	[Email]            VARCHAR (200) NOT NULL,
	[Telefone]         VARCHAR (20)  NULL,
	[Celular]          VARCHAR (20)  NULL,
	[Logotipo]         VARCHAR (250) NULL,
	[Assinatura]       VARCHAR (250) NULL,
	[Logradouro]       VARCHAR (300) NULL,
	[Logr_Nr]          VARCHAR (10)  NULL,
	[Logr_Compl]       VARCHAR (100) NULL,
	[Logr_Bairro]      VARCHAR (200) NULL,
	[Logr_CEP]         VARCHAR (10)  NULL,
	[Logr_Cidade]      VARCHAR (200) NULL,
	[Logr_UF]          VARCHAR (10)  NULL,
	[Logr_Pais]        VARCHAR (150) NULL,
	[Latitude]         VARCHAR (50)  NULL,
	[Longitude]        VARCHAR (50)  NULL,
	[Usuario]          VARCHAR (200) NULL,
	[Senha]            VARCHAR (50)  NULL,
	[Bloqueado]        BIT           NULL,
	[Ativo]            BIT           CONSTRAINT [DF_Pessoas_Ativo] DEFAULT ((1)) NULL,
	[IdOperador]       INT           NULL,
	[IP]               VARCHAR (20)  NULL,
	[DataCadastro]     DATETIME      CONSTRAINT [DF_Pessoas_DataCadastro] DEFAULT (getdate()) NULL,
	[Versao]           ROWVERSION    NULL,
	CONSTRAINT [tmp_ms_xx_constraint_PK_Pessoas1] PRIMARY KEY CLUSTERED ([IdPessoa] ASC),
	CONSTRAINT [tmp_ms_xx_constraint_IX_Pessoas_11] UNIQUE NONCLUSTERED ([Email] ASC)
);

IF EXISTS (SELECT TOP 1 1 
		   FROM   [dbo].[Pessoas])
	BEGIN
		SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Pessoas] ON;
		INSERT INTO [dbo].[tmp_ms_xx_Pessoas] ([IdPessoa], [IdTpPessoa], [Nome], [dTpEntidade], [dNacionalidade], [DataNascimento], [RG], [CPF], [CNPJ], [CRVM], [Email], [Telefone], [Celular], [Logotipo], [Assinatura], [Logradouro], [Logr_Nr], [Logr_Compl], [Logr_Bairro], [Logr_CEP], [Logr_Cidade], [Logr_UF], [Logr_Pais], [Latitude], [Longitude], [Usuario], [Senha], [Bloqueado], [Ativo], [IdOperador], [IP], [DataCadastro])
		SELECT   [IdPessoa],
				 [IdTpPessoa],
				 [Nome],
				 [dTpEntidade],
				 [dNacionalidade],
				 [DataNascimento],
				 [RG],
				 [CPF],
				 [CNPJ],
				 [CRVM],
				 [Email],
				 [Telefone],
				 [Celular],
				 [Logotipo],
				 [Assinatura],
				 [Logradouro],
				 [Logr_Nr],
				 [Logr_Compl],
				 [Logr_Bairro],
				 [Logr_CEP],
				 [Logr_Cidade],
				 [Logr_UF],
				 [Logr_Pais],
				 [Latitude],
				 [Longitude],
				 [Usuario],
				 [Senha],
				 [Bloqueado],
				 [Ativo],
				 [IdOperador],
				 [IP],
				 [DataCadastro]
		FROM     [dbo].[Pessoas]
		ORDER BY [IdPessoa] ASC;
		SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Pessoas] OFF;
	END

DROP TABLE [dbo].[Pessoas];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Pessoas]', N'Pessoas';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Pessoas1]', N'PK_Pessoas', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Pessoas_11]', N'IX_Pessoas_1', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas]
	ON [dbo].[Pessoas]([Nome] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[FK_Pessoas_1]...';


GO
CREATE NONCLUSTERED INDEX [FK_Pessoas_1]
	ON [dbo].[Pessoas]([IdTpPessoa] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas_3]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_3]
	ON [dbo].[Pessoas]([CNPJ] ASC);


GO
PRINT N'Creating [dbo].[Pessoas].[IX_Pessoas_4]...';


GO
CREATE NONCLUSTERED INDEX [IX_Pessoas_4]
	ON [dbo].[Pessoas]([CPF] ASC);


GO
PRINT N'Creating [dbo].[FK_PessoasCartaoCredito_Pessoas]...';


GO
ALTER TABLE [dbo].[PessoasCartaoCredito] WITH NOCHECK
	ADD CONSTRAINT [FK_PessoasCartaoCredito_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_PessoasDocumentos_Pessoas]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos] WITH NOCHECK
	ADD CONSTRAINT [FK_PessoasDocumentos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_PessoasDocumentos_Pessoas1]...';


GO
ALTER TABLE [dbo].[PessoasDocumentos] WITH NOCHECK
	ADD CONSTRAINT [FK_PessoasDocumentos_Pessoas1] FOREIGN KEY ([ComprovanteHomologador]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Acessos_Pessoas]...';


GO
ALTER TABLE [dbo].[Acessos] WITH NOCHECK
	ADD CONSTRAINT [FK_Acessos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Cardapio_Pessoas]...';


GO
ALTER TABLE [dbo].[Cardapio] WITH NOCHECK
	ADD CONSTRAINT [FK_Cardapio_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Alimentos_Pessoas]...';


GO
ALTER TABLE [dbo].[Alimentos] WITH NOCHECK
	ADD CONSTRAINT [FK_Alimentos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_Tutores_Pessoas]...';


GO
ALTER TABLE [dbo].[Tutores] WITH NOCHECK
	ADD CONSTRAINT [FK_Tutores_Pessoas] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Tutores_Pessoas1]...';


GO
ALTER TABLE [dbo].[Tutores] WITH NOCHECK
	ADD CONSTRAINT [FK_Tutores_Pessoas1] FOREIGN KEY ([IdTutor]) REFERENCES [dbo].[Pessoas] ([IdPessoa]);


GO
PRINT N'Creating [dbo].[FK_AcessosVigenciaPlanos_Pessoas]...';


GO
ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH NOCHECK
	ADD CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas] FOREIGN KEY ([IdPessoa]) REFERENCES [dbo].[Pessoas] ([IdPessoa]) ON UPDATE CASCADE;


GO
PRINT N'Altering [dbo].[ListaGeralPessoas]...';


GO
-- =============================================
-- Author:		<Rafael Britto Lobo>
-- Create date: <20/01/2021>
-- Description:	<Lista geral de Pessoas>
-- =============================================
ALTER PROCEDURE [dbo].[ListaGeralPessoas]
AS
BEGIN
SET DATEFORMAT dmy                
SET QUERY_GOVERNOR_COST_LIMIT 0

	SELECT	c.IdPessoa, c.IdTpPessoa AS cIdTpPessoa, 
			(CASE c.IdTpPessoa 
				WHEN 1 THEN 'Administrador do Sistema' 
				WHEN 2 THEN 'Cliente' 
				WHEN 3 THEN 'Tutor' 
			 END) AS cTipoPessoa,
			c.dTpEntidade AS cdTpEntidade, 
			(CASE c.dTpEntidade 
				WHEN 1 THEN 'Pessoa Física' 
				WHEN 2 THEN 'Pessoa Jurídica' 
			 END) AS cTipoEntidade,
			c.Nome AS cNome, c.DataNascimento AS cDtNascim, c.RG AS cRG, c.CPF AS cCPF, 
			c.CNPJ AS cCNPJ, c.Email AS cEMail, c.Passaporte AS cPassaporte, 
			c.DocumentosOutros AS cDocumentosOutros, c.Telefone AS cTelefone, 
			c.Celular AS cCelular, c.Usuario AS cUsuario, c.Senha AS cSenha, 
			c.Bloqueado AS cBloqueado, c.Ativo AS cAtivo, c.IdOperador AS cIdOperador, 
			c.IP AS cIP, c.DataCadastro AS cDataCadastro, ts.IdTutores, ts.IdTutor, 
			t.IdTpPessoa AS tIdTpPessoa, 
			(CASE t.IdTpPessoa 
				WHEN 1 THEN 'Administrador do Sistema' 
				WHEN 2 THEN 'Cliente' 
				WHEN 3 THEN 'Tutor' 
				END) AS tTipoPessoa,
			t.dTpEntidade AS tdTpEntidade, 
			(CASE t.dTpEntidade 
				WHEN 1 THEN 'Pessoa Física' 
				WHEN 2 THEN 'Pessoa Jurídica' 
				END) AS tTipoEntidade,
			t.Nome AS tNome, t.DataNascimento AS tDtNascim, t.RG AS tRG, t.CPF AS tCPF, 
			t.CNPJ AS tCNPJ, t.Email AS tEMail, t.Passaporte AS tPassaporte, 
			t.DocumentosOutros AS tDocumentosOutros, t.Telefone AS tTelefone, 
			t.Celular AS tCelular
	FROM	Pessoas AS t INNER JOIN
			Tutores AS ts ON t.IdPessoa = ts.IdTutor RIGHT OUTER JOIN
			Pessoas AS c ON ts.IdCliente = c.IdPessoa
	WHERE	(c.IdTpPessoa <> 3) AND (ts.IdTutor is not null)
	ORDER BY	cNome, tNome

END
GO
PRINT N'Refreshing [dbo].[ListaGeralAlimentos]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ListaGeralAlimentos]';


GO
PRINT N'Refreshing [dbo].[ListaGeralAlimentosNutrientes]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ListaGeralAlimentosNutrientes]';


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

ALTER TABLE [dbo].[PessoasCartaoCredito] WITH CHECK CHECK CONSTRAINT [FK_PessoasCartaoCredito_Pessoas];

ALTER TABLE [dbo].[PessoasDocumentos] WITH CHECK CHECK CONSTRAINT [FK_PessoasDocumentos_Pessoas];

ALTER TABLE [dbo].[PessoasDocumentos] WITH CHECK CHECK CONSTRAINT [FK_PessoasDocumentos_Pessoas1];

ALTER TABLE [dbo].[Acessos] WITH CHECK CHECK CONSTRAINT [FK_Acessos_Pessoas];

ALTER TABLE [dbo].[Cardapio] WITH CHECK CHECK CONSTRAINT [FK_Cardapio_Pessoas];

ALTER TABLE [dbo].[Alimentos] WITH CHECK CHECK CONSTRAINT [FK_Alimentos_Pessoas];

ALTER TABLE [dbo].[Tutores] WITH CHECK CHECK CONSTRAINT [FK_Tutores_Pessoas];

ALTER TABLE [dbo].[Tutores] WITH CHECK CHECK CONSTRAINT [FK_Tutores_Pessoas1];

ALTER TABLE [dbo].[AcessosVigenciaPlanos] WITH CHECK CHECK CONSTRAINT [FK_AcessosVigenciaPlanos_Pessoas];


GO
PRINT N'Update complete.';


GO
