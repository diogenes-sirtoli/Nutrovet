using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
    public static class clPessoasExt
    {
        public static bllRetorno ValidarRegras(this Pessoa _pessoa,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            bool regReativ = false;

            if (_pessoa.Nome == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME deve ser preenchido!");
            }
            else if (_pessoa.Email == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL deve ser preenchido!");
            }
            else if ((RegistroDuplicado(_pessoa, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_pessoa.DataNascimento == DateTime.Parse("01/01/1910"))
            {
                _pessoa.DataNascimento = null;
            }
            if ((RegistroDuplicado(_pessoa, false)) && (_insersao))
            {
                regReativ = EfetuaUpdate(_pessoa);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ?
                "Dados Válidos!" : "Registro Reativado"));
        }

        public static bllRetorno ValidarRegrasLogon(this Pessoa _pessoa,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();

            if (_pessoa.Email == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL deve ser preenchido!");
            }
            else if (_pessoa.Senha == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SENHA deve ser preenchido!");
            }
            else if ((LogonDuplicado(_pessoa.Email, true)) && (_insersao))
            {
                return bllRetorno.GeraRetorno(false, "Este E-Mail já Existe!!!");
            }

            return bllRetorno.GeraRetorno(true, "Dados Válidos!");
        }

        public static bllRetorno ValidarLogon(this Pessoa _logon)
        {
            CrudDal crud = new CrudDal();
            clAcessosBll acessosBll = new clAcessosBll();
            clPessoasBll logonBll = new clPessoasBll();
            Acesso acessosDcl = new Acesso();
            clAcessosVigenciaPlanosBll vigenciaPlanosBll = new clAcessosVigenciaPlanosBll();

            Pessoa logonDcl = logonBll.CarregarLogon(_logon.Email);

            if (logonDcl != null)
            {
                acessosDcl = acessosBll.Carregar(logonDcl.IdPessoa);
            }

            if (_logon.Email == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo E-MAIL deve ser preenchido!");
            }
            else if (_logon.Senha == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SENHA deve ser preenchido!");
            }
            else if (!logonBll.VerificaConexao().retorno)
            {
                return bllRetorno.GeraRetorno(false, logonBll.VerificaConexao().mensagem);
            }
            else if (!logonBll.ExisteUsuario(_logon.Email).retorno)
            {
                return bllRetorno.GeraRetorno(false, "Usuário Inexistente!");
            }
            else if (!logonBll.Autenticado(_logon.Email, _logon.Senha).retorno)
            {
                return bllRetorno.GeraRetorno(false, "Usuário ou Senha Inválidos!");
            }
            else if (logonBll.IsBloqueado(_logon.Email).retorno)
            {
                return bllRetorno.GeraRetorno(false, "Usuário Bloqueado!");
            }
            else if (logonDcl != null)
            {
                if ((!vigenciaPlanosBll.PlanoEstaNaVigencia(logonDcl.IdPessoa)) &&
                    (!Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.SuperUser)))
                {
                    return bllRetorno.GeraRetorno(false, 
                        "Plano Fora da Vigência! Contate o Administrador! WhatsApp (61) 98136-6230");
                }
            }

            return bllRetorno.GeraRetorno(true, "Logon Válido!");
        }

        private static bool RegistroDuplicado(Pessoa _pessoa, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SET DATEFORMAT dmy                

                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(Nome = '{0}') And (EMail = '{1}') And
                        (Ativo = {2})", _pessoa.Nome,
                _pessoa.Email, (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        private static bool LogonDuplicado(string _email, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(Email = '{0}') And (Ativo = {1})", _email,
                (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        private static bool EfetuaUpdate(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();

            int _upd = crud.ExecutarComando(string.Format(@"
                SET DATEFORMAT dmy

                Update Pessoas Set
                    Ativo = 1
                Where (Nome = '{0}') And (Email = '{1}')", 
                _pessoa.Nome, _pessoa.Email));

            return Funcoes.Funcoes.ConvertePara.Bool(_upd);
        }
    }
}
