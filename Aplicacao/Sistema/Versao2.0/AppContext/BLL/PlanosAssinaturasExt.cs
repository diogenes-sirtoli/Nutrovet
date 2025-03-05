using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clPlanosAssinaturasExt
    {
        public static bllRetorno ValidarRegras(this PlanosAssinatura _plano,
            bool _insersao)
        {
            bool regReativ = false;

            if (_plano.dNomePlano < 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PLANO deve ser selecionado!");
            }
            else if (_plano.ValorPlano <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo VALOR DO PLANO deve ser preenchido!");
            }
            else if (_plano.dPlanoTp <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TIPO DE PLANO deve ser selecionado!");
            }
            else if (_plano.dPeriodo <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PERÍODO DO PLANO deve ser selecionado!");
            }
            else if (_plano.QtdAnimais <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo QUANTIDADE DE ANIMAIS deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_plano, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_plano, false)))
            {
                regReativ = EfetuaUpdate(_plano);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(PlanosAssinatura _plano)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update PlanosAssinaturas Set
                    Ativo = 1
                Where (dNomePlano = {0}) And (dPeriodo = {1}) ", 
                _plano.dNomePlano, _plano.dPeriodo));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(PlanosAssinatura _plano, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdPlano) Total
                  From PlanosAssinaturas
                  Where (dNomePlano = {0}) And (dPeriodo = {1}) AND 
                        (Ativo = {2})", _plano.dNomePlano, _plano.dPeriodo,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
