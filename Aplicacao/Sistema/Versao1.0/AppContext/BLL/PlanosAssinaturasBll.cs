using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
    public class clPlanosAssinaturasBll
    {
        public bllRetorno Inserir(PlanosAssinatura _plano)
        {
            bllRetorno ret = _plano.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_plano);
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

        public bllRetorno Alterar(PlanosAssinatura _plano)
        {
            bllRetorno ret = _plano.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_plano);

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

        public PlanosAssinatura Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            return crud.Carregar<PlanosAssinatura>(_id.ToString());
        }

        public PlanosAssinatura Carregar(string _plano)
        {
            CrudDal crud = new CrudDal();
            PlanosAssinatura _retorno = new PlanosAssinatura();

            string _sql = string.Format(
                @"Select *
                  From PlanosAssinaturas
                  Where (Plano = '{0}')", _plano);

            var ret = crud.ExecutarComando<PlanosAssinatura>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public bllRetorno Excluir(PlanosAssinatura _plano)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_plano);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                msg = Alterar(_plano);
            }

            return msg;
        }

        public List<TOTela3Bll> Listar()
        {
            CrudDal crud = new CrudDal();

            var lista = from l in crud.Listar<PlanosAssinatura>()
                        where l.Ativo == true
                        orderby l.Plano
                        select new TOTela3Bll
                        {
                            Id = l.IdPlano,
                            Nome = l.Plano,
                            Ativo = l.Ativo,
                            IdOperador = l.IdOperador,
                            IP = l.IP,
                            DataCadastro = l.DataCadastro
                        };

            return lista.ToList();
        }

        public ListItem[] ListarPeriodo()
        {
            ListItem[] periodo = Funcoes.Funcoes.GetEnumList<DominiosBll.Periodo>();

            return periodo;
        }
    }
}
