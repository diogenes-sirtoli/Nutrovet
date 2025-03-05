using System;
using System.Collections.Generic;
using System.Linq;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
	public class clAcessosVigenciaPlanosBll
	{
		public bllRetorno Inserir(AcessosVigenciaPlano _vigPlan)
		{
			bllRetorno ret = _vigPlan.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_vigPlan);

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

		public bllRetorno Alterar(AcessosVigenciaPlano _vigPlan)
		{
			bllRetorno ret = _vigPlan.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_vigPlan);

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

		public bllRetorno Excluir(AcessosVigenciaPlano _vigPlan)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_vigPlan);

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

		public AcessosVigenciaPlano Carregar(int _idVigencia)
		{
			CrudDal crud = new CrudDal();
			AcessosVigenciaPlano _retPlano = null;

			var plano = crud.ExecutarComando<AcessosVigenciaPlano>(string.Format(@"
				Select *
				From AcessosVigenciaPlanos
				Where (IdVigencia = {0})", _idVigencia));

			foreach (var item in plano)
			{
				_retPlano = item;
			}

			return _retPlano;
		}

		public AcessosVigenciaPlano CarregarPlanoVigente(int _idPessoa)
		{
			CrudDal crud = new CrudDal();
			AcessosVigenciaPlano _retPlano = null;
			string _today = DateTime.Today.ToString("dd/MM/yyyy");
			string _sql = string.Format(@"
				SET DATEFORMAT dmy

				SELECT	IdPessoa, IdVigencia, IdPlano, IdSubscriptionPagarMe, StatusPagarMe, 
						DtInicial, DtFinal, IdCartao, IdCupom, Ativo, IdOperador, IP, 
						DataCadastro, Versao
				FROM	AcessosVigenciaPlanos
				WHERE	(IdVigencia =	(SELECT	MAX(p.IdVigencia) AS IdVigencia
										 FROM	AcessosVigenciaPlanos AS p INNER JOIN
													AcessosVigenciaSituacao AS s ON 
														p.IdVigencia = s.IdVigencia
										 WHERE	(p.IdPessoa = {0}) AND (p.Ativo = 1) AND
												('{1}' BETWEEN p.DtInicial AND p.DtFinal) AND 
												(s.IdSituacao = 3))) ", _idPessoa, _today);

			var plano = crud.ExecutarComando<AcessosVigenciaPlano>(_sql);

			return _retPlano = plano.FirstOrDefault();
		}

		public TOAcessosVigenciaPlanosBll CarregarTO(int _idVigencia)
		{
			CrudDal crud = new CrudDal();

			TOAcessosVigenciaPlanosBll _usuerFunc = null;
			string _sql = string.Format(@"
				SELECT	avp.IdPessoa, p.Nome, avp.IdVigencia, avp.IdPlano, pa.IdPlanoPagarMe, 
						pa.IdPlanoPagarMeTestes, avp.IdSubscriptionPagarMe, avp.StatusPagarMe,
						(CASE pa.dNomePlano 
							When 1 Then 'Básico'
							When 2 Then 'Intermediário'
							When 3 Then 'Completo'
							When 4 Then 'Receituário'
							When 5 Then 'Prontuário'
							END) AS Plano, pa.ValorPlano, avp.DtInicial, avp.DtFinal, avp.IdCartao, 
						cartao.NrCartao, cartao.CodSeg, cartao.dBandeira, pa.dNomePlano,
						(CASE cartao.dBandeira 
							When null Then ''
							When 0 Then ''
							When 1 Then 'Visa'
							When 2 Then 'Master'
							When 3 Then 'Dinners'
							When 4 Then 'Elo'
							When 5 Then 'Hipercard'
							When 6 Then 'American Express' 
							END) AS Bandeira,
							cartao.VencimCartao, cartao.NomeCartao, avp.IdCupom, 
							cupom.NrCumpom AS NrCupom, cupom.DtInicial AS ValidadeCupomInicial, 
							cupom.DtFinal AS ValidadeCupomFinal, cupom.fAcessoLiberado, pa.QtdAnimais, 
							pa.dPeriodo, 
						(CASE pa.dperiodo 
							WHEN 1 THEN 'Mensal' 
							WHEN 2 THEN 'Anual' 
							END) AS Periodo,
						(SELECT	(CASE s.IdSituacao 
									WHEN 1 THEN 'Julgamento' 
									WHEN 2 THEN 'Pago' 
									WHEN 3 THEN 'Permitido' 
									WHEN 4 THEN 'Cancelado' 
									WHEN 5 THEN 'Pagamento Pendente' 
									WHEN 6 THEN 'Não Pago' 
									WHEN 7 THEN 'Encerrado'
									END)
							FROM	AcessosVigenciaSituacao AS s
							WHERE	(s.IdVigencia = avp.IdVigencia) AND 
									(s.IdVigSit = (SELECT	MAX(sm.IdVigSit)
												   FROM     AcessosVigenciaSituacao AS sm
												   WHERE	(sm.IdVigencia = avp.IdVigencia)))) AS Situacao, 
						avp.Ativo, avp.IdOperador, avp.IP, avp.DataCadastro
				FROM	AcessosVigenciaPlanos AS avp INNER JOIN
							Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
							PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
							PessoasCartaoCredito AS cartao ON avp.IdCartao = 
								cartao.IdCartao LEFT OUTER JOIN
							AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
				WHERE   (avp.IdVigencia = {0})", _idVigencia);


			var logon = crud.ExecutarComando<TOAcessosVigenciaPlanosBll>(_sql).FirstOrDefault();

			_usuerFunc = Descriptografa(logon);

			return _usuerFunc;
		}

		public TOAcessosVigenciaPlanosBll CarregarPlano(int _idPessoa)
		{
			CrudDal crud = new CrudDal();

			TOAcessosVigenciaPlanosBll _usuerFunc = null;
			string _sql = string.Format(@"
				SET DATEFORMAT dmy

				SELECT	avp.IdPessoa, p.Nome, avp.IdVigencia, avp.IdPlano, pa.IdPlanoPagarMe, 
						pa.IdPlanoPagarMeTestes, avp.IdSubscriptionPagarMe, avp.StatusPagarMe,
						(CASE pa.dNomePlano 
							When 1 Then 'Básico'
							When 2 Then 'Intermediário'
							When 3 Then 'Completo'
							When 4 Then 'Receituário'
							When 5 Then 'Prontuário'
						 END) AS Plano, pa.ValorPlano, avp.DtInicial, avp.DtFinal, avp.IdCartao, 
						cartao.NrCartao, cartao.CodSeg, cartao.dBandeira, pa.dNomePlano,
						 (CASE cartao.dBandeira 
							When null Then ''
							When 0 Then ''
							When 1 Then 'Visa'
							When 2 Then 'Master'
							When 3 Then 'Dinners'
							When 4 Then 'Elo'
							When 5 Then 'Hipercard'
							When 6 Then 'American Express' 
						 END) AS Bandeira,
						 cartao.VencimCartao, cartao.NomeCartao, avp.IdCupom, 
						 cupom.NrCumpom AS NrCupom, cupom.DtInicial AS ValidadeCupomInicial, 
						 cupom.DtFinal AS ValidadeCupomFinal, cupom.fAcessoLiberado, pa.QtdAnimais, 
						 pa.dPeriodo, 
						 (CASE pa.dperiodo 
							WHEN 1 THEN 'Mensal' 
							WHEN 2 THEN 'Anual' 
						 END) AS Periodo,
						 (SELECT	(CASE s.IdSituacao 
									WHEN 1 THEN 'Julgamento' 
									WHEN 2 THEN 'Pago' 
									WHEN 3 THEN 'Permitido' 
									WHEN 4 THEN 'Cancelado' 
									WHEN 5 THEN 'Pagamento Pendente' 
									WHEN 6 THEN 'Não Pago' 
									WHEN 7 THEN 'Encerrado'
									END)
						FROM	AcessosVigenciaSituacao AS s
						WHERE	(s.IdVigencia = avp.IdVigencia) AND 
								(s.IdVigSit = (SELECT	MAX(sm.IdVigSit)
												   FROM     AcessosVigenciaSituacao AS sm
												   WHERE	(sm.IdVigencia = avp.IdVigencia)))) AS Situacao, 
							avp.Ativo, avp.IdOperador, avp.IP, avp.DataCadastro
				FROM	AcessosVigenciaPlanos AS avp INNER JOIN
							Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
							PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
							PessoasCartaoCredito AS cartao ON avp.IdCartao = 
								cartao.IdCartao LEFT OUTER JOIN
							AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
				WHERE   (avp.IdVigencia =  (SELECT	MAX(p.IdVigencia) AS IdVigencia
											FROM	AcessosVigenciaPlanos AS p INNER JOIN
													AcessosVigenciaSituacao AS s ON 
														p.IdVigencia = s.IdVigencia
											WHERE	(p.IdPessoa = {0}) AND (p.Ativo = 1))
						)", _idPessoa);


			var logon = crud.ExecutarComando<TOAcessosVigenciaPlanosBll>(_sql).FirstOrDefault();

			_usuerFunc = Descriptografa(logon);

			return _usuerFunc;
		}

		public List<AcessosVigenciaPlano> ListarPlanos(int _idPessoa)
		{
			CrudDal crud = new CrudDal();
			List<AcessosVigenciaPlano> _retPlanos = new List<AcessosVigenciaPlano>();
			string _sql = string.Format(@"
				SELECT	IdPessoa, IdVigencia, IdPlano, IdSubscriptionPagarMe, StatusPagarMe, 
						DtInicial, DtFinal, IdCartao, IdCupom, Ativo, IdOperador, 
						IP, DataCadastro
				FROM	AcessosVigenciaPlanos
				WHERE	(IdPessoa = {0})
				ORDER BY DtInicial DESC", _idPessoa);

			_retPlanos = crud.Listar<AcessosVigenciaPlano>(_sql).ToList();

			return _retPlanos;
		}

		public List<TOAcessosVigenciaPlanosBll> ListarTO(int _idPessoa)
		{
			CrudDal crud = new CrudDal();
			List<TOAcessosVigenciaPlanosBll> _retPlanos = new List<TOAcessosVigenciaPlanosBll>();
			TOAcessosVigenciaPlanosBll _itemPlano;

			string _sql = string.Format(@"

				SELECT	avp.IdPessoa, p.Nome, avp.IdVigencia, avp.IdPlano, pa.IdPlanoPagarMe, 
						pa.IdPlanoPagarMeTestes, avp.IdSubscriptionPagarMe, avp.StatusPagarMe,
						pa.dNomePlano,
						(CASE pa.dNomePlano 
							When 1 Then 'Básico'
							When 2 Then 'Intermediário'
							When 3 Then 'Completo'
							When 4 Then 'Receituário'
							When 5 Then 'Prontuário'
						 END) AS Plano, pa.ValorPlano, avp.DtInicial, avp.DtFinal, 
						avp.IdCartao, cartao.NrCartao, cartao.CodSeg, cartao.dBandeira, 
						 (CASE cartao.dBandeira 
							When null Then ''
							When 0 Then ''
							When 1 Then 'Visa'
							When 2 Then 'Master'
							When 3 Then 'Dinners'
							When 4 Then 'Elo'
							When 5 Then 'Hipercard'
							When 6 Then 'American Express' 
						 END) AS Bandeira,
						 cartao.VencimCartao, cartao.NomeCartao, avp.IdCupom, 
						 cupom.NrCumpom AS NrCupom, cupom.DtInicial AS ValidadeCupomInicial, 
						 cupom.DtFinal AS ValidadeCupomFinal, cupom.fAcessoLiberado, 
						pa.QtdAnimais, pa.dPeriodo, 
						 (CASE pa.dperiodo 
							WHEN 1 THEN 'Mensal' 
							WHEN 2 THEN 'Anual' 
						 END) AS Periodo,
						(SELECT	(CASE s.IdSituacao 
									WHEN 1 THEN 'Julgamento' 
									WHEN 2 THEN 'Pago' 
									WHEN 3 THEN 'Permitido' 
									WHEN 4 THEN 'Cancelado' 
									WHEN 5 THEN 'Pagamento Pendente' 
									WHEN 6 THEN 'Não Pago' 
									WHEN 7 THEN 'Encerrado'
									END)
						FROM	AcessosVigenciaSituacao AS s
						WHERE	(s.IdVigencia = avp.IdVigencia) AND 
								(s.IdVigSit = (SELECT	MAX(sm.IdVigSit)
											   FROM     AcessosVigenciaSituacao AS sm
											   WHERE	(sm.IdVigencia = avp.IdVigencia)))) AS Situacao, 
							avp.Ativo, avp.IdOperador, avp.IP, avp.DataCadastro
				FROM	AcessosVigenciaPlanos AS avp INNER JOIN
							Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
							PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
							PessoasCartaoCredito AS cartao ON avp.IdCartao = 
								cartao.IdCartao LEFT OUTER JOIN
							AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
				WHERE   (avp.IdPessoa = {0})
				ORDER BY avp.DtInicial DESC", _idPessoa);

			var lista = crud.Listar<TOAcessosVigenciaPlanosBll>(_sql);

			if (lista != null)
			{
				foreach (var item in lista)
				{
					_itemPlano = Descriptografa(item);

					_retPlanos.Add(_itemPlano);
				}
			}

			return _retPlanos.ToList();
		}

		public List<TOAcessosVigenciaPlanosBll> ListarClientes(string _pesqNome, int _tamPag,
			int _pagAtual, int _nrDiasAVencer, int _nrTipoAssinatura)
		{
			CrudDal crud = new CrudDal();
			List<TOAcessosVigenciaPlanosBll> _retPlanos = new List<TOAcessosVigenciaPlanosBll>();
			TOAcessosVigenciaPlanosBll _itemPlano;

			string _sql = string.Format(@"

				SET DATEFORMAT dmy

				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}

				SELECT	IdPessoa, Nome, Email, Usuario, Senha, Bloqueado, AcessoNoSistema,
						IdVigencia, IdPlano, IdSubscriptionPagarMe, StatusPagarMe, Plano, 
						IdPlanoPagarMe, IdPlanoPagarMeTestes, ValorPlano, DtInicial, DtFinal, 
						IdCartao, NrCartao, CodSeg, dBandeira, Bandeira, IdCupom, NrCupom, 
						ValidadeCupomInicial, ValidadeCupomFinal, fAcessoLiberado, QtdAnimais, 
						dPeriodo, Periodo, Situacao, NaVigencia, Ativo, IdOperador, IP, 
						DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY p.Nome) AS NUMBER,
									avp.IdPessoa, p.Nome, p.Email, p.Usuario, p.Senha, p.Bloqueado, 
									(Select Cast(Count(a.IdPessoa) as bit)
									 From Acessos a
									 Where a.IdPessoa = avp.IdPessoa) As AcessoNoSistema,
									avp.IdVigencia, 
									avp.IdPlano, pa.IdPlanoPagarMe, pa.IdPlanoPagarMeTestes, 
									avp.IdSubscriptionPagarMe, avp.StatusPagarMe, 
									(CASE pa.dNomePlano 
										When 1 Then 'Básico'
										When 2 Then 'Intermediário'
										When 3 Then 'Completo'
										When 4 Then 'Receituário'
										When 5 Then 'Prontuário'
									 END) AS Plano, pa.ValorPlano, avp.DtInicial, avp.DtFinal, 
									avp.IdCartao, cartao.NrCartao, cartao.CodSeg, cartao.dBandeira, 
									(CASE cartao.dBandeira 
											When null Then ''
											When 0 Then ''
											When 1 Then 'Visa'
											When 2 Then 'Master'
											When 3 Then 'Dinners'
											When 4 Then 'Elo'
											When 5 Then 'Hipercard'
											When 6 Then 'American Express' 
									 END) AS Bandeira,
									 cartao.VencimCartao, cartao.NomeCartao, 
									 avp.IdCupom, cupom.NrCumpom AS NrCupom, 
									 cupom.DtInicial AS ValidadeCupomInicial, 
									 cupom.DtFinal AS ValidadeCupomFinal, 
									 cupom.fAcessoLiberado, pa.QtdAnimais, pa.dPeriodo, 
									(CASE pa.dperiodo 
										WHEN 1 THEN 'Mensal' 
										WHEN 2 THEN 'Anual' 
									 END) AS Periodo, 
									(SELECT	(CASE s.IdSituacao 
												WHEN 1 THEN 'Julgamento' 
												WHEN 2 THEN 'Pago' 
												WHEN 3 THEN 'Permitido' 
												WHEN 4 THEN 'Cancelado' 
												WHEN 5 THEN 'Pagamento Pendente' 
												WHEN 6 THEN 'Não Pago' 
												WHEN 7 THEN 'Encerrado'
												END)
									 FROM	AcessosVigenciaSituacao AS s
									 WHERE	(s.IdVigencia = avp.IdVigencia) AND 
											(s.IdVigSit = (SELECT	MAX(sm.IdVigSit)
															FROM     AcessosVigenciaSituacao AS sm
															WHERE	(sm.IdVigencia = avp.IdVigencia)))) AS Situacao,
									(SELECT	COUNT(plano.IdVigencia) AS IdVigencia
									 FROM	AcessosVigenciaPlanos AS plano INNER JOIN
												AcessosVigenciaSituacao AS sit ON plano.IdVigencia = sit.IdVigencia
									 WHERE	(plano.IdPessoa = p.IdPessoa) AND 
											('{2}' BETWEEN plano.DtInicial AND plano.DtFinal) AND 
											(sit.IdSituacao = 3)) AS NaVigencia, avp.Ativo, avp.IdOperador, 
									avp.IP, avp.DataCadastro
							FROM	AcessosVigenciaPlanos AS avp INNER JOIN
										Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
										PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
										PessoasCartaoCredito AS cartao ON avp.IdCartao = 
											cartao.IdCartao LEFT OUTER JOIN
										AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
							WHERE	(p.IdTpPessoa = 2) AND 
									(avp.IdVigencia = (SELECT	MAX(pl.IdVigencia)
													   FROM		AcessosVigenciaPlanos AS pl
													   WHERE	(pl.IdPessoa = p.IdPessoa) AND 
																(pl.Ativo = 1))) ",
				_tamPag, _pagAtual, DateTime.Today.ToString("dd/MM/yyyy"));

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
									((p.Nome Like '%{0}%'  COLLATE Latin1_General_CI_AI) OR
									 (p.Email Like '%{0}%'  COLLATE Latin1_General_CI_AI))",
					   _pesqNome);
			}

			if (_nrDiasAVencer > 0 && _nrTipoAssinatura != 1)
			{
				_sql += string.Format(@" AND 
									(avp.DtFinal BETWEEN '{0}' AND '{1}')",
						DateTime.Today.ToString("dd/MM/yyyy"),
						DateTime.Today.AddDays(_nrDiasAVencer).ToString("dd/MM/yyyy"));
			}

			if (_nrTipoAssinatura == 1)
			{
				_sql += string.Format(@" AND 
									(avp.DtFinal < '{0}')",
						DateTime.Today.ToString("dd/MM/yyyy"));
			}

			if (_nrTipoAssinatura == 2)
			{
				_sql += string.Format(@" AND 
									(avp.IdSubscriptionPagarMe is not null)");
			}

			if (_nrTipoAssinatura == 3)
			{
				_sql += string.Format(@" AND 
									(avp.IdSubscriptionPagarMe is null or 
									 avp.IdSubscriptionPagarMe = '')");
			}

			_sql += @"
						) AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)";

			var lista = crud.Listar<TOAcessosVigenciaPlanosBll>(_sql);

			if (lista != null)
			{
				foreach (var item in lista)
				{
					_itemPlano = Descriptografa(item);

					_retPlanos.Add(_itemPlano);
				}
			}

			return _retPlanos.ToList();
		}

		public int TotalPaginasClientes(string _pesqNome, int _tamPag, int _nrDiasAVencer, 
			int _nrTipoAssinatura)
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	COUNT(avp.IdVigencia) Total
				FROM	AcessosVigenciaPlanos AS avp INNER JOIN
							Pessoas AS p ON avp.IdPessoa = p.IdPessoa INNER JOIN
							PlanosAssinaturas AS pa ON avp.IdPlano = pa.IdPlano LEFT OUTER JOIN
							PessoasCartaoCredito AS cartao ON avp.IdCartao = 
								cartao.IdCartao LEFT OUTER JOIN
							AcessosVigenciaCupomDesconto AS cupom ON avp.IdCupom = cupom.IdCupom
				WHERE	(p.IdTpPessoa = 2) AND 
						(avp.IdVigencia = (SELECT	MAX(pl.IdVigencia)
											FROM	AcessosVigenciaPlanos AS pl
											WHERE	(pl.IdPessoa = p.IdPessoa) AND (pl.Ativo = 1)))";

			if (_pesqNome != "")
			{
				_sql += string.Format(@" AND 
						((p.Nome Like '%{0}%'  COLLATE Latin1_General_CI_AI) OR
						 (p.Email Like '%{0}%'  COLLATE Latin1_General_CI_AI))",
					_pesqNome);
			}

			if (_nrDiasAVencer > 0)
			{
				_sql += string.Format(@" AND 
						(avp.DtFinal BETWEEN '{0}' AND '{1}')",
						DateTime.Today.ToString("dd/MM/yyyy"),
						DateTime.Today.AddDays(_nrDiasAVencer).ToString("dd/MM/yyyy"));
			}

			if (_nrTipoAssinatura == 2)
			{
				_sql += string.Format(@" AND 
						(avp.IdSubscriptionPagarMe is not null)");
			}

			if (_nrTipoAssinatura == 3)
			{
				_sql += string.Format(@" AND 
						(avp.IdSubscriptionPagarMe is null 
						 or avp.IdSubscriptionPagarMe = '')");
			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public ListItem[] ListarPeriodo()
		{
			ListItem[] periodo = Funcoes.Funcoes.GetEnumList<DominiosBll.Periodo>();

			return periodo;
		}

		public bool PlanoEstaNaVigencia(int _idPessoa)
		{
			CrudDal crud = new CrudDal();
			bool retorno = false;
			string _today = DateTime.Today.ToString("dd/MM/yyyy");
			string _sql = string.Format(@"
				SET DATEFORMAT dmy
				Declare @IdPagarMe as varchar(50);
				Declare @IdSituacao as int;
				Declare @IdVigencia as int;

				Set @IdPagarMe = (Select top 1 IdSubscriptionPagarMe
								  From	AcessosVigenciaPlanos
								  Where	(IdPessoa = {0})
								  Order by IdVigencia desc);

				Set @IdVigencia = (Select top 1 IdVigencia
								   From	AcessosVigenciaPlanos
								   Where	(IdPessoa = {0})
								   Order by IdVigencia desc);

				If ((@IdPagarMe is not null) AND (@IdPagarMe <> '')) 
					Begin
						Set @IdSituacao = ( SELECT  s.IdSituacao
											FROM    AcessosVigenciaPlanos AS p INNER JOIN
														AcessosVigenciaSituacao AS s ON 
															p.IdVigencia = s.IdVigencia
											WHERE	(p.IdPessoa = {0}) AND (p.Ativo = 1) AND
													('{1}' BETWEEN p.DtInicial AND p.DtFinal) AND
													(p.IdSubscriptionPagarMe = @IdPagarMe) AND
													(s.IdVigSit = (	SELECT	MAX(sm.IdVigSit) AS IdVigSit
																	FROM	AcessosVigenciaSituacao AS sm 
																	WHERE	(sm.IdVigencia = p.IdVigencia))));
					End
				Else
					Begin
						Set @IdSituacao = (SELECT	s.IdSituacao
										   FROM		AcessosVigenciaPlanos AS p INNER JOIN
														AcessosVigenciaSituacao AS s ON 
															p.IdVigencia = s.IdVigencia
										   WHERE	(p.IdPessoa = {0}) AND (p.Ativo = 1) AND
													('{1}' BETWEEN p.DtInicial AND p.DtFinal) AND
													(p.IdVigencia = @IdVigencia) AND
													(s.IdVigSit = (	SELECT	MAX(sm.IdVigSit) AS IdVigSit
																	FROM	AcessosVigenciaSituacao AS sm 
																	WHERE	(sm.IdVigencia = p.IdVigencia))));
					End

				Select Coalesce(@IdSituacao, 0) As IdSituacao;", _idPessoa, _today);

			int reg = crud.ExecutarComandoTipoInteiro(_sql);

			switch (reg)
			{
				case 1:
				case 2:
				case 3:
					{
						retorno = true;

						break;
					}
				default:
					{
						retorno = false;
						break;
					}
			}

			return retorno;
		}

		public bllRetorno EhClienteVigente(string _nome, string _email)
		{
			bllRetorno bllRetorno = new bllRetorno();
			CrudDal crud = new CrudDal();
			clPessoasBll pessoaBll = new clPessoasBll();
			TOPessoasBll pessoaTO;

			pessoaTO = pessoaBll.CarregarNomeOuEMail(_nome, _email);

			if (pessoaTO != null)
			{
				if (Funcoes.Funcoes.ConvertePara.Bool(pessoaTO.cBloqueado))
				{
					if (pessoaTO.cIdTpPessoa == (int)DominiosBll.PessoasAuxTipos.Cliente)
					{
						bllRetorno.retorno = false;
						bllRetorno.mensagem =
							"Identificamos que Você JÁ É nosso cliente, mas está com seu Perfil BLOQUEADO! Contate o Administrador Através do WhatsApp (61) 98136-6230!!!";
					}
					else
					{
						bllRetorno.retorno = true;
						bllRetorno.mensagem = "Prosseguir na Compra";
					}
				}
				else
				{
					if (pessoaTO.cIdTpPessoa == (int)DominiosBll.PessoasAuxTipos.Cliente)
					{
						bllRetorno.retorno = false;
						bllRetorno.mensagem =
							"Identificamos que Você JÁ É nosso cliente! Faça LOGIN e Renove seu Plano Através da TELA do seu PERFIL!!!";
					}
					else
					{
						bllRetorno.retorno = true;
						bllRetorno.mensagem = "Prosseguir na Compra";
					}
				}
			}
			else
			{
				bllRetorno.retorno = true;
				bllRetorno.mensagem = "Prosseguir na Compra";
			}

			return bllRetorno;
		}

		private TOAcessosVigenciaPlanosBll Descriptografa(TOAcessosVigenciaPlanosBll _plano)
		{
			TOAcessosVigenciaPlanosBll _retPlano = new TOAcessosVigenciaPlanosBll();

			if (_plano != null)
			{
				_retPlano = _plano;

				_retPlano.NrCartao = (_plano.NrCartao != null && _plano.NrCartao != "" ?
						Funcoes.Funcoes.Seguranca.Descriptografar(_plano.NrCartao) : "");
				_retPlano.CodSeg = (_plano.CodSeg != null && _plano.CodSeg != "" ?
					Funcoes.Funcoes.Seguranca.Descriptografar(_plano.CodSeg) : "");
				_retPlano.VencimCartao = (_plano.VencimCartao != null && _plano.VencimCartao != "" ?
					Funcoes.Funcoes.Seguranca.Descriptografar(_plano.VencimCartao) : "");
				_retPlano.NomeCartao = (_plano.NomeCartao != null && _plano.NomeCartao != "" ?
					Funcoes.Funcoes.Seguranca.Descriptografar(_plano.NomeCartao) : "");
			}

			return _retPlano;
		}

		public bool DiretoParaTelaPerfil(DominiosBll.AcessosPlanosAuxSituacaoIngles _situacao)
		{
			bool _ret = false;
			int _falha = (int)_situacao;

			if (_falha > 0)
			{
				switch (_situacao)
				{
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Canceled:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.PendingPayment:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Unpaid:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Ended:
						{
							_ret = true;

							break;
						}
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Trialing:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Paid:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Allowed:
						{
							_ret = false;

							break;
						}
				}
			}
			else
			{
				_ret = true;
			}

			return _ret;
		}

		public bool MostraBotaoRenovarAssinatura(
			DominiosBll.AcessosPlanosAuxSituacaoIngles _situacao,
			DateTime _dataFinalPlano)
		{
			bool _ret = false;
			int _falha = (int)_situacao;

			if (_falha > 0)
			{
				switch (_situacao)
				{
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Canceled:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Ended:
						{
							_ret = true;

							break;
						}
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.PendingPayment:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Trialing:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Paid:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Unpaid:
					case DominiosBll.AcessosPlanosAuxSituacaoIngles.Allowed:
						{
                            if (_dataFinalPlano <= DateTime.Today)
                            {
                                _ret = true;
                            }
                            else
                            {
                                _ret = false;
                            }

                            break;
						}
				}
			}
			else
			{
				if (_dataFinalPlano <= DateTime.Today)
				{
					_ret = true;
				}
				else
				{
					_ret = false;
				}
			}

			return _ret;
		}

		public bool MostraBotaoUpgradeAssinatura(
			DominiosBll.AcessosPlanosAuxSituacaoIngles _situacao)
		{
			bool _ret = false;

			switch (_situacao)
			{
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.Canceled:
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.Ended:
					{
						_ret = false;

						break;
					}
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.PendingPayment:
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.Trialing:
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.Paid:
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.Unpaid:
				case DominiosBll.AcessosPlanosAuxSituacaoIngles.Allowed:
					{
						_ret = true;

						break;
					}
			}

			return _ret;
		}
	}
}
