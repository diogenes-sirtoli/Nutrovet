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
	public class clNutrientesBll
	{
		public bllRetorno Inserir(Nutriente _nutri)
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

		public bllRetorno Alterar(Nutriente _nutri)
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

		public Nutriente Carregar(int _id)
		{
			CrudDal crud = new CrudDal();
			Nutriente _retorno = new Nutriente();

			string _sql = string.Format(
				@"Select *
				  From Nutrientes
				  Where (IdNutr = {0})", _id);

			var ret = crud.ExecutarComando<Nutriente>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public TONutrientesBll CarregarTO(int _id)
		{
			CrudDal crud = new CrudDal();
			TONutrientesBll _retorno = new TONutrientesBll();

			string _sql = string.Format(
				@"SELECT	n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, n.Referencia, 
							n.ValMin, n.ValMax, n.IdUnidade, 
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
							End) Unidade,
							n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
							n.IdOperador, n.IP, n.DataCadastro
					FROM	Nutrientes AS n INNER JOIN
								NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
					Where	(n.IdNutr = {0})", _id);

			var ret = crud.ExecutarComando<TONutrientesBll>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public TONutrientesBll CarregarTO(int _id, bool _listarEmAlim)
		{
			CrudDal crud = new CrudDal();
			TONutrientesBll _retorno = new TONutrientesBll();

			string _sql = string.Format(
				@"SELECT	n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, n.Referencia, 
							n.ValMin, n.ValMax, n.IdUnidade, 
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
							End) Unidade,
							n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
							n.IdOperador, n.IP, n.DataCadastro
					FROM	Nutrientes AS n INNER JOIN
								NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
					Where	(n.IdNutr = {0}) AND (n.ListarEmAlim = {1})", _id,
				(_listarEmAlim ? 1 : 0));

			var ret = crud.ExecutarComando<TONutrientesBll>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public Nutriente Carregar(string _nutri)
		{
			CrudDal crud = new CrudDal();
			Nutriente _retorno = new Nutriente();

			string _sql = string.Format(
				@"Select *
				  From Nutrientes
				  Where (Nutriente = '{0}')", _nutri);

			var ret = crud.ExecutarComando<Nutriente>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public bllRetorno Excluir(Nutriente _nutri)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_nutri);

				msg = bllRetorno.GeraRetorno(true,
						"EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
            {
                var erro = err;
                msg = Alterar(_nutri);
			}

			return msg;
		}

		public List<TONutrientesBll> Listar()
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, n.Referencia, 
						n.ValMin, n.ValMax, n.IdUnidade, 
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
						End) Unidade,
						n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
						n.IdOperador, n.IP, n.DataCadastro
				FROM	Nutrientes AS n INNER JOIN
							NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
				ORDER BY n.Nutriente";

			var lista = crud.Listar<TONutrientesBll>(_sql);

			return lista.ToList();
		}

		public List<TONutrientesBll> Listar(bool _listarEmAlim)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT	n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, n.Referencia, 
						n.ValMin, n.ValMax, n.IdUnidade, 
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
						End) Unidade,
						n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
						n.IdOperador, n.IP, n.DataCadastro
				FROM	Nutrientes AS n INNER JOIN
							NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
				Where	(n.ListarEmAlim = {0})
				ORDER BY n.Nutriente", (_listarEmAlim ? 1 : 0));

			var lista = crud.Listar<TONutrientesBll>(_sql);

			return lista.ToList();
		}

		public List<TONutrientesBll> Listar(int _idAlim, int _idGrupo, 
			bool _listarEmAlim)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT  Nutrientes.IdGrupo, NutrientesAuxGrupos.Grupo, Nutrientes.IdNutr, 
						Nutrientes.Nutriente, Nutrientes.Referencia,";


			if (_idAlim > 0)
			{
				_sql += string.Format(@"
						(Select an.Valor
						 From   AlimentoNutrientes an
						 Where  (an.IdNutr = Nutrientes.IdNutr) AND 
								(an.IdAlimento = {0})) Valor, ", _idAlim);
			}

			_sql += string.Format(@"
						Nutrientes.ValMin, Nutrientes.ValMax, Nutrientes.IdUnidade,
						(CASE   Nutrientes.IdUnidade
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
						 END) AS Unidade, Nutrientes.ListarCardapio, 
						Nutrientes.ListarEmAlim, Nutrientes.Ativo, 
						Nutrientes.IdOperador, Nutrientes.IP, Nutrientes.DataCadastro
				FROM    Nutrientes INNER JOIN
						NutrientesAuxGrupos ON Nutrientes.IdGrupo = 
							NutrientesAuxGrupos.IdGrupo
				WHERE   (Nutrientes.IdGrupo = {0}) ", _idGrupo);

			if (_listarEmAlim)
			{
				_sql += @"AND
						(NutrientesAuxGrupos.ListarEmAlim = 1) AND 
						(Nutrientes.ListarEmAlim = 1)";
			}

			_sql += @"
				ORDER BY    NutrientesAuxGrupos.Grupo, Nutrientes.Nutriente";

			var lista = crud.Listar<TONutrientesBll>(_sql);

			return lista.ToList();
		}

		public List<TONutrientesBll> Listar(string _pesqNome, bool _listarEmAlim, 
			int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  IdGrupo, Grupo, IdNutr, Nutriente, Referencia, ValMin, 
						ValMax, IdUnidade, Unidade, ListarCardapio, ListarEmAlim,
						Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY Nutriente) AS NUMBER,
									n.IdGrupo, g.Grupo, n.IdNutr, n.Nutriente, 
									n.Referencia, n.ValMin, n.ValMax, n.IdUnidade, 
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
									End) Unidade,
									n.ListarCardapio, n.ListarEmAlim, n.Ativo, 
									n.IdOperador, n.IP, n.DataCadastro
							FROM	Nutrientes AS n INNER JOIN
										NutrientesAuxGrupos AS g ON n.IdGrupo = g.IdGrupo
							WHERE   (n.Ativo = 1) ", _tamPag, _pagAtual);

			if (_listarEmAlim)
			{
				_sql += @"AND
									(g.ListarEmAlim = 1) AND (n.ListarEmAlim = 1)";
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

			var lista = crud.Listar<TONutrientesBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqNome, bool _listarEmAlim, int _tamPag)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(IdNutr) Total
				FROM	Nutrientes
				WHERE   (Ativo = 1)";

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						 (Nutriente LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
			}

			if (_listarEmAlim)
			{
				_sql += @"AND
						 (n.ListarEmAlim = 1)";
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
	}
}
