using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Text.RegularExpressions;

namespace BLL
{
    public static class clPessoasExt
    {
        public static bllRetorno ValidarRegras(this Pessoa _pessoa,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            bool regReativ = false;
            Regex rgEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            Regex rgCpf = new Regex(@"(^\d{3}\x2E\d{3}\x2E\d{3}\x2D\d{2}$)");
            Regex rgCnpj = new Regex(@"(^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$)");

            switch (Funcoes.Funcoes.ConvertePara.Int(_pessoa.dTpEntidade))
            {
                case 0:
                    {
                        return
                            bllRetorno.GeraRetorno(false,
                                "Campo PESSOA FÍSICA ou JURÍDICA deve ser selecionado!");
                    }
                case 1:
                    {
                        if ((_pessoa.CPF != "") && (_pessoa.CPF != null))
                        {
                            string _nomeCpfDuplicado = CPFDuplicado(_pessoa);

                            if (!rgCpf.IsMatch(_pessoa.CPF))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Formato de CPF Inválido!");
                            }
                            else if (!Funcoes.Funcoes.Validacoes.Cpf(_pessoa.CPF))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "CPF Inválido!");
                            }
                            else if ((_nomeCpfDuplicado != "") && (_nomeCpfDuplicado != null))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Este CPF Já Pertence a " + _nomeCpfDuplicado);
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        if ((_pessoa.CNPJ != "") && (_pessoa.CNPJ != null))
                        {
                            string _nomeCnpjDuplicado = CNPJDuplicado(_pessoa);

                            if (!rgCnpj.IsMatch(_pessoa.CNPJ))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Formato de CNPJ Inválido!");
                            }
                            else if (!Funcoes.Funcoes.Validacoes.Cnpj(_pessoa.CNPJ))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "CNPJ Inválido!");
                            }
                            else if ((_nomeCnpjDuplicado != "") && (_nomeCnpjDuplicado != null))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Este CPF Já Pertence a " + _nomeCnpjDuplicado);
                            }
                        }

                        break;
                    }
            }

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
            else if (!rgEmail.IsMatch(_pessoa.Email))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Formato de E-MAIL Inválido!");
            }
            else if ((_insersao) && (RegistroDuplicado(_pessoa, true)))
            {
                return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
            }

            if (_pessoa.DataNascimento == DateTime.Parse("01/01/1910"))
            {
                _pessoa.DataNascimento = null;
            }

            if ((_insersao) && (RegistroDuplicado(_pessoa, false)))
            {
                regReativ = EfetuaUpdate(_pessoa);
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ?
                "Dados Válidos!" : "Registro Reativado"));
        }

        public static bllRetorno ValidarRegrasTutores(this Pessoa _pessoa,
            bool _insersao)
        {
            CrudDal crud = new CrudDal();
            bool regReativ = false;
            Regex rgCpf = new Regex(@"(^\d{3}\x2E\d{3}\x2E\d{3}\x2D\d{2}$)");
            Regex rgCnpj = new Regex(@"(^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$)");

            switch (Funcoes.Funcoes.ConvertePara.Int(_pessoa.dTpEntidade))
            {
                case 0:
                    {
                        return
                            bllRetorno.GeraRetorno(false,
                                "Campo PESSOA FÍSICA ou JURÍDICA deve ser selecionado!");
                    }
                case 1:
                    {
                        if ((_insersao) && (TutorDuplicado(_pessoa)))
                        {
                            return bllRetorno.GeraRetorno(false,
                                "Este Tutor já foi Cadastrado!!!");
                        }
                        else if ((_pessoa.CPF != "") && (_pessoa.CPF != null))
                        {
                            string _nomeCpfDuplicado = CPFDuplicado(_pessoa);

                            if (!rgCpf.IsMatch(_pessoa.CPF))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Formato de CPF Inválido!");
                            }
                            else if (!Funcoes.Funcoes.Validacoes.Cpf(_pessoa.CPF))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "CPF Inválido!");
                            }
                            else if ((_nomeCpfDuplicado != "") && (_nomeCpfDuplicado != null))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Este CPF Já Pertence a " + _nomeCpfDuplicado);
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        if ((_insersao) && (TutorDuplicado(_pessoa)))
                        {
                            return bllRetorno.GeraRetorno(false,
                                "Este Tutor já foi Cadastrado!!!");
                        }
                        else if ((_pessoa.CNPJ != "") && (_pessoa.CNPJ != null))
                        {
                            string _nomeCnpjDuplicado = CNPJDuplicado(_pessoa);

                            if (!rgCnpj.IsMatch(_pessoa.CNPJ))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Formato de CNPJ Inválido!");
                            }
                            else if (!Funcoes.Funcoes.Validacoes.Cnpj(_pessoa.CNPJ))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "CNPJ Inválido!");
                            }
                            else if ((_nomeCnpjDuplicado != "") && (_nomeCnpjDuplicado != null))
                            {
                                return
                                    bllRetorno.GeraRetorno(false,
                                        "Este CPF Já Pertence a " + _nomeCnpjDuplicado);
                            }
                        }

                        break;
                    }
            }

            if (_pessoa.Nome == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo NOME deve ser preenchido!");
            }

            if (_pessoa.DataNascimento == DateTime.Parse("01/01/1910"))
            {
                _pessoa.DataNascimento = null;
            }

            return bllRetorno.GeraRetorno(true, (!regReativ ?
                "Dados Válidos!" : "Registro Reativado"));
        }

        public static bllRetorno ValidarRegrasLogon(this Pessoa _pessoa,
            bool _insersao, bool _alteraSenha)
        {
            CrudDal crud = new CrudDal();

            if (_pessoa.Usuario == "")
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo USUÁRIO deve ser preenchido!");
            }
            else if ((_pessoa.Senha == "") || (_pessoa.Senha == "OH1kJ4cpYt8="))
            {
                return
                    bllRetorno.GeraRetorno(false,
                        "Campo SENHA deve ser preenchido!");
            }
            else if ((LogonMesmoUsuario(_pessoa.IdPessoa, _pessoa.Usuario, true)) &&
                (_insersao == false) && (_alteraSenha == false))
            {
                return bllRetorno.GeraRetorno(false,
                    "Novo Usuário deve ser DIFERENTE do atual!!!!");
            }
            else if ((_insersao) && (LogonDuplicado(_pessoa.Usuario, true)))
            {
                return bllRetorno.GeraRetorno(false, "Este USUARIO já Existe!!!");
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

                if (acessosDcl == null)
                {
                    return
                    bllRetorno.GeraRetorno(false,
                        "Usuário SEM ACESSO ao Sistema! Contate o Administrador! WhatsApp (61) 98136-6230");
                }
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
                        (IdTpPessoa = 2) AND (Ativo = {2})", _pessoa.Nome,
                _pessoa.Email, (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        private static bool TutorDuplicado(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SET DATEFORMAT dmy                

                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(Nome = '{0}') And (IdOperador = {1}) And
                        (IdTpPessoa = 3)", _pessoa.Nome,
                _pessoa.IdOperador);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        private static string CPFDuplicado(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();
            string _sql = string.Format(@"
                SELECT	Nome
                FROM	Pessoas
                WHERE	(Nome <> '{0}') And (EMail <> '{1}') And
                        (CPF = '{2}')", _pessoa.Nome,
                _pessoa.Email, _pessoa.CPF);

            string reg = crud.ExecutarComandoTipoBasico<string>(_sql).FirstOrDefault();

            return reg;
        }

        private static string CNPJDuplicado(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();
            string _sql = string.Format(@"
                SELECT	Nome
                FROM	Pessoas
                WHERE	(Nome <> '{0}') And (EMail <> '{1}') And
                        (CNPJ = '{2}')", _pessoa.Nome,
                _pessoa.Email, _pessoa.CNPJ);

            string reg = crud.ExecutarComandoTipoBasico<string>(_sql).FirstOrDefault();

            return reg;
        }

        private static bool LogonDuplicado(string _user, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(Usuario = '{0}') And (Ativo = {1})", _user,
                (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        private static bool LogonMesmoUsuario(int _idPessoa, string _user, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(Usuario = '{0}') And 
                        (Ativo = {1}) And 
                        (IdPessoa = {2})", _user,
                (_ativo ? 1 : 0), _idPessoa);

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
