using DCL;
using DAL;

namespace BLL
{
    public static class clAtendimentoAuxTiposExt
    {
        public static bllRetorno ValidarRegras(this AtendimentoAuxTipo _tipo,
            bool _insersao)
        {
            bool regReativ = false;

            if (_tipo.TipoAtendimento == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TIPO DE ATENDIMENTO deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_tipo.TipoAtendimento, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_tipo.TipoAtendimento, false)))
            {
                regReativ = EfetuaUpdate(_tipo);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AtendimentoAuxTipo _tp)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AtendimentoAuxTipos Set
                    Ativo = 1
                Where (TipoAtendimento = '{0}') ", _tp.TipoAtendimento));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _tpAtend, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdTpAtend) Total
                  From AtendimentoAuxTipos
                  Where (TipoAtendimento = '{0}') And (Ativo = {1})", 
                _tpAtend, (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
