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
        public static bllRetorno ValidarRegras(this Dietas _dieta,
            bool _insersao)
        {
            if (_dieta.Dieta == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo DIETA deve ser preenchido!");
            }
            else if (_dieta.IdEspecie <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                        "Campo ESPÉCIE deve ser selecionado!");
            }
            else if ((SomaPercent(_dieta) < 0) || (SomaPercent(_dieta) > 100))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        @"A SOMA dos Percentuais deve estar Entre 0 e 100!!!");
            }
            else if ((_insersao) && (RegistroDuplicado(_dieta, true)))
            {
                return bllRetorno.GeraRetorno(false, "Esta Dieta já foi Cadastrada!!!");
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

        private static int SomaPercent(Dietas dieta)
        {
            int soma = Funcoes.Funcoes.ConvertePara.Int(
                dieta.Carboidrato) + Funcoes.Funcoes.ConvertePara.Int(dieta.Proteina) +
                Funcoes.Funcoes.ConvertePara.Int(dieta.Gordura);

            return soma;
        }

        private static bool RegistroDuplicado(Dietas _dieta, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdDieta) Total
                  From Dietas
                  Where (Dieta = '{0}') And 
                        (Ativo = {1})", _dieta.Dieta,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
