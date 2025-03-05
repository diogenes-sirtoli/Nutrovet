SET DATEFORMAT dmy

DECLARE @data AS date, @TotalAnimCad AS INT, @TotalAnimPlano AS INT
Set @data = GetDate()
SET @TotalAnimCad = (SELECT	Count(Animais.IdAnimal) Total
					 FROM	Animais INNER JOIN
								Pessoas ON Animais.IdPessoa = 
									Pessoas.IdPessoa
					 Where Pessoas.IdCliente = 12)
SET @TotalAnimPlano = (SELECT	avp.QtdAnim
					   FROM		AcessosVigenciaPlanos AS avp INNER JOIN
									AcessosVigenciaSituacao AS avs 
										ON avp.IdVigencia = avs.IdVigencia    
					   Where	(avp.IdPessoa = 12) AND 
								(avs.IdSituacao = 3) And 
								(@data Between avp.DtInicial And avp.DtFinal))


Select @TotalAnimPlano - @TotalAnimCad
