using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clDietasAlimentosExt
    {
        public static bllRetorno ValidarRegras(this DietasAlimento _dietaAlim,
            bool _insersao)
        {
            if (_dietaAlim.IdAlimento <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                        "Campo ALIMENTO deve ser selecionado!");
            }
            else if (_dietaAlim.IdDieta <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                        "Campo DIETA deve ser selecionado!");
            }
            else if (_dietaAlim.IdTpIndicacao <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo INDICAÇÃO deve ser selecionado!");
            }
            else if ((_insersao) && (RegistroDuplicado(_dietaAlim)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_dietaAlim.Quant <= 0)
            {
                _dietaAlim.Quant = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(DietasAlimento _dietaAlim)
        {
            CrudDal crud = new CrudDal();

            bool retorno = false;
            string _sql = string.Format(@"
                SELECT  COUNT(IdDietaAlim) AS Total
                FROM    DietasAlimentos
                WHERE   (IdAlimento = {0}) AND (IdDieta = {1})", 
                        _dietaAlim.IdAlimento, _dietaAlim.IdDieta);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
