SELECT	p.Nome, p.DataNascimento, p.Email, p.Telefone, p.Celular, 
		(Case p.Bloqueado
			When 1 Then 'Sim'
			When 0 Then 'Não'
		 End) Bloqueado, 
		plano.Plano, 
		(Case vigencia.Periodo
			When 1 Then 'Mensal'
			When 2 Then 'Anual'
		 End) Periodo,vigencia.DtInicial, vigencia.DtFinal, 
		(Case vigencia.QtdAnim
			When 7000000 Then 'Ilimitado'
			When 700000 Then 'Ilimitado'
			Else Cast(vigencia.QtdAnim as varchar)
		 End) QtdAnimal, cupom.NrCumpom, 
		 (Case cupom.fAcessoLiberado
			When 1 Then 'Sim'
			Else ' '
		  End) CupomLiberado
FROM	AcessosVigenciaPlanos AS vigencia INNER JOIN
			Pessoas AS p ON vigencia.IdPessoa = p.IdPessoa INNER JOIN
            PlanosAssinaturas AS plano ON vigencia.IdPlano = 
				plano.IdPlano LEFT OUTER JOIN
            AcessosVigenciaCupomDesconto AS cupom ON vigencia.IdCupom = 
				cupom.IdCupom
WHERE	(p.IdTpPessoa = 2) AND (p.Ativo = 1) AND (p.Bloqueado = 0)
ORDER BY p.Nome