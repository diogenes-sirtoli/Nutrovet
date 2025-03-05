using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clPrescricaoAuxTiposExt
    {
        public static bllRetorno ValidarRegras(this PrescricaoAuxTipo _prescr,
            bool _insersao)
        {
            bool regReativ = false;

            if (_prescr.Prescricao == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TIPO DE PRESCRIÇÃO deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_prescr.Prescricao, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_prescr.Prescricao, false)))
            {
                regReativ = EfetuaUpdate(_prescr);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(PrescricaoAuxTipo _prescr)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update PrescricaoAuxTipos Set
                    Ativo = 1
                Where (Prescricao = '{0}') ", _prescr.Prescricao));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _prescr, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdPrescr) Total
                  From PrescricaoAuxTipos
                  Where (Prescricao = '{0}') And (Ativo = {1})", _prescr,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
