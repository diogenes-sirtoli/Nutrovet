using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clNutrientesAuxGruposBll
    {
        public bllRetorno Inserir(NutrientesAuxGrupo _grupo)
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

        public bllRetorno Alterar(NutrientesAuxGrupo _grupo)
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

        public NutrientesAuxGrupo Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            return crud.Carregar<NutrientesAuxGrupo>(_id.ToString());
        }

        public NutrientesAuxGrupo Carregar(string _grupo)
        {
            CrudDal crud = new CrudDal();
            NutrientesAuxGrupo _retorno = new NutrientesAuxGrupo();

            string _sql = string.Format(
                @"Select *
                  From NutrientesAuxGrupos
                  Where (Grupo = '{0}')", _grupo);

            var ret = crud.ExecutarComando<NutrientesAuxGrupo>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(NutrientesAuxGrupo _grupo)
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
                msg = Alterar(_grupo);
            }

            return msg;
        }

        public List<NutrientesAuxGrupo> Listar()
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<NutrientesAuxGrupo>()
                        where l.Ativo == true
                        orderby l.Ordem
                        select l;

            return lista.ToList();
        }

        public List<NutrientesAuxGrupo> Listar(bool _listarEmAlim)
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<NutrientesAuxGrupo>()
                        where (l.Ativo == true) && (l.ListarEmAlim == _listarEmAlim)
                        orderby l.Ordem
                        select l;

            return lista.ToList();
        }

        public List<NutrientesAuxGrupo> Listar(string _pesqNome, int _tamPag, 
            int _pagAtual)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                DECLARE @PageNumber AS INT, @RowspPage AS INT
                SET @PageNumber = {1}
                SET @RowspPage = {0}                 

                SELECT  IdGrupo, Grupo, Ordem, ListarEmAlim, Ativo, IdOperador, 
                        IP, DataCadastro
                FROM    (
                            SELECT	ROW_NUMBER() OVER(ORDER BY Ordem) AS NUMBER,
                                    IdGrupo, Grupo, Ordem, ListarEmAlim,
		                            Ativo, IdOperador, IP, DataCadastro
                            FROM	NutrientesAuxGrupos
                            WHERE   (Ativo = 1) ", _tamPag, _pagAtual);

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                                    (Grupo Like '%{0}%' COLLATE Latin1_General_CI_AI)", 
                                    _pesqNome);
            }

            _sql += @") AS TBL
                WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
                ORDER BY    Ordem";

            var lista = crud.Listar<NutrientesAuxGrupo>(_sql).OrderBy(
                            o => Funcoes.Funcoes.ConvertePara.Int(o.Ordem));

            return lista.ToList();
        }

        public int TotalPaginas(string _pesqNome, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	Count(IdGrupo) Total
                FROM	NutrientesAuxGrupos
                WHERE   (Ativo = 1)";

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (Grupo LIKE '%{0}%' COLLATE Latin1_General_CI_AI)", _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }
    }
}
