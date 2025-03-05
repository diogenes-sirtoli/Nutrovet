using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

namespace BLL
{
    public class clPessoasBll
    {
        public bllRetorno Inserir(Pessoa _pessoa)
        {
            bllRetorno ret = _pessoa.ValidarRegras(true);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        crud.Inserir(_pessoa);
                    }

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception err)
            {
                var erro = err;
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!! ");
            }
        }

        public bllRetorno InserirTutor(Pessoa _pessoa)
        {
            bllRetorno ret = _pessoa.ValidarRegrasTutores(true);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Inserir(_pessoa);

                    return bllRetorno.GeraRetorno(true,
                        "INSERÇÃO efetuada com sucesso!!!");
                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a INSERÇÃO!!! ");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno Alterar(Pessoa _pessoa)
        {
            bllRetorno ret = _pessoa.ValidarRegras(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_pessoa);

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a ALTERAÇÃO!!! ");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno AlterarTutor(Pessoa _pessoa)
        {
            bllRetorno ret = _pessoa.ValidarRegrasTutores(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_pessoa);

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch (Exception err)
                {
                    var erro = err;
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a ALTERAÇÃO!!! ");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno AlterarAssinante(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Alterar(_pessoa);

                return bllRetorno.GeraRetorno(true,
                    "ALTERAÇÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a ALTERAÇÃO!!! ");
            }
        }

        public bllRetorno AlterarLogon(Pessoa _user, bool _alteraSenha)
        {
            bllRetorno ret = _user.ValidarRegrasLogon(false, _alteraSenha);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_user);

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch (Exception err)
                {
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a ALTERAÇÃO!!! " +
                        "Erro: " + err.Message.Substring(30));
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno ExcluirFisico(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_pessoa);

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;

                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
        }

        public bllRetorno ExcluirLogico(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();
            bllRetorno msg = new bllRetorno();

            try
            {
                crud.Excluir(_pessoa);

                msg = bllRetorno.GeraRetorno(true,
                          "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                var erro = err;
                msg = Alterar(_pessoa);

                msg.mensagem = msg.mensagem.Replace("ALTERAÇÃO", "EXCLUSÃO");
            }

            return msg;
        }

        public Pessoa Carregar(int _id)
        {
            CrudDal crud = new CrudDal();
            Pessoa _retorno = new Pessoa();

            string _sql = string.Format(
                @"Select *
				  From Pessoas
				  Where (IdPessoa = {0})", _id);

            var ret = crud.ExecutarComando<Pessoa>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public TOPessoasBll CarregarTO(int _idPessoa, int _idTpPessoa)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				execute PessoasCarregarTO 
					@pesquisaNome = '', 
					@pesquisaEmail = '', 
					@idPessoa = {0}, 
					@idTutor = 0, 
					@idTpPessoa = {1}", _idPessoa,
                    _idTpPessoa);

            TOPessoasBll pessoaRetorno = crud.ExecutarComando<TOPessoasBll>(
                _sql).FirstOrDefault();

            return pessoaRetorno;
        }

        public Pessoa CarregarLogon(string _user)
        {
            CrudDal crud = new CrudDal();
            Pessoa userDcl = new Pessoa();

            string _sql = string.Format(@"
				SELECT	*
				FROM	Pessoas
				WHERE	(Usuario = '{0}') And (Ativo = 1)", _user);

            var reg = crud.ExecutarComando<Pessoa>(_sql);

            foreach (var item in reg)
            {
                userDcl = item;
            }

            if (userDcl.IdPessoa > 0)
            {
                return userDcl;
            }
            else
            {
                return userDcl = null;
            }
        }

        public TOPessoasBll CarregarLogon(string _nome, string _user)
        {
            CrudDal crud = new CrudDal();
            TOPessoasBll userTO = new TOPessoasBll();

            string _sql = string.Format(@"
				SET DATEFORMAT dmy
				
				Select	IdPessoa, Nome as cNome, DataNascimento as cDtNascim, Email as cEMail,
						Usuario as cUsuario, Senha as cSenha, Bloqueado as cBloqueado,
						Ativo as cAtivo, IdOperador as cIdOperador, IP as cIP, 
						DataCadastro as cDataCadastro
				From	Pessoas
				Where	(Ativo = 1) And (Nome = '{0}') AND 
						(Usuario = '{1}') ", _nome, _user);

            var reg = crud.ExecutarComando<TOPessoasBll>(_sql);

            foreach (var item in reg)
            {
                userTO = item;
            }

            if (userTO.IdPessoa > 0)
            {
                return userTO;
            }
            else
            {
                return userTO = null;
            }
        }

        public TOPessoasBll CarregarNomeOuEMail(string _nome, string _email)
        {
            CrudDal crud = new CrudDal();
            TOPessoasBll userTO = new TOPessoasBll();

            string _sql = string.Format(@"
				Select	IdPessoa, Nome as cNome, DataNascimento as cDtNascim, Email as cEMail,
						IdTpPessoa as cIdTpPessoa, Usuario as cUsuario, Senha as cSenha, 
						Bloqueado as cBloqueado, Ativo as cAtivo, IdOperador as cIdOperador, 
						IP as cIP, DataCadastro as cDataCadastro
				From	Pessoas
				Where	(Ativo = 1) And (Bloqueado is not null) AND 
						((Nome = '{0}') OR (Email = '{1}')) AND 
						(IdTpPessoa in (1, 2))", _nome, _email);

            TOPessoasBll reg = crud.ExecutarComando<TOPessoasBll>(_sql).FirstOrDefault();

            return reg;
        }

        public TOPessoasBll CarregarNomeOuEMail(string _nome, string _email,
            DominiosBll.PessoasAuxTipos _tpPessoa)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				execute PessoasCarregarTO 
					@pesquisaNome ='{0}', 
					@pesquisaEmail = '{1}', 
					@idPessoa = 0, 
					@idTutor = 0, 
					@idTpPessoa = {2}",
                _nome, _email, (int)_tpPessoa);

            TOPessoasBll pessoaRetorno = crud.ExecutarComando<TOPessoasBll>(
                _sql).FirstOrDefault();

            return pessoaRetorno;
        }

        public string CarregarImagem(int idPessoa)
        {
            HttpServerUtility _server = HttpContext.Current.Server;
            string path = _server.MapPath("~/Perfil/Fotos/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Cliente_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();

            if ((search is null) || (search == ""))
            {
                search = "user_c.png";
            }

            return "~/Perfil/Fotos/" + search + "?time=" + DateTime.Now.ToString();
        }

        public List<TOPessoasBll> Listar()
        {
            CrudDal crud = new CrudDal();

            string _sql = "Execute ListaGeralPessoas";

            var lista = crud.Listar<TOPessoasBll>(_sql);

            return lista.ToList();
        }

        public bllRetorno ExisteUsuario(string _user)
        {
            CrudDal crud = new CrudDal();
            int ret = 0;

            string _sql = string.Format(@"
				SELECT	COUNT (IdPessoa) Id
				FROM	Pessoas
				WHERE	(Usuario = '{0}') And (Ativo = 1)", _user);

            ret = crud.ExecutarComandoTipoInteiro(_sql);

            if (ret > 0)
            {
                return bllRetorno.GeraRetorno(true, "Usuário Existente!!!");
            }
            else
            {
                return bllRetorno.GeraRetorno(false, "Usuário Inexistente!!!");
            }
        }

        public bllRetorno IsBloqueado(string _user)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				SELECT	COUNT (IdPessoa) Total
				FROM	Pessoas
				WHERE	(Usuario = '{0}') And (Ativo = 1) AND
						(Bloqueado = 1)", _user);

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            if (reg > 0)
            {
                return bllRetorno.GeraRetorno(true, "Usuário Bloqueado!!!");
            }
            else
            {
                return bllRetorno.GeraRetorno(false, "Acesso Permitido!!!");
            }
        }

        public bllRetorno Bloquear(string _user)
        {
            string sql = "";
            CrudDal crud = new CrudDal();
            Pessoa logon = CarregarLogon(_user);

            sql = string.Format(@"
				Update Pessoas Set 
					Bloqueado = 1 
				Where IdPessoa = {0}", logon.IdPessoa);

            bool retorno = Funcoes.Funcoes.ConvertePara.Bool(
                crud.ExecutarComando<Pessoa>(sql));

            if (retorno)
            {
                return bllRetorno.GeraRetorno(true,
                    "Usuário bloqueado!!! Entre em contato com o Suporte.");
            }
            else
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar o BLOQUEIO!!!");
            }
        }

        public bllRetorno Autenticado(string _email, string _pass)
        {
            CrudDal crud = new CrudDal();
            bool _retLogon = false;
            Pessoa _logon = CarregarLogon(_email);

            string _decripPass = Funcoes.Funcoes.Seguranca.Descriptografar(
                _logon.Senha);

            _retLogon = (_pass == _decripPass ? true : false);

            if (_retLogon)
            {
                return bllRetorno.GeraRetorno(true,
                    "Usuário possui Credenciais!!!");
            }
            else
            {
                return bllRetorno.GeraRetorno(false,
                    "Usuário ou Senha Inválidos!!!");
            }
        }

        public bllRetorno VerificaConexao()
        {
            CrudDal crud = new CrudDal();
            string _retLogon = crud.VerificaConexao();

            if (_retLogon == "sim")
            {
                return bllRetorno.GeraRetorno(true,
                    "Conexão Estabelecida com Sucesso!!!");
            }
            else
            {
                return bllRetorno.GeraRetorno(false, _retLogon);
            }
        }

        public ListItem[] ListarTipoPessoa()
        {
            ListItem[] und = Funcoes.Funcoes.GetEnumList<DominiosBll.PessoasAuxTipos>();

            return und;
        }

        public ListItem[] ListarTipoEntidade()
        {
            ListItem[] und = Funcoes.Funcoes.GetEnumList<DominiosBll.PessoasEntidadesAuxTipos>();

            return und;
        }

        public List<TOPessoasBll> ListarClientes(string _pesqNome, int _tamPag,
            int _pagAtual, bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
				SET DATEFORMAT dmy

				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}

				SELECT	IdPessoa, cdTpEntidade, cTipoEntidade, cIdTpPessoa, 
						cTipoPessoa, cNome, cEmail, cSenha, cBloqueado, 
						cAtivo
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY Tbv.Nome) AS NUMBER,
									Tbv.IdPessoa, Tbv.dTpEntidade AS cdTpEntidade, 
									Tbv.TipoEntidade AS cTipoEntidade, 
									Tbv.IdTpPessoa AS cIdTpPessoa, 
									Tbv.TipoPessoa AS cTipoPessoa, Tbv.Nome  AS cNome,
									Tbv.Email AS cEMail, Tbv.Senha AS cSenha, 
									Tbv.Bloqueado AS cBloqueado, Tbv.Ativo AS cAtivo
							FROM
								(SELECT p.IdPessoa, p.dTpEntidade, 
										(CASE p.dTpEntidade 
											WHEN 1 THEN 'Pessoa Física' 
											WHEN 2 THEN 'Pessoa Jurídica' 
										 END) AS TipoEntidade, p.IdTpPessoa, 
										(CASE IdTpPessoa 
											WHEN 1 THEN 'Administrador do Sistema' 
											WHEN 2 THEN 'Cliente' 
											WHEN 3 THEN 'Tutor' 
										 END) AS TipoPessoa, p.Nome, p.Email, 
										p.Senha, p.Bloqueado, p.Ativo
								 FROM	Pessoas p
								 WHERE  (p.IdTpPessoa = 2) ",
                _tamPag, _pagAtual);

            if (_ativo != null)
            {
                _sql += string.Format(@"AND 
										(p.Ativo = {0})", (_ativo.Value ? 1 : 0));
            }

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
										((p.Nome Like '%{0}%'  COLLATE Latin1_General_CI_AI) OR
										 (p.Email Like '%{0}%'  COLLATE Latin1_General_CI_AI))",
                       _pesqNome);
            }

            _sql += @"          ) AS Tbv
						) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)
				ORDER BY    cNome";

            var lista = crud.Listar<TOPessoasBll>(_sql);

            return lista.ToList();
        }

        public int TotalPaginasClientes(string _pesqNome, bool? _ativo, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
				SELECT	COUNT(Tbv.IdPessoa) Total
				FROM
					(SELECT p.IdPessoa
					 FROM	Pessoas p
					 WHERE  (p.IdTpPessoa = 2)";

            if (_ativo != null)
            {
                _sql += string.Format(@" AND 
							(p.Ativo = {0})", (_ativo.Value ? 1 : 0));
            }

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
							((p.Nome Like '%{0}%'  COLLATE Latin1_General_CI_AI) OR
							 (p.Email Like '%{0}%'  COLLATE Latin1_General_CI_AI))",
                        _pesqNome);
            }

            _sql += @"
					) AS Tbv";

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }

        public bool IsClient(int _idPessoa, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
				SELECT	COUNT (IdPessoa) Total
				FROM	Pessoas
				WHERE	(IdPessoa = {0}) AND (IdTpPessoa in (1, 2)) And
						(Ativo = {1})", _idPessoa,
                (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }
    }
}
