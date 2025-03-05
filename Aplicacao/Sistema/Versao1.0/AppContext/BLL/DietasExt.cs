using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clDietasExt
    {
        public static bllRetorno ValidarRegras(this Dieta _dieta,
            bool _insersao)
        {
            /*if (_dieta.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CLIENTE deve ser selecionado!");
            }
            else*/ if (_dieta.Dieta1 == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DIETA deve ser preenchido!");
            }
            else if ((SomaPercent(_dieta) < 0) || (SomaPercent(_dieta) > 100))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        @"A SOMA dos Percentuais deve estar Entre 0 e 100!!!");
            }
            else if ((RegistroDuplicado(_dieta, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_dieta.Carboidrato <= 0)
            {
                _dieta.Carboidrato = null;
            }
            if (_dieta.Proteina <= 0)
            {
                _dieta.Proteina = null;
            }
            if (_dieta.Gordura <= 0)
            {
                _dieta.Gordura = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static int SomaPercent(Dieta dieta)
        {
            int soma = Funcoes.Funcoes.ConvertePara.Int(
                dieta.Carboidrato) + Funcoes.Funcoes.ConvertePara.Int(dieta.Proteina) +
                Funcoes.Funcoes.ConvertePara.Int(dieta.Gordura);

            return soma;
        }

        private static bool RegistroDuplicado(Dieta _dieta, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdDieta) Total
                  From Dietas
                  Where (Dieta = '{0}') And 
                        (Ativo = {1})", _dieta.Dieta1,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
