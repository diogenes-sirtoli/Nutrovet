using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;

namespace BLL
{
	public class clPlanosAssinaturasBll
	{
		public bllRetorno Inserir(PlanosAssinatura _plano)
		{
			bllRetorno ret = _plano.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						crud.Inserir(_plano);
					}

					return bllRetorno.GeraRetorno(true,
						"INSERÇÃO efetuada com sucesso!!!");
				}
				else
				{
					return ret;
				}
			}
			catch
			{
				return bllRetorno.GeraRetorno(false,
					"Não foi possível efetuar a INSERÇÃO!!!");
			}
		}

		public bllRetorno Alterar(PlanosAssinatura _plano)
		{
			bllRetorno ret = _plano.ValidarRegras(false);

			if (ret.retorno)
			{
				CrudDal crud = new CrudDal();

				try
				{
					crud.Alterar(_plano);

					return bllRetorno.GeraRetorno(true, 
						"ALTERAÇÃO efetuada com sucesso!!!");
				}
				catch
				{
					return bllRetorno.GeraRetorno(false, 
						"Não foi possível efetuar a ALTERAÇÃO!!!");
				}
			}
			else
			{
				return ret;
			}
		}

		public PlanosAssinatura Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			return crud.Carregar<PlanosAssinatura>(_id.ToString());
		}

		public PlanosAssinatura Carregar(int _planoNome, int _periodo)
		{
			CrudDal crud = new CrudDal();
			PlanosAssinatura _retorno = new PlanosAssinatura();

			string _sql = string.Format(
				@"Select *
				  From PlanosAssinaturas
				  (dNomePlano = {0}) And (dPeriodo = {1})", 
				_planoNome, _periodo);

			var ret = crud.ExecutarComando<PlanosAssinatura>(_sql);

			foreach (var item in ret)
			{
				_retorno = item;
			}

			return _retorno;
		}

		public TOPlanosBll CarregarTO(int _idPlano)
		{
			CrudDal crud = new CrudDal();
			TOPlanosBll _retorno = new TOPlanosBll();

				string _sql = string.Format(@"
				SELECT	IdPlano, IdPlanoPagarMe, IdPlanoPagarMeTestes, dNomePlano,
						(Case dNomePlano
							When 1 Then 'Básico'
							When 2 Then 'Intermediário'
							When 3 Then 'Completo'
							When 4 Then 'Receituário'
							When 5 Then 'Prontuário'
						 End) Plano, ValorPlano, dPlanoTp,
						 (Case dPlanoTp
							When 1 Then 'Plano Principal'
							When 2 Then 'Módulo Complementar'
							When 3 Then 'Voucher 30 Dias e 5% Desconto'
							When 4 Then 'Voucher 30 Dias Gratuíto'
							When 5 Then 'Voucher 5% Desconto'
						 End) TipoPlano,
						dPeriodo, 
						(Case dPeriodo
							When 1 Then 'Mensal'
							When 2 Then 'Anual'
						 End) Periodo,
						QtdAnimais, 
						(Case dPeriodo
							When 1 Then 'M'
							When 2 Then 'A'
						 End) AnualMensal, Ativo, IdOperador, IP, 
						DataCadastro
				FROM	PlanosAssinaturas
				Where	(IdPlano = {0}) AND (Ativo = 1)",
				_idPlano);

			var ret = crud.ExecutarComando<TOPlanosBll>(_sql);

			foreach (var _item in ret)
			{
				_retorno = _item;
				_retorno.ValorDescricao = string.Format("{0:c}", _item.ValorPlano) +
					(_item.AnualMensal == 'M' ? "/mês<br></br>" :
						(_item.AnualMensal == 'A' ? "/ano<br></br>" : ""));
			}

			return _retorno;
		}

		public bllRetorno Excluir(PlanosAssinatura _plano)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_plano);

				msg = bllRetorno.GeraRetorno(true,
						  "EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				var erro = err;
				msg = Alterar(_plano);
			}

			return msg;
		}

		public List<TOPlanosBll> Listar()
		{
			CrudDal crud = new CrudDal();

			var lista = from l in crud.Listar<PlanosAssinatura>()
						where l.Ativo == true
						select new TOPlanosBll
						{
							IdPlano = l.IdPlano,
							IdPlanoPagarMe = l.IdPlanoPagarMe,
							IdPlanoPagarMeTestes = l.IdPlanoPagarMeTestes,
							Plano = Funcoes.Funcoes.GetEnumItem<DominiosBll.PlanosAuxNomes>(
								l.dNomePlano).Text.Replace("Percent", "%"),
							ValorPlano = l.ValorPlano,
							dPlanoTp = l.dPlanoTp,
							TipoPlano = Funcoes.Funcoes.GetEnumItem<DominiosBll.PlanosAuxTipos>(
								l.dPlanoTp).Text.Replace("Percent", "%"),
							Periodo = Funcoes.Funcoes.GetEnumItem<DominiosBll.Periodo>(
								l.dPeriodo).Text,
							QtdAnimais = l.QtdAnimais,
							Ativo = l.Ativo,
							IdOperador = l.IdOperador,
							IP = l.IP,
							DataCadastro = l.DataCadastro
						};

			return lista.ToList();
		}

		public List<TOPlanosBll> ListarPlanos()
		{
			CrudDal crud = new CrudDal();

			var lista = from l in crud.Listar<PlanosAssinatura>()
						where (l.Ativo == true) && (l.dPlanoTp == 1)
						select new TOPlanosBll
						{
							IdPlano = l.IdPlano,
							IdPlanoPagarMe = l.IdPlanoPagarMe,
							IdPlanoPagarMeTestes = l.IdPlanoPagarMeTestes,
							Plano = Funcoes.Funcoes.GetEnumItem<DominiosBll.PlanosAuxNomes>(
								l.dNomePlano).Text.Replace("Percent", "%"),
							PlanoComposto = Funcoes.Funcoes.
								GetEnumItem<DominiosBll.PlanosAuxNomes>(l.dNomePlano).Text +
								" - " + Funcoes.Funcoes.GetEnumItem<DominiosBll.Periodo>(
								l.dPeriodo).Text.Replace("Percent", "%"),
							ValorPlano = l.ValorPlano,
							dPlanoTp = l.dPlanoTp,
							TipoPlano = Funcoes.Funcoes.GetEnumItem<DominiosBll.PlanosAuxTipos>(
								l.dPlanoTp).Text.Replace("Percent", "%"),
							Periodo = Funcoes.Funcoes.GetEnumItem<DominiosBll.Periodo>(
								l.dPeriodo).Text,
							QtdAnimais = l.QtdAnimais,
							Ativo = l.Ativo,
							IdOperador = l.IdOperador,
							IP = l.IP,
							DataCadastro = l.DataCadastro
						};

			return lista.ToList();
		}

		public List<TOPlanosBll> ListarPlanos(int _dPlano, int _dTpPlano, string _voucher)
		{
			CrudDal crud = new CrudDal();

			AcessosVigenciaCupomDesconto voucherDcl;
			clAcessosVigenciaCupomBll voucherBll = new clAcessosVigenciaCupomBll();

			List<TOPlanosBll> _listaRet = new List<TOPlanosBll>();
			TOPlanosBll _itemRet;

			string _sql = @"
				SELECT	IdPlano, IdPlanoPagarMe, IdPlanoPagarMeTestes, dNomePlano,
						(Case dNomePlano
							When 1 Then 'Básico'
							When 2 Then 'Intermediário'
							When 3 Then 'Completo'
							When 4 Then 'Receituário'
							When 5 Then 'Prontuário'
						 End) Plano, ValorPlano, dPlanoTp,
						 (Case dPlanoTp
							When 1 Then 'Plano Principal'
							When 2 Then 'Módulo Complementar'
							When 3 Then 'Voucher 30 Dias e 5% Desconto'
							When 4 Then 'Voucher 30 Dias Gratuíto'
							When 5 Then 'Voucher 5% Desconto'
						 End) TipoPlano,
						dPeriodo, 
						(Case dPeriodo
							When 1 Then 'Mensal'
							When 2 Then 'Anual'
						 End) Periodo,
						QtdAnimais, 
						(Case dPeriodo
							When 1 Then 'M'
							When 2 Then 'A'
						 End) AnualMensal, Ativo, IdOperador, IP, 
						DataCadastro
				FROM	PlanosAssinaturas
				Where	(Ativo = 1)";

			if (_dPlano > 0)
			{
				_sql += string.Format(@" AND 
						(dNomePlano = {0}) ", _dPlano);
			}

			if ((_voucher != null) && (_voucher != "") && 
				(Funcoes.Funcoes.ConvertePara.Int(_voucher) > 0))
			{
				voucherDcl = voucherBll.Carregar(_voucher);

				if (voucherDcl != null)
				{
					_sql += string.Format(@" AND 
						(dPlanoTp = {0}) ", voucherDcl.dPlanoTp);
				}
				else
				{
					_sql += string.Format(@" AND 
						(dPlanoTp = {0}) ", _dTpPlano);
				}
			}
			else if (_dTpPlano > 0)
			{
				_sql += string.Format(@" AND 
						(dPlanoTp = {0}) ", _dTpPlano);
			}

			_sql += @"
				ORDER BY ValorPlano";

			var lista = crud.Listar<TOPlanosBll>(_sql).ToList();

			foreach (var item in lista)
			{
				_itemRet = new TOPlanosBll();

				_itemRet = item;
				_itemRet.ValorDescricao = string.Format("{0:c}", _itemRet.ValorPlano) +
					(_itemRet.AnualMensal == 'M' ? "/mês<br></br>" :
						(_itemRet.AnualMensal == 'A' ? "/ano<br></br>" : ""));
				_itemRet.PlanoComposto = Funcoes.Funcoes.
					GetEnumItem<DominiosBll.PlanosAuxNomes>(_itemRet.dNomePlano.Value).Text +
					" - " + Funcoes.Funcoes.GetEnumItem<DominiosBll.Periodo>(
					_itemRet.dPeriodo.Value).Text.Replace("Percent", "%");

				_listaRet.Add(_itemRet);
			}

			return _listaRet.ToList();
		}

		public ListItem[] ListarNomePlano()
		{
			ListItem[] plano = Funcoes.Funcoes.GetEnumList<DominiosBll.PlanosAuxNomes>();
			ListItem[] _retList = new ListItem[plano.Count()];
			ListItem _retItem;

			for (int i = 0; i < plano.Length; i++)
			{
				_retItem = plano[i];
				_retItem.Text = plano[i].Text.Replace("Percent", "%");

				_retList[i] = new ListItem(_retItem.Text, _retItem.Value);
			}

			return _retList;
		}

		public ListItem[] ListarTipoPlano()
		{
			ListItem[] tipo = Funcoes.Funcoes.GetEnumList<DominiosBll.PlanosAuxTipos>();
			ListItem[] _retList = new ListItem[tipo.Count()];
			ListItem _retItem;

			for (int i = 0; i < tipo.Length; i++)
			{
				_retItem = tipo[i];
				_retItem.Text = tipo[i].Text.Replace("Percent", "%");

				_retList[i] = new ListItem(_retItem.Text, _retItem.Value);
			}

			return _retList;
		}

		public ListItem[] ListarPeriodo()
		{
			ListItem[] periodo = Funcoes.Funcoes.GetEnumList<DominiosBll.Periodo>();

			return periodo;
		}
	}
}
