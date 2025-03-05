Update AcessosVigenciaPlanos Set
	IdCartao = c.IdCartao
From PessoasCartaoCredito c INNER JOIN
		AcessosVigenciaPlanos AS p ON 
			p.IdPessoa = c.IdPessoa

