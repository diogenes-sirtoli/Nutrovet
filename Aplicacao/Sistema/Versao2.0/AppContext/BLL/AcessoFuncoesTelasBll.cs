using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Services;
using DAL;
using DCL;

namespace BLL
{
    public class clAcessosFuncoesTelasBll
    {
        public bllRetorno Inserir(AcessosFuncoesTela _funcTelas)
        {
            bllRetorno ret = _funcTelas.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_funcTelas);
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

        public bllRetorno Alterar(AcessosFuncoesTela _funcTelas)
        {
            bllRetorno ret = _funcTelas.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_funcTelas);

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

        public bllRetorno Excluir(AcessosFuncoesTela _funcTelas)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_funcTelas);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                return bllRetorno.GeraRetorno(false, "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public AcessosFuncoesTela Carregar(int _id)
        {
            CrudDal crud = new CrudDal();
            AcessosFuncoesTela _funcTelas = new AcessosFuncoesTela();
            string _sql = string.Format(@"
                Select  *
                From    AcessosFuncoesTelas
                Where   (IdFuncTela = {0}) ", _id);

            var _ftela = crud.ExecutarComando<AcessosFuncoesTela>(_sql);

            foreach (var item in _ftela)
            {
                _funcTelas = item;
            }

            return _funcTelas;
        }

        public AcessosFuncoesTela Carregar(int _idacFunc, int _idtela)
        {
            CrudDal crud = new CrudDal();
            AcessosFuncoesTela _retFuncTela = new AcessosFuncoesTela();

            var _funcTela = crud.ExecutarComando<AcessosFuncoesTela>(string.Format(@"
                Select  *
                From    AcessosFuncoesTelas
                WHERE	(IdAcFunc = {0}) And (IdTela = {1})", _idacFunc, _idtela));

            foreach (var item in _funcTela)
            {
                _retFuncTela = item;
            }

            return _retFuncTela;
        }

        public TOFuncoesTelas CarregarTO(int _id)
        {
            CrudDal crud = new CrudDal();
            TOFuncoesTelas _retFuncTela = new TOFuncoesTelas();

            string _sql = string.Format(@"
                SELECT  AcessosFuncoesTelas.IdFuncTela, AcessosFuncoesTelas.IdAcFunc, 
                        AcessosAuxFuncoes.Funcao, AcessosFuncoesTelas.IdTela, 
                        AcessosAuxTelas.Telas, AcessosAuxTelas.CodTela, 
                        AcessosFuncoesTelas.Ativo, AcessosFuncoesTelas.IdOperador, 
                        AcessosFuncoesTelas.IP, AcessosFuncoesTelas.DataCadastro
                FROM    AcessosFuncoesTelas INNER JOIN
                        AcessosAuxFuncoes ON AcessosFuncoesTelas.IdAcFunc = 
                            AcessosAuxFuncoes.IdAcFunc INNER JOIN
                        AcessosAuxTelas ON AcessosFuncoesTelas.IdTela = 
                            AcessosAuxTelas.IdTela
                WHERE   (FuncoesTelas.IdFuncTela = {0}) ", _id);

            var _funcTela = crud.ExecutarComando<TOFuncoesTelas>(_sql);

            foreach (TOFuncoesTelas item in _funcTela)
            {
                _retFuncTela = item;
            }

            return _retFuncTela;
        }

        public TOFuncoesTelas CarregarTO(int _idAcFunc, int _idTela)
        {
            CrudDal crud = new CrudDal();
            TOFuncoesTelas _retFuncTela = new TOFuncoesTelas();

            string _sql = string.Format(@"
                SELECT  AcessosFuncoesTelas.IdFuncTela, AcessosFuncoesTelas.IdAcFunc, 
                        AcessosAuxFuncoes.Funcao, AcessosFuncoesTelas.IdTela, 
                        AcessosAuxTelas.Telas, AcessosAuxTelas.CodTela, 
                        AcessosFuncoesTelas.Ativo, AcessosFuncoesTelas.IdOperador, 
                        AcessosFuncoesTelas.IP, AcessosFuncoesTelas.DataCadastro
                FROM    AcessosFuncoesTelas INNER JOIN
                        AcessosAuxFuncoes ON AcessosFuncoesTelas.IdAcFunc = 
                            AcessosAuxFuncoes.IdAcFunc INNER JOIN
                        AcessosAuxTelas ON AcessosFuncoesTelas.IdTela = 
                            AcessosAuxTelas.IdTela
                WHERE   (AcessosFuncoesTelas.IdAcFunc = {0}) AND 
                        (AcessosFuncoesTelas.IdTela = {1}) ",
                _idAcFunc, _idTela);

            var _funcTela = crud.ExecutarComando<TOFuncoesTelas>(_sql);

            foreach (TOFuncoesTelas item in _funcTela)
            {
                _retFuncTela = item;
            }

            return _retFuncTela;
        }

        public List<TOFuncoesTelas> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT  AcessosFuncoesTelas.IdFuncTela, AcessosFuncoesTelas.IdAcFunc, 
                        AcessosAuxFuncoes.Funcao, AcessosFuncoesTelas.IdTela, 
                        AcessosAuxTelas.Telas, AcessosAuxTelas.CodTela, 
                        AcessosFuncoesTelas.Ativo, AcessosFuncoesTelas.IdOperador, 
                        AcessosFuncoesTelas.IP, AcessosFuncoesTelas.DataCadastro
                FROM    AcessosFuncoesTelas INNER JOIN
                        AcessosAuxFuncoes ON AcessosFuncoesTelas.IdAcFunc = 
                            AcessosAuxFuncoes.IdAcFunc INNER JOIN
                        AcessosAuxTelas ON AcessosFuncoesTelas.IdTela = 
                            AcessosAuxTelas.IdTela
                ORDER BY AcessosAuxFuncoes.Funcao, AcessosAuxTelas.Telas";

            var lista = crud.Listar<TOFuncoesTelas>(_sql);

            return lista.ToList();
        }

        public List<TOFuncoesTelas> Listar(int _idAcFunc)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT  AcessosFuncoesTelas.IdFuncTela, AcessosFuncoesTelas.IdAcFunc, 
                        AcessosAuxFuncoes.Funcao, AcessosFuncoesTelas.IdTela, 
                        AcessosAuxTelas.Telas, AcessosAuxTelas.CodTela, 
                        AcessosFuncoesTelas.Ativo, AcessosFuncoesTelas.IdOperador, 
                        AcessosFuncoesTelas.IP, AcessosFuncoesTelas.DataCadastro
                FROM    AcessosFuncoesTelas INNER JOIN
                        AcessosAuxFuncoes ON AcessosFuncoesTelas.IdAcFunc = 
                            AcessosAuxFuncoes.IdAcFunc INNER JOIN
                        AcessosAuxTelas ON AcessosFuncoesTelas.IdTela = 
                            AcessosAuxTelas.IdTela
                WHERE   (AcessosFuncoesTelas.IdAcFunc = {0})
                ORDER BY AcessosAuxFuncoes.Funcao, AcessosAuxTelas.Telas", _idAcFunc);

            var lista = crud.Listar<TOFuncoesTelas>(_sql);

            return lista.ToList();
        }

        public List<TOTela3Bll> ListarTelasNaoCadastradas(int _idAcFunc)
        {
            CrudDal crud = new CrudDal();
            TOTela3Bll obrigBll = new TOTela3Bll();

            string _sql = string.Format(@"
                SELECT	Telas.IdTela As Id, Telas.Telas As Nome, Telas.CodTela As Sigla
                FROM	AcessosAuxTelas AS Telas
                WHERE	(Telas.IdTela Not In  (SELECT	aft.IdTela
                       FROM     AcessosFuncoesTelas AS aft
                       WHERE	(aft.IdAcFunc = {0}) And (aft.Ativo = 1)))
                ORDER BY Telas.CodTela", _idAcFunc);

            var lista = crud.Listar<TOTela3Bll>(_sql);

            return lista.ToList();
        }
    }
}
