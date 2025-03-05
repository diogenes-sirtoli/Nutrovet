using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;

namespace BLL
{
    public static class clAlimentosExt
    {
        public static bllRetorno ValidarRegras(this Alimentos _alimento,
            bool _insersao)
        {
            bool regReativ = false;

            if (_alimento.IdPessoa <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo PESSOA deve ser selecionado!");
            }
            else if (_alimento.IdGrupo <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo GRUPO deve ser selecionado!");
            }
            else if (_alimento.IdCateg <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo Categoria deve ser selecionado!");
            }
            else if (_alimento.IdFonte <= 0)
            {
                return
                    bllRetorno.GeraRetorno(false, "Campo FONTE deve ser selecionado!");
            }
            else if (_alimento.Alimento == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo ALIMENTO deve ser preenchido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_alimento, true)))
            {
                return bllRetorno.GeraRetorno(false,
                    "Um Alimento com Estas Mesmas Informações, já foi Cadastrado!!!");
            }

            if (_alimento.DataHomol == DateTime.Parse("01/01/1910"))
            {
                _alimento.DataHomol = null;
            }
            if (_alimento.fHomologado == null)
            {
                _alimento.fHomologado = false;
            }
            if (_alimento.Compartilhar == null)
            {
                _alimento.Compartilhar = false;
            }

            if ((_insersao) && (RegistroDuplicado(_alimento, false)))
            {
                regReativ = EfetuaUpdate(_alimento);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
                "Registro Reativado"));
        }

        private static bool EfetuaUpdate(Alimentos _alimento)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                Update Alimentos Set
                    Ativo = 1
                Where (Alimento = '{0}') ", 
                _alimento.Alimento));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }

        private static bool RegistroDuplicado(Alimentos _alimento, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(
                @"Select    Count(IdAlimento) Total
                  From      Alimentos
                  Where     (IdFonte = {0}) And 
                            (Alimento = '{1}') And 
                            (Ativo = {2})", _alimento.IdFonte,
                _alimento.Alimento, (_ativo.Value ? 1 : 0));

            int ret = crud.ExecutarComandoTipoInteiro(_sql);

            return Funcoes.Funcoes.ConvertePara.Bool(ret);
        }
    }
}
