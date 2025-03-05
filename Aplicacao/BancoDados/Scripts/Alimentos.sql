USE [Nutrovet]
GO

/****** Object:  Table [dbo].[Alimentos]    Script Date: 26/09/2019 21:27:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Alimentos](
	[IdPessoa] [int] NULL,
	[IdAlimento] [int] IDENTITY(1,1) NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[IdFonte] [int] NOT NULL,
	[IdCateg] [int] NULL,
	[NDB_No] [int] NULL,
	[Alimento] [varchar](150) NOT NULL,
	[Compartilhar] [bit] NULL,
	[fHomologado] [bit] NULL,
	[DataHomol] [date] NULL,
	[Ativo] [bit] NULL,
	[IdOperador] [int] NULL,
	[IP] [varchar](20) NULL,
	[DataCadastro] [datetime] NULL,
	[Versao] [timestamp] NULL,
 CONSTRAINT [PK_Alimentos] PRIMARY KEY CLUSTERED 
(
	[IdAlimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Alimentos] UNIQUE NONCLUSTERED 
(
	[IdFonte] ASC,
	[Alimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Alimentos] ADD  CONSTRAINT [DF_Alimentos_Compartilhar]  DEFAULT ((0)) FOR [Compartilhar]
GO

ALTER TABLE [dbo].[Alimentos] ADD  CONSTRAINT [DF_Alimentos_fHomologado]  DEFAULT ((0)) FOR [fHomologado]
GO

ALTER TABLE [dbo].[Alimentos] ADD  CONSTRAINT [DF_Alimentos_Ativo]  DEFAULT ((1)) FOR [Ativo]
GO

ALTER TABLE [dbo].[Alimentos] ADD  CONSTRAINT [DF_Alimentos_DataCadastro]  DEFAULT (getdate()) FOR [DataCadastro]
GO

ALTER TABLE [dbo].[Alimentos]  WITH CHECK ADD  CONSTRAINT [FK_Alimentos_AlimentosAuxCategorias] FOREIGN KEY([IdCateg])
REFERENCES [dbo].[AlimentosAuxCategorias] ([IdCateg])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Alimentos] CHECK CONSTRAINT [FK_Alimentos_AlimentosAuxCategorias]
GO

ALTER TABLE [dbo].[Alimentos]  WITH CHECK ADD  CONSTRAINT [FK_Alimentos_AlimentosAuxFontes] FOREIGN KEY([IdFonte])
REFERENCES [dbo].[AlimentosAuxFontes] ([IdFonte])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Alimentos] CHECK CONSTRAINT [FK_Alimentos_AlimentosAuxFontes]
GO

ALTER TABLE [dbo].[Alimentos]  WITH CHECK ADD  CONSTRAINT [FK_Alimentos_AlimentosAuxGrupos] FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[AlimentosAuxGrupos] ([IdGrupo])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Alimentos] CHECK CONSTRAINT [FK_Alimentos_AlimentosAuxGrupos]
GO

ALTER TABLE [dbo].[Alimentos]  WITH CHECK ADD  CONSTRAINT [FK_Alimentos_Pessoas] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoas] ([IdPessoa])
GO

ALTER TABLE [dbo].[Alimentos] CHECK CONSTRAINT [FK_Alimentos_Pessoas]
GO

