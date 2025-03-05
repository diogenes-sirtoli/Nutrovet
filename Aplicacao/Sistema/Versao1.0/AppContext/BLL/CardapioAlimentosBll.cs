using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
    public class clCardapioAlimentosBll
    {
        public bllRetorno Inserir(CardapiosAlimento _cardAlim)
        {
            bllRetorno ret = _cardAlim.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_cardAlim);

                    return bllRetorno.GeraRetorno(true, 
                        "INSERÇÃO efetuada com sucesso!!!");

                }
                catch (Exception err)
                {
                    return bllRetorno.GeraRetorno(false, 
                        "Não foi possível efetuar a INSERÇÃO!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno Alterar(CardapiosAlimento _cardAlim)
        {
            bllRetorno ret = _cardAlim.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_cardAlim);

                    return bllRetorno.GeraRetorno(true, 
                        "ALTERAÇÃO efetuada com sucesso!!!");

                }
                catch (Exception err)
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

        public bllRetorno Excluir(CardapiosAlimento _cardAlim)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_cardAlim);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false, 
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public CardapiosAlimento Carregar(int _idCardAlim)
        {
            CrudDal crud = new CrudDal();

            CardapiosAlimento _cardAlim = null;

            var consulta = crud.ExecutarComando<CardapiosAlimento>(string.Format(@"
                Select *
                From CardapiosAlimentos
                Where (IdCardapAlim = {0})", _idCardAlim));

            foreach (var item in consulta)
            {
                _cardAlim = item;
            }

            return _cardAlim;
        }

        public List<TOCardapioAlimentosBll> ListarTO(int _idCard)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT  CardapiosAlimentos.IdCardapio, CardapiosAlimentos.IdCardapAlim, 
                        CardapiosAlimentos.IdAlimento, 
                        (Alimentos.Alimento + ' - ' + AlimentosAuxFontes.Fonte) Alimento, 
                        CardapiosAlimentos.Quant, CardapiosAlimentos.Ativo, 
                        CardapiosAlimentos.IdOperador, CardapiosAlimentos.IP, 
                        CardapiosAlimentos.DataCadastro
                FROM    Alimentos INNER JOIN
                            CardapiosAlimentos ON Alimentos.IdAlimento = 
                            CardapiosAlimentos.IdAlimento LEFT OUTER JOIN
                            AlimentosAuxFontes ON Alimentos.IdFonte = 
                            AlimentosAuxFontes.IdFonte
                WHERE   (CardapiosAlimentos.IdCardapio = {0})
                ORDER BY    Alimentos.Alimento",
                _idCard);

            var lista = crud.Listar<TOCardapioAlimentosBll>(_sql);

            return lista.ToList();
        }

        public List<TOCardapioResumoBll> ListarResumoCardapio(int _idCard)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SET QUERY_GOVERNOR_COST_LIMIT 0

                SELECT	p.Alimento, p.Quant, p.[Carboidratos] AS Carboidrato, 
		                p.[Proteína] As Proteina, p.[Gorduras Totais] As Gordura, 
		                p.[Fibra Alimentar] As Fibras, p.Energia, p.Umidade
                From	(
			                SELECT	Alimentos.Alimento, CardapiosAlimentos.Quant, 
					                Nutrientes.Nutriente, AlimentoNutrientes.Valor
			                FROM	AlimentoNutrientes INNER JOIN
					                Alimentos ON AlimentoNutrientes.IdAlimento = 
						                Alimentos.IdAlimento INNER JOIN
					                CardapiosAlimentos ON Alimentos.IdAlimento = 
						                CardapiosAlimentos.IdAlimento INNER JOIN
					                Nutrientes ON AlimentoNutrientes.IdNutr = 
						                Nutrientes.IdNutr
			                WHERE	(CardapiosAlimentos.IdCardapio = {0}) AND 
					                (Nutrientes.IdNutr Between 1 and 6)
			                GROUP BY	Alimentos.Alimento, CardapiosAlimentos.Quant, 
						                Nutrientes.Nutriente, AlimentoNutrientes.Valor
		                ) As b
                PIVOT (
		                Sum(b.valor) For b.Nutriente in ([Carboidratos], [Proteína], 
							[Gorduras Totais], [Fibra Alimentar], [Energia], [Umidade])
                ) AS P
                Order By P.Alimento", _idCard);

            var lista = crud.Listar<TOCardapioResumoBll>(_sql);

            return lista.ToList();
        }

        public List<BICardapioAlimentosBll> BICardapioAlimento(int _idCard)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                Declare @total decimal

                SET @total = (SELECT    SUM(ca.Quant) AS TOTAL
                              FROM      CardapiosAlimentos AS ca
                              WHERE     ca.IdCardapio = {0})

                Set @total = (Select	Sum(ca.Quant) AS Total
                              From      CardapiosAlimentos AS ca
                              Where 	ca.IdCardapio = {0})

                SELECT	c.Categoria, SUM(ca.Quant) AS SomaQuant, 
		                ((SUM(ca.Quant) * 100) / @total) AS PercentQuant
                FROM	Alimentos AS a INNER JOIN
			                CardapiosAlimentos AS ca ON a.IdAlimento = 
				                ca.IdAlimento LEFT OUTER JOIN
			                AlimentosAuxCategorias AS c ON a.IdCateg = c.IdCateg
                WHERE	(ca.IdCardapio = {0})
                GROUP BY c.Categoria
                ORDER BY c.Categoria", _idCard);

            var lista = crud.Listar<BICardapioAlimentosBll>(_sql);

            return lista.ToList();
        }

        public List<TOExigNutrTabelasBll> ListarCardapioExigNutr(decimal _energia,
            int _idCardapio, int _idTabNutr, int _idEspecie, int _idIndic, 
            int _idGrupo)
        {
            CrudDal crud = new CrudDal();
            List<TOExigNutrTabelasBll> _retLista = new List<TOExigNutrTabelasBll>();
            TOExigNutrTabelasBll _retItem;

            string _energy = Funcoes.Funcoes.ConvertePara.String(_energia).Replace(",", ".");

            string _sql = string.Format(@"
                Execute ListarCardapioExigNutr {0}, {1}, {2}, {3}, {4}", _energy, 
                _idTabNutr, _idEspecie, _idIndic, _idGrupo);

            var lista = crud.Listar<TOExigNutrTabelasBll>(_sql);

            foreach (TOExigNutrTabelasBll item in lista)
            {
                decimal _totalNutr = SomaNutrienteDoCardapio(_idCardapio, 
                    Funcoes.Funcoes.ConvertePara.Int(item.IdNutr));

                _retItem = item;
                _retItem.Falta = 0;
                _retItem.Sobra = 0;
                _retItem.EmCardapio = _totalNutr;

                if ((_totalNutr < Funcoes.Funcoes.ConvertePara.Decimal(item.Minimo)) &&
                    (Funcoes.Funcoes.ConvertePara.Decimal(item.Minimo) > 0))
                {
                    _retItem.Falta = Funcoes.Funcoes.ConvertePara.Decimal(item.Minimo) -
                        _totalNutr;
                }
                else if ((_totalNutr > Funcoes.Funcoes.ConvertePara.Decimal(item.Maximo)) &&
                         (Funcoes.Funcoes.ConvertePara.Decimal(item.Maximo) > 0))
                {
                    _retItem.Sobra = _totalNutr - 
                        Funcoes.Funcoes.ConvertePara.Decimal(item.Maximo);
                }

                _retLista.Add(_retItem);
            }

            return _retLista.ToList();
        }

        private decimal SomaNutrienteDoCardapio(int _idCardapio, int _idNutr)
        {
            CrudDal crud = new CrudDal();
            decimal _retorno = 0;

            string _sql = string.Format(@"
                SELECT	Coalesce((SUM(an.Valor * ca.Quant) / 100), 0) AS total
                FROM	AlimentoNutrientes AS an INNER JOIN
			                Alimentos AS a ON an.IdAlimento = a.IdAlimento INNER JOIN
                            CardapiosAlimentos AS ca ON a.IdAlimento = 
				                ca.IdAlimento INNER JOIN
                            Nutrientes AS n ON an.IdNutr = n.IdNutr
                WHERE	(ca.IdCardapio = {0}) AND (an.IdNutr = {1})",
                        _idCardapio, _idNutr);

            var _total = crud.ExecutarComandoTipoBasico<decimal>(_sql);

            foreach (decimal? item in _total)
            {
                _retorno = Funcoes.Funcoes.ConvertePara.Decimal(item);
            }

            return _retorno;
        }

        public decimal SomaQuantidadesDoCardapio(int _idCardapio)
        {
            CrudDal crud = new CrudDal();
            decimal _total = 0;

            string _sql = string.Format(@"
                Select	Sum(ca.Quant) Total
                From	CardapiosAlimentos ca
                Where	(ca.IdCardapio = {0})",
                _idCardapio);

            foreach (var item in crud.ExecutarComandoTipoBasico<decimal?>(_sql))
            {
                _total = Funcoes.Funcoes.ConvertePara.Decimal(item);
            }
            
            return _total;
        }

        public List<TOExigNutrTabelasBll> MediaDosCardapios(ArrayList conjuntoExigNutr)
        {
            List<TOExigNutrTabelasBll> _listaRetorno = new List<TOExigNutrTabelasBll>();
            TOExigNutrTabelasBll _itemRetorno;
            int Total = conjuntoExigNutr.Count;

            foreach (List<TOExigNutrTabelasBll> listaExigNutr in conjuntoExigNutr)
            {
                foreach (var item in listaExigNutr.OrderBy(o => o.IdNutr))
                {
                    if (_listaRetorno.Count < listaExigNutr.Count)
                    {//faz o insert no objeto
                        _itemRetorno = new TOExigNutrTabelasBll();

                        _itemRetorno.IdGrupo = item.IdGrupo;
                        _itemRetorno.Grupo = item.Grupo;
                        _itemRetorno.IdNutr = item.IdNutr;
                        _itemRetorno.Nutriente = item.Nutriente;
                        _itemRetorno.Minimo = item.Minimo;
                        _itemRetorno.Maximo = item.Maximo;
                        _itemRetorno.Adequado = item.Adequado;
                        _itemRetorno.Recomendado = item.Recomendado;
                        _itemRetorno.EmCardapio = item.EmCardapio;
                        _itemRetorno.Falta = item.Falta;
                        _itemRetorno.Sobra = item.Sobra;
                        _itemRetorno.Valor = item.Valor;
                        _itemRetorno.ValorCalc = item.ValorCalc;

                        _listaRetorno.Add(_itemRetorno);
                    }
                    else
                    {//faz o update no objeto
                        _itemRetorno = _listaRetorno.FirstOrDefault(q => q.IdNutr == item.IdNutr);

                        _itemRetorno.IdGrupo = item.IdGrupo;
                        _itemRetorno.Grupo = item.Grupo;
                        _itemRetorno.Minimo += item.Minimo;
                        _itemRetorno.Maximo += item.Maximo;
                        _itemRetorno.Adequado += item.Adequado;
                        _itemRetorno.Recomendado += item.Recomendado;
                        _itemRetorno.EmCardapio += item.EmCardapio;
                        _itemRetorno.Falta += item.Falta;
                        _itemRetorno.Sobra += item.Sobra;
                        _itemRetorno.Valor += item.Valor;
                        _itemRetorno.ValorCalc += item.ValorCalc;
                    }
                }
            }

            //faz a média das somas
            foreach (TOExigNutrTabelasBll _item in _listaRetorno)
            {
                _item.Minimo = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Minimo) > 0 ?
                    _item.Minimo / Total : _item.Minimo);
                _item.Maximo = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Maximo) > 0 ?
                    _item.Maximo / Total : _item.Maximo);
                _item.Adequado = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Adequado) > 0 ?
                    _item.Adequado / Total : _item.Adequado);
                _item.Recomendado = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Recomendado) > 0 ?
                    _item.Recomendado / Total : _item.Recomendado);
                _item.EmCardapio = (Funcoes.Funcoes.ConvertePara.Decimal(_item.EmCardapio) > 0 ?
                    _item.EmCardapio / Total : _item.EmCardapio);


                //antes
                //_item.Falta = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Falta) > 0 ?
                //    _item.Falta / Total : _item.Falta);
                //_item.Sobra = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Sobra) > 0 ?
                //    _item.Sobra / Total : _item.Sobra);

                //depois
                _item.Falta = null;
                _item.Sobra = null;

                if ((_item.EmCardapio < Funcoes.Funcoes.ConvertePara.Decimal(_item.Minimo)) &&
                    (Funcoes.Funcoes.ConvertePara.Decimal(_item.Minimo) > 0))
                {
                    _item.Falta = Funcoes.Funcoes.ConvertePara.Decimal(_item.Minimo) -
                       _item.EmCardapio;
                }
                else if ((_item.EmCardapio > Funcoes.Funcoes.ConvertePara.Decimal(_item.Maximo)) &&
                         (Funcoes.Funcoes.ConvertePara.Decimal(_item.Maximo) > 0))
                {
                    _item.Sobra = _item.EmCardapio -
                        Funcoes.Funcoes.ConvertePara.Decimal(_item.Maximo);
                }

                _item.Valor = (Funcoes.Funcoes.ConvertePara.Decimal(_item.Valor) > 0 ?
                    _item.Valor / Total : _item.Valor);
                _item.ValorCalc = (Funcoes.Funcoes.ConvertePara.Decimal(_item.ValorCalc) > 0 ?
                    _item.ValorCalc / Total : _item.ValorCalc);
            }

            return _listaRetorno;
        }

        public List<TOExigNutrTabelasBll> GeraRelatorio(List<TOExigNutrTabelasBll> _listaAtual,
            List<TOExigNutrTabelasBll> _listParaAdicionar)
        {
            if (_listaAtual == null)
            {
                _listaAtual = new List<TOExigNutrTabelasBll>();
            }

            _listaAtual.AddRange(_listParaAdicionar);

            return _listaAtual;
        }

        public List<TOExigNutrTabelasBll> ImprimeRelatorio(List<TOExigNutrTabelasBll> _listagem)
        {
            return _listagem;
        }
    }
}
