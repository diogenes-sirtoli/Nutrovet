SELECT        c.IdGrupoCompon, AlimentosComponentesAuxGrupos.GrupoComponente, c.IdComponente, c.Componente,
                             (SELECT        Valor
                               FROM            AlimentoComponentes AS ac
                               WHERE        (IdComponente = c.IdComponente) AND (IdAlimento = 5)) AS Valor
FROM            AlimentosAuxComponentes AS c INNER JOIN
                         AlimentosComponentesAuxGrupos ON c.IdGrupoCompon = AlimentosComponentesAuxGrupos.IdGrupoCompon
WHERE        (c.IdGrupoCompon = 5)
ORDER BY c.IdGrupoCompon