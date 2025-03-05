using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using System.Web;

namespace BLL
{
	public class clTutoresBll
	{
		protected clPessoasBll pessoasBll = new clPessoasBll();

		public bllRetorno Inserir(Tutore _tutor)
		{
			bllRetorno ret = _tutor.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_tutor);
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

		public bllRetorno Alterar(Tutore _tutor)
		{
			bllRetorno ret = _tutor.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_tutor);

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

		public bllRetorno Excluir(Tutore _tutor)
		{
			CrudDal crud = new CrudDal();
			bllRetorno retorno = new bllRetorno();

			string _sql = string.Format(
				@"DELETE FROM Tutores
				  WHERE	(IdTutores = {0})", _tutor.IdTutores);

			try
			{
				crud.ExecutarComando<Tutore>(_sql);

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

		public Tutore Carregar(int _id)
		{
			CrudDal crud = new CrudDal();
			Tutore _retorno = new Tutore();

			string _sql = string.Format(
				@"SELECT	IdCliente, IdTutores, IdTutor, Ativo, 
							IdOperador, IP, DataCadastro
					FROM	Tutores
					WHERE	(IdTutores = {0})", _id);

			var ret = crud.ExecutarComando<Tutore>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public Tutore Carregar(int _idCliente, int _idTutor)
		{
			CrudDal crud = new CrudDal();
			Tutore _retorno = new Tutore();

			string _sql = string.Format(
				@"SELECT	IdCliente, IdTutores, IdTutor, Ativo, 
							IdOperador, IP, DataCadastro
					FROM	Tutores
					WHERE	(IdCliente = {0}) And (IdTutor = {1})",
				_idCliente, _idTutor);

			var ret = crud.ExecutarComando<Tutore>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public TOTutoresBll CarregarTO(int _id)
		{
			CrudDal crud = new CrudDal();
			TOTutoresBll _retorno = new TOTutoresBll();

			string _sql = string.Format(
				@"  SET DATEFORMAT dmy

					SELECT	Tutores.IdCliente, c.Nome AS Cliente, c.Email AS cEmail, 
							Tutores.IdTutores, Tutores.IdTutor, t.dTpEntidade, 
							(CASE t.dTpEntidade 
								WHEN 1 THEN 'Pessoa Física' 
								WHEN 2 THEN 'Pessoa Jurídica' 
							 END) AS TipoEntidade,
							t.Nome AS Tutor, t.DataNascimento, t.RG, t.CPF, t.CNPJ, 
							t.Passaporte, t.DocumentosOutros,
							t.Email AS tEmail, t.Telefone, t.Celular, Tutores.Ativo, 
							Tutores.IdOperador, Tutores.IP, Tutores.DataCadastro
					FROM	Tutores INNER JOIN
								Pessoas AS c ON Tutores.IdCliente = c.IdPessoa INNER JOIN
								Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
					WHERE	(Tutores.Ativo = 1) AND (Tutores.IdTutores = {0})", _id);

			var ret = crud.ExecutarComando<TOTutoresBll>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

        public TOTutoresBll CarregarTOPorTutor(int _idTutor)
        {
            CrudDal crud = new CrudDal();
            TOTutoresBll _retorno = new TOTutoresBll();

            string _sql = string.Format(
                @"  SET DATEFORMAT dmy

					SELECT	Tutores.IdCliente, c.Nome AS Cliente, c.Email AS cEmail, 
							Tutores.IdTutores, Tutores.IdTutor, t.dTpEntidade, 
							(CASE t.dTpEntidade 
								WHEN 1 THEN 'Pessoa Física' 
								WHEN 2 THEN 'Pessoa Jurídica' 
							 END) AS TipoEntidade,
							t.Nome AS Tutor, t.DataNascimento, t.RG, t.CPF, t.CNPJ, 
							t.Passaporte, t.DocumentosOutros,
							t.Email AS tEmail, t.Telefone, t.Celular, Tutores.Ativo, 
							Tutores.IdOperador, Tutores.IP, Tutores.DataCadastro
					FROM	Tutores INNER JOIN
								Pessoas AS c ON Tutores.IdCliente = c.IdPessoa INNER JOIN
								Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
					WHERE	(Tutores.Ativo = 1) AND (Tutores.IdTutor = {0})", _idTutor);

            var ret = crud.ExecutarComando<TOTutoresBll>(_sql);

            foreach (var item in ret)
            {
                _retorno = item;
            }

            return _retorno;
        }

        public TOTutoresBll CarregarTO(int _idCliente, int _idTutor)
		{
			CrudDal crud = new CrudDal();
			TOTutoresBll _retorno = new TOTutoresBll();

			string _sql = string.Format(
				@"  SET DATEFORMAT dmy

					SELECT	Tutores.IdCliente, c.Nome AS Cliente, c.Email AS cEmail, 
							Tutores.IdTutores, Tutores.IdTutor, t.dTpEntidade, 
							(CASE t.dTpEntidade 
								WHEN 1 THEN 'Pessoa Física' 
								WHEN 2 THEN 'Pessoa Jurídica' 
							 END) AS TipoEntidade,
							t.Nome AS Tutor, t.DataNascimento, t.RG, t.CPF, t.CNPJ, 
							t.Passaporte, t.DocumentosOutros,
							t.Email AS tEmail, t.Telefone, t.Celular, Tutores.Ativo, 
							Tutores.IdOperador, Tutores.IP, Tutores.DataCadastro
					FROM	Tutores INNER JOIN
								Pessoas AS c ON Tutores.IdCliente = c.IdPessoa INNER JOIN
								Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
					WHERE	(Tutores.Ativo = 1) AND (Tutores.IdCliente = {0}) AND 
							(Tutores.IdTutor = {1})", _idCliente, _idTutor);

			var ret = crud.ExecutarComando<TOTutoresBll>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}
				
		public List<TOTutoresBll> Listar(bool? _ativo, int _idCliente)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SET DATEFORMAT dmy

				SELECT	Tutores.IdCliente, c.Nome AS Cliente, c.Email AS cEmail, 
						Tutores.IdTutores, Tutores.IdTutor, t.dTpEntidade, 
						(CASE t.dTpEntidade 
							WHEN 1 THEN 'Pessoa Física' 
							WHEN 2 THEN 'Pessoa Jurídica' 
						 END) AS TipoEntidade,
						t.Nome AS Tutor, t.DataNascimento, t.RG, t.CPF, t.CNPJ, 
						t.Passaporte, t.DocumentosOutros,
						t.Email AS tEmail, t.Telefone, t.Celular, Tutores.Ativo, 
						Tutores.IdOperador, Tutores.IP, Tutores.DataCadastro
				FROM	Tutores INNER JOIN
							Pessoas AS c ON Tutores.IdCliente = c.IdPessoa INNER JOIN
							Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
				WHERE	(Tutores.IdCliente = {0}) ", _idCliente);

			if (_ativo != null)
			{
				_sql += string.Format(@" AND
						(Tutores.Ativo = {0})", (_ativo.Value ? 1 : 0));
			}

			_sql += @"
				ORDER BY Cliente, Tutor";

			var lista = crud.Listar<TOTutoresBll>(_sql);

			return lista.ToList();
		}

		public List<TOTutoresBll> ListarAssinantesDosTutores(bool? _ativo, string _Tutor, 
			string _tEmail)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SET DATEFORMAT dmy

				SELECT	Tutores.IdCliente, c.Nome AS Cliente, c.Email AS cEmail, 
						Tutores.IdTutores, Tutores.IdTutor, t.dTpEntidade, 
						(CASE t.dTpEntidade 
							WHEN 1 THEN 'Pessoa Física' 
							WHEN 2 THEN 'Pessoa Jurídica' 
						 END) AS TipoEntidade,
						t.Nome AS Tutor, t.DataNascimento, t.RG, t.CPF, t.CNPJ, 
						t.Passaporte, t.DocumentosOutros,
						t.Email AS tEmail, t.Telefone, t.Celular, Tutores.Ativo, 
						Tutores.IdOperador, Tutores.IP, Tutores.DataCadastro
				FROM	Tutores INNER JOIN
							Pessoas AS c ON Tutores.IdCliente = c.IdPessoa INNER JOIN
							Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
				WHERE	((t.Nome = '{0}') OR (t.Email = '{1}'))  AND
		                (c.IdTpPessoa in (1, 2))", _Tutor, _tEmail);

			if (_ativo != null)
			{
				_sql += string.Format(@" AND
						(Tutores.Ativo = {0})", (_ativo.Value ? 1 : 0));
			}

			_sql += @"
				ORDER BY Cliente, Tutor";

			var lista = crud.Listar<TOTutoresBll>(_sql);

			return lista.ToList();
		}

		public bool IsTutor(string _nome, string _email)
		{
			CrudDal crud = new CrudDal();
			bool retorno = false;
			string _sql = string.Format(@"
				SELECT  COUNT(Tutores.IdTutores) AS Total
				FROM    Tutores INNER JOIN
							Pessoas AS t ON Tutores.IdTutor = t.IdPessoa
				WHERE   (Tutores.Ativo = 1) AND 
						((t.Nome = '{0}') OR (t.Email = '{1}')) AND
		                (t.IdTpPessoa = 3)", 
					_nome, _email);

			int reg = crud.ExecutarComandoTipoInteiro(_sql);

			retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

			return retorno;
		}

		public List<TOPessoasBll> ListarTutores(string _pNome, int _idPessoa,
			int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();
		   
			string _sql = string.Format(@"
				Execute TutoresListarSelecao
							@pesquisaTutor = '{0}', 
							@idPessoa = {1},
							@RowspPage = {2},
							@PageNumber = {3}",
				_pNome, _idPessoa, _tamPag, _pagAtual);

			List<TOPessoasBll> _retLista = crud.Listar<TOPessoasBll>(_sql).ToList();

			return _retLista.ToList();
		}

		public int TotalPaginas(string _pNome, int _idPessoa, int _tamPag)
		{
			CrudDal crud = new CrudDal();
			string _sql = string.Format(@"
				Execute TutoresListarSelecaoTotalPaginas
						@pesquisaTutor = '{0}', 
						@idPessoa = {1}", _pNome, _idPessoa);

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public int TotalAnimaisDoTutor(int _idTutor, int _idCliente)
		{
			CrudDal crud = new CrudDal();
			int retorno = 0;
			string _sql = string.Format(@"
				SELECT	Count(Animais.IdAnimal) Total
				FROM	Animais INNER JOIN
							Tutores ON Animais.IdTutores = Tutores.IdTutores
				WHERE	(Tutores.IdTutor = {0}) AND
						(Tutores.IdCliente = {1})", _idTutor, _idCliente);

			retorno = crud.ExecutarComandoTipoInteiro(_sql);

			return retorno;
		}

		public int TotalCardapiosDoTutor(int _idTutor, int _idCliente)
		{
			CrudDal crud = new CrudDal();
			int retorno = 0;
			string _sql = string.Format(@"
				Select  Count(c.IdCardapio) Total
				From    Cardapio c
				Where   c.IdAnimal in  (SELECT	Animais.IdAnimal
										FROM	Animais INNER JOIN
													Tutores ON Animais.IdTutores = 
														Tutores.IdTutores
										WHERE	(Tutores.IdTutor = {0}) AND
												(Tutores.IdCliente = {1}))", 
				_idTutor, _idCliente);

			retorno = crud.ExecutarComandoTipoInteiro(_sql);

			return retorno;
		}

		public int TotalClientesDoTutor(int _idTutor)
		{
			CrudDal crud = new CrudDal();
			int retorno = 0;
			string _sql = string.Format(@"
				SELECT  Count(p.IdPessoa) Total
				FROM    Tutores INNER JOIN
							Pessoas AS p ON Tutores.IdCliente = p.IdPessoa
				WHERE   (Tutores.IdTutor = {0})", _idTutor);

			retorno = crud.ExecutarComandoTipoInteiro(_sql);

			return retorno;
		}

		public bllRetorno ModificaTutorParaCliente(string _nome, string _eMail)
		{
			CrudDal crud = new CrudDal();
			bllRetorno _ret = new bllRetorno();
			
			string _sql = string.Format(@"
				Execute ModificaTutorParaCliente '{0}', '{1}'", _nome, _eMail);

			var retorno = crud.ExecutarComando<bllRetorno>(_sql);

			foreach (var item in retorno)
			{
				_ret = item;
			}

			return _ret;
		}
	}
}
