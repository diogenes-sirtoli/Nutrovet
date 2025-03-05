using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clNutraceuticosExt
    {
        public static bllRetorno ValidarRegras(this Nutraceutico _nutra,
            bool _insersao)
        {
            if (_nutra.IdNutr <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NUTRIENTE deve ser selecionado!");
            }
            else if (_nutra.IdEspecie <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                    "Campo ESPÉCIE deve ser selecionado!");
            }
            else if ((RegistroDuplicado(_nutra, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_nutra.DoseMin <= 0)
            {
                _nutra.DoseMin = null;
            }
            if (_nutra.IdUnidMin <= 0)
            {
                _nutra.IdUnidMin = null;
            }
            if (_nutra.DoseMax <= 0)
            {
                _nutra.DoseMax = null;
            }
            if (_nutra.IdUnidMax <= 0)
            {
                _nutra.IdUnidMax = null;
            }
            if (_nutra.IdPrescr1 <= 0)
            {
                _nutra.IdPrescr1 = null;
            }
            if (_nutra.IdPrescr2 <= 0)
            {
                _nutra.IdPrescr2 = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(Nutraceutico _nutra, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select    Count(IdNutrac) Total
                  From      Nutraceuticos
                  Where     (IdEspecie = {0}) And (IdNutr = {1}) AND
                            (Ativo = {2})", _nutra.IdEspecie, _nutra.IdNutr,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
