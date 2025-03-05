using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAlimentosAuxGruposExt
    {
        public static bllRetorno ValidarRegras(this AlimentosAuxGrupo _grupo,
            bool _insersao)
        {
            bool regReativ = false;

            if (_grupo.GrupoAlimento == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo GRUPO DE ALIMENTOS deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_grupo.GrupoAlimento, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_grupo.GrupoAlimento, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_grupo);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AlimentosAuxGrupo _grupo)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AlimentosAuxGrupos Set
                    Ativo = 1
                Where (GrupoAlimento = '{0}') ", 
                _grupo.GrupoAlimento));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _fonte, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdGrupo) Total
                  From AlimentosAuxGrupos
                  Where (GrupoAlimento = '{0}') And 
                        (Ativo = {1})", _fonte,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
