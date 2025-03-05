Update Animais Set
	IdTutores = cte.IdTutores
From (SELECT	Tutores.IdTutores, Animais.IdAnimal
	  FROM		Animais INNER JOIN
					Tutores ON Animais.IdPessoa = Tutores.IdTutor) As cte
	Inner join Animais on animais.IdAnimal = cte.IdAnimal
go

Select *
FRom Animais
go


