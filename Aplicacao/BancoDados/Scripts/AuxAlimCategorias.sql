USE [Nutrovet]
GO

/****** Object:  Table [dbo].[AlimentosAuxCategorias]    Script Date: 26/09/2019 21:25:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AlimentosAuxCategorias](
	[IdCateg] [int] IDENTITY(1,1) NOT NULL,
	[Categoria] [varchar](150) NOT NULL,
	[Ativo] [bit] NULL,
	[IdOperador] [int] NULL,
	[IP] [varchar](20) NULL,
	[DataCadastro] [datetime] NULL,
	[Versao] [timestamp] NULL,
 CONSTRAINT [PK_AlimentosAuxCategorias] PRIMARY KEY CLUSTERED 
(
	[IdCateg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AlimentosAuxCategorias] ADD  CONSTRAINT [DF_AlimentosAuxCategorias_Ativo]  DEFAULT ((1)) FOR [Ativo]
GO

ALTER TABLE [dbo].[AlimentosAuxCategorias] ADD  CONSTRAINT [DF_AlimentosAuxCategorias_DataCadastro]  DEFAULT (getdate()) FOR [DataCadastro]
GO

