SELECT	   (Case EN.IdTabNutr
				    WHEN 1 THEN 'FEDIAF' 
				    WHEN 2 THEN 'NRC'
				    WHEN 3 THEN 'AAFCO'
		    End) AS TabelaNutricional, Especies.Especie, 
		    (Case EN.IdTpValor
				    WHEN 1 THEN 'Mínimo' 
				    WHEN 2 THEN 'Máximo'
				    WHEN 3 THEN 'Adequado'
				    WHEN 4 THEN 'Recomendado'
		    End) AS TipoValor, Nutri1.Nutriente AS Nutriente1, 
		    (Case EN.IdUnidade1
				    WHEN 1 THEN 'µg' 
				    WHEN 2 THEN 'g' 
				    WHEN 3 THEN 'mg' 
				    WHEN 4 THEN 'Kcal' 
				    WHEN 5 THEN 'UI'
				    WHEN 6 THEN 'Proporção'
		    End) Unidade1, EN.Proporcao1,
		    EN.Valor1, Nutri2.Nutriente AS Nutriente2, 
		    (Case EN.IdUnidade2
				    WHEN 1 THEN 'µg' 
				    WHEN 2 THEN 'g' 
				    WHEN 3 THEN 'mg' 
				    WHEN 4 THEN 'Kcal' 
				    WHEN 5 THEN 'UI'
				    WHEN 6 THEN 'Proporção'
		    End) Unidade2, EN.Valor2, EN.Proporcao2, 
			((EN.Valor1 * EN.Proporcao1) / 
				(EN.Valor2 * EN.Proporcao2)) AS TotalProporcao
FROM	Nutrientes AS Nutri2 RIGHT OUTER JOIN
            ExigenciasNutricionais AS EN ON Nutri2.IdNutr = 
                EN.IdNutr2 LEFT OUTER JOIN
            ExigenciasNutrAuxIndicacoes AS Indic ON EN.IdIndic = 
                Indic.IdIndic LEFT OUTER JOIN
            AnimaisAuxEspecies AS Especies ON EN.IdEspecie = 
                Especies.IdEspecie LEFT OUTER JOIN
            Nutrientes AS Nutri1 ON EN.IdNutr1 = Nutri1.IdNutr
WHERE		(EN.IdTabNutr = 3 ) and (EN.IdEspecie = 1) AND
			(EN.IdIndic = 5)
ORDER BY TabelaNutricional, Especie, Indicacao, Nutriente1