using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAlimentosAuxCategoriasExt
    {
        public static bllRetorno ValidarRegras(this AlimentosAuxCategorias _categoria,
            bool _insersao)
        {
            bool regReativ = false;

            if (_categoria.Categoria == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CATEGORIA DE ALIMENTOS deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_categoria.Categoria, true)))
            {
                return bllRetorno.GeraRetorno(false, "Esta Categoria já foi Cadastrada!!!");
            }

            if ((_insersao) && (RegistroDuplicado(_categoria.Categoria, false)))
            {
                regReativ = EfetuaUpdate(_categoria);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(AlimentosAuxCategorias _categoria)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AlimentosAuxCategorias Set
                    Ativo = 1
                Where (Categoria = '{0}') ", 
                _categoria.Categoria));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(string _fonte, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select Count(IdCateg) Total
                  From AlimentosAuxCategorias
                  Where (Categoria = '{0}') And 
                        (Ativo = {1})", _fonte,
                (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
