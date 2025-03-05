using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clAlimentosNutrientesExt
    {
        public static bllRetorno ValidarRegras(this AlimentoNutriente _alimNutr,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_alimNutr.IdAlimento <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo ALIMENTO deve ser selecionado!");
            }
            else if (_alimNutr.IdNutr <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo NUTRIENTE deve ser selecionado!");
            }
            //else if (_alimNutr.Valor <= 0)
            //{
            //    return
            //        bllRetorno.GeraRetorno(false, "Campo VALOR deve ser preenchido!");
            //}
            else if ((_insersao) && (RegistroDuplicado(_alimNutr)))
            {
                return bllRetorno.GeraRetorno(false, "Este Nutriente já foi Cadastrado!!!");
            }

            if ((_alimNutr.Valor <= 0) || (_alimNutr.Valor == null))
            {
                _alimNutr.Valor = 0;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(AlimentoNutriente _alimNutr)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT  COUNT(IdAlimNutr) AS Total
                FROM    AlimentoNutrientes
                WHERE   (IdAlimento = {0}) AND (IdNutr = {1})", 
                        _alimNutr.IdAlimento, _alimNutr.IdNutr);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
