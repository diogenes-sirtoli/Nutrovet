SELECT	IdPessoa, Nome, Email, IdSubscriptionPagarMe, StatusPagarMe, Situacao, Plano, 
		ValorPlano, DtInicial, DtFinal, NrCupom, ValidadeCupomInicial, 
		ValidadeCupomFinal, fAcessoLiberado, QtdAnimais, Periodo
FROM    (
			SELECT	avp.IdPessoa, p.Nome, p.Email, avp.IdVigencia, avp.IdPlano, pa.IdPlanoPagarMe, 
					pa.IdPlanoPagarMeTestes, avp.IdSubscriptionPagarMe, avp.StatusPagarMe,
					pa.dNomePlano,
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
						cartao.VencimCartao, cartao.NomeCartao, avp.IdCupom, 
						cupom.NrCumpom AS NrCupom, cupom.DtInicial AS ValidadeCupomInicial, 
						cupom.DtFinal AS ValidadeCupomFinal, cupom.fAcessoLiberado, 
					pa.QtdAnimais, pa.dPeriodo, 
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
						avp.Ativo, avp.IdOperador, avp.IP, avp.DataCadastro
			FROM	AcessosVigenciaPlanos AS avp INNER JOIN
						Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
						PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
						PessoasCartaoCredito AS cartao ON avp.IdCartao = 
							cartao.IdCartao LEFT OUTER JOIN
						AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
						) As tbl
--WHERE	Situacao in ('Pago', 'Permitido', 'Pagamento Pendente')
ORDER BY Nome, DtFinal desc