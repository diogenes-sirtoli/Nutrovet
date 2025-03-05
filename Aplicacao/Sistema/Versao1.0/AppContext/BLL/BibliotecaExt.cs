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
    public static class clBibliotecaExt
    {
        public static bllRetorno ValidarRegras(this Biblioteca _biblio,
            bool _insersao)
        {
            if (_biblio.IdSecao <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SEÇÃO deve ser selecionado!");
            }
            else if (_biblio.NomeArq == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "campo TÍTULO ARQUIVO deve ser preenchido!");
            }
            else if (_biblio.Caminho == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "ARQUIVO deve ser selecionado!");
            }
            else if (_biblio.NomeArq.Length > 300)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "campo TÍTULO ARQUIVO deve conter até 300 Caracteres!");
            }
            else if (_biblio.Descricao.Length > 500)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "campo DESCRIÇÃO deve conter até 500 Caracteres!");
            }
            else if (_biblio.Autor.Length > 300)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "campo AUTOR deve conter até 300 Caracteres!");
            }

            if (_biblio.Ano <= 0)
            {
                _biblio.Ano = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }
    }
}
