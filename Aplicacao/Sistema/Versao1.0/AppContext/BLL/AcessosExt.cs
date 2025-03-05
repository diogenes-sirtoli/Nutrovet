using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clAcessosExt
    {
        public static bllRetorno ValidarRegras(this Acesso _acessos,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_acessos.IdAcFunc <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo FUNÇÕES deve ser selecionado!");
            }
            else if ((RegistroDuplicado(_acessos)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(Acesso _acessos)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (*) Total
                FROM	Acessos
                WHERE	(IdPessoa = {0})", _acessos.IdPessoa);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
