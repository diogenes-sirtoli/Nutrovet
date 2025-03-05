using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Collections;
using Funcoes;

namespace BLL
{
	public class clNutraceuticosDietasBll
    {
		public bllRetorno Inserir(NutraceuticosDieta _nutrDie)
		{
			bllRetorno ret = _nutrDie.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_nutrDie);

					return bllRetorno.GeraRetorno(true, 
						"INSERÇÃO efetuada com sucesso!!!");

				}
				catch (Exception err)
				{
                    var erro = err;
                    return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a INSERÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Alterar(NutraceuticosDieta _nutrDie)
		{
			bllRetorno ret = _nutrDie.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_nutrDie);

					return bllRetorno.GeraRetorno(true, 
						"ALTERAÇÃO efetuada com sucesso!!!");

				}
				catch (Exception err)
				{
                    var erro = err;
                    return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a ALTERAÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Excluir(NutraceuticosDieta _nutrDie)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_nutrDie);

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

        public bllRetorno Excluir(int _idDieta, int _idNutrac)
        {
            CrudDal crud = new CrudDal();
			string _sql = string.Format(@"
				Delete From NutraceuticosDietas
				Where (IdNutrac = {0}) AND (IdDieta = {1})",
				_idNutrac, _idDieta);

            try
            {
                int consulta = crud.ExecutarComando(_sql);

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

        public NutraceuticosDieta Carregar(int _idNutdie)
		{
			CrudDal crud = new CrudDal();

            NutraceuticosDieta _cardAlim = null;

			var consulta = crud.ExecutarComando<NutraceuticosDieta>(string.Format(@"
				Select *
				From NutraceuticosDietas
				Where (IdNutdie = {0})", _idNutdie));

			foreach (var item in consulta)
			{
				_cardAlim = item;
			}

			return _cardAlim;
		}

        public List<TOReceituarioNutrientesBll> Listar(int _idEspecie, int _idDieta,
            string _pesoAtual)
        {
            CrudDal crud = new CrudDal();
			List<TOReceituarioNutrientesBll> _litaRet = new List<TOReceituarioNutrientesBll>();
			TOReceituarioNutrientesBll _itemRet;


            string _sql = string.Format(@"
				Execute NutraceuticosDietasListar
					@idEspecie	= {0},
					@idDieta	= {1},
					@pesoAtual	= {2} ", _idEspecie, _idDieta,
                (_pesoAtual != "" ? _pesoAtual.Replace(",", ".") : "0"));

            var lista = crud.Listar<TONutraceuticosDietasBll>(_sql);

			foreach (TONutraceuticosDietasBll item in lista)
			{
				_itemRet = new TOReceituarioNutrientesBll();

				_itemRet.IdReceita = 0;
				_itemRet.dTpRec = 0;
				_itemRet.TipoReceita = "";
				_itemRet.Titulo = "";
				_itemRet.Arquivo = "";
				_itemRet.NrReceita = "";
				_itemRet.IdNutrRec = 0;
				_itemRet.IdEspecie = item.IdEspecie;
				_itemRet.Especie = item.Especie;
				_itemRet.IdDieta = item.IdDieta;
				_itemRet.Dieta = item.Dieta;
				_itemRet.EmReceita = Funcoes.Funcoes.ConvertePara.Bool(item.EmReceita);
				_itemRet.IdNutr = item.IdNutr;
				_itemRet.Nutriente = item.Nutriente;
				_itemRet.DoseMin = item.DoseMin;
				_itemRet.IdUnidMin = item.IdUnidMin;
				_itemRet.UnidadeMin = item.UnidadeMin;
				_itemRet.DoseMax = item.DoseMax;
                _itemRet.IdUnidMax = item.IdUnidMax;
                _itemRet.UnidadeMax = item.UnidadeMax;
				_itemRet.Dose = item.Dose;
				_itemRet.IdUnid = item.IdUnid;
				_itemRet.Unidade = item.Unidade;
				_itemRet.IdPrescr = item.IdPrescr1;
				_itemRet.Prescricao = item.Prescricao1;
				_itemRet.IdPrescrMax = item.IdPrescr2;
				_itemRet.PrescricaoMax = item.Prescricao2;
                _itemRet.PesoAtual = item.PesoAtual;
				_itemRet.Quantidade = item.Quantidade;

                _litaRet.Add(_itemRet);
            }

            return _litaRet.ToList();
        }

        public List<Dietas> ListarDietasNaoCadastradas(int _idEspecie, int _idNutrac)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				Select	d.IdDieta, d.Dieta
				From	Dietas AS d
				Where	(d.IdEspecie = {0}) AND
						(d.IdDieta not in
							( 
								SELECT	nd.IdDieta
								FROM	NutraceuticosDietas AS nd
								WHERE	(nd.IdNutrac = {1})
							)
						)
				Order By d.Dieta", _idEspecie, _idNutrac);

            var lista = crud.Listar<Dietas>(_sql);

            return lista.ToList();
        }

        public List<Dietas> ListarDietasCadastradas(int _idEspecie, int _idNutrac)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				SELECT	nd.IdDieta, Dietas.Dieta
				FROM	NutraceuticosDietas AS nd INNER JOIN
							Dietas ON nd.IdDieta = Dietas.IdDieta
				WHERE	(nd.IdNutrac = {1}) AND (Dietas.IdEspecie = {0})
				Order By Dietas.Dieta", _idEspecie, _idNutrac);

            var lista = crud.Listar<Dietas>(_sql);

            return lista.ToList();
        }


        public List<object> ListarPivot(int _idEspecie)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				Execute NutraceuticosDietasPivot {0}", 
				_idEspecie);

            var lista = crud.Listar<object>(_sql);

            return lista.ToList();
        }


    }
}
