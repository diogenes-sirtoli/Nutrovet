DECLARE @energia AS decimal(18,3)
SET @energia = 281 

SELECT	p.Nutriente, p.[M�nimo] AS Minimo, 
		p.[M�ximo] As Maximo, p.Adequado, 
		p.Recomendado
From	(
			SELECT	n.Nutriente, 
					(CASE en.IdTpValor 
						WHEN 1 THEN 'M�nimo' 
						WHEN 2 THEN 'M�ximo' 
						WHEN 3 THEN 'Adequado' 
						WHEN 4 THEN 'Recomendado' 
					 END) AS TpValor, 
					 ((@energia * en.Valor1) / 1000) AS ValCalc
			FROM	ExigenciasNutricionais AS en INNER JOIN
						Nutrientes AS n ON en.IdNutr1 = 
							n.IdNutr INNER JOIN
						ExigenciasNutrAuxIndicacoes AS indic ON 
							en.IdIndic = indic.IdIndic
			WHERE	(en.Ativo = 1) AND 
					(en.IdTabNutr = 3) AND 
					(en.IdEspecie = 1) AND
					(en.IdIndic = 1) AND
					(en.IdUnidade1 <> 6)
		) As b
PIVOT (
		Sum(b.ValCalc) For b.TpValor in ([M�nimo], [M�ximo], 
			Adequado, Recomendado)
) AS P
Order By P.Nutriente