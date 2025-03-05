SELECT  b.IdBiblio, b.IdSecao, s.Secao, b.NomeArq, 
        b.Descricao, b.Autor, b.Ano, b.Caminho, 
        b.Ordenador, b.Ativo, b.IdOperador, b.IP, 
        b.DataCadastro
FROM    Biblioteca AS b LEFT OUTER JOIN
            BibliotecaAuxSecoes AS s ON b.IdSecao = s.IdSecao
WHERE   (b.Ano = 'Maria') OR
        (b.Autor Like '%Maria%' COLLATE Latin1_General_CI_AI) OR
        (b.Descricao Like '%Maria%' COLLATE Latin1_General_CI_AI)
ORDER BY b.Ano DESC