using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Transactions;
using System.Web;
using System.Globalization;

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
				var erro = err;
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
				var erro = err;
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
				}
				catch (Exception err)
				{
					var erro = err;
					msg = Alterar(_alimento);
				}
			}
			catch (Exception err)
			{
				var erro = err;
				msg = Alterar(_alimento);
			}

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

		public List<TOAlimentosBll> Listar(string _pesqAlimento, int _idNutr, int _idPessoa,
			int _tamPag, int _pagAtual, bool _gerencia)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				EXECUTE AlimentosNutrientesListar
						@pesqAlimento = '{0}', 
						@idNutr =		{1},
						@idPessoa =		{2}, 
						@RowspPage =	{3},
						@PageNumber =	{4}, 
						@gerencia =		{5} ",
				_pesqAlimento, _idNutr, _idPessoa, _tamPag, 
				_pagAtual, (_gerencia ? 1 : 0));

			var lista = crud.Listar<TOAlimentosBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqAlimento, int _idNutr,
			int _idPessoa, int _tamPag, bool _gerencia)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				EXECUTE AlimentosNutrientesListarTotalPaginas
						@pesqAlimento = '{0}', 
						@idNutr =		{1},
						@idPessoa =		{2},
						@gerencia =		{3} ",
				_pesqAlimento, _idNutr, _idPessoa,
				(_gerencia ? 1 : 0));

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public List<TOAlimentosBll> Listar(string _pesqAlim, int _idPessoa)
		{
			CrudDal crud = new CrudDal();
			List<TOAlimentosBll> _retorno = new List<TOAlimentosBll>();

			string _sql = string.Format(@"
				Execute AlimentosListar 
					@pesquisaAlimento = '{0}',	
					@idPessoa = {1}", _pesqAlim, _idPessoa);

			var lista = crud.Listar<TOAlimentosBll>(_sql);

			return lista.ToList();
		}
	}
}
