using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clCardapioAlimentosExt
    {
        public static bllRetorno ValidarRegras(this CardapiosAlimento _cardAlim,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_cardAlim.IdAlimento <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo ALIMENTO deve ser selecionado!");
            }
            else if (_cardAlim.IdCardapio <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo CARDÁPIO deve ser selecionado!");
            }
            else if (_cardAlim.Quant <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo QUANTIDADE deve ser preenchida!");
            }
            else if ((RegistroDuplicado(_cardAlim)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(CardapiosAlimento _alimNutr)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT  COUNT(IdCardapAlim) AS Total
                FROM    CardapiosAlimentos
                WHERE   (IdAlimento = {0}) AND (IdCardapio = {1})", 
                        _alimNutr.IdAlimento, _alimNutr.IdCardapio);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
