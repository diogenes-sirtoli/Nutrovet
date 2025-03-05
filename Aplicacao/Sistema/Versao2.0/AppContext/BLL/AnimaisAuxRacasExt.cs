using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAnimaisAuxRacasExt
    {
        public static bllRetorno ValidarRegras(this AnimaisAuxRaca _raca,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            bool regReativ = false;

            if (_raca.IdEspecie <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ESPÉCIE deve ser selecionado!");
            }
            else if (_raca.Raca == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo RAÇA deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_raca.IdEspecie, _raca.Raca, true)))
            {
                return bllRetorno.GeraRetorno(false, "Esta Raça já foi Cadastrada!!!");
            }

            if (_raca.IdadeAdulta <= 0)
            {
                _raca.IdadeAdulta = null;
            }
            if (_raca.CrescInicial <= 0)
            {
                _raca.CrescInicial = null;
            }
            if (_raca.CrescFinal <= 0)
            {
                _raca.CrescFinal = null;
            }

            if ((_insersao) && (RegistroDuplicado(_raca.IdEspecie, _raca.Raca, false)))
            {
                regReativ = EfetuaUpdate(_raca);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" :
                "Registro Reativado"));
        }

        private static bool RegistroDuplicado(int _idEspecie, string _raca, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;

            string _sql = string.Format(@"
                SELECT	COUNT (IdRaca) Total
                FROM	AnimaisAuxRacas
                WHERE	(IdEspecie = {0}) And (Raca = '{1}') And 
                        (Ativo = {2})", _idEspecie, _raca,
                (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        private static bool EfetuaUpdate(AnimaisAuxRaca _raca)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update AnimaisAuxRacas Set
                    Ativo = 1
                Where (IdEspecie = {0}) And (Raca = '{1}')",
                _raca.IdEspecie, _raca.Raca));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }
    }
}
