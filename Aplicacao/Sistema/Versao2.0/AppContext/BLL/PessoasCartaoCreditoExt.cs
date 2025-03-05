using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Text.RegularExpressions;

namespace BLL
{
	public static class clPessoasCartaoCreditoExt
	{
		public static bllRetorno ValidarRegras(this PessoasCartaoCredito _cartao,
			bool _insersao)
		{
			bool regReativ = false;
			
			if (_cartao.IdPessoa <= 0)
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo PESSOA deve ser selecionado!");
			}
			else if (_cartao.NrCartao == "")
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo NÚMERO DO CARTÃO deve ser preenchido!");
			}
			else if (_cartao.CodSeg == "")
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo CÓDIGO DE SEGURANÇA deve ser preenchido!");
			}
			else if (_cartao.VencimCartao == "")
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo VENCIMENTO DO CARTÃO deve ser preenchido!");
			}
			else if (_cartao.NomeCartao == "")
			{
				return
					bllRetorno.GeraRetorno(false,
						"Campo NOME DO CLIENTE DO CARTÃO deve ser preenchido!");
			}
			else if ((RegistroDuplicado(_cartao, true)) && (_insersao))
			{
				return bllRetorno.GeraRetorno(false, "Registro Duplicado!!!");
			}

			if ((RegistroDuplicado(_cartao, false)) && (_insersao))
			{
				regReativ = EfetuaUpdate(_cartao);
			}

			return bllRetorno.GeraRetorno(true, (!regReativ ? "Dados Válidos!" : 
				"Registro Reativado"));
		}

		private static bool EfetuaUpdate(PessoasCartaoCredito _cartao)
		{
			CrudDal crud = new CrudDal();

			int _upd = crud.ExecutarComando(string.Format(@"
				Update PessoasCartaoCredito Set
					NomeCartao = '{2}',
					CodSeg = '{3}',
					VencimCartao = '{4}',
					Ativo = 1
				Where (IdPessoa = {0}) AND (NrCartao = '{1}')", 
				_cartao.IdPessoa, Funcoes.Funcoes.Seguranca.Criptografar(
					_cartao.NrCartao), _cartao.NomeCartao,
					_cartao.CodSeg, _cartao.VencimCartao));

			return Funcoes.Funcoes.ConvertePara.Bool(_upd);
		}

		private static bool RegistroDuplicado(PessoasCartaoCredito _cartao, 
			bool? _ativo)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(
				@"Select Count(IdCartao) Total
				  From  PessoasCartaoCredito
				  Where (IdPessoa = {0}) AND (NrCartao = '{1}') And 
						(Ativo = {2})", _cartao.IdPessoa, 
				Funcoes.Funcoes.Seguranca.Criptografar(_cartao.NrCartao),
				(_ativo.Value ? 1 : 0));

			int ret = crud.ExecutarComandoTipoInteiro(_sql);

			return Funcoes.Funcoes.ConvertePara.Bool(ret);
		}
	}
}
