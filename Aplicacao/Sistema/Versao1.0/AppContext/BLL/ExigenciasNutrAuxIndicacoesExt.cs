using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clExigenciasNutrAuxIndicacoesExt
    {
        public static bllRetorno ValidarRegras(this ExigenciasNutrAuxIndicacoe _indic,
            bool _insersao)
        {
            bool regReativ = false;

            if (_indic.Indicacao == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo INDICAÇÃO deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_indic.Indicacao, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_indic.Indicacao, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_indic);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(ExigenciasNutrAuxIndicacoe _indic)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update ExigenciasNutrAuxIndicacoes Set
                    Ativo = 1
                Where (Indicacao = '{0}') ", _indic.Indicacao));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _tela, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdIndic) Total
                  From ExigenciasNutrAuxIndicacoes
                  Where (Indicacao = '{0}') And (Ativo = {1})", _tela,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
