using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;

namespace BLL
{
	public class clAcessosBll
	{
		public bllRetorno Inserir(Acesso _acessos)
		{
			//Valida os campos de Formulário
			bllRetorno ret = _acessos.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_acessos);

					return bllRetorno.GeraRetorno(true, 
						"INSERÇÃO efetuada com sucesso!!!");

				}
				catch (Exception err)
				{
					var erro = err;
					return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a INSERÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Alterar(Acesso _acessos)
		{
			bllRetorno ret = _acessos.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_acessos);

					return bllRetorno.GeraRetorno(true, 
						"ALTERAÇÃO efetuada com sucesso!!!");

				}
				catch (Exception err)
				{
					var erro = err;
					return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a ALTERAÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public bllRetorno Excluir(Acesso _acessos)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_acessos);

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

		public Acesso Carregar(int _idPessoa)
		{
			CrudDal crud = new CrudDal();

			Acesso _usuerFunc = null;

			var consulta = crud.ExecutarComando<Acesso>(string.Format(@"
				Select *
				From Acessos
				Where (IdPessoa = {0})", _idPessoa));

			foreach (var item in consulta)
			{
				_usuerFunc = item;
			}

			return _usuerFunc;
		}

		public bool Permissao(int _idUser, string _nrTela, bool _superUser)
		{
			CrudDal crud = new CrudDal();
			bool retorno = false;
			
			if (_superUser)
			{
				retorno = true;
			}
			else
			{
				string _sql = string.Format(@"
				SELECT  COUNT(Acessos.IdPessoa) AS Total
				FROM    AcessosAuxFuncoes AS f INNER JOIN
						AcessosFuncoesTelas AS ft ON f.IdAcFunc = ft.IdAcFunc INNER JOIN
						Acessos ON f.IdAcFunc = Acessos.IdAcFunc INNER JOIN
						AcessosAuxTelas ON ft.IdTela = AcessosAuxTelas.IdTela
				WHERE   (Acessos.Ativo = 1) AND (Acessos.IdPessoa = {0}) AND 
						(AcessosAuxTelas.CodTela = '{1}')", _idUser, _nrTela);

				int reg = crud.ExecutarComandoTipoInteiro(_sql);

				retorno = Funcoes.Funcoes.ConvertePara.Bool(reg); 
			}

			return retorno;
		}

		public List<TOAcessosBll> Listar()
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT  Pessoas.IdPessoa, Pessoas.Nome, Pessoas.Email AS Usuario, 
						AcessosAuxFuncoes.IdAcFunc, AcessosAuxFuncoes.Funcao, 
						AcessosVigenciaPlanos.IdVigencia, AcessosVigenciaPlanos.DtInicial, 
						AcessosVigenciaPlanos.DtFinal, AcessosVigenciaPlanos.IdPlano, 
						(CASE PlanosAssinaturas.dNomePlano
							WHEN 1 THEN 'Básico' 
							WHEN 2 THEN 'Intermediário' 
							WHEN 3 THEN 'Completo' 
							WHEN 4 THEN 'Receituário Inteligente' 
							WHEN 5 THEN 'Prontuário' 
						END) As Plano,
						AcessosVigenciaSituacao.IdSituacao, 
						(CASE AcessosVigenciaSituacao.IdSituacao  
							WHEN 1 THEN 'cadastrado'
							WHEN 2 THEN 'Pago'
							WHEN 3 THEN 'Permitido'  
							WHEN 4 THEN 'Cancelado'  
							WHEN 5 THEN 'Alterado'  
							WHEN 6 THEN 'Renovado'
						END) As Situacao,
						PlanosAssinaturas.QtdAnimais, 
						(CASE PlanosAssinaturas.dPeriodo  
							WHEN 1 THEN 'Mensal'
							WHEN 2 THEN 'Anual'
						END) As Periodo, 
						Acessos.Inserir, Acessos.Alterar, Acessos.Excluir, Acessos.Consultar, 
						Acessos.AcoesEspeciais, Acessos.Relatorios, Acessos.SuperUser
				FROM    Pessoas INNER JOIN
						Acessos INNER JOIN
						AcessosAuxFuncoes ON Acessos.IdAcFunc = 
							AcessosAuxFuncoes.IdAcFunc ON Pessoas.IdPessoa = 
							Acessos.IdPessoa INNER JOIN
						AcessosVigenciaSituacao INNER JOIN
						AcessosVigenciaPlanos ON AcessosVigenciaSituacao.IdVigencia = 
							AcessosVigenciaPlanos.IdVigencia ON Pessoas.IdPessoa = 
							AcessosVigenciaPlanos.IdPessoa INNER JOIN
						PlanosAssinaturas ON AcessosVigenciaPlanos.IdPlano = 
							PlanosAssinaturas.IdPlano
				ORDER BY    AcessosAuxFuncoes.Funcao, Pessoas.Nome ";

			var lista = crud.Listar<TOAcessosBll>(_sql);

			return lista.ToList();
		}

		public TOSistemaDWHBll ResumoSistema(int _idPessoa)
		{
			CrudDal crud = new CrudDal();
			TOSistemaDWHBll _retorno = null;

			string _sql = string.Format(@"
				SELECT	
					(Select	Count(a.IdAlimento) 
					 From	Alimentos a
					 Where	(a.Ativo = 1) AND ((a.Compartilhar = 1) OR 
							((a.Compartilhar = 0) AND (a.IdOperador = {0})))) TotalAlimentos,
					
					(Select	Count(c.IdCardapio) 
					 From	Cardapio c
					 Where	(c.Ativo = 1) AND (c.IdPessoa = {0})) TotalCardapios,
					
					(SELECT	Count(Tutores.IdTutores) Total
					 FROM	Tutores INNER JOIN
								Pessoas AS p ON Tutores.IdCliente = p.IdPessoa
					 WHERE	(Tutores.IdCliente = {0}) AND 
							(Tutores.IdTutor <> Tutores.IdCliente)) TotalTutores,
					
					(SELECT	Count(Animais.IdAnimal) 
					 FROM	Animais INNER JOIN
								Tutores ON Animais.IdTutores = Tutores.IdTutores
					 WHERE	(Tutores.IdCliente = {0})) TotalPacientes",
			   _idPessoa);

			var consulta = crud.ExecutarComando<TOSistemaDWHBll>(_sql);

			foreach (var item in consulta)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public bool PossuiAcesso(int _idPessoa)
		{
			CrudDal crud = new CrudDal();

			int _usuerFunc = crud.ExecutarComandoTipoInteiro(string.Format(@"
				Select Count(IdPessoa) Total
				From Acessos
				Where (IdPessoa = {0})", _idPessoa));

			return Funcoes.Funcoes.ConvertePara.Bool(_usuerFunc);
		}
	}
}
