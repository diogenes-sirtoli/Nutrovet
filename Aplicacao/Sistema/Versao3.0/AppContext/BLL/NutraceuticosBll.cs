using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Web;

namespace BLL
{
	public class clNutraceuticosBll
	{
		public bllRetorno Inserir(Nutraceutico _nutra)
		{
			bllRetorno ret = _nutra.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
                    CacheExcluir();
                    crud.Inserir(_nutra);

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

		public bllRetorno Alterar(Nutraceutico _nutra)
		{
			bllRetorno ret = _nutra.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
                    CacheExcluir();
                    crud.Alterar(_nutra);

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

		public Nutraceutico Carregar(int _id)
		{
			CrudDal crud = new CrudDal();
			Nutraceutico _retorno = new Nutraceutico();

			string _sql = string.Format(
				@"Select *
				  From Nutraceuticos
				  Where (IdNutrac = {0})", _id);

			var ret = crud.ExecutarComando<Nutraceutico>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public TONutraceuticosBll CarregarTO(int _id)
		{
			var lista = from l in CacheNutraceuticos()
						where (l.IdNutrac == _id)
						select l;

			return lista.FirstOrDefault();
		}

		public bllRetorno Excluir(Nutraceutico _nutra)
		{
			CrudDal crud = new CrudDal();

			try
			{
                CacheExcluir();
                crud.Excluir(_nutra);

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

		private List<TONutraceuticosBll> Listar()
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				Execute NutraceuticosListar;";

			var lista = crud.Listar<TONutraceuticosBll>(_sql);

			return lista.ToList();
		}

		private List<TONutraceuticosBll> ListarCache()
		{
			return CacheNutraceuticos();
		}

		public List<TONutraceuticosBll> Listar(int _idEspecie)
		{
			var lista = from l in CacheNutraceuticos()
						 where (l.IdEspecie == _idEspecie)
						 select l;

			return lista.ToList();
		}

		public List<TONutraceuticosBll> Listar(string _pesqNome, int _idEsp, int _tamPag,
			int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  IdNutrac, IdEspecie, Especie, IdGrupo, Grupo, IdNutr, Nutriente, 
						DoseMin, IdUnidMin, UnidadeMin, DoseMax, IdUnidMax, UnidadeMax, 
						IdPrescr1, TpPrescr1, IdPrescr2, TpPrescr2, Obs, Ativo, 
						IdOperador, IP, DataCadastro
				FROM   (
						SELECT	ROW_NUMBER() OVER(ORDER BY n.Nutriente) AS NUMBER,
								nutra.IdNutrac, nutra.IdEspecie, e.Especie, n.IdGrupo, 
								ng.Grupo, nutra.IdNutr, n.Nutriente, nutra.DoseMin, 
								nutra.IdUnidMin, 
								(CASE nutra.IdUnidMin 
									WHEN 1 THEN 'µg' 
									WHEN 2 THEN 'g' 
									WHEN 3 THEN 'mg' 
									WHEN 4 THEN 'Kcal' 
									WHEN 5 THEN 'UI' 
									WHEN 6 THEN 'Proporção' 
									WHEN 7 THEN 'mcg/Kg' 
									WHEN 8 THEN 'mg/animal' 
									WHEN 9 THEN 'mg/Kg' 
									WHEN 10 THEN 'UI/Kg' END) AS UnidadeMin, 
								nutra.DoseMax, nutra.IdUnidMax, 
								(CASE nutra.IdUnidMax 
									WHEN 1 THEN 'µg' 
									WHEN 2 THEN 'g' 
									WHEN 3 THEN 'mg' 
									WHEN 4 THEN 'Kcal' 
									WHEN 5 THEN 'UI' 
									WHEN 6 THEN 'Proporção' 
									WHEN 7 THEN 'mcg/Kg' 
									WHEN 8 THEN 'mg/animal' 
									WHEN 9 THEN 'mg/Kg' 
									WHEN 10 THEN 'UI/Kg' END) AS UnidadeMax, 
								nutra.IdPrescr1, p1.Prescricao AS TpPrescr1, 
								nutra.IdPrescr2, p2.Prescricao AS TpPrescr2, nutra.Obs, 
								nutra.Ativo, nutra.IdOperador, nutra.IP, 
								nutra.DataCadastro
						FROM    Nutraceuticos AS nutra INNER JOIN
									AnimaisAuxEspecies AS e ON nutra.IdEspecie = 
										e.IdEspecie INNER JOIN
									Nutrientes AS n ON nutra.IdNutr = 
										n.IdNutr INNER JOIN
									NutrientesAuxGrupos AS ng ON n.IdGrupo = 
										ng.IdGrupo LEFT OUTER JOIN
									PrescricaoAuxTipos AS p1 ON nutra.IdPrescr1 = 
										p1.IdPrescr LEFT OUTER JOIN
									PrescricaoAuxTipos AS p2 ON nutra.IdPrescr2 = 
										p2.IdPrescr
						WHERE   (nutra.Ativo = 1) ", _tamPag, _pagAtual);

			if (_idEsp > 0)
			{
				_sql += string.Format(@" AND 
									(nutra.IdEspecie = {0})", _idEsp);
			}

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									(n.Nutriente Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
									_pesqNome);
			}

			_sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Nutriente";

			var lista = crud.Listar<TONutraceuticosBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqNome, int _idEsp, int _tamPag)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(nutra.IdNutrac) Total
				FROM    Nutraceuticos AS nutra INNER JOIN
							AnimaisAuxEspecies AS e ON nutra.IdEspecie = 
								e.IdEspecie INNER JOIN
							Nutrientes AS n ON nutra.IdNutr = 
								n.IdNutr INNER JOIN
							NutrientesAuxGrupos AS ng ON n.IdGrupo = 
								ng.IdGrupo LEFT OUTER JOIN
							PrescricaoAuxTipos AS p1 ON nutra.IdPrescr1 = 
								p1.IdPrescr LEFT OUTER JOIN
							PrescricaoAuxTipos AS p2 ON nutra.IdPrescr2 = 
								p2.IdPrescr
				WHERE	(nutra.Ativo = 1)";

			if (_idEsp > 0)
			{
				_sql += string.Format(@" AND 
						(nutra.IdEspecie = {0})", _idEsp);
			}

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						(n.Nutriente Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
						_pesqNome);
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public ListItem[] ListarUnidades()
		{
			ListItem[] und = Funcoes.Funcoes.GetEnumList<DominiosBll.Unidades>("/");

			return und;
		}

		private List<TONutraceuticosBll> CacheNutraceuticos()
		{
			System.Web.Caching.Cache _Cache;
			_Cache = HttpContext.Current.Cache;
			List<TONutraceuticosBll> CacheNutraceuticos = (List<TONutraceuticosBll>)_Cache["CacheNutraceuticos"];

			if (CacheNutraceuticos == null)
			{
				_Cache.Insert("CacheNutraceuticos", Listar(), null, DateTime.Now.AddDays(1),
					TimeSpan.Zero);
				CacheNutraceuticos = (List<TONutraceuticosBll>)_Cache["CacheNutraceuticos"];
			}

			return CacheNutraceuticos;
		}

		private void CacheExcluir()
		{
			System.Web.Caching.Cache _Cache;
			_Cache = HttpContext.Current.Cache;

			_Cache.Remove("CacheNutraceuticos");
		}
	}
}
