using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.Security;
using System.Web;

namespace BLL
{
    public static class clAnimaisExt
    {
        private static clAnimaisBll animaisBll = new clAnimaisBll();
        
        public static bllRetorno ValidarRegras(this Animai _animal,
            bool _insersao)
        {
            if (_animal.Nome == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME deve ser preenchido!");
            }
            else if (_animal.IdPessoa <= 0)
            {
                return 
                    bllRetorno.GeraRetorno(false,
                        "Campo TUTOR deve ser Selecionado!");
            }
            else if (_animal.DtNascim <= DateTime.Parse("01/01/1910"))
            {
                return
                     bllRetorno.GeraRetorno(false,
                         "Campo DATA DE NASCIMENTO deve ser preenchido!");
            }
            else if (_animal.IdRaca <= 0)
            {
                return
                     bllRetorno.GeraRetorno(false,
                         "Campo RAÇA deve ser selecionado!");
            }
            else if (_animal.Sexo <= 0)
            {
                return
                     bllRetorno.GeraRetorno(false,
                         "Campo SEXO deve ser selecionado!");
            }
            else if (_animal.PesoAtual <= 0)
            {
                return
                     bllRetorno.GeraRetorno(false,
                         "Campo PESO ATUAL deve ser preenchido!");
            }
            else if (_animal.PesoIdeal <= 0)
            {
                return
                     bllRetorno.GeraRetorno(false,
                         "Campo PESO IDEAL deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_animal, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(Animai _animal, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select    Count(IdAnimal) Total
                  From      Animais
                  Where     (Nome = '{0}') And (IdPessoa = {1}) And 
                            (Ativo = {1})", _animal.Nome, _animal.IdPessoa,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
