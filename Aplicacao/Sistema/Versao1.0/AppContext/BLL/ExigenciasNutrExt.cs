using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clExigenciasNutrExt
    {
        public static bllRetorno ValidarRegras(this ExigenciasNutricionai _nutri,
            bool _insersao)
        {
            if (_nutri.IdTabNutr <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TABELA NUTRICIONAL deve ser selecionado!");
            }
            else if (_nutri.IdEspecie <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, 
                    "Campo ESPÉCIE deve ser selecionado!");
            }
            else if (_nutri.IdIndic <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo INDICAÇÃO deve ser selecionado!");
            }
            else if (_nutri.IdNutr1 <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo NUTRIENTE deve ser selecionado!");
            }
            else if (_nutri.IdTpValor <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo TIPO DE VALOR deve ser selecionado!");
            }
            else if (_nutri.IdUnidade1 <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                    "Campo UNIDADE deve ser selecionado!");
            }
            else if ((RegistroDuplicado(_nutri, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_nutri.Valor1 <= 0)
            {
                _nutri.Valor1 = null;
            }
            if (_nutri.IdNutr2 <= 0)
            {
                _nutri.IdNutr2 = null;
            }
            if (_nutri.IdUnidade2 <= 0)
            {
                _nutri.IdUnidade2 = null;
            }
            if (_nutri.Proporcao1 <= 0)
            {
                _nutri.Proporcao1 = null;
            }
            if (_nutri.Proporcao2 <= 0)
            {
                _nutri.Proporcao2 = null;
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        private static bool RegistroDuplicado(ExigenciasNutricionai nutri, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select    Count(IdExigNutr) Total
                  From	    ExigenciasNutricionais
                  Where	    (Ativo = {0}) AND (IdTabNutr = {1}) And 
		                    (IdEspecie = {2}) AND (IdIndic = {3}) AND
		                    (IdTpValor = {4}) And (IdNutr1 = {5}) AND 
                            (IdUnidade1 = {6})", (_ativo.Value ? 1 : 0),
                nutri.IdTabNutr, nutri.IdEspecie, nutri.IdIndic,
                nutri.IdTpValor, nutri.IdNutr1, nutri.IdUnidade1);

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
