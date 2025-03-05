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
    public static class clReceituarioExt
    {
        public static bllRetorno ValidarRegras(this Receituario _rec)
        {
            if (_rec.IdCardapio <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CARDÁPIO deve ser selecionado!");
            }
            else if (_rec.dTpRec <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TIPO DE RECEITA deve ser selecionado!");
            }
            else if (_rec.DataReceita == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DATA DA RECEITA deve ser preenchido!");
            }

            if (Funcoes.Funcoes.ConvertePara.Int(_rec.dTblExigNutr) <= 0)
            {
                _rec.dTblExigNutr = null;
            }

            if (Funcoes.Funcoes.ConvertePara.Int(_rec.QuantDietas) <= 0)
            {
                _rec.QuantDietas = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }
    }
}
