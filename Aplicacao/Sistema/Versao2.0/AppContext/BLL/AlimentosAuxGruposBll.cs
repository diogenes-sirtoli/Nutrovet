using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clAlimentosAuxGruposBll
    {
        public bllRetorno Inserir(AlimentosAuxGrupo _grupo)
        {
            bllRetorno ret = _grupo.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_grupo);
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

        public bllRetorno Alterar(AlimentosAuxGrupo _grupo)
        {
            bllRetorno ret = _grupo.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_grupo);

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

        public AlimentosAuxGrupo Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            return crud.Carregar<AlimentosAuxGrupo>(_id.ToString());
        }

        public AlimentosAuxGrupo Carregar(string _grupo)
        {
            CrudDal crud = new CrudDal();
            AlimentosAuxGrupo _retorno = new AlimentosAuxGrupo();

            string _sql = string.Format(
                @"Select *
                  From AlimentosAuxGrupos
                  Where (GrupoAlimento = '{0}')", _grupo);

            var ret = crud.ExecutarComando<AlimentosAuxGrupo>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(AlimentosAuxGrupo _grupo)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_grupo);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                msg = Alterar(_grupo);
            }

            return msg;
        }

        public List<TOTela3Bll> Listar()
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<AlimentosAuxGrupo>()
                        where l.Ativo == true
                        orderby l.GrupoAlimento
                        select new TOTela3Bll
                        {
                            Id = l.IdGrupo,
                            Nome = l.GrupoAlimento,
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
                            SELECT	ROW_NUMBER() OVER(ORDER BY GrupoAlimento) AS NUMBER,
                                    IdGrupo As Id, GrupoAlimento As Nome, '' As Sigla,
		                            Ativo, IdOperador, IP, DataCadastro
                            FROM	AlimentosAuxGrupos
                            WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                                    (GrupoAlimento Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
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
                SELECT	Count(IdGrupo) Total
                FROM	AlimentosAuxGrupos
                WHERE   (Ativo = 1)";

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (GrupoAlimento LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", 
                         _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }
    }
}
