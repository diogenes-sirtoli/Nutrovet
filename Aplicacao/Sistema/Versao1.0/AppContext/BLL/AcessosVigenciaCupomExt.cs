using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clAcessosVigenciaCupomExt
    {
        public static bllRetorno ValidarRegras(this AcessosVigenciaCupomDesconto _cupom,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_cupom.NrCumpom == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NÚMERO DO CUPOM deve ser preenchido!");
            }
            else if (_cupom.IdTipoDesc <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                    "Campo TIPO DE DESCONTO deve ser informado!");
            }
            else if (_cupom.ValorDesc <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo VALOR DO DESCONTO deve ser informado!");
            }
            else if (_cupom.DtInicial == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA INICIAL deve ser preenchido!");
            }
            else if (_cupom.DtInicial == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA FINAL deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_cupom)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(AcessosVigenciaCupomDesconto _cupom)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SET DATEFORMAT dmy

                SELECT  COUNT(IdCupom) AS Total
                FROM    AcessosVigenciaCupomDesconto
                WHERE   (NrCumpom = '{0}')", _cupom.NrCumpom);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
