using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web;

namespace BLL
{
	public class clAnimaisAuxRacasBll
	{
		public bllRetorno Inserir(AnimaisAuxRaca _raca)
		{
			bllRetorno ret = _raca.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_raca);
						CacheExcluir();

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

		public bllRetorno Alterar(AnimaisAuxRaca _raca)
		{
			bllRetorno ret = _raca.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_raca);
					CacheExcluir();

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

		public AnimaisAuxRaca Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			AnimaisAuxRaca _raca = null;

			var consulta = crud.ExecutarComando<AnimaisAuxRaca>(string.Format(@"
				Select *
				From AnimaisAuxRacas
				Where (IdRaca = {0})", _id));

			foreach (var item in consulta)
			{
				_raca = item;
			}

			return _raca;
		}

		public AnimaisAuxRaca Carregar(int _idEspecie, string _raca)
		{
			CrudDal crud = new CrudDal();
			AnimaisAuxRaca _retorno = new AnimaisAuxRaca();

			string _sql = string.Format(
				@"Select *
				  From AnimaisAuxRacas
				  Where (IdEspecie = {0}) And (Raca = '{1}')", _idEspecie, _raca);

			var ret = crud.ExecutarComando<AnimaisAuxRaca>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public bllRetorno Excluir(AnimaisAuxRaca _raca)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_raca);
				CacheExcluir();

				msg = bllRetorno.GeraRetorno(true,
						  "EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
                var erro = err;
                msg = Alterar(_raca);
			}

			return msg;
		}

		public List<TORacasBll> Listar(string _pesqNome, int _idEsp, int _tamPag, 
			int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  IdEspecie, Especie, IdRaca, Raca, IdadeAdulta, CrescInicial, 
						CrescFinal, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY r.Raca) AS NUMBER, 
									r.IdEspecie, e.Especie, r.IdRaca, r.Raca, 
									r.IdadeAdulta, r.CrescInicial, r.CrescFinal, r.Ativo, 
									r.IdOperador, r.IP, r.DataCadastro
							FROM    AnimaisAuxEspecies AS e INNER JOIN
										AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie
							WHERE   (r.Ativo = 1) ", 
							_tamPag, _pagAtual);

			if (_idEsp > 0)
			{
				_sql += string.Format(@" AND 
									(r.IdEspecie = {0})", _idEsp);
			}

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									(r.Raca Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
									_pesqNome);
			}

			_sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Raca";

			var lista = crud.Listar<TORacasBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqNome, int _idEsp, int _tamPag)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(IdRaca) Total
				FROM	AnimaisAuxRacas
				WHERE   (Ativo = 1)";

			if (_idEsp > 0)
			{
				_sql += string.Format(@" AND 
						(IdEspecie = {0})", _idEsp);
			}

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						 (Raca LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public List<TORacasBll> Listar(int _idEsp)
		{
			List<TORacasBll> lista = (from l in CacheEspecies()
									  where (l.IdEspecie == _idEsp)
									  select l).ToList();


			return lista;
		}

		private List<TORacasBll> ListarCache()
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	r.IdEspecie, e.Especie, r.IdRaca, r.Raca, 
						r.IdadeAdulta, r.CrescInicial, r.CrescFinal, r.Ativo, 
						r.IdOperador, r.IP, r.DataCadastro
				FROM    AnimaisAuxEspecies AS e INNER JOIN
							AnimaisAuxRacas AS r ON e.IdEspecie = r.IdEspecie
				WHERE   (r.Ativo = 1)
				ORDER BY e.Especie, r.Raca";

			var lista = crud.Listar<TORacasBll>(_sql);

			return lista.ToList();
		}

		private List<TORacasBll> CacheEspecies()
		{
			System.Web.Caching.Cache _Cache;
			_Cache = HttpContext.Current.Cache;
			List<TORacasBll> CacheRacas = (List<TORacasBll>)_Cache["CacheRacas"];

			if (CacheRacas == null)
			{
				_Cache.Insert("CacheRacas", ListarCache(), null, DateTime.Now.AddMonths(1),
					TimeSpan.Zero);
				CacheRacas = (List<TORacasBll>)_Cache["CacheRacas"];
			}

			return CacheRacas;
		}

		private void CacheExcluir()
		{
			System.Web.Caching.Cache _Cache;
			_Cache = HttpContext.Current.Cache;

			_Cache.Remove("CacheRacas");
		}
	}
}
