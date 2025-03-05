using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public class clPortalContatosBll
    {
        public bllRetorno Inserir(PortalContato _contato)
        {
            bllRetorno ret = _contato.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    crud.Inserir(_contato);

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!!");
            }
        }

        public bllRetorno Alterar(PortalContato _contato)
        {
            bllRetorno ret = _contato.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_contato);

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

        public PortalContato Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            return crud.Carregar<PortalContato>(_id.ToString());
        }

        public bllRetorno Excluir(PortalContato _contato)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_contato);

                msg = bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                msg = bllRetorno.GeraRetorno(true,
                    "Não foi Possível Efetuar a EXCLUSÃO!!!");
            }

            return msg;
        }

        public List<TOPortalContatoBll> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	IdContato, NomeContato, EmailContato, Assunto, Mensagem, 
		                DataEnvio, MsgSituacao, 
		                (Case MsgSituacao
			                When 1 Then 'Enviada'
			                When 2 Then 'Recebida'
			                When 3 Then 'Lida'
			                When 4 Then 'Respondida'
                            When 5 Then 'Arquivada'
		                 End) Situacao, 
		                DataResposta, Observacoes
                FROM	PortalContato
                Where	(MsgSituacao < 5)
                Order By DataEnvio desc";

            var lista = crud.Listar<TOPortalContatoBll>(_sql);

            return lista.ToList();
        }
    }
}
