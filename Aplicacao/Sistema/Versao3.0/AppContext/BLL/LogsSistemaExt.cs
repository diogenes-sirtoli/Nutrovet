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
    public static class clLogsSistemaExt
    {
        public static bllRetorno ValidarRegras(this LogsSistema _log)
        {
            /*if (_log.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CLIENTE deve ser selecionado!");
            }
            else */if (_log.Mensagem == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "campo MENSAGEM deve ser preenchido!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }
    }
}
