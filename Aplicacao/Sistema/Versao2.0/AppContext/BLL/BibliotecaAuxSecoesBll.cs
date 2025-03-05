using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clBibliotecaAuxSecoesBll
    {
        public bllRetorno Inserir(BibliotecaAuxSecoe _secao)
        {
            bllRetorno ret = _secao.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_secao);
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

        public bllRetorno Alterar(BibliotecaAuxSecoe _secao)
        {
            bllRetorno ret = _secao.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_secao);

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

        public BibliotecaAuxSecoe Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            BibliotecaAuxSecoe _secao = null;

            var consulta = crud.ExecutarComando<BibliotecaAuxSecoe>(string.Format(@"
                Select *
                From BibliotecaAuxSecoes
                Where (IdSecao = {0})", _id));

            foreach (var item in consulta)
            {
                _secao = item;
            }

            return _secao;
        }

        public BibliotecaAuxSecoe Carregar(string _secao)
        {
            CrudDal crud = new CrudDal();
            BibliotecaAuxSecoe _retorno = new BibliotecaAuxSecoe();

            string _sql = string.Format(
                @"Select *
                  From BibliotecaAuxSecoes
                  Where (Secao = '{0}')", _secao);

            var ret = crud.ExecutarComando<BibliotecaAuxSecoe>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(BibliotecaAuxSecoe _secao)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_secao);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                msg = Alterar(_secao);
            }

            return msg;
        }

        public List<TOTela3Bll> Listar()
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<BibliotecaAuxSecoe>()
                        where l.Ativo == true
                        orderby l.Secao
                        select new TOTela3Bll
                        {
                            Id = l.IdSecao,
                            Nome = l.Secao,
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

                SELECT  Id, Nome, Ativo, IdOperador, IP, DataCadastro
                FROM    (
                            SELECT	ROW_NUMBER() OVER(ORDER BY Secao) AS NUMBER,
                                    IdSecao As Id, Secao As Nome,
		                            Ativo, IdOperador, IP, DataCadastro
                            FROM	BibliotecaAuxSecoes
                            WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                                    (Secao Like '%{0}%' COLLATE Latin1_General_CI_AI)",
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
                SELECT	Count(IdSecao) Total
                FROM	BiblitecaAuxSecoes
                WHERE   (Ativo = 1)";

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (Secao LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }
    }
}
