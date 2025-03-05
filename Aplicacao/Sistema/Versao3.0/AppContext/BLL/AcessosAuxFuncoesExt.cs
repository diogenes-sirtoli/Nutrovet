using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAcessoAuxFuncoesExt
    {
        public static bllRetorno ValidarRegras(this AcessosAuxFuncoe _funcao,
            bool _insersao)
        {
            bool regReativ = false;

            if (_funcao.Funcao == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo FUNÇÃO deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_funcao.Funcao, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_funcao.Funcao, false)))
            {
                regReativ = EfetuaUpdate(_funcao);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AcessosAuxFuncoe funcao)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AcessosAuxFuncoes Set
                    Ativo = 1
                Where (Funcao = '{0}') ", funcao.Funcao));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _funcao, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdAcFunc) Total
                  From AcessosAuxFuncoes
                  Where (Funcao = '{0}') And (Ativo = {1})", 
                _funcao,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
