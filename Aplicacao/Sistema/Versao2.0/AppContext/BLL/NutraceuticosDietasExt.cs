using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clNutraceuticosDietasExt
    {
        public static bllRetorno ValidarRegras(this NutraceuticosDieta _NutrDie,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_NutrDie.IdNutrac <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo NUTRACÊUTICO deve ser selecionado!");
            }
            else if (_NutrDie.IdDieta <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DIETA deve ser selecionado!");
            }
            else if ((_insersao) && (RegistroDuplicado(_NutrDie)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(NutraceuticosDieta _nutrDie)
        {
            CrudDal crud = new CrudDal();
            bool retorno;
            string _sql = string.Format(@"
                SELECT  COUNT(IdNutDie) AS Total
                FROM    NutraceuticosDietas
                WHERE   (IdNutrac = {0}) AND (IdDieta = {1})", 
                        _nutrDie.IdNutrac, _nutrDie.IdDieta);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
