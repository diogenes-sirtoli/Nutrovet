using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
    public class clExigenciasNutrBll
    {
        public bllRetorno Inserir(ExigenciasNutricionai _nutri)
        {
            bllRetorno ret = _nutri.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_nutri);
                    }

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");
                }
                else
                {
                    return ret;
                }
            }
            catch
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!!");
            }
        }

        public bllRetorno Alterar(ExigenciasNutricionai _nutri)
        {
            bllRetorno ret = _nutri.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_nutri);

                    return bllRetorno.GeraRetorno(true, 
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch
                {
                    return bllRetorno.GeraRetorno(false, 
                        "Não foi possível efetuar a ALTERAÇÃO!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public ExigenciasNutricionai Carregar(int _id)
        {
            CrudDal crud = new CrudDal();
            ExigenciasNutricionai _retorno = new ExigenciasNutricionai();

            string _sql = string.Format(
                @"Select *
                  From ExigenciasNutricionais
                  Where (IdExigNutr = {0})", _id);

            var ret = crud.ExecutarComando<ExigenciasNutricionai>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public TOExigenciasNutricionaisBll CarregarTO(int _id)
        {
            CrudDal crud = new CrudDal();
            TOExigenciasNutricionaisBll _retorno = new TOExigenciasNutricionaisBll();

            string _sql = string.Format(
                @"SELECT	EN.IdExigNutr, EN.IdTabNutr, 
		                    (Case EN.IdTabNutr
				                    WHEN 1 THEN 'FEDIAF' 
				                    WHEN 2 THEN 'NRC'
				                    WHEN 3 THEN 'AAFCO'
		                    End) AS TabelaNutricional, EN.IdEspecie, Especies.Especie, 
		                    EN.IdIndic, Indic.Indicacao, EN.IdTpValor, 
		                    (Case EN.IdTpValor
				                    WHEN 1 THEN 'Mínimo' 
				                    WHEN 2 THEN 'Máximo'
				                    WHEN 3 THEN 'Adequado'
				                    WHEN 4 THEN 'Recomendado'
		                    End) AS TipoValor, EN.IdNutr1, Nutri1.Nutriente AS Nutriente1, 
		                    EN.IdUnidade1, 
		                    (Case EN.IdUnidade1
				                    WHEN 1 THEN 'µg' 
				                    WHEN 2 THEN 'g' 
				                    WHEN 3 THEN 'mg' 
				                    WHEN 4 THEN 'Kcal' 
				                    WHEN 5 THEN 'UI'
				                    WHEN 6 THEN 'Proporção'
		                    End) Unidade1, EN.Proporcao1,
		                    EN.Valor1, EN.IdNutr2, Nutri2.Nutriente AS Nutriente2, 
		                    EN.IdUnidade2, 
		                    (Case EN.IdUnidade2
				                    WHEN 1 THEN 'µg' 
				                    WHEN 2 THEN 'g' 
				                    WHEN 3 THEN 'mg' 
				                    WHEN 4 THEN 'Kcal' 
				                    WHEN 5 THEN 'UI'
				                    WHEN 6 THEN 'Proporção'
		                    End) Unidade2, EN.Valor2, EN.Proporcao2, 
							((EN.Valor1 * EN.Proporcao1) / 
							 (EN.Valor2 * EN.Proporcao2)) AS TotalProporcao,
							EN.Ativo, EN.IdOperador, EN.IP, EN.DataCadastro
                    FROM	Nutrientes AS Nutri2 RIGHT OUTER JOIN
                                ExigenciasNutricionais AS EN ON Nutri2.IdNutr = 
                                    EN.IdNutr2 LEFT OUTER JOIN
                                ExigenciasNutrAuxIndicacoes AS Indic ON EN.IdIndic = 
                                    Indic.IdIndic LEFT OUTER JOIN
                                AnimaisAuxEspecies AS Especies ON EN.IdEspecie = 
                                    Especies.IdEspecie LEFT OUTER JOIN
                                Nutrientes AS Nutri1 ON EN.IdNutr1 = Nutri1.IdNutr
                    Where (EN.IdExigNutr = {0})", _id);

            var ret = crud.ExecutarComando<TOExigenciasNutricionaisBll>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(ExigenciasNutricionai _nutri)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_nutri);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public List<TOExigenciasNutricionaisBll> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	EN.IdExigNutr, EN.IdTabNutr, 
		                    (Case EN.IdTabNutr
				                    WHEN 1 THEN 'FEDIAF' 
				                    WHEN 2 THEN 'NRC'
				                    WHEN 3 THEN 'AAFCO'
		                    End) AS TabelaNutricional, EN.IdEspecie, Especies.Especie, 
		                    EN.IdIndic, Indic.Indicacao, EN.IdTpValor, 
		                    (Case EN.IdTpValor
				                    WHEN 1 THEN 'Mínimo' 
				                    WHEN 2 THEN 'Máximo'
				                    WHEN 3 THEN 'Adequado'
				                    WHEN 4 THEN 'Recomendado'
		                    End) AS TipoValor, EN.IdNutr1, Nutri1.Nutriente AS Nutriente1, 
		                    EN.IdUnidade1, 
		                    (Case EN.IdUnidade1
				                    WHEN 1 THEN 'µg' 
				                    WHEN 2 THEN 'g' 
				                    WHEN 3 THEN 'mg' 
				                    WHEN 4 THEN 'Kcal' 
				                    WHEN 5 THEN 'UI'
				                    WHEN 6 THEN 'Proporção'
		                    End) Unidade1, EN.Proporcao1,
		                    EN.Valor1, EN.IdNutr2, Nutri2.Nutriente AS Nutriente2, 
		                    EN.IdUnidade2, 
		                    (Case EN.IdUnidade2
				                    WHEN 1 THEN 'µg' 
				                    WHEN 2 THEN 'g' 
				                    WHEN 3 THEN 'mg' 
				                    WHEN 4 THEN 'Kcal' 
				                    WHEN 5 THEN 'UI'
				                    WHEN 6 THEN 'Proporção'
		                    End) Unidade2, EN.Valor2, EN.Proporcao2, 
							((EN.Valor1 * EN.Proporcao1) / 
							 (EN.Valor2 * EN.Proporcao2)) AS TotalProporcao,
							EN.Ativo, EN.IdOperador, EN.IP, EN.DataCadastro
                FROM	Nutrientes AS Nutri2 RIGHT OUTER JOIN
                            ExigenciasNutricionais AS EN ON Nutri2.IdNutr = 
                                EN.IdNutr2 LEFT OUTER JOIN
                            ExigenciasNutrAuxIndicacoes AS Indic ON EN.IdIndic = 
                                Indic.IdIndic LEFT OUTER JOIN
                            AnimaisAuxEspecies AS Especies ON EN.IdEspecie = 
                                Especies.IdEspecie LEFT OUTER JOIN
                            Nutrientes AS Nutri1 ON EN.IdNutr1 = Nutri1.IdNutr
                ORDER BY TabelaNutricional, Especie, Indicacao, Nutriente1";

            var lista = crud.Listar<TOExigenciasNutricionaisBll>(_sql);

            return lista.ToList();
        }
        
        public ListItem[] ListarUnidades()
        {
            ListItem[] und = Funcoes.Funcoes.GetEnumList<DominiosBll.Unidades>();

            return und;
        }

        public ListItem[] ListarTabNutr()
        {
            ListItem[] tab = Funcoes.Funcoes.GetEnumList<
                DominiosBll.ExigenciasNutrAuxTabelas>();

            return tab;
        }

        public ListItem[] ListarTpValor()
        {
            ListItem[] tpVal = Funcoes.Funcoes.GetEnumList<
                DominiosBll.ExigenciasNutrAuxTipoValor>();

            return tpVal;
        }

        public List<TOExigNutrTabelasBll> ListarExigNutr(int _idTabNutr,
            int _idEspecie, int _idIndic)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SET QUERY_GOVERNOR_COST_LIMIT 0

                SELECT	p.IdNutr, (p.Nutriente + ' (' + p.Unidade + ')') Nutriente, 
                        p.[Mínimo] AS Minimo, p.[Máximo] As Maximo, p.Adequado, 
                        p.Recomendado
                From	(
			                SELECT	en.IdNutr1 As IdNutr, n.Nutriente, 
					                (CASE en.IdTpValor 
						                WHEN 1 THEN 'Mínimo' 
						                WHEN 2 THEN 'Máximo' 
						                WHEN 3 THEN 'Adequado' 
						                WHEN 4 THEN 'Recomendado' 
					                    END) AS TpValor, 
					                    en.Valor1 As Valor,
                                    (Case n.IdUnidade
			                            WHEN 1 THEN 'µg' 
			                            WHEN 2 THEN 'g' 
			                            WHEN 3 THEN 'mg' 
			                            WHEN 4 THEN 'Kcal' 
			                            WHEN 5 THEN 'UI'
			                            WHEN 6 THEN 'Proporção'
			                            WHEN 7 THEN 'mcg/Kg' 
			                            WHEN 8 THEN 'mg/animal'
			                            WHEN 9 THEN 'mg/Kg'
			                            WHEN 10 THEN 'UI/Kg'
		                                End) Unidade
			                FROM	ExigenciasNutricionais AS en INNER JOIN
						                Nutrientes AS n ON en.IdNutr1 = 
							                n.IdNutr INNER JOIN
						                ExigenciasNutrAuxIndicacoes AS indic ON 
							                en.IdIndic = indic.IdIndic
			                WHERE	(en.Ativo = 1) AND 
					                (en.IdTabNutr = {0}) AND 
					                (en.IdEspecie = {1}) AND
					                (en.IdIndic = {2}) AND
					                (en.IdUnidade1 <> 6)
		                ) As b
                PIVOT (
		                Sum(b.Valor) For b.TpValor in ([Mínimo], [Máximo], 
			                Adequado, Recomendado)
                ) AS P
                Order By P.Nutriente", _idTabNutr, _idEspecie, _idIndic);

            var lista = crud.Listar<TOExigNutrTabelasBll>(_sql);

            return lista.ToList();
        }
    }
}
