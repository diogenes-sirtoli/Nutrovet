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
            bllRetorno _podeCadastrar = animaisBll.PodeCadastrarAnimal(
                    _animal.IdOperador.Value);

            if ((!_podeCadastrar.retorno) && (_insersao))
            {
                return
                    bllRetorno.GeraRetorno(_podeCadastrar.retorno,
                        _podeCadastrar.mensagem);
            }
            else if (_animal.Nome == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME deve ser preenchido!");
            }
            else if (_animal.IdTutores <= 0)
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
            else if ((_insersao) && (RegistroDuplicado(_animal, true)))
            {
                return bllRetorno.GeraRetorno(false,
                    "Este Paciente já foi Cadastrado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(Animai _animal, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select    Count(IdAnimal) Total
                  From      Animais
                  Where     (Nome = '{0}') And (IdTutores = {1}) And 
                            (Ativo = {2})", _animal.Nome, _animal.IdTutores,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
