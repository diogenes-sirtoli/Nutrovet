using System;
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
            else if (_cupom.dPlanoTp <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo TIPO DE PLANO deve ser selecionado!");
            }
            else if (_cupom.DtInicial == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA INICIAL deve ser preenchido!");
            }
            else if (_cupom.DtFinal == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA FINAL deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_cupom)))
            {
                return bllRetorno.GeraRetorno(false, "Este Cupom já foi Cadastrado!!!");
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
