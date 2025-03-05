using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;

namespace BLL
{
	public class clAtendimentoAuxTiposBll
    {
		public bllRetorno Inserir(AtendimentoAuxTipo _tipo)
		{
			bllRetorno ret = _tipo.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_tipo);
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

        public bllRetorno Alterar(AtendimentoAuxTipo _tipo)
		{
			bllRetorno ret = _tipo.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_tipo);

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

		public AtendimentoAuxTipo Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			return crud.Carregar<AtendimentoAuxTipo>(_id.ToString());
		}

		public AtendimentoAuxTipo Carregar(string _tela)
		{
			CrudDal crud = new CrudDal();
            AtendimentoAuxTipo _retorno = new AtendimentoAuxTipo();

			string _sql = string.Format(
                @"Select *
				  From AtendimentoAuxTipos
				  Where (TipoAtendimento = '{0}')", _tela);

			var ret = crud.ExecutarComando<AtendimentoAuxTipo>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public bllRetorno Excluir(AtendimentoAuxTipo _tipo)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_tipo);

				msg = bllRetorno.GeraRetorno(true,
						"EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				var erro = err;
				msg = Alterar(_tipo);
			}

			return msg;
		}

		public List<TOTela3Bll> Listar()
		{
			CrudDal crud = new CrudDal();

			var lista = from l in crud.Listar<AtendimentoAuxTipo>()
						where l.Ativo == true
						orderby l.TipoAtendimento
						select new TOTela3Bll
						{
							Id = l.IdTpAtend,
							Nome = l.TipoAtendimento,
							Sigla = "",
							Ativo = l.Ativo,
							IdOperador = l.IdOperador,
							IP = l.IP,
							DataCadastro = l.DataCadastro
						};

			return lista.ToList();
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
							SELECT	ROW_NUMBER() OVER(ORDER BY TipoAtendimento) AS NUMBER,
									IdTpAtend As Id, TipoAtendimento As Nome, '' AS Sigla,
									Ativo, IdOperador, IP, DataCadastro
							FROM	AtendimentoAuxTipos
							WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									(TipoAtendimento Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
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
				SELECT	Count(IdTpAtend) Total
				FROM	AtendimentoAuxTipos
				WHERE   (Ativo = 1)";

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						 (TipoAtendimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}
	}
}
