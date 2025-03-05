using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clPlanosAssinaturasExt
    {
        public static bllRetorno ValidarRegras(this PlanosAssinatura _plano,
            bool _insersao)
        {
            bool regReativ = false;

            if (_plano.Plano == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PLANO deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_plano.Plano, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_plano.Plano, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_plano);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(PlanosAssinatura _plano)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update PlanosAssinaturas Set
                    Ativo = 1
                Where (Plano = '{0}') ", _plano.Plano));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _plano, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdPlano) Total
                  From PlanosAssinaturas
                  Where (Plano = '{0}') And (Ativo = {1})", _plano,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
