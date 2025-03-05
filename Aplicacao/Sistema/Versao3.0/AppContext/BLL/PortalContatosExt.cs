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
    public static class clPortalContatosExt
    {
        public static bllRetorno ValidarRegras(this PortalContato _contato,
            bool _insersao)
        {
            if (_contato.NomeContato == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME deve ser preenchido!");
            }
            else if (_contato.EmailContato == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL deve ser preenchido!");
            }
            else if (_contato.Assunto == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ASSUNTO deve ser preenchido!");
            }
            else if (_contato.Mensagem == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo MENSSAGEM deve ser preenchido!");
            }
            else if (!EmailValido(_contato))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Digite um E-Mail Válido!");
            }

            if (_contato.DataEnvio == DateTime.Parse("01/01/1910"))
            {
                _contato.DataEnvio = null;
            }
            if (_contato.DataResposta == DateTime.Parse("01/01/1910"))
            {
                _contato.DataResposta = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool EmailValido(PortalContato _contato)
        {
            string email = _contato.EmailContato;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            return match.Success;
        }
    }
}
