Insert Into ExigenciasNutricionais (IdTabNutr, IdEspecie, IdIndic, 
		IdTpValor, IdNutr1, IdUnidade1, Valor1, Ativo)

Select	3 as IdTabNutr, 2 as IdEspecie, 5 as IdIndic, 1 as IdTpValor,
		n.IdNutr, n.IdUnidade, 0 as Valor1, n.Ativo
From	Nutrientes n
Where	(n.IdNutr not in 
			(SELECT	IdNutr1
			 FROM	ExigenciasNutricionais
			 WHERE	(IdTabNutr = 3) AND (IdEspecie = 2) AND (IdIndic = 5)))




