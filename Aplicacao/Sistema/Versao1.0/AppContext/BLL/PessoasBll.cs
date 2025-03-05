using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        CacheExcluir();
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
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a INSERÇÃO!!! ");
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
                    CacheExcluir();

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                catch (Exception err)
                {
                    return bllRetorno.GeraRetorno(false,
                        "Não foi possível efetuar a ALTERAÇÃO!!! ");
                }
            }
            else
            {
                return ret;
            }
        }

        public bllRetorno AlterarLogon(Pessoa _user)
        {
            bllRetorno ret = _user.ValidarRegrasLogon(false);

            if (ret.retorno)
            {
                CrudDal crud = new CrudDal();

                try
                {
                    crud.Alterar(_user);
                    CacheExcluir();

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

        public bllRetorno Excluir(Pessoa _pessoa)
        {
            CrudDal crud = new CrudDal();

            try
            {
                crud.Excluir(_pessoa);
                CacheExcluir();

                return bllRetorno.GeraRetorno(true,
                    "EXCLUSÃO efetuada com sucesso!!!");
            }
            catch (Exception err)
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a EXCLUSÃO!!!");
            }
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

        public Pessoa Carregar(string _nome, int _idCliente)
        {
            CrudDal crud = new CrudDal();
            Pessoa _retorno = new Pessoa();

            string _sql = string.Format(
                @"SET DATEFORMAT dmy

                  Select *
                  From Pessoas
                  Where (Nome = '{0}') And (Idcliente = {1})", 
                _nome, _idCliente);

            var ret = crud.ExecutarComando<Pessoa>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public Pessoa CarregarLogon(string _email)
        {
            CrudDal crud = new CrudDal();
            Pessoa userDcl = new Pessoa();
            
            string _sql = string.Format(@"
                SELECT	*
                FROM	Pessoas
                WHERE	(EMail = '{0}') And (Ativo = 1)", _email);

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

        public TOPessoasBll CarregarLogon(string _nome, string _email)
        {
            CrudDal crud = new CrudDal();
            TOPessoasBll userTO = new TOPessoasBll();

            string _sql = string.Format(@"
                SET DATEFORMAT dmy
                
                Select	IdPessoa, Nome, DataNascimento, Email, Usuario, Senha, Bloqueado,
		                Ativo, IdOperador, IP, DataCadastro
                From	Pessoas
                Where	(Ativo = 1) And (Nome = '{0}') AND 
		                (Email = '{1}') ", _nome, _email);

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

        public string CarregarImagem(int idPessoa)
        {
            HttpServerUtility _server = HttpContext.Current.Server;
            string path = _server.MapPath("~/Perfil/Fotos/");
            DirectoryInfo _directorio = new DirectoryInfo(path);

            string search = (from d in _directorio.GetFiles("Cliente_" + idPessoa + ".*")
                             orderby d.LastAccessTime descending
                             select d.Name).FirstOrDefault();
            if (search is null)
            {
                search = "user_c.png";
            }

            return "~/Perfil/Fotos/" + search + "?time=" + DateTime.Now.ToString();
        }

        public List<TOPessoasBll> Listar(bool? _ativo)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	p.IdPessoa, p.Nome, p.IdTpPessoa, 
		                (Case p.IdTpPessoa
			                When 1 Then 'Administrador do Sistema'
			                When 2 Then 'Cliente'
			                When 3 Then 'Tutor'
		                 End) AS TipoPessoa, 
		                p.DataNascimento, p.RG, p.CPF, p.CRVM, p.Telefone, p.Celular, 
		                p.Logotipo, p.IdCliente, 
		                (Select pc.Nome
		                 From	Pessoas pc
		                 Where	pc.IdPessoa = p.IdCliente) AS Cliente, p.Email, p.Usuario, 
		                p.Senha, p.Bloqueado, p.Ativo, p.IdOperador, p.IP, p.DataCadastro
                FROM	Pessoas p ";

            if (_ativo != null)
            {
                _sql += string.Format(@"
                WHERE   (p.Ativo = {0})", (_ativo.Value ? 1 : 0));
            }

            _sql += @"
                ORDER BY P.Nome";

            var lista = crud.Listar<TOPessoasBll>(_sql);

            return lista.ToList();
        }

        public List<TOPessoasBll> CachePessoas()
        {
            System.Web.Caching.Cache _Cache;
            _Cache = System.Web.HttpContext.Current.Cache;
            List<TOPessoasBll> CacheFracao = (List<TOPessoasBll>)_Cache["CachePessoas"];

            if (CacheFracao == null)
            {
                _Cache.Insert("CachePessoas", Listar(true), null, DateTime.Now.AddHours(2),
                    TimeSpan.Zero);
                CacheFracao = (List<TOPessoasBll>)_Cache["CachePessoas"];
            }

            return CacheFracao;
        }

        public void CacheExcluir()
        {
            System.Web.Caching.Cache _Cache;
            _Cache = System.Web.HttpContext.Current.Cache;
            
           _Cache.Remove("CachePessoas");
        }

        public bllRetorno ExisteUsuario(string _email)
        {
            CrudDal crud = new CrudDal();
            int ret = 0;

            string _sql = string.Format(@"
                SELECT	COUNT (IdPessoa) Id
                FROM	Pessoas
                WHERE	(Email = '{0}') And (Ativo = 1)", _email);

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

        public bllRetorno IsBloqueado(string _email)
        {
            CrudDal crud = new CrudDal();

            string _sql = string.Format(@"
                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(Email = '{0}') And (Ativo = 1) AND
                        (Bloqueado = 1)", _email);

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

        public List<TOPessoasBll> Listar(string _pesqNome, int _idCliente,
            int _tamPag, int _pagAtual)
        {
            IEnumerable<TOPessoasBll> _listagem;

            if (_pesqNome != "")
            {
                if (_idCliente > 0)
                {
                    _listagem = (from l in CachePessoas()
                                 where (l.IdCliente == _idCliente) &&
                                       (l.Nome.ToUpper().Contains(_pesqNome.ToUpper()))
                                 orderby l.Nome
                                 select l).
                                    Skip((_pagAtual - 1) * _tamPag).
                                    Take(_tamPag);
                }
                else
                {
                    _listagem = (from l in CachePessoas()
                                 where (l.Nome.ToUpper().Contains(_pesqNome.ToUpper()))
                                 orderby l.Nome
                                 select l).
                                    Skip((_pagAtual - 1) * _tamPag).
                                    Take(_tamPag);
                }
            }
            else
            {
                if (_idCliente > 0)
                {
                    _listagem = (from l in CachePessoas()
                                 where (l.IdCliente == _idCliente)
                                 orderby l.Nome
                                 select l).
                                    Skip((_pagAtual - 1) * _tamPag).
                                    Take(_tamPag);
                }
                else
                {
                    _listagem = (from l in CachePessoas()
                                 orderby l.Nome
                                 select l).
                                    Skip((_pagAtual - 1) * _tamPag).
                                    Take(_tamPag);
                }
            }

            return _listagem.ToList();
        }

        public int TotalPaginas(string _pesqNome, int _idCliente, int _tamPag)
        {
            int _total = 0;

            if (_pesqNome != "")
            {
                if (_idCliente > 0)
                {
                    _total = CachePessoas().
                                Where(l => (l.IdCliente == _idCliente) &&
                                           (l.Nome.ToUpper().Contains(
                                               _pesqNome.ToUpper()))).Count();
                }
                else
                {
                    _total = CachePessoas().
                                 Where (l => (l.Nome.ToUpper().Contains(
                                     _pesqNome.ToUpper()))).Count();
                }
            }
            else
            {
                if (_idCliente > 0)
                {
                    _total = CachePessoas().
                                 Where(l => (l.IdCliente == _idCliente)).Count();
                }
                else
                {
                    _total = CachePessoas().Count();
                }
            }

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }

        public int TotalPaginas(string _pesqNome, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SELECT	Count(IdAcFunc) Total
                FROM	AcessosAuxFuncoes
                WHERE   (Ativo = 1)";

            if (_pesqNome != "")
            {
                _sql += string.Format(@" AND 
                         (Funcao LIKE '%{0}%')", _pesqNome);
            }

            int _total = crud.ExecutarComandoTipoInteiro(_sql);

            decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

            return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
        }

        public ListItem[] ListarTipoPessoa()
        {
            ListItem[] und = Funcoes.Funcoes.GetEnumList<DominiosBll.PessoasAuxTipos>();

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

                SELECT	IdPessoa, IdTpPessoa, TipoPessoa, Nome, IdCliente, 
		                Email, Senha, Bloqueado, Ativo, NrCupom, NewUser, PlanoVencido
                FROM    (
			                SELECT	ROW_NUMBER() OVER(ORDER BY Tbv.Nome) AS NUMBER,
					                Tbv.IdPessoa, Tbv.IdTpPessoa, Tbv.TipoPessoa, Tbv.Nome,
					                Tbv.IdCliente, Tbv.Email, Tbv.Senha, Tbv.Bloqueado, 
					                Tbv.Ativo, CAST(Tbv.NrCupom AS Int) NrCupom, 
                                    CAST(Tbv.NewUser AS Bit) NewUser,
                                    CAST(Tbv.PlanoVencido AS Bit) PlanoVencido
			                FROM
				                (SELECT DISTINCT p.IdPessoa, p.IdTpPessoa, 
	                                (CASE IdTpPessoa 
		                                WHEN 1 THEN 'Administrador do Sistema' 
		                                WHEN 2 THEN 'Cliente' 
		                                WHEN 3 THEN 'Tutor' 
	                                END) AS TipoPessoa, p.Nome, p.IdCliente, p.Email, 
	                                p.Senha, p.Bloqueado, p.Ativo,
	                                (Select	cupom.NrCumpom
		                                From	AcessosVigenciaCupomDesconto cupom
		                                Where	(cupom.IdCupom = vp.IdCupom)) NrCupom,
	                                (SELECT
			                                (Case Total
				                                When 0 Then '1' Else '0'
				                                End)
		                                From (
			                                Select Count(IdPessoa) Total
			                                From Acessos
			                                Where (IdPessoa = p.IdPessoa)
			                                ) As Tbt
	                                ) NewUser,
	                                (SELECT
			                                (Case Total
				                                When 0 Then '1' Else '0'
				                                End) Total
		                                From (
				                                Select Count(IdVigencia) Total
				                                From AcessosVigenciaPlanos
				                                Where (Ativo = 1) And (IdPessoa = p.IdPessoa) And
						                                (GETDATE() Between DtInicial And DtFinal)
			                                ) As Tbt
	                                ) As PlanoVencido
                                FROM	Pessoas AS p INNER JOIN
		                                AcessosVigenciaPlanos AS vp ON p.IdPessoa = vp.IdPessoa 
                                WHERE  (p.IdTpPessoa = 2) AND (p.IdCliente is not Null) ", 
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
                ORDER BY    Nome";

            var lista = crud.Listar<TOPessoasBll>(_sql);

            return lista.ToList();
        }

        public int TotalPaginasClientes(string _pesqNome, bool? _ativo, int _tamPag)
        {
            CrudDal crud = new CrudDal();

            string _sql = @"
                SET DATEFORMAT dmy

                SELECT	COUNT(Tbv.IdPessoa) Total
                FROM
	                (SELECT	DISTINCT p.IdPessoa, p.IdTpPessoa, 
			                p.Nome, p.IdCliente, p.Email, 
			                p.Senha, p.Bloqueado, p.Ativo
		                FROM	Pessoas AS p INNER JOIN
				                AcessosVigenciaPlanos AS vp ON p.IdPessoa = 
					                vp.IdPessoa INNER JOIN
				                AcessosVigenciaSituacao AS vs ON vp.IdVigencia = 
					                vs.IdVigencia 
		                WHERE   (p.IdTpPessoa = 2) AND (p.IdCliente is not Null) AND
			                    (GETDATE() BETWEEN vp.DtInicial AND vp.DtFinal)";

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

        public string RetornaTipoPessoa(int _idTpPes)
        {
            string _ret = "";

            switch (_idTpPes)
            {
                case 1:
                    {
                        _ret = "Administrador do Sistema";

                        break;
                    }
                case 2:
                    {
                        _ret = "Cliente";

                        break;
                    }
                case 3:
                    {
                        _ret = "Tutor";

                        break;
                    }
            }

            return _ret;
        }

        public bool IsClient(int _idPessoa, bool _ativo)
        {
            CrudDal crud = new CrudDal();
            bool retorno = false;
            string _sql = string.Format(@"
                SELECT	COUNT (IdPessoa) Total
                FROM	Pessoas
                WHERE	(IdPessoa = {0}) AND (IdTpPessoa = 2) And
                        (Ativo = {1})", _idPessoa,
                (_ativo ? 1 : 0));

            int reg = crud.ExecutarComandoTipoInteiro(_sql);

            retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

            return retorno;
        }

        public bool DesbloquearNovoAssinante(int _idPessoa)
        {
            bool _ret = false;

            string teste = @"
                SET DATEFORMAT dmy

                SELECT	ROW_NUMBER() OVER(ORDER BY p.Nome) AS NUMBER,
                        p.IdPessoa, p.IdTpPessoa, 
		                (CASE IdTpPessoa 
			                WHEN 1 THEN 'Administrador do Sistema' 
			                WHEN 2 THEN 'Cliente' 
			                WHEN 3 THEN 'Tutor' 
		                END) AS TipoPessoa, p.Nome, p.IdCliente, p.Email, 
                        p.Senha, p.Bloqueado, p.Ativo,
		                (SELECT
							(Case Total
								When 0 Then '1' Else '0'
							    End)
						 From (
								Select Count(IdPessoa) Total
								From Acessos
								Where (IdPessoa = p.IdPessoa)
							    ) As Tbt
						) NewUser
                FROM	Pessoas AS p INNER JOIN
			                AcessosVigenciaPlanos AS vp ON p.IdPessoa = 
				                vp.IdPessoa INNER JOIN
			                AcessosVigenciaSituacao AS vs ON vp.IdVigencia = 
				                vs.IdVigencia 
                WHERE   (p.IdTpPessoa = 2) AND 
		                (GETDATE() BETWEEN vp.DtInicial AND vp.DtFinal) AND 
		                (vs.IdSituacao = 3)";

            return _ret;
        }
    }
}
