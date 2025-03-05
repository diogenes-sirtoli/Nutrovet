using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Transactions;

namespace BLL
{
	public class clAlimentosBll
	{
		public bllRetorno Inserir(Alimentos _alimento)
		{
			bllRetorno ret = _alimento.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_alimento);
					}

					return bllRetorno.GeraRetorno(true,
						"INSERÇÃO efetuada com sucesso!!!");
				}
				else
				{
					return ret;
				}
			}
			catch (Exception err)
			{
				return bllRetorno.GeraRetorno(false,
					"Não foi possível efetuar a INSERÇÃO!!!");
			}
		}

		public bllRetorno Alterar(Alimentos _alimento)
		{
			bllRetorno ret = _alimento.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_alimento);

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

		public Alimentos Carregar(int _id)
		{
			CrudDal crud = new CrudDal();
			Alimentos _retorno = new Alimentos();

			string _sql = string.Format(
				@"Select *
				  From Alimentos
				  Where (IdAlimento = {0})", _id);

			var ret = crud.ExecutarComando<Alimentos>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public Alimentos Carregar(int _idFonte, string _alimento)
		{
			CrudDal crud = new CrudDal();
			Alimentos _retorno = new Alimentos();

			string _sql = string.Format(
				@"Select    *
				  From      Alimentos
				  Where     (IdFonte = {0}) AND 
							(Alimento = '{1}')",
				_idFonte, _alimento);

			var ret = crud.ExecutarComando<Alimentos>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public bllRetorno Excluir(Alimentos _alimento)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_alimento);

				msg = bllRetorno.GeraRetorno(true,
						  "EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				msg = Alterar(_alimento);
			}

			return msg;
		}

		public bllRetorno ExcluirGerencia(Alimentos _alimento)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();
			string _sql1 = "", _sql2 = "";
			int _del1 = 0, _del2 = 0;

			_sql1 = string.Format(@"
				Delete From AlimentoNutrientes
				Where (IdAlimento = {0})", _alimento.IdAlimento);

			_sql2 = string.Format(@"
				Delete From Alimentos
				Where (IdAlimento = {0})", _alimento.IdAlimento);

			//using (TransactionScope TS = new TransactionScope())
			//{
			try
			{
				_del1 = crud.ExecutarComando(_sql1);

				try
				{
					_del2 = crud.ExecutarComando(_sql2);

					if (_del2 <= 0)
					{
						msg = Alterar(_alimento);
					}
					else
					{
						msg = bllRetorno.GeraRetorno(true,
							"EXCLUSÃO efetuada com sucesso!!!");
					}

					//TS.Complete();
				}
				catch (Exception _msg2)
				{
					msg = Alterar(_alimento);
				}
			}
			catch (Exception _msg1)
			{
				//TS.Dispose();

				msg = Alterar(_alimento);
			}
			//}

			return msg;
		}

		public bllRetorno Homologar(int _idAlimento, int _idOperador, string _ip)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();
			int _upd = 0;

			string _sql = string.Format(@"
				SET DATEFORMAT dmy                

				Update Alimentos Set
					Compartilhar = 1,
					fHomologado = 1,
					DataHomol = '{1}',
					Ativo = 1,
					IdOperador = {2},
					IP = '{3}',
					DataCadastro = GetDate()
				Where (IdAlimento = {0}) ", _idAlimento,
				DateTime.Today.ToString("dd/MM/yyyy"),
				_idOperador, _ip);

			_upd = crud.ExecutarComando(_sql);

			if (_upd > 0)
			{
				return bllRetorno.GeraRetorno(true,
						 "HOMOLOGAÇÃO efetuada com sucesso!!!");
			}
			else
			{
				return bllRetorno.GeraRetorno(false,
						"Não foi possível efetuar a HOMOLOGAÇÃO!!!");
			}
		}

		public List<TOAlimentosBll> Listar(int _idFonte, int _idGrupo, int _idCateg)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	a.IdFonte, f.Fonte, a.IdGrupo, g.GrupoAlimento, a.IdCateg, 
						c.Categoria, a.IdAlimento, a.Alimento, a.NDB_No, a.Compartilhar, 
						a.Ativo, a.IdOperador, a.IP, a.DataCadastro
				FROM	Alimentos AS a INNER JOIN
							AlimentosAuxFontes AS f ON a.IdFonte = f.IdFonte INNER JOIN
							AlimentosAuxGrupos AS g ON a.IdGrupo = 
								g.IdGrupo LEFT OUTER JOIN
							AlimentosAuxCategorias AS c ON a.IdCateg = c.IdCateg
				WHERE	(a.Ativo = 1)";

			if (_idFonte > 0)
			{
				_sql += string.Format(@" AND 
						(a.IdFonte = {0})", _idFonte);
			}
			if (_idGrupo > 0)
			{
				_sql += string.Format(@" AND 
						(a.IdGrupo = {0})", _idGrupo);
			}
			if (_idCateg > 0)
			{
				_sql += string.Format(@" AND 
						(c.IdCateg = {0})", _idCateg);
			}

			_sql += @"
				ORDER BY    a.Alimento";

			var lista = crud.Listar<TOAlimentosBll>(_sql);

			return lista.ToList();
		}

		public List<TOAlimentosBll> Listar(string _pesqAlimento, string _nutriente, int _idPessoa,
			int _tamPag, int _pagAtual, bool _gerencia)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0} 

				", _tamPag, _pagAtual);


			if ((_nutriente != "") && (_nutriente != null))
			{
				_sql += @"
				SELECT  IdPessoa, Pessoa, IdFonte, fonte, IdGrupo, GrupoAlimento, 
						IdCateg, Categoria, IdAlimento, Alimento, IdNutr, Nutriente,
						NDB_No, Compartilhar, fHomologado, Ativo, IdOperador, 
						IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY a.Alimento) AS NUMBER,
									a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, 
									f.Fonte, a.IdGrupo, g.GrupoAlimento, a.IdCateg, c.Categoria,
									a.IdAlimento, a.Alimento, n.IdNutr, n.Nutriente, a.NDB_No, 
									a.Compartilhar, a.fHomologado, a.Ativo, a.IdOperador, a.IP, 
									a.DataCadastro";
			}
			else
			{
				_sql += @"
				SELECT  IdPessoa, Pessoa, IdFonte, fonte, IdGrupo, GrupoAlimento, 
						IdCateg, Categoria, IdAlimento, Alimento, NDB_No, Compartilhar,
						fHomologado, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY a.Alimento) AS NUMBER,
									a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, 
									f.Fonte, a.IdGrupo, g.GrupoAlimento, a.IdCateg, c.Categoria,
									a.IdAlimento, a.Alimento, a.NDB_No, a.Compartilhar, 
									a.fHomologado, a.Ativo, a.IdOperador, a.IP, 
									a.DataCadastro";
			}

			if ((_nutriente != "") && (_nutriente != null))
			{
				_sql += @"
							FROM	Nutrientes AS n INNER JOIN
									AlimentoNutrientes AS an ON n.IdNutr = 
										an.IdNutr RIGHT OUTER JOIN
									Alimentos AS a ON an.IdAlimento = 
										a.IdAlimento LEFT OUTER JOIN
									AlimentosAuxFontes AS f ON a.IdFonte = 
										f.IdFonte LEFT OUTER JOIN
									AlimentosAuxGrupos AS g ON a.IdGrupo = 
										g.IdGrupo LEFT OUTER JOIN
									AlimentosAuxCategorias AS c ON a.IdCateg = 
										c.IdCateg LEFT OUTER JOIN
									Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}
			else
			{
				_sql += @"
							FROM	Alimentos AS a LEFT OUTER JOIN
									AlimentosAuxFontes AS f ON a.IdFonte = 
										f.IdFonte LEFT OUTER JOIN
									AlimentosAuxGrupos AS g ON a.IdGrupo = 
										g.IdGrupo LEFT OUTER JOIN
									AlimentosAuxCategorias AS c ON a.IdCateg = 
										c.IdCateg LEFT OUTER JOIN
									Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}

			_sql += @"
							WHERE   ";

			if (_gerencia)
			{
				if (_pesqAlimento != "")
				{
					if ((_nutriente != "") && (_nutriente != null))
					{
						_sql += string.Format(@" ((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
									 (n.Nutriente LIKE '%{1}%' COLLATE Latin1_General_CI_AI) AND ",
									_pesqAlimento, _nutriente);
					}
					else
					{
						_sql += string.Format(@" ((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
									(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) AND ",
									_pesqAlimento);
					}
				}
				else if ((_nutriente != "") && (_nutriente != null))
				{
					_sql += string.Format(@" (n.Nutriente LIKE '%{0}%' COLLATE Latin1_General_CI_AI) AND ",
									_nutriente);
				}

				_sql += @" (a.fHomologado = 0)
						) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Pessoa, Alimento";
			}
			else
			{
				_sql += "(a.Ativo = 1) ";

				if (_idPessoa > 0)
				{
					_sql += string.Format(@" AND
									(((a.Compartilhar = 1) And (a.fHomologado = 1)) OR 
									 ((a.fHomologado = 0) AND (a.IdPessoa = {0}))) ",
										_idPessoa);
				}
				else
				{
					_sql += @" AND (a.Compartilhar = 1) AND (a.fHomologado = 1) ";
				}

				if (_pesqAlimento != "")
				{
					if ((_nutriente != "") && (_nutriente != null))
					{
						_sql += string.Format(@"AND 
									((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
									 (n.Nutriente LIKE '%{1}%' COLLATE Latin1_General_CI_AI)  ",
									_pesqAlimento, _nutriente);
					}
					else
					{
						_sql += string.Format(@"AND 
								   ((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
									(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) ",
									_pesqAlimento);
					}
				}
				else if ((_nutriente != "") && (_nutriente != null))
				{
					_sql += string.Format(@"AND 
								   (n.Nutriente LIKE '%{0}%' COLLATE Latin1_General_CI_AI) ",
								   _nutriente);
				}

				_sql += @"  ) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Alimento";
			}

			var lista = crud.Listar<TOAlimentosBll>(_sql);

			return lista.ToList();
		}

		public List<TOAlimentosBll> Listar(string _pesqAlimento, int _idNutr, int _idPessoa,
			int _tamPag, int _pagAtual, bool _gerencia)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0} 

				", _tamPag, _pagAtual);


			if (_idNutr > 0)
			{
				_sql += @"
				SELECT  IdPessoa, Pessoa, IdFonte, fonte, IdGrupo, GrupoAlimento, 
						IdCateg, Categoria, IdAlimento, Alimento, IdNutr, Nutriente,
						Valor, Unidade, NDB_No, Compartilhar, fHomologado, Ativo, 
						IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY an.Valor Desc) AS NUMBER,
									a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, 
									f.Fonte, a.IdGrupo, g.GrupoAlimento, a.IdCateg, c.Categoria,
									a.IdAlimento, a.Alimento, n.IdNutr, n.Nutriente, an.Valor,
									(Case an.IdUnidade
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
									End) Unidade, a.NDB_No, a.Compartilhar, a.fHomologado, 
									a.Ativo, a.IdOperador, a.IP, a.DataCadastro";
			}
			else
			{
				_sql += @"
				SELECT  IdPessoa, Pessoa, IdFonte, fonte, IdGrupo, GrupoAlimento, 
						IdCateg, Categoria, IdAlimento, Alimento, NDB_No, Compartilhar,
						fHomologado, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY a.Alimento) AS NUMBER,
									a.IdPessoa, Pessoas.Nome as Pessoa, a.IdFonte, 
									f.Fonte, a.IdGrupo, g.GrupoAlimento, a.IdCateg, c.Categoria,
									a.IdAlimento, a.Alimento, a.NDB_No, a.Compartilhar, 
									a.fHomologado, a.Ativo, a.IdOperador, a.IP, 
									a.DataCadastro";
			}

			if (_idNutr > 0)
			{
				_sql += @"
							FROM	Nutrientes AS n INNER JOIN
									AlimentoNutrientes AS an ON n.IdNutr = 
										an.IdNutr RIGHT OUTER JOIN
									Alimentos AS a ON an.IdAlimento = 
										a.IdAlimento LEFT OUTER JOIN
									AlimentosAuxFontes AS f ON a.IdFonte = 
										f.IdFonte LEFT OUTER JOIN
									AlimentosAuxGrupos AS g ON a.IdGrupo = 
										g.IdGrupo LEFT OUTER JOIN
									AlimentosAuxCategorias AS c ON a.IdCateg = 
										c.IdCateg LEFT OUTER JOIN
									Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}
			else
			{
				_sql += @"
							FROM	Alimentos AS a LEFT OUTER JOIN
									AlimentosAuxFontes AS f ON a.IdFonte = 
										f.IdFonte LEFT OUTER JOIN
									AlimentosAuxGrupos AS g ON a.IdGrupo = 
										g.IdGrupo LEFT OUTER JOIN
									AlimentosAuxCategorias AS c ON a.IdCateg = 
										c.IdCateg LEFT OUTER JOIN
									Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}

			_sql += @"
							WHERE   ";

			if (_gerencia)
			{
				if (_pesqAlimento != "")
				{
					if (_idNutr > 0)
					{
						_sql += string.Format(@" ((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
									 (an.IdNutr = {1}) AND (an.Valor > 0) AND ",
									_pesqAlimento, _idNutr);
					}
					else
					{
						_sql += string.Format(@" ((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
									(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) AND ",
									_pesqAlimento);
					}
				}
				else if (_idNutr > 0)
				{
					_sql += string.Format(@" (an.IdNutr = {0}) AND (an.Valor > 0) AND ",
						_idNutr);
				}

				_sql += @" (a.fHomologado = 0)
						) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)";
			}
			else
			{
				_sql += "(a.Ativo = 1) ";

				if (_idPessoa > 0)
				{
					_sql += string.Format(@" AND
									(((a.Compartilhar = 1) And (a.fHomologado = 1)) OR 
									 ((a.fHomologado = 0) AND (a.IdPessoa = {0}))) ",
										_idPessoa);
				}
				else
				{
					_sql += @" AND (a.Compartilhar = 1) AND (a.fHomologado = 1) ";
				}

				if (_pesqAlimento != "")
				{
					if (_idNutr > 0)
					{
						_sql += string.Format(@"AND 
									((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
									 (f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
									 (an.IdNutr = {1}) AND (an.Valor > 0) ",
									_pesqAlimento, _idNutr);
					}
					else
					{
						_sql += string.Format(@"AND 
								   ((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
									(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
									(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) ",
									_pesqAlimento);
					}
				}
				else if (_idNutr > 0)
				{
					_sql += string.Format(@"AND 
								   (an.IdNutr = {0}) AND (an.Valor > 0) ", _idNutr);
				}

				_sql += @"  ) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)";
			}

			var lista = crud.Listar<TOAlimentosBll>(_sql);

			return lista.ToList();
		}

		public List<TOAlimentosBll> Listar(string _pesqAlimento, int _idPessoa)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT  a.IdAlimento, (a.Alimento + ' - ' + f.Fonte) Alimento
				FROM	Alimentos AS a LEFT OUTER JOIN
							AlimentosAuxFontes AS f ON a.IdFonte = 
								f.IdFonte LEFT OUTER JOIN
							AlimentosAuxGrupos AS g ON a.IdGrupo = 
								g.IdGrupo LEFT OUTER JOIN
							AlimentosAuxCategorias AS c ON a.IdCateg = 
								c.IdCateg
				WHERE   (a.Ativo = 1) ";

			if (_idPessoa > 0)
			{
				_sql += string.Format(@" AND 
						(((a.Compartilhar = 1) And (a.fHomologado = 1)) OR 
						 ((a.fHomologado = 0) AND (a.IdPessoa = {0}))) ",
						 _idPessoa);
			}
			else
			{
				_sql += @" AND 
						(a.Compartilhar = 1) And (a.fHomologado = 1) ";
			}
			if (_pesqAlimento != "")
			{
				_sql += string.Format(@" AND 
						((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
						 (g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
						 (c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						 (f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI))",
						_pesqAlimento);
			}

			_sql += @"
				ORDER BY    Alimento";

			var lista = crud.Listar<TOAlimentosBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqAlimento, string _nutriente,
			int _idPessoa, int _tamPag, bool _gerencia)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(a.IdAlimento) Total ";

			if ((_nutriente != "") && (_nutriente != null))
			{
				_sql += @"
				FROM	Nutrientes AS n INNER JOIN
						AlimentoNutrientes AS an ON n.IdNutr = 
							an.IdNutr RIGHT OUTER JOIN
						Alimentos AS a ON an.IdAlimento = 
							a.IdAlimento LEFT OUTER JOIN
						AlimentosAuxFontes AS f ON a.IdFonte = 
							f.IdFonte LEFT OUTER JOIN
						AlimentosAuxGrupos AS g ON a.IdGrupo = 
							g.IdGrupo LEFT OUTER JOIN
						AlimentosAuxCategorias AS c ON a.IdCateg = 
							c.IdCateg LEFT OUTER JOIN
						Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}
			else
			{
				_sql += @"
				FROM	Alimentos AS a LEFT OUTER JOIN
						AlimentosAuxFontes AS f ON a.IdFonte = 
							f.IdFonte LEFT OUTER JOIN
						AlimentosAuxGrupos AS g ON a.IdGrupo = 
							g.IdGrupo LEFT OUTER JOIN
						AlimentosAuxCategorias AS c ON a.IdCateg = 
							c.IdCateg LEFT OUTER JOIN
						Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}

			_sql += @"
				WHERE   ";

			if (_gerencia)
			{
				if (_pesqAlimento != "")
				{
					if ((_nutriente != "") && (_nutriente != null))
					{
						_sql += string.Format(@"((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
						(n.Nutriente LIKE '%{1}%' COLLATE Latin1_General_CI_AI) AND ",
						_pesqAlimento, _nutriente);
					}
					else
					{
						_sql += string.Format(@"((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
						(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) AND ",
						_pesqAlimento);
					}
				}
				else if ((_nutriente != "") && (_nutriente != null))
				{
					_sql += string.Format(
						@" (n.Nutriente LIKE '%{0}%' COLLATE Latin1_General_CI_AI) AND ",
						_nutriente);
				}

				_sql += @" (a.fHomologado = 0)";
			}
			else
			{
				_sql += "(a.Ativo = 1) ";

				if (_idPessoa > 0)
				{
					_sql += string.Format(@" AND
						(((a.Compartilhar = 1) And (a.fHomologado = 1)) OR 
							((a.fHomologado = 0) AND (a.IdPessoa = {0}))) ",
							_idPessoa);
				}
				else
				{
					_sql += @" AND (a.Compartilhar = 1) AND (a.fHomologado = 1) ";
				}

				if (_pesqAlimento != "")
				{
					if ((_nutriente != "") && (_nutriente != null))
					{
						_sql += string.Format(@"AND 
						((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
						(n.Nutriente LIKE '%{1}%' COLLATE Latin1_General_CI_AI) ",
						_pesqAlimento, _nutriente);
					}
					else
					{
						_sql += string.Format(@"AND 
						((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
						(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) ",
						_pesqAlimento);
					}
				}
				else if ((_nutriente != "") && (_nutriente != null))
				{
					_sql += string.Format(@"AND 
						(n.Nutriente LIKE '%{0}%' COLLATE Latin1_General_CI_AI) ",
						_nutriente);
				}
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public int TotalPaginas(string _pesqAlimento, int _idNutr,
			int _idPessoa, int _tamPag, bool _gerencia)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	Count(a.IdAlimento) Total ";

			if (_idNutr > 0)
			{
				_sql += @"
				FROM	Nutrientes AS n INNER JOIN
						AlimentoNutrientes AS an ON n.IdNutr = 
							an.IdNutr RIGHT OUTER JOIN
						Alimentos AS a ON an.IdAlimento = 
							a.IdAlimento LEFT OUTER JOIN
						AlimentosAuxFontes AS f ON a.IdFonte = 
							f.IdFonte LEFT OUTER JOIN
						AlimentosAuxGrupos AS g ON a.IdGrupo = 
							g.IdGrupo LEFT OUTER JOIN
						AlimentosAuxCategorias AS c ON a.IdCateg = 
							c.IdCateg LEFT OUTER JOIN
						Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}
			else
			{
				_sql += @"
				FROM	Alimentos AS a LEFT OUTER JOIN
						AlimentosAuxFontes AS f ON a.IdFonte = 
							f.IdFonte LEFT OUTER JOIN
						AlimentosAuxGrupos AS g ON a.IdGrupo = 
							g.IdGrupo LEFT OUTER JOIN
						AlimentosAuxCategorias AS c ON a.IdCateg = 
							c.IdCateg LEFT OUTER JOIN
						Pessoas ON a.IdPessoa = Pessoas.IdPessoa";
			}

			_sql += @"
				WHERE   ";

			if (_gerencia)
			{
				if (_pesqAlimento != "")
				{
					if (_idNutr > 0)
					{
						_sql += string.Format(@"((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
						(an.IdNutr = {1}) AND (an.Valor > 0) AND ", _pesqAlimento, _idNutr);
					}
					else
					{
						_sql += string.Format(@"((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
						(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) AND ",
						_pesqAlimento);
					}
				}
				else if (_idNutr > 0)
				{
					_sql += string.Format(
						@" (an.IdNutr = {0}) AND (an.Valor > 0) AND ", _idNutr);
				}

				_sql += @" (a.fHomologado = 0)";
			}
			else
			{
				_sql += "(a.Ativo = 1) ";

				if (_idPessoa > 0)
				{
					_sql += string.Format(@" AND
						(((a.Compartilhar = 1) And (a.fHomologado = 1)) OR 
							((a.fHomologado = 0) AND (a.IdPessoa = {0}))) ",
							_idPessoa);
				}
				else
				{
					_sql += @" AND (a.Compartilhar = 1) AND (a.fHomologado = 1) ";
				}

				if (_pesqAlimento != "")
				{
					if (_idNutr > 0)
					{
						_sql += string.Format(@"AND 
						((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(g.GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria LIKE '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte LIKE '%{0}%' COLLATE Latin1_General_CI_AI)) AND
						(an.IdNutr = {1}) AND (an.Valor > 0) ",
						_pesqAlimento, _idNutr);
					}
					else
					{
						_sql += string.Format(@"AND 
						((REPLACE(REPLACE(Alimento, ',', ''), '-', ' ') Like '%{0}%' COLLATE Latin1_General_CI_AI) OR 
						(g.GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(c.Categoria Like '%{0}%' COLLATE Latin1_General_CI_AI) OR
						(f.Fonte Like '%{0}%' COLLATE Latin1_General_CI_AI)) ",
						_pesqAlimento);
					}
				}
				else if (_idNutr > 0)
				{
					_sql += string.Format(@"AND 
						(an.IdNutr = {0}) AND (an.Valor > 0) ", _idNutr);
				}
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}
	}
}
