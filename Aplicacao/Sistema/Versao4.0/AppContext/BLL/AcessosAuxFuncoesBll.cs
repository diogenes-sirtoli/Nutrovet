using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using BLL.Properties;

namespace BLL
{
	public class clAcessosAuxFuncoesBll
	{
		public bllRetorno Inserir(AcessosAuxFuncoes _funcao)
		{
			bllRetorno ret = _funcao.ValidarRegras(true);
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

            try
			{
				if (ret.Retorno)
				{
					if (ret.Mensagem != Resources.RegReativado)
					{
						crud.Inserir(_funcao);
					}

					return bllRetorno.GeraRetorno(true, Resources.InsertOk);
				}
				else
				{
					return ret;
				}
			}
			catch
			{
				return bllRetorno.GeraRetorno(false, Resources.InsertDenied);
			}
		}

		public bllRetorno Alterar(AcessosAuxFuncoes _funcao)
		{
			bllRetorno ret = _funcao.ValidarRegras(false);

			if (ret.Retorno)
			{
                CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

                try
				{
					crud.Alterar(_funcao);

					return bllRetorno.GeraRetorno(true, 
						Resources.UpdateOk);
				}
				catch
				{
					return bllRetorno.GeraRetorno(false, 
						Resources.UpdateDenied);
				}
			}
			else
			{
				return ret;
			}
		}

		public AcessosAuxFuncoes Carregar(int _id)
		{
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

            return crud.Carregar(_id);
		}

		public AcessosAuxFuncoes Carregar(string _funcao)
		{
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();
            AcessosAuxFuncoes _retorno;

			string _sql = string.Format(
				@"Select *
				  From AcessosAuxFuncoes
				  Where (Funcao = '{0}')", _funcao);

			_retorno = crud.ExecutarComandoTO(_sql).FirstOrDefault();
			crud.Dispose();

			return _retorno;
		}

		public bllRetorno Excluir(int _idFuncao)
		{
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

            try
            {
                crud.Excluir(e => e.IdAcFunc == _idFuncao);
                crud.Dispose();

                return bllRetorno.GeraRetorno(true, Resources.DeleteOk);
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false, Resources.DeleteDenied);
            }
        }

		public List<TOTela3Bll> Listar()
		{
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

            var lista = from l in crud.Listar()
						where l.Ativo == true
						orderby l.Funcao
						select new TOTela3Bll
						{
							Id = l.IdAcFunc,
							Nome = l.Funcao,
							Ativo = l.Ativo,
							IdOperador = l.IdOperador,
							IP = l.IP,
							DataCadastro = l.DataCadastro
						};
            crud.Dispose();

            return lista.ToList();
		}

		public List<TOTela3Bll> Listar(string _pesqNome, int _tamPag, int _pagAtual)
		{
            CrudsDal<TOTela3Bll> crud = new CrudsDal<TOTela3Bll>();

            string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  Id, Nome, Sigla, Ativo, IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY Funcao) AS NUMBER,
									IdAcFunc As Id, Funcao As Nome, '' As Sigla,
									Ativo, IdOperador, IP, DataCadastro
							FROM	AcessosAuxFuncoes
							WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									(Funcao Like '%{0}%' COLLATE Latin1_General_CI_AI)",
									_pesqNome);
			}

			_sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    Nome";

			var lista = crud.Listar(_sql);
			crud.Dispose();

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqNome, int _tamPag)
		{
            CrudsDal<TOTela3Bll> crud = new CrudsDal<TOTela3Bll>();

            string _sql = @"
				SELECT	Count(IdAcFunc) Total
				FROM	AcessosAuxFuncoes
				WHERE   (Ativo = 1)";

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						 (Funcao LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);
            crud.Dispose();

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}
	}
}
