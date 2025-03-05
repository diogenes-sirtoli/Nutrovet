using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAcessoAuxTelasExt
    {
        public static bllRetorno ValidarRegras(this AcessosAuxTela _tela,
            bool _insersao)
        {
            bool regReativ = false;

            if (_tela.Telas == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TELA deve ser preenchido!");
            }
            else if (_tela.CodTela == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CÓDIGO DA TELA deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_tela.Telas, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_tela.Telas, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_tela);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AcessosAuxTela _tela)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AcessosAuxTelas Set
                    Ativo = 1
                Where (Telas = '{0}') ", _tela.Telas));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _tela, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdTela) Total
                  From AcessosAuxTelas
                  Where (Telas = '{0}') And (Ativo = {1})", 
                _tela, (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
