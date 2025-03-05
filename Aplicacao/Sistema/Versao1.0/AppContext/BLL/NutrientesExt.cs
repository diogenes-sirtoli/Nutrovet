using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clNutrientesExt
    {
        public static bllRetorno ValidarRegras(this Nutriente _nutri,
            bool _insersao)
        {
            bool regReativ = false;

            if (_nutri.Nutriente1 == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NUTRIENTE deve ser preenchido!");
            }
            else if (_nutri.IdUnidade <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                    "Campo UNIDADE DE MEDIDA deve ser selecionado!");
            }
            else if (_nutri.IdGrupo <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo GRUPO DO NUTRIENTE deve ser selecionado!");
            }
            else if ((RegistroDuplicado(_nutri.Nutriente1, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_nutri.Nutriente1, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_nutri);
            }

            if (_nutri.ValMin <= 0)
            {
                _nutri.ValMin = null;
            }
            if (_nutri.ValMax <= 0)
            {
                _nutri.ValMax = null;
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(Nutriente _nutri)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update Nutrientes Set
                    Ativo = 1
                Where (Nutriente = '{0}') ", _nutri.Nutriente1));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _nutri, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdNutr) Total
                  From Nutrientes
                  Where (Nutriente = '{0}') And (Ativo = {1})", _nutri,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
