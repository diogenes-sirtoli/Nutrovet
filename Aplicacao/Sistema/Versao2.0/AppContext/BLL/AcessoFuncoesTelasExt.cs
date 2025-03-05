using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DCL;

namespace BLL
{
    public static class clAcessosFuncoesTelasExt
    {
        public static bllRetorno ValidarRegras(this AcessosFuncoesTela _funcTelas,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            
            if (_funcTelas.IdAcFunc <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo FUNÇÃO deve ser selecionado!");
            }
            else if (_funcTelas.IdTela <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo TELA deve ser selecionado!");
            }
            else if ((_insersao) && (RegistroDuplicado(_funcTelas)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(AcessosFuncoesTela _funcTelas)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (*) Total
                FROM	AcessosFuncoesTelas
                WHERE	(IdAcFunc = {0}) And (IdTela = {1})",
                _funcTelas.IdFuncTela, _funcTelas.IdTela);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
