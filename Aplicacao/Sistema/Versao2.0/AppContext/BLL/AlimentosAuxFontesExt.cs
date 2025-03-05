using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAlimentosAuxFontesExt
    {
        public static bllRetorno ValidarRegras(this AlimentosAuxFonte _fonte,
            bool _insersao)
        {
            bool regReativ = false;

            if (_fonte.Fonte == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo FONTE deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_fonte.Fonte, true)))
            {
                return bllRetorno.GeraRetorno(false, "Esta Fonte já foi Cadastrada!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_fonte.Fonte, false)))
            {
                regReativ = EfetuaUpdate(_fonte);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AlimentosAuxFonte _fonte)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AlimentosAuxFontes Set
                    Ativo = 1
                Where (Fonte = '{0}') ", _fonte.Fonte));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _fonte, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdFonte) Total
                  From AlimentosAuxFontes
                  Where (Fonte = '{0}') And 
                        (Ativo = {1})", _fonte, (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
