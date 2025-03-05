using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace BLL
{
    public class clAlimentosAuxAliasBll
    {
        public bllRetorno Inserir(AlimentosAuxAlia _alias)
        {
            bllRetorno ret = _alias.ValidarRegras();

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_alias);

                    return bllRetorno.GeraRetorno(true,
                        "INCLUSÃO do ALIAS Efetuada com Sucesso!!!");
                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false,
                        "Não foi Possível Efetuar a INCLUSÃO do ALIAS!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno Alterar(AlimentosAuxAlia _alias)
        {
            bllRetorno ret = _alias.ValidarRegras();

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_alias);

                    return bllRetorno.GeraRetorno(true, 
                        "ALTERAÇÃO do ALIAS Efetuada com Sucesso!!!");
                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false, 
                        "Não foi Possível Efetuar a ALTERAÇÃO do ALIAS!!!");
                }
            }
            else
            {
                return ret;
            }
        }

        public AlimentosAuxAlia Carregar(int _id)
        {
            CrudDal crud = new CrudDal();

            AlimentosAuxAlia _alias = null;

            var consulta = crud.ExecutarComando<AlimentosAuxAlia>(
                string.Format($@"
                SELECT	IdPessoa, IdAlias, IdAlimento, Alias, Ativo, 
		                IdOperador, IP, DataCadastro, Versao
                FROM	AlimentosAuxAlias
                WHERE	(IdAlias = {_id})"));

            foreach (AlimentosAuxAlia item in consulta)
            {
                _alias = item;
            }

            return _alias;
        }

        public AlimentosAuxAlia Carregar(int _idPessoa, int _idAlimento)
        {
            CrudDal crud = new CrudDal();
            AlimentosAuxAlia _alias = null;

            string _sql = string.Format($@"
                SELECT	IdPessoa, IdAlias, IdAlimento, Alias, Ativo, 
		                IdOperador, IP, DataCadastro, Versao
                FROM	AlimentosAuxAlias
                WHERE	(IdPessoa = {_idPessoa}) AND
                        (IdAlimento = {_idAlimento})");

            var consulta = crud.ExecutarComando<AlimentosAuxAlia>(_sql);

            foreach (AlimentosAuxAlia item in consulta)
            {
                _alias = item;
            }

            return _alias;
        }

        public bllRetorno Excluir(AlimentosAuxAlia _alias)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg;

            try
            {
                crud.Excluir(_alias);

                msg = bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO do ALIAS Efetuada com Sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                msg = bllRetorno.GeraRetorno(true,
                    "Não foi Possível Efetuar a EXCLUSÃO do ALIAS!!!");
            }

            return msg;
        }
    }
}
