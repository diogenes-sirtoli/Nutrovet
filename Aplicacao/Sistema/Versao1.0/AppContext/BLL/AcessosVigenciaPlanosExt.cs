using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clAcessosVigenciaPlanosExt
    {
        public static bllRetorno ValidarRegras(this AcessosVigenciaPlano _vigPlan,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_vigPlan.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo CLIENTE deve ser selecionado!");
            }
            else if (_vigPlan.IdPlano <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo PLANO deve ser selecionado!");
            }
            else if (_vigPlan.DtInicial == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA INICIAL deve ser preenchido!");
            }
            else if (_vigPlan.DtFinal == DateTime.Parse("01/01/1910"))
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo DATA FINAL deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_vigPlan)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_vigPlan.QtdAnim <= 0)
            {
                _vigPlan.QtdAnim = null;
            }
            if (_vigPlan.Periodo <= 0)
            {
                _vigPlan.Periodo = null;
            }
            if (_vigPlan.IdCupom <= 0)
            {
                _vigPlan.IdCupom = null;
            }
            if (_vigPlan.ComprovanteDtHomolog <= DateTime.Parse("01/01/1910"))
            {
                _vigPlan.ComprovanteDtHomolog = null;
            }
            if (_vigPlan.ComprovanteHomologador <= 0)
            {
                _vigPlan.ComprovanteHomologador = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(AcessosVigenciaPlano _VIGpLAN)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SET DATEFORMAT dmy

                SELECT  COUNT(IdVigencia) AS Total
                FROM    AcessosVigenciaPlanos
                WHERE   (IdPessoa = {0}) AND (IdPlano = {1}) AND 
                        (DtInicial = '{2}') AND (DtFinal = '{3}')", 
                        _VIGpLAN.IdPessoa, _VIGpLAN.IdPlano, 
                        _VIGpLAN.DtInicial.ToString("dd/MM/yyyy"),
                        _VIGpLAN.DtFinal.ToString("dd/MM/yyyy"));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
