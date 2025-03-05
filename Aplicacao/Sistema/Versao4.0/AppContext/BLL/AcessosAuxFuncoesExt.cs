using DCL;
using DAL;
using BLL.Properties;

namespace BLL
{
    public static class clAcessoAuxFuncoesExt
    {
        public static bllRetorno ValidarRegras(this AcessosAuxFuncoes _funcao,
            bool _insersao)
        {
            bool regReativ = false;

            if (_funcao.Funcao == "")
            {
                return
                    bllRetorno.GeraRetorno(false, string.Format(Resources.CampoPreenchido, 
                        Resources.cpFuncao));
            }
            else if ((_insersao) && (RegistroDuplicado(_funcao.Funcao, true)))
            {
                return bllRetorno.GeraRetorno(false, Resources.RegDuplicado);
            }

            if ((_insersao) && (RegistroDuplicado(_funcao.Funcao, false)))
            {
                regReativ = EfetuaUpdate(_funcao);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? Resources.DadosValidos : 
                Resources.RegReativado));
        }

        private static bool EfetuaUpdate(AcessosAuxFuncoes funcao)
        {
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

            string _sql = string.Format(@"
                Update AcessosAuxFuncoes Set
                    Ativo = 1
                Where (Funcao = '{0}') ", funcao.Funcao);

            int _upd = crud.ExecutarComando(_sql);
            crud.Dispose();

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _funcao, bool? _ativo)
        {
            CrudsDal<AcessosAuxFuncoes> crud = new CrudsDal<AcessosAuxFuncoes>();

            string _sql = string.Format(
                @"Select Count(IdAcFunc) Total
                  From AcessosAuxFuncoes
                  Where (Funcao = '{0}') And (Ativo = {1})", 
                _funcao,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);
            crud.Dispose();

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
