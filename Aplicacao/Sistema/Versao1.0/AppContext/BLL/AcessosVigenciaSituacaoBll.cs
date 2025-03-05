using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
    public class clAcessosVigenciaSituacaoBll
    {
        public bllRetorno Inserir(AcessosVigenciaSituacao _vigSit)
        {
            bllRetorno ret = _vigSit.ValidarRegras(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_vigSit);

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

        public bllRetorno Alterar(AcessosVigenciaSituacao _vigSit)
        {
            bllRetorno ret = _vigSit.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_vigSit);

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

        public bllRetorno Excluir(AcessosVigenciaSituacao _vigSit)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_vigSit);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false, 
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public AcessosVigenciaSituacao Carregar(int _idVigSit)
        {
            CrudDal crud = new CrudDal();

            AcessosVigenciaSituacao _usuerFunc = null;

            var logon = crud.ExecutarComando<AcessosVigenciaSituacao>(string.Format(@"
                Select *
                From AcessosVigenciaSituacao
                Where (IdIdVigSit = {0})", _idVigSit));

            foreach (var item in logon)
            {
                _usuerFunc = item;
            }

            return _usuerFunc;
        }

        public List<TOAcessosVigenciaSituacaoBll> ListarTO(int _vigencia)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT	IdVigencia, IdVigSit, IdSituacao, 
		                (Case IdSituacao
			                When 1 Then 'Pago'
			                When 2 Then 'Permitido'
			                When 3 Then 'Cancelado'
			                When 4 Then 'Alterado'
			                When 5 Then 'Renovado'
		                 End) Situacao,
		                DataSituacao, Ativo, IdOperador, IP, DataCadastro
                FROM	AcessosVigenciaSituacao
                WHERE	(IdVigencia = {0})
                ORDER BY Situacao",
                _vigencia);

            var lista = crud.Listar<TOAcessosVigenciaSituacaoBll>(_sql);

            return lista.ToList();
        }

        public ListItem[] ListarSituacao()
        {
            ListItem[] situacao = Funcoes.Funcoes.GetEnumList<
                DominiosBll.AcessosPlanosAuxSituacao>();

            return situacao;
        }
    }
}
