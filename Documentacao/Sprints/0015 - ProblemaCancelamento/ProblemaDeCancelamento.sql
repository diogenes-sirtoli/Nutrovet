

SET DATEFORMAT dmy

DECLARE @PageNumber AS INT, @RowspPage AS INT
SET @PageNumber = 1
SET @RowspPage = 9

SELECT	IdPessoa, Nome, Email, Usuario, Senha, Bloqueado, AcessoNoSistema,
		IdVigencia, IdPlano, IdSubscriptionPagarMe, StatusPagarMe, Plano, 
		IdPlanoPagarMe, IdPlanoPagarMeTestes, ValorPlano, DtInicial, DtFinal, 
		IdCartao, NrCartao, CodSeg, dBandeira, Bandeira, IdCupom, NrCupom, 
		ValidadeCupomInicial, ValidadeCupomFinal, fAcessoLiberado, QtdAnimais, 
		dPeriodo, Periodo, Situacao, NaVigencia, Ativo, IdOperador, IP, 
		DataCadastro
FROM    (
			SELECT	ROW_NUMBER() OVER(ORDER BY p.Nome) AS NUMBER,
					avp.IdPessoa, p.Nome, p.Email, p.Usuario, p.Senha, p.Bloqueado, 
					(Select Cast(Count(a.IdPessoa) as bit)
						From Acessos a
						Where a.IdPessoa = avp.IdPessoa) As AcessoNoSistema,
					avp.IdVigencia, 
					avp.IdPlano, pa.IdPlanoPagarMe, pa.IdPlanoPagarMeTestes, 
					avp.IdSubscriptionPagarMe, avp.StatusPagarMe, 
					(CASE pa.dNomePlano 
						When 1 Then 'Básico'
						When 2 Then 'Intermediário'
						When 3 Then 'Completo'
						When 4 Then 'Receituário'
						When 5 Then 'Prontuário'
						END) AS Plano, pa.ValorPlano, avp.DtInicial, avp.DtFinal, 
					avp.IdCartao, cartao.NrCartao, cartao.CodSeg, cartao.dBandeira, 
					(CASE cartao.dBandeira 
							When null Then ''
							When 0 Then ''
							When 1 Then 'Visa'
							When 2 Then 'Master'
							When 3 Then 'Dinners'
							When 4 Then 'Elo'
							When 5 Then 'Hipercard'
							When 6 Then 'American Express' 
						END) AS Bandeira,
						cartao.VencimCartao, cartao.NomeCartao, 
						avp.IdCupom, cupom.NrCumpom AS NrCupom, 
						cupom.DtInicial AS ValidadeCupomInicial, 
						cupom.DtFinal AS ValidadeCupomFinal, 
						cupom.fAcessoLiberado, pa.QtdAnimais, pa.dPeriodo, 
					(CASE pa.dperiodo 
						WHEN 1 THEN 'Mensal' 
						WHEN 2 THEN 'Anual' 
						END) AS Periodo, 
					(SELECT	(CASE s.IdSituacao 
								WHEN 1 THEN 'Julgamento' 
								WHEN 2 THEN 'Pago' 
								WHEN 3 THEN 'Permitido' 
								WHEN 4 THEN 'Cancelado' 
								WHEN 5 THEN 'Pagamento Pendente' 
								WHEN 6 THEN 'Não Pago' 
								WHEN 7 THEN 'Encerrado'
								END)
						FROM	AcessosVigenciaSituacao AS s
						WHERE	(s.IdVigencia = avp.IdVigencia) AND 
							(s.IdVigSit = (SELECT	MAX(sm.IdVigSit)
											FROM     AcessosVigenciaSituacao AS sm
											WHERE	(sm.IdVigencia = avp.IdVigencia)))) AS Situacao,
					(SELECT	COUNT(plano.IdVigencia) AS IdVigencia
						FROM	AcessosVigenciaPlanos AS plano INNER JOIN
								AcessosVigenciaSituacao AS sit ON plano.IdVigencia = sit.IdVigencia
						WHERE	(plano.IdPessoa = p.IdPessoa) AND 
							('16/08/2024' BETWEEN plano.DtInicial AND plano.DtFinal) AND 
							(sit.IdSituacao = 3)) AS NaVigencia, avp.Ativo, avp.IdOperador, 
					avp.IP, avp.DataCadastro
			FROM	AcessosVigenciaPlanos AS avp INNER JOIN
						Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
						PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
						PessoasCartaoCredito AS cartao ON avp.IdCartao = 
							cartao.IdCartao LEFT OUTER JOIN
						AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
			WHERE	(p.IdTpPessoa = 2) AND 
					(avp.IdVigencia = (SELECT	MAX(pl.IdVigencia)
										FROM		AcessosVigenciaPlanos AS pl
										WHERE	(pl.IdPessoa = p.IdPessoa) AND 
												(pl.Ativo = 1)))  AND 
					((p.Nome Like '%giulilima@hotmail.com%'  COLLATE Latin1_General_CI_AI) OR
						(p.Email Like '%giulilima@hotmail.com%'  COLLATE Latin1_General_CI_AI) OR
						(p.Usuario Like '%giulilima@hotmail.com%'  COLLATE Latin1_General_CI_AI))
		) AS TBL
WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
		(@PageNumber * @RowspPage)