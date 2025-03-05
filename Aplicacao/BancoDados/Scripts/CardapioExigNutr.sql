DECLARE @energia AS decimal(18,3)
SET @energia = 281 /*Vem do sistema*/

SELECT	n.Nutriente, 
		(CASE en.IdTpValor 
			WHEN 1 THEN 'Mínimo' 
			WHEN 2 THEN 'Máximo' 
			WHEN 3 THEN 'Adequado' 
			WHEN 4 THEN 'Recomendado' 
		 END) AS TpValor, 
         en.Valor1, ((@energia * en.Valor1) / 1000) AS ValCalc
FROM	ExigenciasNutricionais AS en INNER JOIN
			Nutrientes AS n ON en.IdNutr1 = n.IdNutr INNER JOIN
            ExigenciasNutrAuxIndicacoes AS indic ON en.IdIndic = 
				indic.IdIndic
WHERE	(en.Ativo = 1) AND 
		(/*Tab Nutr*/en.IdTabNutr = 1) AND 
		(/*Cão ou Gato*/en.IdEspecie = 1) AND
		(/*Indicações - Adulto...*/en.IdIndic = 1) AND
		(en.IdUnidade1 <> 6)
GROUP BY n.Nutriente, en.IdTpValor, en.Valor1