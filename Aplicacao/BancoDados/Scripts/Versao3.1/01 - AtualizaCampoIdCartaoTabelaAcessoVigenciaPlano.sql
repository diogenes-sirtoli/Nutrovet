UPDATE	p	SET
	IdCartao = c.IdCartao
FROM	PessoasCartaoCredito AS c INNER JOIN
			AcessosVigenciaPlanos AS p ON p.IdPessoa = c.IdPessoa
Where	p.IdCartao is null