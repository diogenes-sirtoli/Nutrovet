using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Collections;

namespace BLL
{
	public class clAcessosVigenciaCupomBll
	{
		public bllRetorno Inserir(AcessosVigenciaCupomDesconto _cupom)
		{
			bllRetorno ret = _cupom.ValidarRegras(true);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Inserir(_cupom);

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

		public bllRetorno Alterar(AcessosVigenciaCupomDesconto _cupom)
		{
			bllRetorno ret = _cupom.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_cupom);

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

		public bllRetorno Excluir(AcessosVigenciaCupomDesconto _cupom)
		{
			CrudDal crud = new CrudDal();

			try
			{
				crud.Excluir(_cupom);

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

		public AcessosVigenciaCupomDesconto Carregar(int _idCupom)
		{
			CrudDal crud = new CrudDal();

			AcessosVigenciaCupomDesconto _cupom = null;

			var cupom = crud.ExecutarComando<AcessosVigenciaCupomDesconto>(
				string.Format(@"
				Select *
				From AcessosVigenciaCupomDesconto
				Where (IdCupom = {0})", _idCupom));

			foreach (var item in cupom)
			{
				_cupom = item;
			}

			return _cupom;
		}

		public AcessosVigenciaCupomDesconto Carregar(string _nrCupom)
		{
			CrudDal crud = new CrudDal();

			AcessosVigenciaCupomDesconto _cupom = null;

			var cupom = crud.ExecutarComando<AcessosVigenciaCupomDesconto>(
				string.Format(@"
				Select *
				From AcessosVigenciaCupomDesconto
				Where (NrCumpom = '{0}')", _nrCupom));

			foreach (var item in cupom)
			{
				_cupom = item;
			}

			return _cupom;
		}

		public TOAcessosVigenciaCupomBll CarregarTO(string _nrCupom)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT	c.IdCupom, c.NrCumpom AS NrCupom,
						c.dPlanoTp, 
						(CASE c.dPlanoTp 
							WHEN 1 THEN 'Plano Principal' 
							WHEN 2 THEN 'Módulo Complementar' 
							WHEN 3 THEN 'Voucher 30 Dias e 5%' 
							WHEN 4 THEN 'Voucher 30 Dias' 
							WHEN 5 THEN 'Voucher 5%'
							END) AS TipoPlano, c.DtInicial, c.DtFinal,
						c.fUsado, c.fAcessoLiberado, c.Professor,
						c.Ativo, c.IdOperador, c.IP, c.DataCadastro
				FROM	AcessosVigenciaCupomDesconto c
				Where (c.NrCumpom = '{0}')", _nrCupom);

			TOAcessosVigenciaCupomBll retCupom = 
				crud.ExecutarComando<TOAcessosVigenciaCupomBll>(_sql).FirstOrDefault();

			return retCupom;
		}

		public TOAcessosVigenciaCupomBll CarregarTO(int _idCupom)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(@"
				SELECT	c.IdCupom, c.NrCumpom AS NrCupom,
						c.dPlanoTp, 
						(CASE dPlanoTp 
							WHEN 1 THEN 'Plano Principal' 
							WHEN 2 THEN 'Módulo Complementar' 
							WHEN 3 THEN 'Voucher 30 Dias e 5%' 
							WHEN 4 THEN 'Voucher 30 Dias' 
							WHEN 5 THEN 'Voucher 5%'
							END) AS TipoPlano, c.DtInicial, c.DtFinal,
						c.fUsado, c.fAcessoLiberado, c.Professor,
						c.Ativo, c.IdOperador, c.IP, c.DataCadastro
				FROM	AcessosVigenciaCupomDesconto c
				Where (c.IdCupom = {0})", _idCupom);

			TOAcessosVigenciaCupomBll retCupom =
				crud.ExecutarComando<TOAcessosVigenciaCupomBll>(_sql).FirstOrDefault();

			return retCupom;
		}

		public bool Vigencia(string _Nrcupom)
		{
			CrudDal crud = new CrudDal();
			bool retorno = false;
			string _today = DateTime.Today.ToString("dd/MM/yyyy");
			string _sql = string.Format(@"
				SET DATEFORMAT dmy

				SELECT  COUNT(IdCupom) AS Total
				FROM    AcessosVigenciaCupomDesconto
				WHERE   ('{1}' Between DtInicial And DtFinal) AND 
						(NrCumpom = '{0}') And (fUsado = 0)", _Nrcupom, _today);

			int reg = crud.ExecutarComandoTipoInteiro(_sql);

			retorno = Funcoes.Funcoes.ConvertePara.Bool(reg);

			return retorno;
		}

		public bool InvalidarVoucher(string _Nrcupom)
		{
			bool _ret = false;

			if ((_Nrcupom != null) && (_Nrcupom != ""))
			{
				AcessosVigenciaCupomDesconto cupomDcl = Carregar(_Nrcupom);

				cupomDcl.fUsado = true;
				cupomDcl.Ativo = true;
				cupomDcl.IdOperador = 1;
				cupomDcl.DataCadastro = DateTime.Now;

				bllRetorno _retVoucher = Alterar(cupomDcl);

				_ret = _retVoucher.retorno;
			}

			return _ret;
		}

		public List<TOAcessosVigenciaCupomBll> ListarTO()
		{
			CrudDal crud = new CrudDal();

			string _sql = @"
				SELECT	c.IdCupom, c.NrCumpom AS NrCupom,
						c.dPlanoTp, 
						(CASE dPlanoTp 
							WHEN 1 THEN 'Plano Principal' 
							WHEN 2 THEN 'Módulo Complementar' 
							WHEN 3 THEN 'Voucher 30 Dias e 5%' 
							WHEN 4 THEN 'Voucher 30 Dias' 
							WHEN 5 THEN 'Voucher 5%'
							END) AS TipoPlano, c.DtInicial, c.DtFinal,
						c.fUsado, c.fAcessoLiberado, c.Professor,
						c.Ativo, c.IdOperador, c.IP, c.DataCadastro
				FROM	AcessosVigenciaCupomDesconto c
				ORDER BY NrCupom";

			var lista = crud.Listar<TOAcessosVigenciaCupomBll>(_sql);

			return lista.ToList();
		}

		public List<TOAcessosVigenciaCupomBll> Listar(string _pesqCupom, string _pesqProfessor,
			int _usado, int _tamPag, int _pagAtual)
		{
			CrudDal crud = new CrudDal();

			string _nrInicial = "", _nrFinal = "";
			int _count = 0;

			if (_pesqCupom != "")
			{
				char[] separators = new char[] { ' ', ',' };
				string[] subs = _pesqCupom.Split(separators,
					StringSplitOptions.RemoveEmptyEntries);
				_count = subs.Count();

				if (_count > 1)
				{
					_nrInicial = subs[0];
					_nrFinal = subs[1];
				}
				else if (_count > 0)
				{
					_nrInicial = subs[0];
				}
			}

			string _sql = string.Format(@"
				DECLARE @PageNumber AS INT, @RowspPage AS INT
				SET @PageNumber = {1}
				SET @RowspPage = {0}                 

				SELECT  IdCupom, NrCupom, dPlanoTp, TipoPlano, DtInicial, 
						DtFinal, fUsado, fAcessoLiberado, Professor, Ativo, 
						IdOperador, IP, DataCadastro
				FROM    (
							SELECT	ROW_NUMBER() OVER(ORDER BY NrCumpom DESC) AS NUMBER, 
									c.IdCupom, c.NrCumpom AS NrCupom,
									c.dPlanoTp, 
									(CASE dPlanoTp 
										WHEN 1 THEN 'Plano Principal' 
										WHEN 2 THEN 'Módulo Complementar' 
										WHEN 3 THEN 'Voucher 30 Dias e 5%' 
										WHEN 4 THEN 'Voucher 30 Dias' 
										WHEN 5 THEN 'Voucher 5%'
										END) AS TipoPlano, c.DtInicial, c.DtFinal,
									c.fUsado, c.fAcessoLiberado, c.Professor,
									c.Ativo, c.IdOperador, c.IP, c.DataCadastro
							FROM	AcessosVigenciaCupomDesconto c
							WHERE   (c.Ativo = 1) ",
							_tamPag, _pagAtual);


			if (_count > 1)
			{
				_sql += string.Format(@" AND 
									(c.NrCumpom BETWEEN '{0}' AND '{1}')", _nrInicial,
									_nrFinal);
			}
			else if (_count > 0)
			{
				_sql += string.Format(@" AND 
									(c.NrCumpom = '{0}')", _nrInicial);
			}

			if ((_pesqProfessor != null) && (_pesqProfessor != ""))
			{
				_sql += string.Format(@" AND 
									(c.Professor = '{0}')",
					_pesqProfessor);
			}

			switch (_usado)
			{
				case 1:
					{
						_sql += @" AND 
									(c.FUsado = 1)";
						break;
					}
				case 2:
					{
						_sql += @" AND 
									(c.FUsado = 0)";
						break;
					}

			}

			_sql += @") AS TBL
				WHERE   NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND 
						(@PageNumber * @RowspPage)";

			var lista = crud.Listar<TOAcessosVigenciaCupomBll>(_sql);

			return lista.ToList();
		}

		public int TotalPaginas(string _pesqCupom, string _pesqProfessor, int _usado, 
			int _tamPag)
		{
			CrudDal crud = new CrudDal();
			string _nrInicial = "", _nrFinal = "";
			int _count = 0;

			if (_pesqCupom != "")
			{
				char[] separators = new char[] { ' ', ',' };
				string[] subs = _pesqCupom.Split(separators,
					StringSplitOptions.RemoveEmptyEntries);
				_count = subs.Count();

				if (_count > 1)
				{
					_nrInicial = subs[0];
					_nrFinal = subs[1];
				}
				else if (_count > 0)
				{
					_nrInicial = subs[0];
				}
			}

			string _sql = @"
				SELECT	Count(c.IdCupom) Total
				FROM	AcessosVigenciaCupomDesconto c
				WHERE   (c.Ativo = 1)";

			if (_count > 1)
			{
				_sql += string.Format(@" AND 
									(c.NrCumpom BETWEEN '{0}' AND '{1}')", _nrInicial,
									_nrFinal);
			}
			else if (_count > 0)
			{
				_sql += string.Format(@" AND 
									(c.NrCumpom = '{0}')", _nrInicial);
			}


			if ((_pesqProfessor != null) && (_pesqProfessor != ""))
			{
				_sql += string.Format(@" AND 
									(c.Professor = '{0}')",
					_pesqProfessor);
			}

			switch (_usado)
			{
				case 1:
					{
						_sql += @" AND 
									(c.FUsado = 1)";
						break;
					}
				case 2:
					{
						_sql += @" AND 
									(c.FUsado = 0)";
						break;
					}

			}

			int _total = crud.ExecutarComandoTipoInteiro(_sql);

			decimal _quantPag = Funcoes.Funcoes.TotalPaginas(_tamPag, _total);

			return Funcoes.Funcoes.ConvertePara.Int(_quantPag);
		}

		public List<TOTela3Bll> ListarProfessores()
		{
			CrudDal crud = new CrudDal();
			List<TOTela3Bll> _retLista = new List<TOTela3Bll>();

			string _sql = @"
				Select distinct c.Professor AS Nome
				From AcessosVigenciaCupomDesconto c
				Where (c.Professor is not null) and (c.Professor <> '')
				Order By c.Professor";

			_retLista = crud.Listar<TOTela3Bll>(_sql).ToList();

			return _retLista;
		}

		public ArrayList GerarVouchers(int _QuantVouchers, int _dPlanoTp, DateTime _dtIni, 
			DateTime _dtFim, string _professor, bool _acessoLiberado)
		{
			ArrayList _ret = new ArrayList();
			CrudDal crud = new CrudDal();
			int _nrInicial = Funcoes.Funcoes.ConvertePara.Int(GerarNumeroCupom());

			if (_nrInicial > 0)
			{
				for (int i = 0; i < _QuantVouchers; i++)
				{
					string _sql = string.Format(@"
					SET DATEFORMAT dmy

					Insert Into AcessosVigenciaCupomDesconto 
						(NrCumpom, dPlanoTp, DtInicial, DtFinal, fUsado, 
						 fAcessoLiberado, Professor, Ativo, IdOperador, DataCadastro)
					Values ({0}, {1}, '{2}', '{3}', {4}, {5}, '{6}', {7}, {8}, '{9}')",
					_nrInicial + i, _dPlanoTp, _dtIni.ToString("dd/MM/yyyy"),
					_dtFim.ToString("dd/MM/yyyy"), 0, (_acessoLiberado ? 1 : 0), 
					_professor, 1, 1, DateTime.Now);

					int executar = crud.ExecutarComando(_sql);

					_ret.Add(executar);
				}
			}

			return _ret;
		}

		public bllRetorno VoucherSituacao(string _nrCupom)
		{
			CrudDal crud = new CrudDal();
			bllRetorno retorno = new bllRetorno();
			TOAcessosVigenciaCupomBll cupomTO = CarregarTO(_nrCupom);
			
			if ((cupomTO != null) && (cupomTO.NrCupom != null) && 
				(cupomTO.NrCupom != ""))
			{
				List<object> _listaCupom = new List<object>();
				
				if (Vigencia(_nrCupom))
				{
					_listaCupom.Add(cupomTO);

					retorno.retorno = true;
					retorno.mensagem = "Voucher Válido!!!";
					retorno.objeto = _listaCupom.ToList();

				}
				else
				{
					retorno.retorno = false;
					retorno.mensagem = "Voucher já Utilizado ou Fora da Validade!!!";
				}
			}
			else
			{
				retorno.retorno = false;
				retorno.mensagem = "Voucher Inválido!!!";
			}

			return retorno;
		}

		public string GerarNumeroCupom()
		{
			CrudDal crud = new CrudDal();
			string retorno = "";
			string _sql = @"
				Select	(c.NrCumpom + 1) NrCumpom
				From	AcessosVigenciaCupomDesconto c
				Where	(c.NrCumpom = (Select MAX(avcd.NrCumpom)
									  From AcessosVigenciaCupomDesconto avcd))";

			int reg = crud.ExecutarComandoTipoInteiro(_sql);

			retorno = Funcoes.Funcoes.ConvertePara.String(reg);

			return retorno;
		}
	}
}
