Insert Into Tutores(IdCliente, IdTutor, Ativo, IdOperador)
SELECT	Tutor.IdCliente, Tutor.IdPessoa AS IdTutor, 1, 9
FROM	Pessoas AS Assinante INNER JOIN
			Pessoas AS Tutor ON Assinante.IdPessoa = Tutor.IdCliente
ORDER By IdCliente, IdTutor

