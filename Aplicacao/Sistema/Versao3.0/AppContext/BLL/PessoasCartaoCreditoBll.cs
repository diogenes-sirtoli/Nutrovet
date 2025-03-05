using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;
using DAL;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace BLL
{
	public class clPessoasCartaoCreditoBll
	{
		public bllRetorno Inserir(PessoasCartaoCredito _cartao)
		{
			bllRetorno ret = _cartao.ValidarRegras(true);
			CrudDal crud = new CrudDal();

			try
			{
				if (ret.retorno)
				{
					if (ret.mensagem != "Registro Reativado")
					{
						PessoasCartaoCredito _cartaoInserir = CriptografarDados(_cartao);
						crud.Inserir(_cartaoInserir);
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

		public bllRetorno Alterar(PessoasCartaoCredito _cartao)
		{
            bllRetorno ret = _cartao.ValidarRegras(false);
            CrudDal crud = new CrudDal();

            try
            {
                if (ret.retorno)
                {
                    if (ret.mensagem != "Registro Reativado")
                    {
                        PessoasCartaoCredito _cartaoInserir = CriptografarDados(_cartao);
                        crud.Alterar(_cartaoInserir);
                    }

                    return bllRetorno.GeraRetorno(true,
                        "ALTERAÇÃO efetuada com sucesso!!!");
                }
                else
                {
                    return ret;
                }
            }
            catch
            {
                return bllRetorno.GeraRetorno(false,
                    "Não foi possível efetuar a ALTERAÇÃO!!!");
            }
        }

		public PessoasCartaoCredito Carregar(int _id)
		{
			CrudDal crud = new CrudDal();

			string _sql = string.Format(
				@"Select *
				  From PessoasCartaoCredito
				  Where (IdCartao = {0})", _id);

			PessoasCartaoCredito ret = crud.ExecutarComando<PessoasCartaoCredito>(
				_sql).FirstOrDefault();

			if (ret != null)
			{
				PessoasCartaoCredito _itemRet = DescriptografarDados(ret);
			}

			return ret;
		}

		public TOPessoasCartaoCreditoBll CarregarTO(int _id)
		{
			CrudDal crud = new CrudDal();
			TOPessoasCartaoCreditoBll _ret = new TOPessoasCartaoCreditoBll();

			string _sql = string.Format(@"
				SELECT	cart.IdPessoa, p.Nome, p.CPF, p.CNPJ, 
						p.Passaporte, p.DocumentosOutros,
						cart.IdCartao, cart.NrCartao, cart.CodSeg, 
						cart.dBandeira,
						(CASE cart.dBandeira 
							WHEN 1 THEN 'Visa' 
							WHEN 2 THEN 'Master' 
							WHEN 3 THEN 'Dinners' 
							WHEN 4 THEN 'Elo' 
							WHEN 5 THEN 'HiperCard' 
							WHEN 6 THEN 'American Express' 
						END) AS Bandeira,
						cart.VencimCartao, cart.NomeCartao, cart.Ativo, 
						cart.IdOperador, cart.IP, cart.DataCadastro
				FROM	Pessoas AS p INNER JOIN
							PessoasCartaoCredito AS cart ON p.IdPessoa = cart.IdPessoa
				WHERE	(cart.IdCartao = {0}) ", _id);

			var lista = crud.ExecutarComando<TOPessoasCartaoCreditoBll>(_sql);

			if (lista != null)
			{
				foreach (TOPessoasCartaoCreditoBll item in lista)
				{
					TOPessoasCartaoCreditoBll _itemRet = DescriptografarDados(item);

					_ret = _itemRet;
					_ret.NrCartaoComposto = _itemRet.NrCartao + " - " + 
						_itemRet.VencimCartao.Insert(2, "/") + " - " + _itemRet.CodSeg;
				}
			}

			return _ret;
		}

		public TOPessoasCartaoCreditoBll Carregar(int _idPessoa, string _cartao)
		{
			CrudDal crud = new CrudDal();
			TOPessoasCartaoCreditoBll _ret = new TOPessoasCartaoCreditoBll();

			string _sql = string.Format(@"
				SELECT	cart.IdPessoa, p.Nome, p.CPF, p.CNPJ, 
						p.Passaporte, p.DocumentosOutros,
						cart.IdCartao, cart.NrCartao, cart.CodSeg, 
						cart.dBandeira,
						(CASE cart.dBandeira 
							WHEN 1 THEN 'Visa' 
							WHEN 2 THEN 'Master' 
							WHEN 3 THEN 'Dinners' 
							WHEN 4 THEN 'Elo' 
							WHEN 5 THEN 'HiperCard' 
							WHEN 6 THEN 'American Express' 
						END) AS Bandeira,
						cart.VencimCartao, cart.NomeCartao, cart.Ativo, 
						cart.IdOperador, cart.IP, cart.DataCadastro
				FROM	Pessoas AS p INNER JOIN
							PessoasCartaoCredito AS cart ON p.IdPessoa = cart.IdPessoa
				WHERE	Where (cart.IdPessoa = {0}) AND (cart.NrCartao = '{1}') ",
				_idPessoa, Funcoes.Funcoes.Seguranca.Criptografar(_cartao));

			var lista = crud.ExecutarComando<TOPessoasCartaoCreditoBll>(_sql);

			if (lista != null)
			{
				foreach (TOPessoasCartaoCreditoBll item in lista)
				{
					TOPessoasCartaoCreditoBll _itemRet = DescriptografarDados(item);

					_ret = _itemRet;
					_ret.NrCartaoComposto = _itemRet.NrCartao + " - " + 
						_itemRet.VencimCartao.Insert(2, "/") + " - " + _itemRet.CodSeg;
				}
			}

			return _ret;
		}

		public bllRetorno Excluir(PessoasCartaoCredito _cartao)
		{
			CrudDal crud = new CrudDal();
			bllRetorno msg = new bllRetorno();

			try
			{
				crud.Excluir(_cartao);

				msg = bllRetorno.GeraRetorno(true,
						  "EXCLUSÃO efetuada com sucesso!!!");
			}
			catch (Exception err)
			{
				var erro = err;
				msg = Alterar(_cartao);
			}

			return msg;
		}

		public List<TOPessoasCartaoCreditoBll> Listar(int _idPessoa, bool? _ativo)
		{
			CrudDal crud = new CrudDal();
			List<TOPessoasCartaoCreditoBll> _retorno = new List<TOPessoasCartaoCreditoBll>();
			TOPessoasCartaoCreditoBll _itemRet;

			string _sql = string.Format(@"
				SELECT	cart.IdPessoa, p.Nome, p.CPF, p.CNPJ, 
						p.Passaporte, p.DocumentosOutros,
						cart.IdCartao, cart.NrCartao, cart.CodSeg, 
						cart.dBandeira,
						(CASE cart.dBandeira 
							WHEN 1 THEN 'Visa' 
							WHEN 2 THEN 'Master' 
							WHEN 3 THEN 'Dinners' 
							WHEN 4 THEN 'Elo' 
							WHEN 5 THEN 'HiperCard' 
							WHEN 6 THEN 'American Express' 
						END) AS Bandeira,
						cart.VencimCartao, cart.NomeCartao, cart.Ativo, 
						cart.IdOperador, cart.IP, cart.DataCadastro
				FROM	Pessoas AS p INNER JOIN
							PessoasCartaoCredito AS cart ON p.IdPessoa = cart.IdPessoa
				WHERE	(p.IdPessoa = {0}) ", _idPessoa);

			if (_ativo != null)
			{
				_sql += string.Format(@"AND
						(cart.Ativo = {0})", (_ativo.Value ? 1 : 0));
			}

			var lista = crud.Listar<TOPessoasCartaoCreditoBll>(_sql);

			if (lista != null)
			{
				foreach (TOPessoasCartaoCreditoBll item in lista)
				{
					_itemRet = DescriptografarDados(item);

					string _nrCartao = (_itemRet.NrCartao != null && _itemRet.NrCartao != "" ?
						_itemRet.NrCartao : "");
					string _vencimento = (_itemRet.VencimCartao != null &&
						_itemRet.VencimCartao != "" && _itemRet.VencimCartao.Length > 3 ?
							_itemRet.VencimCartao.Insert(2, "/") : "");
					string _codSeg = (_itemRet.CodSeg != null && _itemRet.CodSeg != "" ?
						" - " + _itemRet.CodSeg : "");


					_itemRet.NrCartaoComposto = (_nrCartao != "" ? _nrCartao + 
						(_vencimento != "" ? " - " + _itemRet.VencimCartao.Insert(2, "/") +
							_codSeg : "") : "");

					_retorno.Add(_itemRet);
				}
			}

			return _retorno.OrderByDescending(o => o.VencimCartao).ToList();
		}

		public ListItem[] ListarBandeira()
		{
			ListItem[] periodo = Funcoes.Funcoes.GetEnumList<
				DominiosBll.CartaoCreditoAuxBandeiras>();

			return periodo;
		}

		public PessoasCartaoCredito CriptografarDados(PessoasCartaoCredito _cartao)
		{
			_cartao.NrCartao = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.NrCartao);
			_cartao.CodSeg = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.CodSeg);
			_cartao.VencimCartao = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.VencimCartao);
			_cartao.NomeCartao = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.NomeCartao);

			return _cartao;
		}

		public PessoasCartaoCredito DescriptografarDados(PessoasCartaoCredito _cartao)
		{
			_cartao.NrCartao = Funcoes.Funcoes.Seguranca.Descriptografar(_cartao.NrCartao);
			_cartao.CodSeg = Funcoes.Funcoes.Seguranca.Descriptografar(_cartao.CodSeg);
			_cartao.VencimCartao = Funcoes.Funcoes.Seguranca.Descriptografar(
				_cartao.VencimCartao);
			_cartao.NomeCartao = Funcoes.Funcoes.Seguranca.Descriptografar(
				_cartao.NomeCartao);

			return _cartao;
		}

		public TOPessoasCartaoCreditoBll CriptografarDados(TOPessoasCartaoCreditoBll _cartao)
		{
			_cartao.NrCartao = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.NrCartao);
			_cartao.CodSeg = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.CodSeg);
			_cartao.VencimCartao = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.VencimCartao);
			_cartao.NomeCartao = Funcoes.Funcoes.Seguranca.Criptografar(_cartao.NomeCartao);

			return _cartao;
		}

		public TOPessoasCartaoCreditoBll DescriptografarDados(TOPessoasCartaoCreditoBll _cartao)
		{
			_cartao.NrCartao = Funcoes.Funcoes.Seguranca.Descriptografar(_cartao.NrCartao);
			_cartao.CodSeg = Funcoes.Funcoes.Seguranca.Descriptografar(_cartao.CodSeg);
			_cartao.VencimCartao = Funcoes.Funcoes.Seguranca.Descriptografar(
				_cartao.VencimCartao);
			_cartao.NomeCartao = Funcoes.Funcoes.Seguranca.Descriptografar(
				_cartao.NomeCartao);

			return _cartao;
		}

		public int BandeiraCreditCard(string _cardNumber)
		{
			//1   ELO
			//2   HIPERCARD
			//3   DINERS
			//4   AMEX
			//5   MASTERCARD
			//6   VISA

			// Visa: ^4[0-9]{12}(?:[0-9]{3})?$ All Visa card numbers start with a 4. New cards have 16 digits. Old cards have 13.
			// MasterCard: ^5[1-5][0-9]{14}$ All MasterCard numbers start with the numbers 51 through 55. All have 16 digits.
			// American Express: ^3[47][0-9]{13}$ American Express card numbers start with 34 or 37 and have 15 digits.
			// Diners Club: ^3(?:0[0-5]|[68][0-9])[0-9]{11}$ Diners Club card numbers begin with 300 through 305, 36 or 38. All have 14 digits. There are Diners Club cards that begin with 5 and have 16 digits. These are a joint venture between Diners Club and MasterCard, and should be processed like a MasterCard.
			// Discover: ^6(?:011|5[0-9]{2})[0-9]{12}$ Discover card numbers begin with 6011 or 65. All have 16 digits.
			// JCB: ^(?:2131|1800|35\d{3})\d{11}$ JCB cards beginning with 2131 or 1800 have 15 digits. JCB cards beginning with 35 have 16 digits.

			Regex rgCartaELO = new Regex(@"^(40117[8-9]|431274|438935|451416|457393|45763[1-2]|506(699|7[0-6][0-9]|77[0-8])|509\d{3}|504175|627780|636297|636368|65003[1-3]|6500(3[5-9]|4[0-9]|5[0-1])|6504(0[5-9]|[1-3][0-9])|650(4[8-9][0-9]|5[0-2][0-9]|53[0-8])|6505(4[1-9]|[5-8][0-9]|9[0-8])|6507(0[0-9]|1[0-8])|65072[0-7]|6509(0[1-9]|1[0-9]|20)|6516(5[2-9]|[6-7][0-9])|6550([0-1][0-9]|2[1-9]|[3-4][0-9]|5[0-8]))$/");
			Regex rgCartaHIPERCARD = new Regex(@"/^(606282\d{10}(\d{3})?)|(3841\d{15})$/");
			Regex rgCartaDiners = new Regex(@"/^3(0[0-5]|[68]\d)\d{11}$/");
			Regex rgCartaAmex = new Regex(@"/^3[47]\d{13}$/");
			Regex rgCartaMaster = new Regex(@"/^(5[1-5]\d{4}|677189)\d{10}$/");
			Regex rgCartaVisa = new Regex(@"/^4\d{12}(\d{3})?$/");

			Regex rgCartaDiscover = new Regex(@"/^6(?:011|5[0-9]{2})[0-9]{12}/");
			Regex rgCartaJCB = new Regex(@"/^(?:2131|1800|35\d{3})\d{11}$/");
			Regex rgCartaAmerican = new Regex(@"^3[47]\\d{0,13}");

			if (rgCartaVisa.IsMatch(_cardNumber))
			{

				return 1; // "VISA";
			}

			if (rgCartaMaster.IsMatch(_cardNumber))
			{
				return 2; // "MASTER";
			}

			if (rgCartaAmex.IsMatch(_cardNumber))
			{
				return 3; // "AMEX";
			}

			if (rgCartaDiners.IsMatch(_cardNumber))
			{
				return 4; // "DINERS";
			}

			if (rgCartaDiscover.IsMatch(_cardNumber))
			{
				return 5; // "DISCOVER";
			}

			if (rgCartaJCB.IsMatch(_cardNumber))
			{
				return 6; // "JCB";
			}

			if (rgCartaAmerican.IsMatch(_cardNumber))
			{
				return 7; // "AMERICAN EXPRESS";
			}

			if (rgCartaHIPERCARD.IsMatch(_cardNumber))
			{
				return 8; // "HIPERCARD";
			}

			if (rgCartaELO.IsMatch(_cardNumber))
			{
				return 9; // "ELO";
			}
			return 10;

		}
	}
}
