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
						IdPrescrMax, Adequado, Recomendado, Sobra, Dose, IdUnid, 
						IdPrescr, PesoAtual, Quantidade, Ativo, IdOperador, 
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
							IdPrescrMax, Adequado, Recomendado, Sobra, Dose, IdUnid, 
							IdPrescr, PesoAtual, Quantidade, Ativo, IdOperador, 
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

        public List<TOReceituarioNutrientesBll> ListarImpressao(int _idRec, int _idPrescr)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"EXECUTE ReceituarioNutrientesImpressao {0}",
                _idRec);

            var lista = crud.Listar<TOReceituarioNutrientesBll>(_sql);

            return lista.Where(w => w.IdPrescr == _idPrescr).ToList();
        }

        public List<TOReceitNutrGruposBll> ListarGruposNutr(int _idRec)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format($"EXECUTE ReceituarioNutrientesGrupos {_idRec}");

            var lista = crud.Listar<TOReceitNutrGruposBll>(_sql);

            return lista.ToList();
        }

        public List<TOReceituarioNutrientesBll> GeraRelatorio(List<TOReceituarioNutrientesBll> _listaAtual,
            List<TOReceituarioNutrientesBll> _listParaAdicionar)
        {
            if (_listaAtual == null)
            {
                _listaAtual = new List<TOReceituarioNutrientesBll>();
            }

            _listaAtual.AddRange(_listParaAdicionar);

            return _listaAtual;
        }

        public List<TOExigNutrTabelasBll> GeraRelatorio(List<TOExigNutrTabelasBll> _listaAtual,
            List<TOReceituarioNutrientesBll> _listParaAdicionar)
        {
            if (_listaAtual == null)
            {
                _listaAtual = new List<TOExigNutrTabelasBll>();
            }

            _listaAtual.AddRange(ConverterBalancImpr(_listParaAdicionar));

            return _listaAtual;
        }

        public TOReceituarioNutrientesBll ConverterTO(TOExigNutrTabelasBll _exigNutrTab)
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
            _recNutr.DoseMin = (_exigNutrTab.Minimo > 0 ? _exigNutrTab.Minimo : 0);
            _recNutr.IdUnidMin = null;
            _recNutr.IdPrescrMin = null;
            _recNutr.DoseMax = (_exigNutrTab.Maximo > 0 ? _exigNutrTab.Maximo : 0);
            _recNutr.Adequado = (_exigNutrTab.Adequado > 0 ? _exigNutrTab.Adequado : 0);
            _recNutr.Recomendado = (_exigNutrTab.Recomendado > 0 ? _exigNutrTab.Recomendado : 0);
            _recNutr.Sobra = (_exigNutrTab.Sobra > 0 ? _exigNutrTab.Sobra : 0);
            _recNutr.IdUnidMax = null;
            _recNutr.IdPrescrMax = null;

            return _recNutr;
        }

        public ReceituarioNutriente ConverterBalanceamento(TOExigNutrTabelasBll _exigNutrTab)
        {
            ReceituarioNutriente _recNutr = new ReceituarioNutriente();

            _recNutr.EmReceita = ((_exigNutrTab.EmCardapio > 0) || 
				(_exigNutrTab.Falta > 0) ? true : false);
            _recNutr.IdNutr = (_exigNutrTab.IdNutr != null ? _exigNutrTab.IdNutr.Value : 0);
            _recNutr.Consta = _exigNutrTab.EmCardapio;
            _recNutr.Falta = _exigNutrTab.Falta;
            _recNutr.Dose = (_exigNutrTab.Falta > 0 ? _exigNutrTab.Falta : 0);
            _recNutr.IdUnid = _exigNutrTab.IdUnid;
            _recNutr.IdPrescr = null;
            _recNutr.DoseMin = (_exigNutrTab.Minimo > 0 ? _exigNutrTab.Minimo : 0);
            _recNutr.IdUnidMin = null;
            _recNutr.IdPrescrMin = null;
            _recNutr.DoseMax = (_exigNutrTab.Maximo > 0 ? _exigNutrTab.Maximo : 0);
            _recNutr.Adequado = (_exigNutrTab.Adequado > 0 ? _exigNutrTab.Adequado : 0);
            _recNutr.Recomendado = (_exigNutrTab.Recomendado > 0 ? _exigNutrTab.Recomendado : 0);
            _recNutr.Sobra = (_exigNutrTab.Sobra > 0 ? _exigNutrTab.Sobra : 0);
            _recNutr.IdUnidMax = null;
            _recNutr.IdPrescrMax = null;

            return _recNutr;
        }

        public TOExigNutrTabelasBll ConverterBalancImpr(
			TOReceituarioNutrientesBll _exigNutrTab)
        {
            TOExigNutrTabelasBll _recNutr = new TOExigNutrTabelasBll();

			_recNutr.IdGrupo = _exigNutrTab.IdGrupo;
			_recNutr.Grupo = _exigNutrTab.Grupo;
            _recNutr.IdNutr = _exigNutrTab.IdNutr;
            _recNutr.Nutriente = _exigNutrTab.Nutriente;
            _recNutr.EmCardapio = _exigNutrTab.Consta;
            _recNutr.Falta = _exigNutrTab.Falta;
            _recNutr.IdUnid = _exigNutrTab.IdUnid;
            _recNutr.Minimo = (_exigNutrTab.DoseMin > 0 ? _exigNutrTab.DoseMin : 0);
            _recNutr.Maximo =(_exigNutrTab.DoseMax > 0 ? _exigNutrTab.DoseMax : 0);
            _recNutr.Adequado = (_exigNutrTab.Adequado > 0 ? _exigNutrTab.Adequado : 0);
            _recNutr.Recomendado = (_exigNutrTab.Recomendado > 0 ? _exigNutrTab.Recomendado : 0);
            _recNutr.Sobra = (_exigNutrTab.Sobra > 0 ? _exigNutrTab.Sobra : 0);

            return _recNutr;
        }

        public List<TOExigNutrTabelasBll> ConverterBalancImpr(
			List<TOReceituarioNutrientesBll> _exigNutrTab)
        {
            List<TOExigNutrTabelasBll> _retList = new List<TOExigNutrTabelasBll>();
            TOExigNutrTabelasBll _recNutr;

			foreach (TOReceituarioNutrientesBll item in _exigNutrTab)
			{
                _recNutr = new TOExigNutrTabelasBll();

                _recNutr.IdGrupo = item.IdGrupo;
                _recNutr.Grupo = item.Grupo;
                _recNutr.IdNutr = item.IdNutr;
                _recNutr.Nutriente = item.Nutriente;
                _recNutr.EmCardapio = item.Consta;
                _recNutr.Falta = item.Falta;
                _recNutr.IdUnid = item.IdUnid;
                _recNutr.Minimo = (item.DoseMin > 0 ? item.DoseMin : 0);
                _recNutr.Maximo =(item.DoseMax > 0 ? item.DoseMax : 0);
                _recNutr.Adequado = (item.Adequado > 0 ? item.Adequado : 0);
                _recNutr.Recomendado = (item.Recomendado > 0 ? item.Recomendado : 0);
                _recNutr.Sobra = (item.Sobra > 0 ? item.Sobra : 0);

				_retList.Add(_recNutr);
            }

            return _retList;
        }
    }
}
