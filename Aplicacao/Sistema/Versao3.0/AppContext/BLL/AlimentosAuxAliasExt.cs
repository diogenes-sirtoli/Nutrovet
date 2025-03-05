using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Text.RegularExpressions;

namespace BLL
{
    public static class clAlimentosAuxAliasExt
    {
        public static bllRetorno ValidarRegras(this AlimentosAuxAlia _alias)
        {
            if (_alias.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo USUÁRIO deve ser selecionado!");
            }
            else if (_alias.IdAlimento <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ALIMENTO deve ser selecionado!");
            }
            else if (_alias.Alias == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "campo ALIAS deve ser preenchido!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }
    }
}
