
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=1
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=2
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=3
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=4
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=5
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=6
UPDATE [dbo].[PlanosAssinaturas] SET [IdPlanoPagarMe]=2300994, [IdPlanoPagarMeTestes]=1107393, [dClassif]=2 WHERE [IdPlano]=7
UPDATE [dbo].[PlanosAssinaturas] SET [IdPlanoPagarMe]=2301024, [IdPlanoPagarMeTestes]=1107397, [dClassif]=2 WHERE [IdPlano]=8
UPDATE [dbo].[PlanosAssinaturas] SET [IdPlanoPagarMe]=2301032, [IdPlanoPagarMeTestes]=1107399, [dClassif]=3 WHERE [IdPlano]=9
UPDATE [dbo].[PlanosAssinaturas] SET [IdPlanoPagarMe]=2301051, [IdPlanoPagarMeTestes]=1107403, [dClassif]=3 WHERE [IdPlano]=10
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=12
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=13
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=14
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=15
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=17
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=18
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=20
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=22
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=24
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=26
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=28
UPDATE [dbo].[PlanosAssinaturas] SET [dClassif]=1 WHERE [IdPlano]=30
GO
SET IDENTITY_INSERT [dbo].[PlanosAssinaturas] ON
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (31, 2300995, 1107394, 4, 9.5000, 2, 5, 1, 0, 1, 1, '192.168.0.100', '20220629 18:11:50.633')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (32, 2300996, 1107395, 4, 9.5000, 2, 3, 1, 0, 1, 1, '192.168.0.100', '20220629 18:12:15.117')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (33, 2300999, 1107396, 4, 0.0000, 2, 4, 1, 0, 1, 1, '192.168.0.100', '20220629 18:12:37.497')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (34, 2301030, 1107398, 4, 95.0000, 2, 5, 2, 0, 1, 1, '192.168.0.100', '20220629 18:13:11.803')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (35, 2301033, 1107400, 5, 14.2500, 3, 5, 1, 0, 1, 1, '192.168.0.100', '20220629 18:17:33.917')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (36, 2301046, 1107401, 5, 14.2500, 3, 3, 1, 0, 1, 1, '192.168.0.100', '20220629 18:17:55.067')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (37, 2301050, 1107402, 5, 0.0000, 3, 4, 1, 0, 1, 1, '192.168.0.100', '20220629 18:18:11.890')
INSERT INTO [dbo].[PlanosAssinaturas] ([IdPlano], [IdPlanoPagarMe], [IdPlanoPagarMeTestes], [dNomePlano], [ValorPlano], [dClassif], [dPlanoTp], [dPeriodo], [QtdAnimais], [Ativo], [IdOperador], [IP], [DataCadastro]) VALUES (38, 2301053, 1107404, 5, 142.5000, 3, 5, 2, 0, 1, 1, '192.168.0.100', '20220629 18:18:38.613')
SET IDENTITY_INSERT [dbo].[PlanosAssinaturas] OFF
COMMIT TRANSACTION
