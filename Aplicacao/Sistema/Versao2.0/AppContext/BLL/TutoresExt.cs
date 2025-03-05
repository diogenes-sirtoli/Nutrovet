using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Text.RegularExpressions;

namespace BLL
{
    public static class clTutoresExt
    {
        public static bllRetorno ValidarRegras(this Tutore _tutor,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            bool regReativ = false;

            if (_tutor.IdCliente <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo CLIENTE deve ser selecionado!");
            }
            if (_tutor.IdTutor <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo TUTOR deve ser selecionado!");
            }
            else if ((_insersao) && (RegistroDuplicado(_tutor, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            
            return bllRetorno.GeraRetorno(true, (!regReativ ?
                "Dados Válidos!" : "Registro Reativado"));
        }
        
        private static bool RegistroDuplicado(Tutore _tutor, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (IdTutor) Total
                FROM	Tutores
                WHERE	(IdCliente = {0}) And (IdTutor = {1}) And
                        (Ativo = {2})", _tutor.IdCliente,
                _tutor.IdTutor, (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

       private static bool EfetuaUpdate(Tutore _tutor)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update Tutores Set
                    Ativo = 1
                Where (IdCliente = {0}) And (IdTutor = {1})", 
                _tutor.IdCliente, _tutor.IdTutor));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }
    }
}
