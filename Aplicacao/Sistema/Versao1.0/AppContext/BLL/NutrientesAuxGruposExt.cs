using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clNutrientesAuxGruposExt
    {
        public static bllRetorno ValidarRegras(this NutrientesAuxGrupo _grupo,
            bool _insersao)
        {
            bool regReativ = false;

            if (_grupo.Grupo == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo GRUPO DE NUTRIENTES deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_grupo.Grupo, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_grupo.Grupo, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_grupo);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(NutrientesAuxGrupo _grupo)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update NutrientesAuxGrupos Set
                    Ativo = 1
                Where (Grupo = '{0}') ", _grupo.Grupo));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _fonte, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdGrupo) Total
                  From NutrientesAuxGrupos
                  Where (Grupo = '{0}') And (Ativo = {1})", _fonte,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
