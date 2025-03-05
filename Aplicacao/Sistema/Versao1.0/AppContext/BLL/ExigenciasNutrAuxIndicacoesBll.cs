using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clExigenciasNutrAuxIndicacoesBll
    {
        public bllRetorno Inserir(ExigenciasNutrAuxIndicacoe _indic)
        {
            bllRetorno ret = _indic.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_indic);
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

        public bllRetorno Alterar(ExigenciasNutrAuxIndicacoe _indic)
        {
            bllRetorno ret = _indic.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_indic);

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

        public ExigenciasNutrAuxIndicacoe Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            ExigenciasNutrAuxIndicacoe _indicacoes = null;

            var consulta = crud.ExecutarComando<ExigenciasNutrAuxIndicacoe>(
                string.Format(@"
                Select *
                From ExigenciasNutrAuxIndicacoes
                Where (IdIndic = {0})", _id));

            foreach (var item in consulta)
            {
                _indicacoes = item;
            }

            return _indicacoes;
        }

        public ExigenciasNutrAuxIndicacoe Carregar(string _indic)
        {
            CrudDal crud = new CrudDal();
            ExigenciasNutrAuxIndicacoe _retorno = new ExigenciasNutrAuxIndicacoe();

            string _sql = string.Format(
                @"Select *
                  From ExigenciasNutrAuxIndicacoes
                  Where (Indicacao = '{0}')", _indic);

            var ret = crud.ExecutarComando<ExigenciasNutrAuxIndicacoe>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(ExigenciasNutrAuxIndicacoe _indic)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_indic);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                msg = Alterar(_indic);
            }

            return msg;
        }

        public List<TOTela3Bll> Listar()
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<ExigenciasNutrAuxIndicacoe>()
                        where l.Ativo == true
                        orderby l.Indicacao
                        select new TOTela3Bll
                        {
                            Id = l.IdIndic,
                            Nome = l.Indicacao,
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
                            SELECT	ROW_NUMBER() OVER(ORDER BY Indicacao) AS NUMBER,
                                    IdIndic As Id, Indicacao As Nome, '' As Sigla,
		                            Ativo, IdOperador, IP, DataCadastro
                            FROM	ExigenciasNutrAuxIndicacoes
                            WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                                    (Indicacao Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
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
                SELECT	Count(IdIndic) Total
                FROM	ExigenciasNutrAuxIndicacoes
                WHERE   (Ativo = 1)";

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (Indicacao LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", 
                         _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }
    }
}
