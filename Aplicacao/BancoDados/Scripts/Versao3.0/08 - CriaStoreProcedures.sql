
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

PRINT N'Creating [dbo].[fncRemoveCaracteresEspeciais]...';


GO
CREATE FUNCTION [dbo].[fncRemoveCaracteresEspeciais](
	@String VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN    
	DECLARE 
		@Result VARCHAR(MAX), 
		@StartingIndex INT = 0
	
	WHILE (1 = 1)
	BEGIN 
		SET @StartingIndex = PATINDEX('%[^a-Z|0-9|^ ]%',@String) 
		
		IF (@StartingIndex <> 0)
			SET @String = REPLACE(@String,SUBSTRING(@String, @StartingIndex,1),'') 
		ELSE 
			BREAK
	END	
	
	SET @Result = REPLACE(@String,'|','')
	
	RETURN @Result

END
GO

PRINT N'Creating [dbo].[ListaGeralAlimentos]...';


GO
-- =============================================
-- Author:		Lobo, Rafael Britto
-- Create date: 22/03/2021
-- Description:	Listagem geral de alimentos
-- =============================================
CREATE PROCEDURE [dbo].[ListaGeralAlimentos] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, 
			f.Fonte, 
			(f.Fonte COLLATE SQL_Latin1_General_CP1251_CI_AS) FontePesquisa, 
			a.IdGrupo, g.GrupoAlimento, 
			(g.GrupoAlimento COLLATE SQL_Latin1_General_CP1251_CI_AS) GrupoAlimentoPesquisa,
			a.IdCateg, c.Categoria,
			(c.Categoria COLLATE SQL_Latin1_General_CP1251_CI_AS) CategoriaPesquisa, 
			a.IdAlimento, a.Alimento, 
			(a.Alimento + ' - ' + f.Fonte) AlimentoFonte,
			((REPLACE(REPLACE(Alimento, ',', ''), '-', ' '))) AS AlimentoTexto,
			(a.Alimento COLLATE SQL_Latin1_General_CP1251_CI_AS) AlimentoPesquisa,
			a.NDB_No, a.Compartilhar, 
			a.fHomologado, a.Ativo, a.IdOperador, a.IP, 
			a.DataCadastro
	FROM	Alimentos AS a LEFT OUTER JOIN
				AlimentosAuxFontes AS f ON a.IdFonte = 
					f.IdFonte LEFT OUTER JOIN
				AlimentosAuxGrupos AS g ON a.IdGrupo = 
					g.IdGrupo LEFT OUTER JOIN
				AlimentosAuxCategorias AS c ON a.IdCateg = 
					c.IdCateg LEFT OUTER JOIN
				Pessoas ON a.IdPessoa = Pessoas.IdPessoa
END
GO
PRINT N'Creating [dbo].[ListaGeralAlimentosNutrientes]...';


GO
-- =============================================
-- Author:		Lobo, Rafael Britto
-- Create date: 22/03/2021
-- Description:	Listagem geral de alimentos e nutrientes
-- =============================================
CREATE PROCEDURE [dbo].[ListaGeralAlimentosNutrientes] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, f.Fonte,
			(f.Fonte COLLATE SQL_Latin1_General_CP1251_CI_AS) FontePesquisa,
			a.IdGrupo, g.GrupoAlimento, 
			(g.GrupoAlimento COLLATE SQL_Latin1_General_CP1251_CI_AS) GrupoAlimentoPesquisa,
			a.IdCateg, c.Categoria,
			(c.Categoria COLLATE SQL_Latin1_General_CP1251_CI_AS) CategoriaPesquisa,
			a.IdAlimento, a.Alimento, 
			(a.Alimento + ' - ' + f.Fonte) AlimentoFonte,
			((REPLACE(REPLACE(Alimento, ',', ''), '-', ' '))) AS AlimentoTexto,
			(a.Alimento COLLATE SQL_Latin1_General_CP1251_CI_AS) AlimentoPesquisa,
			n.IdNutr, n.Nutriente, an.Valor,
			(Case an.IdUnidade
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
			End) Unidade, a.NDB_No, a.Compartilhar, a.fHomologado, 
			a.Ativo, a.IdOperador, a.IP, a.DataCadastro
	FROM	Nutrientes AS n INNER JOIN
				AlimentoNutrientes AS an ON n.IdNutr = 
					an.IdNutr RIGHT OUTER JOIN
				Alimentos AS a ON an.IdAlimento = 
					a.IdAlimento LEFT OUTER JOIN
				AlimentosAuxFontes AS f ON a.IdFonte = 
					f.IdFonte LEFT OUTER JOIN
				AlimentosAuxGrupos AS g ON a.IdGrupo = 
					g.IdGrupo LEFT OUTER JOIN
				AlimentosAuxCategorias AS c ON a.IdCateg = 
					c.IdCateg LEFT OUTER JOIN
				Pessoas ON a.IdPessoa = Pessoas.IdPessoa
END
GO
PRINT N'Creating [dbo].[ListaGeralAnimais]...';


GO
-- =============================================
-- Author:		<Rafael Britto Lobo>
-- Create date: <20/01/2021>
-- Description:	<Lista geral de Animais>
-- =============================================
CREATE PROCEDURE [dbo].[ListaGeralAnimais]
AS
BEGIN
SET DATEFORMAT dmy                
SET QUERY_GOVERNOR_COST_LIMIT 0

	SELECT  c.IdPessoa, c.Nome AS cNome, a.IdTutores, Tutores.IdTutor, 
			t.Nome AS tNome, a.IdAnimal, a.Nome AS Animal, r.IdEspecie, 
			e.Especie, a.IdRaca, r.Raca, a.Sexo AS IdSexo, 
			(CASE a.Sexo 
				WHEN 1 THEN 'Macho' 
				WHEN 2 THEN 'Fêmea' 
				END) AS Sexo, a.DtNascim, a.PesoAtual, a.PesoIdeal, 
			r.IdadeAdulta, r.CrescInicial, r.CrescFinal, a.RecalcularNEM, 
			a.Observacoes, a.Ativo, a.IdOperador, a.IP, a.DataCadastro,
			a.Versao
	FROM    Pessoas AS c INNER JOIN
				Animais AS a INNER JOIN
				Pessoas AS t INNER JOIN
				Tutores ON t.IdPessoa = Tutores.IdTutor ON a.IdTutores = 
					Tutores.IdTutores ON c.IdPessoa = 
					Tutores.IdCliente LEFT OUTER JOIN
				AnimaisAuxEspecies AS e INNER JOIN
				AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie ON a.IdRaca = 
					r.IdRaca
	ORDER BY cNome, Animal

END
GO
PRINT N'Creating [dbo].[ListaGeralNutrientes]...';


GO
-- =============================================
-- Author:		Lobo, Rafael Britto
-- Create date: 22/03/2021
-- Description:	Listagem geral de alimentos
-- =============================================
CREATE PROCEDURE ListaGeralNutrientes 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, n.Referencia, 
			n.ValMin, n.ValMax, n.IdUnidade, 
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
			End) Unidade,
			n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
			n.IdOperador, n.IP, n.DataCadastro
	FROM	Nutrientes AS n INNER JOIN
				NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
END
GO
PRINT N'Creating [dbo].[ListaGeralPessoas]...';


GO
-- =============================================
-- Author:		<Rafael Britto Lobo>
-- Create date: <20/01/2021>
-- Description:	<Lista geral de Pessoas>
-- =============================================
CREATE PROCEDURE ListaGeralPessoas
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
			c.CNPJ AS cCNPJ, c.Email AS cEMail, c.Telefone AS cTelefone, 
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
			t.CNPJ AS tCNPJ, t.Email AS tEMail, t.Telefone AS tTelefone, 
			t.Celular AS tCelular
	FROM	Pessoas AS t INNER JOIN
			Tutores AS ts ON t.IdPessoa = ts.IdTutor RIGHT OUTER JOIN
			Pessoas AS c ON ts.IdCliente = c.IdPessoa
	WHERE	(c.IdTpPessoa <> 3) AND (ts.IdTutor is not null)
	ORDER BY	cNome, tNome

END
GO
PRINT N'Creating [dbo].[ModificaTutorParaCliente]...';


GO
-- =============================================
-- Author:		<Lobo, Rafael>
-- Create date: <30/03/2021>
-- Description:	<Processo de Atualização de Tutor para Cliente>
-- =============================================
CREATE PROCEDURE [dbo].[ModificaTutorParaCliente] 
	@nome as varchar(300),
	@eMail as varchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	SET QUERY_GOVERNOR_COST_LIMIT 0;
	SET DATEFORMAT dmy;

	Declare @idTutores int;
	Declare @idTutor int;
	Declare @ehTutor int;
	Declare @result bit;
	Declare @mensagem varchar(350);

	SELECT @ehTutor =	COUNT(Tutores.IdTutores)
						FROM    Tutores INNER JOIN
							Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
						WHERE   (Tutores.Ativo = 1) AND 
								((t.Nome = @nome) OR (t.Email = @eMail));
	SELECT @idTutor =	Tutores.IdTutor
						FROM    Tutores INNER JOIN
							Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
						WHERE   (Tutores.Ativo = 1) AND 
								((t.Nome = @nome) OR (t.Email = @eMail));
								
	If	((@ehTutor > 0) AND (@idTutor > 0)) 
		Begin
			/* Início do Update na Tabela Pessoas */
			BEGIN TRANSACTION;  
			BEGIN TRY  
				UPDATE Pessoas Set
					IdTpPessoa = 2
				WHERE (IdPessoa = @idTutor);

				/* Início do Update na Tabela Animais */
				BEGIN TRANSACTION;  
				BEGIN TRY  
					UPDATE Animais Set
						IdTutores = (Select top 1 IdTutores
									 From Tutores
									 Where IdTutor = @idTutor
									 Order By IdTutores)
					Where (IdTutores in (Select IdTutores
										 From Tutores
										 Where (IdTutor = @idTutor)));

					/* Início da Exclusão na Tabela Tutores */
					BEGIN TRANSACTION;  
					BEGIN TRY  
						Delete	From Tutores
						Where	(IdTutor = @idTutor) And 
								(IdTutores <>  (Select top 1 IdTutores
												From Tutores
												Where IdTutor = @idTutor
												Order By IdTutores));

						/* Início do Update na Tabela Tutores */
						BEGIN TRANSACTION;
						BEGIN TRY  
							UPDATE Tutores Set
								IdCliente = @idTutor
							WHERE (IdTutor = @idTutor);

							/* Início do Update na Tabela Cardápios */
							BEGIN TRANSACTION;
							BEGIN TRY  
								UPDATE Cardapio Set
									IdPessoa = @idTutor
								WHERE (IdAnimal IN (Select	a.IdAnimal
													FROM	Animais a
													WHERE (a.IdTutores in (Select IdTutores
																		   From Tutores
																		   Where IdTutor = @idTutor))));
						
								/* Início do INSERT na Tabela Acessos */
								BEGIN TRANSACTION;
								BEGIN TRY  
									INSERT INTO Acessos
										(IdPessoa, IdAcFunc, Inserir, Alterar, Excluir, Consultar, 
											AcoesEspeciais, Relatorios, SuperUser, TermoUso, Ativo, 
											IdOperador)
									VALUES	(@idTutor, 3, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1)
								END TRY  
								BEGIN CATCH  
									SELECT   
										ERROR_NUMBER() AS ErrorNumber,
										ERROR_SEVERITY() AS ErrorSeverity,
										ERROR_STATE() AS ErrorState,
										ERROR_PROCEDURE() AS ErrorProcedure,
										ERROR_LINE() AS ErrorLine,
										ERROR_MESSAGE() AS ErrorMessage;  
  
									IF @@TRANCOUNT > 0
										BEGIN
											SELECT  @result = 0;
											SELECT  @mensagem = 'Erro ao INSERIR na Tabela Acessos!';

											ROLLBACK TRANSACTION;

											SELECT @result As Retorno, @mensagem As Mensagem;
										END
								END CATCH;  
  
								IF @@TRANCOUNT > 0  
									COMMIT TRANSACTION;
								/* Fim do INSERT na Tabela Acessos */
							END TRY  
							BEGIN CATCH  
								SELECT   
									ERROR_NUMBER() AS ErrorNumber,
									ERROR_SEVERITY() AS ErrorSeverity,
									ERROR_STATE() AS ErrorState,
									ERROR_PROCEDURE() AS ErrorProcedure,
									ERROR_LINE() AS ErrorLine,
									ERROR_MESSAGE() AS ErrorMessage;  
  
								IF @@TRANCOUNT > 0
									BEGIN
										SELECT  @result = 0;
										SELECT  @mensagem = 'Erro ao Atualizar a Tabela Cardápios!';

										ROLLBACK TRANSACTION;

										SELECT @result As Retorno, @mensagem As Mensagem;
									END
							END CATCH;  
  
							IF @@TRANCOUNT > 0  
								COMMIT TRANSACTION;
							/* Fim Update na Tabela Cardápios */
						END TRY  
						BEGIN CATCH  
							SELECT   
								ERROR_NUMBER() AS ErrorNumber,
								ERROR_SEVERITY() AS ErrorSeverity,
								ERROR_STATE() AS ErrorState,
								ERROR_PROCEDURE() AS ErrorProcedure,
								ERROR_LINE() AS ErrorLine,
								ERROR_MESSAGE() AS ErrorMessage;  
  
							IF @@TRANCOUNT > 0 
								BEGIN
									SELECT  @result = 0;
									SELECT  @mensagem = 'Erro ao Atualizar a Tabela Tutores!';

									ROLLBACK TRANSACTION;

									SELECT @result As Retorno, @mensagem As Mensagem;
								END
						END CATCH;  
  
						IF @@TRANCOUNT > 0  
							COMMIT TRANSACTION;
						/* Fim Update na Tabela Tutores */
					END TRY  
					BEGIN CATCH  
						SELECT   
							ERROR_NUMBER() AS ErrorNumber,
							ERROR_SEVERITY() AS ErrorSeverity,
							ERROR_STATE() AS ErrorState,
							ERROR_PROCEDURE() AS ErrorProcedure,
							ERROR_LINE() AS ErrorLine,
							ERROR_MESSAGE() AS ErrorMessage;  
  
						IF @@TRANCOUNT > 0 
							BEGIN
								SELECT  @result = 0;
								SELECT  @mensagem = 'Erro ao Excluir na Tabela Tutores!';

								ROLLBACK TRANSACTION;

								SELECT @result As Retorno, @mensagem As Mensagem;
							END
					END CATCH;  
  
					IF @@TRANCOUNT > 0  
						COMMIT TRANSACTION;
					/* Fim da Exclusão na Tabela Tutores */
				END TRY  
				BEGIN CATCH  
					SELECT   
						ERROR_NUMBER() AS ErrorNumber,
						ERROR_SEVERITY() AS ErrorSeverity,
						ERROR_STATE() AS ErrorState,
						ERROR_PROCEDURE() AS ErrorProcedure,
						ERROR_LINE() AS ErrorLine,
						ERROR_MESSAGE() AS ErrorMessage;  
  
					IF @@TRANCOUNT > 0 
						BEGIN
							SELECT  @result = 0;
							SELECT  @mensagem = 'Erro ao Atualizar a Tabela Animais!';

							ROLLBACK TRANSACTION;

							SELECT @result As Retorno, @mensagem As Mensagem;
						END
				END CATCH;  
  
				IF @@TRANCOUNT > 0  
					COMMIT TRANSACTION;
				/* Fim Update na Tabela animais */
			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber,
					ERROR_SEVERITY() AS ErrorSeverity,
					ERROR_STATE() AS ErrorState,
					ERROR_PROCEDURE() AS ErrorProcedure,
					ERROR_LINE() AS ErrorLine,
					ERROR_MESSAGE() AS ErrorMessage;  
  
				IF @@TRANCOUNT > 0 
					BEGIN
						SELECT  @result = 0;
						SELECT  @mensagem = 'Erro ao Atualizar a Tabela Pessoas!';

						ROLLBACK TRANSACTION;

						SELECT @result As Retorno, @mensagem As Mensagem;
					END
			END CATCH;  
  
			IF @@TRANCOUNT > 0 
				BEGIN
					SELECT  @result = 1;
					SELECT  @mensagem = 'Tutor convertido em Cliente com Sucesso!';

					COMMIT TRANSACTION;

					SELECT @result As Retorno, @mensagem As Mensagem;
				END
			/* Fim Update na Tabela Pessoas */
		End
	Else
		Begin
			SELECT  @result = 0;
			SELECT  @mensagem = 'Pessoa Selecionada Não é um Tutor';

			SELECT @result As Retorno, @mensagem As Mensagem;
		End
END
GO
PRINT N'Update complete.';


GO
