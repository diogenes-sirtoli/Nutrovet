using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clBibliotecaAuxSecoesExt
    {
        public static bllRetorno ValidarRegras(this BibliotecaAuxSecoe _sessao,
            bool _insersao)
        {
            bool regReativ = false;

            if (_sessao.Secao == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SESSÃO deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_sessao.Secao, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_sessao.Secao, false)))
            {
                regReativ = EfetuaUpdate(_sessao);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(BibliotecaAuxSecoe _sessao)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update BibliotecaAuxSecoes Set
                    Ativo = 1
                Where (Secao = '{0}') ", _sessao.Secao));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _sessao, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdSecao) Total
                  From BibliotecaAuxSecoes
                  Where (Secao = '{0}') And (Ativo = {1})",
                _sessao, (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
