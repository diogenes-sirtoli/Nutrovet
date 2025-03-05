using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clDietasBll
    {
        public bllRetorno Inserir(Dieta _dieta)
        {
            bllRetorno ret = _dieta.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_dieta);

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");

                }
                catch (Exception err)
                {
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a INSERÇÃO!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno Alterar(Dieta _dieta)
        {
            bllRetorno ret = _dieta.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_dieta);

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

        public Dieta Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            Dieta _dietas = null;

            var consulta = crud.ExecutarComando<Dieta>(string.Format(@"
                Select *
                From Dietas
                Where (IdDieta = {0})", _id));

            foreach (var item in consulta)
            {
                _dietas = item;
            }

            return _dietas;
        }

        public Dieta Carregar(string _dieta)
        {
            CrudDal crud = new CrudDal();
            Dieta _retorno = new Dieta();

            string _sql = string.Format(
                @"Select *
                  From Dietas
                  Where (Dieta = '{0}')", _dieta);

            var ret = crud.ExecutarComando<Dieta>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(Dieta _dieta)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_dieta);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                msg = Alterar(_dieta);
            }

            return msg;
        }

        public List<TODietasBll> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT  Dietas.IdPessoa, Pessoas.Nome, Dietas.IdDieta, Dietas.Dieta, 
                        Dietas.Carboidrato, Dietas.Proteina, Dietas.Gordura, 
                        Dietas.Ativo, Dietas.IdOperador, Dietas.IP, Dietas.DataCadastro
                FROM    Dietas INNER JOIN
                            Pessoas ON Dietas.IdPessoa = Pessoas.IdPessoa
                WHERE   (Dietas.Ativo = 1) 
                ORDER BY Dietas.Dieta";

            var lista = crud.Listar<TODietasBll>(_sql);

            return lista.ToList();
        }

        public List<TODietasBll> Listar(string _pesqNome, int _idPessoa, int _tamPag,
            int _pagAtual)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                DECLARE @PageNumber AS INT, @RowspPage AS INT
                SET @PageNumber = {1}
                SET @RowspPage = {0}                 

                SELECT  IdPessoa, Nome, IdDieta, Dieta, Carboidrato, Proteina, 
                        Gordura, Ativo, IdOperador, IP, DataCadastro
                FROM    (
                            SELECT	ROW_NUMBER() OVER(ORDER BY Dietas.Dieta) AS NUMBER, 
                                    Dietas.IdPessoa, Pessoas.Nome, Dietas.IdDieta, 
                                    Dietas.Dieta, Dietas.Carboidrato, Dietas.Proteina, 
                                    Dietas.Gordura, Dietas.Ativo, Dietas.IdOperador, 
                                    Dietas.IP, Dietas.DataCadastro
                            FROM    Dietas INNER JOIN
                                        Pessoas ON Dietas.IdPessoa = Pessoas.IdPessoa
                            WHERE   (Dietas.Ativo = 1) ",
                            _tamPag, _pagAtual);

            //if (_idPessoa > 0)
            //{
            //    _sql += string.Format(@" AND 
            //                        (Dietas.IdPessoa = {0})", _idPessoa);
            //}

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                                    (Dietas.Dieta Like '%{0}%' COLLATE Latin1_General_CI_AI)",
                                    _pesqNome);
            }

            _sql += @") AS TBL
                WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
                ORDER BY    Dieta";

            var lista = crud.Listar<TODietasBll>(_sql);

            return lista.ToList();
        }

        public int TotalPaginas(string _pesqNome, int _idPessoa, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	Count(IdDieta) Total
                FROM	Dietas
                WHERE   (Ativo = 1)";

            //if (_idPessoa > 0)
            //{
            //    _sql += string.Format(@" AND 
            //            (IdPessoa = {0})", _idPessoa);
            //}

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (Dieta LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }
    }
}
