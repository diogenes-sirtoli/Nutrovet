using DCL;
using DAL;

namespace BLL
{
    public static class clConfigReceituarioExt
    {
        public static bllRetorno ValidarRegras(this ConfigReceituario _config)
        {
            if (_config.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ASSINANTE deve ser selecionado!");
            }
            else if (_config.NomeClinica == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME DA CLÍNICA/VETERINÁRIO deve ser Preenchido!");
            }
            else if (_config.Email == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL DA CLÍNICA/VETERINÁRIO deve ser Preenchido!");
            }
            else if (_config.Logr_CEP == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CEP DA CLÍNICA/VETERINÁRIO deve ser Preenchido!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }
    }
}
