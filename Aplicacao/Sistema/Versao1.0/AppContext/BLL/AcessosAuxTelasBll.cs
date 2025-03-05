using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clAcessosAuxTelasBll
    {
        public bllRetorno Inserir(AcessosAuxTela _tela)
        {
            bllRetorno ret = _tela.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_tela);
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

        public bllRetorno Alterar(AcessosAuxTela _tela)
        {
            bllRetorno ret = _tela.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_tela);

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

        public AcessosAuxTela Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            return crud.Carregar<AcessosAuxTela>(_id.ToString());
        }

        public AcessosAuxTela Carregar(string _tela)
        {
            CrudDal crud = new CrudDal();
            AcessosAuxTela _retorno = new AcessosAuxTela();

            string _sql = string.Format(
                @"Select *
                  From AcessosAuxTelas
                  Where (Telas = '{0}')", _tela);

            var ret = crud.ExecutarComando<AcessosAuxTela>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(AcessosAuxTela _tela)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_tela);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                msg = Alterar(_tela);
            }

            return msg;
        }

        public List<TOTela3Bll> Listar()
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<AcessosAuxTela>()
                        where l.Ativo == true
                        orderby l.Telas
                        select new TOTela3Bll
                        {
                            Id = l.IdTela,
                            Nome = l.Telas,
                            Sigla = l.CodTela,
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
                            SELECT	ROW_NUMBER() OVER(ORDER BY CodTela) AS NUMBER,
                                    IdTela As Id, Telas As Nome, CodTela As Sigla,
		                            Ativo, IdOperador, IP, DataCadastro
                            FROM	AcessosAuxTelas
                            WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                                    (Telas Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
                                    _pesqNome);
            }

            _sql += @") AS TBL
                WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
                ORDER BY    Sigla";

            var lista = crud.Listar<TOTela3Bll>(_sql);

            return lista.ToList();
        }

        public int TotalPaginas(string _pesqNome, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	Count(IdTela) Total
                FROM	AcessosAuxTelas
                WHERE   (Ativo = 1)";

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (Telas LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }
    }
}
