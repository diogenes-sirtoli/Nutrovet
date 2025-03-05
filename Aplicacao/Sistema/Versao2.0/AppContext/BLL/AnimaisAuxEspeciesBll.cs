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
	public class clAnimaisAuxEspeciesBll
	{
		public bllRetorno Inserir(AnimaisAuxEspecy _especie)
		{
			bllRetorno ret = _especie.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_especie);
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

		public bllRetorno Alterar(AnimaisAuxEspecy _especie)
		{
			bllRetorno ret = _especie.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_especie);
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

		public AnimaisAuxEspecy Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			AnimaisAuxEspecy _especie = null;

			var consulta = crud.ExecutarComando<AnimaisAuxEspecy>(string.Format(@"
				Select *
				From AnimaisAuxEspecies
				Where (IdEspecie = {0})", _id));

			foreach (var item in consulta)
			{
				_especie = item;
			}

			return _especie;
		}

		public AnimaisAuxEspecy Carregar(string _especie)
		{
			CrudDal crud = new CrudDal();
			AnimaisAuxEspecy _retorno = new AnimaisAuxEspecy();

			string _sql = string.Format(
				@"Select *
				  From AnimaisAuxEspecies
				  Where (Especie = '{0}')", _especie);

			var ret = crud.ExecutarComando<AnimaisAuxEspecy>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public bllRetorno Excluir(AnimaisAuxEspecy _especie)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_especie);
				CacheExcluir();


				msg = bllRetorno.GeraRetorno(true,
						  "EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
                var erro = err;
                msg = Alterar(_especie);
			}

			return msg;
		}

		private List<TOTela3Bll> ListarCache()
		{
			CrudDal crud = new CrudDal();

			var lista = from l in crud.Listar<AnimaisAuxEspecy>()
						where l.Ativo == true
						orderby l.Especie
						select new TOTela3Bll
						{
							Id = l.IdEspecie,
							Nome = l.Especie,
							Ativo = l.Ativo,
							IdOperador = l.IdOperador,
							IP = l.IP,
							DataCadastro = l.DataCadastro
						};

			return lista.ToList();
		}

		public List<TOTela3Bll> Listar()
		{
			return CacheEspecies();
		}

		public List<TOTela3Bll> Listar(string _pesqNome, int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  Id, Nome, Sigla, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY Especie) AS NUMBER,
									IdEspecie As Id, Especie As Nome, '' As Sigla,
									Ativo, IdOperador, IP, DataCadastro
							FROM	AnimaisAuxEspecies
							WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									(Especie Like '%{0}%' COLLATE Latin1_General_CI_AI)",
									_pesqNome);
			}

			_sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Nome";

			var lista = crud.Listar<TOTela3Bll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqNome, int _tamPag)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(IdEspecie) Total
				FROM	AnimaisAuxEspecies
				WHERE   (Ativo = 1)";

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						 (Especie LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		private List<TOTela3Bll> CacheEspecies()
		{
			System.Web.Caching.Cache _Cache;
			_Cache = HttpContext.Current.Cache;
			List<TOTela3Bll> CacheEspecies = (List<TOTela3Bll>)_Cache["CacheEspecies"];

			if (CacheEspecies == null)
			{
				_Cache.Insert("CacheEspecies", ListarCache(), null, DateTime.Now.AddMonths(1),
					TimeSpan.Zero);
				CacheEspecies = (List<TOTela3Bll>)_Cache["CacheEspecies"];
			}

			return CacheEspecies;
		}

		private void CacheExcluir()
		{
			System.Web.Caching.Cache _Cache;
			_Cache = HttpContext.Current.Cache;

			_Cache.Remove("CacheEspecies");
		}
	}
}
