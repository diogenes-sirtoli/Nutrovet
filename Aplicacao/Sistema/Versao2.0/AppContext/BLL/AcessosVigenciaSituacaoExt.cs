using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clAcessosVigenciaSituacaoExt
    {
        public static bllRetorno ValidarRegras(this AcessosVigenciaSituacao _vigSit,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_vigSit.IdVigencia <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo VIGÊNCIA deve ser selecionado!");
            }
            else if (_vigSit.IdSituacao <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo SITUAÇÃO deve ser selecionado!");
            }
            else if (_vigSit.DataSituacao == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA ATUAL deve ser preenchido!");
            }
            //else if ((RegistroDuplicado(_vigSit)) && (_insersao))
            //{
            //    return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            //}

            if (_vigSit.DataSituacao <= DateTime.Parse("01/01/1910"))
            {
                _vigSit.DataSituacao = DateTime.Today;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        //private static bool RegistroDuplicado(AcessosVigenciaSituacao _VigSit)
        //{
        //    CrudDal crud = new CrudDal();
        //    bool retorno = false;
        //    string _sql = string.Format(@"
        //        SELECT  COUNT(IdVigSit) AS Total
        //        FROM    AcessosVigenciaSituacao
        //        WHERE   (IdVigencia = {0}) AND (IdSituacao = {1})", 
        //                _VigSit.IdVigencia, _VigSit.IdSituacao);

        //    int reg = crud.ExecutarComandoTipoInteiro(_sql);

        //    retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

        //    return retorno;
        //}
    }
}
