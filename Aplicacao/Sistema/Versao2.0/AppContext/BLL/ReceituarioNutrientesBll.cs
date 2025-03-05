using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;

namespace BLL
{
	public class clReceituarioNutrientesBll
    {
		public bllRetorno Inserir(ReceituarioNutriente _recNutr)
		{
			bllRetorno ret = _recNutr.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_recNutr);

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

		public bllRetorno Alterar(ReceituarioNutriente _recNutr)
		{
			bllRetorno ret = _recNutr.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_recNutr);

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

		public bllRetorno Excluir(ReceituarioNutriente _recNutr)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_recNutr);

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

		public ReceituarioNutriente Carregar(int _idNutrRec)
		{
			CrudDal crud = new CrudDal();

			ReceituarioNutriente _nutri = null;

			var _nutrRec = crud.ExecutarComando<ReceituarioNutriente>(string.Format(@"
				SELECT	IdReceita, IdNutrRec, EmReceita, IdNutr, Consta, Falta, 
						DoseMin, IdUnidMin, IdPrescrMin, DoseMax, IdUnidMax, 
						IdPrescrMax, Dose, IdUnid, IdPrescr, Ativo, IdOperador, 
						IP, DataCadastro, Versao
				FROM	ReceituarioNutrientes
				Where (IdNutrRec = {0})", _idNutrRec));

			foreach (var item in _nutrRec)
			{
				_nutri = item;
			}

			return _nutri;
		}

		public ReceituarioNutriente Carregar(int _idRec, int _idNutr)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(
                @"SELECT	IdReceita, IdNutrRec, EmReceita, IdNutr, Consta, Falta, 
							DoseMin, IdUnidMin, IdPrescrMin, DoseMax, IdUnidMax, 
							IdPrescrMax, Dose, IdUnid, IdPrescr, Ativo, IdOperador, 
							IP, DataCadastro, Versao
				  From		ReceituarioNutriente
				  Where		(IdReceita = {0}) AND (IdNutr = {1})",
				_idRec, _idNutr);

			var ret = crud.ExecutarComando<ReceituarioNutriente>(_sql).SingleOrDefault();

			return ret;
		}

		public bool ExisteNutriente(int _idRec, int _idNutr)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(
                @"SELECT  COUNT(rn.IdNutrRec) AS Total
                  FROM    ReceituarioNutrientes rn
                  WHERE   (rn.IdReceita = {0}) AND (rn.IdNutr = {1})",
				_idRec, _idNutr);

			int ret = crud.ExecutarComandoTipoInteiro(_sql);

			return Funcoes.Funcoes.ConvertePara.Bool(ret);
		}

		public List<TOReceituarioNutrientesBll> ListarTO(int _idRec)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"EXECUTE ReceituarioNutrientesListar	{0}",
				_idRec);

			var lista = crud.Listar<TOReceituarioNutrientesBll>(_sql);

			return lista.ToList();
		}

        public List<TOReceituarioNutrientesBll> ListarImpressao(int _idRec)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"EXECUTE ReceituarioNutrientesImpressao {0}",
                _idRec);

            var lista = crud.Listar<TOReceituarioNutrientesBll>(_sql);

            return lista.ToList();
        }

		public TOReceituarioNutrientesBll Converter(TOExigNutrTabelasBll _exigNutrTab)
		{
			TOReceituarioNutrientesBll _recNutr = new TOReceituarioNutrientesBll();

			_recNutr.Nutriente = _exigNutrTab.Nutriente;
            _recNutr.EmReceita = (_exigNutrTab.Falta > 0 ? true : false);
			_recNutr.IdNutr = (_exigNutrTab.IdNutr != null ? _exigNutrTab.IdNutr.Value : 0);
			_recNutr.Consta = _exigNutrTab.EmCardapio;
			_recNutr.Falta = _exigNutrTab.Falta;
			_recNutr.Dose = (_exigNutrTab.Falta > 0 ? _exigNutrTab.Falta : 0);
            _recNutr.IdUnid = _exigNutrTab.IdUnid;
			_recNutr.IdPrescr = null;
            _recNutr.DoseMin = 0;
            _recNutr.IdUnidMin = null;
            _recNutr.IdPrescrMin = null;
            _recNutr.DoseMax = 0;
            _recNutr.IdUnidMax = null;
            _recNutr.IdPrescrMax = null;

            return _recNutr;
        }
    }
}
