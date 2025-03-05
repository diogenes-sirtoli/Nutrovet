using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAnimaisAuxEspeciesExt
    {
        public static bllRetorno ValidarRegras(this AnimaisAuxEspecy _especie,
            bool _insersao)
        {
            bool regReativ = false;

            if (_especie.Especie == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ESPÉCIE deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_especie.Especie, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if ((RegistroDuplicado(_especie.Especie, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_especie);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AnimaisAuxEspecy _especie)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AnimaisAuxEspecies Set
                    Ativo = 1
                Where (Especie = '{0}') ", _especie.Especie));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _tela, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdEspecie) Total
                  From AnimaisAuxEspecies
                  Where (Especie = '{0}') And (Ativo = {1})", _tela,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
