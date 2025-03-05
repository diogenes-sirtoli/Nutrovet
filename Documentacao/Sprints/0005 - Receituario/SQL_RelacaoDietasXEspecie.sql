SELECT	Cardapio.IdCardapio, Cardapio.IdDieta IdDietaCardap, 
		Dietas.Dieta, AnimaisAuxEspecies.Especie,
		Dietas.IdEspecie DietaEsp, AnimaisAuxRacas.IdEspecie PacienteEsp
FROM	Cardapio INNER JOIN
			Dietas ON Cardapio.IdDieta = Dietas.IdDieta INNER JOIN
            Animais ON Cardapio.IdAnimal = Animais.IdAnimal INNER JOIN
            AnimaisAuxRacas ON Animais.IdRaca = AnimaisAuxRacas.IdRaca INNER JOIN
            AnimaisAuxEspecies ON AnimaisAuxRacas.IdEspecie = AnimaisAuxEspecies.IdEspecie
WHERE	(AnimaisAuxEspecies.IdEspecie = 2) AND (Dietas.IdEspecie = 1) /*AND
		(Cardapio.IdDieta = 1)*/
ORDER By Dietas.IdDieta